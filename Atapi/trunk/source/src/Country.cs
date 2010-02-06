// Country.cs
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

namespace JulMar.Atapi
{
    /// <summary>
    /// This class represents a single country identified by TAPI dialing rules.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Country/region identifier of the entry. The country/region identifier is an internal identifier that allows multiple entries to exist in the country/region 
        /// list with the same country/region code, for example, all countries or regions in North America and the Caribbean share the country/region code 1, but require 
        /// separate entries in the list. 
        /// </summary>
        public readonly int Id;
        /// <summary>
        /// Country/region code of the country/region represented by the entry; that is, the digits dialed in an international call. 
        /// Only this value should be displayed to users. Country/region identifiers should never be displayed
        /// </summary>
        public readonly int CountryCode;
        /// <summary>
        /// Specifies the name of the country/region
        /// </summary>
        public readonly string Name;
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
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="countryCode"></param>
        /// <param name="name"></param>
        /// <param name="areaCodeRule"></param>
        /// <param name="longDistRule"></param>
        /// <param name="intRule"></param>
        internal Country(int id, int countryCode, string name, string areaCodeRule, string longDistRule, string intRule)
        {
            Id = id;
            CountryCode = countryCode;
            Name = name;
            SameAreaCodeRules = areaCodeRule;
            LongDistanceRules = longDistRule;
            InternationalRules = intRule;
        }

        /// <summary>
        /// Returns a System.String representing this country
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
