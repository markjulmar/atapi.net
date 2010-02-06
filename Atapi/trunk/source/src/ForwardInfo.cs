// ForwardInfo.cs
//
// This is a part of the TAPI Applications Classes .NET library (ATAPI)
//
// Copyright (c) 2005-2010 JulMar Technology, Inc.
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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using JulMar.Atapi.Interop;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class describes a single set of forwarding instructions for an address or line device.
    /// </summary>
    public class ForwardInfo
    {
        /// <summary>
        /// Types of forwarding.
        /// </summary>
        public ForwardingMode ForwardMode = ForwardingMode.Unknown;
        /// <summary>
        /// Type of caller address. Only used in specific forwarding cases (BusyNoAnswerSpecific, NoAnswerSpecific, UnconditionalSpecific, BusySpecific).
        /// </summary>
        public AddressType CallerAddressType = AddressType.PhoneNumber;
        /// <summary>
        /// Address of a caller to be forwarded. Only used in specific forwarding cases (BusyNoAnswerSpecific, NoAnswerSpecific, UnconditionalSpecific, BusySpecific).
        /// </summary>
        public string CallerAddress = string.Empty;
        /// <summary>
        /// Country code of the destination address to which the call is to be forwarded. 
        /// </summary>
        public int DestinationCountryCode;
        /// <summary>
        /// Destination address type.
        /// </summary>
        public AddressType DestinationAddressType = AddressType.PhoneNumber;
        /// <summary>
        /// The address of the address where calls are to be forwarded.
        /// </summary>
        public string DestinationAddress = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ForwardInfo()
        {
        }

        /// <summary>
        /// Most common form of constructor.
        /// </summary>
        /// <param name="mode">Forwarding mode</param>
        /// <param name="destinationCountryCode">Destination country code, zero for default</param>
        /// <param name="destinationAddress">Destination address</param>
        public ForwardInfo(ForwardingMode mode, int destinationCountryCode, string destinationAddress)
        {
            ForwardMode = mode;
            DestinationCountryCode = destinationCountryCode;
            DestinationAddress = destinationAddress;
        }

        /// <summary>
        /// Constructor for typical forwarding call when destination is not a phone number.
        /// </summary>
        /// <param name="mode">Forwarding mode</param>
        /// <param name="destinationCountryCode">Destination country code, zero for default</param>
        /// <param name="destinationType">Destination address type</param>
        /// <param name="destinationAddress">Destination address</param>
        public ForwardInfo(ForwardingMode mode, int destinationCountryCode, AddressType destinationType, string destinationAddress)
        {
            ForwardMode = mode;
            DestinationCountryCode = destinationCountryCode;
            DestinationAddressType = destinationType;
            DestinationAddress = destinationAddress;
        }

        /// <summary>
        /// Specific forwarding constructor
        /// </summary>
        /// <param name="mode">Forwarding mode - must be BusyNoAnswerSpecific, NoAnswerSpecific, UnconditionalSpecific or BusySpecific</param>
        /// <param name="callerAddress">Caller address</param>
        /// <param name="destinationCountryCode">Destination country code, zero for default</param>
        /// <param name="destinationAddress">Destination address</param>
        public ForwardInfo(ForwardingMode mode, string callerAddress, int destinationCountryCode, string destinationAddress)
        {
            ForwardMode = mode;
            CallerAddress = callerAddress;
            DestinationCountryCode = destinationCountryCode;
            DestinationAddress = destinationAddress;
        }

        /// <summary>
        /// Constructor for specific forwarding call when caller or destination is not a phone number.
        /// </summary>
        /// <param name="mode">Forwarding mode - must be BusyNoAnswerSpecific, NoAnswerSpecific, UnconditionalSpecific or BusySpecific</param>
        /// <param name="callerType">Caller address type</param>
        /// <param name="callerAddress">Caller address</param>
        /// <param name="destinationCountryCode">Destination country code, zero for default</param>
        /// <param name="destinationType">Destination address type</param>
        /// <param name="destinationAddress">Destination address</param>
        public ForwardInfo(ForwardingMode mode, AddressType callerType, string callerAddress, int destinationCountryCode, AddressType destinationType, string destinationAddress)
        {
            ForwardMode = mode;
            if (Enum.IsDefined(typeof(AddressType), callerType))
                CallerAddressType = callerType;
            CallerAddress = callerAddress;
            DestinationCountryCode = destinationCountryCode;
            if (Enum.IsDefined(typeof(AddressType), destinationType))
                DestinationAddressType = destinationType;
            DestinationAddress = destinationAddress;
        }

        /// <summary>
        /// Provides string implementation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return CallerAddress.Length > 0 
                ? string.Format("{0} from {1}:{2} fwd to {3}:{4}", ForwardMode, CallerAddressType, CallerAddress, DestinationAddressType, DestinationAddress) 
                : string.Format("{0} to {1}:{2}", ForwardMode, DestinationAddressType, DestinationAddress);
        }

        /// <summary>
        /// This method converts an array of forwarding information instructions to a LINEFORWARDLIST array
        /// </summary>
        /// <param name="forwardInstructions">Input array</param>
        /// <returns></returns>
        internal static IntPtr ProcessForwardList(ForwardInfo[] forwardInstructions)
        {
            if (forwardInstructions == null)
                throw new ArgumentNullException("forwardInstructions");
            if (forwardInstructions.Length == 0)
                throw new ArgumentException("forwardInstructions must contain at least one entry");

            var lfl = new LINEFORWARDLIST {dwNumEntries = forwardInstructions.Length};
            lfl.dwTotalSize = Marshal.SizeOf(lfl) + (Marshal.SizeOf(typeof(LINEFORWARD)) * lfl.dwNumEntries);
            int pos = lfl.dwTotalSize;

            var arrBuff = new List<LINEFORWARD>();
            for (int i = 0; i < forwardInstructions.Length; i++)
            {
                LINEFORWARD lf = forwardInstructions[i].ConvertToTapiStructure(ref pos);
                arrBuff.Add(lf);
                lfl.dwTotalSize += (lf.dwCallerAddressSize + lf.dwDestAddressSize);
            }

            // Create a buffer to hold our entire block.
            IntPtr lpFl = Marshal.AllocHGlobal(lfl.dwTotalSize);
            var buff = new byte[lfl.dwTotalSize];

            // Alloc temp blocks
            int lfs = Marshal.SizeOf(typeof(LINEFORWARD));
            IntPtr ip = Marshal.AllocHGlobal(lfs);
            var ipBuff = new byte[lfs];

            // Copy in the header and move it to our working buffer.
            Marshal.StructureToPtr(lfl, lpFl, false);
            Marshal.Copy(lpFl, buff, 0, lfl.dwTotalSize);

            // Copy each of the structures over.
            pos = Marshal.SizeOf(lfl);
            foreach (LINEFORWARD lf in arrBuff)
            {
                Marshal.StructureToPtr(lf, ip, true);
                Marshal.Copy(ip, ipBuff, 0, lfs);
                Array.Copy(ipBuff, 0, buff, pos, lfs);
                pos += lfs;
            }
            Marshal.FreeHGlobal(ip);

            // Go back through and add each of the string values.
            for (int i = 0; i < arrBuff.Count; i++)
            {
                if (!String.IsNullOrEmpty(forwardInstructions[i].CallerAddress))
                {
                    System.Diagnostics.Debug.Assert(pos == arrBuff[i].dwCallerAddressOffset);
                    Array.Copy(Encoding.Unicode.GetBytes(forwardInstructions[i].CallerAddress), 0, buff, pos, arrBuff[i].dwCallerAddressSize);
                    pos += arrBuff[i].dwCallerAddressSize;
                }
                if (!String.IsNullOrEmpty(forwardInstructions[i].DestinationAddress))
                {
                    System.Diagnostics.Debug.Assert(pos == arrBuff[i].dwDestAddressOffset);
                    Array.Copy(Encoding.Unicode.GetBytes(forwardInstructions[i].DestinationAddress), 0, buff, pos, arrBuff[i].dwDestAddressSize);
                    pos += arrBuff[i].dwDestAddressSize;
                }
            }

            // Finally, move it back to our IntPtr
            Marshal.Copy(buff, 0, lpFl, lfl.dwTotalSize);

            return lpFl;
        }

        /// <summary>
        /// This converts the managed version to the unmanaged version of the structure.
        /// </summary>
        /// <returns></returns>
        internal LINEFORWARD ConvertToTapiStructure(ref int pos)
        {
            var lf = new LINEFORWARD { dwForwardMode = (int) ForwardMode, dwCallerAddressType = (int) CallerAddressType };

            if (!String.IsNullOrEmpty(CallerAddress))
            {
                lf.dwCallerAddressOffset = pos;
                lf.dwCallerAddressSize = Encoding.Unicode.GetByteCount(CallerAddress);
                pos += lf.dwCallerAddressSize;
            }
            lf.dwDestCountryCode = DestinationCountryCode;
            lf.dwDestAddressType = (int) DestinationAddressType;
            if (!String.IsNullOrEmpty(DestinationAddress))
            {
                lf.dwDestAddressOffset = pos;
                lf.dwDestAddressSize = Encoding.Unicode.GetByteCount(DestinationAddress);
                pos += lf.dwDestAddressSize;
            }

            return lf;
        }
    }
}
