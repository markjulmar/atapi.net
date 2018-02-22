using System;
using System.Runtime.InteropServices;

namespace JulMar.Atapi
{
    /// <summary>
    /// This object represents a single exposed line device from Tapi.
    /// </summary>
    public interface ITapiLine
    {
        /// <summary>
        /// The available addresses on this line.
        /// </summary>
        ITapiAddress[] Addresses { get; }

        /// <summary>
        /// Returns the <see cref="LineCapabilities"/> object for this line.
        /// </summary>
        LineCapabilities Capabilities { get; }

        /// <summary>
        /// This returns the available TSP device-specific extension ID.  It is the form of a string "a.b.c.d" and will be "0.0.0.0" if no
        /// device-specific extensions are present.
        /// </summary>
        string DeviceSpecificExtensionID { get; }

        /// <summary>
        /// The numeric device ID representing the line.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Returns true/false whether the line is currently open.
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// This returns whether the line device is usable or not.  Removed lines are
        /// not usable and have no capabilities or properties.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// This returns the underlying HTLINE which you can use in your
        /// own interop scenarios to deal with custom methods or places
        /// which are not wrapped by ATAPI
        /// </summary>
        SafeHandle LineHandle { get; }

        /// <summary>
        /// Returns the Line Name associated with the line.  It will never be empty.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The <see cref="TapiVersion"/> that this line negotiated to.
        /// </summary>
        TapiVersion NegotiatedVersion { get; }

        /// <summary>
        /// The permanent numeric ID representing this line
        /// </summary>
        int PermanentId { get; }

        /// <summary>
        /// Returns the <see cref="LineStatus"/> object for this line.
        /// </summary>
        LineStatus Status { get; }

        /// <summary>
        /// This associates an arbitrary object with the line device
        /// </summary>
        object Tag { get; set; }

        /// <summary>
        /// This retrieves the total number of calls on the line.
        /// </summary>
        /// <returns>Call Count</returns>
        int TotalCallCount { get; }

        /// <summary>
        /// This event is raised when a call on this address changes state
        /// </summary>
        event EventHandler<CallStateEventArgs> CallStateChanged;

        /// <summary>
        /// This event is raised when the information associated with a call changes
        /// </summary>
        event EventHandler<CallInfoChangeEventArgs> CallInfoChanged;

        /// <summary>
        /// This event is raised when a new call is placed or offering on the line.
        /// </summary>
        event EventHandler<NewCallEventArgs> NewCall;

        /// <summary>
        /// This event is raised when an address on this line changes
        /// </summary>
        event EventHandler<AddressInfoChangeEventArgs> AddressChanged;

        /// <summary>
        /// This event is raised when the status or capabilities of the line has changed.
        /// </summary>
        event EventHandler<LineInfoChangeEventArgs> Changed;

        /// <summary>
        /// This event is raised when the line is ringing.
        /// </summary>
        event EventHandler<RingEventArgs> Ringing;

        /// <summary>
        /// Cancels the specified call completion request on the specified line
        /// </summary>
        /// <param name="completionId">Original completion id from <see cref="TapiCall.CompleteCall"/>.</param>
        /// <param name="acb">AsyncCallback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult BeginUncompleteCall(int completionId, AsyncCallback acb, object state);

        /// <summary>
        /// This cancels any forwarding request that is currently in effect.
        /// </summary>
        void CancelForward();

        /// <summary>
        /// This closes the line device.
        /// </summary>
        void Close();

        /// <summary>
        /// This method displays the line configuration dialog.
        /// </summary>
        /// <param name="hwnd">Handle to Form owner or IntPtr.Zero</param>
        /// <param name="deviceClass">Page to display or null</param>
        /// <returns>true/false</returns>
        bool Config(IntPtr hwnd, string deviceClass);

        /// <summary>
        /// IDisposable.Dispose method
        /// </summary>
        void Dispose();

        /// <summary>
        /// Harvests the results of a previously issued <see cref="TapiLine.BeginUncompleteCall"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginCompleteCall</param>
        void EndUncompleteCall(IAsyncResult ar);

        /// <summary>
        /// This locates an address using the Dialable Number
        /// </summary>
        /// <param name="number">DN to locate</param>
        /// <returns>TapiAddress or null if not found.</returns>
        ITapiAddress FindAddress(string number);

        /// <summary>
        /// This forwards calls destined for all addresses on the specified line, according to the specified forwarding instructions. 
        /// Any specified incoming calls for that address are deflected to the other number by the switch. 
        /// This function provides a combination of forward and do-not-disturb features.
        /// </summary>
        /// <param name="forwardInstructions">The forwarding instructions to apply</param>
        /// <param name="numRingsNoAnswer">Number of rings before a call is considered a "no answer." If dwNumRingsNoAnswer is out of range, the actual value is set to the nearest value in the allowable range.</param>
        /// <param name="param">Optional call parameters - only used if a consultation call is returned; otherwise ignored.  May be null for default parameters</param>
        ITapiCall Forward(ForwardInfo[] forwardInstructions, int numRingsNoAnswer, MakeCallParams param);

        /// <summary>
        /// Returns the phone device associated with this line.
        /// </summary>
        /// <returns></returns>
        TapiPhone GetAssociatedPhone();

        /// <summary>
        /// This returns a call using the call-id.
        /// </summary>
        /// <param name="callId">Callid</param>
        /// <returns>TapiCall object</returns>
        ITapiCall GetCallById(int callId);

