// LocationInformation.cs
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
using System.Collections.Generic;
using JulMar.Atapi.Interop;
using System.Runtime.InteropServices;

namespace JulMar.Atapi
{
    /// <summary>
    /// This class represents all the collective location and translation information for the TAPI application
    /// </summary>
    public class LocationInformation
    {
        private readonly TapiManager _mgr;
        private readonly List<Country> _countries = new List<Country>();
        private readonly List<CallingCard> _cards = new List<CallingCard>();
        private CallingLocation _currLocation;
        private readonly List<CallingLocation> _clocations = new List<CallingLocation>();

        internal LocationInformation(TapiManager mgr)
        {
            _mgr = mgr;
            ReadCountryList();
            ReadCallingLocations();
        }

        /// <summary>
        /// This returns a list of the countries supported by TAPI.
        /// </summary>
        public Country[] Countries
        {
            get { return _countries.ToArray(); }
        }

        /// <summary>
        /// Returns the defined calling cards
        /// </summary>
        public CallingCard[] CallingCards
        {
            get { return _cards.ToArray(); }
        }

        /// <summary>
        /// This returns a list of available calling locations for this server
        /// </summary>
        public CallingLocation[] CallingLocations
        {
            get { return _clocations.ToArray(); }
        }

        /// <summary>
        /// This gets and sets the current calling location for address translation
        /// </summary>
        public CallingLocation CurrentLocation
        {
            get
            {
                return _currLocation;
            }

            set
            {
                SetLocationInfo(value);
                _currLocation = value;
            }
        }

        /// <summary>
        /// This retrieves a <see cref="Country "/> object using the country code.
        /// </summary>
        /// <param name="countryCode">Country code to locate</param>
        /// <returns>Country object</returns>
        public Country GetCountryByCode(int countryCode)
        {
            foreach (Country c in _countries)
            {
                if (c.CountryCode == countryCode)
                    return c;
            }
            return null;
        }

        private void SetLocationInfo(CallingLocation loc)
        {
            int rc = NativeMethods.lineSetCurrentLocation(_mgr.LineHandle, (loc != null) ? loc.Id : 0);
            if (rc != 0)
                throw new TapiException("lineSetCurrentLocation failed", rc);
        }

        private void ReadCountryList()
        {
            var lcl = new LINECOUNTRYLIST();

            int rc, neededSize = 30 * 1024; // 30K up front
            do
            {
                lcl.dwTotalSize = neededSize;
                IntPtr pLcl = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(lcl, pLcl, true);
                rc = NativeMethods.lineGetCountry(0, (int)TapiVersion.V21, pLcl);
                Marshal.PtrToStructure(pLcl, lcl);
                if (lcl.dwNeededSize > neededSize)
                {
                    neededSize = lcl.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    var rawBuffer = new byte[lcl.dwUsedSize];
                    Marshal.Copy(pLcl, rawBuffer, 0, lcl.dwUsedSize);
                    for (int i = 0; i < lcl.dwNumCountries; i++)
                        _countries.Add(ReadCountryEntry(lcl, rawBuffer, i));
                }
                Marshal.FreeHGlobal(pLcl);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);
        }

        private static Country ReadCountryEntry(LINECOUNTRYLIST lcl, byte[] rawBuffer, int pos)
        {
            var lce = new LINECOUNTRYENTRY();
            int size = Marshal.SizeOf(lce);
            pos = lcl.dwCountryListOffset + (pos * size);
            IntPtr pLce = Marshal.AllocHGlobal(size);
            Marshal.Copy(rawBuffer, pos, pLce, size);
            Marshal.PtrToStructure(pLce, lce);
            Marshal.FreeHGlobal(pLce);

            return new Country(lce.dwCountryID, lce.dwCountryCode,
                    NativeMethods.GetString(rawBuffer, lce.dwCountryNameOffset, lce.dwCountryNameSize, NativeMethods.STRINGFORMAT_UNICODE),
                    NativeMethods.GetString(rawBuffer, lce.dwSameAreaRuleOffset, lce.dwSameAreaRuleSize, NativeMethods.STRINGFORMAT_UNICODE),
                    NativeMethods.GetString(rawBuffer, lce.dwLongDistanceRuleOffset, lce.dwLongDistanceRuleSize, NativeMethods.STRINGFORMAT_UNICODE),
                    NativeMethods.GetString(rawBuffer, lce.dwInternationalRuleOffset, lce.dwInternationalRuleSize, NativeMethods.STRINGFORMAT_UNICODE));
        }

