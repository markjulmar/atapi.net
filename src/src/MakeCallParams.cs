// MakeCallParams.cs
//
// This is a part of the TAPI Applications Classes .NET library (ATAPI)
//
// Copyright (c) 2005-2018 JulMar Technology, Inc.
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
using System.Text;
using System.Runtime.InteropServices;
using JulMar.Atapi.Interop;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class holds optional data for MakeCall to use to place a call
    /// </summary>
    public class MakeCallParams
    {
        /// <summary>
        /// Value that specifies the bearer mode for the call. 
        /// </summary>
        public BearerModes BearerMode = BearerModes.Voice;
        /// <summary>
        /// Value that specifies the data rate range requested for the call's data stream in bps (bits per second).
        /// </summary>
        public int MinRate;
        /// <summary>
        /// Value that specifies the data rate range requested for the call's data stream in bps (bits per second).
        /// </summary>
        public int MaxRate;
        /// <summary>
        /// Requested media mode for the call.
        /// </summary>
        public MediaModes MediaMode = MediaModes.InteractiveVoice;
        /// <summary>
        /// Origination address
        /// </summary>
        public string OriginationAddress = string.Empty;
        /// <summary>
        /// Target address (used for SetupConference, SetupTransfer and PrepareAddToConference)
        /// </summary>
        public string TargetAddress = string.Empty;
        /// <summary>
        /// UserUser data to be passed along the call.
        /// </summary>
        public byte[] UserUserInfo;
        /// <summary>
        /// Number of seconds, after the completion of dialing, that the call should wait in the PROCEEDING or RINGBACK state, 
        /// before abandoned by the service provider with a LINECALLSTATE_DISCONNECTED and LINEDISCONNECTMODE_NOANSWER. A value of zero indicates 
        /// that the application does not desire automatic call abandonment. 
        /// </summary>
        public int NoAnswerTimeout;
        /// <summary>
        /// Duration of a comma in the dialable address, in milliseconds. 
        /// </summary>
        public int DialPause;
        /// <summary>
        /// Interdigit time period between successive digits, in milliseconds.
        /// </summary>
        public int DialSpeed;
        /// <summary>
        /// Duration of a digit, in milliseconds. 
        /// </summary>
        public int DigitDuration;
        /// <summary>
        /// Maximum amount of time to wait for a dial tone when a 'W' is used in the dialable address, in milliseconds. 
        /// </summary>
        public int WaitForDialtoneDuration;

        /// <summary>
        /// The originator identity should be concealed (block caller ID).
        /// </summary>
        public bool BlockCallerId;
        /// <summary>
        /// The called party's phone should be automatically taken offhook.
        /// </summary>
        public bool TakeDestinationOffhook;
        /// <summary>
        /// The caller party's phone should be automatically taken offhook.
        /// </summary>
        public bool TakeOriginationOffhook;
        /// <summary>
        /// This flag is used only when placing a call on an address with predictive dialing capability 
        /// (LINEADDRCAPFLAGS_PREDICTIVEDIALER is on in the dwAddrCapFlags member in LINEADDRESSCAPS). 
        /// The flag must be on to enable the enhanced call progress and/or media device monitoring capabilities 
        /// of the device. If this bit is not on, the call will be placed without enhanced call progress or 
        /// media type monitoring, and no automatic transfer will be initiated based on call state. 
        /// </summary>
        public bool WantPredictiveDialing;
        /// <summary>
        /// The call should be originated on an idle call appearance and not join a call in progress. 
        /// When using the lineMakeCall function, if the IDLE value is not set and there is an existing call 
        /// on the line, the function breaks into the existing call if necessary to make the new call. 
        /// If there is no existing call, the function makes the new call as specified
        /// </summary>
        public bool OriginateOnIdleCall = true;
        /// <summary>
        /// The call should be set up as secure. 
        /// </summary>
        public bool WantSecureCall;

        /// <summary>
        /// Constructor
        /// </summary>
        public MakeCallParams()
        {
            MaxRate = 0;
            MinRate = 0;
        }

        static internal IntPtr ProcessCallParams(int addressId, MakeCallParams param, int callFlags)
        {
            IntPtr lpCp = IntPtr.Zero;

            if (param != null)
            {
                var lcp = new LINECALLPARAMS
                  {
                      dwBearerMode = (int) param.BearerMode,
                      dwMinRate = param.MinRate,
                      dwMaxRate = param.MaxRate,
                      dwMediaMode = (int) param.MediaMode,
                      dwCallParamFlags = callFlags
                  };

                if (param.BlockCallerId)
                    lcp.dwCallParamFlags |= NativeMethods.LINECALLPARAMFLAGS_BLOCKID;
                if (param.TakeDestinationOffhook)
                    lcp.dwCallParamFlags |= NativeMethods.LINECALLPARAMFLAGS_DESTOFFHOOK;
                if (param.TakeOriginationOffhook)
                    lcp.dwCallParamFlags |= NativeMethods.LINECALLPARAMFLAGS_ORIGOFFHOOK;
                if (param.OriginateOnIdleCall)
                    lcp.dwCallParamFlags |= NativeMethods.LINECALLPARAMFLAGS_IDLE;
                if (param.WantSecureCall)
                    lcp.dwCallParamFlags |= NativeMethods.LINECALLPARAMFLAGS_SECURE;
                if (param.WantPredictiveDialing)
                    lcp.dwCallParamFlags |= NativeMethods.LINECALLPARAMFLAGS_PREDICTIVEDIAL;

                lcp.dwUserUserInfoSize = (param.UserUserInfo == null) ? 0 : param.UserUserInfo.Length;
                lcp.dwNoAnswerTimeout = param.NoAnswerTimeout;
                lcp.dwOrigAddressSize = String.IsNullOrEmpty(param.OriginationAddress) ? 0 : param.OriginationAddress.Length;
                lcp.dwTargetAddressSize = String.IsNullOrEmpty(param.TargetAddress) ? 0 : param.TargetAddress.Length;
                lcp.dwAddressMode = NativeMethods.LINEADDRESSMODE_ADDRESSID;
                lcp.dwAddressID = addressId;
                lcp.DialParams.dwDialPause = param.DialPause;
                lcp.DialParams.dwDialSpeed = param.DialSpeed;
                lcp.DialParams.dwDigitDuration = param.DigitDuration;
                lcp.DialParams.dwWaitForDialtone = param.WaitForDialtoneDuration;

                lcp.dwTotalSize = Marshal.SizeOf(lcp);
                if (lcp.dwUserUserInfoSize > 0)
                {
                    lcp.dwUserUserInfoOffset = lcp.dwTotalSize;
                    lcp.dwTotalSize += lcp.dwUserUserInfoSize;
                }
                if (lcp.dwOrigAddressSize > 0)
                {
                    lcp.dwOrigAddressOffset = lcp.dwTotalSize;
                    lcp.dwTotalSize += lcp.dwOrigAddressSize;
                }
                if (lcp.dwTargetAddressSize > 0)
                {
                    lcp.dwTargetAddressOffset = lcp.dwTotalSize;
                    lcp.dwTotalSize += lcp.dwTargetAddressSize;
                }

                lpCp = Marshal.AllocHGlobal(lcp.dwTotalSize);
                Marshal.StructureToPtr(lcp, lpCp, true);
                if (lcp.dwUserUserInfoSize > 0)
                    NativeMethods.WriteByteArray(param.UserUserInfo, lpCp, lcp.dwUserUserInfoOffset);
                if (lcp.dwOrigAddressSize > 0 && param.OriginationAddress != null)
                    NativeMethods.WriteByteArray(Encoding.Default.GetBytes(param.OriginationAddress), lpCp, lcp.dwOrigAddressOffset);
                if (lcp.dwTargetAddressSize > 0 && param.TargetAddress != null)
                    NativeMethods.WriteByteArray(Encoding.Default.GetBytes(param.TargetAddress), lpCp, lcp.dwTargetAddressOffset);
            }

            return lpCp;
        }
    }
}
