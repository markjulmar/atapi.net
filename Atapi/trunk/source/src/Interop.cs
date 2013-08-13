// Interop.cs
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
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Runtime.ConstrainedExecution;

namespace JulMar.Atapi.Interop
{
    /// <summary>
    /// Messages for phones and lines
    /// </summary>
    internal enum TapiEvent
    {
        LINE_ADDRESSSTATE = 0,
        LINE_CALLINFO,
        LINE_CALLSTATE,
        LINE_CLOSE,
        LINE_DEVSPECIFIC,
        LINE_DEVSPECIFICFEATURE,
        LINE_GATHERDIGITS,
        LINE_GENERATE,
        LINE_LINEDEVSTATE,
        LINE_MONITORDIGITS,
        LINE_MONITORMEDIA,
        LINE_MONITORTONE,
        LINE_REPLY,
        LINE_REQUEST,
        PHONE_BUTTON,
        PHONE_CLOSE,
        PHONE_DEVSPECIFIC,
        PHONE_REPLY,
        PHONE_STATE,
        LINE_CREATE,
        PHONE_CREATE,
        LINE_AGENTSPECIFIC,
        LINE_AGENTSTATUS,
        LINE_APPNEWCALL,
        LINE_PROXYREQUEST,
        LINE_REMOVE,
        PHONE_REMOVE,
        LINE_AGENTSESSIONSTATUS,
        LINE_QUEUESTATUS,
        LINE_AGENTSTATUSEX,
        LINE_GROUPSTATUS,
        LINE_PROXYSTATUS,
        LINE_APPNEWCALLHUB,
        LINE_CALLHUBCLOSE,
        LINE_DEVSPECIFICEX,
    };

    /// <summary>
    /// This class holds a TAPI call handle
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class HTCALL : SafeHandle
    {
        internal HTCALL()
            : base(IntPtr.Zero, true)
        {
        }

        internal HTCALL(uint preexistingHandle, bool ownsHandle) 
            : base(new IntPtr((int)preexistingHandle), ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.lineDeallocateCall((uint)handle.ToInt32());
                handle = IntPtr.Zero;
            }
            return true;
        }

