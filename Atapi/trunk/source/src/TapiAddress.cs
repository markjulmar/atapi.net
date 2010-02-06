// TapiAddress.cs
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
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using JulMar.Atapi.Interop;
using System.Globalization;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class wraps the capabilities for a single <see>TapiAddress</see> object.
    /// </summary>
    public sealed class AddressCapabilities
    {
        readonly LINEADDRESSCAPS _lac;
        readonly byte[] _rawBuffer;
        readonly int _stringFormat;
        readonly CallFeatureSet _callFeatures;
        readonly CallTreatment[] _treatments;

        /// <summary>
        /// Constructor for the AddressCapabilties
        /// </summary>
        /// <param name="lac"></param>
        /// <param name="rawBuffer"></param>
        /// <param name="stringFormat"></param>
        internal AddressCapabilities(LINEADDRESSCAPS lac, byte[] rawBuffer, int stringFormat)
        {
            _lac = lac;
            _rawBuffer = rawBuffer;
            _stringFormat = stringFormat;
            _callFeatures = new CallFeatureSet(lac.dwCallFeatures);

            // Grab the available call treatments
            if (_lac.dwCallTreatmentListSize > 0 && _lac.dwNumCallTreatments > 0)
            {
                IntPtr pi = Marshal.AllocHGlobal(_lac.dwCallTreatmentListSize);
                var lce = new LINECALLTREATMENTENTRY();
                _treatments = new CallTreatment[_lac.dwNumCallTreatments];
                int len = Marshal.SizeOf(lce);
                for (int i = 0; i < _lac.dwNumCallTreatments; i++)
                {
                    Marshal.Copy(rawBuffer, _lac.dwCallTreatmentListOffset + (i * len), pi, len);
                    Marshal.PtrToStructure(pi, lce);
                    _treatments[i] = new CallTreatment(lce.dwCallTreatmentID, 
                                    NativeMethods.GetString(rawBuffer, lce.dwCallTreatmentNameOffset, lce.dwCallTreatmentNameSize, stringFormat));
                }
                Marshal.FreeHGlobal(pi);
            }
        }

        /// <summary>
        /// The features that are possibly available to calls on this address.
        /// </summary>
        public CallFeatureSet CallFeatures
        {
            get { return _callFeatures; }
        }

        /// <summary>
        /// The Dialable number associated with this address.  This may be blank.
        /// </summary>
        public string DialableAddress
        {
            get { return NativeMethods.GetString(_rawBuffer, _lac.dwAddressOffset, _lac.dwAddressSize, _stringFormat); } 
        }

        /// <summary>
        /// This returns the state of any call removed from a conference
        /// </summary>
        public CallState RemoveFromConferenceState
        {
            get { return (CallState) _lac.dwRemoveFromConfState; }
        }

        /// <summary>
        /// This returns whether calls may be removed from conferences on this address.
        /// </summary>
        public RemoveFromConferenceType RemoveFromConferenceTypes
        {
            get { return (RemoveFromConferenceType)_lac.dwRemoveFromConfCaps; }
        }

        /// <summary>
        /// The address sharing mode (bridged, private, monitored).
        /// </summary>
        public AddressSharingModes SharingMode
        {
            get { return (AddressSharingModes)_lac.dwAddressSharing; }
        }

        /// <summary>
        /// The valid call states for calls on this address.
        /// </summary>
        public CallState ValidCallStates
        {
            get { return (CallState)_lac.dwCallStates; }
        }

        /// <summary>
        /// The supported dialtone types which may be reported when a call is in the dialtone state.
        /// </summary>
        public DialtoneModes SupportedDialtoneModes
        {
            get { return (DialtoneModes)_lac.dwDialToneModes; }
        }

        /// <summary>
        /// The supported busy tone types which may be reported when a call is in the busy state.
        /// </summary>
        public BusyModes SupportedBusyModes
        {
            get { return (BusyModes)_lac.dwBusyModes; }
        }

        /// <summary>
        /// The supported connect mode types which may be reported when a call is in the connected state.
        /// </summary>
        public ConnectModes SupportedConnectModes
        {
            get { return (ConnectModes)_lac.dwConnectedModes; }
        }

        /// <summary>
        /// The supported offering mode types which may be reported when a call is in the ringing state.
        /// </summary>
        public OfferingModes SupportedOfferingModes
        {
            get { return (OfferingModes)_lac.dwOfferingModes; }
        }

        /// <summary>
        /// The types of disconnects which can occur.
        /// </summary>
        public DisconnectModes SupportedDisconnectModes
        {
            get { return (DisconnectModes)_lac.dwDisconnectModes; }
        }

        /// <summary>
        /// Returns the types of parked calls this address can manage
        /// </summary>
        public bool SupportsDirectedPark
        {
            get { return (_lac.dwParkModes & NativeMethods.LINEPARKMODE_DIRECTED) > 0; }
        }

        /// <summary>
        /// Returns the types of parked calls this address can manage
        /// </summary>
        public bool SupportsNonDirectedPark
        {
            get { return (_lac.dwParkModes & NativeMethods.LINEPARKMODE_NONDIRECTED) > 0; }
        }

        /// <summary>
        /// Returns whether this address supports predictive dialing or not.
        /// </summary>
        public bool SupportsPredictiveDialing
        {
            get { return (_lac.dwAddrCapFlags & NativeMethods.LINEADDRCAPFLAGS_PREDICTIVEDIALER) > 0; }
        }

        /// <summary>
        /// The types of forwarding supported on this address.
        /// </summary>
        public ForwardingMode SupportedForwardingModes
        {
            get { return (ForwardingMode)_lac.dwForwardModes; }
        }

        /// <summary>
        /// Returns the maximum number of forwarding entries allowed.
        /// </summary>
        public int MaxForwardEntries
        {
            get { return _lac.dwMaxForwardEntries;  }
        }

        /// <summary>
        /// The maximum number of calls which may be active simultaneously on this address.
        /// </summary>
        public int MaxActiveCallCount
        {
            get { return _lac.dwMaxNumActiveCalls; }
        }

        /// <summary>
        /// The maximum number of calls which can be on hold simultaneously.
        /// </summary>
        public int MaxNumOnHoldCallCount
        {
            get { return _lac.dwMaxNumOnHoldCalls; }
        }

        /// <summary>
        /// The maximum number of calls which can be on hold pending a transfer simultaneously.
        /// </summary>
        public int MaxNumOnHoldPendingTransferCallCount
        {
            get { return _lac.dwMaxNumOnHoldPendingCalls; }
        }

        /// <summary>
        /// The maximum number of calls which can be transferred into a conference state.
        /// </summary>
        public int MaxNumTransferToConferenceCallCount
        {
            get { return _lac.dwMaxNumTransConf; }
        }

        /// <summary>
        /// The maximum number of parties in a conference on this address.
        /// </summary>
        public int MaxNumPartiesInConferenceCount
        {
            get { return _lac.dwMaxNumConference; }
        }

        /// <summary>
        /// The maximum size for any CallData on a call.
        /// </summary>
        public int MaxCallDataSize
        {
            get { return _lac.dwMaxCallDataSize; }
        }

        /// <summary>
        /// This returns the list of available call treatments for calls on this address, or null if none are available.
        /// </summary>
        public CallTreatment[] AvailableCallTreatments
        {
            get { return (CallTreatment[]) _treatments.Clone(); }
        }

        /// <summary>
        /// This returns the array of call completion messages which can be applied
        /// </summary>
        public string[] AvailableCallCompletionMessages
        {
            get
            {
                string[] complMessages = null;
                if (_lac.dwNumCompletionMessages > 0 && _lac.dwCompletionMsgTextSize > 0)
                {
                    complMessages = new string[_lac.dwNumCompletionMessages];
                    for (int i = 0; i < _lac.dwNumCompletionMessages; i++)
                        complMessages[i] = NativeMethods.GetString(_rawBuffer, _lac.dwCompletionMsgTextOffset + (i * _lac.dwCompletionMsgTextEntrySize), _lac.dwCompletionMsgTextEntrySize, _stringFormat);
                }
                return complMessages;
            }
        }

        /// <summary>
        /// Returns the Device Specific data for this line
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get
            {
                var data = new byte[_lac.dwDevSpecificSize];
                Array.Copy(_rawBuffer, _lac.dwDevSpecificOffset, data, 0, _lac.dwDevSpecificSize);
                return data;
            }
        }

        /// <summary>
        /// Returns a System.String that represents the current AddressCapabilities object.
        /// </summary>
        /// <returns></returns>
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
    /// This class wraps the current status for a single <see>TapiAddress</see> object.
    /// </summary>
    public sealed class AddressStatus
    {
        readonly LINEADDRESSSTATUS _las;
        readonly byte[] _rawBuffer;
        readonly int _stringFormat;

        internal AddressStatus(LINEADDRESSSTATUS las, byte[] rawBuffer, int stringFormat)
        {
            _las = las;
            _rawBuffer = rawBuffer;
            _stringFormat = stringFormat;
        }

        /// <summary>
        /// The in use count.
        /// </summary>
        public int InUseCount
        {
            get { return _las.dwNumInUse; }
        }

        /// <summary>
        /// Number of active calls on this address.
        /// </summary>
        public int ActiveCallCount
        {
            get { return _las.dwNumActiveCalls; }
        }

        /// <summary>
        /// Number of held calls on this address.
        /// </summary>
        public int OnHoldCount
        {
            get { return _las.dwNumOnHoldCalls; }
        }

        /// <summary>
        /// Number of calls pending a transfer on this address.
        /// </summary>
        public int OnHoldPendingTransferCount
        {
            get { return _las.dwNumOnHoldPendCalls; }
        }

        /// <summary>
        /// The no answer ring count.
        /// </summary>
        public int NoAnswerRingCount
        {
            get { return _las.dwNumRingsNoAnswer; }
        }

        /// <summary>
        /// Returns whether the address can forward calls.
        /// </summary>
        public bool CanForward
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_FORWARD) > 0; }
        }

        /// <summary>
        /// Returns whether the address can make a call.
        /// </summary>
        public bool CanMakeCall
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_MAKECALL) > 0; }
        }

        /// <summary>
        /// Returns whether the address can pickup a call.
        /// </summary>
        public bool CanPickupCall
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_PICKUP) > 0; }
        }

        /// <summary>
        /// Returns whether the address can set media control.
        /// </summary>
        public bool CanSetMediaControl
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_SETMEDIACONTROL) > 0; }
        }

        /// <summary>
        /// Returns whether the address can change terminal settings.
        /// </summary>
        public bool CanSetTerminal
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_SETTERMINAL) > 0; }
        }

        /// <summary>
        /// Returns whether the address can setup a new conference.
        /// </summary>
        public bool CanSetupConference
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_SETUPCONF) > 0; }
        }

        /// <summary>
        /// Returns whether an address can uncomplete a call.
        /// </summary>
        public bool CanUncompleteCall
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_UNCOMPLETECALL) > 0; }
        }

        /// <summary>
        /// Returns whether an address can unpark a call.
        /// </summary>
        public bool CanUnparkCall
        {
            get { return (_las.dwAddressFeatures & NativeMethods.LINEADDRFEATURE_UNPARK) > 0; }
        }

        /// <summary>
        /// Returns the Device Specific data for this line
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get
            {
                var data = new byte[_las.dwDevSpecificSize];
                Array.Copy(_rawBuffer, _las.dwDevSpecificOffset, data, 0, _las.dwDevSpecificSize);
                return data;
            }
        }

        /// <summary>
        /// Returns the current forwarding information for an address.
        /// </summary>
        public ForwardInfo[] ForwardingInformation
        {
            get
            {
                var fwdInfo = new ForwardInfo[_las.dwForwardNumEntries];
                if (_las.dwForwardNumEntries > 0)
                {
                    for (int i = 0; i < _las.dwForwardNumEntries; i++)
                    {
                        var lfw = new LINEFORWARD();
                        int size = Marshal.SizeOf(lfw);
                        int pos = i * size;
                        IntPtr pLpe = Marshal.AllocHGlobal(size);
                        Marshal.Copy(_rawBuffer, pos + _las.dwForwardOffset, pLpe, size);
                        Marshal.PtrToStructure(pLpe, lfw);
                        Marshal.FreeHGlobal(pLpe);

                        fwdInfo[i] = new ForwardInfo(
                            (ForwardingMode)lfw.dwForwardMode, 
                            (AddressType)lfw.dwCallerAddressType, 
                            (lfw.dwCallerAddressOffset > 0) ?
                                NativeMethods.GetString(_rawBuffer, lfw.dwCallerAddressOffset, lfw.dwCallerAddressSize, _stringFormat) : string.Empty,
                            lfw.dwDestCountryCode,
                            (AddressType)lfw.dwDestAddressType,
                            (lfw.dwDestAddressOffset > 0) ?
                                NativeMethods.GetString(_rawBuffer, lfw.dwDestAddressOffset, lfw.dwDestAddressSize, _stringFormat) : string.Empty);
                    }
                }
                return fwdInfo;
            }
        }

        /// <summary>
        /// Returns a System.String that represents the current AddressStatus object.
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
    /// This class represents a single Tapi Address object.
    /// </summary>
    public sealed class TapiAddress
	{
		private readonly int _addressId;
		private readonly TapiLine _lineOwner;
		private string _name;
        private AddressCapabilities _addrCaps;
        private AddressStatus _addrStatus;
        private readonly List<TapiCall> _calls = new List<TapiCall>();

        /// <summary>
        /// This event is raised when a new call is discovered on the address.  It is not raised initially when the 
        /// owner line is opened and existing calls are there.
        /// </summary>
        public event EventHandler<NewCallEventArgs> NewCall;
        /// <summary>
        /// This event is raised when a call on this address changes state.
        /// </summary>
        public event EventHandler<CallStateEventArgs> CallStateChanged;
        /// <summary>
        /// This event is raised when the information associated with a call changes.
        /// </summary>
        public event EventHandler<CallInfoChangeEventArgs> CallInfoChanged;
        /// <summary>
        /// This event is raised when the status of the address changes.
        /// </summary>
        public event EventHandler<AddressInfoChangeEventArgs> Changed;
		
		internal TapiAddress(TapiLine line, int addressId)
		{
			_lineOwner = line;
			_addressId = addressId;
            _lineOwner.NewCall += HandleNewCall;

            GatherAddressCaps();
            GatherAddressStatus();
		}

        /// <summary>
        /// The <see>TapiLine</see> associated with the address.
        /// </summary>
        public TapiLine Line
        {
            get { return _lineOwner; }
        }

        /// <summary>
        /// The numeric address ID
        /// </summary>
		public int Id
		{ 
            get 
            {
                return _addressId;
            }
        }
		
        /// <summary>
        /// The Dialable number for the address. This will never be blank.
        /// </summary>
		public string Address
		{ 
            get 
            {
                if (_name == null)
                {
                    _name = _addrCaps.DialableAddress.Trim();
                    if (_name.Length == 0)
                        _name = string.Format(CultureInfo.CurrentCulture, "address {0}", Id);
                }
                return _name;
            }
        }

        /// <summary>
        /// Returns the <see>AddressCapabilities</see> capabilities structure.
        /// </summary>
        public AddressCapabilities Capabilities
        {
            get { return _addrCaps; }
        }

        /// <summary>
        /// Returns the <see>AddressStatus</see> status structure.
        /// </summary>
        public AddressStatus Status
        {
            get { return _addrStatus; }
        }

        /// <summary>
        /// Returns the list of active calls on the address.
        /// </summary>
        public TapiCall[] Calls
        {
            get
            {
                lock (_calls)
                {
                    return _calls.ToArray();
                }
            }
        }

        /// <summary>
        /// This gets or sets the number of rings that must occur before an incoming call is answered.  This can be used to implement a "toll-saver" style application.
        /// </summary>
        public int AnswerRingCount
        {
            get
            {
                int numRings;
                int rc = NativeMethods.lineGetNumRings(Line.Handle, _addressId, out numRings);
                return (rc == 0) ? numRings : 0;
            }

            set
            {
                NativeMethods.lineSetNumRings(Line.Handle, _addressId, value);
            }
        }

        /// <summary>
        /// This locates all the matching calls based on call state.
        /// </summary>
        /// <param name="requestedCallstates">Callstate desired</param>
        /// <returns>TapiCall array</returns>
        public TapiCall[] FindCallsByCallState(CallState requestedCallstates)
        {
            var calls = new List<TapiCall>();
            lock (_calls)
            {
                foreach (TapiCall call in _calls)
                {
                    if ((call.CallState & requestedCallstates) != 0)
                        calls.Add(call);
                }
            }
            return calls.ToArray();
        }

        internal void ClearCalls()
        {
            TapiCall[] calls = Calls;
            lock (_calls)
            {
                _calls.Clear();
            }
            foreach (TapiCall call in calls)
                call.ForceClose();
        }

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

            int rc = NativeMethods.lineDevSpecific(Line.Handle, _addressId, IntPtr.Zero, ip, inData.Length);
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

        #region lineDevSpecificFeature
        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="featureCode">Numeric feature code to execute</param>
        /// <param name="inData">Input data</param>
        /// <returns>Output data</returns>
        public byte[] DeviceSpecific(int featureCode, byte[] inData)
        {
            IAsyncResult ar = BeginDeviceSpecific(featureCode, inData, null, null);
            return EndDeviceSpecific(ar);
        }

        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="featureCode">Numeric feature code to execute</param>
        /// <param name="inData">Input data</param>
        /// <param name="acb">Callback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginDeviceSpecific(int featureCode, byte[] inData, AsyncCallback acb, object state)
        {
            if (inData == null)
                throw new ArgumentNullException("inData");

            IntPtr ip = Marshal.AllocHGlobal(inData.Length);
            Marshal.Copy(inData, 0, ip, inData.Length);

            int rc = NativeMethods.lineDevSpecificFeature(Line.Handle, featureCode, ip, inData.Length);
            if (rc < 0)
            {
                Marshal.FreeHGlobal(ip);
                throw new TapiException("lineDevSpecificFeature failed", rc);
            }

            return Line.TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state, ip, inData.Length));
        }
        #endregion

        #region lineForward
        /// <summary>
        /// This forwards calls destined for this address according to the specified forwarding instructions. 
        /// Any specified incoming calls for that address are deflected to the other number by the switch. 
        /// This function provides a combination of forward and do-not-disturb features.
        /// </summary>
        /// <param name="forwardInstructions">The forwarding instructions to apply</param>
        /// <param name="numRingsNoAnswer">Number of rings before a call is considered a "no answer." If dwNumRingsNoAnswer is out of range, the actual value is set to the nearest value in the allowable range.</param>
        /// <param name="param">Optional call parameters - only used if a consultation call is returned; otherwise ignored.  May be null for default parameters</param>
        public TapiCall Forward(ForwardInfo[] forwardInstructions, int numRingsNoAnswer, MakeCallParams param)
        {
            if (!Line.IsOpen)
                throw new TapiException("Line is not open", NativeMethods.LINEERR_OPERATIONUNAVAIL);

            IntPtr lpCp = IntPtr.Zero;
            IntPtr fwdList = ForwardInfo.ProcessForwardList(forwardInstructions);
            try
            {
                lpCp = MakeCallParams.ProcessCallParams(_addressId, param, 0);
                IntPtr hCall;

                int rc = NativeMethods.lineForward(Line.Handle, 0, _addressId, fwdList, numRingsNoAnswer, out hCall, lpCp);
                if (rc < 0)
                    throw new TapiException("lineForward failed", rc);
                else
                {
                    // Wait for the LINE_REPLY so we don't need to deal with the value type 
                    // issues of IntPtr being filled in async.
                    var req = new PendingTapiRequest(rc, null, null);
                    Line.TapiManager.AddAsyncRequest(req);
                    req.AsyncWaitHandle.WaitOne();
                    if (req.Result < 0)
                        throw new TapiException("lineForward failed", req.Result);

                    if (hCall != IntPtr.Zero)
                    {
                        var call = new TapiCall(this, hCall);
                        AddCall(call);
                        return call;
                    }
                }
            }
            finally
            {
                if (lpCp != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpCp);
                if (fwdList != IntPtr.Zero)
                    Marshal.FreeHGlobal(fwdList);
            }

            return null;
        }

        /// <summary>
        /// This cancels any forwarding request that is currently in effect on this address
        /// </summary>
        public void CancelForward()
        {
            IntPtr hCall;
            int rc = NativeMethods.lineForward(Line.Handle, 0, _addressId, IntPtr.Zero, 0, out hCall, IntPtr.Zero);
            if (rc < 0)
                throw new TapiException("lineForward failed", rc);
            
            // Wait for the LINE_REPLY so we don't need to deal with the value type 
            // issues of IntPtr being filled in async.
            var req = new PendingTapiRequest(rc, null, null);
            Line.TapiManager.AddAsyncRequest(req);
            if (req.AsyncWaitHandle.WaitOne(1000, true))
            {
                if (req.Result < 0)
                    throw new TapiException("lineForward failed", req.Result);
                if (hCall != IntPtr.Zero)
                    NativeMethods.lineDeallocateCall(hCall);
            }
        }
        #endregion

        #region lineMakeCall
        /// <summary>
        /// Places a new call on the address
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <returns>New <see>TapiCall</see> object.</returns>
        public TapiCall MakeCall(string address)
        {
            return MakeCall(address, 0, null);
        }

        /// <summary>
        /// Places a new call on the address
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <param name="countryCode">Country code</param>
        /// <param name="param">Optional <see>MakeCallParams</see> to use while dialing</param>
        /// <returns>New <see cref="TapiCall"/> object.</returns>
        public TapiCall MakeCall(string address, int countryCode, MakeCallParams param)
        {
            if (!Line.IsOpen)
                throw new TapiException("Line is not open", NativeMethods.LINEERR_OPERATIONUNAVAIL);

            IntPtr lpCp = IntPtr.Zero;
            try
            {
                lpCp = MakeCallParams.ProcessCallParams(_addressId, param, 0);
                IntPtr hCall;
                int rc = NativeMethods.lineMakeCall(Line.Handle, out hCall, address, countryCode, lpCp);
                if (rc < 0)
                {
                    throw new TapiException("lineMakeCall failed", rc);
                }
                else
                {
                    // Wait for the LINE_REPLY so we don't need to deal with the value type 
                    // issues of IntPtr being filled in async.
                    var req = new PendingTapiRequest(rc, null, null);
                    Line.TapiManager.AddAsyncRequest(req);
                    req.AsyncWaitHandle.WaitOne();
                    if (req.Result < 0)
                        throw new TapiException("lineMakeCall failed", req.Result);

                    var call = new TapiCall(this, hCall);
                    AddCall(call);
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

        #region linePickup
        /// <summary>
        /// This picks up a call alerting at the specified destination address and returns a call handle for the picked-up call. 
        /// If invoked with null for the alertingAddress parameter, a group pickup is performed. If required by the device, groupId specifies the 
        /// group identifier to which the alerting station belongs.
        /// </summary>
        /// <param name="alertingAddress">Address to retrieve call from</param>
        /// <param name="groupId">Optional group ID, can be null or empty</param>
        /// <returns>New <see cref="TapiCall"/> object.</returns>
        public TapiCall Pickup(string alertingAddress, string groupId)
        {
            IntPtr hCall;
            int rc = NativeMethods.linePickup(Line.Handle, _addressId, out hCall, alertingAddress, groupId);
            if (rc < 0)
                throw new TapiException("linePickup failed", rc);

            // Wait for the LINE_REPLY .. same reason as lineMakeCall..
            var req = new PendingTapiRequest(rc, null, null);
            Line.TapiManager.AddAsyncRequest(req);
            req.AsyncWaitHandle.WaitOne();
            if (req.Result < 0)
                throw new TapiException("linePickup failed", req.Result);

            var call = new TapiCall(this, hCall);
            lock (_calls)
            {
                _calls.Add(call);
            }
            return call;
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
                lpCp = MakeCallParams.ProcessCallParams(Id, mcp, callFlags);
                IntPtr hCall, hConfCall;
                int rc = NativeMethods.lineSetupConference(new HTCALL(), Line.Handle, out hConfCall, out hCall, conferenceCount, lpCp);
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
                        consultCall = new TapiCall(this, hCall);
                        AddCall(consultCall);
                    }
                    else
                        consultCall = null;

                    var confCall = new TapiCall(this, hConfCall);
                    AddCall(confCall);

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


        #region lineUnpark
        /// <summary>
        /// This retrieves a call off a parked address
        /// </summary>
        /// <param name="parkedAddress">Address to retrieve call from</param>
        /// <returns>New <see cref="TapiCall"/> object.</returns>
        public TapiCall Unpark(string parkedAddress)
        {
            IntPtr hCall;
            int rc = NativeMethods.lineUnpark(Line.Handle, _addressId, out hCall, parkedAddress);
            if (rc < 0)
                throw new TapiException("lineUnpark failed", rc);

            // Wait for the LINE_REPLY .. same reason as lineMakeCall..
            var req = new PendingTapiRequest(rc, null, null);
            Line.TapiManager.AddAsyncRequest(req);
            req.AsyncWaitHandle.WaitOne();
            if (req.Result < 0)
                throw new TapiException("lineUnpark failed", req.Result);

            var call = new TapiCall(this, hCall);
            lock (_calls)
            {
                _calls.Add(call);
            }
            return call;
        }
        #endregion

        /// <summary>
        /// This returns a device identifier for the specified device class associated with the call
        /// </summary>
        /// <param name="deviceClass">Device Class</param>
        /// <returns>string or byte[]</returns>
        public object GetExternalDeviceInfo(string deviceClass)
        {
            var vs = new VARSTRING();
            object retValue = null;
            int rc, neededSize = Marshal.SizeOf(vs) + 100;
            do
            {
                vs.dwTotalSize = neededSize;
                IntPtr lpVs = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(vs, lpVs, true);
                rc = NativeMethods.lineGetID(Line.Handle, Id, null, NativeMethods.LINECALLSELECT_ADDRESS, lpVs, deviceClass);
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

        private void GatherAddressCaps()
        {
            var lac = new LINEADDRESSCAPS();
            byte[] rawBuffer = null;

            int rc, neededSize = 1024;
            do
            {
                lac.dwTotalSize = neededSize;
                IntPtr pLac = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lac, pLac, true);
                rc = NativeMethods.lineGetAddressCaps(_lineOwner.TapiManager.LineHandle, _lineOwner.Id, _addressId, (int) _lineOwner.NegotiatedVersion, 0, pLac);
                Marshal.PtrToStructure(pLac, lac);
                if (lac.dwNeededSize > neededSize)
                {
                    neededSize = lac.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    rawBuffer = new byte[lac.dwUsedSize];
                    Marshal.Copy(pLac, rawBuffer, 0, lac.dwUsedSize);
                }
                Marshal.FreeHGlobal(pLac);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
            _addrCaps = new AddressCapabilities(lac, rawBuffer, Line.StringFormat);
        }

        internal void OnAddressStateChange(int changedState)
        {
            GatherAddressStatus();
            if (Changed != null)
                Changed(this, new AddressInfoChangeEventArgs(this, (AddressInfoChangeTypes)changedState));
        }

        internal void CheckForExistingCalls()
        {
            var lcl = new LINECALLLIST();
            int rc, neededSize = 1024;
            do
            {
                lcl.dwTotalSize = neededSize;
                IntPtr pLcl = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lcl, pLcl, true);
                
                // Get any existing calls on the address..
                rc = NativeMethods.lineGetNewCalls(Line.Handle, Id, NativeMethods.LINECALLSELECT_ADDRESS, pLcl);
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
                            hCall = (IntPtr) BitConverter.ToInt32(rawBuffer, lcl.dwCallsOffset + (i * IntPtr.Size));
                        else
                            hCall = (IntPtr) BitConverter.ToInt64(rawBuffer, lcl.dwCallsOffset + (i * IntPtr.Size));
                        AddCall(new TapiCall(this, hCall));
                    }
                }
                Marshal.FreeHGlobal(pLcl);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
        }

        internal void AddCall(TapiCall call)
        {
            lock (_calls)
            {
                _calls.Add(call);
            }
        }

        internal void RemoveCall(TapiCall call)
        {
            lock (_calls)
            {
                _calls.Remove(call);
            }
        }

        internal void GatherAddressStatus()
        {
            var las = new LINEADDRESSSTATUS();
            byte[] rawBuffer = null;

            int rc, neededSize = 1024;
            do
            {
                las.dwTotalSize = neededSize;
                IntPtr pLas = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(las, pLas, true);
                rc = NativeMethods.lineGetAddressStatus(Line.Handle, _addressId, pLas);
                Marshal.PtrToStructure(pLas, las);
                if (las.dwNeededSize > neededSize)
                {
                    neededSize = las.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    rawBuffer = new byte[las.dwUsedSize];
                    Marshal.Copy(pLas, rawBuffer, 0, las.dwUsedSize);
                }
                Marshal.FreeHGlobal(pLas);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
            _addrStatus = new AddressStatus(las, rawBuffer, Line.StringFormat);
        }

        /// <summary>
        /// Returns a System.String that represents the Address object.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return Address;
        }

        internal void OnCallStateChange(CallStateEventArgs e)
        {
            if (CallStateChanged != null)
            {
                CallStateChanged(this, e);
            }
        }

        internal void OnCallInfoChange(CallInfoChangeEventArgs e)
        {
            if (CallInfoChanged != null)
                CallInfoChanged(this, e);
        }

        private void HandleNewCall(object sender, NewCallEventArgs e)
        {
            AddCall(e.Call);
            if (NewCall != null)
                NewCall(this, e);
        }
	}
}
