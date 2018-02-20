namespace JulMar.Atapi
{
    /// <summary>
    /// This class holds the Call Features for a given <see cref="TapiCall"/> or <see cref="TapiAddress"/>.
    /// </summary>
    public interface ICallFeatureSet
    {
        /// <summary>
		/// Accept the call
		/// </summary>
        bool CanAccept { get; }

        /// <summary>
		/// Add the call to the conference
		/// </summary>
        bool CanAddToConference { get; }

        /// <summary>
		/// Answer the call
		/// </summary>
        bool CanAnswer { get; }

        /// <summary>
		/// Blind transfer the call
		/// </summary>
        bool CanBlindTransfer { get; }

        /// <summary>
		/// Complete the call
		/// </summary>
        bool CanCompleteCall { get; }

        /// <summary>
		/// Complete a 2-step transfer of the call
		/// </summary>
        bool CanCompleteTransfer { get; }

        /// <summary>
		/// Dial additional digits on the call
		/// </summary>
        bool CanDial { get; }

        /// <summary>
		/// Drop the call
		/// </summary>
        bool CanDrop { get; }

        /// <summary>
		/// Gather DTMF digits on the call.
		/// </summary>
        bool CanGatherDigits { get; }

        /// <summary>
		/// Generate DTMF digits
		/// </summary>
        bool CanGenerateDigits { get; }

        /// <summary>
		/// Generate tones
		/// </summary>
        bool CanGenerateTone { get; }

        /// <summary>
		/// Place the call on hold
		/// </summary>
        bool CanHold { get; }

        /// <summary>
		/// Monitor digits on the call
		/// </summary>
        bool CanMonitorDigits { get; }

        /// <summary>
		/// Monitor media changes on the call
		/// </summary>
        bool CanMonitorMedia { get; }

        /// <summary>
		/// Monitor tones on the call
		/// </summary>
        bool CanMonitorTones { get; }

        /// <summary>
		/// Park the call
		/// </summary>
        bool CanPark { get; }

        /// <summary>
		/// Prepare to add the call to a conference
		/// </summary>
        bool CanPrepareAddToConference { get; }

        /// <summary>
		/// Redirect the call
		/// </summary>
        bool CanRedirect { get; }

        /// <summary>
		/// Release OOB UUI data associated with the call
		/// </summary>
        bool CanReleaseUserUserInfo { get; }

        /// <summary>
		/// Remove the call from a conference
		/// </summary>
        bool CanRemoveFromConference { get; }

        /// <summary>
		/// Secure the call from outside interference
		/// </summary>
        bool CanSecureCall { get; }

        /// <summary>
		/// Send OOB UUI to the peer.
		/// </summary>
        bool CanSendUserUserInfo { get; }

        /// <summary>
		/// Set call data to travel with the call appearance
		/// </summary>
        bool CanSetCallData { get; }

        /// <summary>
		/// Set call parameters on the call
		/// </summary>
        bool CanSetCallParams { get; }

        /// <summary>
		/// Change media control for the call
		/// </summary>
        bool CanSetMediaControl { get; }

        /// <summary>
        /// Set the Quality of Service associated with the call
        /// </summary>
        bool CanSetQos { get; }

        /// <summary>
		/// Set/Change terminal information
		/// </summary>
        bool CanSetTerminal { get; }

        /// <summary>
		/// Change the call treatment
		/// </summary>
        bool CanSetTreatment { get; }

        /// <summary>
		/// Setup a conference for the call
		/// </summary>
        bool CanSetupConference { get; }

        /// <summary>
		/// Initiate a 2-step transfer
		/// </summary>
        bool CanSetupTransfer { get; }

        /// <summary>
		/// Swap the call with a held/active call
		/// </summary>
        bool CanSwapHold { get; }

        /// <summary>
		/// Bring the call off hold
		/// </summary>
        bool CanUnhold { get; }
    }
}