// Events.cs
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

namespace JulMar.Atapi
{
    /// <summary>
    /// Sent along with the New Call EventHandler
    /// </summary>
    public class NewCallEventArgs : EventArgs
    {
        /// <summary>
        /// Call which was created
        /// </summary>
        public readonly TapiCall Call;
        /// <summary>
        /// Current privileges for the given call
        /// </summary>
        public readonly Privilege Privilege;
        
        /// <summary>
        /// Constructor
        /// </summary>
        internal NewCallEventArgs(TapiCall call, Privilege priv)
        {
            Call = call;
            Privilege = priv;
        }
    }

    /// <summary>
    /// Sent along with the CallState change Call state change EventHandler
    /// </summary>
    public class CallStateEventArgs : EventArgs
    {
        /// <summary>
        /// New call state for the call.
        /// </summary>
        public readonly CallState CallState;
        /// <summary>
        /// Previous call state for the call.
        /// </summary>
        public readonly CallState OldCallState;
        /// <summary>
        /// Media modes presented on the call.
        /// </summary>
        public readonly MediaModes MediaModes;
        /// <summary>
        /// The call that has just changed
        /// </summary>
        public readonly TapiCall Call;

        /// <summary>
        /// Constructor
        /// </summary>
        internal CallStateEventArgs(TapiCall call, CallState newState, CallState oldState, MediaModes modes)
        {
            Call = call;
            CallState = newState;
            OldCallState = oldState;
            MediaModes = modes;
        }
    }

    /// <summary>
    /// Sent with a LINECALLSTATE_CONNECTED event
    /// </summary>
    public class ConnectedCallStateEventArgs : CallStateEventArgs
    {
        /// <summary>
        /// The additional connection mode type if available.
        /// </summary>
        public readonly ConnectModes Mode;
        internal ConnectedCallStateEventArgs(TapiCall call, CallState newState, CallState oldState, ConnectModes mode, MediaModes modes) 
            : base(call, newState, oldState, modes)
        {
            Mode = mode;
        }
    }

    /// <summary>
    /// Sent with a LINECALLSTATE_DISCONNECTED event
    /// </summary>
    public class DisconnectedCallStateEventArgs : CallStateEventArgs
    {
        /// <summary>
        /// The additional disconnection mode type if available.
        /// </summary>
        public readonly DisconnectModes Mode;
        internal DisconnectedCallStateEventArgs(TapiCall call, CallState newState, CallState oldState, DisconnectModes mode, MediaModes modes)
            : base(call, newState, oldState, modes)
        {
            Mode = mode;
        }
    }

    /// <summary>
    /// Sent with a LINECALLSTATE_BUSY event
    /// </summary>
    public class BusyCallStateEventArgs : CallStateEventArgs
    {
        /// <summary>
        /// The additional busy mode type if available.
        /// </summary>
        public readonly BusyModes Mode;
        internal BusyCallStateEventArgs(TapiCall call, CallState newState, CallState oldState, BusyModes mode, MediaModes modes)
            : base(call, newState, oldState, modes)
        {
            Mode = mode;
        }
    }

    /// <summary>
    /// Sent with a LINECALLSTATE_DIALTONE event
    /// </summary>
    public class DialtoneCallStateEventArgs : CallStateEventArgs
    {
        /// <summary>
        /// The additional dialtone mode type if available.
        /// </summary>
        public readonly DialtoneModes Mode;
        internal DialtoneCallStateEventArgs(TapiCall call, CallState newState, CallState oldState, DialtoneModes mode, MediaModes modes)
            : base(call, newState, oldState, modes)
        {
            Mode = mode;
        }
    }

    /// <summary>
    /// Sent with a LINECALLSTATE_OFFERING event
    /// </summary>
    public class OfferingCallStateEventArgs : CallStateEventArgs
    {
        /// <summary>
        /// The additional offering mode type if available.
        /// </summary>
        public readonly OfferingModes Mode;
        internal OfferingCallStateEventArgs(TapiCall call, CallState newState, CallState oldState, OfferingModes mode, MediaModes modes)
            : base(call, newState, oldState, modes)
        {
            Mode = mode;
        }
    }

