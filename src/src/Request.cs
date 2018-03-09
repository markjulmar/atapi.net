// Request.cs
//
// This is a part of the TAPI Applications Classes .NET library (ATAPI)
//
// Copyright (c) 2005-2018 JulMar Technology, Inc.
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
using System.Threading;
using System.Diagnostics;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class implements the AsyncRequest support through IAsyncResult
    /// </summary>
    internal class PendingTapiRequest : IAsyncResult
    {
        private readonly Stopwatch _timeStarted = new Stopwatch();
        private readonly AsyncCallback _callback;
        private readonly ManualResetEvent _asyncWaitHandle;

        internal PendingTapiRequest(int reqId, AsyncCallback acb, object state)
            : this(reqId, acb, state, IntPtr.Zero, 0)
        {
        }

        internal PendingTapiRequest(int reqId, AsyncCallback acb, object state, IntPtr apiData, int size)
        {
            _asyncWaitHandle = new ManualResetEvent(false);
            AsyncRequestId = reqId;
            _timeStarted.Start();
            _callback = acb;
            AsyncState = state;
            ApiData = apiData;
            ApiDataSize = size;

            if (reqId == 0)
            {
                _timeStarted.Stop();
                _asyncWaitHandle.Set();
            }
        }

        internal int AsyncRequestId { get; private set; }
        internal IntPtr ApiData { get; private set; }
        internal int ApiDataSize { get; private set; }

        /// <summary>
        /// Result code from request
        /// </summary>
        public long Result { get; private set; }

        internal void CompleteRequest(long resultCode)
        {
            Result = resultCode;
            _timeStarted.Stop();
            _asyncWaitHandle.Set();
            if (_callback != null)
                _callback.BeginInvoke(this, ar => _callback.EndInvoke(ar), null);
        }

        /// <summary>
        /// Total elapsed time for the request
        /// </summary>
        public TimeSpan Elapsed
        {
            get { return _timeStarted.Elapsed; }
        }

        /// <summary>
        /// Total elapsed time in milliseconds for the request
        /// </summary>
        public long ElapsedMilliseconds
        {
            get { return _timeStarted.ElapsedMilliseconds; }
        }

        /// <summary>
        /// Async state object
        /// </summary>
        public object AsyncState { get; private set; }

        /// <summary>
        /// Async Wait Handle
        /// </summary>
        public WaitHandle AsyncWaitHandle
        {
            get { return _asyncWaitHandle; }
        }

        bool IAsyncResult.CompletedSynchronously
        {
            get { return (AsyncRequestId == 0); }
        }

        bool IAsyncResult.IsCompleted
        {
            get { return AsyncWaitHandle.WaitOne(0, true); }
        }
    }
}