        private void ReadCallingLocations()
        {
            var ltc = new LINETRANSLATECAPS();

            int rc, neededSize = 4092;
            do
            {
                ltc.dwTotalSize = neededSize;
                IntPtr pLtc = Marshal.AllocHGlobal(neededSize);
                Marshal.StructureToPtr(ltc, pLtc, true);
                rc = NativeMethods.lineGetTranslateCaps(_mgr.LineHandle, (int)TapiVersion.V21, pLtc);
                Marshal.PtrToStructure(pLtc, ltc);
                if (ltc.dwNeededSize > neededSize)
                {
                    neededSize = ltc.dwNeededSize;
                    rc = NativeMethods.LINEERR_STRUCTURETOOSMALL;
                }
                else if (rc == NativeMethods.LINEERR_OK)
                {
                    var rawBuffer = new byte[ltc.dwUsedSize];
                    Marshal.Copy(pLtc, rawBuffer, 0, ltc.dwUsedSize);
                    for (int i = 0; i < ltc.dwNumCards; i++)
                        _cards.Add(ReadCallingCard(ltc, rawBuffer, i));
                    for (int i = 0; i < ltc.dwNumLocations; i++)
                        _clocations.Add(ReadLocationEntry(ltc, rawBuffer, i));
                }
                Marshal.FreeHGlobal(pLtc);
            }
            while (rc == NativeMethods.LINEERR_STRUCTURETOOSMALL);

            // Assign the current country
            for (int index = 0; index < _clocations.Count; index++)
            {
                var location = _clocations[index];
                if (ltc.dwCurrentLocationID == location.Id)
                {
                    _currLocation = location;
                    break;
                }
            }
        }

        private static CallingCard ReadCallingCard(LINETRANSLATECAPS lcl, byte[] rawBuffer, int pos)
        {
            var lce = new LINECARDENTRY();
            int size = Marshal.SizeOf(lce);
            pos = lcl.dwCardListOffset + (pos * size);
            IntPtr pLce = Marshal.AllocHGlobal(size);
            Marshal.Copy(rawBuffer, pos, pLce, size);
            Marshal.PtrToStructure(pLce, lce);
            Marshal.FreeHGlobal(pLce);

            return new CallingCard(lce.dwPermanentCardID,
                NativeMethods.GetString(rawBuffer, lce.dwCardNameOffset, lce.dwCardNameSize, NativeMethods.STRINGFORMAT_UNICODE), lce.dwCardNumberDigits,
                NativeMethods.GetString(rawBuffer, lce.dwSameAreaRuleOffset, lce.dwSameAreaRuleSize, NativeMethods.STRINGFORMAT_UNICODE),
                NativeMethods.GetString(rawBuffer, lce.dwLongDistanceRuleOffset, lce.dwLongDistanceRuleSize, NativeMethods.STRINGFORMAT_UNICODE),
                NativeMethods.GetString(rawBuffer, lce.dwInternationalRuleOffset, lce.dwInternationalRuleSize, NativeMethods.STRINGFORMAT_UNICODE), lce.dwOptions);
        }

        private CallingLocation ReadLocationEntry(LINETRANSLATECAPS lcl, byte[] rawBuffer, int pos)
        {
            var lce = new LINELOCATIONENTRY();
            int size = Marshal.SizeOf(lce);
            pos = lcl.dwLocationListOffset + (pos * size);
            IntPtr pLce = Marshal.AllocHGlobal(size);
            Marshal.Copy(rawBuffer, pos, pLce, size);
            Marshal.PtrToStructure(pLce, lce);
            Marshal.FreeHGlobal(pLce);

            // Locate the country
            Country locCountry = null;
            foreach (Country country in _countries)
            {
                if (country.Id == lce.dwCountryID)
                {
                    locCountry = country;
                    break;
                }
            }

            // Locate the default calling card (if any)
            CallingCard card = null;
            if (lce.dwPreferredCardID < _cards.Count)
                card = _cards[lce.dwPreferredCardID];

            return new CallingLocation(lce.dwPermanentLocationID, card,
                NativeMethods.GetString(rawBuffer, lce.dwLocationNameOffset, lce.dwLocationNameSize, NativeMethods.STRINGFORMAT_UNICODE), locCountry,
                NativeMethods.GetString(rawBuffer, lce.dwCityCodeOffset, lce.dwCityCodeSize, NativeMethods.STRINGFORMAT_UNICODE),
                NativeMethods.GetString(rawBuffer, lce.dwLocalAccessCodeOffset, lce.dwLocalAccessCodeSize, NativeMethods.STRINGFORMAT_UNICODE),
                NativeMethods.GetString(rawBuffer, lce.dwLongDistanceAccessCodeOffset, lce.dwLongDistanceAccessCodeSize, NativeMethods.STRINGFORMAT_UNICODE),
                NativeMethods.GetString(rawBuffer, lce.dwTollPrefixListOffset, lce.dwTollPrefixListSize, NativeMethods.STRINGFORMAT_UNICODE),
                NativeMethods.GetString(rawBuffer, lce.dwCancelCallWaitingOffset, lce.dwCancelCallWaitingSize, NativeMethods.STRINGFORMAT_UNICODE), lce.dwOptions);
        }

    }
}
