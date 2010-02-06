// TapiManager.cs
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
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using JulMar.Atapi.Interop;
using System.Globalization;

[assembly: System.Security.AllowPartiallyTrustedCallers]

namespace JulMar.Atapi
{
	/// <summary>
	/// The TapiManager class is the starting point for the library.  It does the initial negotiation with TAPI
    /// and maintains the list of lines which can be worked with.
	/// </summary>
	public sealed class TapiManager : IDisposable
    {
        private readonly string _appName;
        private HTLINEAPP _hTapiLine = new HTLINEAPP();
        private HTPHONEAPP _hTapiPhone = new HTPHONEAPP();
        private int _lineVersion;
        private int _phoneVersion;
        private int _nextId = -1;
        private List<TapiLine> _lineArray;
        private List<TapiPhone> _phoneArray;
        private readonly ManualResetEvent _evtReceivedLineEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _evtReceivedPhoneEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _evtStop = new ManualResetEvent(false);
        private readonly Thread _workerThread;
        private List<PendingTapiRequest> _requests = new List<PendingTapiRequest>();
        private readonly List<TapiProvider> _providers = new List<TapiProvider>();
        private LocationInformation _locInfo;

        /// <summary>
        /// This event is raised when TAPI requires a reinitialization
        /// </summary>
        public event EventHandler ReinitRequired;
        /// <summary>
        /// This event is raised when TAPI indicates a new line is present
        /// </summary>
        public event EventHandler<LineAddedEventArgs> LineAdded;
        /// <summary>
        /// This event is raised when TAPI indicates a line has been dynamically removed from the system.
        /// </summary>
        public event EventHandler<LineRemovedEventArgs> LineRemoved;
        /// <summary>
        /// This event is raised when a new call is presented on any line.
        /// </summary>
        public event EventHandler<NewCallEventArgs> NewCall;
        /// <summary>
        /// This event is raised when a call on this address changes state
        /// </summary>
        public event EventHandler<CallStateEventArgs> CallStateChanged;
        /// <summary>
        /// This event is raised when the information associated with a call changes
        /// </summary>
        public event EventHandler<CallInfoChangeEventArgs> CallInfoChanged;
        /// <summary>
        /// This event is raised when an address on this line changes
        /// </summary>
        public event EventHandler<AddressInfoChangeEventArgs> AddressChanged;
        /// <summary>
        /// This event is raised when the status or capabilities of the line has changed.
        /// </summary>
        public event EventHandler<LineInfoChangeEventArgs> LineChanged;
        /// <summary>
        /// This event is raised when the line is ringing.
        /// </summary>
        public event EventHandler<RingEventArgs> LineRinging;
        /// <summary>
        /// This event is raised when TAPI indicates a new phone is present
        /// </summary>
        public event EventHandler<PhoneAddedEventArgs> PhoneAdded;
        /// <summary>
        /// This event is raised when TAPI indicates a phone has been dynamically removed from the system.
        /// </summary>
        public event EventHandler<PhoneRemovedEventArgs> PhoneRemoved;


        /// <summary>
        /// Constructor for the TapiManager
        /// </summary>
        /// <param name="appname">Application Name</param>
        public TapiManager(string appname)
            : this(appname, TapiVersion.V31)
        {
        }

        /// <summary>
        /// Constructor for the TapiManager
        /// </summary>
        /// <param name="appName">Application Name</param>
        /// <param name="ver">TapiVersion</param>
        public TapiManager(string appName, TapiVersion ver)
        {
            _appName = appName;
            _lineVersion = (int) ver;
            _phoneVersion = (int)ver;
            _workerThread = new Thread(ProcessTapiMessages) {Name = "Tapi Message Processor"};
            _lineArray = new List<TapiLine>();
            _phoneArray = new List<TapiPhone>();
		}

        /// <summary>
        /// This method initializes the TAPI infrastructure.
        /// </summary>
        /// <returns>true/false success indicator</returns>
        public bool Initialize()
        {
            if (!_hTapiLine.IsInvalid)
                throw new TapiException("TAPI has already been initialized.", NativeMethods.LINEERR_OPERATIONUNAVAIL);

            _evtStop.Reset();

            int numLines = InitializeLineDevices();
            int numPhones = InitializePhoneDevices();

            if (numLines > 0 || numPhones > 0)
            {
                _workerThread.Start();
                ReadProviderList();
                _locInfo = new LocationInformation(this);

                return true;
            }

            return false;
        }