        /// <summary>
        /// Returns all the calls on this line device.
        /// </summary>
        /// <returns>Array of calls</returns>
        ITapiCall[] GetCalls();

        /// <summary>
        /// This returns an "opaque" data structure object, the contents of which are specific to the line (service provider) and device class. 
        /// The data structure object stores the current configuration of a media-stream device associated with the line device.
        /// </summary>
        /// <param name="deviceClass">Specifies the device class of the device whose configuration is requested.</param>
        /// <returns>Opaque data block which may be passed back</returns>
        byte[] GetDeviceConfig(string deviceClass);

        /// <summary>
        /// Returns a device ID handle from an identifier.
        /// </summary>
        /// <param name="identifier">Identifier to lookup</param>
        /// <returns>Handle or null</returns>
        int? GetDeviceID(string identifier);

        /// <summary>
        /// This returns a device identifier for the specified device class associated with the call
        /// </summary>
        /// <param name="deviceClass">Device Class</param>
        /// <returns>string or byte[]</returns>
        object GetExternalDeviceInfo(string deviceClass);

        /// <summary>
        /// Returns the device id for the MIDI input device.  This identifier may be passed to "midiInOpen" to get a HMIDI handle.
        /// </summary>
        /// <returns>MIDI Device identifier</returns>
        int? GetMidiInDeviceID();

        /// <summary>
        /// Returns the device id for the MIDI output device.  This identifier may be passed to "midiOutOpen" to get a HMIDI handle.
        /// </summary>
        /// <returns>MIDI Device identifier</returns>
        int? GetMidiOutDeviceID();

        /// <summary>
        /// Returns the device id for the wave input device.  This identifier may be passed to "waveInOpen" to get a HWAVE handle.
        /// </summary>
        /// <returns>Wave Device identifier</returns>
        int? GetWaveInDeviceID();

        /// <summary>
        /// Returns the device id for the wave output device.  This identifier may be passed to "waveOutOpen" to get a HWAVE handle.
        /// </summary>
        /// <returns>Wave Device identifier</returns>
        int? GetWaveOutDeviceID();

        /// <summary>
        /// This places a call on the first available address of the line.
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <returns><see cref="TapiCall"/> object or null.</returns>
        ITapiCall MakeCall(string address);

        /// <summary>
        /// This places a call on the first available address of the line.
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <param name="country"><see cref="Country"/> object (null for default).</param>
        /// <param name="param">Optional <see cref="MakeCallParams"/> to use when dialing.</param>
        /// <returns><see cref="TapiCall"/> object or null.</returns>
        ITapiCall MakeCall(string address, Country country, MakeCallParams param);

        /// <summary>
        /// This opens the line in non-owner (monitor) mode so that new and existing calls can be viewed but not manipulated.
        /// </summary>
        void Monitor();

        /// <summary>
        /// This method is used to negotiate extension versions for the TSP.  It is only necessary if the application intends to use device-specific extensions.
        /// </summary>
        /// <param name="minVersion">Minimum version to negotiate to</param>
        /// <param name="maxVersion">Maximum version to negotiate to</param>
        /// <param name="dsc">Callback for any device-specific notification</param>
        /// <returns>Negotiated extensions version</returns>
        int NegotiateExtensions(int minVersion, int maxVersion, EventHandler<DeviceSpecificEventArgs> dsc);

        /// <summary>
        /// This method opens the line and allows it to be used to place or receive calls.
        /// </summary>
        /// <param name="mediaModes"><see cref="MediaModes"/> which will be used by the application</param>
        void Open(MediaModes mediaModes);

        /// <summary>
        /// This method opens the line and allows it to be used to place or receive calls.
        /// </summary>
        /// <param name="mediaModes"><see cref="MediaModes"/> which will be used by the application</param>
        /// <param name="addressId">Address index to only monitor a single address</param>
        void Open(MediaModes mediaModes, int addressId);

        /// <summary>
        /// This sets the line-specific device information.
        /// </summary>
        /// <param name="deviceClass">Specifies the device class of the device whose configuration is requested.</param>
        /// <param name="data">Data obtained from a previous call to <see cref="TapiLine.GetDeviceConfig"/>.</param>
        void SetDeviceConfig(string deviceClass, byte[] data);

        /// <summary>
        /// Returns a System.String representing this line object
        /// </summary>
        /// <returns>String</returns>
        string ToString();

        /// <summary>
        /// This method translates the input number to a dialable number for this line and <see cref="LocationInformation"/>
        /// </summary>
        /// <param name="number">Number to translate</param>
        /// <param name="callingCard">Calling card to use for call</param>
        /// <param name="options">TranslationOptions</param>
        /// <returns>Dialable number</returns>
        NumberInfo TranslateNumber(string number, CallingCard callingCard, TranslationOptions options);

        /// <summary>
        /// This method translates the input number to a dialable number for this line and <see cref="LocationInformation"/>
        /// </summary>
        /// <param name="number">Number to translate</param>
        /// <param name="options">TranslationOptions</param>
        /// <returns>Dialable number</returns>
        NumberInfo TranslateNumber(string number, TranslationOptions options);

        /// <summary>
        /// Cancels the specified call completion request on the specified line
        /// </summary>
        /// <param name="completionId">Original completion id from <see cref="TapiCall.CompleteCall"/>.</param>
        void UncompleteCall(int completionId);
    }
}