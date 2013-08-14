// TapiLine.cs
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
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Reflection;
using JulMar.Atapi.Interop;
using System.Globalization;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class contains the capabilities for a single <see cref="TapiLine"/> object.
    /// </summary>
    public sealed class LineCapabilities : IFormattable
    {
        readonly LINEDEVCAPS _ldc;
        readonly byte[] _rawBuffer;
        readonly SpecialDialingChars _dialChars;
        readonly string[] _devClasses;

        /// <summary>
        /// Constructor
        /// </summary>
        internal LineCapabilities(LINEDEVCAPS ldc, byte[] buff)
        {
            _ldc = ldc;
            _rawBuffer = buff;

            _dialChars = 0;
            if ((_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_DIALBILLING) > 0)
                _dialChars |= SpecialDialingChars.Billing;
            if ((_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_DIALQUIET) > 0)
                _dialChars |= SpecialDialingChars.Quiet;
            if ((_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_DIALDIALTONE) > 0)
                _dialChars |= SpecialDialingChars.Dialtone;

            if (_ldc.dwDeviceClassesSize > 0)
            {
                var arr = new List<String>(); int spos = _ldc.dwDeviceClassesOffset;
                for (int epos = _ldc.dwDeviceClassesOffset; epos < _ldc.dwDeviceClassesOffset + _ldc.dwDeviceClassesSize - 2; epos++)
                {
                    char ch = BitConverter.ToChar(_rawBuffer, epos);
                    if (ch == '\0')
                    {
                        string s = NativeMethods.GetString(_rawBuffer, spos, epos - spos, _ldc.dwStringFormat);
                        if (s.Length > 0)
                            arr.Add(s);
                        spos = epos + 1;
                        epos++;
                    }
                }
                _devClasses = arr.ToArray();
            }
            else
                _devClasses = new string[0];
        }

        /// <summary>
        /// This provides data about the provider hardware and/or software, such as the vendor name and version numbers of hardware and software.
        /// </summary>
        public string ProviderInfo
        {
            get { return NativeMethods.GetString(_rawBuffer, _ldc.dwProviderInfoOffset, _ldc.dwProviderInfoSize, _ldc.dwStringFormat); }
        }

        /// <summary>
        /// This provides data about the switch to which the line device is connected, such as the switch manufacturer, the model name, the software version, etc.
        /// </summary>
        public string SwitchInfo
        {
            get { return NativeMethods.GetString(_rawBuffer, _ldc.dwSwitchInfoOffset, _ldc.dwSwitchInfoSize, _ldc.dwStringFormat); }
        }

        /// <summary>
        /// Permanent identifier by which the line device is known in the system's configuration.
        /// </summary>
        public int PermanentLineId
        {
            get { return _ldc.dwPermanentLineID; }
        }

        /// <summary>
        /// This returns whether the line supports traditional voice calls.
        /// </summary>
        public bool SupportsVoiceCalls
        {
            get
            {
                return ((BearerModes & BearerModes.Voice) > 0 &&
                    (MediaModes & MediaModes.InteractiveVoice) > 0);
            }
        }

        /// <summary>
        /// Maximum data rate for information exchange over the call, in bits per second.
        /// </summary>
        public int MaxDataRate
        {
            get { return _ldc.dwMaxRate; }
        }

        /// <summary>
        /// Flag array that indicates the different <see>MediaModes</see> types the address is able to support.
        /// </summary>
        public MediaModes MediaModes
        {
            get { return (MediaModes)_ldc.dwMediaModes; }
        }

        /// <summary>
        /// <see>ToneModes</see> that can be generated on this line.
        /// </summary>
        public ToneModes GenerateToneModes
        {
            get { return (ToneModes)_ldc.dwGenerateToneModes; }
        }

        /// <summary>
        /// Maximum number of frequencies that can be specified when generating a tone using GenerateTone. 
        /// A value of 0 indicates that tone generation is not available. 
        /// </summary>
        public int MaxGenerateToneModeFrequencies
        {
            get { return _ldc.dwGenerateToneMaxNumFreq; }
        }

        /// <summary>
        /// Maximum number of frequencies that can be specified in describing a general tone using the MonitorTone array
        /// </summary>
        public int MaxMonitoredToneFrequencies
        {
            get { return _ldc.dwMonitorToneMaxNumFreq; }
        }

        /// <summary>
        /// Maximum number of entries that can be specified in a tone list to <see cref="TapiCall.BeginMonitoringTones"/>.
        /// </summary>
        public int MaxMonitoredTones
        {
            get { return _ldc.dwMonitorToneMaxNumEntries; }
        }

        /// <summary>
        /// Minimum value that can be specified for both the first digit and inter-digit timeout values in milliseconds.
        /// </summary>
        public int MinGatherDigitsTimeout
        {
            get { return _ldc.dwGatherDigitsMinTimeout; }
        }

        /// <summary>
        /// Maximum value that can be specified for both the first digit and inter-digit timeout values in milliseconds.
        /// </summary>
        public int MaxGatherDigitsTimeout
        {
            get { return _ldc.dwGatherDigitsMaxTimeout; }
        }

        /// <summary>
        /// Digit modes than can be detected on this line.
        /// </summary>
        public DigitModes AvailableMonitorDigitModes
        {
            get { return (DigitModes)_ldc.dwMonitorDigitModes; }
        }

        /// <summary>
        /// Flag array that indicates the different <see>BearerModes</see> that the address is able to support
        /// </summary>
        public BearerModes BearerModes
        {
            get { return (BearerModes)_ldc.dwBearerModes; }
        }

        /// <summary>
        /// Name for the line device. This name can be configured by the user when configuring the 
        /// line device's service provider, and is provided for the user's convenience
        /// </summary>
        public string LineName
        {
            get { return NativeMethods.GetString(_rawBuffer, _ldc.dwLineNameOffset, _ldc.dwLineNameSize, _ldc.dwStringFormat); }
        }

        /// <summary>
        /// Maximum number of (minimum bandwidth) calls that can be active (connected) on the line at any one time. 
        /// The actual number of active calls may be lower if higher bandwidth calls have been established on the line. 
        /// </summary>
        public int MaxActiveCallCount
        {
            get { return _ldc.dwMaxNumActiveCalls; }
        }

        /// <summary>
        /// Number of different ring modes that can be reported in the <see cref="TapiLine.Ringing"/> event. 
        /// Different ring modes range from one to RingModeCount. Zero indicates no ring. 
        /// </summary>
        public int RingModeCount
        {
            get { return _ldc.dwRingModes; }
        }

        /// <summary>
        /// Effect on the active call when answering another offering call on a line device.
        /// </summary>
        public AnswerModes AnswerMode
        {
            get { return (AnswerModes) _ldc.dwAnswerMode; }
        }

        /// <summary>
        /// Specifies whether calls on different addresses on this line can be conferenced.
        /// </summary>
        public bool SupportsCrossAddressConferencing
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_CROSSADDRCONF) > 0; }
        }

        /// <summary>
        /// Specifies whether high-level compatibility information elements are supported on this line.
        /// </summary>
        public bool SupportsHighLevelInfoElements
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_HIGHLEVCOMP) > 0; }
        }

        /// <summary>
        /// Specifies whether low-level compatibility information elements are supported on this line. 
        /// </summary>
        public bool SupportsLowLevelInfoElements
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_LOWLEVCOMP) > 0; }
        }

        /// <summary>
        /// Specifies whether media-control operations are available for calls at this line. 
        /// </summary>
        public bool SupportsMediaControl
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_MEDIACONTROL) > 0; }
        }

        /// <summary>
        /// Specifies whether <see cref="TapiAddress.MakeCall(string)"/> is able to deal with multiple addresses at once (inverse multiplexing). 
        /// </summary>
        public bool SupportsMultipleAddresses
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_MULTIPLEADDR) > 0; }
        }

        /// <summary>
        /// Specifies what happens when an open line is closed while the application has calls active on the line. If true, the service provider 
        /// clears all active calls on the line when the last application that has opened the line closes it. If false, the service provider does 
        /// not drop active calls in such cases. Instead, the calls remain active and under control of external devices. A service provider typically 
        /// sets this to false if there is some other device that can keep the call alive, for example, if an analog line has the computer and 
        /// phone set both connect directly to them in a party-line configuration, the offhook phone will automatically keep the call active even 
        /// after the computer powers down. Applications should check this flag to determine whether to warn the user (with an OK/Cancel dialog box) 
        /// that active calls will be lost.
        /// </summary>
        public bool DropsActiveCallsOnClose
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_CLOSEDROP) > 0; }
        }

        /// <summary>
        /// Indicates whether a Media Service Provider (MSP) is associated with the line.
        /// </summary>
        public bool HasMediaServiceProvider
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_MSP) > 0; }
        }

        /// <summary>
        /// Indicates whether call hubs are supported on this line.
        /// </summary>
        public bool SupportsCallHubs
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_CALLHUB) > 0; }
        }

        /// <summary>
        /// Indicates whether call hub tracking is supported on this line. 
        /// </summary>
        public bool SupportsCallHubTracking
        {
            get { return (_ldc.dwDevCapFlags & NativeMethods.LINEDEVCAPFLAGS_CALLHUBTRACKING) > 0; }
        }

        /// <summary>
        /// Determines whether forwarding of all addresses can be used on the line.
        /// </summary>
        public bool SupportsForwarding
        {
            get { return (_ldc.dwLineFeatures & NativeMethods.LINEFEATURE_FORWARD) > 0; }
        }

        /// <summary>
        /// An outgoing call can be placed on this line using an unspecified address.
        /// </summary>
        public bool SupportsMakeCall
        {
            get { return (_ldc.dwLineFeatures & NativeMethods.LINEFEATURE_MAKECALL) > 0; }
        }

        /// <summary>
        /// Media control can be set on this line. 
        /// </summary>
        public bool SupportsSetMediaControl
        {
            get { return (_ldc.dwLineFeatures & NativeMethods.LINEFEATURE_SETMEDIACONTROL) > 0; }
        }

        /// <summary>
        /// Terminal modes for this line can be set. 
        /// </summary>
        public bool SupportsSetTerminal
        {
            get { return (_ldc.dwLineFeatures & NativeMethods.LINEFEATURE_SETTERMINAL) > 0; }
        }

        /// <summary>
        /// Device status may be set on this line.
        /// </summary>
        public bool SupportsSetDeviceStatus
        {
            get { return (_ldc.dwLineFeatures & NativeMethods.LINEFEATURE_SETDEVSTATUS) > 0; }
        }

        /// <summary>
        /// Returns the supported dialing characters which may be used when placing calls.
        /// </summary>
        public SpecialDialingChars SupportedDialingChars
        {
            get { return _dialChars; }
        }

        /// <summary>
        /// Maximum size of user-user information, including the null terminator, that can be sent during a call accept.
        /// </summary>
        public int MaxUUIAcceptSize
        {
            get { return _ldc.dwUUIAcceptSize; }
        }

        /// <summary>
        /// Maximum size of user-user information, including the null terminator, that can be sent during a call answer.
        /// </summary>
        public int MaxUUIAnswerSize
        {
            get { return _ldc.dwUUIAnswerSize; }
        }

        /// <summary>
        /// Maximum size of user-user information, including the null terminator, that can be sent during a makecall method.
        /// </summary>
        public int MaxUUIMakeCallSize
        {
            get { return _ldc.dwUUIMakeCallSize; }
        }

        /// <summary>
        /// Maximum size of user-user information, including the null terminator, that can be sent during a call drop.
        /// </summary>
        public int MaxUUIDropSize
        {
            get { return _ldc.dwUUIDropSize; }
        }

        /// <summary>
        /// Maximum size of user-user information, including the null terminator, that can be sent separately any time during a call with <see cref="TapiCall.SendUserUserInfo"/>.
        /// </summary>
        public int MaxUUISendSize
        {
            get { return _ldc.dwUUISendUserUserInfoSize; }
        }

        /// <summary>
        /// Returns the Device Specific data for this line
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get
            {
                var data = new byte[_ldc.dwDevSpecificSize];
                Array.Copy(_rawBuffer, _ldc.dwDevSpecificOffset, data, 0, _ldc.dwDevSpecificSize);
                return data;
            }
        }

        /// <summary>
        /// GUID permanently associated with the line device.
        /// </summary>
        public Guid Guid
        {
            get
            {
                return _ldc.PermanentLineGuid;
            }
        }

        /// <summary>
        /// Array of device class identifiers supported on one or more addresses on this line.
        /// </summary>
        public string[] AvailableDeviceClasses
        {
            get { return (string[]) _devClasses.Clone(); }
        }

        /// <summary>
        /// Returns a System.String that represents this object.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// Formattable ToString
        /// </summary>
        /// <param name="format">Format</param>
        /// <returns>String</returns>
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        /// <summary>
        /// This implementation provides a customizable formatter
        /// </summary>
        /// <param name="format">Format</param>
        /// <param name="formatProvider">Format provider (not used)</param>
        /// <returns>String</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            Type t = GetType();

            if (format == "f")
            {
                var sb = new StringBuilder();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    ParameterInfo[] pinfo = pi.GetIndexParameters();
                    if (pinfo.Length == 0)
                    {
                        object value;
                        try { value = pi.GetValue(this, null); } catch { value = null; }
                        if (value is Array)
                        {
                            sb.AppendFormat("{0}: ", pi.Name);
                            bool needsComma = false;
                            foreach (object o in (Array)value)
                            {
                                if (needsComma)
                                    sb.Append(",");
                                else
                                    needsComma = true;
                                sb.Append(o);
                            }
                            sb.Append("\n");
                        }
                        else
                        {
                            if (value is Int32 || value is Int64)
                                sb.AppendFormat("{0}: 0x{1:X}\n", pi.Name, value);
                            else
                                sb.AppendFormat("{0}: {1}\n", pi.Name, value);
                        }
                    }
                }
                return sb.ToString();
            }

            return t.Name;
        }
    }

    /// <summary>
    /// This class wraps the status for a given <see cref="TapiLine"/> object.
    /// </summary>
    public sealed class LineStatus
    {
        readonly TapiLine _lineOwner;
        readonly LINEDEVSTATUS _lds;
        readonly byte[] _rawBuffer;

        internal LineStatus(TapiLine owner, LINEDEVSTATUS lds, byte[] buffer)
        {
            _lineOwner = owner;
            _lds = lds;
            _rawBuffer = buffer;
        }

        /// <summary>
        /// Number of active opens on the line device.
        /// </summary>
        public int OpenCount
        {
            get { return _lds.dwNumOpens; }
        }

        /// <summary>
        /// Value that specifies a bit array that indicates for which <see cref="MediaModes"/> the line device is currently open. 
        /// </summary>
        public MediaModes OpenMediaModes
        {
            get { return (MediaModes) _lds.dwOpenMediaModes; }
        }

        /// <summary>
        /// Number of calls on the line in call states other than idle, onhold, onholdpendingtransfer, and onholdpendingconference.
        /// </summary>
        public int ActiveCallCount
        {
            get { return _lds.dwNumActiveCalls; }
        }

        /// <summary>
        /// Number of calls on the line in the onhold state.
        /// </summary>
        public int OnHoldCount
        {
            get { return _lds.dwNumOnHoldCalls; }
        }

        /// <summary>
        /// Number of calls on the line in the onholdpendingtransfer or onholdpendingconference state.
        /// </summary>
        public int OnHoldPendingTransferCount
        {
            get { return _lds.dwNumOnHoldPendCalls; }
        }

        /// <summary>
        /// Number of outstanding call completion requests on the line.
        /// </summary>
        public int CallCompletionCount
        {
            get { return _lds.dwNumCallCompletions; }
        }

        /// <summary>
        /// Current ring mode on the line device. 
        /// </summary>
        public int CurrentRingMode
        {
            get { return _lds.dwRingMode; }
        }

        /// <summary>
        /// Current signal level of the connection on the line. This is a value in the range 0x00000000 (weakest signal) to 0x0000FFFF (strongest signal).
        /// </summary>
        public int SignalLevel
        {
            get { return _lds.dwSignalLevel; }
        }

        /// <summary>
        /// Current battery level of the line device hardware. This is a value in the range 0x00000000 (battery empty) to 0x0000FFFF (battery full). 
        /// </summary>
        public int BatteryLevel
        {
            get { return _lds.dwBatteryLevel; }
        }

        /// <summary>
        /// Specifies the current <see cref="RoamingModes"/> of the line device.
        /// </summary>
        public RoamingModes RoamMode
        {
            get { return (RoamingModes)_lds.dwRoamMode; }
        }

        /// <summary>
        /// Forwarding of all addresses can be used on the line.
        /// </summary>
        public bool CanForward
        {
            get { return (_lds.dwLineFeatures & NativeMethods.LINEFEATURE_FORWARD) > 0; }
        }

        /// <summary>
        /// An outgoing call can be placed on this line using an unspecified address.
        /// </summary>
        public bool CanMakeCall
        {
            get { return (_lds.dwLineFeatures & NativeMethods.LINEFEATURE_MAKECALL) > 0; }
        }

        /// <summary>
        /// Media control can be set on this line.
        /// </summary>
        public bool CanSetMediaControl
        {
            get { return (_lds.dwLineFeatures & NativeMethods.LINEFEATURE_SETMEDIACONTROL) > 0; }
        }

        /// <summary>
        /// Terminal modes for this line can be set.
        /// </summary>
        public bool CanSetTerminal
        {
            get { return (_lds.dwLineFeatures & NativeMethods.LINEFEATURE_SETTERMINAL) > 0; }
        }

        /// <summary>
        /// Device status may be set on the line.
        /// </summary>
        public bool CanSetDeviceStatus
        {
            get { return (_lds.dwLineFeatures & NativeMethods.LINEFEATURE_SETDEVSTATUS) > 0; }
        }

        /// <summary>
        /// Indicates whether the message waiting lamp is turned on.
        /// </summary>
        public bool MessageWaitingLampState
        {
            get { return (_lds.dwDevStatusFlags & NativeMethods.LINEDEVSTATUSFLAGS_MSGWAIT) > 0; }
            set { _lineOwner.SetDeviceState(NativeMethods.LINEDEVSTATUSFLAGS_MSGWAIT, value); }
        }

        /// <summary>
        /// Indicates whether the line is in service. If true, the line is in service. If false, the line is out of service.
        /// </summary>
        public bool InService
        {
            get { return (_lds.dwDevStatusFlags & NativeMethods.LINEDEVSTATUSFLAGS_INSERVICE) > 0; }
            set { _lineOwner.SetDeviceState(NativeMethods.LINEDEVSTATUSFLAGS_INSERVICE, value); }
        }

        /// <summary>
        /// Indicates whether the line is locked or unlocked. This bit is most often used with line devices associated with cellular phones. 
        /// Many cellular phones have a security mechanism that requires the entry of a password to enable the phone to place calls. This bit 
        /// can be used to indicate to applications that the phone is locked and cannot place calls until the password is entered on the user 
        /// interface of the phone so that the application can present an appropriate alert to the user. 
        /// </summary>
        public bool Locked
        {
            get { return (_lds.dwDevStatusFlags & NativeMethods.LINEDEVSTATUSFLAGS_LOCKED) > 0; }
            set { _lineOwner.SetDeviceState(NativeMethods.LINEDEVSTATUSFLAGS_LOCKED, value); }
        }

        /// <summary>
        /// Specifies whether the line is connected to TAPI. If true, the line is connected and TAPI is able to operate on the line device. 
        /// If false, the line is disconnected and the application is unable to control the line device through TAPI.
        /// </summary>
        public bool Connected
        {
            get { return (_lds.dwDevStatusFlags & NativeMethods.LINEDEVSTATUSFLAGS_CONNECTED) > 0;}
            set { _lineOwner.SetDeviceState(NativeMethods.LINEDEVSTATUSFLAGS_CONNECTED, value); }
        }

        /// <summary>
        /// Returns the Device Specific data for this line
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get
            {
                var data = new byte[_lds.dwDevSpecificSize];
                Array.Copy(_rawBuffer, _lds.dwDevSpecificOffset, data, 0, _lds.dwDevSpecificSize);
                return data;
            }
        }

        /// <summary>
        /// Indicates the media types that can be invoked on new calls created on this line device.
        /// </summary>
        public MediaModes AvailableMediaModes
        {
            get { return (MediaModes) _lds.dwAvailableMediaModes; }
        }

        /// <summary>
        /// Returns a System.String that represents this object.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// Formattable ToString
        /// </summary>
        /// <param name="format">Format</param>
        /// <returns>String</returns>
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        /// <summary>
        /// This implementation provides a customizable formatter
        /// </summary>
        /// <param name="format">Format</param>
        /// <param name="formatProvider">Format provider (not used)</param>
        /// <returns>String</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            Type t = GetType();

            if (format == "f")
            {
                var sb = new StringBuilder();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    ParameterInfo[] pinfo = pi.GetIndexParameters();
                    if (pinfo.Length == 0)
                    {
                        object value;
                        try { value = pi.GetValue(this, null); }
                        catch { value = null; }
                        if (value is Array)
                        {
                            sb.AppendFormat("{0}: ", pi.Name);
                            bool needsComma = false;
                            foreach (object o in (Array)value)
                            {
                                if (needsComma)
                                    sb.Append(",");
                                else
                                    needsComma = true;
                                sb.Append(o);
                            }
                            sb.Append("\n");
                        }
                        else
                        {
                            if (value is Int32 || value is Int64)
                                sb.AppendFormat("{0}: 0x{1:X}\n", pi.Name, value);
                            else
                                sb.AppendFormat("{0}: {1}\n", pi.Name, value);
                        }
                    }
                }
                return sb.ToString();
            }

            return t.Name;
        }
    }

    /// <summary>
    /// This object represents a single exposed line device from Tapi.
    /// </summary>
    public sealed class TapiLine : IDisposable
	{
        private const int MinTapiVersion = (int) TapiVersion.V13;
        private const int MaxTapiVersion = (int) TapiVersion.V31;

        private readonly TapiManager _mgr;
		private readonly int _deviceId;
		private readonly int _negotiatedVersion;
        private int _negotiatedExtVersion;
        private readonly int _stringFormat;
        private string _lineName = string.Empty;
        private HTLINE _hLine = new HTLINE();
        private LineCapabilities _props;
        private LineStatus _status;
        private readonly TapiAddress[] _addresses;
        private readonly TapiEventCallback _lcb;
        private readonly string _extensionId = "0.0.0.0";
        private EventHandler<DeviceSpecificEventArgs> _devsCallback;

        /// <summary>
        /// This event is raised when a call on this address changes state
        /// </summary>
        public event EventHandler<CallStateEventArgs> CallStateChanged;
        /// <summary>
        /// This event is raised when the information associated with a call changes
        /// </summary>
        public event EventHandler<CallInfoChangeEventArgs> CallInfoChanged;
        /// <summary>
        /// This event is raised when a new call is placed or offering on the line.
        /// </summary>
        public event EventHandler<NewCallEventArgs> NewCall;
        /// <summary>
        /// This event is raised when an address on this line changes
        /// </summary>
        public event EventHandler<AddressInfoChangeEventArgs> AddressChanged;
        /// <summary>
        /// This event is raised when the status or capabilities of the line has changed.
        /// </summary>
        public event EventHandler<LineInfoChangeEventArgs> Changed;
        /// <summary>
        /// This event is raised when the line is ringing.
        /// </summary>
        public event EventHandler<RingEventArgs> Ringing;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mgr">Owner</param>
        /// <param name="deviceId">Device ID for this line</param>
		internal TapiLine(TapiManager mgr, int deviceId)
		{
            _mgr = mgr;
			_deviceId = deviceId;
            _lcb = LineCallback;

            LINEEXTENSIONID extId;
            int rc = NativeMethods.lineNegotiateAPIVersion(_mgr.LineHandle, _deviceId, 
                        MinTapiVersion, MaxTapiVersion, out _negotiatedVersion, out extId);
            if (rc == NativeMethods.LINEERR_OK)
            {
                IsValid = true;

                GatherStatus(); // will fail

                LINEDEVCAPS ldc = GatherDevCaps();
                _stringFormat = ldc.dwStringFormat;
                _addresses = new TapiAddress[ldc.dwNumAddresses];
                for (int i = 0; i < ldc.dwNumAddresses; i++)
                {
                    _addresses[i] = new TapiAddress(this, i);
                    // Forward events from this address
                    _addresses[i].Changed += delegate(object sender, AddressInfoChangeEventArgs e) { if (AddressChanged != null) AddressChanged(this, e); };
                    _addresses[i].CallStateChanged += delegate(object sender, CallStateEventArgs e) { if (CallStateChanged != null) CallStateChanged(this, e); };
                    _addresses[i].CallInfoChanged += delegate(object sender, CallInfoChangeEventArgs e) { if (CallInfoChanged != null) CallInfoChanged(this, e); };
                }

                _extensionId = String.Format(CultureInfo.CurrentCulture, "{0}.{1}.{2}.{3}", extId.dwExtensionID0, extId.dwExtensionID1, extId.dwExtensionID2, extId.dwExtensionID3);
            }
            else
            {
                IsValid = false;
                _addresses = new TapiAddress[0];
                _props = new LineCapabilities(new LINEDEVCAPS(), null);
            }
		}

        private LINEDEVCAPS GatherDevCaps()
        {
            var ldc = new LINEDEVCAPS();
            byte[] rawBuffer = null;

            int rc, neededSize = 1024;
            do
            {
                ldc.dwTotalSize = neededSize;
                IntPtr pLdc = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(ldc, pLdc, true);
                rc = NativeMethods.lineGetDevCaps(_mgr.LineHandle, _deviceId, _negotiatedVersion, 0, pLdc);
                Marshal.PtrToStructure(pLdc, ldc);
                if (ldc.dwNeededSize > neededSize)
                {
                    neededSize = ldc.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    rawBuffer = new byte[ldc.dwUsedSize];
                    Marshal.Copy(pLdc, rawBuffer, 0, ldc.dwUsedSize);
                }
				Marshal.FreeHGlobal(pLdc);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
            _props = new LineCapabilities(ldc, rawBuffer);
            return ldc;
        }

        private void GatherStatus()
        {
            var lds = new LINEDEVSTATUS();
            byte[] rawBuffer = null;

            if (!IsOpen)
            {
                _status = new LineStatus(this, lds, null);
                return;
            }

            int rc, neededSize = 1024;
            do
            {
                lds.dwTotalSize = neededSize;
                IntPtr pLds = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lds, pLds, true);
                rc = NativeMethods.lineGetLineDevStatus(Handle, pLds);
                Marshal.PtrToStructure(pLds, lds);
                if (lds.dwNeededSize > neededSize)
                {
                    neededSize = lds.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    rawBuffer = new byte[lds.dwUsedSize];
                    Marshal.Copy(pLds, rawBuffer, 0, lds.dwUsedSize);
                }
                Marshal.FreeHGlobal(pLds);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
            _status = new LineStatus(this, lds, rawBuffer);
        }

        internal TapiManager TapiManager
        {
            get { return _mgr; }
        }

        internal HTLINE Handle
        {
            get { return _hLine; }
        }

        /// <summary>
        /// This returns the underlying HTLINE which you can use in your
        /// own interop scenarios to deal with custom methods or places
        /// which are not wrapped by ATAPI
        /// </summary>
        public SafeHandle LineHandle
        {
            get { return _hLine; }
        }

        internal int StringFormat
        {
            get { return _stringFormat; }
        }

        /// <summary>
        /// This returns whether the line device is usable or not.  Removed lines are
        /// not usable and have no capabilities or properties.
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        /// The numeric device ID representing the line.
        /// </summary>
		public int Id
		{ 
            get { return _deviceId; }
		}

        /// <summary>
        /// The permanent numeric ID representing this line
        /// </summary>
        public int PermanentId
        {
            get { return _props.PermanentLineId; }
        }

        /// <summary>
        /// The <see cref="TapiVersion"/> that this line negotiated to.
        /// </summary>
		public TapiVersion NegotiatedVersion
		{ 
            get { return (TapiVersion) _negotiatedVersion; }
		}

        /// <summary>
        /// This associates an arbitrary object with the line device
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// This method is used to negotiate extension versions for the TSP.  It is only necessary if the application intends to use device-specific extensions.
        /// </summary>
        /// <param name="minVersion">Minimum version to negotiate to</param>
        /// <param name="maxVersion">Maximum version to negotiate to</param>
        /// <param name="dsc">Callback for any device-specific notification</param>
        /// <returns>Negotiated extensions version</returns>
        public int NegotiateExtensions(int minVersion, int maxVersion, EventHandler<DeviceSpecificEventArgs> dsc)
        {
            int rc = NativeMethods.lineNegotiateExtVersion(_mgr.LineHandle, _deviceId, _negotiatedVersion, minVersion, maxVersion, out _negotiatedExtVersion);
            if (rc < 0)
            {
                throw new TapiException("lineNegotiateExtVersion failed", rc);
            }

            _devsCallback = dsc;
            
            // Must re-read dev caps
            GatherDevCaps();

            return _negotiatedExtVersion;
        }

        /// <summary>
        /// This returns the available TSP device-specific extension ID.  It is the form of a string "a.b.c.d" and will be "0.0.0.0" if no
        /// device-specific extensions are present.
        /// </summary>
        public string DeviceSpecificExtensionID
        {
            get { return _extensionId; }
        }

        /// <summary>
        /// The available addresses on this line.
        /// </summary>
        public TapiAddress[] Addresses
        {
            get { return (TapiAddress[]) _addresses.Clone(); }
        }

        /// <summary>
        /// This retrieves the total number of calls on the line.
        /// </summary>
        /// <returns>Call Count</returns>
        public int TotalCallCount
        {
            get { return _addresses.Sum(addr => addr.Calls.Length); }
        }

        /// <summary>
        /// This returns a call using the call-id.
        /// </summary>
        /// <param name="callId">Callid</param>
        /// <returns>TapiCall object</returns>
        public TapiCall GetCallById(int callId)
        {
            return _addresses.SelectMany(addr => addr.Calls).FirstOrDefault(call => call.Id == callId);
        }

        /// <summary>
        /// Returns all the calls on this line device.
        /// </summary>
        /// <returns>Array of calls</returns>
        public TapiCall[] GetCalls()
        {
            var calls = new List<TapiCall>();
            foreach (TapiAddress addr in _addresses)
            {
                calls.AddRange(addr.Calls.Where(call => !calls.Contains(call)));
            }
            return calls.ToArray();
        }

        /// <summary>
        /// This locates an address using the Dialable Number
        /// </summary>
        /// <param name="number">DN to locate</param>
        /// <returns>TapiAddress or null if not found.</returns>
        public TapiAddress FindAddress(string number)
        {
            return _addresses.FirstOrDefault(addr => string.Compare(addr.Address, number, true, CultureInfo.InvariantCulture) == 0);
        }

        /// <summary>
        /// Returns the Line Name associated with the line.  It will never be empty.
        /// </summary>
        public string Name
        {
            get 
            {
                if (_lineName.Length == 0 && _props != null)
                    _lineName = _props.LineName;
                if (_lineName.Length == 0)
                    _lineName = string.Format(CultureInfo.CurrentCulture, "Line {0}", _deviceId);
                return _lineName;
            }
        }

        /// <summary>
        /// Returns the <see cref="LineCapabilities"/> object for this line.
        /// </summary>
        public LineCapabilities Capabilities
        {
            get { return _props; }
        }

        /// <summary>
        /// Returns the <see cref="LineStatus"/> object for this line.
        /// </summary>
        public LineStatus Status
        {
            get { return _status; }
        }

        /// <summary>
        /// Returns a System.String representing this line object
        /// </summary>
        /// <returns>String</returns>
		public override string ToString()
		{
			return Name;
		}

        /// <summary>
        /// Returns true/false whether the line is currently open.
        /// </summary>
        public bool IsOpen
        {
            get { return _hLine.IsInvalid == false; }
        }

        /// <summary>
        /// This method opens the line and allows it to be used to place or receive calls.
        /// </summary>
        /// <param name="mediaModes"><see cref="MediaModes"/> which will be used by the application</param>
        public void Open(MediaModes mediaModes)
        {
            Open(Privilege.Owner, mediaModes, -1);
        }

        /// <summary>
        /// This method opens the line and allows it to be used to place or receive calls.
        /// </summary>
        /// <param name="mediaModes"><see cref="MediaModes"/> which will be used by the application</param>
        /// <param name="addressId">Address index to only monitor a single address</param>
        public void Open(MediaModes mediaModes, int addressId)
        {
            Open(Privilege.Owner, mediaModes, addressId);
        }

        /// <summary>
        /// This opens the line in non-owner (monitor) mode so that new and existing calls can be viewed but not manipulated.
        /// </summary>
        public void Monitor()
        {
            Open(Privilege.Monitor, MediaModes.None, -1);
        }

        /// <summary>
        /// Internal method to open the line
        /// </summary>
        /// <param name="openMode"></param>
        /// <param name="mediaMode"></param>
        /// <param name="addressId">Address ID to open (-1 for all)</param>
        private void Open(Privilege openMode, MediaModes mediaMode, int addressId)
		{
            if (IsOpen)
                throw new TapiException("Line is already open", NativeMethods.LINEERR_OPERATIONUNAVAIL);

            var lcp = new LINECALLPARAMS();
            lcp.dwTotalSize = Marshal.SizeOf(lcp);
            int privilege = (openMode == Privilege.None) ? NativeMethods.LINECALLPRIVILEGE_NONE :
                (openMode == Privilege.Monitor) ? NativeMethods.LINECALLPRIVILEGE_MONITOR :
                (NativeMethods.LINECALLPRIVILEGE_MONITOR | NativeMethods.LINECALLPRIVILEGE_OWNER);
            if (addressId != -1 && addressId < _addresses.Length)
            {
                lcp.dwAddressID = addressId;
                privilege |= NativeMethods.LINEOPENOPTION_SINGLEADDRESS;
            }
            mediaMode &= Capabilities.MediaModes;

            uint hLine;
            int rc = NativeMethods.lineOpen(_mgr.LineHandle, _deviceId, out hLine, _negotiatedVersion, _negotiatedExtVersion,
                Marshal.GetFunctionPointerForDelegate(_lcb), privilege, (int) mediaMode, ref lcp);

            if (rc == NativeMethods.LINEERR_OK)
            {
                // Open our handle
                _hLine = new HTLINE(hLine, true);

                // Turn on reporting for all known messages (TAPI 2.2)
                NativeMethods.lineSetStatusMessages(Handle, 0x01FFFFFF, 0x000001FF);

                // Get the current status of the line
                GatherStatus();

                // Update the state of the addresses
                foreach (TapiAddress addr in _addresses)
                {
                    addr.GatherAddressStatus();
                    addr.CheckForExistingCalls();
                }
            }
            else
                throw new TapiException("lineOpen failed", rc);
		}

        /// <summary>
        /// This closes the line device.
        /// </summary>
        public void Close()
        {
            // Close all the calls on the line -- not we don't drop them.
            foreach (TapiAddress addr in _addresses)
            {
                TapiCall[] calls = addr.Calls;
                foreach (TapiCall call in calls)
                {
                    try
                    {
                        call.Deallocate();
                    }
                    catch
                    {
                    }
                }
            }

            try
            {
                // Close the line handle
                _hLine.Close();
            }
            catch
            {
            }

            _hLine.SetHandleAsInvalid();
            GatherStatus();
        }

        #region lineMakeCall
        /// <summary>
        /// This places a call on the first available address of the line.
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <returns><see cref="TapiCall"/> object or null.</returns>
        public TapiCall MakeCall(string address)
        {
            return MakeCall(address, null, null);
        }

        /// <summary>
        /// This places a call on the first available address of the line.
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <param name="country"><see cref="Country"/> object (null for default).</param>
        /// <param name="param">Optional <see cref="MakeCallParams"/> to use when dialing.</param>
        /// <returns><see cref="TapiCall"/> object or null.</returns>
        public TapiCall MakeCall(string address, Country country, MakeCallParams param)
        {
            return (from addr in Addresses 
                    where addr.Status.CanMakeCall 
                    select addr.MakeCall(address, (country == null) ? 0 : country.CountryCode, param)
                    ).FirstOrDefault();
        }

        #endregion

        #region lineUncompleteCall
        /// <summary>
        /// Cancels the specified call completion request on the specified line
        /// </summary>
        /// <param name="completionId">Original completion id from <see cref="TapiCall.CompleteCall"/>.</param>
        public void UncompleteCall(int completionId)
        {
            IAsyncResult ar = BeginUncompleteCall(completionId, null, null);
            EndUncompleteCall(ar);
        }

        /// <summary>
        /// Cancels the specified call completion request on the specified line
        /// </summary>
        /// <param name="completionId">Original completion id from <see cref="TapiCall.CompleteCall"/>.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        public IAsyncResult BeginUncompleteCall(int completionId, AsyncCallback acb, object state)
        {
            int rc = NativeMethods.lineUncompleteCall(Handle, completionId);
            if (rc < 0)
                throw new TapiException("lineUncompleteCall failed", rc);
            return TapiManager.AddAsyncRequest(new PendingTapiRequest(rc, acb, state));
        }

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiLine.BeginUncompleteCall"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginCompleteCall</param>
        public void EndUncompleteCall(IAsyncResult ar)
        {
            if (ar == null)
                throw new ArgumentNullException("ar");

            ar.AsyncWaitHandle.WaitOne();
            var req = (PendingTapiRequest)ar;
            if (req.Result < 0)
                throw new TapiException("lineUncompleteCall failed", req.Result);
        }
        #endregion

        #region lineTranslateAddress
        /// <summary>
        /// This method translates the input number to a dialable number for this line and <see cref="LocationInformation"/>
        /// </summary>
        /// <param name="number">Number to translate</param>
        /// <param name="options">TranslationOptions</param>
        /// <returns>Dialable number</returns>
        public NumberInfo TranslateNumber(string number, TranslationOptions options)
        {
            return TranslateNumber(number, null, options);
        }

        /// <summary>
        /// This method translates the input number to a dialable number for this line and <see cref="LocationInformation"/>
        /// </summary>
        /// <param name="number">Number to translate</param>
        /// <param name="callingCard">Calling card to use for call</param>
        /// <param name="options">TranslationOptions</param>
        /// <returns>Dialable number</returns>
        public NumberInfo TranslateNumber(string number, CallingCard callingCard, TranslationOptions options)
        {
            var lto = new LINETRANSLATEOUTPUT();
            byte[] rawBuffer = null;
            int rc, neededSize = 1024;
            int bits = (int)options, card = 0;
            if (callingCard != null)
            {
                bits |= NativeMethods.LINETRANSLATEOPTION_CARDOVERRIDE;
                card = callingCard.Id;
            }

            do
            {
                lto.dwTotalSize = neededSize;
                IntPtr pLto = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lto, pLto, true);
                rc = NativeMethods.lineTranslateAddress(_mgr.LineHandle, _deviceId, (int)TapiVersion.V21, number, card, bits, pLto);
                Marshal.PtrToStructure(pLto, lto);
                if (lto.dwNeededSize > neededSize)
                {
                    neededSize = lto.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    rawBuffer = new byte[lto.dwUsedSize];
                    Marshal.Copy(pLto, rawBuffer, 0, lto.dwUsedSize);
                }
                Marshal.FreeHGlobal(pLto);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);

            if (rc == NativeMethods.LINEERR_OK)
                return new NumberInfo(lto, rawBuffer, _mgr.LocationInformation);

            throw new TapiException("lineTranslateAddress failed", rc);
        }
        #endregion

        #region lineForward
        /// <summary>
        /// This forwards calls destined for all addresses on the specified line, according to the specified forwarding instructions. 
        /// Any specified incoming calls for that address are deflected to the other number by the switch. 
        /// This function provides a combination of forward and do-not-disturb features.
        /// </summary>
        /// <param name="forwardInstructions">The forwarding instructions to apply</param>
        /// <param name="numRingsNoAnswer">Number of rings before a call is considered a "no answer." If dwNumRingsNoAnswer is out of range, the actual value is set to the nearest value in the allowable range.</param>
        /// <param name="param">Optional call parameters - only used if a consultation call is returned; otherwise ignored.  May be null for default parameters</param>
        public TapiCall Forward(ForwardInfo[] forwardInstructions, int numRingsNoAnswer, MakeCallParams param)
        {
            if (!IsOpen)
                throw new TapiException("Line is not open", NativeMethods.LINEERR_OPERATIONUNAVAIL);

            IntPtr lpCp = IntPtr.Zero;
            IntPtr fwdList = ForwardInfo.ProcessForwardList(forwardInstructions);
            try
            {
                lpCp = MakeCallParams.ProcessCallParams(0, param, 0);
                uint hCall;

                int rc = NativeMethods.lineForward(Handle, 1, 0, fwdList, numRingsNoAnswer, out hCall, lpCp);
                if (rc < 0)
                    throw new TapiException("lineForward failed", rc);
                else
                {
                    // Wait for the LINE_REPLY so we don't need to deal with the value type 
                    // issues of IntPtr being filled in async.
                    var req = new PendingTapiRequest(rc, null, null);
                    _mgr.AddAsyncRequest(req);
                    req.AsyncWaitHandle.WaitOne();
                    if (req.Result < 0)
                        throw new TapiException("lineForward failed", req.Result);

                    if (hCall != 0)
                    {
                        var call = new TapiCall(this, hCall);
                        TapiAddress addrOwner = call.Address;
                        addrOwner.AddCall(call);
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
        /// This cancels any forwarding request that is currently in effect.
        /// </summary>
        public void CancelForward()
        {
            uint hCall;
            int rc = NativeMethods.lineForward(Handle, 1, 0, IntPtr.Zero, 0, out hCall, IntPtr.Zero);
            if (rc < 0)
                throw new TapiException("lineForward failed", rc);
            
            // Wait for the LINE_REPLY so we don't need to deal with the value type 
            // issues of IntPtr being filled in async.
            var req = new PendingTapiRequest(rc, null, null);
            _mgr.AddAsyncRequest(req);
            if (req.AsyncWaitHandle.WaitOne(1000, true))
            {
                if (req.Result < 0)
                    throw new TapiException("lineForward failed", req.Result);
                if (hCall != 0)
                    NativeMethods.lineDeallocateCall(hCall);
            }
        }
        #endregion

        /// <summary>
        /// This returns a device identifier for the specified device class associated with the call
        /// </summary>
        /// <param name="deviceClass">Device Class</param>
        /// <returns>string or byte[]</returns>
        public object GetExternalDeviceInfo(string deviceClass)
        {
            if (Handle.IsInvalid)
                throw new InvalidOperationException("Line is not open");

            var vs = new VARSTRING();
            object retValue = null;
            int rc, neededSize = Marshal.SizeOf(vs) + 100;
            do
            {
                vs.dwTotalSize = neededSize;
                IntPtr lpVs = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(vs, lpVs, true);
                rc = NativeMethods.lineGetID(Handle, 0, new HTCALL(), NativeMethods.LINECALLSELECT_LINE, lpVs, deviceClass);
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
        /// Returns a device ID handle from an identifier.
        /// </summary>
        /// <param name="identifier">Identifier to lookup</param>
        /// <returns>Handle or null</returns>
        public int? GetDeviceID(string identifier)
        {
            var buffer = (byte[])GetExternalDeviceInfo(identifier);
            return buffer == null || buffer.Length == 0 ? (int?)null : BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Returns the device id for the wave input device.  This identifier may be passed to "waveInOpen" to get a HWAVE handle.
        /// </summary>
        /// <returns>Wave Device identifier</returns>
        public int? GetWaveInDeviceID()
        {
            return GetDeviceID("wave/in");
        }

        /// <summary>
        /// Returns the device id for the wave output device.  This identifier may be passed to "waveOutOpen" to get a HWAVE handle.
        /// </summary>
        /// <returns>Wave Device identifier</returns>
        public int? GetWaveOutDeviceID()
        {
            return GetDeviceID("wave/out");
        }

        /// <summary>
        /// Returns the device id for the MIDI input device.  This identifier may be passed to "midiInOpen" to get a HMIDI handle.
        /// </summary>
        /// <returns>MIDI Device identifier</returns>
        public int? GetMidiInDeviceID()
        {
            return GetDeviceID("midi/in");
        }

        /// <summary>
        /// Returns the device id for the MIDI output device.  This identifier may be passed to "midiOutOpen" to get a HMIDI handle.
        /// </summary>
        /// <returns>MIDI Device identifier</returns>
        public int? GetMidiOutDeviceID()
        {
            return GetDeviceID("midi/out");
        }

        /// <summary>
        /// Returns the phone device associated with this line.
        /// </summary>
        /// <returns></returns>
        public TapiPhone GetAssociatedPhone()
        {
            if (_mgr.Phones.Length > 0)
            {
                var buffer = (byte[])GetExternalDeviceInfo("tapi/phone");
                if (buffer != null && buffer.Length > 0)
                {
                    int deviceId = BitConverter.ToInt32(buffer, 0);
                    return _mgr.Phones.FirstOrDefault(phone => phone.Id == deviceId);
                }
            }
            return null;
        }

        /// <summary>
        /// This returns an "opaque" data structure object, the contents of which are specific to the line (service provider) and device class. 
        /// The data structure object stores the current configuration of a media-stream device associated with the line device.
        /// </summary>
        /// <param name="deviceClass">Specifies the device class of the device whose configuration is requested.</param>
        /// <returns>Opaque data block which may be passed back</returns>
        public byte[] GetDeviceConfig(string deviceClass)
        {
            var vs = new VARSTRING();
            byte[] rawBuffer = null;
            int rc, neededSize = 512;
            do
            {
                vs.dwTotalSize = neededSize;
                IntPtr pVs = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(vs, pVs, true);
                rc = NativeMethods.lineGetDevConfig(_deviceId, pVs, deviceClass);
                Marshal.PtrToStructure(pVs, vs);
                if (vs.dwNeededSize > neededSize)
                {
                    neededSize = vs.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    var buffer = new byte[vs.dwTotalSize];
                    Marshal.Copy(pVs, buffer, 0, vs.dwTotalSize);
                    rawBuffer = new byte[vs.dwStringSize];
                    Array.Copy(buffer, vs.dwStringOffset, rawBuffer, 0, vs.dwStringSize);
                }
                Marshal.FreeHGlobal(pVs);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
            
            if (rc != 0)
                throw new TapiException("lineGetDevConfig failed", rc);
            return rawBuffer;
        }

        /// <summary>
        /// This sets the line-specific device information.
        /// </summary>
        /// <param name="deviceClass">Specifies the device class of the device whose configuration is requested.</param>
        /// <param name="data">Data obtained from a previous call to <see cref="TapiLine.GetDeviceConfig"/>.</param>
        public void SetDeviceConfig(string deviceClass, byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            IntPtr vsData = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, vsData, data.Length);
            int rc = NativeMethods.lineSetDevConfig(_deviceId, vsData, data.Length, deviceClass);
            Marshal.FreeHGlobal(vsData);

            if (rc != 0)
                throw new TapiException("lineSetDevConfig failed", rc);
        }

        /// <summary>
        /// This is used to change the line device status
        /// </summary>
        /// <param name="type">LINEDEVSTATUS_xxx flag</param>
        /// <param name="value">True/False</param>
        internal void SetDeviceState(int type, bool value)
        {
            if (_status.CanSetDeviceStatus)
            {
                int rc = NativeMethods.lineSetLineDevStatus(Handle, type, value ? -1 : 0);
                if (rc != 0)
                    throw new TapiException("lineSetLineDevStatus failed", rc);
            }
        }

        #region lineConfig
        /// <summary>
        /// This method displays the line configuration dialog.
        /// </summary>
        /// <param name="hwnd">Handle to Form owner or IntPtr.Zero</param>
        /// <param name="deviceClass">Page to display or null</param>
        /// <returns>true/false</returns>
        public bool Config(IntPtr hwnd, string deviceClass)
        {
            if (deviceClass == null)
                deviceClass = string.Empty;

            int rc = NativeMethods.lineConfigDialog(_deviceId, hwnd, deviceClass);
            if (rc < 0 && rc != NativeMethods.LINEERR_OPERATIONUNAVAIL)
                throw new TapiException("lineConfigDialog failed", rc);
            return rc == 0;
        }
        #endregion

        private void LineCallback(TapiEvent dwMessage, IntPtr dwParam1, IntPtr dwParam2, IntPtr dwParam3)
        {
            switch (dwMessage)
            {
                case TapiEvent.LINE_ADDRESSSTATE:
                    _addresses[dwParam1.ToInt32()].OnAddressStateChange(dwParam2.ToInt32());
                    break;
            
                case TapiEvent.LINE_LINEDEVSTATE:
                    HandleDevStateChange(dwParam1.ToInt32(), dwParam2, dwParam3);
                    break;

                case TapiEvent.LINE_CLOSE:
                    _hLine.SetHandleAsInvalid();
                    foreach (TapiAddress addr in Addresses)
                        addr.ClearCalls();
                    GatherStatus();
                    break;

                case TapiEvent.LINE_APPNEWCALL:
                    HandleNewCall(new TapiCall(_addresses[dwParam1.ToInt32()], (uint)dwParam2.ToInt32()), dwParam3.ToInt32());
                    break;

                case TapiEvent.LINE_DEVSPECIFIC:
                    OnDeviceSpecific(null, dwParam1, dwParam2, dwParam3);
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false, string.Format("Unknown Tapi Event {0} encountered in Line Handler", dwMessage.ToString()));
                    break;
            }
        }

        /// <summary>
        /// This is called for device-specific extensions
        /// </summary>
        /// <param name="call"></param>
        /// <param name="dwParam1"></param>
        /// <param name="dwParam2"></param>
        /// <param name="dwParam3"></param>
        internal void OnDeviceSpecific(TapiCall call, IntPtr dwParam1, IntPtr dwParam2, IntPtr dwParam3)
        {
            if (_devsCallback != null)
            {
                DeviceSpecificEventArgs e = (call != null) ?
                    new DeviceSpecificEventArgs(call, dwParam1, dwParam2, dwParam3) :
                    new DeviceSpecificEventArgs(this, dwParam1, dwParam2, dwParam3);
                _devsCallback(this, e);
            }
        }

        private void HandleNewCall(TapiCall call, int callPrivileges)
        {
            if (NewCall != null)
            {
                Privilege priv = (callPrivileges == NativeMethods.LINECALLPRIVILEGE_NONE) ? Privilege.None :
                    (callPrivileges == NativeMethods.LINECALLPRIVILEGE_MONITOR) ? Privilege.Monitor : Privilege.Owner;
                foreach (EventHandler<NewCallEventArgs> nc in NewCall.GetInvocationList())
                {
                    nc.BeginInvoke(this, new NewCallEventArgs(call, priv),
                        delegate(IAsyncResult ar) 
                        {
                            try
                            {
                                var nce = (EventHandler<NewCallEventArgs>)ar.AsyncState;
                                nce.EndInvoke(ar);
                            }
                            catch
                            {
                            }
                        }, nc);
                }
            }
        }

        private void HandleDevStateChange(int notificationType, IntPtr p2, IntPtr p3)
        {
            if (notificationType == NativeMethods.LINEDEVSTATE_CAPSCHANGE)
            {
                GatherDevCaps();
                if (Changed != null)
                    Changed(this, new LineInfoChangeEventArgs(this, (LineInfoChangeTypes)notificationType));
            }
            else if (notificationType == NativeMethods.LINEDEVSTATE_RINGING)
            {
                if (Ringing != null)
                    Ringing(this, new RingEventArgs(this, p2.ToInt32(), p3.ToInt32()));
            }
            else if (notificationType == NativeMethods.LINEDEVSTATE_COMPLCANCEL)
            {
                //TODO:
                //int completionId = p2.ToInt32();
            }
            else if (notificationType == NativeMethods.LINEDEVSTATE_REINIT)
            {
                // Ignore
            }
            else
            {
                GatherStatus();
                if (Changed != null)
                    Changed(this, new LineInfoChangeEventArgs(this, (LineInfoChangeTypes)notificationType));
            }
        }

        /// <summary>
        /// IDisposable.Dispose method
        /// </summary>
        public void Dispose()
        {
            _hLine.Dispose();
        }
    }
}