        private int InitializeLineDevices()
        {
            var parms = new LINEINITIALIZEEXPARAMS();
            parms.dwTotalSize = parms.dwNeededSize = parms.dwUsedSize = Marshal.SizeOf(parms);
            parms.dwOptions = NativeMethods.LINEINITIALIZEEXOPTION_USEEVENT;
            parms.dwCompletionKey = 0;
            parms.hEvent = IntPtr.Zero;

            int numDevices; IntPtr hTapi;

            int rc = NativeMethods.lineInitializeEx(out hTapi, IntPtr.Zero, null, _appName,
                    out numDevices, ref _lineVersion, ref parms);
            if (rc == NativeMethods.LINEERR_OK)
            {
                _hTapiLine = new HTLINEAPP(hTapi, true);
                _evtReceivedLineEvent.SafeWaitHandle = new SafeWaitHandle(parms.hEvent, false);

                _lineArray = new List<TapiLine>();
                for (int i = 0; i < numDevices; i++)
                {
                    var line = new TapiLine(this, i);
                    line.NewCall += HandleNewCall;
                    line.CallStateChanged += HandleCallStateChanged;
                    line.CallInfoChanged += HandleCallInfoChanged;
                    line.AddressChanged += HandleAddressChanged;
                    line.Changed += HandleLineChanged;
                    line.Ringing += HandleLineRinging;

                    _lineArray.Add(line);
                }
            }
            else
            {
                numDevices = 0;
            }
            return numDevices;
        }

        private int InitializePhoneDevices()
        {
            var parms = new PHONEINITIALIZEEXPARAMS();
            parms.dwTotalSize = parms.dwNeededSize = parms.dwUsedSize = Marshal.SizeOf(parms);
            parms.dwOptions = NativeMethods.PHONEINITIALIZEEXOPTION_USEEVENT;
            parms.dwCompletionKey = 0;
            parms.hEvent = IntPtr.Zero;

            int numDevices; IntPtr hTapi;

            int rc = NativeMethods.phoneInitializeEx(out hTapi, IntPtr.Zero, null, _appName,
                    out numDevices, ref _phoneVersion, ref parms);
            if (rc == NativeMethods.PHONEERR_OK)
            {
                _hTapiPhone = new HTPHONEAPP(hTapi, true);
                _evtReceivedPhoneEvent.SafeWaitHandle = new SafeWaitHandle(parms.hEvent, false);

                _phoneArray = new List<TapiPhone>();
                for (int i = 0; i < numDevices; i++)
                {
                    _phoneArray.Add(new TapiPhone(this, i));
                }
            }
            else
            {
                numDevices = 0;
            }
            return numDevices;
        }

        /// <summary>
        /// This returns the list of <see cref="TapiLine"/> objects which can be worked with.
        /// </summary>
        public TapiLine[] Lines
        {
            get
            {
                return _lineArray.ToArray();
            }
        }

        /// <summary>
        /// This returns the list of <see cref="TapiPhone"/> objects which can be used.
        /// </summary>
        public TapiPhone[] Phones
        {
            get
            {
                return _phoneArray.ToArray();
            }
        }

        /// <summary>
        /// This returns a list of <see cref="TapiProvider"/> objects represented installed .TSP drivers.
        /// </summary>
        public TapiProvider[] Providers
        {
            get
            {
                return _providers == null ? new TapiProvider[0] : _providers.ToArray();
            }
        }

        /// <summary>
        /// This returns the <see cref="LocationInformation"/> object which holds location, calling card and country information.
        /// </summary>
        public LocationInformation LocationInformation
        {
            get { return _locInfo; }
        }

        /// <summary>
        /// This method shuts down the TAPI system and releases all handles.
        /// </summary>
        public void Shutdown()
        {
            _evtStop.Set();
            foreach (TapiLine line in _lineArray)
            {
                try
                {
                    line.Close();
                }
                catch
                {
                }
            }

            try 
            {
                _hTapiLine.Close();
            }
            catch
            {
                _hTapiLine.SetHandleAsInvalid();
            }

            try
            {
                _hTapiPhone.Close();
            }
            catch
            {
                _hTapiPhone.SetHandleAsInvalid();
            }

            // Wait for the worker
            if (!_workerThread.Join(5000))
            {
                _workerThread.Interrupt();
                _workerThread.Join();
            }

            _lineArray = new List<TapiLine>();
            _phoneArray = new List<TapiPhone>();

            foreach (PendingTapiRequest req in _requests)
                req.CompleteRequest(NativeMethods.LINEERR_OPERATIONFAILED);
            _requests = new List<PendingTapiRequest>();
        }