    /// <summary>
    /// Sent with a LINECALLSTATE_SPECIALINFO event
    /// </summary>
    public class SpecialInfoCallStateEventArgs : CallStateEventArgs
    {
        /// <summary>
        /// The additional special info mode type if available.
        /// </summary>
        public readonly SpecialInfoModes Mode;
        internal SpecialInfoCallStateEventArgs(TapiCall call, CallState newState, CallState oldState, SpecialInfoModes mode, MediaModes modes)
            : base(call, newState, oldState, modes)
        {
            Mode = mode;
        }
    }

    /// <summary>
    /// Sent with a LINECALLSTATE_CONFERENCED event
    /// </summary>
    public class ConferencedCallStateEventArgs : CallStateEventArgs
    {
        /// <summary>
        /// The conference call owner
        /// </summary>
        public readonly TapiCall ConferenceOwner;
        internal ConferencedCallStateEventArgs(TapiCall call, CallState newState, CallState oldState, TapiCall conferenceOwner, MediaModes modes)
            : base(call, newState, oldState, modes)
        {
            ConferenceOwner = conferenceOwner;
        }
    }

    /// <summary>
    /// Sent when a call changes (other than state)
    /// </summary>
    public class CallInfoChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Call that has new information
        /// </summary>
        public readonly TapiCall Call;
        /// <summary>
        /// Item that changed (bitmask)
        /// </summary>
        public readonly CallInfoChangeTypes Change;