        /// <summary>
        /// Fixed according to http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.safehandle.isinvalid(v=vs.80).aspx
        /// </summary>
        public override bool IsInvalid
        {
            get { return IsClosed || handle == IntPtr.Zero; }
        }
    }

    /// <summary>
    /// This class holds a TAPI line handle
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class HTLINE : SafeHandle
    {
        internal HTLINE()
            : base(IntPtr.Zero, true)
        {
        }

        internal HTLINE(uint preexistingHandle, bool ownsHandle)
            : base(new IntPtr((int)preexistingHandle), ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.lineClose((uint)handle.ToInt32());
                handle = IntPtr.Zero;
            }
            return true;
        }

        /// <summary>
        /// Fixed according to http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.safehandle.isinvalid(v=vs.80).aspx
        /// </summary>
        public override bool IsInvalid
        {
            get { return IsClosed || handle == IntPtr.Zero; }
        }
    }

    /// <summary>
    /// This class holds a TAPI phone handle
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class HTPHONE : SafeHandle
    {
        internal HTPHONE()
            : base(IntPtr.Zero, true)
        {
        }

        internal HTPHONE(uint preexistingHandle, bool ownsHandle)
            : base(new IntPtr((int)preexistingHandle), ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.phoneClose((uint)handle.ToInt32());
                handle = IntPtr.Zero;
            }
            return true;
        }

        /// <summary>
        /// Fixed according to http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.safehandle.isinvalid(v=vs.80).aspx
        /// </summary>
        public override bool IsInvalid
        {
            get { return IsClosed || handle == IntPtr.Zero; }
        }
    }

    /// <summary>
    /// This class holds a TAPI application handle
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class HTLINEAPP : SafeHandle
    {
        internal HTLINEAPP()
            : base(IntPtr.Zero, true)
        {
        }

        internal HTLINEAPP(uint preexistingHandle, bool ownsHandle)
            : base(new IntPtr((int)(int)preexistingHandle), ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.lineShutdown((uint)this.handle.ToInt32());
                handle = IntPtr.Zero;
            }
            return true;
        }

        /// <summary>
        /// Fixed according to http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.safehandle.isinvalid(v=vs.80).aspx
        /// </summary>
        public override bool IsInvalid
        {
            get { return IsClosed || handle == IntPtr.Zero; }
        }
    }

    /// <summary>
    /// This class holds a TAPI phone application handle
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class HTPHONEAPP : SafeHandle
    {
        internal HTPHONEAPP()
            : base(IntPtr.Zero, true)
        {
        }

        internal HTPHONEAPP(uint preexistingHandle, bool ownsHandle)
            : base(new IntPtr((int)preexistingHandle), ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.phoneShutdown((uint)this.handle.ToInt32());
                handle = IntPtr.Zero;
            }
            return true;
        }

        /// <summary>
        /// Fixed according to http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.safehandle.isinvalid(v=vs.80).aspx
        /// </summary>
        public override bool IsInvalid
        {
            get { return IsClosed || handle == IntPtr.Zero; }
        }
    }

    /// <summary>
    /// Delegate used to describe the PHONECALLBACK and LINECALLBACK used by TAPI32.DLL to notify the application of events
    /// </summary>
    /// <param name="dwMessage">TAPI Message</param>
    /// <param name="dwParam1">Param1</param>
    /// <param name="dwParam2">Param2</param>
    /// <param name="dwParam3">Param3</param>
    internal delegate void TapiEventCallback(TapiEvent dwMessage, IntPtr dwParam1, IntPtr dwParam2, IntPtr dwParam3);

    /// <summary>
    /// This class holds all the API and structures/constants used by TAPI 2.x
    /// </summary>
    internal static class NativeMethods
    {
        #region TAPI Api definitions
        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineAccept(HTCALL hCall, byte[] UserUserInfo, int dwSize);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineAddToConference(HTCALL hConfCall, HTCALL hConsultCall);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineAnswer(HTCALL hCall, byte[] UserUserInfo, int dwSize);

        [DllImport("tapi32.dll", EntryPoint = "lineBlindTransferW", CharSet = CharSet.Auto)]
        internal static extern int lineBlindTransfer(HTCALL hCall, string destAddress, int countryCode);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineClose(uint hLine);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineCompleteCall(HTCALL hCall, IntPtr lpCompletionId, int mode, int messageId);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineCompleteTransfer(HTCALL hCall, HTCALL htConsult, out uint htConfCall, int dwTransferMode);

        [DllImport("tapi32.dll", EntryPoint = "lineConfigDialogW", CharSet = CharSet.Auto)]
        internal static extern int lineConfigDialog(int dwDeviceID, IntPtr hwndOwner, string lpszDeviceClass);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineDeallocateCall(uint hLine);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineDevSpecific(HTLINE hLine, int dwAddressID, uint hCall, IntPtr lpParams, int dwSize);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineDevSpecificFeature(HTLINE hLine, int dwFeature, IntPtr lpParams, int dwSize);

        [DllImport("tapi32.dll", EntryPoint = "lineDialW", CharSet = CharSet.Auto)]
        internal static extern int lineDial(HTCALL hCall, string destAddress, int countryCode);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineDrop(HTCALL hCall, byte[] UserInfo, int dwSize);

        [DllImport("Tapi32.dll", EntryPoint="lineForwardW", CharSet = CharSet.Auto)]
        internal static extern int lineForward(HTLINE hLine, int bAllAddresses, int dwAddressID, IntPtr lpForwardList, int dwNumRingsNoAnswer, out uint lphConsultCall, IntPtr lpCallParams);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGatherDigits(HTCALL hCall, int dwDigitModes, out StringBuilder buffer, int numDigits, string termDigits, int firstDigitTimeout, int interDigitTimeout);

        [DllImport("Tapi32.dll", EntryPoint="lineGatherDigitsW", CharSet = CharSet.Auto)]
        internal static extern int lineCancelGatherDigits(HTCALL hCall, int dwDigitModes, string buffer, int numDigits, string termDigits, int firstDigitTimeout, int interDigitTimeout);

        [DllImport("Tapi32.dll", EntryPoint="lineGenerateDigitsW", CharSet = CharSet.Auto)]
        internal static extern int lineGenerateDigits(HTCALL hCall, int dwDigitMode, string buffer, int dwDuration);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGenerateTone(HTCALL hCall, int dwToneMode, int dwDuration, int dwNumTones, LINEGENERATETONE[] arrTones);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetAddressCapsW", CharSet = CharSet.Auto)]
        internal static extern int lineGetAddressCaps(HTLINEAPP hLineApp, int dwDeviceID, int dwAddressID, int dwAPIVersion, int dwExtVersion, IntPtr lpAddressCaps);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetAddressStatusW", CharSet = CharSet.Auto)]
        internal static extern int lineGetAddressStatus(HTLINE hLine, int dwAddressID, IntPtr lpAddressStatus);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetCallInfoW", CharSet = CharSet.Auto)]
        internal static extern int lineGetCallInfo(HTCALL hCall, IntPtr lpCallInfo);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetCallStatus", CharSet = CharSet.Auto)]
        internal static extern int lineGetCallStatus(HTCALL hCall, IntPtr lpCallStatus);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGetConfRelatedCalls(HTCALL hCall, IntPtr lpCallList);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetCountryW", CharSet = CharSet.Auto)]
        internal static extern int lineGetCountry(int dwCountryID, int dwAPIVersion, IntPtr lpLineCountryList);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetDevCapsW", CharSet = CharSet.Auto)]
        internal static extern int lineGetDevCaps(HTLINEAPP hLineApp, int dwDeviceID, int dwAPIVersion, int dwExtVersion, IntPtr lpLineDevCaps);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGetDevConfig(int dwDeviceID, IntPtr deviceConfig, string DeviceClass);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGetID(HTLINE hLine, int dwAddressID, HTCALL hCall, int dwSelect, IntPtr deviceID, string deviceClass);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetLineDevStatusW", CharSet = CharSet.Auto)]
        internal static extern int lineGetLineDevStatus(HTLINE htLine, IntPtr lpLineStatus);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGetMessage(HTLINEAPP hLineApp, ref LINEMESSAGE lpMessage, int dwTimeout);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGetNewCalls(HTLINE hLine, int dwAddressID, int dwSelect, IntPtr lpCallList);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineGetNumRings(HTLINE hLine, int dwAddressID, out int lpdwNumRings);

        [DllImport("tapi32.dll", EntryPoint = "lineGetProviderListW", CharSet = CharSet.Auto)]
        internal static extern int lineGetProviderList(int dwAPIVersion, IntPtr lpProviderList);

        [DllImport("Tapi32.dll", EntryPoint = "lineGetTranslateCapsW", CharSet = CharSet.Auto)]
        internal static extern int lineGetTranslateCaps(HTLINEAPP hLineApp, int dwAPIVersion, IntPtr lpTranslateCaps);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineHold(HTCALL hCall);

        [DllImport("Tapi32.dll", EntryPoint = "lineInitializeExW", CharSet = CharSet.Auto)]
        internal static extern int lineInitializeEx(out uint hLineApp, IntPtr hAppHandle, TapiEventCallback CalllBack, string friendlyAppName, out int numDevices, ref int apiVersion, ref LINEINITIALIZEEXPARAMS lineExParms);

        [DllImport("Tapi32.dll", EntryPoint = "lineMakeCallW", CharSet = CharSet.Auto)]
        internal static extern int lineMakeCall(HTLINE hLine, out uint hCall, string DestAddress, int CountryCode, IntPtr lpCallParams);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineNegotiateAPIVersion(HTLINEAPP hLineApp, int dwDeviceID, int dwAPILowVersion, int dwAPIHighVersion, out int lpdwAPIVersion, out LINEEXTENSIONID lpExtensionID);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineNegotiateExtVersion(HTLINEAPP hLineApp, int dwDeviceID, int dwAPIVersion, int dwExtLowVersion, int dwExtHighVersion, out int dwExtVersion);

        [DllImport("Tapi32.dll", EntryPoint = "lineOpenW", CharSet = CharSet.Auto)]
        internal static extern int lineOpen(HTLINEAPP hLineApp, int dwDeviceID, out uint hLine, int dwAPIVersion, int dwExtVersion, IntPtr dwCallbackInstance, int dwPrivileges, int dwMediaModes, ref LINECALLPARAMS lpCallParams);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineMonitorDigits(HTCALL hCall, int digitModes);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineMonitorMedia(HTCALL hCall, int mediaModes);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineMonitorTones(HTCALL hCall, LINEMONITORTONE[] tones, int numEntries);

        [DllImport("Tapi32.dll", EntryPoint = "lineParkW", CharSet = CharSet.Auto)]
        internal static extern int linePark(HTCALL hCall, int parkMode, string address, IntPtr ndAddress);

        [DllImport("Tapi32.dll", EntryPoint = "linePickupW", CharSet = CharSet.Auto)]
        internal static extern int linePickup(HTLINE hLine, int addressId, out uint hCall, string destAddress, string groupId);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int linePrepareAddToConference(HTCALL hConfCall, out uint hConsultCall, IntPtr lpCallParams);

        [DllImport("Tapi32.dll", EntryPoint = "lineRedirectW", CharSet = CharSet.Auto)]
        internal static extern int lineRedirect(HTCALL hCall, string address, int countryCode);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineReleaseUserUserInfo(HTCALL hCall);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineRemoveFromConference(HTCALL hConfCall, HTCALL hConsultCall);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSecureCall(HTCALL hCall);

        [DllImport("Tapi32.dll", CharSet = CharSet.Ansi)]
        internal static extern int lineSendUserUserInfo(HTCALL hCall, string data, int dwLength);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetAppSpecific(HTCALL hCall, int appData);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetCallData(HTCALL hCall, byte[] pBuffer, int dwLength);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetCallParams(HTCALL hCall, int dwBearerMode, int dwMinRate, int dwMaxRate, LINEDIALPARAMS ldp);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetCallPrivilege(HTCALL hCall, int privilege);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetCallTreatment(HTCALL hCall, int dwCallTreatment);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetCurrentLocation(HTLINEAPP hLineApp, int currLocation);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetDevConfig(int deviceId, IntPtr devConfig, int dwSize, string deviceConfig);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetLineDevStatus(HTLINE hLine, int dwStatus, int onOff);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetMediaMode(HTCALL hCall, int mediaMode);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetNumRings(HTLINE hLine, int dwAddressID, int lpdwNumRings);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetStatusMessages(HTLINE hLine, int dwLineStates, int dwAddressStates);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetupConference(HTCALL hCall, HTLINE hLine, out uint htConferenceCall, out uint htConsultCall, int dwNumParties, IntPtr lpCallParams);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSetupTransfer(HTCALL hCall, out uint htConsultCall, IntPtr lpCallParams);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static extern int lineShutdown(uint hTapi);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineSwapHold(HTCALL hCall, HTCALL otherCall);

        [DllImport("Tapi32.dll", EntryPoint = "lineTranslateAddressW", CharSet = CharSet.Auto)]
        internal static extern int lineTranslateAddress(HTLINEAPP hLineApp, int dwDeviceID, int dwAPIVersion, string sAddressIn, int dwCard, int dwTranslateOptions, IntPtr lpTranslateOutput);

        [DllImport("tapi32.dll", EntryPoint = "lineTranslateDialogW", CharSet = CharSet.Auto)]
        internal static extern int lineTranslateDialog(HTLINEAPP hLineApp, int dwDeviceID, int dwAPIVersion, SafeHandle hwndOwner, string lpszAddressIn);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineUncompleteCall(HTLINE hLine, int completionId);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int lineUnhold(HTCALL hCall);

        [DllImport("tapi32.dll", EntryPoint = "lineUnparkW", CharSet = CharSet.Auto)]
        internal static extern int lineUnpark(HTLINE hLine, int addressID, out uint hCall, string parkAddress);

        [DllImport("tapi32.dll", EntryPoint = "phoneClose", CharSet = CharSet.Auto)]
        internal static extern int phoneClose(uint hPhone);

        [DllImport("tapi32.dll", EntryPoint = "phoneConfigDialogW", CharSet = CharSet.Auto)]
        internal static extern int phoneConfigDialog(int dwDeviceID, IntPtr hwndOwner, string deviceClass);

        [DllImport("tapi32.dll", EntryPoint = "phoneDevSpecific", CharSet = CharSet.Auto)]
        internal static extern int phoneDevSpecific(HTPHONE hPhone, IntPtr lpParams, int dwSize);

        [DllImport("tapi32.dll", EntryPoint = "phoneGetButtonInfoW", CharSet = CharSet.Auto)]
        internal static extern int phoneGetButtonInfo(HTPHONE hPhone, int dwButtonLampID, IntPtr lpButtonInfo); 

        [DllImport("tapi32.dll", EntryPoint = "phoneGetData", CharSet = CharSet.Auto)]
        internal static extern int phoneGetData(HTPHONE hPhone, int dwDataID, IntPtr lpData, int dwSize);

        [DllImport("Tapi32.dll", EntryPoint = "phoneGetDevCapsW", CharSet = CharSet.Auto)]
        internal static extern int phoneGetDevCaps(HTPHONEAPP hPhoneApp, int dwDeviceID, int dwAPIVersion, int dwExtVersion, IntPtr lpPhoneCaps);

        [DllImport("Tapi32.dll", EntryPoint = "phoneGetDisplay", CharSet = CharSet.Auto)]
        internal static extern int phoneGetDisplay(HTPHONE hPhone, IntPtr lpDisplay);

        [DllImport("Tapi32.dll", EntryPoint = "phoneGetGain", CharSet = CharSet.Auto)]
        internal static extern int phoneGetGain(HTPHONE hPhone, int dwHookSwitchDev, ref int lpdwGain);

        [DllImport("Tapi32.dll", EntryPoint = "phoneGetHookSwitch", CharSet = CharSet.Auto)]
        internal static extern int phoneGetGain(HTPHONE hPhone, ref int lpdwHookSwitchDevs);

        [DllImport("Tapi32.dll", EntryPoint = "phoneGetHookIDW", CharSet = CharSet.Auto)]
        internal static extern int phoneGetID(HTPHONE hPhone, IntPtr lpDeviceID, string lpszDeviceClass);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int phoneGetLamp(HTPHONE hPhone, int dwButtonLampID, out int lpdwLampMode);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int phoneGetMessage(HTPHONEAPP hPhoneApp, ref LINEMESSAGE lpMessage, int dwTimeout);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int phoneGetRing(HTPHONE hPhone, ref int lpdwRingMode, ref int lpdwVolume);

        [DllImport("Tapi32.dll", EntryPoint="phoneGetStatusW", CharSet = CharSet.Auto)]
        internal static extern int phoneGetStatus(HTPHONE hPhone, IntPtr lpPhoneStatus);

        [DllImport("Tapi32.dll", EntryPoint="phoneGetStatusMessages", CharSet = CharSet.Auto)]
        internal static extern int phoneGetStatusMessages(HTPHONE hPhone, ref int lpdwPhoneStates, ref int lpdwButtonModes, ref int lpdwButtonStates);

        [DllImport("Tapi32.dll", EntryPoint="phoneGetVolume", CharSet = CharSet.Auto)]
        internal static extern int phoneGetVolume(HTPHONE hPhone, int dwHookSwitchDev, ref int lpdwVolume);

        [DllImport("Tapi32.dll", EntryPoint = "phoneInitializeExW", CharSet = CharSet.Auto)]
        internal static extern int phoneInitializeEx(out uint hPhoneApp, uint hAppHandle, TapiEventCallback CalllBack, string friendlyAppName, out int numDevices, ref int apiVersion, ref PHONEINITIALIZEEXPARAMS phoneExParms);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int phoneNegotiateAPIVersion(HTPHONEAPP hPhoneApp, int dwDeviceID, int dwAPILowVersion, int dwAPIHighVersion, out int lpdwAPIVersion, out PHONEEXTENSIONID lpExtensionID);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int phoneNegotiateExtVersion(HTPHONEAPP hPhoneApp, int dwDeviceID, int dwAPIVersion, int dwExtLowVersion, int dwExtHighVersion, out int dwExtVersion);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int phoneOpen(HTPHONEAPP hPhoneApp, int dwDeviceID, out uint hPhone, int dwAPIVersion, int dwExtVersion, IntPtr dwCallbackInstance, int dwPrivileges);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetButtonInfoW", CharSet = CharSet.Auto)]
        internal static extern int phoneSetButtonInfo(HTPHONE hPhone, int dwButtonLampID, IntPtr lpButtonInfo);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetData", CharSet = CharSet.Auto)]
        internal static extern int phoneSetData(HTPHONE hPhone, int dwDataID, IntPtr lpData, int dwSize);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetDisplay", CharSet = CharSet.Ansi)]
        internal static extern int phoneSetDisplay(HTPHONE hPhone, int dwRow, int dwColumn, string lpsDisplay, int dwSize);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetGain", CharSet = CharSet.Auto)]
        internal static extern int phoneSetGain(HTPHONE hPhone, int dwHookSwitchDev, int dwGain);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetHookSwitch", CharSet = CharSet.Auto)]
        internal static extern int phoneSetHookSwitch(HTPHONE hPhone, int dwHookSwitchDev, int dwHookSwitchMode);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetLamp", CharSet = CharSet.Auto)]
        internal static extern int phoneSetLamp(HTPHONE hPhone, int dwButtonLampID, int dwLampMode);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetRing", CharSet = CharSet.Auto)]
        internal static extern int phoneSetRing(HTPHONE hPhone, int dwRingMode, int dwVolume);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetStatusMessages", CharSet = CharSet.Auto)]
        internal static extern int phoneSetStatusMessages(HTPHONE hPhone, int dwPhoneStates, int dwButtonModes, int dwButtonStates);

        [DllImport("Tapi32.dll", EntryPoint = "phoneSetVolume", CharSet = CharSet.Auto)]
        internal static extern int phoneSetVolume(HTPHONE hPhone, int dwHookSwitchDev, int dwVolume);

        [DllImport("Tapi32.dll", CharSet = CharSet.Auto), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        internal static extern int phoneShutdown(uint hTapi);

        #endregion

        #region Constants
        internal const int LINEADDRCAPFLAGS_FWDNUMRINGS = unchecked((int)0x00000001);
        internal const int LINEADDRCAPFLAGS_PICKUPGROUPID = unchecked((int)0x00000002);
        internal const int LINEADDRCAPFLAGS_SECURE = unchecked((int)0x00000004);
        internal const int LINEADDRCAPFLAGS_BLOCKIDDEFAULT = unchecked((int)0x00000008);
        internal const int LINEADDRCAPFLAGS_BLOCKIDOVERRIDE = unchecked((int)0x00000010);
        internal const int LINEADDRCAPFLAGS_DIALED = unchecked((int)0x00000020);
        internal const int LINEADDRCAPFLAGS_ORIGOFFHOOK = unchecked((int)0x00000040);
        internal const int LINEADDRCAPFLAGS_DESTOFFHOOK = unchecked((int)0x00000080);
        internal const int LINEADDRCAPFLAGS_FWDCONSULT = unchecked((int)0x00000100);
        internal const int LINEADDRCAPFLAGS_SETUPCONFNULL = unchecked((int)0x00000200);
        internal const int LINEADDRCAPFLAGS_AUTORECONNECT = unchecked((int)0x00000400);
        internal const int LINEADDRCAPFLAGS_COMPLETIONID = unchecked((int)0x00000800);
        internal const int LINEADDRCAPFLAGS_TRANSFERHELD = unchecked((int)0x00001000);
        internal const int LINEADDRCAPFLAGS_TRANSFERMAKE = unchecked((int)0x00002000);
        internal const int LINEADDRCAPFLAGS_CONFERENCEHELD = unchecked((int)0x00004000);
        internal const int LINEADDRCAPFLAGS_CONFERENCEMAKE = unchecked((int)0x00008000);
        internal const int LINEADDRCAPFLAGS_PARTIALDIAL = unchecked((int)0x00010000);
        internal const int LINEADDRCAPFLAGS_FWDSTATUSVALID = unchecked((int)0x00020000);
        internal const int LINEADDRCAPFLAGS_FWDINTEXTADDR = unchecked((int)0x00040000);
        internal const int LINEADDRCAPFLAGS_FWDBUSYNAADDR = unchecked((int)0x00080000);
        internal const int LINEADDRCAPFLAGS_ACCEPTTOALERT = unchecked((int)0x00100000);
        internal const int LINEADDRCAPFLAGS_CONFDROP = unchecked((int)0x00200000);
        internal const int LINEADDRCAPFLAGS_PICKUPCALLWAIT = unchecked((int)0x00400000);
        internal const int LINEADDRCAPFLAGS_PREDICTIVEDIALER = unchecked((int)0x00800000);       // TAPI v2.0
        internal const int LINEADDRCAPFLAGS_QUEUE = unchecked((int)0x01000000);                  // TAPI v2.0
        internal const int LINEADDRCAPFLAGS_ROUTEPOINT = unchecked((int)0x02000000);             // TAPI v2.0
        internal const int LINEADDRCAPFLAGS_HOLDMAKESNEW = unchecked((int)0x04000000);           // TAPI v2.0
        internal const int LINEADDRCAPFLAGS_NOINTERNALCALLS = unchecked((int)0x08000000);        // TAPI v2.0
        internal const int LINEADDRCAPFLAGS_NOEXTERNALCALLS = unchecked((int)0x10000000);        // TAPI v2.0
        internal const int LINEADDRCAPFLAGS_SETCALLINGID = unchecked((int)0x20000000);           // TAPI v2.0
        internal const int LINEADDRCAPFLAGS_ACDGROUP = unchecked((int)0x40000000);               // TAPI v2.2
        internal const int LINEADDRCAPFLAGS_NOPSTNADDRESSTRANSLATION = unchecked((int)0x80000000);  // TAPI v3.0

        internal const int LINEADDRESSMODE_ADDRESSID = unchecked((int)0x00000001);
        internal const int LINEADDRESSMODE_DIALABLEADDR = unchecked((int)0x00000002);

        internal const int LINEADDRESSSHARING_PRIVATE = unchecked((int)0x00000001);
        internal const int LINEADDRESSSHARING_BRIDGEDEXCL = unchecked((int)0x00000002);
        internal const int LINEADDRESSSHARING_BRIDGEDNEW = unchecked((int)0x00000004);
        internal const int LINEADDRESSSHARING_BRIDGEDSHARED = unchecked((int)0x00000008);
        internal const int LINEADDRESSSHARING_MONITORED = unchecked((int)0x00000010);

        internal const int LINEADDRESSSTATE_OTHER = unchecked((int)0x00000001);
        internal const int LINEADDRESSSTATE_DEVSPECIFIC = unchecked((int)0x00000002);
        internal const int LINEADDRESSSTATE_INUSEZERO = unchecked((int)0x00000004);
        internal const int LINEADDRESSSTATE_INUSEONE = unchecked((int)0x00000008);
        internal const int LINEADDRESSSTATE_INUSEMANY = unchecked((int)0x00000010);
        internal const int LINEADDRESSSTATE_NUMCALLS = unchecked((int)0x00000020);
        internal const int LINEADDRESSSTATE_FORWARD = unchecked((int)0x00000040);
        internal const int LINEADDRESSSTATE_TERMINALS = unchecked((int)0x00000080);
        internal const int LINEADDRESSSTATE_CAPSCHANGE = unchecked((int)0x00000100);      // TAPI v1.4    

        internal const int LINEADDRESSTYPE_PHONENUMBER = unchecked((int)0x00000001);
        internal const int LINEADDRESSTYPE_SDP = unchecked((int)0x00000002);
        internal const int LINEADDRESSTYPE_EMAILNAME = unchecked((int)0x00000004);
        internal const int LINEADDRESSTYPE_DOMAINNAME = unchecked((int)0x00000008);
        internal const int LINEADDRESSTYPE_IPADDRESS = unchecked((int)0x00000010);

        internal const int LINEADDRFEATURE_FORWARD = unchecked((int)0x00000001);
        internal const int LINEADDRFEATURE_MAKECALL = unchecked((int)0x00000002);
        internal const int LINEADDRFEATURE_PICKUP = unchecked((int)0x00000004);
        internal const int LINEADDRFEATURE_SETMEDIACONTROL = unchecked((int)0x00000008);
        internal const int LINEADDRFEATURE_SETTERMINAL = unchecked((int)0x00000010);
        internal const int LINEADDRFEATURE_SETUPCONF = unchecked((int)0x00000020);
        internal const int LINEADDRFEATURE_UNCOMPLETECALL = unchecked((int)0x00000040);
        internal const int LINEADDRFEATURE_UNPARK = unchecked((int)0x00000080);
        internal const int LINEADDRFEATURE_PICKUPHELD = unchecked((int)0x00000100);          // TAPI v2.0
        internal const int LINEADDRFEATURE_PICKUPGROUP = unchecked((int)0x00000200);         // TAPI v2.0
        internal const int LINEADDRFEATURE_PICKUPDIRECT = unchecked((int)0x00000400);        // TAPI v2.0
        internal const int LINEADDRFEATURE_PICKUPWAITING = unchecked((int)0x00000800);       // TAPI v2.0
        internal const int LINEADDRFEATURE_FORWARDFWD = unchecked((int)0x00001000);          // TAPI v2.0
        internal const int LINEADDRFEATURE_FORWARDDND = unchecked((int)0x00002000);          // TAPI v2.0

        internal const int LINEANSWERMODE_NONE = unchecked((int)0x00000001);
        internal const int LINEANSWERMODE_DROP = unchecked((int)0x00000002);
        internal const int LINEANSWERMODE_HOLD = unchecked((int)0x00000004);

        internal const int LINEBEARERMODE_VOICE = unchecked((int)0x00000001);
        internal const int LINEBEARERMODE_SPEECH = unchecked((int)0x00000002);
        internal const int LINEBEARERMODE_MULTIUSE = unchecked((int)0x00000004);
        internal const int LINEBEARERMODE_DATA = unchecked((int)0x00000008);
        internal const int LINEBEARERMODE_ALTSPEECHDATA = unchecked((int)0x00000010);
        internal const int LINEBEARERMODE_NONCALLSIGNALING = unchecked((int)0x00000020);
        internal const int LINEBEARERMODE_PASSTHROUGH = unchecked((int)0x00000040);       // TAPI v1.4
        internal const int LINEBEARERMODE_RESTRICTEDDATA = unchecked((int)0x00000080);     // TAPI v2.0    

        internal const int LINEBUSYMODE_STATION = unchecked((int)0x00000001);
        internal const int LINEBUSYMODE_TRUNK = unchecked((int)0x00000002);
        internal const int LINEBUSYMODE_UNKNOWN = unchecked((int)0x00000004);
        internal const int LINEBUSYMODE_UNAVAIL = unchecked((int)0x00000008);

        internal const int LINECALLCOMPLCOND_BUSY = unchecked((int)0x00000001);
        internal const int LINECALLCOMPLCOND_NOANSWER = unchecked((int)0x00000002);

        internal const int LINECALLFEATURE_ACCEPT = unchecked((int)0x00000001);
        internal const int LINECALLFEATURE_ADDTOCONF = unchecked((int)0x00000002);
        internal const int LINECALLFEATURE_ANSWER = unchecked((int)0x00000004);
        internal const int LINECALLFEATURE_BLINDTRANSFER = unchecked((int)0x00000008);
        internal const int LINECALLFEATURE_COMPLETECALL = unchecked((int)0x00000010);
        internal const int LINECALLFEATURE_COMPLETETRANSF = unchecked((int)0x00000020);
        internal const int LINECALLFEATURE_DIAL = unchecked((int)0x00000040);
        internal const int LINECALLFEATURE_DROP = unchecked((int)0x00000080);
        internal const int LINECALLFEATURE_GATHERDIGITS = unchecked((int)0x00000100);
        internal const int LINECALLFEATURE_GENERATEDIGITS = unchecked((int)0x00000200);
        internal const int LINECALLFEATURE_GENERATETONE = unchecked((int)0x00000400);
        internal const int LINECALLFEATURE_HOLD = unchecked((int)0x00000800);
        internal const int LINECALLFEATURE_MONITORDIGITS = unchecked((int)0x00001000);
        internal const int LINECALLFEATURE_MONITORMEDIA = unchecked((int)0x00002000);
        internal const int LINECALLFEATURE_MONITORTONES = unchecked((int)0x00004000);
        internal const int LINECALLFEATURE_PARK = unchecked((int)0x00008000);
        internal const int LINECALLFEATURE_PREPAREADDCONF = unchecked((int)0x00010000);
        internal const int LINECALLFEATURE_REDIRECT = unchecked((int)0x00020000);
        internal const int LINECALLFEATURE_REMOVEFROMCONF = unchecked((int)0x00040000);
        internal const int LINECALLFEATURE_SECURECALL = unchecked((int)0x00080000);
        internal const int LINECALLFEATURE_SENDUSERUSER = unchecked((int)0x00100000);
        internal const int LINECALLFEATURE_SETCALLPARAMS = unchecked((int)0x00200000);
        internal const int LINECALLFEATURE_SETMEDIACONTROL = unchecked((int)0x00400000);
        internal const int LINECALLFEATURE_SETTERMINAL = unchecked((int)0x00800000);
        internal const int LINECALLFEATURE_SETUPCONF = unchecked((int)0x01000000);
        internal const int LINECALLFEATURE_SETUPTRANSFER = unchecked((int)0x02000000);
        internal const int LINECALLFEATURE_SWAPHOLD = unchecked((int)0x04000000);
        internal const int LINECALLFEATURE_UNHOLD = unchecked((int)0x08000000);
        internal const int LINECALLFEATURE_RELEASEUSERUSERINFO = unchecked((int)0x10000000);      // TAPI v1.4
        internal const int LINECALLFEATURE_SETTREATMENT = unchecked((int)0x20000000);             // TAPI v2.0
        internal const int LINECALLFEATURE_SETQOS = unchecked((int)0x40000000);                   // TAPI v2.0
        internal const int LINECALLFEATURE_SETCALLDATA = unchecked((int)0x80000000);              // TAPI v2.0

        internal const int LINECALLFEATURE2_NOHOLDCONFERENCE = unchecked((int)0x00000001);       // TAPI v2.0
        internal const int LINECALLFEATURE2_ONESTEPTRANSFER = unchecked((int)0x00000002);        // TAPI v2.0
        internal const int LINECALLFEATURE2_COMPLCAMPON = unchecked((int)0x00000004);            // TAPI v2.0
        internal const int LINECALLFEATURE2_COMPLCALLBACK = unchecked((int)0x00000008);          // TAPI v2.0
        internal const int LINECALLFEATURE2_COMPLINTRUDE = unchecked((int)0x00000010);           // TAPI v2.0
        internal const int LINECALLFEATURE2_COMPLMESSAGE = unchecked((int)0x00000020);           // TAPI v2.0
        internal const int LINECALLFEATURE2_TRANSFERNORM = unchecked((int)0x00000040);           // TAPI v2.0
        internal const int LINECALLFEATURE2_TRANSFERCONF = unchecked((int)0x00000080);           // TAPI v2.0
        internal const int LINECALLFEATURE2_PARKDIRECT = unchecked((int)0x00000100);             // TAPI v2.0
        internal const int LINECALLFEATURE2_PARKNONDIRECT = unchecked((int)0x00000200);           // TAPI v2.0

        internal const int LINECALLINFOSTATE_OTHER = unchecked((int)0x00000001);
        internal const int LINECALLINFOSTATE_DEVSPECIFIC = unchecked((int)0x00000002);
        internal const int LINECALLINFOSTATE_BEARERMODE = unchecked((int)0x00000004);
        internal const int LINECALLINFOSTATE_RATE = unchecked((int)0x00000008);
        internal const int LINECALLINFOSTATE_MEDIAMODE = unchecked((int)0x00000010);
        internal const int LINECALLINFOSTATE_APPSPECIFIC = unchecked((int)0x00000020);
        internal const int LINECALLINFOSTATE_CALLID = unchecked((int)0x00000040);
        internal const int LINECALLINFOSTATE_RELATEDCALLID = unchecked((int)0x00000080);
        internal const int LINECALLINFOSTATE_ORIGIN = unchecked((int)0x00000100);
        internal const int LINECALLINFOSTATE_REASON = unchecked((int)0x00000200);
        internal const int LINECALLINFOSTATE_COMPLETIONID = unchecked((int)0x00000400);
        internal const int LINECALLINFOSTATE_NUMOWNERINCR = unchecked((int)0x00000800);
        internal const int LINECALLINFOSTATE_NUMOWNERDECR = unchecked((int)0x00001000);
        internal const int LINECALLINFOSTATE_NUMMONITORS = unchecked((int)0x00002000);
        internal const int LINECALLINFOSTATE_TRUNK = unchecked((int)0x00004000);
        internal const int LINECALLINFOSTATE_CALLERID = unchecked((int)0x00008000);
        internal const int LINECALLINFOSTATE_CALLEDID = unchecked((int)0x00010000);
        internal const int LINECALLINFOSTATE_CONNECTEDID = unchecked((int)0x00020000);
        internal const int LINECALLINFOSTATE_REDIRECTIONID = unchecked((int)0x00040000);
        internal const int LINECALLINFOSTATE_REDIRECTINGID = unchecked((int)0x00080000);
        internal const int LINECALLINFOSTATE_DISPLAY = unchecked((int)0x00100000);
        internal const int LINECALLINFOSTATE_USERUSERINFO = unchecked((int)0x00200000);
        internal const int LINECALLINFOSTATE_HIGHLEVELCOMP = unchecked((int)0x00400000);
        internal const int LINECALLINFOSTATE_LOWLEVELCOMP = unchecked((int)0x00800000);
        internal const int LINECALLINFOSTATE_CHARGINGINFO = unchecked((int)0x01000000);
        internal const int LINECALLINFOSTATE_TERMINAL = unchecked((int)0x02000000);
        internal const int LINECALLINFOSTATE_DIALPARAMS = unchecked((int)0x04000000);
        internal const int LINECALLINFOSTATE_MONITORMODES = unchecked((int)0x08000000);
        internal const int LINECALLINFOSTATE_TREATMENT = unchecked((int)0x10000000);      // TAPI v2.0
        internal const int LINECALLINFOSTATE_QOS = unchecked((int)0x20000000);            // TAPI v2.0
        internal const int LINECALLINFOSTATE_CALLDATA = unchecked((int)0x40000000);        // TAPI v2.0

        internal const int LINECALLORIGIN_OUTBOUND = unchecked((int)0x00000001);
        internal const int LINECALLORIGIN_INTERNAL = unchecked((int)0x00000002);
        internal const int LINECALLORIGIN_EXTERNAL = unchecked((int)0x00000004);
        internal const int LINECALLORIGIN_UNKNOWN = unchecked((int)0x00000010);
        internal const int LINECALLORIGIN_UNAVAIL = unchecked((int)0x00000020);
        internal const int LINECALLORIGIN_CONFERENCE = unchecked((int)0x00000040);
        internal const int LINECALLORIGIN_INBOUND = unchecked((int)0x00000080);      // TAPI v1.4    

        internal const int LINECALLPARAMFLAGS_SECURE = unchecked((int)0x00000001);
        internal const int LINECALLPARAMFLAGS_IDLE = unchecked((int)0x00000002);
        internal const int LINECALLPARAMFLAGS_BLOCKID = unchecked((int)0x00000004);
        internal const int LINECALLPARAMFLAGS_ORIGOFFHOOK = unchecked((int)0x00000008);
        internal const int LINECALLPARAMFLAGS_DESTOFFHOOK = unchecked((int)0x00000010);
        internal const int LINECALLPARAMFLAGS_NOHOLDCONFERENCE = unchecked((int)0x00000020);       // TAPI v2.0
        internal const int LINECALLPARAMFLAGS_PREDICTIVEDIAL = unchecked((int)0x00000040);         // TAPI v2.0
        internal const int LINECALLPARAMFLAGS_ONESTEPTRANSFER = unchecked((int)0x00000080);         // TAPI v2.0    

        internal const int LINECALLPARTYID_BLOCKED = unchecked((int)0x00000001);
        internal const int LINECALLPARTYID_OUTOFAREA = unchecked((int)0x00000002);
        internal const int LINECALLPARTYID_NAME = unchecked((int)0x00000004);
        internal const int LINECALLPARTYID_ADDRESS = unchecked((int)0x00000008);
        internal const int LINECALLPARTYID_PARTIAL = unchecked((int)0x00000010);
        internal const int LINECALLPARTYID_UNKNOWN = unchecked((int)0x00000020);
        internal const int LINECALLPARTYID_UNAVAIL = unchecked((int)0x00000040);

        internal const int LINECALLPRIVILEGE_NONE = unchecked((int)0x00000001);
        internal const int LINECALLPRIVILEGE_MONITOR = unchecked((int)0x00000002);
        internal const int LINECALLPRIVILEGE_OWNER = unchecked((int)0x00000004);

        internal const int LINECALLREASON_DIRECT = unchecked((int)0x00000001);
        internal const int LINECALLREASON_FWDBUSY = unchecked((int)0x00000002);
        internal const int LINECALLREASON_FWDNOANSWER = unchecked((int)0x00000004);
        internal const int LINECALLREASON_FWDUNCOND = unchecked((int)0x00000008);
        internal const int LINECALLREASON_PICKUP = unchecked((int)0x00000010);
        internal const int LINECALLREASON_UNPARK = unchecked((int)0x00000020);
        internal const int LINECALLREASON_REDIRECT = unchecked((int)0x00000040);
        internal const int LINECALLREASON_CALLCOMPLETION = unchecked((int)0x00000080);
        internal const int LINECALLREASON_TRANSFER = unchecked((int)0x00000100);
        internal const int LINECALLREASON_REMINDER = unchecked((int)0x00000200);
        internal const int LINECALLREASON_UNKNOWN = unchecked((int)0x00000400);
        internal const int LINECALLREASON_UNAVAIL = unchecked((int)0x00000800);
        internal const int LINECALLREASON_INTRUDE = unchecked((int)0x00001000);          // TAPI v1.4
        internal const int LINECALLREASON_PARKED = unchecked((int)0x00002000);           // TAPI v1.4
        internal const int LINECALLREASON_CAMPEDON = unchecked((int)0x00004000);         // TAPI v2.0
        internal const int LINECALLREASON_ROUTEREQUEST = unchecked((int)0x00008000);      // TAPI v2.0    

        internal const int LINECALLSELECT_LINE = unchecked((int)0x00000001);
        internal const int LINECALLSELECT_ADDRESS = unchecked((int)0x00000002);
        internal const int LINECALLSELECT_CALL = unchecked((int)0x00000004);
        internal const int LINECALLSELECT_DEVICEID = unchecked((int)0x00000008);      // TAPI v2.1
        internal const int LINECALLSELECT_CALLID = unchecked((int)0x00000010);      // TAPI v3.0

        internal const int LINECALLSTATE_IDLE = unchecked((int)0x00000001);
        internal const int LINECALLSTATE_OFFERING = unchecked((int)0x00000002);
        internal const int LINECALLSTATE_ACCEPTED = unchecked((int)0x00000004);
        internal const int LINECALLSTATE_DIALTONE = unchecked((int)0x00000008);
        internal const int LINECALLSTATE_DIALING = unchecked((int)0x00000010);
        internal const int LINECALLSTATE_RINGBACK = unchecked((int)0x00000020);
        internal const int LINECALLSTATE_BUSY = unchecked((int)0x00000040);
        internal const int LINECALLSTATE_SPECIALINFO = unchecked((int)0x00000080);
        internal const int LINECALLSTATE_CONNECTED = unchecked((int)0x00000100);
        internal const int LINECALLSTATE_PROCEEDING = unchecked((int)0x00000200);
        internal const int LINECALLSTATE_ONHOLD = unchecked((int)0x00000400);
        internal const int LINECALLSTATE_CONFERENCED = unchecked((int)0x00000800);
        internal const int LINECALLSTATE_ONHOLDPENDCONF = unchecked((int)0x00001000);
        internal const int LINECALLSTATE_ONHOLDPENDTRANSFER = unchecked((int)0x00002000);
        internal const int LINECALLSTATE_DISCONNECTED = unchecked((int)0x00004000);
        internal const int LINECALLSTATE_UNKNOWN = unchecked((int)0x00008000);

        internal const int LINECALLTREATMENT_SILENCE = unchecked((int)0x00000001);       // TAPI v2.0
        internal const int LINECALLTREATMENT_RINGBACK = unchecked((int)0x00000002);      // TAPI v2.0
        internal const int LINECALLTREATMENT_BUSY = unchecked((int)0x00000003);          // TAPI v2.0
        internal const int LINECALLTREATMENT_MUSIC = unchecked((int)0x00000004);          // TAPI v2.0    

        internal const int LINECARDENTRY_PREDEFINED = unchecked((int)0x00000001);      // TAPI v1.4
        internal const int LINECARDENTRY_HIDDEN = unchecked((int)0x00000002);          // TAPI v1.4

        internal const int LINECARDOPTION_PREDEFINED = unchecked((int)0x00000001);      // TAPI v1.4
        internal const int LINECARDOPTION_HIDDEN = unchecked((int)0x00000002);      // TAPI v1.4

        internal const int LINECALLCOMPLMODE_CAMPON  = unchecked((int)0x00000001);
        internal const int LINECALLCOMPLMODE_CALLBACK = unchecked((int)0x00000002);
        internal const int LINECALLCOMPLMODE_INTRUDE = unchecked((int)0x00000004);
        internal const int LINECALLCOMPLMODE_MESSAGE = unchecked((int)0x00000008);

        internal const int LINECONNECTEDMODE_ACTIVE = unchecked((int)0x00000001);         // TAPI v1.4
        internal const int LINECONNECTEDMODE_INACTIVE = unchecked((int)0x00000002);       // TAPI v1.4
        internal const int LINECONNECTEDMODE_ACTIVEHELD = unchecked((int)0x00000004);     // TAPI v2.0
        internal const int LINECONNECTEDMODE_INACTIVEHELD = unchecked((int)0x00000008);   // TAPI v2.0
        internal const int LINECONNECTEDMODE_CONFIRMED = unchecked((int)0x00000010);       // TAPI v2.0

        internal const int LINEDEVCAPFLAGS_CROSSADDRCONF = unchecked((int)0x00000001);
        internal const int LINEDEVCAPFLAGS_HIGHLEVCOMP = unchecked((int)0x00000002);
        internal const int LINEDEVCAPFLAGS_LOWLEVCOMP = unchecked((int)0x00000004);
        internal const int LINEDEVCAPFLAGS_MEDIACONTROL = unchecked((int)0x00000008);
        internal const int LINEDEVCAPFLAGS_MULTIPLEADDR = unchecked((int)0x00000010);
        internal const int LINEDEVCAPFLAGS_CLOSEDROP = unchecked((int)0x00000020);
        internal const int LINEDEVCAPFLAGS_DIALBILLING = unchecked((int)0x00000040);
        internal const int LINEDEVCAPFLAGS_DIALQUIET = unchecked((int)0x00000080);
        internal const int LINEDEVCAPFLAGS_DIALDIALTONE = unchecked((int)0x00000100);
        internal const int LINEDEVCAPFLAGS_MSP = unchecked((int)0x00000200);      // TAPI v3.0
        internal const int LINEDEVCAPFLAGS_CALLHUB = unchecked((int)0x00000400);     // TAPI v3.0
        internal const int LINEDEVCAPFLAGS_CALLHUBTRACKING = unchecked((int)0x00000800);      // TAPI v3.0
        internal const int LINEDEVCAPFLAGS_PRIVATEOBJECTS = unchecked((int)0x00001000);      // TAPI v3.0
        internal const int LINEDEVCAPFLAGS_LOCAL = unchecked((int)0x00002000);

        internal const int LINEDEVSTATE_OTHER = unchecked((int)0x00000001);
        internal const int LINEDEVSTATE_RINGING = unchecked((int)0x00000002);
        internal const int LINEDEVSTATE_CONNECTED = unchecked((int)0x00000004);
        internal const int LINEDEVSTATE_DISCONNECTED = unchecked((int)0x00000008);
        internal const int LINEDEVSTATE_MSGWAITON = unchecked((int)0x00000010);
        internal const int LINEDEVSTATE_MSGWAITOFF = unchecked((int)0x00000020);
        internal const int LINEDEVSTATE_INSERVICE = unchecked((int)0x00000040);
        internal const int LINEDEVSTATE_OUTOFSERVICE = unchecked((int)0x00000080);
        internal const int LINEDEVSTATE_MAINTENANCE = unchecked((int)0x00000100);
        internal const int LINEDEVSTATE_OPEN = unchecked((int)0x00000200);
        internal const int LINEDEVSTATE_CLOSE = unchecked((int)0x00000400);
        internal const int LINEDEVSTATE_NUMCALLS = unchecked((int)0x00000800);
        internal const int LINEDEVSTATE_NUMCOMPLETIONS = unchecked((int)0x00001000);
        internal const int LINEDEVSTATE_TERMINALS = unchecked((int)0x00002000);
        internal const int LINEDEVSTATE_ROAMMODE = unchecked((int)0x00004000);
        internal const int LINEDEVSTATE_BATTERY = unchecked((int)0x00008000);
        internal const int LINEDEVSTATE_SIGNAL = unchecked((int)0x00010000);
        internal const int LINEDEVSTATE_DEVSPECIFIC = unchecked((int)0x00020000);
        internal const int LINEDEVSTATE_REINIT = unchecked((int)0x00040000);
        internal const int LINEDEVSTATE_LOCK = unchecked((int)0x00080000);
        internal const int LINEDEVSTATE_CAPSCHANGE = unchecked((int)0x00100000);           // TAPI v1.4
        internal const int LINEDEVSTATE_CONFIGCHANGE = unchecked((int)0x00200000);         // TAPI v1.4
        internal const int LINEDEVSTATE_TRANSLATECHANGE = unchecked((int)0x00400000);      // TAPI v1.4
        internal const int LINEDEVSTATE_COMPLCANCEL = unchecked((int)0x00800000);          // TAPI v1.4
        internal const int LINEDEVSTATE_REMOVED = unchecked((int)0x01000000);              // TAPI v1.4    

        internal const int LINEDEVSTATUSFLAGS_CONNECTED = unchecked((int)0x00000001);
        internal const int LINEDEVSTATUSFLAGS_MSGWAIT = unchecked((int)0x00000002);
        internal const int LINEDEVSTATUSFLAGS_INSERVICE = unchecked((int)0x00000004);
        internal const int LINEDEVSTATUSFLAGS_LOCKED = unchecked((int)0x00000008);

        internal const int LINEDIALTONEMODE_NORMAL = unchecked((int)0x00000001);
        internal const int LINEDIALTONEMODE_SPECIAL = unchecked((int)0x00000002);
        internal const int LINEDIALTONEMODE_INTERNAL = unchecked((int)0x00000004);
        internal const int LINEDIALTONEMODE_EXTERNAL = unchecked((int)0x00000008);
        internal const int LINEDIALTONEMODE_UNKNOWN = unchecked((int)0x00000010);
        internal const int LINEDIALTONEMODE_UNAVAIL = unchecked((int)0x00000020);

        internal const int LINEDIGITMODE_PULSE = unchecked((int)0x00000001);
        internal const int LINEDIGITMODE_DTMF = unchecked((int)0x00000002);
        internal const int LINEDIGITMODE_DTMFEND = unchecked((int)0x00000004);

        internal const int LINEDISCONNECTMODE_NORMAL = unchecked((int)0x00000001);
        internal const int LINEDISCONNECTMODE_UNKNOWN = unchecked((int)0x00000002);
        internal const int LINEDISCONNECTMODE_REJECT = unchecked((int)0x00000004);
        internal const int LINEDISCONNECTMODE_PICKUP = unchecked((int)0x00000008);
        internal const int LINEDISCONNECTMODE_FORWARDED = unchecked((int)0x00000010);
        internal const int LINEDISCONNECTMODE_BUSY = unchecked((int)0x00000020);
        internal const int LINEDISCONNECTMODE_NOANSWER = unchecked((int)0x00000040);
        internal const int LINEDISCONNECTMODE_BADADDRESS = unchecked((int)0x00000080);
        internal const int LINEDISCONNECTMODE_UNREACHABLE = unchecked((int)0x00000100);
        internal const int LINEDISCONNECTMODE_CONGESTION = unchecked((int)0x00000200);
        internal const int LINEDISCONNECTMODE_INCOMPATIBLE = unchecked((int)0x00000400);
        internal const int LINEDISCONNECTMODE_UNAVAIL = unchecked((int)0x00000800);
        internal const int LINEDISCONNECTMODE_NODIALTONE = unchecked((int)0x00001000);         // TAPI v1.4
        internal const int LINEDISCONNECTMODE_NUMBERCHANGED = unchecked((int)0x00002000);      // TAPI v2.0
        internal const int LINEDISCONNECTMODE_OUTOFORDER = unchecked((int)0x00004000);         // TAPI v2.0
        internal const int LINEDISCONNECTMODE_TEMPFAILURE = unchecked((int)0x00008000);        // TAPI v2.0
        internal const int LINEDISCONNECTMODE_QOSUNAVAIL = unchecked((int)0x00010000);         // TAPI v2.0
        internal const int LINEDISCONNECTMODE_BLOCKED = unchecked((int)0x00020000);            // TAPI v2.0
        internal const int LINEDISCONNECTMODE_DONOTDISTURB = unchecked((int)0x00040000);       // TAPI v2.0
        internal const int LINEDISCONNECTMODE_CANCELLED = unchecked((int)0x00080000);           // TAPI v2.0

        internal const int LINEERR_OK = unchecked((int)0x00000000);
        internal const int LINEERR_ALLOCATED = unchecked((int)0x80000001);
        internal const int LINEERR_BADDEVICEID = unchecked((int)0x80000002);
        internal const int LINEERR_BEARERMODEUNAVAIL = unchecked((int)0x80000003);
        internal const int LINEERR_CALLUNAVAIL = unchecked((int)0x80000005);
        internal const int LINEERR_COMPLETIONOVERRUN = unchecked((int)0x80000006);
        internal const int LINEERR_CONFERENCEFULL = unchecked((int)0x80000007);
        internal const int LINEERR_DIALBILLING = unchecked((int)0x80000008);
        internal const int LINEERR_DIALDIALTONE = unchecked((int)0x80000009);
        internal const int LINEERR_DIALPROMPT = unchecked((int)0x8000000A);
        internal const int LINEERR_DIALQUIET = unchecked((int)0x8000000B);
        internal const int LINEERR_INCOMPATIBLEAPIVERSION = unchecked((int)0x8000000C);
        internal const int LINEERR_INCOMPATIBLEEXTVERSION = unchecked((int)0x8000000D);
        internal const int LINEERR_INIFILECORRUPT = unchecked((int)0x8000000E);
        internal const int LINEERR_INUSE = unchecked((int)0x8000000F);
        internal const int LINEERR_INVALADDRESS = unchecked((int)0x80000010);
        internal const int LINEERR_INVALADDRESSID = unchecked((int)0x80000011);
        internal const int LINEERR_INVALADDRESSMODE = unchecked((int)0x80000012);
        internal const int LINEERR_INVALADDRESSSTATE = unchecked((int)0x80000013);
        internal const int LINEERR_INVALAPPHANDLE = unchecked((int)0x80000014);
        internal const int LINEERR_INVALAPPNAME = unchecked((int)0x80000015);
        internal const int LINEERR_INVALBEARERMODE = unchecked((int)0x80000016);
        internal const int LINEERR_INVALCALLCOMPLMODE = unchecked((int)0x80000017);
        internal const int LINEERR_INVALCALLHANDLE = unchecked((int)0x80000018);
        internal const int LINEERR_INVALCALLPARAMS = unchecked((int)0x80000019);
        internal const int LINEERR_INVALCALLPRIVILEGE = unchecked((int)0x8000001A);
        internal const int LINEERR_INVALCALLSELECT = unchecked((int)0x8000001B);
        internal const int LINEERR_INVALCALLSTATE = unchecked((int)0x8000001C);
        internal const int LINEERR_INVALCALLSTATELIST = unchecked((int)0x8000001D);
        internal const int LINEERR_INVALCARD = unchecked((int)0x8000001E);
        internal const int LINEERR_INVALCOMPLETIONID = unchecked((int)0x8000001F);
        internal const int LINEERR_INVALCONFCALLHANDLE = unchecked((int)0x80000020);
        internal const int LINEERR_INVALCONSULTCALLHANDLE = unchecked((int)0x80000021);
        internal const int LINEERR_INVALCOUNTRYCODE = unchecked((int)0x80000022);
        internal const int LINEERR_INVALDEVICECLASS = unchecked((int)0x80000023);
        internal const int LINEERR_INVALDEVICEHANDLE = unchecked((int)0x80000024);
        internal const int LINEERR_INVALDIALPARAMS = unchecked((int)0x80000025);
        internal const int LINEERR_INVALDIGITLIST = unchecked((int)0x80000026);
        internal const int LINEERR_INVALDIGITMODE = unchecked((int)0x80000027);
        internal const int LINEERR_INVALDIGITS = unchecked((int)0x80000028);
        internal const int LINEERR_INVALEXTVERSION = unchecked((int)0x80000029);
        internal const int LINEERR_INVALGROUPID = unchecked((int)0x8000002A);
        internal const int LINEERR_INVALLINEHANDLE = unchecked((int)0x8000002B);
        internal const int LINEERR_INVALLINESTATE = unchecked((int)0x8000002C);
        internal const int LINEERR_INVALLOCATION = unchecked((int)0x8000002D);
        internal const int LINEERR_INVALMEDIALIST = unchecked((int)0x8000002E);
        internal const int LINEERR_INVALMEDIAMODE = unchecked((int)0x8000002F);
        internal const int LINEERR_INVALMESSAGEID = unchecked((int)0x80000030);
        internal const int LINEERR_INVALPARAM = unchecked((int)0x80000032);
        internal const int LINEERR_INVALPARKID = unchecked((int)0x80000033);
        internal const int LINEERR_INVALPARKMODE = unchecked((int)0x80000034);
        internal const int LINEERR_INVALPOINTER = unchecked((int)0x80000035);
        internal const int LINEERR_INVALPRIVSELECT = unchecked((int)0x80000036);
        internal const int LINEERR_INVALRATE = unchecked((int)0x80000037);
        internal const int LINEERR_INVALREQUESTMODE = unchecked((int)0x80000038);
        internal const int LINEERR_INVALTERMINALID = unchecked((int)0x80000039);
        internal const int LINEERR_INVALTERMINALMODE = unchecked((int)0x8000003A);
        internal const int LINEERR_INVALTIMEOUT = unchecked((int)0x8000003B);
        internal const int LINEERR_INVALTONE = unchecked((int)0x8000003C);
        internal const int LINEERR_INVALTONELIST = unchecked((int)0x8000003D);
        internal const int LINEERR_INVALTONEMODE = unchecked((int)0x8000003E);
        internal const int LINEERR_INVALTRANSFERMODE = unchecked((int)0x8000003F);
        internal const int LINEERR_LINEMAPPERFAILED = unchecked((int)0x80000040);
        internal const int LINEERR_NOCONFERENCE = unchecked((int)0x80000041);
        internal const int LINEERR_NODEVICE = unchecked((int)0x80000042);
        internal const int LINEERR_NODRIVER = unchecked((int)0x80000043);
        internal const int LINEERR_NOMEM = unchecked((int)0x80000044);
        internal const int LINEERR_NOREQUEST = unchecked((int)0x80000045);
        internal const int LINEERR_NOTOWNER = unchecked((int)0x80000046);
        internal const int LINEERR_NOTREGISTERED = unchecked((int)0x80000047);
        internal const int LINEERR_OPERATIONFAILED = unchecked((int)0x80000048);
        internal const int LINEERR_OPERATIONUNAVAIL = unchecked((int)0x80000049);
        internal const int LINEERR_RATEUNAVAIL = unchecked((int)0x8000004A);
        internal const int LINEERR_RESOURCEUNAVAIL = unchecked((int)0x8000004B);
        internal const int LINEERR_REQUESTOVERRUN = unchecked((int)0x8000004C);
        internal const int LINEERR_STRUCTURETOOSMALL = unchecked((int)0x8000004D);
        internal const int LINEERR_TARGETNOTFOUND = unchecked((int)0x8000004E);
        internal const int LINEERR_TARGETSELF = unchecked((int)0x8000004F);
        internal const int LINEERR_UNINITIALIZED = unchecked((int)0x80000050);
        internal const int LINEERR_USERUSERINFOTOOBIG = unchecked((int)0x80000051);
        internal const int LINEERR_REINIT = unchecked((int)0x80000052);
        internal const int LINEERR_ADDRESSBLOCKED = unchecked((int)0x80000053);
        internal const int LINEERR_BILLINGREJECTED = unchecked((int)0x80000054);
        internal const int LINEERR_INVALFEATURE = unchecked((int)0x80000055);
        internal const int LINEERR_NOMULTIPLEINSTANCE = unchecked((int)0x80000056);
        internal const int LINEERR_INVALAGENTID = unchecked((int)0x80000057);
        internal const int LINEERR_INVALAGENTGROUP = unchecked((int)0x80000058);
        internal const int LINEERR_INVALPASSWORD = unchecked((int)0x80000059);
        internal const int LINEERR_INVALAGENTSTATE = unchecked((int)0x8000005A);
        internal const int LINEERR_INVALAGENTACTIVITY = unchecked((int)0x8000005B);
        internal const int LINEERR_DIALVOICEDETECT = unchecked((int)0x8000005C);

        internal const int PHONEERR_OK = 0;
        internal const int PHONEERR_ALLOCATED = unchecked((int)0x90000001);
        internal const int PHONEERR_BADDEVICEID = unchecked((int)0x90000002);
        internal const int PHONEERR_INCOMPATIBLEAPIVERSION = unchecked((int)0x90000003);
        internal const int PHONEERR_INCOMPATIBLEEXTVERSION = unchecked((int)0x90000004);
        internal const int PHONEERR_INIFILECORRUPT = unchecked((int)0x90000005);
        internal const int PHONEERR_INUSE = unchecked((int)0x90000006);
        internal const int PHONEERR_INVALAPPHANDLE = unchecked((int)0x90000007);
        internal const int PHONEERR_INVALAPPNAME = unchecked((int)0x90000008);
        internal const int PHONEERR_INVALBUTTONLAMPID = unchecked((int)0x90000009);
        internal const int PHONEERR_INVALBUTTONMODE = unchecked((int)0x9000000A);
        internal const int PHONEERR_INVALBUTTONSTATE = unchecked((int)0x9000000B);
        internal const int PHONEERR_INVALDATAID = unchecked((int)0x9000000C);
        internal const int PHONEERR_INVALDEVICECLASS = unchecked((int)0x9000000D);
        internal const int PHONEERR_INVALEXTVERSION = unchecked((int)0x9000000E);
        internal const int PHONEERR_INVALHOOKSWITCHDEV = unchecked((int)0x9000000F);
        internal const int PHONEERR_INVALHOOKSWITCHMODE = unchecked((int)0x90000010);
        internal const int PHONEERR_INVALLAMPMODE = unchecked((int)0x90000011);
        internal const int PHONEERR_INVALPARAM = unchecked((int)0x90000012);
        internal const int PHONEERR_INVALPHONEHANDLE = unchecked((int)0x90000013);
        internal const int PHONEERR_INVALPHONESTATE = unchecked((int)0x90000014);
        internal const int PHONEERR_INVALPOINTER = unchecked((int)0x90000015);
        internal const int PHONEERR_INVALPRIVILEGE = unchecked((int)0x90000016);
        internal const int PHONEERR_INVALRINGMODE = unchecked((int)0x90000017);
        internal const int PHONEERR_NODEVICE = unchecked((int)0x90000018);
        internal const int PHONEERR_NODRIVER = unchecked((int)0x90000019);
        internal const int PHONEERR_NOMEM = unchecked((int)0x9000001A);
        internal const int PHONEERR_NOTOWNER = unchecked((int)0x9000001B);
        internal const int PHONEERR_OPERATIONFAILED = unchecked((int)0x9000001C);
        internal const int PHONEERR_OPERATIONUNAVAIL = unchecked((int)0x9000001D);
        internal const int PHONEERR_RESOURCEUNAVAIL = unchecked((int)0x9000001F);
        internal const int PHONEERR_REQUESTOVERRUN = unchecked((int)0x90000020);
        internal const int PHONEERR_STRUCTURETOOSMALL = unchecked((int)0x90000021);
        internal const int PHONEERR_UNINITIALIZED = unchecked((int)0x90000022);
        internal const int PHONEERR_REINIT = unchecked((int)0x90000023);
        internal const int PHONEERR_DISCONNECTED = unchecked((int)0x90000024);
        internal const int PHONEERR_SERVICE_NOT_RUNNING = unchecked((int)0x90000025);
        
        internal const int LINEFEATURE_DEVSPECIFIC = unchecked((int)0x00000001);
        internal const int LINEFEATURE_DEVSPECIFICFEAT = unchecked((int)0x00000002);
        internal const int LINEFEATURE_FORWARD = unchecked((int)0x00000004);
        internal const int LINEFEATURE_MAKECALL = unchecked((int)0x00000008);
        internal const int LINEFEATURE_SETMEDIACONTROL = unchecked((int)0x00000010);
        internal const int LINEFEATURE_SETTERMINAL = unchecked((int)0x00000020);
        internal const int LINEFEATURE_SETDEVSTATUS = unchecked((int)0x00000040);      // TAPI v2.0
        internal const int LINEFEATURE_FORWARDFWD = unchecked((int)0x00000080);        // TAPI v2.0
        internal const int LINEFEATURE_FORWARDDND = unchecked((int)0x00000100);         // TAPI v2.0    

        internal const int LINEFORWARDMODE_UNCOND = unchecked((int)0x00000001);
        internal const int LINEFORWARDMODE_UNCONDINTERNAL = unchecked((int)0x00000002);
        internal const int LINEFORWARDMODE_UNCONDEXTERNAL = unchecked((int)0x00000004);
        internal const int LINEFORWARDMODE_UNCONDSPECIFIC = unchecked((int)0x00000008);
        internal const int LINEFORWARDMODE_BUSY = unchecked((int)0x00000010);
        internal const int LINEFORWARDMODE_BUSYINTERNAL = unchecked((int)0x00000020);
        internal const int LINEFORWARDMODE_BUSYEXTERNAL = unchecked((int)0x00000040);
        internal const int LINEFORWARDMODE_BUSYSPECIFIC = unchecked((int)0x00000080);
        internal const int LINEFORWARDMODE_NOANSW = unchecked((int)0x00000100);
        internal const int LINEFORWARDMODE_NOANSWINTERNAL = unchecked((int)0x00000200);
        internal const int LINEFORWARDMODE_NOANSWEXTERNAL = unchecked((int)0x00000400);
        internal const int LINEFORWARDMODE_NOANSWSPECIFIC = unchecked((int)0x00000800);
        internal const int LINEFORWARDMODE_BUSYNA = unchecked((int)0x00001000);
        internal const int LINEFORWARDMODE_BUSYNAINTERNAL = unchecked((int)0x00002000);
        internal const int LINEFORWARDMODE_BUSYNAEXTERNAL = unchecked((int)0x00004000);
        internal const int LINEFORWARDMODE_BUSYNASPECIFIC = unchecked((int)0x00008000);
        internal const int LINEFORWARDMODE_UNKNOWN = unchecked((int)0x00010000);           // TAPI v1.4
        internal const int LINEFORWARDMODE_UNAVAIL = unchecked((int)0x00020000);            // TAPI v1.4    

        internal const int LINEGATHERTERM_BUFFERFULL = unchecked((int)0x00000001);
        internal const int LINEGATHERTERM_TERMDIGIT = unchecked((int)0x00000002);
        internal const int LINEGATHERTERM_FIRSTTIMEOUT = unchecked((int)0x00000004);
        internal const int LINEGATHERTERM_INTERTIMEOUT = unchecked((int)0x00000008);
        internal const int LINEGATHERTERM_CANCEL = unchecked((int)0x00000010);

        internal const int LINEGENERATETERM_DONE = unchecked((int)0x00000001);
        internal const int LINEGENERATETERM_CANCEL = unchecked((int)0x00000002);

        internal const int LINEINITIALIZEEXOPTION_USEHIDDENWINDOW = unchecked((int)0x00000001);  // TAPI v2.0
        internal const int LINEINITIALIZEEXOPTION_USEEVENT = unchecked((int)0x00000002);  // TAPI v2.0
        internal const int LINEINITIALIZEEXOPTION_USECOMPLETIONPORT = unchecked((int)0x00000003);  // TAPI v2.0

        internal const int PHONEINITIALIZEEXOPTION_USEHIDDENWINDOW = unchecked((int)0x00000001);  // TAPI v2.0
        internal const int PHONEINITIALIZEEXOPTION_USEEVENT = unchecked((int)0x00000002);  // TAPI v2.0
        internal const int PHONEINITIALIZEEXOPTION_USECOMPLETIONPORT = unchecked((int)0x00000003);  // TAPI v2.0

        internal const int LINELOCATIONOPTION_PULSEDIAL = unchecked((int)0x00000001);

        internal const int LINEMEDIAMODE_UNKNOWN = unchecked((int)0x00000002);
        internal const int LINEMEDIAMODE_INTERACTIVEVOICE = unchecked((int)0x00000004);
        internal const int LINEMEDIAMODE_AUTOMATEDVOICE = unchecked((int)0x00000008);
        internal const int LINEMEDIAMODE_DATAMODEM = unchecked((int)0x00000010);
        internal const int LINEMEDIAMODE_G3FAX = unchecked((int)0x00000020);
        internal const int LINEMEDIAMODE_TDD = unchecked((int)0x00000040);
        internal const int LINEMEDIAMODE_G4FAX = unchecked((int)0x00000080);
        internal const int LINEMEDIAMODE_DIGITALDATA = unchecked((int)0x00000100);
        internal const int LINEMEDIAMODE_TELETEX = unchecked((int)0x00000200);
        internal const int LINEMEDIAMODE_VIDEOTEX = unchecked((int)0x00000400);
        internal const int LINEMEDIAMODE_TELEX = unchecked((int)0x00000800);
        internal const int LINEMEDIAMODE_MIXED = unchecked((int)0x00001000);
        internal const int LINEMEDIAMODE_ADSI = unchecked((int)0x00002000);
        internal const int LINEMEDIAMODE_VOICEVIEW = unchecked((int)0x00004000);
        internal const int LINEMEDIAMODE_VIDEO = unchecked((int)0x00008000);

        internal const int LINEOFFERINGMODE_ACTIVE = unchecked((int)0x00000001);       // TAPI v1.4
        internal const int LINEOFFERINGMODE_INACTIVE = unchecked((int)0x00000002);     // TAPI v1.4

        internal const int LINEOPENOPTION_SINGLEADDRESS = unchecked((int)0x80000000);      // TAPI v2.0
        internal const int LINEOPENOPTION_PROXY = unchecked((int)0x40000000);               // TAPI v2.0

        internal const int LINEPARKMODE_DIRECTED = unchecked((int)0x00000001);
        internal const int LINEPARKMODE_NONDIRECTED = unchecked((int)0x00000002);

        internal const int LINEROAMMODE_UNKNOWN = unchecked((int)0x00000001);
        internal const int LINEROAMMODE_UNAVAIL = unchecked((int)0x00000002);
        internal const int LINEROAMMODE_HOME = unchecked((int)0x00000004);
        internal const int LINEROAMMODE_ROAMA = unchecked((int)0x00000008);
        internal const int LINEROAMMODE_ROAMB = unchecked((int)0x00000010);

        internal const int LINEREMOVEFROMCONF_NONE = unchecked((int)0x00000001);
        internal const int LINEREMOVEFROMCONF_LAST = unchecked((int)0x00000002);
        internal const int LINEREMOVEFROMCONF_ANY = unchecked((int)0x00000003);

        internal const int LINESPECIALINFO_NOCIRCUIT = unchecked((int)0x00000001);
        internal const int LINESPECIALINFO_CUSTIRREG = unchecked((int)0x00000002);
        internal const int LINESPECIALINFO_REORDER = unchecked((int)0x00000004);
        internal const int LINESPECIALINFO_UNKNOWN = unchecked((int)0x00000008);
        internal const int LINESPECIALINFO_UNAVAIL = unchecked((int)0x00000010);

        internal const int LINETERMDEV_PHONE = unchecked((int)0x00000001);
        internal const int LINETERMDEV_HEADSET = unchecked((int)0x00000002);
        internal const int LINETERMDEV_SPEAKER = unchecked((int)0x00000004);

        internal const int LINETERMMODE_BUTTONS = unchecked((int)0x00000001);
        internal const int LINETERMMODE_LAMPS = unchecked((int)0x00000002);
        internal const int LINETERMMODE_DISPLAY = unchecked((int)0x00000004);
        internal const int LINETERMMODE_RINGER = unchecked((int)0x00000008);
        internal const int LINETERMMODE_HOOKSWITCH = unchecked((int)0x00000010);
        internal const int LINETERMMODE_MEDIATOLINE = unchecked((int)0x00000020);
        internal const int LINETERMMODE_MEDIAFROMLINE = unchecked((int)0x00000040);
        internal const int LINETERMMODE_MEDIABIDIRECT = unchecked((int)0x00000080);

        internal const int LINETERMSHARING_PRIVATE = unchecked((int)0x00000001);
        internal const int LINETERMSHARING_SHAREDEXCL = unchecked((int)0x00000002);
        internal const int LINETERMSHARING_SHAREDCONF = unchecked((int)0x00000004);

        internal const int LINETONEMODE_CUSTOM = unchecked((int)0x00000001);
        internal const int LINETONEMODE_RINGBACK = unchecked((int)0x00000002);
        internal const int LINETONEMODE_BUSY = unchecked((int)0x00000004);
        internal const int LINETONEMODE_BEEP = unchecked((int)0x00000008);
        internal const int LINETONEMODE_BILLING = unchecked((int)0x00000010);

        internal const int LINETRANSFERMODE_TRANSFER = unchecked((int)0x00000001);
        internal const int LINETRANSFERMODE_CONFERENCE = unchecked((int)0x00000002);

        internal const int LINETRANSLATEOPTION_CARDOVERRIDE = unchecked((int)0x00000001);
        internal const int LINETRANSLATEOPTION_CANCELCALLWAITING = unchecked((int)0x00000002); // TAPI v1.4
        internal const int LINETRANSLATEOPTION_FORCELOCAL = unchecked((int)0x00000004);        // TAPI v1.4
        internal const int LINETRANSLATEOPTION_FORCELD = unchecked((int)0x00000008);           // TAPI v1.4

        internal const int LINETRANSLATERESULT_CANONICAL = unchecked((int)0x00000001);
        internal const int LINETRANSLATERESULT_INTERNATIONAL = unchecked((int)0x00000002);
        internal const int LINETRANSLATERESULT_LONGDISTANCE = unchecked((int)0x00000004);
        internal const int LINETRANSLATERESULT_LOCAL = unchecked((int)0x00000008);
        internal const int LINETRANSLATERESULT_INTOLLLIST = unchecked((int)0x00000010);
        internal const int LINETRANSLATERESULT_NOTINTOLLLIST = unchecked((int)0x00000020);
        internal const int LINETRANSLATERESULT_DIALBILLING = unchecked((int)0x00000040);
        internal const int LINETRANSLATERESULT_DIALQUIET = unchecked((int)0x00000080);
        internal const int LINETRANSLATERESULT_DIALDIALTONE = unchecked((int)0x00000100);
        internal const int LINETRANSLATERESULT_DIALPROMPT = unchecked((int)0x00000200);
        internal const int LINETRANSLATERESULT_VOICEDETECT = unchecked((int)0x00000400);       // TAPI v2.0
        internal const int LINETRANSLATERESULT_NOTRANSLATION = unchecked((int)0x00000800);      // TAPI v3.0

        internal const int PHONESTATE_OTHER = unchecked((int)0x00000001);
        internal const int PHONESTATE_CONNECTED = unchecked((int)0x00000002);
        internal const int PHONESTATE_DISCONNECTED = unchecked((int)0x00000004);
        internal const int PHONESTATE_OWNER = unchecked((int)0x00000008);
        internal const int PHONESTATE_MONITORS = unchecked((int)0x00000010);
        internal const int PHONESTATE_DISPLAY = unchecked((int)0x00000020);
        internal const int PHONESTATE_LAMP = unchecked((int)0x00000040);
        internal const int PHONESTATE_RINGMODE = unchecked((int)0x00000080);
        internal const int PHONESTATE_RINGVOLUME = unchecked((int)0x00000100);
        internal const int PHONESTATE_HANDSETHOOKSWITCH = unchecked((int)0x00000200);
        internal const int PHONESTATE_HANDSETVOLUME = unchecked((int)0x00000400);
        internal const int PHONESTATE_HANDSETGAIN = unchecked((int)0x00000800);
        internal const int PHONESTATE_SPEAKERHOOKSWITCH = unchecked((int)0x00001000);
        internal const int PHONESTATE_SPEAKERVOLUME = unchecked((int)0x00002000);
        internal const int PHONESTATE_SPEAKERGAIN = unchecked((int)0x00004000);
        internal const int PHONESTATE_HEADSETHOOKSWITCH = unchecked((int)0x00008000);
        internal const int PHONESTATE_HEADSETVOLUME = unchecked((int)0x00010000);
        internal const int PHONESTATE_HEADSETGAIN = unchecked((int)0x00020000);
        internal const int PHONESTATE_SUSPEND = unchecked((int)0x00040000);
        internal const int PHONESTATE_RESUME = unchecked((int)0x00080000);
        internal const int PHONESTATE_DEVSPECIFIC = unchecked((int)0x00100000);
        internal const int PHONESTATE_REINIT = unchecked((int)0x00200000);
        internal const int PHONESTATE_CAPSCHANGE = unchecked((int)0x00400000);      // TAPI v1.4
        internal const int PHONESTATE_REMOVED = unchecked((int)0x00800000);      // TAPI v1.4

        internal const int PHONEFEATURE_GETBUTTONINFO = unchecked((int)0x00000001);      // TAPI v2.0
        internal const int PHONEFEATURE_GETDATA = unchecked((int)0x00000002);      // TAPI v2.0
        internal const int PHONEFEATURE_GETDISPLAY = unchecked((int)0x00000004);     // TAPI v2.0
        internal const int PHONEFEATURE_GETGAINHANDSET = unchecked((int)0x00000008);      // TAPI v2.0
        internal const int PHONEFEATURE_GETGAINSPEAKER = unchecked((int)0x00000010);      // TAPI v2.0
        internal const int PHONEFEATURE_GETGAINHEADSET = unchecked((int)0x00000020);      // TAPI v2.0
        internal const int PHONEFEATURE_GETHOOKSWITCHHANDSET = unchecked((int)0x00000040);      // TAPI v2.0
        internal const int PHONEFEATURE_GETHOOKSWITCHSPEAKER = unchecked((int)0x00000080);      // TAPI v2.0
        internal const int PHONEFEATURE_GETHOOKSWITCHHEADSET = unchecked((int)0x00000100);      // TAPI v2.0
        internal const int PHONEFEATURE_GETLAMP = unchecked((int)0x00000200);      // TAPI v2.0
        internal const int PHONEFEATURE_GETRING = unchecked((int)0x00000400);      // TAPI v2.0
        internal const int PHONEFEATURE_GETVOLUMEHANDSET = unchecked((int)0x00000800);      // TAPI v2.0
        internal const int PHONEFEATURE_GETVOLUMESPEAKER = unchecked((int)0x00001000);      // TAPI v2.0
        internal const int PHONEFEATURE_GETVOLUMEHEADSET = unchecked((int)0x00002000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETBUTTONINFO = unchecked((int)0x00004000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETDATA = unchecked((int)0x00008000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETDISPLAY = unchecked((int)0x00010000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETGAINHANDSET = unchecked((int)0x00020000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETGAINSPEAKER = unchecked((int)0x00040000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETGAINHEADSET = unchecked((int)0x00080000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETHOOKSWITCHHANDSET = unchecked((int)0x00100000);     // TAPI v2.0
        internal const int PHONEFEATURE_SETHOOKSWITCHSPEAKER = unchecked((int)0x00200000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETHOOKSWITCHHEADSET = unchecked((int)0x00400000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETLAMP = unchecked((int)0x00800000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETRING = unchecked((int)0x01000000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETVOLUMEHANDSET = unchecked((int)0x02000000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETVOLUMESPEAKER = unchecked((int)0x04000000);      // TAPI v2.0
        internal const int PHONEFEATURE_SETVOLUMEHEADSET = unchecked((int)0x08000000);      // TAPI v2.0
        internal const int PHONEFEATURE_GENERICPHONE = unchecked((int)0x10000000);      // TAPI v3.1

        internal const int PHONEHOOKSWITCHDEV_HANDSET = unchecked((int)0x00000001);
        internal const int PHONEHOOKSWITCHDEV_SPEAKER = unchecked((int)0x00000002);
        internal const int PHONEHOOKSWITCHDEV_HEADSET = unchecked((int)0x00000004);

        internal const int PHONEHOOKSWITCHMODE_ONHOOK = unchecked((int)0x00000001);
        internal const int PHONEHOOKSWITCHMODE_MIC = unchecked((int)0x00000002);
        internal const int PHONEHOOKSWITCHMODE_SPEAKER = unchecked((int)0x00000004);
        internal const int PHONEHOOKSWITCHMODE_MICSPEAKER = unchecked((int)0x00000008);
        internal const int PHONEHOOKSWITCHMODE_UNKNOWN = unchecked((int)0x00000010);

        internal const int PHONELAMPMODE_DUMMY = unchecked((int)0x00000001);
        internal const int PHONELAMPMODE_OFF = unchecked((int)0x00000002);
        internal const int PHONELAMPMODE_STEADY = unchecked((int)0x00000004);
        internal const int PHONELAMPMODE_WINK = unchecked((int)0x00000008);
        internal const int PHONELAMPMODE_FLASH = unchecked((int)0x00000010);
        internal const int PHONELAMPMODE_FLUTTER = unchecked((int)0x00000020);
        internal const int PHONELAMPMODE_BROKENFLUTTER = unchecked((int)0x00000040);
        internal const int PHONELAMPMODE_UNKNOWN = unchecked((int)0x00000080);

        internal const int PHONEPRIVILEGE_MONITOR = unchecked((int)0x00000001);
        internal const int PHONEPRIVILEGE_OWNER = unchecked((int)0x00000002);

        internal const int PHONESTATUSFLAGS_CONNECTED = unchecked((int)0x00000001);
        internal const int PHONESTATUSFLAGS_SUSPENDED = unchecked((int)0x00000002);

        internal const int PHONEBUTTONFUNCTION_UNKNOWN = unchecked((int)0x00000000);
        internal const int PHONEBUTTONFUNCTION_CONFERENCE = unchecked((int)0x00000001);
        internal const int PHONEBUTTONFUNCTION_TRANSFER = unchecked((int)0x00000002);
        internal const int PHONEBUTTONFUNCTION_DROP = unchecked((int)0x00000003);
        internal const int PHONEBUTTONFUNCTION_HOLD = unchecked((int)0x00000004);
        internal const int PHONEBUTTONFUNCTION_RECALL = unchecked((int)0x00000005);
        internal const int PHONEBUTTONFUNCTION_DISCONNECT = unchecked((int)0x00000006);
        internal const int PHONEBUTTONFUNCTION_CONNECT = unchecked((int)0x00000007);
        internal const int PHONEBUTTONFUNCTION_MSGWAITON = unchecked((int)0x00000008);
        internal const int PHONEBUTTONFUNCTION_MSGWAITOFF = unchecked((int)0x00000009);
        internal const int PHONEBUTTONFUNCTION_SELECTRING = unchecked((int)0x0000000A);
        internal const int PHONEBUTTONFUNCTION_ABBREVDIAL = unchecked((int)0x0000000B);
        internal const int PHONEBUTTONFUNCTION_FORWARD = unchecked((int)0x0000000C);
        internal const int PHONEBUTTONFUNCTION_PICKUP = unchecked((int)0x0000000D);
        internal const int PHONEBUTTONFUNCTION_RINGAGAIN = unchecked((int)0x0000000E);
        internal const int PHONEBUTTONFUNCTION_PARK = unchecked((int)0x0000000F);
        internal const int PHONEBUTTONFUNCTION_REJECT = unchecked((int)0x00000010);
        internal const int PHONEBUTTONFUNCTION_REDIRECT = unchecked((int)0x00000011);
        internal const int PHONEBUTTONFUNCTION_MUTE = unchecked((int)0x00000012);
        internal const int PHONEBUTTONFUNCTION_VOLUMEUP = unchecked((int)0x00000013);
        internal const int PHONEBUTTONFUNCTION_VOLUMEDOWN = unchecked((int)0x00000014);
        internal const int PHONEBUTTONFUNCTION_SPEAKERON = unchecked((int)0x00000015);
        internal const int PHONEBUTTONFUNCTION_SPEAKEROFF = unchecked((int)0x00000016);
        internal const int PHONEBUTTONFUNCTION_FLASH = unchecked((int)0x00000017);
        internal const int PHONEBUTTONFUNCTION_DATAON = unchecked((int)0x00000018);
        internal const int PHONEBUTTONFUNCTION_DATAOFF = unchecked((int)0x00000019);
        internal const int PHONEBUTTONFUNCTION_DONOTDISTURB = unchecked((int)0x0000001A);
        internal const int PHONEBUTTONFUNCTION_INTERCOM = unchecked((int)0x0000001B);
        internal const int PHONEBUTTONFUNCTION_BRIDGEDAPP = unchecked((int)0x0000001C);
        internal const int PHONEBUTTONFUNCTION_BUSY = unchecked((int)0x0000001D);
        internal const int PHONEBUTTONFUNCTION_CALLAPP = unchecked((int)0x0000001E);
        internal const int PHONEBUTTONFUNCTION_DATETIME = unchecked((int)0x0000001F);
        internal const int PHONEBUTTONFUNCTION_DIRECTORY = unchecked((int)0x00000020);
        internal const int PHONEBUTTONFUNCTION_COVER = unchecked((int)0x00000021);
        internal const int PHONEBUTTONFUNCTION_CALLID = unchecked((int)0x00000022);
        internal const int PHONEBUTTONFUNCTION_LASTNUM = unchecked((int)0x00000023);
        internal const int PHONEBUTTONFUNCTION_NIGHTSRV = unchecked((int)0x00000024);
        internal const int PHONEBUTTONFUNCTION_SENDCALLS = unchecked((int)0x00000025);
        internal const int PHONEBUTTONFUNCTION_MSGINDICATOR = unchecked((int)0x00000026);
        internal const int PHONEBUTTONFUNCTION_REPDIAL = unchecked((int)0x00000027);
        internal const int PHONEBUTTONFUNCTION_SETREPDIAL = unchecked((int)0x00000028);
        internal const int PHONEBUTTONFUNCTION_SYSTEMSPEED = unchecked((int)0x00000029);
        internal const int PHONEBUTTONFUNCTION_STATIONSPEED = unchecked((int)0x0000002A);
        internal const int PHONEBUTTONFUNCTION_CAMPON = unchecked((int)0x0000002B);
        internal const int PHONEBUTTONFUNCTION_SAVEREPEAT = unchecked((int)0x0000002C);
        internal const int PHONEBUTTONFUNCTION_QUEUECALL = unchecked((int)0x0000002D);
        internal const int PHONEBUTTONFUNCTION_NONE = unchecked((int)0x0000002E);
        internal const int PHONEBUTTONFUNCTION_SEND = unchecked((int)0x0000002F);      // TAPI v3.1

        internal const int PHONEBUTTONMODE_DUMMY = unchecked((int)0x00000001);
        internal const int PHONEBUTTONMODE_CALL = unchecked((int)0x00000002);
        internal const int PHONEBUTTONMODE_FEATURE = unchecked((int)0x00000004);
        internal const int PHONEBUTTONMODE_KEYPAD = unchecked((int)0x00000008);
        internal const int PHONEBUTTONMODE_LOCAL = unchecked((int)0x00000010);
        internal const int PHONEBUTTONMODE_DISPLAY = unchecked((int)0x00000020);

        internal const int PHONEBUTTONSTATE_UP = unchecked((int)0x00000001);
        internal const int PHONEBUTTONSTATE_DOWN = unchecked((int)0x00000002);
        internal const int PHONEBUTTONSTATE_UNKNOWN = unchecked((int)0x00000004);    // TAPI v1.4
        internal const int PHONEBUTTONSTATE_UNAVAIL = unchecked((int)0x00000008);      // TAPI v1.4

        internal const int STRINGFORMAT_ASCII = unchecked((int)0x00000001);
        internal const int STRINGFORMAT_DBCS = unchecked((int)0x00000002);
        internal const int STRINGFORMAT_UNICODE = unchecked((int)0x00000003);
        internal const int STRINGFORMAT_BINARY = unchecked((int)0x00000004);
        #endregion

        internal static string GetString(byte[] buff, int offset, int len, int stringType)
        {
            if (buff == null || buff.Length == 0 || len == 0)
                return string.Empty;

            Encoding enc = GetTextEncoding(stringType);
            if (enc == null)
                return string.Empty;

            if ((len - offset) > (buff.Length - offset))
                len = buff.Length - offset;
            return enc.GetString(buff, offset, len).Replace("\0", "");
        }

        internal static void WriteByteArray(byte[] buff, IntPtr ptr, int offset)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException("ptr");
            if (buff == null || buff.Length == 0)
                return;

            IntPtr dataPtr = Marshal.AllocHGlobal(buff.Length);
            try
            {
                Marshal.Copy(buff, 0, dataPtr, buff.Length);
                Marshal.WriteIntPtr(ptr, offset, dataPtr);
            }
            finally
            {
                Marshal.FreeHGlobal(dataPtr);
            }
        }

        private static Encoding GetTextEncoding(int stringType)
        {
            switch (stringType)
            {
                case STRINGFORMAT_ASCII:
                    return ASCIIEncoding.ASCII;
                case STRINGFORMAT_UNICODE:
                    return UnicodeEncoding.Default;
                case STRINGFORMAT_DBCS:
                    return Encoding.GetEncoding(1252);
                default:
                    break;
            }
            return null;
        }	    
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LINEINITIALIZEEXPARAMS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwOptions;
        internal IntPtr hEvent;
        internal int dwCompletionKey;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PHONEINITIALIZEEXPARAMS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwOptions;
        internal IntPtr hEvent;
        internal int dwCompletionKey;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEADDRESSCAPS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwLineDeviceID;
        internal int dwAddressSize;
        internal int dwAddressOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwAddressSharing;
        internal int dwAddressStates;
        internal int dwCallInfoStates;
        internal int dwCallerIDFlags;
        internal int dwCalledIDFlags;
        internal int dwConnectedIDFlags;
        internal int dwRedirectionIDFlags;
        internal int dwRedirectingIDFlags;
        internal int dwCallStates;
        internal int dwDialToneModes;
        internal int dwBusyModes;
        internal int dwSpecialInfo;
        internal int dwDisconnectModes;
        internal int dwMaxNumActiveCalls;
        internal int dwMaxNumOnHoldCalls;
        internal int dwMaxNumOnHoldPendingCalls;
        internal int dwMaxNumConference;
        internal int dwMaxNumTransConf;
        internal int dwAddrCapFlags;
        internal int dwCallFeatures;
        internal int dwRemoveFromConfCaps;
        internal int dwRemoveFromConfState;
        internal int dwTransferModes;
        internal int dwParkModes;
        internal int dwForwardModes;
        internal int dwMaxForwardEntries;
        internal int dwMaxSpecificEntries;
        internal int dwMinFwdNumRings;
        internal int dwMaxFwdNumRings;
        internal int dwMaxCallCompletions;
        internal int dwCallCompletionConds;
        internal int dwCallCompletionModes;
        internal int dwNumCompletionMessages;
        internal int dwCompletionMsgTextEntrySize;
        internal int dwCompletionMsgTextSize;
        internal int dwCompletionMsgTextOffset;
        internal int dwAddressFeatures;
        internal int dwPredictiveAutoTransferStates;
        internal int dwNumCallTreatments;
        internal int dwCallTreatmentListSize;
        internal int dwCallTreatmentListOffset;
        internal int dwDeviceClassesSize;
        internal int dwDeviceClassesOffset;
        internal int dwMaxCallDataSize;
        internal int dwCallFeatures2;
        internal int dwMaxNoAnswerTimeout;
        internal int dwConnectedModes;
        internal int dwOfferingModes;
        internal int dwAvailableMediaModes;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEADDRESSSTATUS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwNumInUse;
        internal int dwNumActiveCalls;
        internal int dwNumOnHoldCalls;
        internal int dwNumOnHoldPendCalls;
        internal int dwAddressFeatures;
        internal int dwNumRingsNoAnswer;
        internal int dwForwardNumEntries;
        internal int dwForwardSize;
        internal int dwForwardOffset;
        internal int dwTerminalModesSize;
        internal int dwTerminalModesOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECALLINFO
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int hLine; 
        internal int dwLineDeviceID;
        internal int dwAddressID;
        internal int dwBearerMode;
        internal int dwRate;
        internal int dwMediaMode;
        internal int dwAppSpecific;
        internal int dwCallID;
        internal int dwRelatedCallID;
        internal int dwCallParamFlags;
        internal int dwCallStates;
        internal int dwMonitorDigitModes;
        internal int dwMonitorMediaModes;
        internal LINEDIALPARAMS DialParams;
        internal int dwOrigin;
        internal int dwReason;
        internal int dwCompletionID;
        internal int dwNumOwners;
        internal int dwNumMonitors;
        internal int dwCountryCode;
        internal int dwTrunk;
        internal int dwCallerIDFlags;
        internal int dwCallerIDSize;
        internal int dwCallerIDOffset;
        internal int dwCallerIDNameSize;
        internal int dwCallerIDNameOffset;
        internal int dwCalledIDFlags;
        internal int dwCalledIDSize;
        internal int dwCalledIDOffset;
        internal int dwCalledIDNameSize;
        internal int dwCalledIDNameOffset;
        internal int dwConnectedIDFlags;
        internal int dwConnectedIDSize;
        internal int dwConnectedIDOffset;
        internal int dwConnectedIDNameSize;
        internal int dwConnectedIDNameOffset;
        internal int dwRedirectionIDFlags;
        internal int dwRedirectionIDSize;
        internal int dwRedirectionIDOffset;
        internal int dwRedirectionIDNameSize;
        internal int dwRedirectionIDNameOffset;
        internal int dwRedirectingIDFlags;
        internal int dwRedirectingIDSize;
        internal int dwRedirectingIDOffset;
        internal int dwRedirectingIDNameSize;
        internal int dwRedirectingIDNameOffset;
        internal int dwAppNameSize;
        internal int dwAppNameOffset;
        internal int dwDisplayableAddressSize;
        internal int dwDisplayableAddressOffset;
        internal int dwCalledPartySize;
        internal int dwCalledPartyOffset;
        internal int dwCommentSize;
        internal int dwCommentOffset;
        internal int dwDisplaySize;
        internal int dwDisplayOffset;
        internal int dwUserUserInfoSize;
        internal int dwUserUserInfoOffset;
        internal int dwHighLevelCompSize;
        internal int dwHighLevelCompOffset;
        internal int dwLowLevelCompSize;
        internal int dwLowLevelCompOffset;
        internal int dwChargingInfoSize;
        internal int dwChargingInfoOffset;
        internal int dwTerminalModesSize;
        internal int dwTerminalModesOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwCallTreatment;
        internal int dwCallDataSize;
        internal int dwCallDataOffset;
        internal int dwSendingFlowspecSize;
        internal int dwSendingFlowspecOffset;
        internal int dwReceivingFlowspecSize;
        internal int dwReceivingFlowspecOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECALLLIST
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwCallsNumEntries;
        internal int dwCallsSize;
        internal int dwCallsOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECALLPARAMS
    {
        internal int dwTotalSize;
        internal int dwBearerMode;              // voice
        internal int dwMinRate;                 // (3.1kHz)
        internal int dwMaxRate;                 // (3.1kHz)
        internal int dwMediaMode;               // interactiveVoice
        internal int dwCallParamFlags;          // 0
        internal int dwAddressMode;             // addressID
        internal int dwAddressID;               // (any available)
        internal LINEDIALPARAMS DialParams;        // (0, 0, 0, 0)
        internal int dwOrigAddressSize;         // 0
        internal int dwOrigAddressOffset;
        internal int dwDisplayableAddressSize;  // 0
        internal int dwDisplayableAddressOffset;
        internal int dwCalledPartySize;         // 0
        internal int dwCalledPartyOffset;
        internal int dwCommentSize;             // 0
        internal int dwCommentOffset;
        internal int dwUserUserInfoSize;        // 0
        internal int dwUserUserInfoOffset;
        internal int dwHighLevelCompSize;       // 0
        internal int dwHighLevelCompOffset;
        internal int dwLowLevelCompSize;        // 0
        internal int dwLowLevelCompOffset;
        internal int dwDevSpecificSize;         // 0
        internal int dwDevSpecificOffset;
        internal int dwPredictiveAutoTransferStates;//TAPI Version 2.0
        internal int dwTargetAddressSize;     //TAPI Version 2.0
        internal int dwTargetAddressOffset;   //TAPI Version 2.0
        internal int dwSendingFlowspecSize;   //TAPI Version 2.0
        internal int dwSendingFlowspecOffset; //TAPI Version 2.0
        internal int dwReceivingFlowspecSize;   //TAPI Version 2.0
        internal int dwReceivingFlowspecOffset; //TAPI Version 2.0
        internal int dwDeviceClassSize;      //TAPI Version 2.0
        internal int dwDeviceClassOffset;    //TAPI Version 2.0
        internal int dwDeviceConfigSize;     //TAPI Version 2.0
        internal int dwDeviceConfigOffset;   //TAPI Version 2.0
        internal int dwCallDataSize;         //TAPI Version 2.0
        internal int dwCallDataOffset;       //TAPI Version 2.0
        internal int dwNoAnswerTimeout;      //TAPI Version 2.0
        internal int dwCallingPartyIDSize;   //TAPI Version 2.0
        internal int dwCallingPartyIDOffset; //TAPI Version 2.0
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECALLSTATUS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwCallState;
        internal int dwCallStateMode;
        internal int dwCallPrivilege;
        internal int dwCallFeatures;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwCallFeatures2;                // TAPI v2.0
        internal DateTime tStateEntryTime;           // TAPI v2.0
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECALLTREATMENTENTRY
    {
        internal int dwCallTreatmentID;
        internal int dwCallTreatmentNameSize;
        internal int dwCallTreatmentNameOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECOUNTRYENTRY
    {
        internal int dwCountryID;                                    // TAPI v1.4
        internal int dwCountryCode;                                  // TAPI v1.4
        internal int dwNextCountryID;                                // TAPI v1.4
        internal int dwCountryNameSize;                              // TAPI v1.4
        internal int dwCountryNameOffset;                            // TAPI v1.4
        internal int dwSameAreaRuleSize;                             // TAPI v1.4
        internal int dwSameAreaRuleOffset;                           // TAPI v1.4
        internal int dwLongDistanceRuleSize;                         // TAPI v1.4
        internal int dwLongDistanceRuleOffset;                       // TAPI v1.4
        internal int dwInternationalRuleSize;                        // TAPI v1.4
        internal int dwInternationalRuleOffset;                      // TAPI v1.4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECOUNTRYLIST
    {
        internal int dwTotalSize;                                    // TAPI v1.4
        internal int dwNeededSize;                                   // TAPI v1.4
        internal int dwUsedSize;                                     // TAPI v1.4
        internal int dwNumCountries;                                 // TAPI v1.4
        internal int dwCountryListSize;                              // TAPI v1.4
        internal int dwCountryListOffset;                            // TAPI v1.4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEDEVCAPS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwProviderInfoSize;
        internal int dwProviderInfoOffset;
        internal int dwSwitchInfoSize;
        internal int dwSwitchInfoOffset;
        internal int dwPermanentLineID;
        internal int dwLineNameSize;
        internal int dwLineNameOffset;
        internal int dwStringFormat;
        internal int dwAddressModes;
        internal int dwNumAddresses;
        internal int dwBearerModes;
        internal int dwMaxRate;
        internal int dwMediaModes;
        internal int dwGenerateToneModes;
        internal int dwGenerateToneMaxNumFreq;
        internal int dwGenerateDigitModes;
        internal int dwMonitorToneMaxNumFreq;
        internal int dwMonitorToneMaxNumEntries;
        internal int dwMonitorDigitModes;
        internal int dwGatherDigitsMinTimeout;
        internal int dwGatherDigitsMaxTimeout;
        internal int dwMedCtlDigitMaxListSize;
        internal int dwMedCtlMediaMaxListSize;
        internal int dwMedCtlToneMaxListSize;
        internal int dwMedCtlCallStateMaxListSize;
        internal int dwDevCapFlags;
        internal int dwMaxNumActiveCalls;
        internal int dwAnswerMode;
        internal int dwRingModes;
        internal int dwLineStates;
        internal int dwUUIAcceptSize;
        internal int dwUUIAnswerSize;
        internal int dwUUIMakeCallSize;
        internal int dwUUIDropSize;
        internal int dwUUISendUserUserInfoSize;
        internal int dwUUICallInfoSize;
        internal LINEDIALPARAMS MinDialParams;
        internal LINEDIALPARAMS MaxDialParams;
        internal LINEDIALPARAMS DefaultDialParams;
        internal int dwNumTerminals;
        internal int dwTerminalCapsSize;
        internal int dwTerminalCapsOffset;
        internal int dwTerminalTextEntrySize;
        internal int dwTerminalTextSize;
        internal int dwTerminalTextOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwLineFeatures;
        internal int dwSettableDevStatus;
        internal int dwDeviceClassesSize;
        internal int dwDeviceClassesOffset;
        internal Guid PermanentLineGuid;      //TAPI Version 2.2
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEDEVSTATUS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwNumOpens;
        internal int dwOpenMediaModes;
        internal int dwNumActiveCalls;
        internal int dwNumOnHoldCalls;
        internal int dwNumOnHoldPendCalls;
        internal int dwLineFeatures;
        internal int dwNumCallCompletions;
        internal int dwRingMode;
        internal int dwSignalLevel;
        internal int dwBatteryLevel;
        internal int dwRoamMode;
        internal int dwDevStatusFlags;
        internal int dwTerminalModesSize;
        internal int dwTerminalModesOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwAvailableMediaModes;                          // TAPI v2.0
        internal int dwAppInfoSize;                                  // TAPI v2.0
        internal int dwAppInfoOffset;                                // TAPI v2.0
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEFORWARD
    {
        internal int dwForwardMode;
        internal int dwCallerAddressSize;
        internal int dwCallerAddressOffset;
        internal int dwDestCountryCode;
        internal int dwDestAddressSize;
        internal int dwDestAddressOffset;
        internal int dwCallerAddressType;
        internal int dwDestAddressType;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEFORWARDLIST
    {
        internal int dwTotalSize;
        internal int dwNumEntries;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal class LINETRANSLATECAPS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwNumLocations;
        internal int dwLocationListSize;
        internal int dwLocationListOffset;
        internal int dwCurrentLocationID;
        internal int dwNumCards;
        internal int dwCardListSize;
        internal int dwCardListOffset;
        internal int dwCurrentPreferredCardID;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINELOCATIONENTRY
    {
        internal int dwPermanentLocationID;
        internal int dwLocationNameSize;
        internal int dwLocationNameOffset;
        internal int dwCountryCode;
        internal int dwCityCodeSize;
        internal int dwCityCodeOffset;
        internal int dwPreferredCardID;
        internal int dwLocalAccessCodeSize;                          // TAPI v1.4
        internal int dwLocalAccessCodeOffset;                        // TAPI v1.4
        internal int dwLongDistanceAccessCodeSize;                   // TAPI v1.4
        internal int dwLongDistanceAccessCodeOffset;                 // TAPI v1.4
        internal int dwTollPrefixListSize;                           // TAPI v1.4
        internal int dwTollPrefixListOffset;                         // TAPI v1.4
        internal int dwCountryID;                                    // TAPI v1.4
        internal int dwOptions;                                      // TAPI v1.4
        internal int dwCancelCallWaitingSize;                        // TAPI v1.4
        internal int dwCancelCallWaitingOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINECARDENTRY
    {
        internal int dwPermanentCardID;
        internal int dwCardNameSize;
        internal int dwCardNameOffset;
        internal int dwCardNumberDigits;                             // TAPI v1.4
        internal int dwSameAreaRuleSize;                             // TAPI v1.4
        internal int dwSameAreaRuleOffset;                           // TAPI v1.4
        internal int dwLongDistanceRuleSize;                         // TAPI v1.4
        internal int dwLongDistanceRuleOffset;                       // TAPI v1.4
        internal int dwInternationalRuleSize;                        // TAPI v1.4
        internal int dwInternationalRuleOffset;                      // TAPI v1.4
        internal int dwOptions;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LINEDIALPARAMS
    {
        internal int dwDialPause;
        internal int dwDialSpeed;
        internal int dwDigitDuration;
        internal int dwWaitForDialtone;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LINEEXTENSIONID
    {
        internal int dwExtensionID0;
        internal int dwExtensionID1;
        internal int dwExtensionID2;
        internal int dwExtensionID3;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LINEGENERATETONE
    {
        internal int dwFrequency;
        internal int dwCadenceOn;
        internal int dwCadenceOff;
        internal int dwVolume;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LINEMESSAGE
    {
        internal uint hDevice;                           // TAPI v2.0
        internal TapiEvent dwMessageID;                  // TAPI v2.0
        internal IntPtr dwCallbackInstance;              // TAPI v2.0
        internal IntPtr dwParam1;                        // TAPI v2.0
        internal IntPtr dwParam2;                        // TAPI v2.0
        internal IntPtr dwParam3;                        // TAPI v2.0
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LINEMONITORTONE
    {
        internal int dwAppSpecific;
        internal int dwDuration;
        internal int dwFrequency1;
        internal int dwFrequency2;
        internal int dwFrequency3;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEPROVIDERENTRY
    {
        internal int dwPermanentProviderID;
        internal int dwProviderFilenameSize;
        internal int dwProviderFilenameOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINEPROVIDERLIST
    {
        internal int dwTotalSize;                                    // TAPI v1.4
        internal int dwNeededSize;                                   // TAPI v1.4
        internal int dwUsedSize;                                     // TAPI v1.4
        internal int dwNumProviders;                                 // TAPI v1.4
        internal int dwProviderListSize;                             // TAPI v1.4
        internal int dwProviderListOffset;                           // TAPI v1.4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LINETERMCAPS
    {
        internal int dwTermDev;
        internal int dwTermModes;
        internal int dwTermSharing;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class LINETRANSLATEOUTPUT
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwDialableStringSize;
        internal int dwDialableStringOffset;
        internal int dwDisplayableStringSize;
        internal int dwDisplayableStringOffset;
        internal int dwCurrentCountry;
        internal int dwDestCountry;
        internal int dwTranslateResults;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class PHONEBUTTONINFO
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwButtonMode;
        internal int dwButtonFunction;
        internal int dwButtonTextSize;
        internal int dwButtonTextOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwButtonState;                                  // TAPI v1.4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class PHONECAPS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwProviderInfoSize;
        internal int dwProviderInfoOffset;
        internal int dwPhoneInfoSize;
        internal int dwPhoneInfoOffset;
        internal int dwPermanentPhoneID;
        internal int dwPhoneNameSize;
        internal int dwPhoneNameOffset;
        internal int dwStringFormat;
        internal int dwPhoneStates;
        internal int dwHookSwitchDevs;
        internal int dwHandsetHookSwitchModes;
        internal int dwSpeakerHookSwitchModes;
        internal int dwHeadsetHookSwitchModes;
        internal int dwVolumeFlags;
        internal int dwGainFlags;
        internal int dwDisplayNumRows;
        internal int dwDisplayNumColumns;
        internal int dwNumRingModes;
        internal int dwNumButtonLamps;
        internal int dwButtonModesSize;
        internal int dwButtonModesOffset;
        internal int dwButtonFunctionsSize;
        internal int dwButtonFunctionsOffset;
        internal int dwLampModesSize;
        internal int dwLampModesOffset;
        internal int dwNumSetData;
        internal int dwSetDataSize;
        internal int dwSetDataOffset;
        internal int dwNumGetData;
        internal int dwGetDataSize;
        internal int dwGetDataOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwDeviceClassesSize;                            // TAPI v2.0
        internal int dwDeviceClassesOffset;                          // TAPI v2.0
        internal int dwPhoneFeatures;                                // TAPI v2.0
        internal int dwSettableHandsetHookSwitchModes;               // TAPI v2.0
        internal int dwSettableSpeakerHookSwitchModes;               // TAPI v2.0
        internal int dwSettableHeadsetHookSwitchModes;               // TAPI v2.0
        internal int dwMonitoredHandsetHookSwitchModes;              // TAPI v2.0
        internal int dwMonitoredSpeakerHookSwitchModes;              // TAPI v2.0
        internal int dwMonitoredHeadsetHookSwitchModes;              // TAPI v2.0
        internal Guid PermanentPhoneGuid;                             // TAPI v2.2
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PHONEEXTENSIONID
    {
        internal int dwExtensionID0;
        internal int dwExtensionID1;
        internal int dwExtensionID2;
        internal int dwExtensionID3;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class PHONESTATUS
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwStatusFlags;
        internal int dwNumOwners;
        internal int dwNumMonitors;
        internal int dwRingMode;
        internal int dwRingVolume;
        internal int dwHandsetHookSwitchMode;
        internal int dwHandsetVolume;
        internal int dwHandsetGain;
        internal int dwSpeakerHookSwitchMode;
        internal int dwSpeakerVolume;
        internal int dwSpeakerGain;
        internal int dwHeadsetHookSwitchMode;
        internal int dwHeadsetVolume;
        internal int dwHeadsetGain;
        internal int dwDisplaySize;
        internal int dwDisplayOffset;
        internal int dwLampModesSize;
        internal int dwLampModesOffset;
        internal int dwOwnerNameSize;
        internal int dwOwnerNameOffset;
        internal int dwDevSpecificSize;
        internal int dwDevSpecificOffset;
        internal int dwPhoneFeatures;                                // TAPI v2.0
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class VARSTRING
    {
        internal int dwTotalSize;
        internal int dwNeededSize;
        internal int dwUsedSize;
        internal int dwStringFormat;
        internal int dwStringSize;
        internal int dwStringOffset;
    }
}
