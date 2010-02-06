// CallingCard.cs
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
    /// Describes a calling card
    /// </summary>
    public class CallingCard
    {
        /// <summary>
        /// Permanent identifier that identifies the card. 
        /// </summary>
        public readonly int Id;
        /// <summary>
        /// String that describes the card in a user-friendly manner.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Number of digits in the existing card number. The card number itself is not returned for security reasons 
        /// (it is stored in scrambled form by TAPI). The application can use this to insert filler bytes into a text control in "password" mode to show that a number exists. 
        /// </summary>
        public readonly int DigitCount;
        /// <summary>
        /// The dialing rules applied to calls placed within the same area code.
        /// </summary>
        public readonly string SameAreaCodeRules;
        /// <summary>
        /// The dialing rules applied to calls identified as long distance within the same country code.
        /// </summary>
        public readonly string LongDistanceRules;
        /// <summary>
        /// The dialing rules applied to calls outside the country.
        /// </summary>
        public readonly string InternationalRules;
        /// <summary>
        /// Known features
        /// </summary>
        private readonly int _features;

        /// <summary>
        /// Constructor
        /// </summary>
        internal CallingCard(int id, string name, int digitCount, string areaCodeRule, string longDistRule, string intRule, int features)
        {
            Id = id;
            Name = name;
            DigitCount = digitCount;
            SameAreaCodeRules = areaCodeRule;
            LongDistanceRules = longDistRule;
            InternationalRules = intRule;
            _features = features;
        }

        /// <summary>
        /// This calling card has been hidden by the user. It is not shown by Dial Helper in the main listing of available calling cards, 
        /// but will be shown in the list of cards from which dialing rules can be copied. 
        /// </summary>
        public bool IsHidden
        {
            get
            {
                return (_features & NativeMethods.LINECARDOPTION_HIDDEN) > 0;
            }
        }

        /// <summary>
        /// This calling card is one of the predefined calling card definitions included with Telephony by Microsoft. It cannot be removed entirely using Dial Helper; 
        /// if the user attempts to remove it, it will become HIDDEN. It thus continues to be accessible for copying of dialing rules. 
        /// </summary>
        public bool IsPredefined
        {
            get
            {
                return (_features & NativeMethods.LINECARDOPTION_PREDEFINED) > 0; 
            }
        }

        /// <summary>
        /// Returns a System.String representing this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

    }
}
