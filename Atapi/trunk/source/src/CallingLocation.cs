// CallingLocation.cs
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

using JulMar.Atapi.Interop;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class describes a location used to provide an address translation context.
    /// </summary>
    public class CallingLocation
    {
        /// <summary>
        /// Permanent identifier that identifies the location. 
        /// </summary>
        public readonly int Id;
        /// <summary>
        /// String that describes the location in a user-friendly manner.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Country for this location
        /// </summary>
        public readonly Country Country;
        /// <summary>
        /// String specifying the city/area code associated with the location. This information, along with the country code, can be used by 
        /// applications to "default" entry fields for the user when entering phone numbers, to encourage the entry of proper canonical numbers
        /// </summary>
        public readonly string CityCode;
        /// <summary>
        /// Preferred Calling card id
        /// </summary>
        public readonly CallingCard PreferredCallingCard;
        /// <summary>
        /// The access code to be dialed before calls to addresses in the local calling area.
        /// </summary>
        public readonly string LocalAccessCode;
        /// <summary>
        /// The access code to be dialed before calls to long-distance addresses.
        /// </summary>
        public readonly string LongDistanceAccessCode;

        /// <summary>
        /// The toll prefix list for the location. The string array contains only prefixes consisting of the digits "0" through "9".
        /// </summary>
        public string[] TollPrefixes
        {
            get { return (string[]) _tollPrefixes.Clone(); }
        }

        /// <summary>
        /// String containing the dial digits and modifier characters that should be prefixed to the dialable string to cancel call waiting for this location.
        /// </summary>
        public readonly string CancelCallWaitingPrefix;

        private readonly string[] _tollPrefixes;
        private readonly int _features;

        /// <summary>
        /// Constructor
        /// </summary>
        internal CallingLocation(int id, CallingCard card, string name, Country country, string cityCode, string localCode, string ldCode, string prefixes, string cancelCW, int features)
        {
            Id = id;
            Name = name;
            PreferredCallingCard = card;
            Country = country;
            CityCode = cityCode;
            LocalAccessCode = localCode;
            LongDistanceAccessCode = ldCode;
            _tollPrefixes = prefixes.Length > 0 ? prefixes.Split(',') : new string[0];
            CancelCallWaitingPrefix = cancelCW;
            _features = features;
        }

        /// <summary>
        /// The default dialing mode at this location is pulse dialing. If this returns true, then TranslateAddress will insert a "P" dial modifier at the beginning 
        /// of the dialable string returned when this location is selected. Otherwise, TranslateAddress will insert a "T" dial modifier at the beginning of the dialable string.
        /// </summary>
        public bool CanPulseDial
        {
            get { return (_features & NativeMethods.LINELOCATIONOPTION_PULSEDIAL) > 0; }
        }

        /// <summary>
        /// This provides an override for the ToString method.
        /// </summary>
        /// <returns>String representing this calling location</returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}, Country={2}, CityCode={3}", Id, Name, Country, CityCode);
        }
    }
}
