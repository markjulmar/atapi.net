// Enumeration.cs
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
using JulMar.Atapi.Interop;

namespace JulMar.Atapi
{
    /// <summary>
    /// This identifies address format, such as standard phone number or e-mail address. 
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// Not supplied
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Address type is a standard phone number. 
        /// </summary>
        PhoneNumber = NativeMethods.LINEADDRESSTYPE_PHONENUMBER,
        /// <summary>
        /// Address type is Session Description Protocol (SDP) conference.
        /// </summary>
        SDP = NativeMethods.LINEADDRESSTYPE_SDP,
        /// <summary>
        /// Address type is an e-mail name. 
        /// </summary>
        Email = NativeMethods.LINEADDRESSTYPE_EMAILNAME,
        /// <summary>
        /// Address type is a domain name. 
        /// </summary>
        DomainName = NativeMethods.LINEADDRESSTYPE_DOMAINNAME,
        /// <summary>
        /// Address type is an IP address. 
        /// </summary>
        IPAddress = NativeMethods.LINEADDRESSTYPE_IPADDRESS
    }

    /// <summary>
    /// Supported TAPI versions
    /// </summary>
    public enum TapiVersion
    {
        /// <summary>
        /// TAPI 1.3 - Windows 3.1
        /// </summary>
        V13 = 0x10003,
        /// <summary>
        /// TAPI 1.4 - Windows 95
        /// </summary>
        V14 = 0x10004,
        /// <summary>
        /// TAPI 2.0 - Windows NT 4
        /// </summary>
        V20 = 0x20000,
        /// <summary>
        /// TAPI 2.1 - update
        /// </summary>
        V21 = 0x20001,
        /// <summary>
        /// TAPI 3.0 - Windows 2000
        /// </summary>
        V30 = 0x30000,
        /// <summary>
        /// TAPI 3.1 - Windows XP/2003
        /// </summary>
        V31 = 0x30001
    }

    /// <summary>
    /// These modes are used to select a certain quality of service for the requested connection from the underlying telephone network. 
    /// Bearer modes available on a given line are a device capability of the line.
    /// </summary>
    [Flags]
    public enum BearerModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// This is a regular 3.1 kHz analog voice-grade bearer service. Bit integrity is not assured. Voice can support fax and modem media types. 
        /// </summary>
        Voice = NativeMethods.LINEBEARERMODE_VOICE,
        /// <summary>
        /// This corresponds to G.711 speech transmission on the call. The network can use processing techniques such as analog transmission, 
        /// echo cancellation, and compression/decompression. Bit integrity is not assured. Speech is not intended to support fax and modem media types.
        /// </summary>
        Speech = NativeMethods.LINEBEARERMODE_SPEECH,
        /// <summary>
        /// The multiuse mode defined by ISDN.
        /// </summary>
        MultiUse = NativeMethods.LINEBEARERMODE_MULTIUSE,
        /// <summary>
        /// The unrestricted data transfer on the call. The data rate is specified separately.
        /// </summary>
        Data = NativeMethods.LINEBEARERMODE_DATA,
        /// <summary>
        /// The alternate transfer of speech or unrestricted data on the same call (ISDN).
        /// </summary>
        AlternateSpeechData = NativeMethods.LINEBEARERMODE_ALTSPEECHDATA,
        /// <summary>
        /// This corresponds to a non-call-associated signaling connection from the application to the service provider or switch.
        /// </summary>
        NonCallSignaling = NativeMethods.LINEBEARERMODE_NONCALLSIGNALING,
        /// <summary>
        /// When a call is active in LINEBEARERMODE_PASSTHROUGH, the service provider gives direct access to the attached hardware 
        /// for control by the application. This mode is used primarily by applications desiring temporary direct control over asynchronous modems, 
        /// accessed through the Win32 communication functions, for the purpose of configuring or using special features not otherwise supported 
        /// by the service provider.
        /// </summary>
        Passthrough = NativeMethods.LINEBEARERMODE_PASSTHROUGH,
        /// <summary>
        /// Bearer service for digital data in which only the low-order seven bits of each octet may contain user data 
        /// (for example, for Switched 56kbit/s service).
        /// </summary>
        RestrictedData = NativeMethods.LINEBEARERMODE_RESTRICTEDDATA
    }

    /// <summary>
    /// These constants describe media types (or modes) of a communications session or call. 
    /// </summary>
    [Flags]
    public enum MediaModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// A media stream exists but its mode is not currently known and may become known later. This would correspond to a call with an 
        /// unclassified media type. In typical analog telephony environments, an incoming call's media type may be unknown until after 
        /// the call has been answered and the media stream has been filtered to make a determination. If the unknown media-mode flag is set, 
        /// other media flags can also be set. This is used to signify that the media is unknown but that it is likely to be one of the 
        /// other selected media types.
        /// </summary>
        Unknown = NativeMethods.LINEMEDIAMODE_UNKNOWN,
        /// <summary>
        /// Voice energy was detected on the call, and the call is handled as an interactive voice call with humans on both ends.
        /// </summary>
        InteractiveVoice = NativeMethods.LINEMEDIAMODE_INTERACTIVEVOICE,
        /// <summary>
        /// Voice energy was detected on the call, and the voice is locally handled by an automated application such as with an 
        /// answering machine application. When a service provider cannot distinguish between interactive and automated voice on an 
        /// incoming call, it will report the call as interactive voice.
        /// </summary>
        AutomatedVoice = NativeMethods.LINEMEDIAMODE_AUTOMATEDVOICE,
        /// <summary>
        /// A data modem session on the call. Current modem protocols require the called station to initiate the handshake. For an incoming data modem call, 
        /// the application can typically make no positive detection. How the service provider makes this determination is its choice. For example, 
        /// a period of silence just after answering an incoming call can be used as a heuristic to decide that this might be a data modem call.
        /// </summary>
        DataModem = NativeMethods.LINEMEDIAMODE_DATAMODEM,
        /// <summary>
        /// A group 3 fax is being sent or received over the call.
        /// </summary>
        Group3Fax = NativeMethods.LINEMEDIAMODE_G3FAX,
        /// <summary>
        /// A Telephony Devices for the Deaf (TDD) () session on the call.
        /// </summary>
        TDD = NativeMethods.LINEMEDIAMODE_TDD,
        /// <summary>
        /// A group 4 fax is being sent or received over the call.
        /// </summary>
        Group4Fax = NativeMethods.LINEMEDIAMODE_G4FAX,
        /// <summary>
        /// A digital data stream of unspecified format. 
        /// </summary>
        DigitalData = NativeMethods.LINEMEDIAMODE_DIGITALDATA,
        /// <summary>
        /// A teletex session on the call. Teletex is one of the telematic services.
        /// </summary>
        Teletex = NativeMethods.LINEMEDIAMODE_TELETEX,
        /// <summary>
        /// A videotex session on the call. Videotex is one the telematic services.
        /// </summary>
        Videotex = NativeMethods.LINEMEDIAMODE_VIDEOTEX,
        /// <summary>
        /// A telex session on the call. Telex is one of the telematic services.
        /// </summary>
        Telex = NativeMethods.LINEMEDIAMODE_TELEX,
        /// <summary>
        /// A mixed session on the call. Mixed is one of the ISDN telematic services.
        /// </summary>
        Mixed = NativeMethods.LINEMEDIAMODE_MIXED,
        /// <summary>
        /// An Analog Display Services Interface (ADSI) session on the call. ADSI enhances voice calls with alphanumeric information downloaded to 
        /// the phone and the use of soft buttons on the phone.
        /// </summary>
        ADSI = NativeMethods.LINEMEDIAMODE_ADSI,
        /// <summary>
        /// The media type of the call is VoiceView.
        /// </summary>
        VoiceView = NativeMethods.LINEMEDIAMODE_VOICEVIEW,
        /// <summary>
        /// The media type of the call is video conference
        /// </summary>
        Video = NativeMethods.LINEMEDIAMODE_VIDEO,
        /// <summary>
        /// All media modes
        /// </summary>
        All = (Unknown | InteractiveVoice | AutomatedVoice | DataModem | Group3Fax | TDD | Group4Fax | DigitalData | Teletex | Videotex | Telex | Mixed | ADSI | VoiceView + Video)
    }

    /// <summary>
    /// These constants describe different selections that are used when generating line tones. 
    /// </summary>
    [Flags]
    public enum ToneModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// The tone is a custom tone defined by its component frequencies.
        /// </summary>
        Custom = NativeMethods.LINETONEMODE_CUSTOM,
        /// <summary>
        /// The tone is ringback tone. Exact definition is service-provider defined.
        /// </summary>
        Ringback = NativeMethods.LINETONEMODE_RINGBACK,
        /// <summary>
        /// The tone is a busy tone. Exact definition is service-provider defined.
        /// </summary>
        Busy = NativeMethods.LINETONEMODE_BUSY,
        /// <summary>
        /// The tone is a beep, such as that used to announce the beginning of a recording. Exact definition is service-provider defined.
        /// </summary>
        Beep = NativeMethods.LINETONEMODE_BEEP,
        /// <summary>
        /// The tone is a billing information tone such as a credit card prompt tone. Exact definition is service-provider defined.
        /// </summary>
        Billing = NativeMethods.LINETONEMODE_BILLING,
    }

    /// <summary>
    /// These constants describe special dialing characters which may be supported through Dial or MakeCall
    /// </summary>
    [Flags]
    public enum SpecialDialingChars
    {
        /// <summary>
        /// Billing tone "$"
        /// </summary>
        Billing = 1,
        /// <summary>
        /// Quite "@"
        /// </summary>
        Quiet = 2,
        /// <summary>
        /// Wait for dialtone "W"
        /// </summary>
        Dialtone = 4
    }

    /// <summary>
    /// These bit-flag constants describe how an existing active call on a line device is affected by answering another offering call on the same line.
    /// </summary>
    [Flags]
    public enum AnswerModes
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Answering another call on the same line has no effect on the existing active call on the line.
        /// </summary>
        None = NativeMethods.LINEANSWERMODE_NONE,
        /// <summary>
        /// The currently active call will automatically be dropped.
        /// </summary>
        Drop = NativeMethods.LINEANSWERMODE_DROP,
        /// <summary>
        /// The currently active call will automatically be placed on hold.
        /// </summary>
        Hold = NativeMethods.LINEANSWERMODE_HOLD
    }

    /// <summary>
    /// These constants indicate the privileges for a given line opener
    /// </summary>
    public enum Privilege
    {
        /// <summary>
        /// No privileges
        /// </summary>
        None,
        /// <summary>
        /// Read-only
        /// </summary>
        Monitor,
        /// <summary>
        /// Read-write
        /// </summary>
        Owner
    }

    /// <summary>
    /// The call states for a given TapiCall
    /// </summary>
    [Flags]
    public enum CallState
    {
        /// <summary>
        /// No call-state available
        /// </summary>
        None = 0,
        /// <summary>
        /// The call exists but has not been connected. No activity exists on the call. This means that no call is currently active. 
        /// A call can never transition out of the idle state.
        /// </summary>
        Idle = NativeMethods.LINECALLSTATE_IDLE,                
        /// <summary>
        /// The call is being offered to the station, signaling the arrival of a new call. The offering state is not the same as causing a 
        /// phone or computer to ring. In some environments, a call in the offering state does not ring the user until the switch instructs 
        /// the line to ring. For example this state is in use when an incoming call appears on several station sets but only the primary 
        /// address rings. The instruction to ring does not affect any call states.
        /// </summary>
        Offering = NativeMethods.LINECALLSTATE_OFFERING,    
        /// <summary>
        /// The call was in the offering state and has been accepted. This indicates to other, monitoring, applications that the current owner 
        /// application has claimed responsibility for answering the call. In ISDN, the accepted state is entered when the called-party equipment 
        /// sends a message to the switch indicating that it is willing to present the call to the called person. This has the side effect of alerting 
        /// (ringing) the users at both ends of the call. An incoming call can always be immediately answered without first being separately accepted.
        /// </summary>
        Accepted = NativeMethods.LINECALLSTATE_ACCEPTED,   
        /// <summary>
        /// The call is receiving a dial tone from the switch. This means that the switch is ready to receive a dialed number.
        /// </summary>
        Dialtone = NativeMethods.LINECALLSTATE_DIALTONE,           
        /// <summary>
        /// The originator is dialing digits on the call. The dialed digits are collected by the switch.
        /// </summary>
        Dialing = NativeMethods.LINECALLSTATE_DIALING,           
        /// <summary>
        /// The station to be called has been reached, and the destination's switch is generating a ring tone back to the originator. A ringback means that 
        /// the destination address is being alerted to the call.
        /// </summary>
        Ringback = NativeMethods.LINECALLSTATE_RINGBACK,           
        /// <summary>
        /// The call is receiving a busy tone. A busy tone indicates that the call cannot be completed. This occurs if either a 
        /// circuit (trunk) or the remote party's station are in use.
        /// </summary>
        Busy = NativeMethods.LINECALLSTATE_BUSY,                
        /// <summary>
        /// The call is receiving a special information signal that precedes a prerecorded announcement indicating why a call cannot be completed. 
        /// </summary>
        SpecialInfo = NativeMethods.LINECALLSTATE_SPECIALINFO,   
        /// <summary>
        /// The call has been established and the connection is made. Information is able to flow over the call between the 
        /// originating address and the destination address.
        /// </summary>
        Connected = NativeMethods.LINECALLSTATE_CONNECTED,    
        /// <summary>
        /// Dialing has completed and the call is proceeding through the switch or telephone network. This occurs after dialing is complete and before the call 
        /// reaches the dialed party, as indicated by ringback, busy, or answer.
        /// </summary>
        Proceeding = NativeMethods.LINECALLSTATE_PROCEEDING,   
        /// <summary>
        /// The call is on hold by the switch. This frees the physical line. This allows another call to use the line.
        /// </summary>
        OnHold = NativeMethods.LINECALLSTATE_ONHOLD, 
        /// <summary>
        /// The call is a member of a conference call and is logically in the connected state.
        /// </summary>
        Conferenced = NativeMethods.LINECALLSTATE_CONFERENCED,         
        /// <summary>
        /// The call is currently on hold while it is being added to a conference.
        /// </summary>
        OnHoldPendingConference = NativeMethods.LINECALLSTATE_ONHOLDPENDCONF,   
        /// <summary>
        /// The call is currently on hold awaiting transfer to another number.
        /// </summary>
        OnHoldPendingTransfer = NativeMethods.LINECALLSTATE_ONHOLDPENDTRANSFER,  
        /// <summary>
        /// The remote party has disconnected from the call.
        /// </summary>
        Disconnected = NativeMethods.LINECALLSTATE_DISCONNECTED,  
        /// <summary>
        /// The call exists, but its state is currently unknown. This may be the result of poor call progress detection by the service provider. 
        /// A call state message with the call state set to unknown may also be generated to inform the TAPI DLL about a new call at a time 
        /// when the actual call state of the call is not exactly known
        /// </summary>
        Unknown = NativeMethods.LINECALLSTATE_UNKNOWN             
    }

    /// <summary>
    /// These bit-flag constants describe different substates of a connected call. A mode is available as call 
    /// status to the application after the call state transitions to connected, and within the LINE_CALLSTATE message 
    /// indicating the call is in LINECALLSTATE_CONNECTED. These values are used when the call is on an address that is 
    /// shared (bridged) with other stations primarily electronic key systems. 
    /// </summary>
    [Flags]
    public enum ConnectModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates that the call is connected at the current station (the current station is a participant in the call). If the call
        /// state mode is zero (0), the application should assume that the value is "active" This would be the situation on a non-bridged address. 
        /// The mode can switch between ACTIVE and INACTIVE during a call if the user joins and leaves the call through manual action. In such 
        /// a bridged situation, a <see cref="TapiCall.Drop(byte[])"/> or <see cref="TapiCall.Hold"/> function call may possibly not actually drop the call 
        /// or place it on hold, because the status of other stations on the call may govern. For example, attempting to "hold" a call when 
        /// other stations are participating won't be possible. Instead, the call may simply be changed to the INACTIVE mode if it 
        /// remains CONNECTED at other stations. 
        /// </summary>
        Active = NativeMethods.LINECONNECTEDMODE_ACTIVE,
        /// <summary>
        /// Indicates that the call is active at one or more other stations, but the current station is not a participant in the call. If the call state 
        /// mode is ZERO, the application should assume that the value is "active". This would be the situation on a non-bridged address. A call in 
        /// the INACTIVE state can be joined using Answer. Many operations that are valid in calls in the CONNECTED state can be impossible in 
        /// the INACTIVE mode, such as monitoring for tones and digits, because the station is not actually participating in the call; monitoring is usually 
        /// suspended, although not canceled, while the call is in the INACTIVE mode.
        /// </summary>
        Inactive = NativeMethods.LINECONNECTEDMODE_INACTIVE,
        /// <summary>
        /// Indicates that the station is an active participant in the call, but that the remote party has placed the call on hold. The other party 
        /// considers the call to be in the onhold state. Normally, such information is available only when both endpoints of the call fall within
        /// the same switching domain.
        /// </summary>
        ActiveHeld = NativeMethods.LINECONNECTEDMODE_ACTIVEHELD,
        /// <summary>
        /// Indicates that the station is not an active participant in the call, and that the remote party has placed the call on hold.
        /// </summary>
        InactiveHeld = NativeMethods.LINECONNECTEDMODE_INACTIVEHELD,
        /// <summary>
        /// Indicates that the service provider received affirmative notification that the call has entered the connected state. For example, 
        /// the notification was received through answer supervision or similar mechanisms.
        /// </summary>
        Confirmed = NativeMethods.LINECONNECTEDMODE_CONFIRMED   
    }

    /// <summary>
    /// These bit-flag constants describe different reasons for a remote disconnect request. A disconnect mode is 
    /// available as call status to the application after the call state transitions to disconnected. 
    /// </summary>
    [Flags]
    public enum DisconnectModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// This is a normal disconnect request by the remote party. The call was terminated normally.
        /// </summary>
        Normal = NativeMethods.LINEDISCONNECTMODE_NORMAL,
        /// <summary>
        /// The reason for the disconnect request is unknown but may become known later. 
        /// </summary>
        Unknown = NativeMethods.LINEDISCONNECTMODE_UNKNOWN,
        /// <summary>
        /// The remote user has rejected the call.
        /// </summary>
        Reject = NativeMethods.LINEDISCONNECTMODE_REJECT,
        /// <summary>
        /// The call was picked up from elsewhere.
        /// </summary>
        Pickup = NativeMethods.LINEDISCONNECTMODE_PICKUP,
        /// <summary>
        /// The call was forwarded by the switch. 
        /// </summary>
        Forwarded = NativeMethods.LINEDISCONNECTMODE_FORWARDED,
        /// <summary>
        /// The remote user's station is busy.
        /// </summary>
        Busy = NativeMethods.LINEDISCONNECTMODE_BUSY,
        /// <summary>
        /// The remote user's station does not answer.
        /// </summary>
        NoAnswer = NativeMethods.LINEDISCONNECTMODE_NOANSWER,
        /// <summary>
        /// The destination address is invalid. 
        /// </summary>
        BadAddress = NativeMethods.LINEDISCONNECTMODE_BADADDRESS,
        /// <summary>
        /// The remote user could not be reached.
        /// </summary>
        Unreachable = NativeMethods.LINEDISCONNECTMODE_UNREACHABLE,
        /// <summary>
        /// The network is congested.
        /// </summary>
        Congestion = NativeMethods.LINEDISCONNECTMODE_CONGESTION,
        /// <summary>
        /// The remote user's station equipment is incompatible with the type of call requested.
        /// </summary>
        Incompatible = NativeMethods.LINEDISCONNECTMODE_INCOMPATIBLE,
        /// <summary>
        /// The reason for the disconnect is unavailable and will not become known later.
        /// </summary>
        Unavailable = NativeMethods.LINEDISCONNECTMODE_UNAVAIL,
        /// <summary>
        /// A dial tone was not detected within a service-provider defined timeout, at a point during dialing when one was expected 
        /// (such as at a "W" in the dialable string). 
        /// </summary>
        NoDialtone = NativeMethods.LINEDISCONNECTMODE_NODIALTONE,
        /// <summary>
        /// The call could not be connected because the destination number has been changed, but automatic redirection to the new number is not provided.
        /// </summary>
        NumberChanged = NativeMethods.LINEDISCONNECTMODE_NUMBERCHANGED,
        /// <summary>
        /// The call could not be connected or was disconnected because the destination device is out of order. This could occur if for 
        /// example there were hardware failure.
        /// </summary>
        OutOfOrder = NativeMethods.LINEDISCONNECTMODE_OUTOFORDER,
        /// <summary>
        /// The call could not be connected or was disconnected because of a temporary failure in the network; the call can be reattempted later and is expected to eventually complete.
        /// </summary>
        TemporaryFailure = NativeMethods.LINEDISCONNECTMODE_TEMPFAILURE,
        /// <summary>
        /// The call could not be connected or was disconnected because the minimum quality of service could not be obtained or sustained.
        /// </summary>
        QosUnavailable = NativeMethods.LINEDISCONNECTMODE_QOSUNAVAIL,
        /// <summary>
        /// The call could not be connected because calls from the origination address are not being accepted at the destination address. 
        /// This differs from Rejected in that blocking is implemented in the network (a passive reject) while a rejection is 
        /// implemented in the destination equipment (an active reject). The blocking can be due to a specific exclusion of the origination address, 
        /// or because the destination accepts calls from only a selected set of origination address (closed user group). This is appropriate as a 
        /// blacklisted response. For example, a modem has received an answer, gone more than six seconds without detecting Ringback, failed to connect 
        /// a defined number of times, determines that the phone number is not valid to call, and issues a 'blacklisted' response.
        /// </summary>
        Blocked = NativeMethods.LINEDISCONNECTMODE_BLOCKED,
        /// <summary>
        /// The call could not be connected because the destination has invoked the Do Not Disturb feature.
        /// </summary>
        DoNotDisturb = NativeMethods.LINEDISCONNECTMODE_DONOTDISTURB,
        /// <summary>
        /// The call was cancelled.
        /// </summary>
        Canceled = NativeMethods.LINEDISCONNECTMODE_CANCELLED   
    }

    /// <summary>
    /// These bit-flag constants describe different busy signals that the switch or network can generate. 
    /// These busy signals typically indicate that a different resource that is required to make a call is currently in use. 
    /// </summary>
    [Flags]
    public enum BusyModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// The busy signal indicates that the called party's station is busy. This is usually signaled with a normal busy tone. 
        /// </summary>
        Station = NativeMethods.LINEBUSYMODE_STATION,
        /// <summary>
        /// The busy signal indicates that a trunk or circuit is busy. This is usually signaled with a fast busy tone. 
        /// </summary>
        Trunk = NativeMethods.LINEBUSYMODE_TRUNK,
        /// <summary>
        /// The busy signal's specific mode is currently unknown but may become known later. 
        /// </summary>
        Unknown = NativeMethods.LINEBUSYMODE_UNKNOWN,
        /// <summary>
        /// The busy signal's specific mode is unavailable and will not become known. 
        /// </summary>
        Unavailable = NativeMethods.LINEBUSYMODE_UNAVAIL
    }

    /// <summary>
    /// These bit-flag constants describe different types of dial tones which may be presented when the caller picks up a handset.
    /// </summary>
    [Flags]
    public enum DialtoneModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// This is a normal dial tone, which typically is a continuous tone.
        /// </summary>
        Normal = NativeMethods.LINEDIALTONEMODE_NORMAL,
        /// <summary>
        /// This is a special dial tone indicating that a certain condition, known by the switch or network, is currently in effect. 
        /// Special dial tones typically use an interrupted tone. As with a normal dial tone, this indicates that the switch is ready to 
        /// receive the number to be dialed.
        /// </summary>
        Special = NativeMethods.LINEDIALTONEMODE_SPECIAL,
        /// <summary>
        /// This is an internal dial tone, as within a PBX. 
        /// </summary>
        Internal = NativeMethods.LINEDIALTONEMODE_INTERNAL,
        /// <summary>
        /// This is an external (public network) dial tone.
        /// </summary>
        External = NativeMethods.LINEDIALTONEMODE_EXTERNAL,
        /// <summary>
        /// The dial tone mode is not currently known but may become known later.
        /// </summary>
        Unknown = NativeMethods.LINEDIALTONEMODE_UNKNOWN,
        /// <summary>
        /// The dial tone mode is unavailable and will not become known. 
        /// </summary>
        Unavailable = NativeMethods.LINEDIALTONEMODE_UNAVAIL
    }

    /// <summary>
    /// These bit-flag constants describe different substates of an offering call. A mode is available as call status to the application 
    /// after the call state transitions to offering, and within the LINE_CALLSTATE message indicating the call is in LINECALLSTATE_OFFERING. 
    /// These values are used when the call is on an address that is shared (bridged) with other stations, primarily electronic key systems. 
    /// </summary>
    [Flags]
    public enum OfferingModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates that the call is alerting at the current station and if any application is set up to automatically answer, it can do so. 
        /// If the call state mode is ZERO, the application should assume that the value is active. This would be the situation on a non-bridged address.
        /// </summary>
        Active = NativeMethods.LINEOFFERINGMODE_ACTIVE,
        /// <summary>
        /// Indicates that the call is being offered at more than one station, but the current station is not alerting. For example, it may be 
        /// an attendant station where the offering status is advisory, such as blinking a light. Software at the station set for 
        /// automatic answering should preferably not answer the call, because this should be the prerogative at the primary (alerting) station, 
        /// but TapiCall.Answer may be used to connect the call. 
        /// </summary>
        Inactive = NativeMethods.LINEOFFERINGMODE_INACTIVE
    }

    /// <summary>
    /// These bit-flag constants describes special information signals that the network can use to report various reporting 
    /// and network observation operations. They are special coded tone sequences transmitted at the beginning of network advisory recorded announcements. 
    /// </summary>
    [Flags]
    public enum SpecialInfoModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// This special information tone precedes a no circuit or an emergency announcement. These announcements are in the trunk blockage category.
        /// </summary>
        NoCircuit = NativeMethods.LINESPECIALINFO_NOCIRCUIT,
        /// <summary>
        /// This special information tone precedes a vacant number, AIS, Centrex number change and nonworking station, access code not dialed or 
        /// dialed in error, or manual intercept operator message. These conditions are in the customer irregularity category. This is also reported 
        /// when billing information is rejected and when the dialed address is blocked at the switch. 
        /// </summary>
        CustomerIrregularity = NativeMethods.LINESPECIALINFO_CUSTIRREG,
        /// <summary>
        /// This special information tone precedes a reorder announcement. This announcement is in the equipment irregularity category.
        /// This is also reported when the telephone is kept offhook too long. 
        /// </summary>
        Reorder = NativeMethods.LINESPECIALINFO_REORDER,
        /// <summary>
        /// Specifics about the special information tone are currently unknown but may become known later. 
        /// </summary>
        Unknown = NativeMethods.LINESPECIALINFO_UNKNOWN,
        /// <summary>
        /// Specifics about the special information tone are unavailable and will not become known. 
        /// </summary>
        Unavailable = NativeMethods.LINESPECIALINFO_UNAVAIL
    }

    /// <summary>
    /// These constants describe different types of inband digit generation. 
    /// </summary>
    [Flags]
    public enum DigitModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Uses rotary pulse sequences to signal digits. Valid digits are zero (0) through 9. 
        /// </summary>
        Pulse = NativeMethods.LINEDIGITMODE_PULSE,
        /// <summary>
        /// Uses DTMF tones to signal digits. Valid digits are zero (0) through 9, '*', '#', 'A', 'B', 'C', and 'D'.
        /// </summary>
        Dtmf = NativeMethods.LINEDIGITMODE_DTMF,
        /// <summary>
        /// Uses DTMF tones to signal digits and detect the down edges. Valid digits are zero (0) through 9, '*', '#', 'A', 'B', 'C', and 'D'.
        /// </summary>
        DtmfEnd = NativeMethods.LINEDIGITMODE_DTMFEND
    }

    /// <summary>
    /// These bit-flag constants describe the conditions under which buffered digit gathering is terminated. 
    /// </summary>
    [Flags]
    public enum DigitGatherComplete
    {
        /// <summary>
        /// No value
        /// </summary>
        None = 0,
        /// <summary>
        /// The requested number of digits has been gathered. The buffer is full.
        /// </summary>
        BufferFull = NativeMethods.LINEGATHERTERM_BUFFERFULL,
        /// <summary>
        /// One of the termination digits matched a received digit. The matched termination digit is the last digit in the buffer.
        /// </summary>
        DigitTermination = NativeMethods.LINEGATHERTERM_TERMDIGIT,
        /// <summary>
        /// The first digit timeout expired. The buffer contains no digits.
        /// </summary>
        FirstTimeout = NativeMethods.LINEGATHERTERM_FIRSTTIMEOUT,
        /// <summary>
        /// The inter-digit timeout expired. The buffer contains at least one digit.
        /// </summary>
        InterDigitTimeout = NativeMethods.LINEGATHERTERM_INTERTIMEOUT,
        /// <summary>
        /// The request was canceled by this application, by another application, or because the call terminated.
        /// </summary>
        Canceled = NativeMethods.LINEGATHERTERM_CANCEL
    }

    /// <summary>
    /// The roaming styles which may be presented on a cellular style call.
    /// </summary>
    [Flags]
    public enum RoamingModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// The roam mode is currently unknown but may become known later.
        /// </summary>
        Unknown = NativeMethods.LINEROAMMODE_UNKNOWN,
        /// <summary>
        /// The roam mode is unavailable and will not be known.
        /// </summary>
        Unavailable = NativeMethods.LINEROAMMODE_UNAVAIL,
        /// <summary>
        /// The line is connected to the home network node.
        /// </summary>
        Home = NativeMethods.LINEROAMMODE_HOME,
        /// <summary>
        /// The line is connected to the Roam-A carrier and calls are charged accordingly.
        /// </summary>
        RoamingCarrierA = NativeMethods.LINEROAMMODE_ROAMA,
        /// <summary>
        /// The line is connected to the Roam-B carrier and calls are charged accordingly.
        /// </summary>
        RoamingCarrierB = NativeMethods.LINEROAMMODE_ROAMB
    }

    /// <summary>
    /// The various ways an address can be shared between lines.
    /// </summary>
    [Flags]
    public enum AddressSharingModes
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// The address is bridged to one or more other stations. The first line to activate a call on the line will have exclusive access to the 
        /// address and calls that may exist on it. Other lines will not be able to use the bridged address while it is in use.
        /// </summary>
        BridgedExclusive = NativeMethods.LINEADDRESSSHARING_BRIDGEDEXCL,
        /// <summary>
        /// The address is bridged with one or more other stations. The first line to activate a call on the line will have exclusive access to 
        /// only the corresponding call. Other applications that use the address will result in new and separate call appearances.
        /// </summary>
        BridgedNew = NativeMethods.LINEADDRESSSHARING_BRIDGEDNEW,
        /// <summary>
        /// The address is bridged with one or more other lines. All bridged parties can share in calls on the address, which then functions as a conference.
        /// </summary>
        BridgedShared = NativeMethods.LINEADDRESSSHARING_BRIDGEDSHARED,
        /// <summary>
        /// This is an address whose idle/busy status is made available to this line.
        /// </summary>
        Monitored = NativeMethods.LINEADDRESSSHARING_MONITORED,
        /// <summary>
        /// The address is private to the user's line. It is not assigned to any other station.
        /// </summary>
        Private = NativeMethods.LINEADDRESSSHARING_PRIVATE

    }

    /// <summary>
    /// The origins for a given call
    /// </summary>
    [Flags]
    public enum CallOrigins
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// The call origin is currently unknown but may become known later. 
        /// </summary>
        Unknown = NativeMethods.LINECALLORIGIN_UNKNOWN,
        /// <summary>
        /// The call origin is not available and will never become known for this call.
        /// </summary>
        Unavailable = NativeMethods.LINECALLORIGIN_UNAVAIL,
        /// <summary>
        /// The call originated from this station as an outgoing call.
        /// </summary>
        Outbound = NativeMethods.LINECALLORIGIN_OUTBOUND,
        /// <summary>
        /// The call originated as an incoming call, but the service provider is unable to determine whether it came from another station on the same 
        /// switch or from an external line.
        /// </summary>
        Inbound = NativeMethods.LINECALLORIGIN_INBOUND,
        /// <summary>
        /// The call originated as an incoming call at a station internal to the same switching environment.
        /// </summary>
        Internal = NativeMethods.LINECALLORIGIN_INTERNAL,
        /// <summary>
        /// The call originated as an incoming call on an external line.
        /// </summary>
        External = NativeMethods.LINECALLORIGIN_EXTERNAL,
        /// <summary>
        /// The call is a conference call. That is, it is the application's connection to the conference bridge in the switch.
        /// </summary>
        Conference = NativeMethods.LINECALLORIGIN_CONFERENCE
    }

    /// <summary>
    /// The reason for a call
    /// </summary>
    [Flags]
    public enum CallReasons
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// This is a direct incoming or outgoing call.
        /// </summary>
        Direct = NativeMethods.LINECALLREASON_DIRECT,        
        /// <summary>
        /// This call was forwarded from another extension that was busy at the time of the call.
        /// </summary>
        BusyForward = NativeMethods.LINECALLREASON_FWDBUSY,        
        /// <summary>
        /// The call was forwarded from another extension that didn't answer the call after some number of rings.
        /// </summary>
        NoAnswerForward = NativeMethods.LINECALLREASON_FWDNOANSWER,    
        /// <summary>
        /// The call was forwarded unconditionally from another number.
        /// </summary>
        UnconditionalForward = NativeMethods.LINECALLREASON_FWDUNCOND,      
        /// <summary>
        /// The call was picked up from another extension.
        /// </summary>
        Pickup = NativeMethods.LINECALLREASON_PICKUP,         
        /// <summary>
        /// The call was retrieved as a parked call.
        /// </summary>
        Unpark = NativeMethods.LINECALLREASON_UNPARK,     
        /// <summary>
        /// The call was redirected to this station.
        /// </summary>
        Redirect = NativeMethods.LINECALLREASON_REDIRECT, 
        /// <summary>
        /// The call was the result of a call completion request.
        /// </summary>
        CallCompletion = NativeMethods.LINECALLREASON_CALLCOMPLETION, 
        /// <summary>
        /// The call has been transferred from another number.
        /// </summary>
        Transfer = NativeMethods.LINECALLREASON_TRANSFER,       
        /// <summary>
        /// The call is a reminder, or "recall", that the user has a call parked or on hold for potentially a long time.
        /// </summary>
        Reminder = NativeMethods.LINECALLREASON_REMINDER,       
        /// <summary>
        /// The reason for the call is currently unknown but may become known later.
        /// </summary>
        Unknown = NativeMethods.LINECALLREASON_UNKNOWN,        
        /// <summary>
        /// The reason for the call is unavailable and will not become known later.
        /// </summary>
        Unavailable = NativeMethods.LINECALLREASON_UNAVAIL,        
        /// <summary>
        /// The call intruded onto the line, either by a call completion action invoked by another station or by operator action. Depending on switch 
        /// implementation, the call may appear either in the connected state, or conferenced with an existing active call on the line.
        /// </summary>
        Intrude = NativeMethods.LINECALLREASON_INTRUDE,        
        /// <summary>
        /// The call was parked on the address. Usually, it appears initially in the onhold state.
        /// </summary>
        Parked = NativeMethods.LINECALLREASON_PARKED,         
        /// <summary>
        /// The call was camped on the address. Usually, it appears initially in the onhold state, and can be switched to using SwapHold. 
        /// If an active call becomes idle, the camped-on call may change to the offering state and the device start ringing.
        /// </summary>
        CampedOn = NativeMethods.LINECALLREASON_CAMPEDON,       
        /// <summary>
        /// The call appears on the address because the switch needs routing instructions from the application. The application should examine the CalledID 
        /// member and use the Redirect method to provide a new dialable address for the call. If the call is to be blocked instead, the application 
        /// may call Drop. If the application fails to take action within a switch-defined timeout period, a default action will be taken. 
        /// </summary>
        RouteRequest = NativeMethods.LINECALLREASON_ROUTEREQUEST   
    }

    /// <summary>
    /// The different portions of a <seealso cref="TapiCall"/> that can change and cause a CallInfoChange event.
    /// </summary>
    [Flags]
    public enum CallInfoChangeTypes
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Some other portion of the call has changed
        /// </summary>
        Other = NativeMethods.LINECALLINFOSTATE_OTHER,
        /// <summary>
        /// Device specific data has changed
        /// </summary>
        DeviceSpecificData = NativeMethods.LINECALLINFOSTATE_DEVSPECIFIC,
        /// <summary>
        /// The bearer mode for the call has changed.
        /// </summary>
        BearerMode = NativeMethods.LINECALLINFOSTATE_BEARERMODE,
        /// <summary>
        /// The data rate for the call has changed
        /// </summary>
        DataRate = NativeMethods.LINECALLINFOSTATE_RATE,
        /// <summary>
        /// The media mode for the call has changed
        /// </summary>
        MediaMode = NativeMethods.LINECALLINFOSTATE_MEDIAMODE,
        /// <summary>
        /// The application supplied data has changed
        /// </summary>
        AppSpecificData = NativeMethods.LINECALLINFOSTATE_APPSPECIFIC,
        /// <summary>
        /// The call id has changed
        /// </summary>
        Id = NativeMethods.LINECALLINFOSTATE_CALLID,               
        /// <summary>
        /// The related call id has changed
        /// </summary>
        RelatedId = NativeMethods.LINECALLINFOSTATE_RELATEDCALLID, 
        /// <summary>
        /// The origin of the call has changed
        /// </summary>
        CallOrigin = NativeMethods.LINECALLINFOSTATE_ORIGIN,        
        /// <summary>
        /// The reason for the call has changed
        /// </summary>
        CallReason = NativeMethods.LINECALLINFOSTATE_REASON,        
        /// <summary>
        /// The completion id for the call has changed
        /// </summary>
        CompletionId = NativeMethods.LINECALLINFOSTATE_COMPLETIONID,
        /// <summary>
        /// The number of call owners has incremented.
        /// </summary>
        OwnerIncrement = NativeMethods.LINECALLINFOSTATE_NUMOWNERINCR, 
        /// <summary>
        /// The number of call owners has decremented.
        /// </summary>
        OwnerDecrement = NativeMethods.LINECALLINFOSTATE_NUMOWNERDECR, 
        /// <summary>
        /// The number of applications monitoring this call has changed.
        /// </summary>
        NumberOfMonitors = NativeMethods.LINECALLINFOSTATE_NUMMONITORS,
        /// <summary>
        /// The trunk id for the call has been changed.
        /// </summary>
        TrunkId = NativeMethods.LINECALLINFOSTATE_TRUNK,     
        /// <summary>
        /// The callerid information has changed.
        /// </summary>
        CallerId = NativeMethods.LINECALLINFOSTATE_CALLERID,           
        /// <summary>
        /// The calledid information has changed.
        /// </summary>
        CalledId = NativeMethods.LINECALLINFOSTATE_CALLEDID,           
        /// <summary>
        /// The connectedid information has changed.
        /// </summary>
        ConnectedId = NativeMethods.LINECALLINFOSTATE_CONNECTEDID,     
        /// <summary>
        /// The redirectionid information has changed.
        /// </summary>
        RedirectionId = NativeMethods.LINECALLINFOSTATE_REDIRECTIONID, 
        /// <summary>
        /// The redirecting id information has changed.
        /// </summary>
        RedirectingId = NativeMethods.LINECALLINFOSTATE_REDIRECTINGID, 
        /// <summary>
        /// The display has changed.
        /// </summary>
        Display = NativeMethods.LINECALLINFOSTATE_DISPLAY,             
        /// <summary>
        /// The UUI data has changed.
        /// </summary>
        UserUserInfo = NativeMethods.LINECALLINFOSTATE_USERUSERINFO,   
        /// <summary>
        /// The high-level compatibility field has changed.
        /// </summary>
        HighLevelCompatibilityInfo = NativeMethods.LINECALLINFOSTATE_HIGHLEVELCOMP,
        /// <summary>
        /// The low-level compatibility field has changed.
        /// </summary>
        LowLevelCompatibilityInfo = NativeMethods.LINECALLINFOSTATE_LOWLEVELCOMP,
        /// <summary>
        /// The Charging information has changed.
        /// </summary>
        ChargingInfo = NativeMethods.LINECALLINFOSTATE_CHARGINGINFO,
        /// <summary>
        /// The terminal information has changed.
        /// </summary>
        Terminal = NativeMethods.LINECALLINFOSTATE_TERMINAL,
        /// <summary>
        /// The dialing parameters for the call has changed.
        /// </summary>
        DialingParameters = NativeMethods.LINECALLINFOSTATE_DIALPARAMS,
        /// <summary>
        /// One or more of the digit, tone, or media monitoring fields has changed.
        /// </summary>
        MonitorModes = NativeMethods.LINECALLINFOSTATE_MONITORMODES,
        /// <summary>
        /// The call treatment has changed.
        /// </summary>
        CallTreatment = NativeMethods.LINECALLINFOSTATE_TREATMENT,
        /// <summary>
        /// The quality of service associated with the call has changed.
        /// </summary>
        QualityOfService = NativeMethods.LINECALLINFOSTATE_QOS,
        /// <summary>
        /// The call data field has changed.
        /// </summary>
        CallData = NativeMethods.LINECALLINFOSTATE_CALLDATA
    }

    /// <summary>
    /// Flags passed to indicate what has changed on an address for a AddressInfoChange event.
    /// </summary>
    [Flags]
    public enum AddressInfoChangeTypes
    {
        /// <summary>
        /// Unknown items changed
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Other
        /// </summary>
        Other = NativeMethods.LINEADDRESSSTATE_OTHER,
        /// <summary>
        /// Device specific information changed
        /// </summary>
        DeviceSpecificData = NativeMethods.LINEADDRESSSTATE_DEVSPECIFIC,   
        /// <summary>
        /// The address has changed to idle. It is not in use by any stations
        /// </summary>
        InuseZero = NativeMethods.LINEADDRESSSTATE_INUSEZERO,          
        /// <summary>
        /// The address has changed from idle or in use by many bridged stations to being in use by just one station. 
        /// </summary>
        InuseOne = NativeMethods.LINEADDRESSSTATE_INUSEONE,            
        /// <summary>
        /// The monitored or bridged address has changed from being in use by one station to being in use by more than one station. 
        /// </summary>
        InuseMany = NativeMethods.LINEADDRESSSTATE_INUSEMANY,          
        /// <summary>
        /// The number of calls on the address has changed. This is the result of events such as a new incoming call, an outgoing call on the address, or a call changing its hold status.
        /// </summary>
        NumberOfCalls = NativeMethods.LINEADDRESSSTATE_NUMCALLS,       
        /// <summary>
        /// The forwarding status of the address has changed, including possibly the number of rings for determining a no-answer condition. 
        /// The application should check the address status to determine details about the address's current forwarding status. 
        /// </summary>
        ForwardingInformation = NativeMethods.LINEADDRESSSTATE_FORWARD,
        /// <summary>
        /// The terminal settings for the address have changed. 
        /// </summary>
        Terminals = NativeMethods.LINEADDRESSSTATE_TERMINALS,         
        /// <summary>
        /// Indicates that, due to configuration changes made by the user or other circumstances, the capabilities of the address have changed.
        /// </summary>
        Capabilities = NativeMethods.LINEADDRESSSTATE_CAPSCHANGE      
    }

    /// <summary>
    /// This enumeration determines which calls, if any, may be removed from conferences
    /// </summary>
    [Flags]
    public enum RemoveFromConferenceType
    {
        /// <summary>
        /// Unknown if the feature is supported
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Parties cannot be removed from the conference call. 
        /// </summary>
        None = NativeMethods.LINEREMOVEFROMCONF_NONE,
        /// <summary>
        /// Only the most recently added party can be removed from the conference call 
        /// </summary>
        Last = NativeMethods.LINEREMOVEFROMCONF_LAST,
        /// <summary>
        /// Any participating party can be removed from the conference call. 
        /// </summary>
        Any = NativeMethods.LINEREMOVEFROMCONF_ANY
    }

    /// <summary>
    /// These flags are used to determine what has changed on a line device for the 
    /// </summary>
    [Flags]
    public enum LineInfoChangeTypes
    {
        /// <summary>
        /// Unknown items changed
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The application should check the current device status to determine which items have changed.
        /// </summary>
        Other = NativeMethods.LINEDEVSTATE_OTHER,
        /// <summary>
        /// The switch tells the line to alert the user.
        /// </summary>
        Ringing = NativeMethods.LINEDEVSTATE_RINGING,         
        /// <summary>
        /// The line was previously disconnected and is now connected to TAPI.
        /// </summary>
        Connected = NativeMethods.LINEDEVSTATE_CONNECTED,         
        /// <summary>
        /// This line was previously connected and is now disconnected from TAPI.
        /// </summary>
        Disconnected = NativeMethods.LINEDEVSTATE_DISCONNECTED,      
        /// <summary>
        /// The message waiting indicator has changed.
        /// </summary>
        MessageWaitingLamp = (NativeMethods.LINEDEVSTATE_MSGWAITON | NativeMethods.LINEDEVSTATE_MSGWAITOFF),        
        /// <summary>
        /// The line is connected to TAPI. This happens when TAPI is first activated or when the line wire is physically plugged in and in-service at the switch while TAPI is active.
        /// </summary>
        InService = NativeMethods.LINEDEVSTATE_INSERVICE,         
        /// <summary>
        /// The line is out of service at the switch or physically disconnected. TAPI cannot be used to operate on the line device.
        /// </summary>
        OutOfService = NativeMethods.LINEDEVSTATE_OUTOFSERVICE,      
        /// <summary>
        /// Maintenance is being performed on the line at the switch. TAPI cannot be used to operate on the line device.
        /// </summary>
        Maintenance = NativeMethods.LINEDEVSTATE_MAINTENANCE,       
        /// <summary>
        /// The line has been opened by another application.
        /// </summary>
        Open = NativeMethods.LINEDEVSTATE_OPEN,              
        /// <summary>
        /// The line has been closed by another application.
        /// </summary>
        Close = NativeMethods.LINEDEVSTATE_CLOSE,             
        /// <summary>
        /// The number of calls on the line device has changed.
        /// </summary>
        NumberOfCalls = NativeMethods.LINEDEVSTATE_NUMCALLS,         
        /// <summary>
        /// The number of outstanding call completions on the line device has changed.
        /// </summary>
        NumberOfCallCompletions = NativeMethods.LINEDEVSTATE_NUMCOMPLETIONS,    
        /// <summary>
        /// The terminal settings have changed. This can happen, for example, if multiple line devices share terminals among them. 
        /// For example, this can happen when two lines share a phone terminal.
        /// </summary>
        Terminals = NativeMethods.LINEDEVSTATE_TERMINALS,         
        /// <summary>
        /// The roam mode of the line device has changed.
        /// </summary>
        RoamingMode = NativeMethods.LINEDEVSTATE_ROAMMODE,          
        /// <summary>
        /// The battery level has changed significantly. This condition applies to cellular phones. 
        /// </summary>
        Battery = NativeMethods.LINEDEVSTATE_BATTERY,           
        /// <summary>
        /// The signal level has changed significantly (cellular).
        /// </summary>
        SignalLevel = NativeMethods.LINEDEVSTATE_SIGNAL,            
        /// <summary>
        /// The line's device-specific information has changed.
        /// </summary>
        DeviceSpecificData = NativeMethods.LINEDEVSTATE_DEVSPECIFIC, 
        /// <summary>
        /// Items have changed in the configuration of line devices. To become aware of these changes, for example to become aware ofthe 
        /// appearance of new line devices, the application should reinitialize its use of TAPI.
        /// </summary>
        ReinitRequired = NativeMethods.LINEDEVSTATE_REINIT,            
        /// <summary>
        /// The locked status of the line device has changed. 
        /// </summary>
        Lock = NativeMethods.LINEDEVSTATE_LOCK,              
        /// <summary>
        /// Indicates that, due to configuration changes made by the user or by other circumstances, the line capabilities have changed.
        /// </summary>
        Capabilities = NativeMethods.LINEDEVSTATE_CAPSCHANGE,        
        /// <summary>
        /// Indicates that configuration changes have been made to one or more of the media devices associated with the line device.
        /// </summary>
        Configuration = NativeMethods.LINEDEVSTATE_CONFIGCHANGE,      
        /// <summary>
        /// Indicates that, due to configuration changes made by the user or other circumstances, the number translation information for this line has changed.
        /// </summary>
        Translate = NativeMethods.LINEDEVSTATE_TRANSLATECHANGE,   
        /// <summary>
        /// Indicates that the call completion has been externally canceled and is no longer considered valid
        /// </summary>
        CallCompletionCanceled = NativeMethods.LINEDEVSTATE_COMPLCANCEL, 
        /// <summary>
        /// Indicates that the device is being removed from the system by the service provider most likely through user action, through a control panel or similar utility.
        /// </summary>
        Removed = NativeMethods.LINEDEVSTATE_REMOVED           
    }

    /// <summary>
    /// These flags are used to determine what has changed on a phone device
    /// </summary>
    [Flags]
    public enum PhoneStateChangeTypes
    {
        /// <summary>
        /// Unknown items changed
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Phone-status items other than those listed below have changed. The application 
        /// should check the current phone status to determine which items have changed
        /// </summary>
        Other = NativeMethods.PHONESTATE_OTHER,
        /// <summary>
        /// The connection between the phone device and TAPI was just made. This happens when 
        /// TAPI is first invoked or when the wire connecting the phone to the PC is plugged 
        /// in with TAPI active.
        /// </summary>
        Connected = NativeMethods.PHONESTATE_CONNECTED,
        /// <summary>
        /// The connection between the phone device and TAPI was just broken. This happens when 
        /// the wire connecting the phone set to the PC is unplugged while TAPI is active.
        /// </summary>
        Disconnected = NativeMethods.PHONESTATE_DISCONNECTED,
        /// <summary>
        /// The number of owners for the phone device.
        /// </summary>
        Owner = NativeMethods.PHONESTATE_OWNER,
        /// <summary>
        /// The number of monitors for the phone device.
        /// </summary>
        Monitors = NativeMethods.PHONESTATE_MONITORS,
        /// <summary>
        /// The display of the phone has changed.
        /// </summary>
        Display = NativeMethods.PHONESTATE_DISPLAY,
        /// <summary>
        /// A lamp of the phone has changed.
        /// </summary>
        Lamp = NativeMethods.PHONESTATE_LAMP,
        /// <summary>
        /// The ring mode of the phone has changed.
        /// </summary>
        RingMode = NativeMethods.PHONESTATE_RINGMODE,
        /// <summary>
        /// The ring volume of the phone has changed
        /// </summary>
        RingVolume = NativeMethods.PHONESTATE_RINGVOLUME,
        /// <summary>
        /// The handset hookswitch state has changed.
        /// </summary>
        HandsetHookswitch = NativeMethods.PHONESTATE_HANDSETHOOKSWITCH,
        /// <summary>
        /// The handset volume state has changed.
        /// </summary>
        HandsetVolume = NativeMethods.PHONESTATE_HANDSETVOLUME,
        /// <summary>
        /// The handset gain state has changed.
        /// </summary>
        HandsetGain = NativeMethods.PHONESTATE_HANDSETGAIN,
        /// <summary>
        /// The speakerphone's hookswitch state has changed.
        /// </summary>
        SpeakerHookswitch = NativeMethods.PHONESTATE_SPEAKERHOOKSWITCH,
        /// <summary>
        /// The speakerphone's volume state has changed.
        /// </summary>
        SpeakerVolume = NativeMethods.PHONESTATE_SPEAKERVOLUME,
        /// <summary>
        /// The speakerphone's gain state has changed.
        /// </summary>
        SpeakerGain = NativeMethods.PHONESTATE_SPEAKERGAIN,
        /// <summary>
        /// The headset's hookswitch state has changed.
        /// </summary>
        HeadsetHookswitch = NativeMethods.PHONESTATE_HEADSETHOOKSWITCH,
        /// <summary>
        /// The headset's microphone volume setting has changed.
        /// </summary>
        HeadsetVolume = NativeMethods.PHONESTATE_HEADSETVOLUME,
        /// <summary>
        /// The headset's microphone gain setting has changed.
        /// </summary>
        HeadsetGain = NativeMethods.PHONESTATE_HEADSETGAIN,
        /// <summary>
        /// The application's use of the phone is temporarily suspended.
        /// </summary>
        Suspend = NativeMethods.PHONESTATE_SUSPEND,
        /// <summary>
        /// The application's use of the phone device is resumed after having been 
        /// suspended for some time.
        /// </summary>
        Resume = NativeMethods.PHONESTATE_RESUME,
        /// <summary>
        /// The phone's device-specific information has changed.
        /// </summary>
        DevSpecific = NativeMethods.PHONESTATE_DEVSPECIFIC,
        /// <summary>
        /// Items have changed in the configuration of phone devices. To become aware of 
        /// these changes, as for the appearance of new phone devices, the application 
        /// should reinitialize its use of TAPI.
        /// </summary>
        Reinit = NativeMethods.PHONESTATE_REINIT,
        /// <summary>
        /// Indicates that, due to configuration changes made by the user or other circumstances, 
        /// one or more of the members in the PHONECAPS structure have changed. The application 
        /// should read the updated structure. If a service provider sends a PHONE_STATE 
        /// message containing this value to TAPI, TAPI will pass it along to applications 
        /// that have negotiated TAPI version 1.4 or later; applications negotiating a previous 
        /// API version will receive PHONE_STATE messages specifying PHONESTATE_REINIT, requiring 
        /// them to shut down and reinitialize their connection to TAPI to obtain the updated 
        /// information.
        /// </summary>
        CapabilitiesChanged = NativeMethods.PHONESTATE_CAPSCHANGE,
        /// <summary>
        /// Indicates that the device is being removed from the system by the service provider, 
        /// most likely through user action, through a control panel, or through a similar 
        /// utility. A Removed message will normally be immediately 
        /// followed by a Closed message on the device. Subsequent attempts to access the 
        /// device prior to TAPI being reinitialized will result in PHONEERR_NODEVICE 
        /// being returned to the application. If a service provider sends a PHONE_STATE 
        /// message containing this value to TAPI, TAPI will pass it along to applications 
        /// that have negotiated TAPI version 1.4 or later. Applications negotiating a previous 
        /// API version will not receive any notification. 
        /// </summary>
        Removed = NativeMethods.PHONESTATE_REMOVED
    }

    /// <summary>
    /// These are options used in <see cref="TapiLine.TranslateNumber(string, TranslationOptions)"/>.
    /// </summary>
    [Flags]
    public enum TranslationOptions
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// If a Cancel Call Waiting string is defined for the location, setting this bit will cause that string to be inserted at the beginning of the dialable string. 
        /// This is commonly used by data modem and fax applications to prevent interruption of calls by call waiting beeps. If no Cancel Call Waiting string is defined 
        /// for the location, this bit has no affect. 
        /// </summary>
        CancelCallWaiting = NativeMethods.LINETRANSLATEOPTION_CANCELCALLWAITING,
        /// <summary>
        /// This option forces the address (number) to be translated as long distance. 
        /// </summary>
        ForceLongDistanceCall = NativeMethods.LINETRANSLATEOPTION_FORCELD,
        /// <summary>
        /// This option forces the number (address) to be translated as local. 
        /// </summary>
        ForceLocalCall = NativeMethods.LINETRANSLATEOPTION_FORCELOCAL
    }

    /// <summary>
    /// These describe different ways in which a call can be completed
    /// </summary>
    [Flags]
    public enum CallCompletionMode
    {
        /// <summary>
        /// No completion
        /// </summary>
        None = 0,
        /// <summary>
        /// Requests the called station to return the call when it returns to idle
        /// </summary>
        Callback = NativeMethods.LINECALLCOMPLMODE_CALLBACK,
        /// <summary>
        /// Queues the call until the call can be completed. 
        /// </summary>
        Campon = NativeMethods.LINECALLCOMPLMODE_CAMPON,
        /// <summary>
        /// Adds the application to the existing call at the called station (barge in). 
        /// </summary>
        Intrude = NativeMethods.LINECALLCOMPLMODE_INTRUDE,
        /// <summary>
        /// Leaves a short predefined message for the called station (Leave Word Calling). The message to be sent is specified separately. 
        /// </summary>
        Message = NativeMethods.LINECALLCOMPLMODE_MESSAGE
    }

    /// <summary>
    /// Describes the different types of forwarding which may be applied to lines or addresses.  This is used by the <see cref="ForwardInfo"/> structure.
    /// </summary>
    public enum ForwardingMode
    {
        /// <summary>
        /// Forward all calls unconditionally, irrespective of their origin. 
        /// Use this value when unconditional forwarding for internal and external calls cannot be controlled separately. 
        /// Unconditional forwarding overrides forwarding on busy and/or no answer conditions. 
        /// </summary>
        Unconditional = NativeMethods.LINEFORWARDMODE_UNCOND,
        /// <summary>
        /// Forward all internal calls unconditionally. Use this value when unconditional forwarding for internal and external calls can be controlled separately. 
        /// </summary>
        UnconditionalInternal = NativeMethods.LINEFORWARDMODE_UNCONDINTERNAL,
        /// <summary>
        /// Forward all external calls unconditionally. Use this value when unconditional forwarding for internal and external calls can be controlled separately. 
        /// </summary>
        UnconditionalExternal = NativeMethods.LINEFORWARDMODE_UNCONDEXTERNAL,
        /// <summary>
        /// Unconditionally forward all calls that originated at a specified address (selective call forwarding). 
        /// </summary>
        UnconditionaSpecific = NativeMethods.LINEFORWARDMODE_UNCONDSPECIFIC,
        /// <summary>
        /// Forward all calls on busy, irrespective of their origin. 
        /// Use this value when forwarding for internal and external calls on busy and on no answer cannot be controlled separately. 
        /// </summary>
        Busy = NativeMethods.LINEFORWARDMODE_BUSY,
        /// <summary>
        /// Forward all internal calls on busy. 
        /// Use this value when forwarding for internal and external calls on busy and on no answer can be controlled separately. 
        /// </summary>
        BusyInternal = NativeMethods.LINEFORWARDMODE_BUSYINTERNAL,
        /// <summary>
        /// Forward all external calls on busy. 
        /// Use this value when forwarding for internal and external calls on busy and on no answer can be controlled separately. 
        /// </summary>
        BusyExternal = NativeMethods.LINEFORWARDMODE_BUSYEXTERNAL,
        /// <summary>
        /// Forward on busy all calls that originated at a specified address (selective call forwarding). 
        /// </summary>
        BusySpecific = NativeMethods.LINEFORWARDMODE_BUSYSPECIFIC,
        /// <summary>
        /// Forward all calls on no answer, irrespective of their origin. 
        /// Use this value when call forwarding for internal and external calls on no answer cannot be controlled separately. 
        /// </summary>
        NoAnswer = NativeMethods.LINEFORWARDMODE_NOANSW,
        /// <summary>
        /// Forward all internal calls on no answer. Use this value when forwarding for internal and external calls on no answer can be controlled separately. 
        /// </summary>
        NoAnswerInternal = NativeMethods.LINEFORWARDMODE_NOANSWINTERNAL,
        /// <summary>
        /// Forward all external calls on no answer. 
        /// Use this value when forwarding for internal and external calls on no answer can be controlled separately. 
        /// </summary>
        NoAnswerExternal = NativeMethods.LINEFORWARDMODE_NOANSWEXTERNAL,
        /// <summary>
        /// Forward on no answer all calls that originated at a specified address (selective call forwarding). 
        /// </summary>
        NoAnswerSpecific = NativeMethods.LINEFORWARDMODE_NOANSWSPECIFIC,
        /// <summary>
        /// Forward all calls on busy/no answer, irrespective of their origin. 
        /// Use this value when forwarding for internal and external calls on busy and on no answer cannot be controlled separately. 
        /// </summary>
        BusyNoAnswer = NativeMethods.LINEFORWARDMODE_BUSYNA,
        /// <summary>
        /// Forward all internal calls on busy/no answer. 
        /// Use this value when call forwarding on busy and on no answer cannot be controlled separately for internal calls. 
        /// </summary>
        BusyNoAnswerInternal = NativeMethods.LINEFORWARDMODE_BUSYNAINTERNAL,
        /// <summary>
        /// Forward all external calls on busy/no answer. 
        /// Use this value when call forwarding on busy and on no answer cannot be controlled separately for internal calls. 
        /// </summary>
        BusyNoAnswerExternal = NativeMethods.LINEFORWARDMODE_BUSYNAEXTERNAL,
        /// <summary>
        /// Forward on busy/no answer all calls that originated at a specified address (selective call forwarding). 
        /// </summary>
        BusyNoAnswerSpecific = NativeMethods.LINEFORWARDMODE_BUSYNASPECIFIC,
        /// <summary>
        /// Calls are forwarded, but the conditions under which forwarding will occur are not known at this time. 
        /// It is possible that the conditions may become known at a future time. (TAPI versions 1.4 and later) 
        /// </summary>
        Unknown = NativeMethods.LINEFORWARDMODE_UNKNOWN,
        /// <summary>
        /// Forward on no answer all calls that originated at a specified address (selective call forwarding). 
        /// </summary>
        Unavailable = NativeMethods.LINEFORWARDMODE_UNAVAIL
    }

    /// <summary>
    /// Describe the microphone and speaker components of a hookswitch device
    /// </summary>
    [Flags]
    public enum HookswitchMode
    {
        /// <summary>
        /// The device's microphone and speaker are both onhook
        /// </summary>
        Onhook = NativeMethods.PHONEHOOKSWITCHMODE_ONHOOK,
        /// <summary>
        /// The device's microphone and speaker are both onhook
        /// </summary>
        Microphone = NativeMethods.PHONEHOOKSWITCHMODE_MIC,
        /// <summary>
        /// The device's speaker is active, the microphone is mute. 
        /// </summary>
        Speaker = NativeMethods.PHONEHOOKSWITCHMODE_SPEAKER,
        /// <summary>
        /// The device's microphone and speaker are both active
        /// </summary>
        MicrophoneAndSpeaker = NativeMethods.PHONEHOOKSWITCHMODE_MICSPEAKER,
        /// <summary>
        /// The device's hookswitch mode is currently unknown. 
        /// </summary>
        Unknown = NativeMethods.PHONEHOOKSWITCHMODE_UNKNOWN
    }

    /// <summary>
    /// Commonly defined button functions. These button functions can be used to invoke the corresponding function 
    /// from the switch using DeviceSpecificFeature. Note that TAPI does not define the semantics of the button 
    /// functions; it only provides access to the corresponding function. The behavior associated with each of the 
    /// function values above is generic and can vary based on the telephony environment
    /// </summary>
    /// <remarks>Note that vendors can add their own extensions and the value may not be present here.</remarks>
    public enum ButtonFunction
    {
        /// <summary>
        /// A "dummy" function assignment that indicates that the exact function of the button is unknown or has not been assigned. 
        /// </summary>
        Unknown = NativeMethods.PHONEBUTTONFUNCTION_UNKNOWN,
        /// <summary>
        /// Initiates a conference call or adds a call to a conference call. 
        /// </summary>
        Conference = NativeMethods.PHONEBUTTONFUNCTION_CONFERENCE,
        /// <summary>
        /// Initiates a call transfer or completes the transfer of a call. 
        /// </summary>
        Transfer = NativeMethods.PHONEBUTTONFUNCTION_TRANSFER,
        /// <summary>
        /// Drops the active call
        /// </summary>
        Drop = NativeMethods.PHONEBUTTONFUNCTION_DROP,
        /// <summary>
        /// Places the active call on hold. 
        /// </summary>
        Hold = NativeMethods.PHONEBUTTONFUNCTION_HOLD,
        /// <summary>
        /// Unholds a call. 
        /// </summary>
        Recall = NativeMethods.PHONEBUTTONFUNCTION_RECALL,
        /// <summary>
        /// Disconnects a call, such as after initiating a transfer. 
        /// </summary>
        Disconnect = NativeMethods.PHONEBUTTONFUNCTION_DISCONNECT,
        /// <summary>
        /// Reconnects a call that is on consultation hold. 
        /// </summary>
        Reconnect = NativeMethods.PHONEBUTTONFUNCTION_CONNECT,
        /// <summary>
        /// Turns on a message waiting lamp.
        /// </summary>
        MessageWaitOn = NativeMethods.PHONEBUTTONFUNCTION_MSGWAITON,
        /// <summary>
        /// Turns off a message waiting lamp. 
        /// </summary>
        MessageWaitOff = NativeMethods.PHONEBUTTONFUNCTION_MSGWAITOFF,
        /// <summary>
        /// Allows the user to select the ring pattern of the phone. 
        /// </summary>
        SelectRing = NativeMethods.PHONEBUTTONFUNCTION_SELECTRING,
        /// <summary>
        /// The number to be dialed will be indicated using a short, abbreviated number consisting of one digit or a few digits.
        /// </summary>
        AbbreviatedDial = NativeMethods.PHONEBUTTONFUNCTION_ABBREVDIAL,
        /// <summary>
        /// Initiates or changes call forwarding to this phone. 
        /// </summary>
        Forward = NativeMethods.PHONEBUTTONFUNCTION_FORWARD,
        /// <summary>
        /// Picks up a call ringing on another phone. 
        /// </summary>
        Pickup = NativeMethods.PHONEBUTTONFUNCTION_PICKUP,
        /// <summary>
        /// Initiates a request to be notified if a call cannot be completed normally because of a busy signal or no answer. 
        /// </summary>
        RingAgain = NativeMethods.PHONEBUTTONFUNCTION_RINGAGAIN,
        /// <summary>
        /// Parks the active call on another phone, placing it on hold there. 
        /// </summary>
        Park = NativeMethods.PHONEBUTTONFUNCTION_PARK,
        /// <summary>
        /// Rejects an incoming call before the call has been answered. 
        /// </summary>
        Reject = NativeMethods.PHONEBUTTONFUNCTION_REJECT,
        /// <summary>
        /// Redirects an incoming call to another extension before the call has been answered. 
        /// </summary>
        Redirect = NativeMethods.PHONEBUTTONFUNCTION_REDIRECT,
        /// <summary>
        /// Mutes the phone's microphone device. 
        /// </summary>
        Mute = NativeMethods.PHONEBUTTONFUNCTION_MUTE,
        /// <summary>
        /// Increases the volume of audio through the phone's handset speaker or speakerphone. 
        /// </summary>
        VolumeUp = NativeMethods.PHONEBUTTONFUNCTION_VOLUMEUP,
        /// <summary>
        /// Decreases the volume of audio through the phone's handset speaker or speakerphone. 
        /// </summary>
        VolumeDown = NativeMethods.PHONEBUTTONFUNCTION_VOLUMEDOWN,
        /// <summary>
        /// Turns the phone's external speaker on. 
        /// </summary>
        SpeakerOn = NativeMethods.PHONEBUTTONFUNCTION_SPEAKERON,
        /// <summary>
        /// Turns the phone's external speaker off. 
        /// </summary>
        SpeakerOff = NativeMethods.PHONEBUTTONFUNCTION_SPEAKEROFF,
        /// <summary>
        /// Generates the equivalent of an onhook/offhook sequence. A flash typically indicates that any digits 
        /// typed next are to be understood as commands to the switch. On many switches, places an active call on consultation hold. 
        /// </summary>
        Flash = NativeMethods.PHONEBUTTONFUNCTION_FLASH,
        /// <summary>
        /// Indicates that the next call is a data call. 
        /// </summary>
        DataOn = NativeMethods.PHONEBUTTONFUNCTION_DATAON,
        /// <summary>
        /// Indicates that the next call is not a data call. 
        /// </summary>
        DataOff = NativeMethods.PHONEBUTTONFUNCTION_DATAOFF,
        /// <summary>
        /// Places the phone in "do not disturb" mode; incoming calls receive a busy signal or are forwarded to an 
        /// operator or voice mail system. 
        /// </summary>
        DoNotDisturb = NativeMethods.PHONEBUTTONFUNCTION_DONOTDISTURB,
        /// <summary>
        /// Connects to the intercom to broadcast a page. 
        /// </summary>
        Intercom = NativeMethods.PHONEBUTTONFUNCTION_INTERCOM,
        /// <summary>
        /// Selects a particular appearance of a bridged address.
        /// </summary>
        BridgedApp = NativeMethods.PHONEBUTTONFUNCTION_BRIDGEDAPP,
        /// <summary>
        /// Makes the phone appear "busy" to incoming calls. 
        /// </summary>
        Busy = NativeMethods.PHONEBUTTONFUNCTION_BUSY,
        /// <summary>
        /// Selects a particular call appearance. 
        /// </summary>
        SelectCall = NativeMethods.PHONEBUTTONFUNCTION_CALLAPP,
        /// <summary>
        /// Causes the phone to display current date and time; this information would be sent by the switch. 
        /// </summary>
        DateTime = NativeMethods.PHONEBUTTONFUNCTION_DATETIME,
        /// <summary>
        /// Calls up directory service from the switch
        /// </summary>
        Directory = NativeMethods.PHONEBUTTONFUNCTION_DIRECTORY,
        /// <summary>
        /// Forwards all calls destined for this phone to another phone used for coverage.
        /// </summary>
        ForwardCover = NativeMethods.PHONEBUTTONFUNCTION_COVER,
        /// <summary>
        /// Requests display of caller ID on the phone's display. 
        /// </summary>
        CallerID = NativeMethods.PHONEBUTTONFUNCTION_CALLID,
        /// <summary>
        /// Redials last number dialed. 
        /// </summary>
        RedialLastNumber = NativeMethods.PHONEBUTTONFUNCTION_LASTNUM,
        /// <summary>
        /// Places the phone in the mode it is configured for during night hours. 
        /// </summary>
        NightService = NativeMethods.PHONEBUTTONFUNCTION_NIGHTSRV,
        /// <summary>
        /// Sends all calls to another phone used for coverage; same as ForwardCover. 
        /// </summary>
        SendCalls = NativeMethods.PHONEBUTTONFUNCTION_SENDCALLS,
        /// <summary>
        /// Controls the message indicator lamp. 
        /// </summary>
        MessageIndicator = NativeMethods.PHONEBUTTONFUNCTION_MSGINDICATOR,
        /// <summary>
        /// Repertory dialing—the number to be dialed is provided as a shorthand following pressing of this button. 
        /// </summary>
        RepertoryDial = NativeMethods.PHONEBUTTONFUNCTION_REPDIAL,
        /// <summary>
        /// Programs the shorthand-to-phone number mappings accessible by means of repertory dialing (the "REPDIAL" button). 
        /// </summary>
        SetReperatoryDial = NativeMethods.PHONEBUTTONFUNCTION_SETREPDIAL,
        /// <summary>
        /// The number to be dialed is provided as a shorthand following pressing of this button. 
        /// The mappings for system speed dialing are configured inside the switch. 
        /// </summary>
        SystemSpeed = NativeMethods.PHONEBUTTONFUNCTION_SYSTEMSPEED,
        /// <summary>
        /// The number to be dialed is provided as a shorthand following pressing of this button. The mappings 
        /// for station speed dialing are specific to this station (phone). 
        /// </summary>
        StationSpeed = NativeMethods.PHONEBUTTONFUNCTION_STATIONSPEED,
        /// <summary>
        /// Camps-on an extension that returns a busy indication. When the remote station returns to idle, 
        /// the phone will be rung with a distinctive patterns. Picking up the local phone reinitiates the call. 
        /// </summary>
        CampOn = NativeMethods.PHONEBUTTONFUNCTION_CAMPON,
        /// <summary>
        /// When pressed while a call or call attempt is active, it will remember that call's number or command. When pressed 
        /// while no call is active (such as during dial tone), it repeats the most saved command. 
        /// </summary>
        SaveRepeat = NativeMethods.PHONEBUTTONFUNCTION_SAVEREPEAT,
        /// <summary>
        /// Queues a call to an outside number after it encounters a trunk-busy indication. When a trunk becomes 
        /// later available, the phone will be rung with a distinctive pattern. Picking up the local phone reinitiates the call. 
        /// </summary>
        QueueCall = NativeMethods.PHONEBUTTONFUNCTION_QUEUECALL,
        /// <summary>
        /// A "dummy" function assignment that indicates that the button does not have a function. 
        /// </summary>
        None = NativeMethods.PHONEBUTTONFUNCTION_NONE,
        /// <summary>
        /// Send the call to a directed destination
        /// </summary>
        Send = NativeMethods.PHONEBUTTONFUNCTION_SEND
    }

    /// <summary>
    /// Keypad buttons
    /// </summary>
    public enum ButtonKey
    {
        /// <summary>
        /// Button does not represent a key
        /// </summary>
        None = 0,
        /// <summary>
        /// The # symbol
        /// </summary>
        Pound = 35,
        /// <summary>
        /// The * symbols
        /// </summary>
        Star = 42,
        /// <summary>
        /// 0
        /// </summary>
        Zero = 48,
        /// <summary>
        /// 1
        /// </summary>
        One = 49,
        /// <summary>
        /// 2
        /// </summary>
        Two = 50,
        /// <summary>
        /// 3
        /// </summary>
        Three = 51,
        /// <summary>
        /// 4
        /// </summary>
        Four = 52,
        /// <summary>
        /// 5
        /// </summary>
        Five = 53,
        /// <summary>
        /// 6
        /// </summary>
        Six = 54,
        /// <summary>
        /// 7
        /// </summary>
        Seven = 55,
        /// <summary>
        /// 8
        /// </summary>
        Eight = 56,
        /// <summary>
        /// 9
        /// </summary>
        Nine = 57,
        /// <summary>
        /// A
        /// </summary>
        A = 65,
        /// <summary>
        /// B
        /// </summary>
        B = 66,
        /// <summary>
        /// C
        /// </summary>
        C = 67,
        /// <summary>
        /// D
        /// </summary>
        D = 68
    }

    /// <summary>
    /// Used to to describe the meaning associated with the phone's buttons
    /// </summary>
    /// <remarks>Note that vendors can add their own extensions and the value may not be present here.</remarks>
    [Flags]
    public enum ButtonMode
    {
        /// <summary>
        /// This value is used to describe a button/lamp position that has no corresponding button but has only a lamp. 
        /// </summary>
        Dummy = NativeMethods.PHONEBUTTONMODE_DUMMY,
        /// <summary>
        /// The button is assigned to a call appearance
        /// </summary>
        Call = NativeMethods.PHONEBUTTONMODE_CALL,
        /// <summary>
        /// The button is assigned to requesting features from the switch, such as hold, conference, and transfer. 
        /// </summary>
        Feature = NativeMethods.PHONEBUTTONMODE_FEATURE,
        /// <summary>
        /// The button is one of the twelve keypad buttons, 0 through 9, '*', and '#'. 
        /// </summary>
        Keypad = NativeMethods.PHONEBUTTONMODE_KEYPAD,
        /// <summary>
        /// The button is a local function button, such as mute or volume control. 
        /// </summary>
        Local = NativeMethods.PHONEBUTTONMODE_LOCAL,
        /// <summary>
        /// The button is a "soft" button associated with the phone's display. A phone set can have zero or more display buttons. 
        /// </summary>
        Display = NativeMethods.PHONEBUTTONMODE_DISPLAY,
    }

    /// <summary>
    /// Describes the various ways a lamp may be lit. Where the exact on and off cadences can differ across 
    /// phone sets from different vendors, mapping of actual lamp lighting patterns for most phones onto the 
    /// values listed above should be straightforward.
    /// </summary>
    /// <remarks>Note that vendors can add their own extensions and the value may not be present here.</remarks>
    public enum LampMode
    {
        /// <summary>
        /// This value is used to describe a button/lamp position that has no corresponding lamp. 
        /// </summary>
        Dummy = NativeMethods.PHONELAMPMODE_DUMMY,
        /// <summary>
        /// The lamp is off. 
        /// </summary>
        Off = NativeMethods.PHONELAMPMODE_OFF,
        /// <summary>
        /// Steady means the lamp is continuously lit.
        /// </summary>
        Steady = NativeMethods.PHONELAMPMODE_STEADY,
        /// <summary>
        /// Wink means normal rate on and off. 
        /// </summary>
        Wink = NativeMethods.PHONELAMPMODE_WINK,
        /// <summary>
        /// Flash means slow on and off. 
        /// </summary>
        Flash = NativeMethods.PHONELAMPMODE_FLASH,
        /// <summary>
        /// Flutter means fast on and off. 
        /// </summary>
        Flutter = NativeMethods.PHONELAMPMODE_FLUTTER,
        /// <summary>
        /// Broken flutter is the superposition of flash and flutter. 
        /// </summary>
        BrokenFlutter = NativeMethods.PHONELAMPMODE_BROKENFLUTTER,
        /// <summary>
        /// The lamp mode is currently unknown.
        /// </summary>
        Unknown = NativeMethods.PHONELAMPMODE_UNKNOWN
    }

    /// <summary>
    /// This models the current phone button states
    /// </summary>
    public enum ButtonState
    {
        /// <summary>
        /// Button is "unpressed"
        /// </summary>
        Up = NativeMethods.PHONEBUTTONSTATE_UP,
        /// <summary>
        /// Button is currently "pressed"
        /// </summary>
        Down = NativeMethods.PHONEBUTTONSTATE_DOWN,
        /// <summary>
        /// Button status is unknown, but might become known at some point
        /// </summary>
        Unknown = NativeMethods.PHONEBUTTONSTATE_UNKNOWN,
        /// <summary>
        /// Button state is unknown and never will be.
        /// </summary>
        Unavailable = NativeMethods.PHONEBUTTONSTATE_UNAVAIL,
    }
}
