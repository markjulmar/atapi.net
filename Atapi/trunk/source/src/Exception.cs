// Exception.cs
//
// This is a part of the TAPI Applications Classes .NET library (ATAPI)
//
// Copyright (c) 2005-2013 JulMar Technology, Inc.
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Runtime.Serialization;
using System.Globalization;
using System.Runtime.InteropServices;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class is used to bubble up underlying Tapi errors to the application
    /// </summary>
    [Serializable]
    public class TapiException : Exception
    {
        private readonly long _err;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TapiException() 
            : this(string.Empty, 0, null)
        {
        }

        /// <summary>
        /// Standard exception
        /// </summary>
        /// <param name="msg">Message</param>
        public TapiException(string msg)
            : this(msg, 0, null)
        {
        }

        /// <summary>
        /// Standard exception
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="innerException">Inner Exception</param>
        public TapiException(string msg, Exception innerException)
            : this(msg, 0, innerException)
        {
        }

        /// <summary>
        /// Tapi exception constructor
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="rc">Tapi RC</param>
        public TapiException(string msg, long rc)
            : this(msg, rc, null)
        {
        }

        /// <summary>
        /// Tapi exception constructor
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="rc">Tapi RC</param>
        /// <param name="innerException">Inner Exception passed to base class</param>
        public TapiException(string msg, long rc, Exception innerException)
            : base(string.Format(CultureInfo.CurrentCulture, "{0} [0x{1:X}] {2}", msg, rc, LookupErrorMessage(rc)), innerException)
        {
            _err = rc;
        }

        /// <summary>
        /// Serialization constructor
        /// </summary>
        /// <param name="info">Serialization Info</param>
        /// <param name="ctx">Streaming Context</param>
        protected TapiException(SerializationInfo info, StreamingContext ctx)
            :base(info, ctx)
        {
        }

        /// <summary>
        /// The TAPI error code reported by the TAPI api.
        /// </summary>
        public long Error
        {
            get { return _err; }
        }

        [DllImport("kernel32.dll", SetLastError=true)]
        static extern uint FormatMessage(uint dwFlags, IntPtr lpSource,
           uint dwMessageId, uint dwLanguageId, out IntPtr lpBuffer,
           uint nSize, IntPtr arguments);
        
        [DllImport("kernel32.dll", SetLastError=true)]
        static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr handle, uint dwFlags);

        // From WinBase.h
        const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
        const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
        const uint FORMAT_MESSAGE_FROM_HMODULE = 0x00000800;
        const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        const uint LOAD_LIBRARY_AS_DATAFILE = 0x00000002;

        static uint RemapErrorCode(uint rc)
        {
            //if (__ErrCode__ is a TAPIERR)
            //            strip off high word

            //            else if (__ErrCode__ is a PHONEERR)
            //                strip off 0x90000000
            //                add 0xE000

            //                else
            //                    strip off 0x80000000
            //                    add 0xF000
            if (rc > 0xFFFF0000)
                return rc & 0xffff;
            if (rc >= 0x90000000)
                return (rc - 0x90000000) + 0xf000;
            if (rc >= 0x80000000)
                return (rc - 0x80000000) + 0xe000;

            return rc;
        }

        static string LookupErrorMessage(long rc)
        {
            uint errorCode = RemapErrorCode((uint)rc);

            // Check all the places where TAPI error messages might exist.
            string message = FormatErrorMessage(errorCode, "TAPI32.DLL");
            if (String.IsNullOrEmpty(message))
                message = FormatErrorMessage(errorCode, "TAPIUI.DLL");
            if (String.IsNullOrEmpty(message))
                message = FormatErrorMessage(errorCode, null);
            return message;
        }

        static string FormatErrorMessage(uint errorCode, string dllName)
        {
            IntPtr lpMsgBuf = IntPtr.Zero;
            uint flags = FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;
            IntPtr modHandle = IntPtr.Zero;

            if (!String.IsNullOrEmpty(dllName))
            {
                flags = FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_HMODULE | FORMAT_MESSAGE_IGNORE_INSERTS;
                modHandle = LoadLibraryEx(dllName, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
                System.Diagnostics.Debug.Assert(modHandle != IntPtr.Zero);
            }

            try
            {
                if (FormatMessage(
                    flags,
                    modHandle,
                    errorCode,
                    0, // MAKELANGID(LANG_NEUTRAL,SUBLANG_DEFAULT)
                    out lpMsgBuf,
                    0,
                    IntPtr.Zero) > 0)

                    return Marshal.PtrToStringAnsi(lpMsgBuf);
            }
            finally
            {
                // Free the buffer.
                if (lpMsgBuf != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpMsgBuf);
            }

            return String.Empty;
        }
    }

}