        private void ReadProviderList()
        {
            var lpl = new LINEPROVIDERLIST();

            int rc, neededSize = 1024;
            do
            {
                lpl.dwTotalSize = neededSize;
                IntPtr pLpl = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lpl, pLpl, true);
                rc = NativeMethods.lineGetProviderList((int)TapiVersion.V21, pLpl);
                Marshal.PtrToStructure(pLpl, lpl);
                if (lpl.dwNeededSize > neededSize)
                {
                    neededSize = lpl.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    var rawBuffer = new byte[lpl.dwUsedSize];
                    Marshal.Copy(pLpl, rawBuffer, 0, lpl.dwUsedSize);
                    for (int i = 0; i < lpl.dwNumProviders; i++)
                        _providers.Add(ReadTapiProviderInfo(lpl, rawBuffer, i));
                }
                Marshal.FreeHGlobal(pLpl);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
        }

        private static TapiProvider ReadTapiProviderInfo(LINEPROVIDERLIST lpl, byte[] rawBuffer, int pos)
        {
            var lpe = new LINEPROVIDERENTRY();
            int size = Marshal.SizeOf(lpe);
            pos = lpl.dwProviderListOffset + (pos * size);
            IntPtr pLpe = Marshal.AllocHGlobal(size);
            Marshal.Copy(rawBuffer, pos, pLpe, size);
            Marshal.PtrToStructure(pLpe, lpe);
            Marshal.FreeHGlobal(pLpe);

            return new TapiProvider(lpe.dwPermanentProviderID, 
                NativeMethods.GetString(rawBuffer, 
                lpe.dwProviderFilenameOffset, lpe.dwProviderFilenameSize, 
                NativeMethods.STRINGFORMAT_UNICODE));
        }

        void IDisposable.Dispose()
        {
            Shutdown();
        }

        internal HTLINEAPP LineHandle
        {
            get { return _hTapiLine; }
        }

        internal HTPHONEAPP PhoneHandle
        {
            get { return _hTapiPhone; }
        }

        /// <summary>
        /// This returns a specific line device using the permanent line id.
        /// </summary>
        /// <param name="permanentLineId">Id searching for</param>
        /// <returns>TapiLine</returns>
        public TapiLine GetLineByPermanentId(int permanentLineId)
        {
            lock (_lineArray)
            {
                foreach (TapiLine line in _lineArray)
                {
                    if (line.PermanentId == permanentLineId)
                        return line;
                }
            }

            // Not found
            return null;
        }

        /// <summary>
        /// Locates a name using the line name as the search criteria
        /// </summary>
        /// <param name="name">Name of the line</param>
        /// <param name="ignoreCase">True/False whether should be case-sensitive search</param>
        /// <returns>TapiLine or null</returns>
        public TapiLine GetLineByName(string name, bool ignoreCase)
        {
            if (!String.IsNullOrEmpty(name))
            {
                lock (_lineArray)
                {
                    foreach (TapiLine line in _lineArray)
                    {
                        if (string.Compare(line.Name, name, ignoreCase, CultureInfo.InvariantCulture) == 0)
                            return line;
                    }
                }
            }

            // Not found
            return null;
        }

        /// <summary>
        /// This returns a specific line phone using the permanent phone id.
        /// </summary>
        /// <param name="permanentLineId">Id searching for</param>
        /// <returns>TapiPhone</returns>
        public TapiPhone GetPhoneByPermanentId(int permanentLineId)
        {
            lock (_phoneArray)
            {
                foreach (TapiPhone phone in _phoneArray)
                {
                    if (phone.PermanentId == permanentLineId)
                        return phone;
                }
            }

            // Not found
            return null;
        }

