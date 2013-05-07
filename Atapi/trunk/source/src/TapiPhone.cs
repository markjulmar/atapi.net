// TapiPhone.cs
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
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using JulMar.Atapi.Interop;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class models the capabilities for a hook switch device
    /// </summary>
    public sealed class HookswitchCapabilities
    {
        readonly string _name;
        readonly bool _isPresent;
        readonly HookswitchMode _modes;
        readonly HookswitchMode _settableModes;
        readonly HookswitchMode _monitorModes;
        readonly bool _canReadVolume;
        readonly bool _canAdjustVolume;
        readonly bool _canReadGain;
        readonly bool _canAdjustGain;
        readonly bool _canGetState;
        readonly bool _canSetState;

        /// <summary>
        /// Constructor
        /// </summary>
        internal HookswitchCapabilities(string name, bool present, int modes, int settableModes, int monitorModes, 
            bool canReadVolume, bool canAdjustVolume, 
            bool canReadGain, bool canAdjustGain, 
            bool canGetState, bool canSetState)
        {
            _name = name;
            _isPresent = present;
            _modes = (HookswitchMode) modes;
            _settableModes = (HookswitchMode) settableModes;
            _monitorModes = (HookswitchMode) monitorModes;
            _canReadVolume = canReadVolume;
            _canAdjustVolume = canAdjustVolume;
            _canReadGain = canReadGain;
            _canAdjustGain = canAdjustGain;
            _canGetState = canGetState;
            _canSetState = canSetState;
        }

        /// <summary>
        /// Name of the hook switch
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// True/False whether this hookswitch is present or not
        /// </summary>
        public bool IsPresent
        {
            get { return _isPresent; }
        }

        /// <summary>
        /// Returns the available hookswitch modes for this device
        /// </summary>
        public HookswitchMode AvailableModes
        {
            get { return _modes; }
        }

        /// <summary>
        /// Hookswitch devices which may be set
        /// </summary>
        public HookswitchMode SettableModes
        {
            get { return _settableModes; }
        }

        /// <summary>
        /// Hookswitch devices which may be monitored
        /// </summary>
        public HookswitchMode MonitorableModes
        {
            get { return _monitorModes; }
        }

        /// <summary>
        /// Returns whether the volume can be determined
        /// </summary>
        public bool CanGetVolume
        {
            get { return _canReadVolume; }
        }

        /// <summary>
        /// True if the volume can be adjusted on this hookswitch device
        /// </summary>
        public bool CanAdjustVolume
        {
            get { return _canAdjustVolume; }
        }

        /// <summary>
        /// Returns whether this hookswitch gain can be determined
        /// </summary>
        public bool CanGetGain
        {
            get { return _canReadGain; }
        }

        /// <summary>
        /// True if the gain can be adjusted on this hookswitch device
        /// </summary>
        public bool CanAdjustGain
        {
            get { return _canAdjustGain; }
        }

        /// <summary>
        /// Returns whether the current hookswitch status can be determined
        /// </summary>
        public bool CanGetState
        {
            get { return _canGetState; }
        }

        /// <summary>
        /// Returns whether the hookswitch status can be changed
        /// </summary>
        public bool CanAdjustState
        {
            get { return _canSetState; }
        }

        /// <summary>
        /// Provides a textual representation of the hookswitch
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return _name + "HookSwitchPresent = " + IsPresent;
        }
    }

    /// <summary>
    /// This models a single button/lamp combination
    /// </summary>
    public class ButtonLampInformation
    {
        readonly ButtonFunction _function;
        readonly ButtonMode _buttonMode;
        readonly LampMode _lampMode;
        readonly ButtonKey _key;

        /// <summary>
        /// Constructor
        /// </summary>
        internal ButtonLampInformation(int function, int mode, int lampMode, ButtonKey key)
        {
            _function = (ButtonFunction)function;
            _buttonMode = (ButtonMode)mode;
            _lampMode = (LampMode)lampMode;
            _key = key;
        }

        /// <summary>
        /// Returns whether this button has a function or not (lamp-only)
        /// </summary>
        public bool HasButton
        {
            get { return _buttonMode != ButtonMode.Dummy; }
        }

        /// <summary>
        /// Returns whether this has a lamp
        /// </summary>
        public bool HasLamp
        {
            get { return _lampMode != LampMode.Dummy; }
        }

        /// <summary>
        /// Returns the button function
        /// </summary>
        public ButtonFunction Function
        {
            get { return _function; }
        }

        /// <summary>
        /// Returns the button type
        /// </summary>
        public ButtonMode Type
        {
            get { return _buttonMode; }
        }

        /// <summary>
        /// Returns the lamp styles 
        /// </summary>
        public LampMode LampStyles
        {
            get { return _lampMode; }
        }

        /// <summary>
        /// Returns the button key (if any)
        /// </summary>
        public ButtonKey Key
        {
            get { return _key; }
        }

        /// <summary>
        /// Provides a textual representation of the button
        /// </summary>
        /// <returns>Button text</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} L:{2}, K:{3}", _buttonMode, _function, _lampMode, _key);
        }
    }

    /// <summary>
    /// This class holds the capabilities for a single phone device exposed by TAPI.
    /// </summary>
    public sealed class PhoneCapabilities : IFormattable
    {
        readonly PHONECAPS _pdc;
        readonly byte[] _rawBuffer;
        readonly string[] _devClasses;
        readonly List<ButtonLampInformation> _buttonArray = new List<ButtonLampInformation>();
        readonly int[] _downloadBufferSizes;
        readonly int[] _uploadBufferSizes;

        /// <summary>
        /// Constructor
        /// </summary>
        internal PhoneCapabilities(PHONECAPS pdc, byte[] buff)
        {
            _pdc = pdc;
            _rawBuffer = buff;

            if (_pdc.dwDeviceClassesSize > 0)
            {
                var arr = new List<String>(); int spos = _pdc.dwDeviceClassesOffset;
                for (int epos = _pdc.dwDeviceClassesOffset; epos < _pdc.dwDeviceClassesOffset + _pdc.dwDeviceClassesSize - 2; epos++)
                {
                    char ch = BitConverter.ToChar(_rawBuffer, epos);
                    if (ch == '\0')
                    {
                        string s = NativeMethods.GetString(_rawBuffer, spos, epos - spos, _pdc.dwStringFormat);
                        if (s.Length > 0)
                            arr.Add(s);
                        spos = epos + 1;
                        epos++;
                    }
                }
                _devClasses = new string[arr.Count];
                arr.CopyTo(_devClasses);
            }
            else
                _devClasses = new string[0];

            // Gather the button/lamps
            if (_pdc.dwNumButtonLamps > 0)
            {
                int keyPos = 0;
                var keyArray = (ButtonKey[])Enum.GetValues(typeof(ButtonKey));
                for (int i = 0; i < _pdc.dwNumButtonLamps; i++)
                {
                    int buttonMode, buttonFunction, lampMode;

                    if (_pdc.dwButtonModesOffset > 0 &&
                        _pdc.dwButtonModesSize > 0)
                        buttonMode = BitConverter.ToInt32(_rawBuffer, _pdc.dwButtonModesOffset + (i * 4));
                    else
                    {
                        if (i <= keyArray.Length)
                            buttonMode = (int) ButtonMode.Keypad;
                        else
                            buttonMode = (int) ButtonMode.Dummy;
                    }

                    if (_pdc.dwButtonFunctionsOffset > 0 &&
                        _pdc.dwButtonFunctionsSize > 0)
                        buttonFunction = BitConverter.ToInt32(_rawBuffer, _pdc.dwButtonFunctionsOffset + (i * 4));
                    else
                        buttonFunction = (int) ButtonFunction.None;

                    if (_pdc.dwLampModesOffset > 0 &&
                        _pdc.dwLampModesSize > 0)
                        lampMode = BitConverter.ToInt32(_rawBuffer, _pdc.dwLampModesOffset + (i * 4));
                    else
                        lampMode = (int) LampMode.Unknown;

                    ButtonKey key = (ButtonMode.Keypad == (ButtonMode)buttonMode && keyPos < keyArray.Length) ? keyArray[keyPos++] : ButtonKey.None;
                    _buttonArray.Add(new ButtonLampInformation(buttonFunction, buttonMode, lampMode, key));
                }
            }

            // Get the download buffer sizes
            _downloadBufferSizes = new int[_pdc.dwNumSetData];
            if (_pdc.dwNumSetData > 0)
            {
                for (int i = 0; i < _pdc.dwNumSetData; i++)
                    _downloadBufferSizes[i] = BitConverter.ToInt32(_rawBuffer, _pdc.dwSetDataOffset + (i * 4)); 
            }

            // Get the upload buffer sizes
            _uploadBufferSizes = new int[_pdc.dwNumGetData];
            if (_pdc.dwNumGetData > 0)
            {
                for (int i = 0; i < _pdc.dwNumGetData; i++)
                    _uploadBufferSizes[i] = BitConverter.ToInt32(_rawBuffer, _pdc.dwGetDataOffset + (i * 4));
            }
        }

        /// <summary>
        /// This provides data about the provider hardware and/or software, such as the vendor name and version numbers of hardware and software.
        /// </summary>
        public string ProviderInfo
        {
            get { return NativeMethods.GetString(_rawBuffer, _pdc.dwProviderInfoOffset, _pdc.dwProviderInfoSize, _pdc.dwStringFormat); }
        }

        /// <summary>
        /// This provides data about the phone hardware and/or software, such as the vendor name and version numbers of hardware and software.
        /// </summary>
        public string PhoneInfo
        {
            get { return NativeMethods.GetString(_rawBuffer, _pdc.dwPhoneInfoOffset, _pdc.dwPhoneInfoSize, _pdc.dwStringFormat); }
        }

        /// <summary>
        /// Permanent identifier by which the phone device is known in the system's configuration.
        /// </summary>
        public int PermanentPhoneId
        {
            get { return _pdc.dwPermanentPhoneID; }
        }

        /// <summary>
        /// Name for the phone device. This name can be configured by the user when configuring the 
        /// line device's service provider, and is provided for the user's convenience
        /// </summary>
        public string PhoneName
        {
            get { return NativeMethods.GetString(_rawBuffer, _pdc.dwPhoneNameOffset, _pdc.dwPhoneNameSize, _pdc.dwStringFormat); }
        }

        /// <summary>
        /// Returns the Device Specific data for this phone
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get
            {
                var data = new byte[_pdc.dwDevSpecificSize];
                Array.Copy(_rawBuffer, _pdc.dwDevSpecificOffset, data, 0, _pdc.dwDevSpecificSize);
                return data;
            }
        }

        /// <summary>
        /// GUID permanently associated with the phone device.
        /// </summary>
        public Guid Guid
        {
            get
            {
                return _pdc.PermanentPhoneGuid;
            }
        }

        /// <summary>
        /// Array of device class identifiers supported on one or more addresses on this phone.
        /// </summary>
        public string[] AvailableDeviceClasses
        {
            get { return (string[])_devClasses.Clone(); }
        }

        /// <summary>
        /// This returns the handset capabilities
        /// </summary>
        public HookswitchCapabilities Handset
        {
            get 
            {
                return new HookswitchCapabilities("Handset",
                (_pdc.dwHookSwitchDevs & NativeMethods.PHONEHOOKSWITCHDEV_HANDSET) != 0,
                _pdc.dwHandsetHookSwitchModes, _pdc.dwSettableHandsetHookSwitchModes, _pdc.dwMonitoredHandsetHookSwitchModes,
                (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETVOLUMEHANDSET) > 0,
                (_pdc.dwVolumeFlags & NativeMethods.PHONEHOOKSWITCHDEV_HANDSET) != 0 && (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETVOLUMEHANDSET) > 0,
                (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETGAINHANDSET) != 0,
                (_pdc.dwGainFlags & NativeMethods.PHONEHOOKSWITCHDEV_HANDSET) != 0 && (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETGAINHANDSET) > 0,
                (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETHOOKSWITCHHANDSET) != 0,
                (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETHOOKSWITCHHANDSET) != 0);
            }
        }

        /// <summary>
        /// This returns the headset capabilities
        /// </summary>
        public HookswitchCapabilities Headset
        {
            get 
            {
                return new HookswitchCapabilities("Headset",
                    (_pdc.dwHookSwitchDevs & NativeMethods.PHONEHOOKSWITCHDEV_HEADSET) != 0,
                    _pdc.dwHeadsetHookSwitchModes, _pdc.dwSettableHeadsetHookSwitchModes, _pdc.dwMonitoredHeadsetHookSwitchModes,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETVOLUMEHEADSET) > 0,
                    (_pdc.dwVolumeFlags & NativeMethods.PHONEHOOKSWITCHDEV_HEADSET) != 0 && (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETVOLUMEHEADSET) > 0,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETGAINHEADSET) != 0,
                    (_pdc.dwGainFlags & NativeMethods.PHONEHOOKSWITCHDEV_HEADSET) != 0 && (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETGAINHEADSET) > 0,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETHOOKSWITCHHEADSET) != 0,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETHOOKSWITCHHEADSET) != 0);
            }
        }

        /// <summary>
        /// This returns the speaker capabilities
        /// </summary>
        public HookswitchCapabilities Speaker
        {
            get 
            { 
                return new HookswitchCapabilities("Speaker", 
                    (_pdc.dwHookSwitchDevs & NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER) != 0, 
                    _pdc.dwSpeakerHookSwitchModes, _pdc.dwSettableSpeakerHookSwitchModes, _pdc.dwMonitoredSpeakerHookSwitchModes,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETVOLUMESPEAKER) > 0,
                    (_pdc.dwVolumeFlags & NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER) != 0 && (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETVOLUMESPEAKER) > 0,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETGAINSPEAKER) != 0,
                    (_pdc.dwGainFlags & NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER) != 0 && (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETGAINSPEAKER) > 0,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETHOOKSWITCHSPEAKER) != 0,
                    (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETHOOKSWITCHSPEAKER) != 0);
            }
        }

        /// <summary>
        /// Returns the ringer style count
        /// </summary>
        public int RingerStyleCount
        {
            get { return _pdc.dwNumRingModes; }
        }

        /// <summary>
        /// This returns the list of buttons and lamps
        /// </summary>
        public ButtonLampInformation[] ButtonsLamps
        {
            get { return _buttonArray.ToArray(); }
        }

        /// <summary>
        /// Supports retrieving button information on this phone
        /// </summary>
        public bool CanGetButtonInfo
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETBUTTONINFO) > 0; }
        }

        /// <summary>
        /// Supports uploading data to buffers on the phone
        /// </summary>
        public bool CanUploadData
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETDATA ) > 0; }
        }

        /// <summary>
        /// Supports getting the current lamp state
        /// </summary>
        public bool CanGetLampState
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETLAMP) > 0; }
        }

        /// <summary>
        /// Supports getting the current ringer
        /// </summary>
        public bool CanGetRinger
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETRING) > 0; }
        }

        /// <summary>
        /// Supports changing the button information (programmable buttons)
        /// </summary>
        public bool CanAdjustButtonInfo
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETBUTTONINFO) > 0; }
        }

        /// <summary>
        /// Supports downloading data from the phone
        /// </summary>
        public bool CanDownloadData
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETDATA) > 0; }
        }

        /// <summary>
        /// Supports changing the lamp state
        /// </summary>
        public bool CanSetLampState
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETLAMP) > 0; }
        }

        /// <summary>
        /// Supports changing the ringer
        /// </summary>
        public bool CanSetRinger
        {
            get { return (_pdc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETRING) > 0; }
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
                        try { value = pi.GetValue(this, null); } catch { value = null;  }
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
    /// This class holds the current phone status
    /// </summary>
    public sealed class PhoneStatus
    {
        readonly TapiPhone _phoneOwner;
        internal PHONESTATUS _phoneStatus;
        readonly byte[] _rawBuffer;

        internal PhoneStatus(TapiPhone owner, PHONESTATUS ps, byte[] buffer)
        {
            _phoneOwner = owner;
            _phoneStatus = ps;
            _rawBuffer = buffer;
        }

        /// <summary>
        /// Number of active opens on the phone device.
        /// </summary>
        public int OwnerCount
        {
            get { return _phoneStatus.dwNumOwners; }
        }

        /// <summary>
        /// Number of active opens on the phone device.
        /// </summary>
        public int MonitorCount
        {
            get { return _phoneStatus.dwNumMonitors; }
        }

        /// <summary>
        /// Specifies whether the phone is currently connected to TAPI. TRUE if connected, FALSE otherwise. 
        /// </summary>
        public bool Connected
        {
            get { return (_phoneStatus.dwStatusFlags & NativeMethods.PHONESTATUSFLAGS_CONNECTED) > 0; }
        }

        /// <summary>
        /// Specifies whether TAPI's manipulation of the phone device is suspended. TRUE if suspended, FALSE otherwise. 
        /// An application's use of a phone device can be temporarily suspended when the switch wants to manipulate the phone 
        /// in a way that cannot tolerate interference from the application. 
        /// </summary>
        public bool Suspended
        {
            get { return (_phoneStatus.dwStatusFlags & NativeMethods.PHONESTATUSFLAGS_SUSPENDED) > 0; }
        }

        /// <summary>
        /// Current ring mode of a phone device - based on PhoneCapabilities.RingerCount
        /// </summary>
        public int RingerMode
        {
            get { return _phoneStatus.dwRingMode; }
            set
            {
                if (!_phoneOwner.Capabilities.CanSetRinger)
                    throw new NotSupportedException("Cannot change ringer on this device.");
                if (!_phoneOwner.IsOpen)
                    throw new InvalidOperationException("Phone is not open.");

                int rc = NativeMethods.phoneSetRing(_phoneOwner.Handle, value, RingerVolume);
                if (rc < 0)
                {
                    throw new TapiException("phoneSetRing failed", rc);
                }

                _phoneStatus.dwRingMode = value;
            }
        }

        /// <summary>
        /// Returns the current ringer volume
        /// </summary>
        public int RingerVolume
        {
            get 
            { 
                return _phoneStatus.dwRingVolume; 
            }
            set
            {
                if (!_phoneOwner.Capabilities.CanSetRinger)
                    throw new NotSupportedException("Cannot set volume on this device.");
                if (!_phoneOwner.IsOpen)
                    throw new InvalidOperationException("Phone is not open.");

                int rc = NativeMethods.phoneSetRing(_phoneOwner.Handle, RingerMode, value);
                if (rc < 0)
                {
                    throw new TapiException("phoneSetRing failed", rc);
                }

                _phoneStatus.dwRingVolume = value;
            }
        }

        /// <summary>
        /// Returns the Device Specific data for this phone
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get
            {
                var data = new byte[_phoneStatus.dwDevSpecificSize];
                Array.Copy(_rawBuffer, _phoneStatus.dwDevSpecificOffset, data, 0, _phoneStatus.dwDevSpecificSize);
                return data;
            }
        }

        /// <summary>
        /// Supports retrieving button information on this phone
        /// </summary>
        public bool CanGetButtonInfo
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETBUTTONINFO) > 0; }
        }

        /// <summary>
        /// Supports uploading data to buffers on the phone
        /// </summary>
        public bool CanUploadData
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETDATA) > 0; }
        }

        /// <summary>
        /// Supports getting the current lamp state
        /// </summary>
        public bool CanGetLampState
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETLAMP) > 0; }
        }

        /// <summary>
        /// Supports getting the current ringer
        /// </summary>
        public bool CanGetRinger
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_GETRING) > 0; }
        }

        /// <summary>
        /// Supports changing the button information (programmable buttons)
        /// </summary>
        public bool CanAdjustButtonInfo
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETBUTTONINFO) > 0; }
        }

        /// <summary>
        /// Supports downloading data from the phone
        /// </summary>
        public bool CanDownloadData
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETDATA) > 0; }
        }

        /// <summary>
        /// Supports changing the lamp state
        /// </summary>
        public bool CanSetLampState
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETLAMP) > 0; }
        }

        /// <summary>
        /// Supports changing the ringer
        /// </summary>
        public bool CanSetRinger
        {
            get { return (_phoneStatus.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETRING) > 0; }
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
    /// This class represents a phone display device
    /// </summary>
    public sealed class PhoneDisplay
    {
        private readonly TapiPhone _phoneOwner;
        private string _display;
        private readonly int _cols;
        private readonly int _rows;
        private readonly bool _canChange;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">Phone object</param>
        /// <param name="cols">Number of columns</param>
        /// <param name="rows">Number of rows</param>
        /// <param name="canChangeDisplay">True if the display is changable by the application</param>
        internal PhoneDisplay(TapiPhone owner, int cols, int rows, bool canChangeDisplay)
        {
            _phoneOwner = owner;
            _cols = cols;
            _rows = rows;
            _canChange = canChangeDisplay;
        }

        /// <summary>
        /// This returns whether the phone display can be changed programatically.
        /// </summary>
        public bool CanModify
        {
            get
            {
                return _canChange;
            }
        }

        /// <summary>
        /// Returns the number of columns on this device
        /// </summary>
        public int Columns
        {
            get { return _cols; }
        }

        /// <summary>
        /// Returns the number of rows on this device
        /// </summary>
        public int Rows
        {
            get { return _rows; }
        }

        /// <summary>
        /// Returns the display text information
        /// </summary>
        public string Text
        {
            get { return _display ?? (_display = RetrieveDisplayInfo()); }

            set
            {
                if (!_canChange)
                    throw new NotSupportedException();

                _display = value;
                SetDisplayInfo();
            }
        }

        private void SetDisplayInfo()
        {
            int rc = NativeMethods.phoneSetDisplay(_phoneOwner.Handle, 0, 0, _display, _display.Length+1);
            if (rc < 0)
                throw new TapiException("phoneSetDisplay failed", rc);
            var tapiReq = new PendingTapiRequest(rc, null, null);
            _phoneOwner.TapiManager.AddAsyncRequest(tapiReq);
            tapiReq.AsyncWaitHandle.WaitOne();
            if (tapiReq.Result < 0)
                throw new TapiException("phoneSetDisplay failed", rc);
        }

        private string RetrieveDisplayInfo()
        {
            if (!_phoneOwner.IsOpen)
                throw new NotSupportedException("Phone is not open.");

            var vs = new VARSTRING();
            string retValue = null;
            int rc, neededSize = Marshal.SizeOf(vs) + (_rows*_cols*2);
            do
            {
                vs.dwTotalSize = neededSize;
                IntPtr lpVs = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(vs, lpVs, true);
                rc = NativeMethods.phoneGetDisplay(_phoneOwner.Handle, lpVs);
                Marshal.PtrToStructure(lpVs, vs);
                if (vs.dwNeededSize > neededSize)
                {
                    neededSize = vs.dwNeededSize;
                    rc = NativeMethods.PHONEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.PHONEERR_OK)
                {
                    var finalBuffer = new byte[vs.dwNeededSize];
                    Marshal.Copy(lpVs, finalBuffer, 0, vs.dwNeededSize);
                    retValue = NativeMethods.GetString(finalBuffer, vs.dwStringOffset, vs.dwStringSize, vs.dwStringFormat);
                }
                Marshal.FreeHGlobal(lpVs);
            }
            while (rc == NativeMethods.PHONEERR_STRUCTURETOOSMALL);

            return retValue;
        }

        /// <summary>
        /// This is called to invalidate the display contents.  Typically when the
        /// device notifies us that the display has changed.  It will be retrieved again when
        /// the client application requests the text.
        /// </summary>
        internal void Invalidate()
        {
            _display = null;
        }
    }

    /// <summary>
    /// This class represents a phone hookswitch device
    /// </summary>
    public sealed class PhoneHookswitch
    {
        readonly TapiPhone _phoneOwner;
        readonly HookswitchCapabilities _caps;
        readonly int _type;

        internal PhoneHookswitch(TapiPhone phoneOwner, int type, HookswitchCapabilities caps)
        {
            _phoneOwner = phoneOwner;
            _type = type;
            _caps = caps;
        }

        /// <summary>
        /// Returns the name of this hookswitch device
        /// </summary>
        public string Name
        {
            get { return _caps.Name; }
        }

        /// <summary>
        /// Returns the capabilities for this hookswitch device
        /// </summary>
        public HookswitchCapabilities Capabilities
        {
            get { return _caps; }
        }

        /// <summary>
        /// Returns whether the status of this hookswitch can be changed
        /// </summary>
        public bool CanChangeState
        {
            get 
            { 
                int mask;
                switch (_type)
                {
                    case NativeMethods.PHONEHOOKSWITCHDEV_HANDSET:
                        mask = NativeMethods.PHONEFEATURE_SETHOOKSWITCHHANDSET;
                        break;
                    case NativeMethods.PHONEHOOKSWITCHDEV_HEADSET:
                        mask = NativeMethods.PHONEFEATURE_SETHOOKSWITCHHEADSET;
                        break;
                    case NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER:
                        mask = NativeMethods.PHONEFEATURE_SETHOOKSWITCHSPEAKER;
                        break;
                    default:
                        mask = 0;
                        break;
                }
                return (_phoneOwner.Status._phoneStatus.dwPhoneFeatures & mask) > 0;
            }
        }

        /// <summary>
        /// Returns whether the volume can be changed
        /// </summary>
        public bool CanChangeVolume
        {
            get
            {
                int mask;
                switch (_type)
                {
                    case NativeMethods.PHONEHOOKSWITCHDEV_HANDSET:
                        mask = NativeMethods.PHONEFEATURE_SETVOLUMEHANDSET;
                        break;
                    case NativeMethods.PHONEHOOKSWITCHDEV_HEADSET:
                        mask = NativeMethods.PHONEFEATURE_SETVOLUMEHANDSET;
                        break;
                    case NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER:
                        mask = NativeMethods.PHONEFEATURE_SETVOLUMEHANDSET;
                        break;
                    default:
                        mask = 0;
                        break;
                }
                return (_phoneOwner.Status._phoneStatus.dwPhoneFeatures & mask) > 0;
            }
        }

        /// <summary>
        /// Returns whether the gain can be changed
        /// </summary>
        public bool CanChangeGain
        {
            get
            {
                int mask;
                switch (_type)
                {
                    case NativeMethods.PHONEHOOKSWITCHDEV_HANDSET:
                        mask = NativeMethods.PHONEFEATURE_SETGAINHANDSET;
                        break;
                    case NativeMethods.PHONEHOOKSWITCHDEV_HEADSET:
                        mask = NativeMethods.PHONEFEATURE_SETGAINHEADSET;
                        break;
                    case NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER:
                        mask = NativeMethods.PHONEFEATURE_SETGAINSPEAKER;
                        break;
                    default:
                        mask = 0;
                        break;
                }
                return (_phoneOwner.Status._phoneStatus.dwPhoneFeatures & mask) > 0;
            }
        }

        /// <summary>
        /// Sets or Returns the full hookswitch status
        /// </summary>
        public HookswitchMode Status
        {
            get
            {
                switch (_type)
                {
                    case NativeMethods.PHONEHOOKSWITCHDEV_HANDSET:
                        return (HookswitchMode)_phoneOwner.Status._phoneStatus.dwHandsetHookSwitchMode;
                    case NativeMethods.PHONEHOOKSWITCHDEV_HEADSET:
                        return (HookswitchMode)_phoneOwner.Status._phoneStatus.dwHeadsetHookSwitchMode;
                    case NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER:
                        return (HookswitchMode)_phoneOwner.Status._phoneStatus.dwSpeakerHookSwitchMode;
                    default:
                        break;
                }
                return HookswitchMode.Unknown;
            }

            set
            {
                if ((Capabilities.SettableModes & value) > 0)
                    throw new ArgumentOutOfRangeException("Status", "Invalid hookswitch mode passed");
                if (!_phoneOwner.IsOpen)
                    throw new InvalidOperationException("Phone is not open");
                if (!CanChangeState)
                    throw new InvalidOperationException("Cannot change hookswitch mode");

                int rc = NativeMethods.phoneSetHookSwitch(_phoneOwner.Handle, _type, (int) value);
                if (rc < 0)
                {
                    throw new TapiException("phoneSetHookSwitch failed", rc);
                }
            }
        }

        /// <summary>
        /// Sets or Returns the current volume of the hookswitch
        /// </summary>
        public int Volume
        {
            get 
            {
                switch (_type)
                {
                    case NativeMethods.PHONEHOOKSWITCHDEV_HANDSET:
                        return _phoneOwner.Status._phoneStatus.dwHandsetVolume;
                    case NativeMethods.PHONEHOOKSWITCHDEV_HEADSET:
                        return _phoneOwner.Status._phoneStatus.dwHeadsetVolume;
                    case NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER:
                        return _phoneOwner.Status._phoneStatus.dwSpeakerVolume;
                    default:
                        break;
                }
                return 0;
            }

            set
            {
                if (!Capabilities.CanGetVolume)
                    throw new NotSupportedException();
                if (!_phoneOwner.IsOpen)
                    throw new InvalidOperationException("Phone is not open");
                if (!CanChangeVolume)
                    throw new InvalidOperationException("Cannot change hookswitch volume");

                int rc = NativeMethods.phoneSetVolume(_phoneOwner.Handle, _type, value);
                if (rc < 0)
                {
                    throw new TapiException("phoneSetVolume failed", rc);
                }
            }
        }

        /// <summary>
        /// Sets or Returns the current gain of the hookswitch
        /// </summary>
        public int Gain
        {
            get
            {
                switch (_type)
                {
                    case NativeMethods.PHONEHOOKSWITCHDEV_HANDSET:
                        return _phoneOwner.Status._phoneStatus.dwHandsetGain;
                    case NativeMethods.PHONEHOOKSWITCHDEV_HEADSET:
                        return _phoneOwner.Status._phoneStatus.dwHandsetGain;
                    case NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER:
                        return _phoneOwner.Status._phoneStatus.dwHandsetGain;
                    default:
                        break;
                }
                return 0;
            }

            set
            {
                if (!Capabilities.CanGetGain)
                    throw new NotSupportedException();
                if (!_phoneOwner.IsOpen)
                    throw new InvalidOperationException("Phone is not open");
                if (!CanChangeGain)
                    throw new InvalidOperationException("Cannot change hookswitch gain");

                int rc = NativeMethods.phoneSetGain(_phoneOwner.Handle, _type, value);
                if (rc < 0)
                {
                    throw new TapiException("phoneSetGain failed", rc);
                }
            }
        }

        /// <summary>
        /// Provides a textual representation of the hookswitch device
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// This class models a button
    /// </summary>
    public sealed class PhoneButton
    {
        readonly TapiPhone _phoneOwner;
        readonly int _id;
        readonly ButtonLampInformation _bi;
        string _text = string.Empty;
        ButtonState _state;
        byte[] _dsData;

        internal PhoneButton(TapiPhone phone, int index, ButtonLampInformation bi)
        {
            _phoneOwner = phone;
            _id = index;
            _bi = bi;
        }

        internal void Reload()
        {
            if (!_phoneOwner.IsOpen)
                return;

            var pbi = new PHONEBUTTONINFO();
            int rc, neededSize = 256;
            do
            {
                pbi.dwTotalSize = neededSize;
                IntPtr ppbi = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(pbi, ppbi, true);
                rc = NativeMethods.phoneGetButtonInfo(_phoneOwner.Handle, _id, ppbi);
                Marshal.PtrToStructure(ppbi, pbi);
                if (pbi.dwNeededSize > neededSize)
                {
                    neededSize = pbi.dwNeededSize;
                    rc = NativeMethods.PHONEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.PHONEERR_OK)
                {
                    var rawBuffer = new byte[pbi.dwUsedSize];
                    Marshal.Copy(ppbi, rawBuffer, 0, pbi.dwUsedSize);
                    
                    _text = pbi.dwButtonTextSize > 0 
                        ? NativeMethods.GetString(rawBuffer, pbi.dwButtonTextOffset, pbi.dwButtonTextSize, NativeMethods.STRINGFORMAT_ASCII) 
                        : string.Empty;

                    _state = (ButtonState) pbi.dwButtonState;
                    if (pbi.dwDevSpecificSize > 0)
                    {
                        _dsData = new byte[pbi.dwDevSpecificSize];
                        Array.Copy(rawBuffer, pbi.dwDevSpecificOffset, _dsData, 0, pbi.dwDevSpecificSize);
                    }
                }
                Marshal.FreeHGlobal(ppbi);
            }
            while (rc == NativeMethods.PHONEERR_STRUCTURETOOSMALL);
        }

        /// <summary>
        /// Returns the assigned button/lamp id
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Returns whether this button has a function or not (lamp-only)
        /// </summary>
        public bool HasButton
        {
            get { return _bi.HasButton; }
        }

        /// <summary>
        /// Returns whether this has a lamp
        /// </summary>
        public bool HasLamp
        {
            get { return _bi.HasLamp; }
        }

        /// <summary>
        /// Returns the button function
        /// </summary>
        public ButtonFunction Function
        {
            get { return _bi.Function; }
        }

        /// <summary>
        /// Returns the button type
        /// </summary>
        public ButtonMode Type
        {
            get { return _bi.Type; }
        }

        /// <summary>
        /// Returns the lamp styles 
        /// </summary>
        public LampMode AvailableLampStyles
        {
            get { return _bi.LampStyles; }
        }

        /// <summary>
        /// Returns the button key (if any)
        /// </summary>
        public ButtonKey Key
        {
            get { return _bi.Key; }
        }

        /// <summary>
        /// The button text
        /// </summary>
        public string Text
        {
            get 
            {
                return string.IsNullOrEmpty(_text) ? string.Format("{0} {1}", _bi.Type, _bi.Function) : _text;
            }
        }

        /// <summary>
        /// Returns the current button state
        /// </summary>
        public ButtonState State
        {
            get
            {
                return _state;
            }
        }

        /// <summary>
        /// Gets or sets the current lamp state
        /// </summary>
        public LampMode LampState
        {
            get
            {
                if (!_phoneOwner.IsOpen)
                    return LampMode.Unknown;
                if (!_bi.HasLamp)
                    return LampMode.Dummy;
                
                int lampMode;
                int rc = NativeMethods.phoneGetLamp(_phoneOwner.Handle, _id, out lampMode);
                return (rc == NativeMethods.PHONEERR_OK) ? (LampMode)lampMode : LampMode.Unknown;
            }

            set
            {
                if (!_phoneOwner.IsOpen)
                    throw new InvalidOperationException("Phone is not open");
                if (!_bi.HasLamp || (_bi.LampStyles & value) == 0)
                    throw new ArgumentOutOfRangeException("LampState", "Cannot change lamp state to specified value");

                int rc = NativeMethods.phoneSetLamp(_phoneOwner.Handle, _id, (int) value);
                if (rc < 0)
                    throw new TapiException("phoneSetLamp failed", rc);
            }
        }

        /// <summary>
        /// Returns the device specific data
        /// </summary>
        public byte[] DeviceSpecificData
        {
            get { return (_dsData != null) ? (byte[])_dsData.Clone() : null; }
        }

        /// <summary>
        /// This method is called when the phone receives a PHONE_BUTTON event to update the
        /// current button status
        /// </summary>
        /// <param name="buttonMode">Button mode</param>
        /// <param name="buttonState">Button status</param>
        internal void ProcessButtonNotify(ButtonMode buttonMode, ButtonState buttonState)
        {
            _state = buttonState;
        }

        /// <summary>
        /// Provides a textual representation of the button
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }

    /// <summary>
    /// This class represents a single phone device exposed by TAPI.
    /// </summary>
    public sealed class TapiPhone : IDisposable
    {
        private const int MinTapiVersion = (int)TapiVersion.V13;
        private const int MaxTapiVersion = (int)TapiVersion.V31;

        private readonly TapiManager _mgr;
        private readonly int _deviceId;
        private readonly int _negotiatedVersion;
        private int _negotiatedExtVersion;
        private readonly int _stringFormat;
        private string _phoneName = string.Empty;
        private HTPHONE _hPhone = new HTPHONE();
        private readonly TapiEventCallback _pcb;
        private PhoneCapabilities _props;
        private PhoneStatus _status;
        private readonly string _extensionId = "0.0.0.0";
        private readonly PhoneDisplay _display;

        private readonly PhoneHookswitch _handset;
        private readonly PhoneHookswitch _headset;
        private readonly PhoneHookswitch _speaker;

        private readonly List<PhoneButton> _buttons = new List<PhoneButton>();

        private EventHandler<DeviceSpecificEventArgs> _devsCallback;
        
		internal TapiPhone(TapiManager mgr, int deviceId)
		{
            _mgr = mgr;
			_deviceId = deviceId;
            _pcb = new TapiEventCallback(PhoneCallback);

            PHONEEXTENSIONID extId;
            int rc = NativeMethods.phoneNegotiateAPIVersion(_mgr.PhoneHandle, _deviceId, 
                        MinTapiVersion, MaxTapiVersion, out _negotiatedVersion, out extId);
            if (rc == NativeMethods.PHONEERR_OK)
            {
                IsValid = true;

                GatherStatus(); // will fail

                PHONECAPS lpc = GatherCaps();
                _stringFormat = lpc.dwStringFormat;
                _extensionId = String.Format(CultureInfo.CurrentCulture, "{0}.{1}.{2}.{3}", extId.dwExtensionID0, extId.dwExtensionID1, extId.dwExtensionID2, extId.dwExtensionID3);

                // If we have a display device, then create it.
                if ((lpc.dwPhoneFeatures & (NativeMethods.PHONEFEATURE_GETDISPLAY | NativeMethods.PHONEFEATURE_SETDISPLAY)) > 0 ||
                    (lpc.dwDisplayNumColumns > 0 && lpc.dwDisplayNumRows > 0))
                {
                    _display = new PhoneDisplay(this, lpc.dwDisplayNumColumns, lpc.dwDisplayNumRows, 
                                    (lpc.dwPhoneFeatures & NativeMethods.PHONEFEATURE_SETDISPLAY)>0);
                }

                // Setup the hookswitches
                if (Capabilities.Handset.IsPresent)
                    _handset = new PhoneHookswitch(this, NativeMethods.PHONEHOOKSWITCHDEV_HANDSET, Capabilities.Handset);
                if (Capabilities.Headset.IsPresent)
                    _headset = new PhoneHookswitch(this, NativeMethods.PHONEHOOKSWITCHDEV_HEADSET, Capabilities.Headset);
                if (Capabilities.Speaker.IsPresent)
                    _speaker = new PhoneHookswitch(this, NativeMethods.PHONEHOOKSWITCHDEV_SPEAKER, Capabilities.Speaker);

                // Add all the buttons
                int numButton = 0;
                foreach (ButtonLampInformation bli in Capabilities.ButtonsLamps)
                    _buttons.Add(new PhoneButton(this, numButton++, bli));
            }
            else
            {
                IsValid = false;
                _props = new PhoneCapabilities(new PHONECAPS(), null);
            }
		}

        internal TapiManager TapiManager
        {
            get { return _mgr; }
        }

        internal HTPHONE Handle
        {
            get { return _hPhone; }
        }

        internal int StringFormat
        {
            get { return _stringFormat; }
        }

        /// <summary>
        /// This event is raised when a call on this address changes state.
        /// </summary>
        public event EventHandler<PhoneStateEventArgs> PhoneStateChanged;

        /// <summary>
        /// This event is raised when a call on this address changes state.
        /// </summary>
        public event EventHandler<PhoneButtonPressEventArgs> PhoneButtonPressed;

        /// <summary>
        /// This returns whether the phone device is usable or not.  Removed phones are
        /// not usable and have no capabilities or properties.
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        /// The numeric device ID representing the phone device.
        /// </summary>
        public int Id
        {
            get { return _deviceId; }
        }

        /// <summary>
        /// The permanent numeric ID representing this phone device.
        /// </summary>
        public int PermanentId
        {
            get { return _props.PermanentPhoneId; }
        }

        /// <summary>
        /// The <see cref="TapiVersion"/> that this line negotiated to.
        /// </summary>
        public TapiVersion NegotiatedVersion
        {
            get { return (TapiVersion)_negotiatedVersion; }
        }

        /// <summary>
        /// Returns the phone display information.  This can return null if no display
        /// is available for this device.
        /// </summary>
        public PhoneDisplay Display
        {
            get 
            {
                return _display; 
            }
        }

        /// <summary>
        /// This opens the phone in OWNER mode
        /// </summary>
        public void Open()
        {
            Open(NativeMethods.PHONEPRIVILEGE_OWNER);
        }

        /// <summary>
        /// This opens the phone in MONITOR mode - application seeks read-only access
        /// </summary>
        public void Monitor()
        {
            Open(NativeMethods.PHONEPRIVILEGE_MONITOR);
        }

        /// <summary>
        /// This method opens the phone device and allows it to be interacted with.
        /// </summary>
        private void Open(int privilege)
        {
            if (IsOpen)
                throw new TapiException("Phone is already open", NativeMethods.PHONEERR_OPERATIONUNAVAIL);

            IntPtr hPhone;
            int rc = NativeMethods.phoneOpen(_mgr.PhoneHandle, _deviceId, out hPhone, _negotiatedVersion, _negotiatedExtVersion,
                Marshal.GetFunctionPointerForDelegate(_pcb), privilege);

            if (rc == NativeMethods.PHONEERR_OK)
            {
                // Open our handle
                _hPhone = new HTPHONE(hPhone, true);

                // Turn on reporting for all known messages (TAPI 2.2)
                NativeMethods.phoneSetStatusMessages(Handle, 0x00FFFFFF, 0x0000003F, 0x0000000F);

                // Get the current status of the phone
                GatherStatus();

                // Refresh the button status
                foreach (PhoneButton button in _buttons)
                    button.Reload();
            }
            else
                throw new TapiException("phoneOpen failed", rc);
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
            int rc = NativeMethods.phoneNegotiateExtVersion(_mgr.PhoneHandle, _deviceId, _negotiatedVersion, minVersion, maxVersion, out _negotiatedExtVersion);
            if (rc < 0)
            {
                throw new TapiException("phoneNegotiateExtVersion failed", rc);
            }

            _devsCallback = dsc;
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
        /// Returns the Name associated with the phone.  It will never be empty.
        /// </summary>
        public string Name
        {
            get
            {
                if (_phoneName.Length == 0 && _props != null)
                    _phoneName = _props.PhoneName;
                if (_phoneName.Length == 0)
                    _phoneName = string.Format(CultureInfo.CurrentCulture, "Phone {0}", _deviceId);
                return _phoneName;
            }
        }

        /// <summary>
        /// Returns the line device associated with this phone.
        /// </summary>
        /// <returns>TapiLine</returns>
        public TapiLine GetAssociatedLine()
        {
            if (_mgr.Lines.Length > 0)
            {
                var buffer = (byte[])GetExternalDeviceInfo("tapi/line");
                if (buffer != null && buffer.Length > 0)
                {
                    int deviceId = BitConverter.ToInt32(buffer, 0);
                    foreach (TapiLine line in _mgr.Lines)
                    {
                        if (line.Id == deviceId)
                            return line;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// This returns a device identifier for the specified device class associated with the phone
        /// </summary>
        /// <param name="deviceClass">Device Class</param>
        /// <returns>string or byte[]</returns>
        public object GetExternalDeviceInfo(string deviceClass)
        {
            if (Handle.IsInvalid)
                throw new InvalidOperationException("Phone is not open");

            var vs = new VARSTRING();
            object retValue = null;
            int rc, neededSize = Marshal.SizeOf(vs) + 100;
            do
            {
                vs.dwTotalSize = neededSize;
                IntPtr lpVs = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(vs, lpVs, true);
                rc = NativeMethods.phoneGetID(Handle, lpVs, deviceClass);
                Marshal.PtrToStructure(lpVs, vs);
                if (vs.dwNeededSize > neededSize)
                {
                    neededSize = vs.dwNeededSize;
                    rc = NativeMethods.PHONEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.PHONEERR_OK)
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
            while (rc == NativeMethods.PHONEERR_STRUCTURETOOSMALL);

            return retValue;
        }

        #region phoneGetData
        /// <summary>
        /// This method retrieves the information from the specified phone buffer location in the open phone device to 
        /// the specified buffer
        /// </summary>
        /// <param name="id">Buffer id</param>
        /// <param name="buffer">Storage area for data - must be allocated</param>
        public void GetPhoneBuffer(int id, byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (buffer.Length == 0)
                throw new ArgumentException("buffer must be at least one byte in length");
            if (!IsOpen)
                throw new InvalidOperationException("Phone is not open");

            IntPtr data = Marshal.AllocHGlobal(buffer.Length);
            try
            {
                int rc = NativeMethods.phoneGetData(Handle, id, data, buffer.Length);
                if (rc < 0)
                    throw new TapiException("phoneGetData failed", rc);
                Marshal.Copy(data, buffer, 0, buffer.Length);
            }
            finally
            {
                Marshal.FreeHGlobal(data);
            }
        }
        #endregion

        #region phoneDevSpecific
        /// <summary>
        /// This method sends information to the phoneDevSpecific function
        /// </summary>
        /// <param name="buffer">Data to put into buffer</param>
        public void DevSpecific(byte[] buffer) 
        {
            if (!IsOpen)
                throw new InvalidOperationException("Phone is not open");
            if (buffer == null)
                buffer = new byte[0];

            IntPtr data = Marshal.AllocHGlobal(buffer.Length);
            if (buffer.Length > 0)
                Marshal.Copy(buffer, 0, data, buffer.Length);
            try
            {
                int rc = NativeMethods.phoneDevSpecific(Handle, data, buffer.Length);
                if (rc < 0)
                    throw new TapiException("phoneDevSpecific failed", rc);
            }
            finally
            {
                Marshal.FreeHGlobal(data);
            }
        }
        #endregion

        #region phoneSetData
        /// <summary>
        /// This method sets information into the specified phone buffer location in the open phone device from
        /// the specified buffer
        /// </summary>
        /// <param name="id">Buffer id</param>
        /// <param name="buffer">Data to put into buffer</param>
        public void SetPhoneBuffer(int id, byte[] buffer)
        {
            if (!IsOpen)
                throw new InvalidOperationException("Phone is not open");
            if (buffer == null)
                buffer = new byte[0];

            IntPtr data = Marshal.AllocHGlobal(buffer.Length);
            if (buffer.Length > 0)
                Marshal.Copy(buffer, 0, data, buffer.Length);
            try
            {
                int rc = NativeMethods.phoneSetData(Handle, id, data, buffer.Length);
                if (rc < 0)
                    throw new TapiException("phoneGetData failed", rc);
            }
            finally
            {
                Marshal.FreeHGlobal(data);
            }
        }
        #endregion

        /// <summary>
        /// Returns a System.String representing this phone object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Returns the <see cref="PhoneCapabilities"/> object for this phone.
        /// </summary>
        public PhoneCapabilities Capabilities
        {
            get { return _props; }
        }

        /// <summary>
        /// Returns the <see cref="PhoneStatus"/> object for this phone.
        /// </summary>
        public PhoneStatus Status
        {
            get { return _status; }
        }

        /// <summary>
        /// Returns true/false whether the phone is currently open.
        /// </summary>
        public bool IsOpen
        {
            get { return _hPhone.IsInvalid == false; }
        }

        /// <summary>
        /// This closes the phone device.
        /// </summary>
        public void Close()
        {
            try
            {
                // Close the phone handle
                _hPhone.Close();
            }
            catch
            {
            }

            _hPhone.SetHandleAsInvalid();
            GatherStatus();
        }

        /// <summary>
        /// Provides access to the handset hookswitch device.  It may be null if no handset is present.
        /// </summary>
        public PhoneHookswitch Handset
        {
            get { return _handset; }
        }

        /// <summary>
        /// Provides access to the headset hookswitch device.  It may be null if no headset is present.
        /// </summary>
        public PhoneHookswitch Headset
        {
            get { return _headset; }
        }

        /// <summary>
        /// Provides access to the speaker hookswitch device.  It may be null if no speaker is present.
        /// </summary>
        public PhoneHookswitch Speaker
        {
            get { return _speaker; }
        }

        /// <summary>
        /// Returns the buttons associated with the phone
        /// </summary>
        public PhoneButton[] Buttons
        {
            get { return _buttons.ToArray(); }
        }

        private PHONECAPS GatherCaps()
        {
            var pdc = new PHONECAPS();
            byte[] rawBuffer = null;

            int rc, neededSize = 1024;
            do
            {
                pdc.dwTotalSize = neededSize;
                IntPtr pLdc = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(pdc, pLdc, true);
                rc = NativeMethods.phoneGetDevCaps(_mgr.PhoneHandle, _deviceId, _negotiatedVersion, 0, pLdc);
                Marshal.PtrToStructure(pLdc, pdc);
                if (pdc.dwNeededSize > neededSize)
                {
                    neededSize = pdc.dwNeededSize;
                    rc = NativeMethods.PHONEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.PHONEERR_OK)
                {
                    rawBuffer = new byte[pdc.dwUsedSize];
                    Marshal.Copy(pLdc, rawBuffer, 0, pdc.dwUsedSize);
                }
                Marshal.FreeHGlobal(pLdc);
            }
            while (rc == NativeMethods.PHONEERR_STRUCTURETOOSMALL);
            _props = new PhoneCapabilities(pdc, rawBuffer);
            return pdc;
        }

        private void GatherStatus()
        {
            var ps = new PHONESTATUS();
            byte[] rawBuffer = null;

            if (!IsOpen)
            {
                _status = new PhoneStatus(this, ps, rawBuffer);
                return;
            }

            int rc, neededSize = 1024;
            do
            {
                ps.dwTotalSize = neededSize;
                IntPtr pLds = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(ps, pLds, true);
                rc = NativeMethods.phoneGetStatus(Handle, pLds);
                Marshal.PtrToStructure(pLds, ps);
                if (ps.dwNeededSize > neededSize)
                {
                    neededSize = ps.dwNeededSize;
                    rc = NativeMethods.PHONEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.PHONEERR_OK)
                {
                    rawBuffer = new byte[ps.dwUsedSize];
                    Marshal.Copy(pLds, rawBuffer, 0, ps.dwUsedSize);
                }
                Marshal.FreeHGlobal(pLds);
            }
            while (rc == NativeMethods.PHONEERR_STRUCTURETOOSMALL);
            
            _status = new PhoneStatus(this, ps, rawBuffer);
        }

        private void PhoneCallback(TapiEvent dwMessage, IntPtr dwParam1, IntPtr dwParam2, IntPtr dwParam3)
        {
            switch (dwMessage)
            {
                case TapiEvent.PHONE_BUTTON:
                    {
                        int buttonId = dwParam1.ToInt32();
                        if (buttonId >= 0 && buttonId < _buttons.Count)
                        {
                            PhoneButton pb = _buttons[buttonId];
                            pb.ProcessButtonNotify((ButtonMode)dwParam2.ToInt32(), (ButtonState)dwParam3.ToInt32());
                            if (PhoneButtonPressed != null)
                                PhoneButtonPressed(this, new PhoneButtonPressEventArgs(this, pb, (ButtonState) dwParam3.ToInt32()));
                        }
                    }
                    break;

                case TapiEvent.PHONE_CLOSE:
                    _hPhone.SetHandleAsInvalid();
                    GatherStatus();
                    break;

                case TapiEvent.PHONE_DEVSPECIFIC:
                    OnDeviceSpecific(dwParam1, dwParam2, dwParam3);
                    break;

                case TapiEvent.PHONE_STATE:
                    GatherStatus();
                    _display.Invalidate();
                    if (PhoneStateChanged != null)
                        PhoneStateChanged(this, new PhoneStateEventArgs(this, (PhoneStateChangeTypes) dwParam1));
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false, string.Format("Unknown Tapi Event {0} encountered in Phone Handler", dwMessage.ToString()));
                    break;
            }
        }

        private void OnDeviceSpecific(IntPtr param1, IntPtr param2, IntPtr param3)
        {
            if (_devsCallback != null)
                _devsCallback(this, new DeviceSpecificEventArgs(this, param1, param2, param3));
        }

        /// <summary>
        /// This method disposes of resources owned by the phone device
        /// </summary>
        public void Dispose()
        {
            _hPhone.Dispose();
        }
    }
}
