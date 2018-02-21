using System;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class represents a single Tapi Address object.
    /// </summary>
    public interface ITapiAddress
    {
        /// <summary>
        /// The Dialable number for the address. This will never be blank.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// This gets or sets the number of rings that must occur before an incoming call is answered.  This can be used to implement a "toll-saver" style application.
        /// </summary>
        int AnswerRingCount { get; set; }

        /// <summary>
        /// Returns the list of active calls on the address.
        /// </summary>
        ITapiCall[] Calls { get; }

        /// <summary>
        /// Returns the <see>AddressCapabilities</see> capabilities structure.
        /// </summary>
        AddressCapabilities Capabilities { get; }

        /// <summary>
        /// The numeric address ID
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The <see>TapiLine</see> associated with the address.
        /// </summary>
        ITapiLine Line { get; }

        /// <summary>
        /// Returns the <see>AddressStatus</see> status structure.
        /// </summary>
        AddressStatus Status { get; }

        /// <summary>
        /// This event is raised when the information associated with a call changes.
        /// </summary>
        event EventHandler<CallInfoChangeEventArgs> CallInfoChanged;

        /// <summary>
        /// This event is raised when a call on this address changes state.
        /// </summary>
        event EventHandler<CallStateEventArgs> CallStateChanged;

        /// <summary>
        /// This event is raised when the status of the address changes.
        /// </summary>
        event EventHandler<AddressInfoChangeEventArgs> Changed;

        /// <summary>
        /// This event is raised when a new call is discovered on the address.  It is not raised initially when the 
        /// owner line is opened and existing calls are there.
        /// </summary>
        event EventHandler<NewCallEventArgs> NewCall;

        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="inData">Input data</param>
        /// <param name="acb">Callback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult BeginDeviceSpecific(byte[] inData, AsyncCallback acb, object state);

        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="featureCode">Numeric feature code to execute</param>
        /// <param name="inData">Input data</param>
        /// <param name="acb">Callback</param>
        /// <param name="state">State data</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult BeginDeviceSpecific(int featureCode, byte[] inData, AsyncCallback acb, object state);

        /// <summary>
        /// This cancels any forwarding request that is currently in effect on this address
        /// </summary>
        void CancelForward();

        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="inData">Input data</param>
        /// <returns>Output data</returns>
        byte[] DeviceSpecific(byte[] inData);

        /// <summary>
        /// This method executes device-specific functionality on the underlying service provider.
        /// </summary>
        /// <param name="featureCode">Numeric feature code to execute</param>
        /// <param name="inData">Input data</param>
        /// <returns>Output data</returns>
        byte[] DeviceSpecific(int featureCode, byte[] inData);

        /// <summary>
        /// This method harvests the results from a <see cref="TapiAddress.BeginDeviceSpecific(byte[], AsyncCallback, object)"/> call.
        /// </summary>
        /// <param name="ar">IAsyncResult from BeginDeviceSpecific</param>
        /// <returns>Output data</returns>
        byte[] EndDeviceSpecific(IAsyncResult ar);

        /// <summary>
        /// This locates all the matching calls based on call state.
        /// </summary>
        /// <param name="requestedCallstates">Callstate desired</param>
        /// <returns>TapiCall array</returns>
        ITapiCall[] FindCallsByCallState(CallState requestedCallstates);

        /// <summary>
        /// This forwards calls destined for this address according to the specified forwarding instructions. 
        /// Any specified incoming calls for that address are deflected to the other number by the switch. 
        /// This function provides a combination of forward and do-not-disturb features.
        /// </summary>
        /// <param name="forwardInstructions">The forwarding instructions to apply</param>
        /// <param name="numRingsNoAnswer">Number of rings before a call is considered a "no answer." If dwNumRingsNoAnswer is out of range, the actual value is set to the nearest value in the allowable range.</param>
        /// <param name="param">Optional call parameters - only used if a consultation call is returned; otherwise ignored.  May be null for default parameters</param>
        ITapiCall Forward(ForwardInfo[] forwardInstructions, int numRingsNoAnswer, MakeCallParams param);

        /// <summary>
        /// This returns a device identifier for the specified device class associated with the call
        /// </summary>
        /// <param name="deviceClass">Device Class</param>
        /// <returns>string or byte[]</returns>
        object GetExternalDeviceInfo(string deviceClass);

        /// <summary>
        /// Places a new call on the address
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <returns>New <see>TapiCall</see> object.</returns>
        ITapiCall MakeCall(string address);

        /// <summary>
        /// Places a new call on the address
        /// </summary>
        /// <param name="address">Number to dial</param>
        /// <param name="countryCode">Country code</param>
        /// <param name="param">Optional <see>MakeCallParams</see> to use while dialing</param>
        /// <returns>New <see cref="TapiCall"/> object.</returns>
        ITapiCall MakeCall(string address, int countryCode, MakeCallParams param);

        /// <summary>
        /// This picks up a call alerting at the specified destination address and returns a call handle for the picked-up call. 
        /// If invoked with null for the alertingAddress parameter, a group pickup is performed. If required by the device, groupId specifies the 
        /// group identifier to which the alerting station belongs.
        /// </summary>
        /// <param name="alertingAddress">Address to retrieve call from</param>
        /// <param name="groupId">Optional group ID, can be null or empty</param>
        /// <returns>New <see cref="TapiCall"/> object.</returns>
        ITapiCall Pickup(string alertingAddress, string groupId);

        /// <summary>
        /// This method is used to establish a conference call
        /// </summary>
        /// <param name="conferenceCount"># of parties anticipated the conference</param>
        /// <param name="mcp">Call parameters for created consultation call</param>
        /// <param name="consultCall">Returning consultation call</param>
        /// <returns>Conference call</returns>
        ITapiCall SetupConference(int conferenceCount, MakeCallParams mcp, out TapiCall consultCall);

        /// <summary>
        /// This retrieves a call off a parked address
        /// </summary>
        /// <param name="parkedAddress">Address to retrieve call from</param>
        /// <returns>New <see cref="TapiCall"/> object.</returns>
        ITapiCall Unpark(string parkedAddress);
    }
}