		private void ProcessTapiMessages()
		{
            var arrWait = new WaitHandle[] { _evtStop, _evtReceivedLineEvent, _evtReceivedPhoneEvent };
            var msg = new LINEMESSAGE();
            ProcessTapiMessageDelegate ptmCb = ProcessTapiMessage;

            int rc = 0;

            bool stopEventRaised = false;
            while (!stopEventRaised)
            {
                try
                {
                    switch (WaitHandle.WaitAny(arrWait))
                    {
                        case 0:
                            stopEventRaised = true;
                            break;
                        case 1:
                            rc = NativeMethods.lineGetMessage(_hTapiLine, ref msg, 0);
                            break;
                        case 2:
                            rc = NativeMethods.phoneGetMessage(_hTapiPhone, ref msg, 0);
                            break;
                    }
                }

                // Someone called Shutdown with pending requests..
                catch (ObjectDisposedException)
                {
                    break;
                }

                if (rc == NativeMethods.LINEERR_OK)
                {
                    // Call on another worker thread - just in case the UI 
                    // made a blocking call and is waiting on the result from this
                    // thread.. if the WinForms/WPF app then attempts to marshal to 
                    // the UI thread from this callback it will deadlock.
                    ptmCb.BeginInvoke(msg,
                        delegate(IAsyncResult ar)
                        {
                            try
                            {
                                ptmCb.EndInvoke(ar);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("TAPI message exception: " + ex.Message);
                            }

                        }, null);
                }
            }
		}

        delegate void ProcessTapiMessageDelegate(LINEMESSAGE msg);

        private void ProcessTapiMessage(LINEMESSAGE msg)
        {
            switch (msg.dwMessageID)
            {
                case TapiEvent.LINE_CALLSTATE:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.OnCallStateChange(msg.dwParam1.ToInt32(), msg.dwParam2, (MediaModes)msg.dwParam3.ToInt32());
                    }
                    break;
                case TapiEvent.LINE_CALLINFO:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.OnCallInfoChange(msg.dwParam1.ToInt32());
                    }
                    break;
                case TapiEvent.LINE_GATHERDIGITS:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.OnGatherDigitsComplete(msg.dwParam1.ToInt32());
                    }
                    break;

