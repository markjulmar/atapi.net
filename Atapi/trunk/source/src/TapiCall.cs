// TapiCall.cs
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
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using JulMar.Atapi.Interop;
using System.Globalization;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class holds the Call Features for a given <see cref="TapiCall"/> or <see cref="TapiAddress"/>.
    /// </summary>
    public sealed class CallFeatureSet
    {
        private readonly int _features1;

        internal CallFeatureSet(int features)
        {
            _features1 = features;
        }

        /// <summary>
        /// Accept the call
        /// </summary>
        public bool CanAccept
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_ACCEPT) > 0; }
        }

        /// <summary>
        /// Add the call to the conference
        /// </summary>
        public bool CanAddToConference
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_ADDTOCONF) > 0; }
        }

        /// <summary>
        /// Answer the call
        /// </summary>
        public bool CanAnswer
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_ANSWER) > 0; }
        }

        /// <summary>
        /// Blind transfer the call
        /// </summary>
        public bool CanBlindTransfer
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_BLINDTRANSFER) > 0; }
        }

        /// <summary>
        /// Complete the call
        /// </summary>
        public bool CanCompleteCall
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_COMPLETECALL) > 0; }
        }

        /// <summary>
        /// Complete a 2-step transfer of the call
        /// </summary>
        public bool CanCompleteTransfer
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_COMPLETETRANSF) > 0; }
        }

        /// <summary>
        /// Dial additional digits on the call
        /// </summary>
        public bool CanDial
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_DIAL) > 0; }
        }

        /// <summary>
        /// Drop the call
        /// </summary>
        public bool CanDrop
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_DROP) > 0; }
        }

        /// <summary>
        /// Gather DTMF digits on the call.
        /// </summary>
        public bool CanGatherDigits
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_GATHERDIGITS) > 0; }
        }

        /// <summary>
        /// Generate DTMF digits
        /// </summary>
        public bool CanGenerateDigits
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_GENERATEDIGITS) > 0; }
        }

        /// <summary>
        /// Generate tones
        /// </summary>
        public bool CanGenerateTone
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_GENERATETONE) > 0; }
        }

        /// <summary>
        /// Place the call on hold
        /// </summary>
        public bool CanHold
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_HOLD) > 0; }
        }

        /// <summary>
        /// Monitor digits on the call
        /// </summary>
        public bool CanMonitorDigits
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_MONITORDIGITS) > 0; }
        }

        /// <summary>
        /// Monitor media changes on the call
        /// </summary>
        public bool CanMonitorMedia
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_MONITORMEDIA) > 0; }
        }

        /// <summary>
        /// Monitor tones on the call
        /// </summary>
        public bool CanMonitorTones
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_MONITORTONES) > 0; }
        }

        /// <summary>
        /// Park the call
        /// </summary>
        public bool CanPark
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_PARK) > 0; }
        }

        /// <summary>
        /// Prepare to add the call to a conference
        /// </summary>
        public bool CanPrepareAddToConference
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_PREPAREADDCONF) > 0; }
        }

        /// <summary>
        /// Redirect the call
        /// </summary>
        public bool CanRedirect
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_REDIRECT) > 0; }
        }

        /// <summary>
        /// Remove the call from a conference
        /// </summary>
        public bool CanRemoveFromConference
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_REMOVEFROMCONF) > 0; }
        }

        /// <summary>
        /// Secure the call from outside interference
        /// </summary>
        public bool CanSecureCall
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SECURECALL) > 0; }
        }

        /// <summary>
        /// Send OOB UUI to the peer.
        /// </summary>
        public bool CanSendUserUserInfo
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SENDUSERUSER) > 0; }
        }

        /// <summary>
        /// Set call parameters on the call
        /// </summary>
        public bool CanSetCallParams
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETCALLPARAMS) > 0; }
        }

        /// <summary>
        /// Change media control for the call
        /// </summary>
        public bool CanSetMediaControl
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETMEDIACONTROL) > 0; }
        }

        /// <summary>
        /// Set/Change terminal information
        /// </summary>
        public bool CanSetTerminal
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETTERMINAL) > 0; }
        }

        /// <summary>
        /// Setup a conference for the call
        /// </summary>
        public bool CanSetupConference
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETUPCONF) > 0; }
        }

        /// <summary>
        /// Initiate a 2-step transfer
        /// </summary>
        public bool CanSetupTransfer
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETUPTRANSFER) > 0; }
        }

        /// <summary>
        /// Swap the call with a held/active call
        /// </summary>
        public bool CanSwapHold
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SWAPHOLD) > 0; }
        }

        /// <summary>
        /// Bring the call off hold
        /// </summary>
        public bool CanUnhold
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_UNHOLD) > 0; }
        }

        /// <summary>
        /// Release OOB UUI data associated with the call
        /// </summary>
        public bool CanReleaseUserUserInfo
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_RELEASEUSERUSERINFO) > 0; }
        }

        /// <summary>
        /// Change the call treatment
        /// </summary>
        public bool CanSetTreatment
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETTREATMENT) > 0; }
        }

        /// <summary>
        /// Set the Quality of Service associated with the call
        /// </summary>
        public bool CanSetQos
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETQOS) > 0; }
        }

        /// <summary>
        /// Set call data to travel with the call appearance
        /// </summary>
        public bool CanSetCallData
        {
            get { return (_features1 & NativeMethods.LINECALLFEATURE_SETCALLDATA) > 0; }
        }

        /// <summary>
        /// Returns a System.String representing this call object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (PropertyInfo pi in GetType().GetProperties())
            {
                object value;
                try { value = pi.GetValue(this, null); }
                catch { value = null; }
                sb.AppendFormat(CultureInfo.CurrentUICulture, "{0}: {1}\n", pi.Name, value);
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// This class represents a single 1st party call in Tapi.
    /// </summary>
    public class TapiCall : IDisposable
    {
        #region Class Data
        private readonly HTCALL _hCall;
        private readonly LINECALLINFO _lci = new LINECALLINFO();
        private byte[] _lciData;
        private readonly TapiAddress _addrOwner;
        private CallState _callState = CallState.Unknown;
        private DateTime _csTime;
        private Privilege _csPrivilege = Privilege.None;
        private CallFeatureSet _features = new CallFeatureSet(0);
        private readonly object _lock = new object();
        private StringBuilder _sbDigits;
        private int _gdreqId;
        private int _gtdreqId;
        private EventHandler<DigitDetectedEventArgs> _digitDetectedCb;
        private bool _mediaDetection = true;
        private MonitorTone[] _monitorTones;
        private EventHandler<ToneDetectedEventArgs> _toneDetectedCb;
        static private readonly Dictionary<IntPtr, TapiCall> CallsMap = new Dictionary<IntPtr, TapiCall>();
        #endregion

        internal TapiCall(TapiLine lineOwner, IntPtr hCall)
        {
            _hCall = new HTCALL(hCall, true);
            lock (CallsMap)
            {
                CallsMap.Add(hCall, this);
            }

            GatherCallInfo();
            GatherCallStatus();

            int addrId = _lci.dwAddressID;
            _addrOwner = lineOwner.Addresses[addrId];
        }

        internal TapiCall(TapiAddress addrOwner, IntPtr hCall)
		{
            _hCall = new HTCALL(hCall, true);
            _addrOwner = addrOwner;

            lock (CallsMap)
            {
                CallsMap.Add(hCall, this);
            }

            GatherCallInfo();
            GatherCallStatus();

            System.Diagnostics.Debug.Assert(addrOwner.Id == _lci.dwAddressID);
		}

        internal HTCALL Handle
        {
            get { return _hCall; }
        }

        /// <summary>
        /// This returns the underlying HTCALL which you can use in your
        /// own interop scenarios to deal with custom methods or places
        /// which are not wrapped by ATAPI
        /// </summary>
        public SafeHandle CallHandle
        {
            get { return _hCall; }
        }

        private TapiManager TapiManager
        {
            get { return Line.TapiManager; }
        }

        internal static TapiCall FindCallByHandle(IntPtr htCall)
        {
            lock (CallsMap)
            {
                if (CallsMap.ContainsKey(htCall))
                {
                    return CallsMap[htCall];
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the <see cref="TapiAddress"/> associated with the call.
        /// </summary>
        public TapiAddress Address
        {
            get { return _addrOwner; }
        }

        /// <summary>
        /// Returns the <see cref="TapiLine"/> associated with the call.
        /// </summary>
        public TapiLine Line
        {
            get { return _addrOwner.Line; }
        }

        /// <summary>
        /// Returns the current call state for the call.
        /// </summary>
        public CallState CallState
        {
            get { return _callState; }
        }

        /// <summary>
        /// Returns the call origin information
        /// </summary>
        public CallOrigins CallOrigin
        {
            get { return (CallOrigins)_lci.dwOrigin; }
        }

        /// <summary>
        /// Returns the call reason information
        /// </summary>
        public CallReasons CallReason
        {
            get { return (CallReasons)_lci.dwReason; }
        }

        /// <summary>
        /// Returns the time/date that the last status change occurred.
        /// </summary>
        public DateTime LastEventTime
        {
            get { return _csTime; }
        }

        /// <summary>
        /// Gets and sets the CallData associated with the call. Depending on the service provider implementation, the CallData member can be propagated to all 
        /// applications having handles to the call, including those on other machines (through the server), and can travel with the call when it is transferred.
        /// </summary>
        public byte[] CallData
        {
            get
            {
                if (_lci.dwCallDataSize == 0 || _lci.dwCallDataOffset == 0)
                    return null;
                var data = new byte[_lci.dwCallDataSize];
                Array.Copy(_lciData, _lci.dwCallDataOffset, data, 0, _lci.dwCallDataSize);
                return data;
            }

            set
            {
                int len = (value != null) ? value.Length : 0;
                if (len > Address.Capabilities.MaxCallDataSize)
                    len = Address.Capabilities.MaxCallDataSize;

                NativeMethods.lineSetCallData(Handle, value, len);
            }
        }

        /// <summary>
        /// In some telephony environments, the switch, or service provider can assign a unique identifier to each call. 
        /// This enables the call to be tracked across transfers, forwards, or other events. The domain of these call identifiers 
        /// and their scope is service provider-defined. The dwCallID member makes this unique identifier available to the applications. 
        /// </summary>
        public int Id
        {
            get { return _lci.dwCallID; }
        }

        /// <summary>
        /// Returns any related numeric call id
        /// </summary>
        public int RelatedId
        {
            get { return _lci.dwRelatedCallID; }
        }

        /// <summary>
        /// Number of the trunk over which the call is routed. This will be 0xffffffff if unknown.
        /// </summary>
        public int TrunkId
        {
            get { return _lci.dwTrunk; }
        }

        /// <summary>
        /// Rate of the call data stream, in bits per second (bps).
        /// </summary>
        public int DataRate
        {
            get { return _lci.dwRate; }
            set { SetCallParams("DataRate", BearerMode, value, Line.Capabilities.MaxDataRate, _lci.DialParams); }
        }

        /// <summary>
        /// Value that specifies the current bearer mode of the call.
        /// </summary>
        public BearerModes BearerMode
        {
            get { return (BearerModes)_lci.dwBearerMode; }
            set { SetCallParams("BearerMode", value, 0, Line.Capabilities.MaxDataRate, _lci.DialParams); }
        }

        /// <summary>
        /// Value that specifies the media mode of the data stream currently on the call. This is the media mode determined by the owner of the call.
        /// </summary>
        public MediaModes MediaMode
        {
            get { return (MediaModes)_lci.dwMediaMode; }
        }

        /// <summary>
        /// Enables an application to get/set the application-specific field of the specified call's call-information record
        /// </summary>
        public int AppSpecificData
        {
            get { return _lci.dwAppSpecific; }
            set { NativeMethods.lineSetAppSpecific(Handle, value); }
        }

        /// <summary>
        /// This retrieves the user-user information which can be passed along the wire on some telephony systems.
        /// It returns an empty string if no UUI exists for this call.  You can use <see cref="TapiCall.SendUserUserInfo"/> to send data.
        /// </summary>
        public string UserUserInfo
        {
            get
            {
                if (_lci.dwUserUserInfoOffset == 0 || _lci.dwUserUserInfoSize == 0)
                    return string.Empty;
                return NativeMethods.GetString(_lciData, _lci.dwUserUserInfoOffset, _lci.dwUserUserInfoSize, NativeMethods.STRINGFORMAT_ASCII);
            }
        }

        /// <summary>
        /// Returns the <see cref="CallFeatureSet"/> representing the available features for the call.
        /// </summary>
        public CallFeatureSet Features
        {
            get { return _features; }
        }

        /// <summary>
        /// Retrieve or set the application's privilege to this call
        /// </summary>
        public Privilege Privilege
        {
            get { return _csPrivilege; }
            set
            {
                NativeMethods.lineSetCallPrivilege(Handle,
                    (value == Privilege.Monitor) ? NativeMethods.LINECALLPRIVILEGE_MONITOR :
                    (value == Privilege.Owner) ? NativeMethods.LINECALLPRIVILEGE_OWNER : NativeMethods.LINECALLPRIVILEGE_NONE);
            }
        }

        /// <summary>
        /// Returns the caller ID number
        /// </summary>
        public string CallerId
        {
            get 
            {
                return ((_lci.dwCallerIDFlags & NativeMethods.LINECALLPARTYID_ADDRESS) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwCallerIDOffset, _lci.dwCallerIDSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the caller ID name or empty string
        /// </summary>
        public string CallerName
        {
            get
            {
                return ((_lci.dwCallerIDFlags & NativeMethods.LINECALLPARTYID_NAME) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwCallerIDNameOffset, _lci.dwCallerIDNameSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the called ID number
        /// </summary>
        public string CalledId
        {
            get
            {
                return ((_lci.dwCalledIDFlags & NativeMethods.LINECALLPARTYID_ADDRESS) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwCalledIDOffset, _lci.dwCalledIDSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the called ID name or empty string.
        /// </summary>
        public string CalledName
        {
            get
            {
                return ((_lci.dwCalledIDFlags & NativeMethods.LINECALLPARTYID_NAME) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwCalledIDNameOffset, _lci.dwCalledIDNameSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the connected ID number.
        /// </summary>
        public string ConnectedId
        {
            get
            {
                return ((_lci.dwConnectedIDFlags & NativeMethods.LINECALLPARTYID_ADDRESS) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwConnectedIDOffset, _lci.dwConnectedIDSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the connected ID name or empty string.
        /// </summary>
        public string ConnectedName
        {
            get
            {
                return ((_lci.dwConnectedIDFlags & NativeMethods.LINECALLPARTYID_NAME) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwConnectedIDNameOffset, _lci.dwConnectedIDNameSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the redirecting ID number.
        /// </summary>
        public string RedirectingId
        {
            get
            {
                return ((_lci.dwRedirectingIDFlags & NativeMethods.LINECALLPARTYID_ADDRESS) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwRedirectingIDOffset, _lci.dwRedirectingIDSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the redirecting ID name or empty string.
        /// </summary>
        public string RedirectingName
        {
            get
            {
                return ((_lci.dwRedirectingIDFlags & NativeMethods.LINECALLPARTYID_NAME) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwRedirectingIDNameOffset, _lci.dwRedirectingIDNameSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the redirection ID number
        /// </summary>
        public string RedirectionId
        {
            get
            {
                return ((_lci.dwRedirectionIDFlags & NativeMethods.LINECALLPARTYID_ADDRESS) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwRedirectionIDOffset, _lci.dwRedirectionIDSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// Returns the redirection ID name or empty string.
        /// </summary>
        public string RedirectionName
        {
            get
            {
                return ((_lci.dwRedirectionIDFlags & NativeMethods.LINECALLPARTYID_NAME) > 0) ?
                    NativeMethods.GetString(_lciData, _lci.dwRedirectionIDNameOffset, _lci.dwRedirectionIDNameSize, Line.StringFormat) : string.Empty;
            }
        }

        /// <summary>
        /// This turns media detection on and off.  It is enabled by default, but can be turned off to avoid computational overhead when 
        /// media detection is not required.
        /// </summary>
        public bool MediaDetection
        {
            get
            {
                return _mediaDetection;
            }

            set
            {
                _mediaDetection = value;
                NativeMethods.lineMonitorMedia(Handle, (value == false) ? 0 : (int)Line.Capabilities.MediaModes);
            }
        }

        /// <summary>
        /// Duration of a comma in the dialable address, in milliseconds. 
        /// </summary>
        public int DialPause
        {
            get { return _lci.DialParams.dwDialPause; }
            set
            {
                var ldp = new LINEDIALPARAMS
                  {
                      dwDialPause = value,
                      dwDialSpeed = DialSpeed,
                      dwDigitDuration = DigitDuration,
                      dwWaitForDialtone = WaitForDialtoneDuration
                  };
                SetCallParams("DialPause", BearerMode, DataRate, Line.Capabilities.MaxDataRate, ldp);
            }
        }

        /// <summary>
        /// Interdigit time period between successive digits, in milliseconds.
        /// </summary>
        public int DialSpeed
        {
            get { return _lci.DialParams.dwDialSpeed; }
            set
            {
                var ldp = new LINEDIALPARAMS
                  {
                      dwDialPause = DialPause,
                      dwDialSpeed = value,
                      dwDigitDuration = DigitDuration,
                      dwWaitForDialtone = WaitForDialtoneDuration
                  };
                SetCallParams("DialSpeed", BearerMode, DataRate, Line.Capabilities.MaxDataRate, ldp);
            }
        }

        /// <summary>
        /// Duration of a digit, in milliseconds. 
        /// </summary>
        public int DigitDuration
        {
            get { return _lci.DialParams.dwDigitDuration; }
            set
            {
                var ldp = new LINEDIALPARAMS
                  {
                      dwDialPause = DialPause,
                      dwDialSpeed = DialSpeed,
                      dwDigitDuration = value,
                      dwWaitForDialtone = WaitForDialtoneDuration
                  };
                SetCallParams("DigitDuration", BearerMode, DataRate, Line.Capabilities.MaxDataRate, ldp);
            }
        }

        /// <summary>
        /// Maximum amount of time to wait for a dial tone when a 'W' is used in the dialable address, in milliseconds. 
        /// </summary>
        public int WaitForDialtoneDuration
        {
            get { return _lci.DialParams.dwWaitForDialtone; }
            set
            {
                var ldp = new LINEDIALPARAMS
                  {
                      dwDialPause = DialPause,
                      dwDialSpeed = DialSpeed,
                      dwDigitDuration = DigitDuration,
                      dwWaitForDialtone = value
                  };
                SetCallParams("WaitForDialtoneDuration", BearerMode, DataRate, Line.Capabilities.MaxDataRate, ldp);
            }
        }

        /// <summary>
        /// Internal method to set call parameters
        /// </summary>
        /// <param name="param">Name of the parameter</param>
        /// <param name="bmode"></param>
        /// <param name="minRate"></param>
        /// <param name="maxRate"></param>
        /// <param name="ldp"></param>
        private void SetCallParams(string param, BearerModes bmode, int minRate, int maxRate, LINEDIALPARAMS ldp)
        {
            int rc = NativeMethods.lineSetCallParams(Handle, (int)bmode, minRate, maxRate, ldp);
            if (rc > 0)
            {
                var req = new PendingTapiRequest(rc, null, null);
                TapiManager.AddAsyncRequest(req);
                req.AsyncWaitHandle.WaitOne();
            }

            if (rc == NativeMethods.LINEERR_INVALPARAM)
                throw new ArgumentException("Invalid parameter - " + param);
            if (rc == 0)
                GatherCallInfo();
        }

        /// <summary>
        /// This associates an arbitrary object with the call
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Returns the Device Specific data for this call
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get
            {
                var data = new byte[_lci.dwDevSpecificSize];
                Array.Copy(_lciData, _lci.dwDevSpecificOffset, data, 0, _lci.dwDevSpecificSize);
                return data;
            }
        }

        /// <summary>
        /// Returns a System.String represnting this Tapi line object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Call 0x{0:X} id:{1}", Handle.DangerousGetHandle().ToInt32(), Id);
        }

        #region lineAccept
        /// <summary>
        /// This function accepts the specified offered call. It can optionally send the specified user-user information to the calling party.
        /// </summary>
        /// <param name="uuInfo">Buffer containing user-user information to be sent to the remote party as part of the call accept or null if no user-user information is to be sent.</param>
        public void Accept(byte[] uuInfo)
        {
            IAsyncResult ar = BeginAccept(uuInfo, null, null);
            EndAccept(ar);
        }

        /// <summary>
        /// This function accepts the specified offered call. It can optionally send the specified user-user information to the calling party.
        /// </summary>
        public void Accept()
        {
            Accept(null);
        }

        /// <summary>
        /// This function accepts the specified offered call. It can optionally send the specified user-user information to the calling party.
        /// </summary>
        /// <param name="acb">AsyncCallback method</param>
        /// <param name="state">State data</param>
        public IAsyncResult BeginAccept(AsyncCallback acb, object state)
        {
            return BeginAccept(null, acb, state);
        }

        /// <summary>
        /// This function accepts the specified offered call. It can optionally send the specified user-user information to the calling party.
        /// </summary>
        /// <param name="uuInfo">Buffer containing user-user information to be sent to the remote party as part of the call accept or null if no user-user information is to be sent.</param>
        /// <param name="acb">AsyncCallback method</param>
        /// <param name="state">State data</param>
        public IAsyncResult BeginAccept(byte[] uuInfo, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineAccept(Handle, uuInfo, (uuInfo != null) ? uuInfo.Length : 0);
            if (rc < 0)
                throw new TapiException("lineAccept failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// This retrieves the results of a previously issued <seealso cref="TapiCall.BeginAccept(byte[],AsyncCallback,object)"/> method.
        /// </summary>
        /// <param name="ar">IAsyncResult from the BeginAccept call.</param>
        public void EndAccept(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineAccept failed", req.Result);
        }
        #endregion

        #region lineAddToConference
        /// <summary>
        /// This adds the call to the existing conference.  The current call must be a
        /// conference call and in the proper call state.
        /// </summary>
        /// <param name="callToAdd">Call to add to this conference</param>
        public void AddToConference(TapiCall callToAdd)
        {
            IAsyncResult ar = BeginAddToConference(callToAdd, null, null);
            EndAddToConference(ar);
        }

        /// <summary>
        /// This adds the call to the existing conference.  The current call must be a
        /// conference call and in the proper call state.
        /// </summary>
        /// <param name="callToAdd">Call to add to this conference</param>
        /// <param name="acb">Callback function</param>
        /// <param name="state">State parameter</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginAddToConference(TapiCall callToAdd, AsyncCallback acb, object state)
        {
            if (callToAdd == null)
                throw new ArgumentNullException("callToAdd");

            int rc = NativeMethods.lineAddToConference(Handle, callToAdd.Handle);
            if (rc < 0)
                throw new TapiException("lineAddToConference failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// This ends an asynchronous AddToConference.
        /// </summary>
        /// <param name="ar">Pending IAsyncResult request</param>
        public void EndAddToConference(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineAddToConference failed", req.Result);
        }
        #endregion

        #region lineAnswer
        /// <summary>
        /// This function answers the specified offering call. 
        /// </summary>
        /// <param name="uuInfo">Buffer containing user-user information to be sent to the remote party as part of the call answer or null if no user-user information is to be sent.</param>
        public void Answer(byte[] uuInfo)
        {
            IAsyncResult ar = BeginAnswer(uuInfo, null, null);
            EndAnswer(ar);
        }

        /// <summary>
        /// This function answers the specified offering call. 
        /// </summary>
        public void Answer()
        {
            Answer(null);
        }

        /// <summary>
        /// This function answers the specified offering call. 
        /// </summary>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginAnswer(AsyncCallback acb, object state)
        {
            return BeginAnswer(null, acb, state);
        }

        /// <summary>
        /// This function answers the specified offering call. 
        /// </summary>
        /// <param name="uuInfo">Buffer containing user-user information to be sent to the remote party as part of the call answer or null if no user-user information is to be sent.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginAnswer(byte[] uuInfo, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineAnswer(Handle, uuInfo, (uuInfo != null) ? uuInfo.Length : 0);
            if (rc < 0)
                throw new TapiException("lineAnswer failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Harvests the results from a previously issued <seealso cref="TapiCall.BeginAnswer(AsyncCallback, object)"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginAnswer</param>
        public void EndAnswer(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineAnswer failed", req.Result);
        }
        #endregion

        #region lineBlindTransfer
        /// <summary>
        /// Performs a single-step (blind) transfer of the call to a target number.
        /// </summary>
        /// <param name="destAddress">Destinationa address</param>
        /// <param name="countryCode">Country code or zero for default.</param>
        public void BlindTransfer(string destAddress, int countryCode)
        {
            IAsyncResult ar = BeginBlindTransfer(destAddress, countryCode, null, null);
            EndBlindTransfer(ar);
        }

        /// <summary>
        /// Performs a single-step (blind) transfer of the call to a target number.
        /// </summary>
        /// <param name="destAddress">Destinationa address</param>
        /// <param name="countryCode">Country code or zero for default.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginBlindTransfer(string destAddress, int countryCode, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineBlindTransfer(Handle, destAddress, countryCode);
            if (rc < 0)
                throw new TapiException("lineBlindTransfer failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiCall.BeginBlindTransfer"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginBlindTransfer</param>
        public void EndBlindTransfer(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineBlindTransfer failed", req.Result);
        }
        #endregion

        #region lineCompleteCall
        /// <summary>
        /// Specifies how a call that could not be connected normally should be completed instead. The network or switch may not be able to 
        /// complete a call because network resources are busy or the remote station is busy or doesn't answer. The application can request that 
        /// the call be completed in one of a number of ways.
        /// </summary>
        /// <param name="mode">Completion mode</param>
        /// <param name="messageIndex">Message Id if sending message - this is the index of the <see cref="AddressCapabilities.AvailableCallCompletionMessages"/> entry.</param>
        public int CompleteCall(CallCompletionMode mode, int messageIndex)
        {
            IAsyncResult ar = BeginCompleteCall(mode, messageIndex, null, null);
            return EndCompleteCall(ar);
        }

        /// <summary>
        /// Specifies how a call that could not be connected normally should be completed instead. The network or switch may not be able to 
        /// complete a call because network resources are busy or the remote station is busy or doesn't answer. The application can request that 
        /// the call be completed in one of a number of ways.
        /// </summary>
        /// <param name="mode">Completion mode</param>
        /// <param name="messageId">Message Id if sending message</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginCompleteCall(CallCompletionMode mode, int messageId, AsyncCallback acb, object state)
        {
            IntPtr complId = Marshal.AllocHGlobal(4);
            int rc = NativeMethods.lineCompleteCall(Handle, complId, (int)mode, messageId);
            if (rc < 0)
                throw new TapiException("lineCompleteCall failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state, complId, 4));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiCall.BeginCompleteCall"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginCompleteCall</param>
        /// <returns>Completion Id</returns>
        public int EndCompleteCall(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;

            var buff = new byte[4];
            Marshal.Copy(req.ApiData, buff, 0, 4);
            Marshal.FreeHGlobal(req.ApiData);

            if (req.Result < 0)
                throw new TapiException("lineCompleteCall failed", req.Result);

            return BitConverter.ToInt32(buff, 0);
        }
        #endregion

        #region lineDial
        /// <summary>
        /// Dials digits on the call
        /// </summary>
        /// <param name="destAddress">Destination address</param>
        /// <param name="countryCode">Country code or zero for default.</param>
        public void Dial(string destAddress, int countryCode)
        {
            IAsyncResult ar = BeginDial(destAddress, countryCode, null, null);
            EndDial(ar);
        }

        /// <summary>
        /// Dials digits on the call
        /// </summary>
        /// <param name="destAddress">Destination address</param>
        /// <param name="countryCode">Country code or zero for default.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginDial(string destAddress, int countryCode, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineDial(Handle, destAddress, countryCode);
            if (rc < 0)
                throw new TapiException("lineDial failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiCall.BeginDial"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginDial</param>
        public void EndDial(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineDial failed", req.Result);
        }
        #endregion

        #region lineDevSpecific
        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="inData">Input data</param>
        /// <returns>Output data</returns>
        public byte[] DeviceSpecific(byte[] inData)
        {
            IAsyncResult ar = BeginDeviceSpecific(inData, null, null);
            return EndDeviceSpecific(ar);
        }

        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="inData">Input data</param>
        /// <param name="acb">Callback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginDeviceSpecific(byte[] inData, AsyncCallback acb, object state)
        {
            if (inData == null)
                throw new ArgumentNullException("inData");

            IntPtr ip = Marshal.AllocHGlobal(inData.Length);
            Marshal.Copy(inData, 0, ip, inData.Length);

            int rc = NativeMethods.lineDevSpecific(Line.Handle, Address.Id, Handle.DangerousGetHandle(), ip, inData.Length);
            if (rc < 0)
            {
                Marshal.FreeHGlobal(ip);
                throw new TapiException("lineDevSpecific failed", rc);
            }

            return Line.TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state, ip, inData.Length));
        }

        /// <summary>
        /// This method harvests the results from a <see cref="TapiAddress.BeginDeviceSpecific(byte[], AsyncCallback, object)"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginDeviceSpecific</param>
        /// <returns>Output data</returns>
        public byte[] EndDeviceSpecific(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            IntPtr ip = req.ApiData;

            if (req.Result < 0)
            {
                Marshal.FreeHGlobal(ip);
                throw new TapiException("lineDevSpecific failed", req.Result);
            }

            var outData = new byte[req.ApiDataSize];
            Marshal.Copy(ip, outData, 0, req.ApiDataSize);
            Marshal.FreeHGlobal(ip);

            return outData;
        }
        #endregion

        #region lineDrop
        /// <summary>
        /// Drops the active call
        /// </summary>
        /// <param name="uuInfo">Buffer containing user-user information to be sent to the remote party as part of the call drop or null if no user-user information is to be sent.</param>
        public void Drop(byte[] uuInfo)
        {
            IAsyncResult ar = BeginDrop(uuInfo, null, null);
            EndDrop(ar);
        }

        /// <summary>
        /// Drops the active call
        /// </summary>
        public void Drop()
        {
            Drop(null);
        }

        /// <summary>
        /// Drops the active call
        /// </summary>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginDrop(AsyncCallback acb, object state)
        {
            return BeginDrop(null, acb, state);
        }

        /// <summary>
        /// Drops the active call
        /// </summary>
        /// <param name="uuInfo">Buffer containing user-user information to be sent to the remote party as part of the call drop or null if no user-user information is to be sent.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginDrop(byte[] uuInfo, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineDrop(Handle, uuInfo, (uuInfo != null) ? uuInfo.Length : 0);
            if (rc < 0)
                throw new TapiException("lineDrop failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiCall.BeginDrop(AsyncCallback, object)"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult</param>
        public void EndDrop(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineDrop failed", req.Result);
        }
        #endregion

        #region lineGatherDigits
        /// <summary>
        /// Gathers digits from the call until an end condition is met.
        /// </summary>
        /// <param name="digitModes">Digit mode(s) to be monitored</param>
        /// <param name="maxDigits">Number of digits to be collected</param>
        /// <param name="termDigits">List of termination digits. If one of the digits in the string is detected, that termination digit is appended to the buffer and digit collection is terminated</param>
        /// <param name="firstDigitTimeout">Time duration in milliseconds in which the first digit is expected</param>
        /// <param name="interDigitTimeout">Maximum time duration in milliseconds between consecutive digits.</param>
        /// <param name="digits">Buffer to return digits</param>
        /// <returns>Completion reason</returns>
        public DigitGatherComplete GatherDigits(DigitModes digitModes, int maxDigits, string termDigits, int firstDigitTimeout, int interDigitTimeout, out string digits)
        {
            IAsyncResult ar = BeginGatherDigits(digitModes, maxDigits, termDigits, firstDigitTimeout, interDigitTimeout, null, null);
            return EndGatherDigits(ar, out digits);
        }

        /// <summary>
        /// Gathers digits from the call until an end condition is met.
        /// </summary>
        /// <param name="digitModes">Digit mode(s) to be monitored</param>
        /// <param name="maxDigits">Number of digits to be collected</param>
        /// <param name="termDigits">List of termination digits. If one of the digits in the string is detected, that termination digit is appended to the buffer and digit collection is terminated</param>
        /// <param name="firstDigitTimeout">Time duration in milliseconds in which the first digit is expected</param>
        /// <param name="interDigitTimeout">Maximum time duration in milliseconds between consecutive digits.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">Object state</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginGatherDigits(DigitModes digitModes, int maxDigits, string termDigits, int firstDigitTimeout, int interDigitTimeout, AsyncCallback acb, object state)
        {
            lock (_lock)
            {
                if (_sbDigits != null)
                    throw new TapiException("Already gathering digits on this call", NativeMethods.LINEERR_OPERATIONUNAVAIL);
                _sbDigits = new StringBuilder(maxDigits);
            }

            int rc = NativeMethods.lineGatherDigits(Handle, (int)digitModes, out _sbDigits, maxDigits, termDigits, firstDigitTimeout, interDigitTimeout);
            if (rc < 0)
                throw new TapiException("lineGatherDigits failed", rc);

            _gdreqId = TapiManager.GetAsyncRequestid();
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(_gdreqId, acb, state));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiCall.BeginGatherDigits"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginGatherDigits</param>
        /// <param name="digits">Buffer to return digits</param>
        /// <returns>Completion reason</returns>
        public DigitGatherComplete EndGatherDigits(IAsyncResult ar, out string digits)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            digits = _sbDigits.ToString();
            _sbDigits = null;
            _gdreqId = 0;
            return (DigitGatherComplete) req.Result;
        }

        internal void OnGatherDigitsComplete(int reason)
        {
            System.Diagnostics.Debug.Assert(_gdreqId != 0 && _sbDigits != null);
            if (_gdreqId != 0)
                TapiManager.HandleCompletion(_gdreqId, reason);
        }

        /// <summary>
        /// This method cancels a previously request digit gather.  The <see cref="TapiCall.EndGatherDigits"/> may then be called.
        /// </summary>
        public void CancelGatherDigits()
        {
            lock (_lock)
            {
                if (_sbDigits == null)
                    return;
            }
            NativeMethods.lineCancelGatherDigits(Handle, 0, null, 0, null, 0, 0);
        }
        #endregion

        #region lineGenerateDigits
        /// <summary>
        /// This method generates DTMF digits on the call.
        /// </summary>
        /// <param name="digitMode">Format to be used for signaling these digits.</param>
        /// <param name="digits">Buffer of digits to be generated.</param>
        /// <param name="duration">Both the duration in milliseconds of DTMF digits and pulse and DTMF inter-digit spacing. A value of 0 uses a default value.</param>
        /// <returns>true/false</returns>
        public bool GenerateDigits(DigitModes digitMode, string digits, int duration)
        {
            IAsyncResult ar = BeginGenerateDigits(digitMode, digits, duration, null, null);
            return EndGenerateDigits(ar);
        }

        /// <summary>
        /// This method generates DTMF digits on the call.
        /// </summary>
        /// <param name="digitMode">Format to be used for signaling these digits.</param>
        /// <param name="digits">Buffer of digits to be generated.</param>
        /// <param name="duration">Both the duration in milliseconds of DTMF digits and pulse and DTMF inter-digit spacing. A value of 0 uses a default value.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">Object state</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginGenerateDigits(DigitModes digitMode, string digits, int duration, AsyncCallback acb, object state)
        {
            lock (_lock)
            {
                if (_gtdreqId != 0)
                    throw new TapiException("Digit or Tone generation already in progress.", NativeMethods.LINEERR_OPERATIONUNAVAIL);
                _gtdreqId = TapiManager.GetAsyncRequestid();
            }

            int rc = NativeMethods.lineGenerateDigits(Handle, (int)digitMode, digits, duration);
            if (rc < 0)
            {
                _gtdreqId = 0;
                throw new TapiException("lineGenerateDigits failed", rc);
            }
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(_gtdreqId, acb, state));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiCall.BeginGenerateDigits"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginGenerateDigits</param>
        /// <returns>true/false success</returns>
        public bool EndGenerateDigits(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            return (req.Result == 1);
        }

        /// <summary>
        /// This method cancels a digit generation request.
        /// </summary>
        public void CancelGenerateDigits()
        {
            lock (_lock)
            {
                if (_gtdreqId != 0)
                    return;
            }
            NativeMethods.lineGenerateDigits(Handle, 0, null, 0);
        }
        #endregion

        #region lineGenerateTone
        /// <summary>
        /// This method generates the specified inband tone over the specified call.
        /// </summary>
        /// <param name="toneMode">Format to be used for tone.</param>
        /// <param name="duration">Duration in milliseconds during which the tone should be sustained. A value of 0 for dwDuration uses a default duration for the specified tone.</param>
        /// <returns>true/false</returns>
        public bool GenerateTone(ToneModes toneMode, int duration)
        {
            IAsyncResult ar = BeginGenerateTone(toneMode, duration, null, null);
            return EndGenerateTone(ar);
        }

        /// <summary>
        /// This method generates the specified custom tone inband over the specified call.
        /// </summary>
        /// <param name="tones">Array of custom tones to generate</param>
        /// <param name="duration">Duration of tone generation</param>
        /// <returns>True/False</returns>
        public bool GenerateTone(CustomTone[] tones, int duration)
        {
            IAsyncResult ar = BeginGenerateTone(tones, duration, null, null);
            return EndGenerateTone(ar);
        }

        /// <summary>
        /// This method generates the specified inband tone over the specified call.
        /// </summary>
        /// <param name="toneMode">Format to be used for tone.</param>
        /// <param name="duration">Duration in milliseconds during which the tone should be sustained. A value of 0 for dwDuration uses a default duration for the specified tone.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">Object state</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginGenerateTone(ToneModes toneMode, int duration, AsyncCallback acb, object state)
        {
            if (((int)toneMode & NativeMethods.LINETONEMODE_CUSTOM) > 0)
                throw new ArgumentException("Invalid toneMode supplied.");

            lock (_lock)
            {
                if (_gtdreqId != 0)
                    throw new TapiException("Digit or Tone generation already in progress.", NativeMethods.LINEERR_OPERATIONUNAVAIL);
                _gtdreqId = TapiManager.GetAsyncRequestid();
            }

            int rc = NativeMethods.lineGenerateTone(Handle, (int)toneMode, duration, 0, null);
            if (rc < 0)
            {
                _gtdreqId = 0;
                throw new TapiException("lineGenerateTone failed", rc);
            }
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(_gtdreqId, acb, state));
        }

        /// <summary>
        /// This method generates the specified inband tone over the specified call.
        /// </summary>
        /// <param name="tones">Array of custom tones to generate</param>
        /// <param name="duration">Length of tone generation</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">Object state</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginGenerateTone(CustomTone[] tones, int duration, AsyncCallback acb, object state)
        {
            if (tones == null)
                throw new ArgumentNullException("tones");

            lock (_lock)
            {
                if (_gtdreqId != 0)
                    throw new TapiException("Digit or Tone generation already in progress.", NativeMethods.LINEERR_OPERATIONUNAVAIL);
                _gtdreqId = TapiManager.GetAsyncRequestid();
            }

            var intTone = new LINEGENERATETONE[tones.Length];
            for (int i = 0; i < tones.Length; i++)
            {
                intTone[i] = new LINEGENERATETONE
                 {
                     dwFrequency = tones[i].Frequency,
                     dwCadenceOff = tones[i].CadenceOff,
                     dwCadenceOn = tones[i].CadenceOn,
                     dwVolume = tones[i].Volume
                 };
            }

            int rc = NativeMethods.lineGenerateTone(Handle, NativeMethods.LINETONEMODE_CUSTOM, duration, tones.Length, intTone);
            if (rc < 0)
            {
                _gtdreqId = 0;
                if (rc == NativeMethods.LINEERR_INVALPOINTER)
                    throw new ArgumentException("Supplied CustomTone array has too many tones.");
                throw new TapiException("lineGenerateTone failed", rc);
            }
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(_gtdreqId, acb, state));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiCall.BeginGenerateTone(ToneModes, int, AsyncCallback, object)"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginGenerateTone</param>
        /// <returns>true/false success</returns>
        public bool EndGenerateTone(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            return (req.Result == 1);
        }

        /// <summary>
        /// This method cancels a tone generation request.
        /// </summary>
        public void CancelGenerateTone()
        {
            lock (_lock)
            {
                if (_gtdreqId != 0)
                    return;
            }
            NativeMethods.lineGenerateTone(Handle, 0, 0, 0, null);
        }
        #endregion

        #region lineGetConfRelatedCalls
        /// <summary>
        /// This returns the list of related conference call parties that are part of the same conference call as this call. 
        /// The specified call is either a conference call or a participant call in a conference call. 
        /// New handles are generated for those calls for which the application does not already have handles, 
        /// and the application is granted monitor privilege to those calls. 
        /// </summary>
        /// <returns></returns>
        public TapiCall[] GetRelatedConferenceCalls()
        {
            var callList = new List<TapiCall>();
            var lcl = new LINECALLLIST();
            int rc, neededSize = 1024;
            do
            {
                lcl.dwTotalSize = neededSize;
                IntPtr pLcl = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lcl, pLcl, true);

                // Get any existing calls on the address..
                rc = NativeMethods.lineGetConfRelatedCalls(Handle, pLcl);
                Marshal.PtrToStructure(pLcl, lcl);

                if (lcl.dwNeededSize > neededSize)
                {
                    neededSize = lcl.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    var rawBuffer = new byte[lcl.dwUsedSize];
                    Marshal.Copy(pLcl, rawBuffer, 0, lcl.dwUsedSize);
                    for (int i = 0; i < lcl.dwCallsNumEntries; i++)
                    {
                        IntPtr hCall;
                        if (IntPtr.Size == 4)
                            hCall = (IntPtr)BitConverter.ToInt32(rawBuffer, lcl.dwCallsOffset + (i * IntPtr.Size));
                        else
                            hCall = (IntPtr)BitConverter.ToInt64(rawBuffer, lcl.dwCallsOffset + (i * IntPtr.Size));

                        TapiCall call = FindCallByHandle(hCall) ?? new TapiCall(Line, hCall);
                        callList.Add(call);
                    }
                }
                Marshal.FreeHGlobal(pLcl);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);

            if (rc < 0)
                throw new TapiException("lineGetConfRelatedCalls failed", rc);

            return callList.ToArray();
        }
        #endregion

        #region lineGetID
        /// <summary>
        /// This returns a device identifier for the specified device class associated with the call
        /// </summary>
        /// <param name="deviceClass">Device Class</param>
        /// <returns>string or byte[]</returns>
        public object GetExternalDeviceInfo(string deviceClass)
        {
            if (Line.Handle.IsInvalid)
                throw new InvalidOperationException("Line is not open");
            if (Handle.IsInvalid)
                throw new InvalidOperationException("Call is not active");

            var vs = new VARSTRING();
            object retValue = null;
            int rc, neededSize = Marshal.SizeOf(vs) + 100;
            do
            {
                vs.dwTotalSize = neededSize;
                IntPtr lpVs = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(vs, lpVs, true);
                rc = NativeMethods.lineGetID(Line.Handle, Address.Id, Handle, NativeMethods.LINECALLSELECT_CALL, lpVs, deviceClass);
                Marshal.PtrToStructure(lpVs, vs);
                if (vs.dwNeededSize > neededSize)
                {
                    neededSize = vs.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    var finalBuffer = new byte[vs.dwNeededSize];
                    Marshal.Copy(lpVs, finalBuffer, 0, vs.dwNeededSize);
                    if (vs.dwStringFormat != NativeMethods.STRINGFORMAT_BINARY)
                        retValue = NativeMethods.GetString(finalBuffer, vs.dwStringOffset, vs.dwStringSize, vs.dwStringFormat);
                    else
                    {
                        var buffer = new byte[vs.dwStringSize];
                        Array.Copy(finalBuffer, vs.dwStringOffset, buffer, 0, vs.dwStringSize);
                        retValue = buffer;
                    }
                }
                Marshal.FreeHGlobal(lpVs);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);

            return retValue;
        }

        /// <summary>
        /// This returns the underlying COMM handle for the given call.  This is primarily for modem-style lines.
        /// </summary>
        /// <returns>SafeHandle representing COMM handle or null</returns>
        public Microsoft.Win32.SafeHandles.SafeFileHandle GetCommHandle()
        {
            var buffer = (byte[])GetExternalDeviceInfo("comm/datamodem");
            if (buffer != null && buffer.Length >= IntPtr.Size)
            {
                var handle = new IntPtr(BitConverter.ToInt32(buffer, 0));
                return new Microsoft.Win32.SafeHandles.SafeFileHandle(handle, true);
            }
            return null;
        }

        /// <summary>
        /// This method returns a FileStream to represent our I/O channel
        /// with the COMM port.  It is always in asynch mode.
        /// </summary>
        /// <returns>FileStream object</returns>
        public System.IO.FileStream GetCommStream()
        {
            return new System.IO.FileStream(GetCommHandle(),
                System.IO.FileAccess.ReadWrite, 4096, true);
        }

        /// <summary>
        /// This returns the name of the COMM device this call is running on
        /// </summary>
        /// <returns>String representing COM port or null</returns>
        public string GetCommDevice()
        {
            return (string)GetExternalDeviceInfo(@"comm/datamodem/portname");
        }

        /// <summary>
        /// Returns the device id for the wave input device.  This identifier may be passed to "waveInOpen" to get a HWAVE handle.
        /// </summary>
        /// <returns>Wave Device identifier</returns>
        public int GetWaveInDeviceID()
        {
            var buffer = (byte[])GetExternalDeviceInfo("wave/in");
            return (buffer != null && buffer.Length > 0) ? BitConverter.ToInt32(buffer, 0) : -1;
        }

        /// <summary>
        /// Returns the device id for the wave output device.  This identifier may be passed to "waveOutOpen" to get a HWAVE handle.
        /// </summary>
        /// <returns>Wave Device identifier</returns>
        public int GetWaveOutDeviceID()
        {
            var buffer = (byte[])GetExternalDeviceInfo("wave/out");
            return (buffer != null && buffer.Length > 0) ? BitConverter.ToInt32(buffer, 0) : -1;
        }

        /// <summary>
        /// Returns the device id for the MIDI input device.  This identifier may be passed to "midiInOpen" to get a HMIDI handle.
        /// </summary>
        /// <returns>MIDI Device identifier</returns>
        public int GetMidiInDeviceID()
        {
            var buffer = (byte[])GetExternalDeviceInfo("midi/in");
            return (buffer != null && buffer.Length > 0) ? BitConverter.ToInt32(buffer, 0) : -1;
        }

        /// <summary>
        /// Returns the device id for the MIDI output device.  This identifier may be passed to "midiOutOpen" to get a HMIDI handle.
        /// </summary>
        /// <returns>MIDI Device identifier</returns>
        public int GetMidiOutDeviceID()
        {
            var buffer = (byte[])GetExternalDeviceInfo("midi/out");
            return (buffer != null && buffer.Length > 0) ? BitConverter.ToInt32(buffer, 0) : -1;
        }
        #endregion

        #region lineHold
        /// <summary>
        /// Places the call on hold
        /// </summary>
        public void Hold()
        {
            IAsyncResult ar = BeginHold(null, null);
            EndHold(ar);
        }

        /// <summary>
        /// Places the call on hold
        /// </summary>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAscyncResult</returns>
        public IAsyncResult BeginHold(AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineHold(Handle);
            if (rc < 0)
                throw new TapiException("lineHold failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Retrieves the final result from a <see cref="TapiCall.BeginHold"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginHold</param>
        public void EndHold(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineHold failed", req.Result);
        }
        #endregion

        #region lineMonitorDigits
        /// <summary>
        /// Enables the unbuffered detection of digits received on the call. Each time a digit of the specified digit mode is detected, a 
        /// callback event is raised in the application indicating which digit has been detected.
        /// </summary>
        /// <param name="modes">DigitModes to detect</param>
        /// <param name="acb">Callback to use</param>
        public void BeginMonitoringDigits(DigitModes modes, EventHandler<DigitDetectedEventArgs> acb)
        {
            if (acb == null)
                throw new ArgumentNullException("acb");
            if (modes == DigitModes.None)
                throw new ArgumentException("DigitModes must be supplied.");

            _digitDetectedCb = acb;
            int rc = NativeMethods.lineMonitorDigits(Handle, (int)modes);
            if (rc < 0)
            {
                _digitDetectedCb = null;
                throw new TapiException("lineMonitorDigits failed", rc);
            }
        }

        /// <summary>
        /// This is called when a digit is detected on the call.
        /// </summary>
        /// <param name="digit">Detected digit</param>
        /// <param name="digitMode">Detected mode</param>
        internal void OnDigitDetected(int digit, int digitMode)
        {
            if (_digitDetectedCb != null)
                _digitDetectedCb(this, new DigitDetectedEventArgs(this, Encoding.ASCII.GetString(BitConverter.GetBytes(digit)), digitMode));
        }

        /// <summary>
        /// This cancels any digit monitor enabled on this call.
        /// </summary>
        public void CancelMonitoringDigits()
        {
            _digitDetectedCb = null;
            NativeMethods.lineMonitorDigits(Handle, 0);
        }
        #endregion

        #region lineMonitorTones
        /// <summary>
        /// Enables the detection of inband tones on the call. Each time a specified tone is detected, a message is sent to the application.
        /// </summary>
        /// <param name="tones">List of tones to watch for</param>
        /// <param name="acb">Callback</param>
        public void BeginMonitoringTones(MonitorTone[] tones, EventHandler<ToneDetectedEventArgs> acb)
        {
            if (acb == null)
                throw new ArgumentNullException("acb");
            if (tones == null)
                throw new ArgumentNullException("tones");
            if (tones.Length == 0)
                throw new ArgumentException("MonitorTones must be supplied.");

            // Copy tone list to TAPI specific structure
            var intTones = new LINEMONITORTONE[tones.Length];
            for (int i = 0; i < tones.Length; i++)
            {
                intTones[i] = new LINEMONITORTONE
                  {
                      dwAppSpecific = i,
                      dwDuration = tones[i].Duration,
                      dwFrequency1 = tones[i].Frequency1,
                      dwFrequency2 = tones[i].Frequency2,
                      dwFrequency3 = tones[i].Frequency3
                  };
            }

            // Set our callback and tone list
            _toneDetectedCb = acb;
            _monitorTones = tones;

            // Turn on tone monitoring
            int rc = NativeMethods.lineMonitorTones(Handle, intTones, intTones.Length);
            if (rc < 0)
            {
                _toneDetectedCb = null;
                _monitorTones = null;
                if (rc == NativeMethods.LINEERR_INVALPOINTER)
                    throw new ArgumentException("Tone list has too many entries");
                throw new TapiException("lineMonitorTones failed", rc);
            }
        }

        /// <summary>
        /// This is called when a tone is detected
        /// </summary>
        internal void OnToneDetected(int pos)
        {
            // Ignore if cancled
            if (_toneDetectedCb == null || _monitorTones == null || _monitorTones.Length < pos)
                return;

            // Callback the app
            _toneDetectedCb(this, new ToneDetectedEventArgs(this, _monitorTones[pos]));
        }

        /// <summary>
        /// This is used to cancel a tone monitor request.
        /// </summary>
        public void CancelMonitoringTones()
        {
            _toneDetectedCb = null;
            _monitorTones = null;

            NativeMethods.lineMonitorTones(Handle, null, 0);
        }
        #endregion

        #region linePark
        /// <summary>
        /// Parks the call at the directed address
        /// </summary>
        /// <param name="address">Number to park the call at</param>
        public void Park(string address)
        {
            IAsyncResult ar = BeginPark(address, null, null);
            EndPark(ar);
        }

        /// <summary>
        /// Parks the call at a non-directed address and returns where it was parked.
        /// </summary>
        /// <returns>Park address</returns>
        public string Park()
        {
            IAsyncResult ar = BeginPark(null, null);
            return EndPark(ar); 
        }

        /// <summary>
        /// Parks the call at the directed address
        /// </summary>
        /// <param name="address">Number to park the call at</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginPark(string address, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.linePark(Handle, NativeMethods.LINEPARKMODE_DIRECTED, address, IntPtr.Zero);
            if (rc < 0)
                throw new TapiException("linePark failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Parks the call at a non-directed address
        /// </summary>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginPark(AsyncCallback acb, object state)
        {
            var vs = new VARSTRING();
            vs.dwTotalSize = Marshal.SizeOf(vs) + 1024;
            IntPtr pvs = Marshal.AllocHGlobal(vs.dwTotalSize);
            Marshal.StructureToPtr(vs, pvs, true);

            int rc = NativeMethods.linePark(Handle, NativeMethods.LINEPARKMODE_NONDIRECTED, string.Empty, pvs);
            if (rc < 0)
            {
                Marshal.FreeHGlobal(pvs);
                throw new TapiException("linePark failed", rc);
            }

            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state, pvs, vs.dwTotalSize));
        }

        /// <summary>
        /// Retrieves the result of a <see cref="TapiCall.BeginPark(AsyncCallback, object)"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginPark</param>
        /// <returns>Non-directed park address if available</returns>
        public string EndPark(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            IntPtr pvs = req.ApiData;
            if (req.Result < 0)
            {
                if (pvs != IntPtr.Zero)
                    Marshal.FreeHGlobal(pvs);
                throw new TapiException("linePark failed", req.Result);
            }

            // Get the address
            string result = string.Empty;
            if (pvs != IntPtr.Zero)
            {
                var vs = new VARSTRING();
                Marshal.PtrToStructure(pvs, vs);
                if (vs.dwStringOffset > 0 && vs.dwStringSize > 0)
                {
                    var buffer = new byte[vs.dwUsedSize];
                    Marshal.Copy(pvs, buffer, 0, vs.dwUsedSize);
                    result = NativeMethods.GetString(buffer, vs.dwStringOffset, vs.dwStringSize, vs.dwStringFormat);
                }
                Marshal.FreeHGlobal(pvs);
            }
            return result;
        }
        #endregion

        #region linePrepareAddToConference
        /// <summary>
        /// This function gets a consultation call from a conference in order to add a new party to the conference
        /// </summary>
        /// <param name="mcp">Optional call parameters for the consultation call.  You can perform an auto conference by supplying a TargetAddress.</param>
        /// <returns>Consultation call</returns>
        public TapiCall PrepareAddToConference(MakeCallParams mcp)
        {
            IntPtr lpCp = IntPtr.Zero;
            int callFlags = 0;
            try
            {
                if (mcp != null && !String.IsNullOrEmpty(mcp.TargetAddress))
                    callFlags |= NativeMethods.LINECALLPARAMFLAGS_NOHOLDCONFERENCE;
                lpCp = MakeCallParams.ProcessCallParams(Address.Id, mcp, callFlags);
                IntPtr hCall;
                int rc = NativeMethods.linePrepareAddToConference(Handle, out hCall, lpCp);
                if (rc < 0)
                {
                    throw new TapiException("linePrepareAddToConference failed", rc);
                }
                else
                {
                    // Wait for the LINE_REPLY so we don't need to deal with the value type 
                    // issues of IntPtr being filled in async.
                    var req = new PendingTapiRequest(rc, null, null);
                    Line.TapiManager.AddAsyncRequest(req);
                    req.AsyncWaitHandle.WaitOne();
                    if (req.Result < 0)
                        throw new TapiException("linePrepareAddToConference failed", req.Result);

                    var call = new TapiCall(Address, hCall);
                    Address.AddCall(call);
                    return call;
                }
            }
            finally
            {
                if (lpCp != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpCp);
            }
        }
        #endregion

        #region lineRedirect
        /// <summary>
        /// Redirects the specified offering call to the specified destination address
        /// </summary>
        /// <param name="destAddress">Address to redirect to</param>
        /// <param name="countryCode">Country code (zero for default)</param>
        public void Redirect(string destAddress, int countryCode)
        {
            IAsyncResult ar = BeginRedirect(destAddress, countryCode, null, null);
            EndRedirect(ar);
        }

        /// <summary>
        /// Redirects the specified offering call to the specified destination address
        /// </summary>
        /// <param name="destAddress">Address to redirect to</param>
        /// <param name="countryCode">Country code (zero for default)</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginRedirect(string destAddress, int countryCode, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineRedirect(Handle, destAddress, countryCode);
            if (rc < 0)
                throw new TapiException("lineRedirect failed", rc);

            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Harvests the results from a call to <see cref="TapiCall.BeginRedirect"/>.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginRedirect</param>
        public void EndRedirect(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineRedirect failed", req.Result);
        }
        #endregion

        #region lineReleaseUserUserInfo
        /// <summary>
        /// This function informs the service provider that the application has processed the user-user information contained in the call, and that subsequently 
        /// received user-user information can now be written into that structure. 
        /// </summary>
        public void ClearUserUserInfo()
        {
            int rc = NativeMethods.lineReleaseUserUserInfo(Handle);
            if (rc > 0)
            {
                // Wait for it to complete
                var req = new PendingTapiRequest(rc, null, null);
                TapiManager.AddAsyncRequest(req);
                req.AsyncWaitHandle.WaitOne();
            }
        }
        #endregion

        #region lineRemoveFromConference
        /// <summary>
        /// This removes a call from the existing conference.  The current call must be a
        /// conference call and in the proper call state.
        /// </summary>
        /// <param name="callToRemove">Call to remove from this conference</param>
        public void RemoveFromConference(TapiCall callToRemove)
        {
            IAsyncResult ar = BeginRemoveToConference(callToRemove, null, null);
            EndRemoveFromConference(ar);
        }

        /// <summary>
        /// This removes a call from the existing conference.  The current call must be a
        /// conference call and in the proper call state.
        /// </summary>
        /// <param name="callToRemove">Call to remove from this conference</param>
        /// <param name="acb">Callback function</param>
        /// <param name="state">State parameter</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginRemoveToConference(TapiCall callToRemove, AsyncCallback acb, object state)
        {
            if (callToRemove == null)
                throw new ArgumentNullException("callToRemove");

            int rc = NativeMethods.lineRemoveFromConference(Handle, callToRemove.Handle);
            if (rc < 0)
                throw new TapiException("lineRemoveFromConference failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// This ends an asynchronous RemoveFromConference.
        /// </summary>
        /// <param name="ar">Pending IAsyncResult request</param>
        public void EndRemoveFromConference(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineRemoveFromConference failed", req.Result);
        }
        #endregion

        #region lineSecureCall
        /// <summary>
        /// Secures the call from any interruptions or interference that can affect the call's media stream.
        /// </summary>
        public void SecureCall()
        {
            IAsyncResult ar = BeginSecureCall(null, null);
            EndSecureCall(ar);
        }

        /// <summary>
        /// Secures the call from any interruptions or interference that can affect the call's media stream.
        /// </summary>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAscyncResult</returns>
        public IAsyncResult BeginSecureCall(AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineSecureCall(Handle);
            if (rc < 0)
                throw new TapiException("lineSecureCall failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Retrieves the final result from a <see cref="TapiCall.BeginSecureCall"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginHold</param>
        public void EndSecureCall(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineSecureCall failed", req.Result);
        }
        #endregion

        #region lineSendUserUserInfo
        /// <summary>
        /// This method sends user-user information to the remote party on the specified call.
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <returns>true/false success</returns>
        public bool SendUserUserInfo(string message)
        {
            if (message == null)
                message = string.Empty;

            int rc = NativeMethods.lineSendUserUserInfo(Handle, message, message.Length);
            if (rc < 0)
                return false;
            if (rc > 0)
            {
                // Wait for it to complete
                var req = new PendingTapiRequest(rc, null, null);
                TapiManager.AddAsyncRequest(req);
                req.AsyncWaitHandle.WaitOne();
                return (req.Result == 0);
            }
            return true;
        }
        #endregion

        #region lineSetCallTreatment
        /// <summary>
        /// This method sets the sounds a party on a call that is unanswered or on hold hears.
        /// </summary>
        /// <param name="ct">Designated call treatment type</param>
        /// <returns></returns>
        public bool SetCallTreatment(CallTreatment ct)
        {
            if (ct == null)
                throw new ArgumentNullException("ct");

            int rc = NativeMethods.lineSetCallTreatment(Handle, ct.Id);
            if (rc < 0)
                throw new TapiException("lineSetCallTreatment failed", rc);

            var req = new PendingTapiRequest(rc, null, null);
            TapiManager.AddAsyncRequest(req);
            req.AsyncWaitHandle.WaitOne();
            return req.Result == 0;
        }
        #endregion

        #region lineSetupConference
        /// <summary>
        /// This method is used to establish a conference call
        /// </summary>
        /// <param name="conferenceCount"># of parties anticipated the conference</param>
        /// <param name="mcp">Call parameters for created consultation call</param>
        /// <param name="consultCall">Returning consultation call</param>
        /// <returns>Conference call</returns>
        public TapiCall SetupConference(int conferenceCount, MakeCallParams mcp, out TapiCall consultCall)
        {
            IntPtr lpCp = IntPtr.Zero;
            int callFlags = 0;
            try
            {
                if (mcp != null && !String.IsNullOrEmpty(mcp.TargetAddress))
                    callFlags |= NativeMethods.LINECALLPARAMFLAGS_NOHOLDCONFERENCE;
                lpCp = MakeCallParams.ProcessCallParams(Address.Id, mcp, callFlags);
                IntPtr hCall, hConfCall;
                int rc = NativeMethods.lineSetupConference(Handle, new HTLINE(), out hConfCall, out hCall, conferenceCount, lpCp);
                if (rc < 0)
                {
                    throw new TapiException("lineSetupConference failed", rc);
                }
                else
                {
                    // Wait for the LINE_REPLY so we don't need to deal with the value type 
                    // issues of IntPtr being filled in async.
                    var req = new PendingTapiRequest(rc, null, null);
                    Line.TapiManager.AddAsyncRequest(req);
                    req.AsyncWaitHandle.WaitOne();
                    if (req.Result < 0)
                        throw new TapiException("lineSetupConference failed", req.Result);

                    if (hCall != IntPtr.Zero)
                    {
                        consultCall = new TapiCall(Address, hCall);
                        Address.AddCall(consultCall);
                    }
                    else
                        consultCall = null;

                    var confCall = new TapiCall(Address, hConfCall);
                    Address.AddCall(confCall);
                    
                    return confCall;
                }
            }
            finally
            {
                if (lpCp != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpCp);
            }
        }
        #endregion

        #region lineSetupTransfer and lineCompleteTransfer
        /// <summary>
        ///  This function initiates a transfer of the call through the use of a consultation call on which the party can be dialed that can 
        /// become the destination of the transfer. The application acquires owner privilege to the returned call.
        /// </summary>
        /// <param name="param">Optional call parameters for the returned consultation call.  You can perform a one-step transfer by supplying a TargetAddress</param>
        /// <returns>Consultation call</returns>
        public TapiCall SetupTransfer(MakeCallParams param)
        {
            IntPtr lpCp = IntPtr.Zero;
            try
            {
                int callFlags = 0;
                if (param != null && !String.IsNullOrEmpty(param.TargetAddress))
                    callFlags |= NativeMethods.LINECALLPARAMFLAGS_ONESTEPTRANSFER;
                lpCp = MakeCallParams.ProcessCallParams(Address.Id, param, callFlags);
                IntPtr hCall;

                int rc = NativeMethods.lineSetupTransfer(Handle, out hCall, lpCp);
                if (rc < 0)
                    throw new TapiException("lineSetupTransfer failed", rc);
                else
                {
                    // Wait for the LINE_REPLY so we don't need to deal with the value type 
                    // issues of IntPtr being filled in async.
                    var req = new PendingTapiRequest(rc, null, null);
                    Line.TapiManager.AddAsyncRequest(req);
                    req.AsyncWaitHandle.WaitOne();
                    if (req.Result < 0)
                        throw new TapiException("lineSetupTransfer failed", req.Result);

                    if (hCall != IntPtr.Zero)
                    {
                        var call = new TapiCall(Address, hCall);
                        Address.AddCall(call);
                        return call;
                    }
                }
            }
            finally
            {
                if (lpCp != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpCp);
            }
            return null;
        }

        /// <summary>
        /// This function completes the transfer of the specified call to the party connected in the consultation call.
        /// </summary>
        /// <param name="consultationCall">Consultation call to transfer to</param>
        public void CompleteTransfer(TapiCall consultationCall)
        {
            if (consultationCall == null)
                throw new ArgumentNullException("consultationCall");

            IntPtr lpCp;
            int rc = NativeMethods.lineCompleteTransfer(Handle, consultationCall.Handle, out lpCp, NativeMethods.LINETRANSFERMODE_TRANSFER);
            if (rc < 0)
                throw new TapiException("lineCompleteTransfer failed", rc);
            
            var req = new PendingTapiRequest(rc, null, null);
            Line.TapiManager.AddAsyncRequest(req);
            if (req.AsyncWaitHandle.WaitOne(5000, true))
            {
                if (req.Result < 0)
                    throw new TapiException("lineCompleteTransfer failed", req.Result);
            }
        }

        /// <summary>
        /// This function completes a transfer by conferencing the two parties together.
        /// </summary>
        /// <param name="consultationCall">The second call to conference in with this one.</param>
        /// <returns>Conference call</returns>
        public TapiCall CompleteTransferToConference(TapiCall consultationCall)
        {
            if (consultationCall == null)
                throw new ArgumentNullException("consultationCall");

            IntPtr hCall;
            int rc = NativeMethods.lineCompleteTransfer(Handle, consultationCall.Handle, out hCall, NativeMethods.LINETRANSFERMODE_CONFERENCE);
            if (rc < 0)
                throw new TapiException("lineCompleteTransfer failed", rc);
            
            var req = new PendingTapiRequest(rc, null, null);
            Line.TapiManager.AddAsyncRequest(req);
            if (req.AsyncWaitHandle.WaitOne(5000, true))
            {
                if (req.Result < 0)
                    throw new TapiException("lineCompleteTransfer failed", req.Result);
            }

            // We assume same address..
            var call = new TapiCall(Address, hCall);
            Address.AddCall(call);
            return call;
        }
        #endregion

        #region lineSwapHold
        /// <summary>
        /// Swaps two calls from active to on hold
        /// </summary>
        public void SwapHold(TapiCall otherCall)
        {
            IAsyncResult ar = BeginSwapHold(otherCall, null, null);
            EndSwapHold(ar);
        }

        /// <summary>
        /// Swaps two calls from active to on hold
        /// </summary>
        /// <param name="otherCall">Call to swap with</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAscyncResult</returns>
        public IAsyncResult BeginSwapHold(TapiCall otherCall, AsyncCallback acb, object state)
        {
            if (otherCall == null)
                throw new ArgumentNullException("otherCall");

            HTCALL heldCall, activeCall;
            if ((CallState & (CallState.OnHold | CallState.OnHoldPendingConference | CallState.OnHoldPendingTransfer)) > 0)
            {
                heldCall = Handle;
                activeCall = otherCall.Handle;
            }
            else
            {
                heldCall = otherCall.Handle;
                activeCall = Handle;
            }

            int rc = NativeMethods.lineSwapHold(activeCall, heldCall);
            if (rc < 0)
                throw new TapiException("lineSwapHold failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Retrieves the final result from a <see cref="TapiCall.BeginSwapHold"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginSwapHold</param>
        public void EndSwapHold(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineSwapHold failed", req.Result);
        }
        #endregion

        #region lineUnhold
        /// <summary>
        /// Retrieves a call that is holding
        /// </summary>
        public void Unhold()
        {
            IAsyncResult ar = BeginUnhold(null, null);
            EndUnhold(ar);
        }

        /// <summary>
        /// Retrieves a call that is holding
        /// </summary>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAscyncResult</returns>
        public IAsyncResult BeginUnhold(AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineUnhold(Handle);
            if (rc < 0)
                throw new TapiException("lineUnhold failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Retrieves the final result from a <see cref="TapiCall.BeginUnhold"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginUnhold</param>
        public void EndUnhold(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineUnhold failed", req.Result);
        }
        #endregion

        /// <summary>
        /// This returns an array of calls by call-id.  Warning - it can return calls across lines and providers.
        /// </summary>
        /// <param name="callId">Callid</param>
        /// <returns>Array of call objects</returns>
        static public TapiCall[] GetCallsById(int callId)
        {
            var rCalls = new List<TapiCall>();
            lock (CallsMap)
            {
                foreach (TapiCall call in CallsMap.Values)
                {
                    if (call.Id == callId)
                        rCalls.Add(call);
                }
            }
            return rCalls.ToArray();
        }

        internal void ForceClose()
        {
            Address.RemoveCall(this);
            lock (CallsMap)
            {
                CallsMap.Remove(_hCall.DangerousGetHandle());
            }

            _hCall.SetHandleAsInvalid();
            CallState oldState = _callState;
            _callState = CallState.Idle;
            Address.OnCallStateChange(new CallStateEventArgs(this, CallState.Idle, oldState, MediaModes.None));
        }

        internal void Deallocate()
        {
            Address.RemoveCall(this);
            lock (CallsMap)
            {
                CallsMap.Remove(_hCall.DangerousGetHandle());
            }
            _hCall.Close();
        }

        internal void OnCallStateChange(int callState, IntPtr stateData, MediaModes mediaModes)
        {
            var cs = (CallState)callState;
            CallStateEventArgs e;

            switch (cs)
            {
                case CallState.Offering:
                    e = new OfferingCallStateEventArgs(this, cs, CallState, (OfferingModes)stateData.ToInt32(), mediaModes);
                    break;
                case CallState.Busy:
                    e = new BusyCallStateEventArgs(this, cs, CallState, (BusyModes)stateData.ToInt32(), mediaModes);
                    break;
                case CallState.SpecialInfo:
                    e = new SpecialInfoCallStateEventArgs(this, cs, CallState, (SpecialInfoModes)stateData.ToInt32(), mediaModes);
                    break;
                case CallState.Connected:
                    e = new ConnectedCallStateEventArgs(this, cs, CallState, (ConnectModes)stateData.ToInt32(), mediaModes);
                    break;
                case CallState.Disconnected:
                    e = new DisconnectedCallStateEventArgs(this, cs, CallState, (DisconnectModes)stateData.ToInt32(), mediaModes);
                    break;
                case CallState.Conferenced:
                    e = new ConferencedCallStateEventArgs(this, cs, CallState, FindCallByHandle(stateData), mediaModes);
                    break;
                default:
                    e = new CallStateEventArgs(this, cs, CallState, mediaModes);
                    break;
            }

            // Reload LINECALLSTATUS
            GatherCallStatus();

            // Notify subscribers through address and line
            Address.OnCallStateChange(e);

            // Deallocate call
            if (callState == NativeMethods.LINECALLSTATE_IDLE)
                Deallocate();
        }

        internal void OnCallInfoChange(int changeInfo)
        {
            GatherCallInfo();
            Address.OnCallInfoChange(new CallInfoChangeEventArgs(this, (CallInfoChangeTypes)changeInfo));
        }

        internal void OnMediaModeDetected(MediaModes mode)
        {
            int rc = NativeMethods.lineSetMediaMode(Handle, (int)mode);
            if (rc == 0)
                OnCallInfoChange(NativeMethods.LINECALLINFOSTATE_MEDIAMODE);
        }

        private void GatherCallStatus()
        {
            var lcs = new LINECALLSTATUS();
            int rc, neededSize = Marshal.SizeOf(lcs) + 100;
            do
            {
                lcs.dwTotalSize = neededSize;
                IntPtr pLcs = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lcs, pLcs, true);
                rc = NativeMethods.lineGetCallStatus(_hCall, pLcs);
                Marshal.PtrToStructure(pLcs, lcs);
                if (lcs.dwNeededSize > neededSize)
                {
                    neededSize = lcs.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                Marshal.FreeHGlobal(pLcs);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);

            if (rc == NativeMethods.LINEERR_INVALCALLHANDLE)
            {
                Deallocate();
            }
            else
            {
                _callState = (CallState)lcs.dwCallState;
                _csTime = lcs.tStateEntryTime;
                _csPrivilege = (lcs.dwCallPrivilege == NativeMethods.LINECALLPRIVILEGE_NONE) ? Privilege.None :
                    (lcs.dwCallPrivilege == NativeMethods.LINECALLPRIVILEGE_MONITOR) ? Privilege.Monitor : Privilege.Owner;
                _features = new CallFeatureSet(lcs.dwCallFeatures);
            }
        }

        private void GatherCallInfo()
        {
            int rc, neededSize = 1024;
            do
            {
                _lci.dwTotalSize = neededSize;
                IntPtr pLci = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(_lci, pLci, true);
                rc = NativeMethods.lineGetCallInfo(_hCall, pLci);
                Marshal.PtrToStructure(pLci, _lci);
                if (_lci.dwNeededSize > neededSize)
                {
                    neededSize = _lci.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    _lciData = new byte[_lci.dwUsedSize];
                    Marshal.Copy(pLci, _lciData, 0, _lci.dwUsedSize);
                }
                Marshal.FreeHGlobal(pLci);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);

            if (rc == NativeMethods.LINEERR_INVALCALLHANDLE)
            {
                Deallocate();
            }
        }

        /// <summary>
        /// This is invoked when the digits or tone are completed or canceled.
        /// </summary>
        /// <param name="reason">Reason code</param>
        internal void OnGenerateDigitsOrToneComplete(int reason)
        {
            int id = System.Threading.Interlocked.Exchange(ref _gtdreqId, 0);
            System.Diagnostics.Debug.Assert(id != 0);
            if (id != 0)
                TapiManager.HandleCompletion(id, reason);
        }


        /// <summary>
        /// IDisposable.Dispose implementation
        /// </summary>
        public void Dispose()
        {
            _hCall.Dispose();
        }
    }
}