        internal CallInfoChangeEventArgs(TapiCall call, CallInfoChangeTypes change)
        {
            Call = call;
            Change = change;
        }
    }

    /// <summary>
    /// Sent with the LINEDEVSTATE_RINGING message
    /// </summary>
    public class RingEventArgs : EventArgs
    {
        /// <summary>
        /// The line that is ringing
        /// </summary>
        public readonly TapiLine Line;
        /// <summary>
        /// The number of rings which have occurred.
        /// </summary>
        public readonly int RingCount;
        /// <summary>
        /// The current ringer type.
        /// </summary>
        public readonly int RingerStyle;

        /// <summary>
        /// Constructor
        /// </summary>
        internal RingEventArgs(TapiLine line, int mode, int rcount)
        {
            Line = line;
            RingerStyle = mode;
            RingCount = rcount;
        }
    }

    /// <summary>
    /// Data sent with the AddressInfo change event
    /// </summary>
    public class AddressInfoChangeEventArgs : EventArgs
    {
        /// <summary>
        /// The address which has changed
        /// </summary>
        public readonly TapiAddress Address;
        /// <summary>
        /// The change(s) which have occurred.
        /// </summary>
        public readonly AddressInfoChangeTypes Change;
        internal AddressInfoChangeEventArgs(TapiAddress addr, AddressInfoChangeTypes change)
        {
            Address = addr;
            Change = change;
        }
    }

    /// <summary>
    /// Data sent with the Line Info change event.
    /// </summary>
    public class LineInfoChangeEventArgs : EventArgs
    {
        /// <summary>
        /// The line which has changed.
        /// </summary>
        public readonly TapiLine Line;
        /// <summary>
        /// The change(s) which have occurred.
        /// </summary>
        public readonly LineInfoChangeTypes Change;
        internal LineInfoChangeEventArgs(TapiLine line, LineInfoChangeTypes change)
        {
            Line = line;
            Change = change;
        }
    }

    /// <summary>
    /// Data sent with a <see cref="TapiCall.BeginMonitoringDigits"/> event.
    /// </summary>
    public class DigitDetectedEventArgs : EventArgs
    {
        /// <summary>
        /// The call where the digit was encountered
        /// </summary>
        public readonly TapiCall Call;
        /// <summary>
        /// The digit itself
        /// </summary>
        public readonly string Digit;
        /// <summary>
        /// The digit mode that was detected.
        /// </summary>
        public readonly DigitModes DigitMode;
        internal DigitDetectedEventArgs(TapiCall call, string digit, int digitMode)
        {
            Call = call;
            Digit = digit;
            DigitMode = (DigitModes)digitMode;
        }
    }

    /// <summary>
    /// This is passed with the <see cref="TapiCall.BeginMonitoringTones"/> callback.
    /// </summary>
    public class ToneDetectedEventArgs : EventArgs
    {
        /// <summary>
        /// The call where the tone was encountered
        /// </summary>
        public readonly TapiCall Call;
        /// <summary>
        /// The tone detected
        /// </summary>
        public readonly MonitorTone Tone;
        internal ToneDetectedEventArgs(TapiCall call, MonitorTone tone)
        {
            Call = call;
            Tone = tone;
        }
    }

    /// <summary>
    /// Event passed with the Device Specific event handler
    /// </summary>
    public class DeviceSpecificEventArgs : EventArgs
    {
        /// <summary>
        /// The line associated with the Device Specific request
        /// </summary>
        public readonly TapiLine Line;
        /// <summary>
        /// The call associated with the device specific request (may be null).
        /// </summary>
        public readonly TapiCall Call;
        /// <summary>
        /// The phone associated with the device specific request (may be null).
        /// </summary>
        public readonly TapiPhone Phone;
        /// <summary>
        /// Device-specific parameter 1
        /// </summary>
        public readonly IntPtr Param1;
        /// <summary>
        /// Device-specific parameter 1
        /// </summary>
        public readonly IntPtr Param2;
        /// <summary>
        /// Device-specific parameter 1
        /// </summary>
        public readonly IntPtr Param3;

        internal DeviceSpecificEventArgs(TapiLine line, IntPtr p1, IntPtr p2, IntPtr p3)
        {
            Line = line;
            Param1 = p1;
            Param2 = p2;
            Param3 = p3;
        }

        internal DeviceSpecificEventArgs(TapiPhone phone, IntPtr p1, IntPtr p2, IntPtr p3)
        {
            Phone = phone;
            Param1 = p1;
            Param2 = p2;
            Param3 = p3;
        }

        internal DeviceSpecificEventArgs(TapiCall call, IntPtr p1, IntPtr p2, IntPtr p3)
        {
            Line = call.Line;
            Call = call;
            Param1 = p1;
            Param2 = p2;
            Param3 = p3;
        }
    }

    /// <summary>
    /// This event is sent along with the <see cref="TapiManager.LineAdded"/> event.
    /// </summary>
    public class LineAddedEventArgs : EventArgs
    {
        /// <summary>
        /// The new line being added to the system.
        /// </summary>
        public readonly TapiLine Line;

        internal LineAddedEventArgs(TapiLine newLine)
        {
            Line = newLine;
        }
    }

    /// <summary>
    /// This event is sent along with the <see cref="TapiManager.LineRemoved"/> event.
    /// </summary>
    public class LineRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// The new line being added to the system.
        /// </summary>
        public readonly TapiLine Line;

        internal LineRemovedEventArgs(TapiLine line)
        {
            Line = line;
        }
    }

    /// <summary>
    /// This event is sent along with the <see cref="TapiManager.PhoneAdded"/> event.
    /// </summary>
    public class PhoneAddedEventArgs : EventArgs
    {
        /// <summary>
        /// The new phone being added to the system.
        /// </summary>
        public readonly TapiPhone Phone;

        internal PhoneAddedEventArgs(TapiPhone newPhone)
        {
            Phone = newPhone;
        }
    }

    /// <summary>
    /// This event is sent along with the <see cref="TapiManager.PhoneRemoved"/> event.
    /// </summary>
    public class PhoneRemovedEventArgs : EventArgs
    {
        /// <summary>
        /// The new phone being added to the system.
        /// </summary>
        public readonly TapiPhone Phone;

        internal PhoneRemovedEventArgs(TapiPhone phone)
        {
            Phone = phone;
        }
    }

    ///<summary>
    /// This class is used to report phone state change notifications
    ///</summary>
    public class PhoneStateEventArgs : EventArgs
    {
        /// <summary>
        /// The phone device where the state changed
        /// </summary>
        public readonly TapiPhone Phone;

        /// <summary>
        /// The change(s) which have occurred
        /// </summary>
        public readonly PhoneStateChangeTypes Change;

        internal PhoneStateEventArgs(TapiPhone phone, PhoneStateChangeTypes change)
        {
            Phone = phone;
            Change = change;
        }
    }

    /// <summary>
    /// This class is used to report button press notifications
    /// </summary>
    public class PhoneButtonPressEventArgs : EventArgs
    {
        /// <summary>
        /// The phone device 
        /// </summary>
        public readonly TapiPhone Phone;

        /// <summary>
        /// The button that was pressed
        /// </summary>
        public readonly PhoneButton Button;

        /// <summary>
        /// Button status
        /// </summary>
        public readonly ButtonState State;

        internal PhoneButtonPressEventArgs(TapiPhone phone, PhoneButton button, ButtonState state)
        {
            Phone = phone;
            Button = button;
            State = state;
        }
    }
}