                case TapiEvent.LINE_GENERATE:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.OnGenerateDigitsOrToneComplete(msg.dwParam1.ToInt32());
                    }
                    break;

                case TapiEvent.LINE_MONITORDIGITS:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.OnDigitDetected(msg.dwParam1.ToInt32(), msg.dwParam2.ToInt32());
                    }
                    break;

                case TapiEvent.LINE_MONITORMEDIA:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.OnMediaModeDetected((MediaModes)msg.dwParam1.ToInt32());
                    }
                    break;

                case TapiEvent.LINE_MONITORTONE:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.OnToneDetected(msg.dwParam1.ToInt32());
                    }
                    break;

                case TapiEvent.LINE_LINEDEVSTATE:
                    if (msg.dwParam1.ToInt32() == NativeMethods.LINEDEVSTATE_REINIT && msg.dwParam2.ToInt32() == 0)
                    {
                        if (ReinitRequired != null)
                            ReinitRequired(this, EventArgs.Empty);
                    }
                    goto case TapiEvent.LINE_CLOSE;

                case TapiEvent.LINE_DEVSPECIFIC:
                    {
                        TapiCall call = TapiCall.FindCallByHandle(msg.hDevice);
                        if (call != null)
                            call.Line.OnDeviceSpecific(call, msg.dwParam1, msg.dwParam2, msg.dwParam3);
                        else
                            goto case TapiEvent.LINE_CLOSE;
                    }
                    break;

                case TapiEvent.LINE_DEVSPECIFICFEATURE:
                    {
                        TapiLine line = null;
                        lock (_lineArray)
                        {
                            foreach (TapiLine currLine in _lineArray)
                            {
                                if (currLine.Handle.DangerousGetHandle() == msg.hDevice)
                                {
                                    line = currLine;
                                    break;
                                }
                            }
                        }
                        if (line != null)
                            line.OnDeviceSpecific(null, msg.dwParam1, msg.dwParam2, msg.dwParam3);
                    }
                    break;

                case TapiEvent.LINE_AGENTSTATUS:
                case TapiEvent.LINE_AGENTSTATUSEX:
                case TapiEvent.LINE_AGENTSPECIFIC:
                case TapiEvent.LINE_AGENTSESSIONSTATUS:
                case TapiEvent.LINE_PROXYREQUEST:
                case TapiEvent.LINE_PROXYSTATUS:
                case TapiEvent.LINE_QUEUESTATUS:
                case TapiEvent.LINE_REQUEST:
                    break;

                case TapiEvent.PHONE_BUTTON:
                case TapiEvent.PHONE_CLOSE:
                case TapiEvent.PHONE_DEVSPECIFIC:

                case TapiEvent.PHONE_REPLY:
                    HandleCompletion(msg.dwParam1.ToInt32(), msg.dwParam2.ToInt32());
                    break;
                
                case TapiEvent.PHONE_STATE:
                    break;

                case TapiEvent.PHONE_CREATE:
                    {
                        var newPhone = new TapiPhone(this, msg.dwParam1.ToInt32());
                        lock (_phoneArray)
                        {
                            _phoneArray.Add(newPhone);
                        }
                        if (PhoneAdded != null)
                            PhoneAdded(this, new PhoneAddedEventArgs(newPhone));
                    }
                    break;

                case TapiEvent.PHONE_REMOVE:
                    {
                        TapiPhone phone = null;
                        lock (_phoneArray)
                        {
                            if (msg.dwParam1.ToInt32() < _phoneArray.Count)
                                phone = _phoneArray[msg.dwParam1.ToInt32()];
                        }
                        if (phone != null)
                        {
                            phone.IsValid = false;
                            if (PhoneRemoved != null)
                                PhoneRemoved(this, new PhoneRemovedEventArgs(phone));
                        }
                    }
                    break;

                case TapiEvent.LINE_ADDRESSSTATE:
                case TapiEvent.LINE_CLOSE:
                case TapiEvent.LINE_APPNEWCALL:
                    if (msg.dwCallbackInstance.ToInt32() != 0)
                    {
                        try
                        {
                            var del = (TapiEventCallback)Marshal.GetDelegateForFunctionPointer(msg.dwCallbackInstance, typeof(TapiEventCallback));
                            if (del != null)
                            {
                                del(msg.dwMessageID, msg.dwParam1, msg.dwParam2, msg.dwParam3);
                            }
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex);
                        }
                    }
                    break;

                case TapiEvent.LINE_CREATE:
                    {
                        var newLine = new TapiLine(this, msg.dwParam1.ToInt32());
                        lock (_lineArray)
                        {
                            _lineArray.Add(newLine);
                        }
                        if (LineAdded != null)
                            LineAdded(this, new LineAddedEventArgs(newLine));
                    }
                    break;

                case TapiEvent.LINE_REMOVE:
                    {
                        TapiLine line = null;
                        lock (_lineArray)
                        {
                            if (msg.dwParam1.ToInt32() < _lineArray.Count)
                                line = _lineArray[msg.dwParam1.ToInt32()];
                        }
                        if (line != null)
                        {
                            line.IsValid = false;
                            if (LineRemoved != null)
                                LineRemoved(this, new LineRemovedEventArgs(line));
                        }
                    }
                    break;

                case TapiEvent.LINE_REPLY:
                    HandleCompletion(msg.dwParam1.ToInt32(), msg.dwParam2.ToInt32());
                    break;

                
                default:
                    break;
            }
        }

        internal void HandleCompletion(int reqId, int finalResult)
        {
            DateTime timeEntered = DateTime.Now;
            for (; ; )
            {
                // Wait up to 5 seconds for a completion event to show up.
                if ((DateTime.Now - timeEntered).TotalSeconds > 5)
                    break;

                lock (_requests)
                {
                    foreach (PendingTapiRequest req in _requests)
                    {
                        if (req.AsyncRequestId == reqId)
                        {
                            _requests.Remove(req);
                            req.CompleteRequest(finalResult);
                            return;
                        }
                    }
                }

                // Not found -- must be recent and being added; wait for it.
                Thread.Sleep(0);
            }
        }

		private void HandleNewCall(object sender, NewCallEventArgs e)
		{
            if (NewCall != null)
                NewCall(this, e);
		}

        private void HandleCallStateChanged(object sender, CallStateEventArgs e)
        {
            if (CallStateChanged != null)
                CallStateChanged(this, e);
        }

        private void HandleCallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            if (CallInfoChanged != null)
                CallInfoChanged(this, e);
        }

        private void HandleAddressChanged(object sender, AddressInfoChangeEventArgs e)
        {
            if (AddressChanged != null)
                AddressChanged(this, e);
        }

        private void HandleLineChanged(object sender, LineInfoChangeEventArgs e)
        {
            if (LineChanged != null)
                LineChanged(this, e);
        }

        private void HandleLineRinging(object sender, RingEventArgs e)
        {
            if (LineRinging != null)
                LineRinging(this, e);
        }

        internal IAsyncResult AddAsyncRequest(PendingTapiRequest req)
        {
            if (req.AsyncRequestId != 0)
            {
                lock (_requests)
                {
                    _requests.Add(req);
                }
            }
            return req;
        }

        internal int GetAsyncRequestid()
        {
            int id = Interlocked.Decrement(ref _nextId);
            if (id >= 0)
            {
                _nextId = -1;
                return GetAsyncRequestid();
            }
            return id;
        }
    }
}
