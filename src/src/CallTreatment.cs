// CallTreatment.cs
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

using JulMar.Atapi.Interop;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class provides information on the type of call treatment, such as music, recorded announcement, or silence, on the current call.
    /// </summary>
    public class CallTreatment
    {
        /// <summary>
        /// When the call is not actively connected to a device (offering or onhold), the party hears silence. 
        /// </summary>
        public const int Silence = NativeMethods.LINECALLTREATMENT_SILENCE;
        /// <summary>
        /// When the call is not actively connected to a device (offering or onhold), the party hears ringback tone. 
        /// </summary>
        public const int Ringback = NativeMethods.LINECALLTREATMENT_RINGBACK;
        /// <summary>
        /// When the call is not actively connected to a device (offering or onhold), the party hears busy signal. 
        /// </summary>
        public const int Busy = NativeMethods.LINECALLTREATMENT_BUSY;
        /// <summary>
        /// When the call is not actively connected to a device (offering or onhold), the party hears music. 
        /// </summary>
        public const int Music = NativeMethods.LINECALLTREATMENT_MUSIC;

        /// <summary>
        /// The id for this treatment (predefined or custom)
        /// </summary>
        public readonly int Id;
        /// <summary>
        /// The textual name for this treatment
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">name</param>
        internal CallTreatment(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
