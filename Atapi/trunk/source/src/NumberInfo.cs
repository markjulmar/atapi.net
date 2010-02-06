// NumberInfo.cs
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
    /// This class is returned from the <see cref="TapiLine.TranslateNumber(string, TranslationOptions)"/> method as the resultant translated number.
    /// </summary>
    public class NumberInfo
    {
        /// <summary>
        /// The number that can be displayed to the user for confirmation.
        /// </summary>
        public readonly string DisplayNumber;
        /// <summary>
        /// The translated output that can be passed to the MakeCall, Dial, or other function requiring a dialable string. 
        /// Ancillary fields such as name and subaddress are included in this output string if they were in the input string. 
        /// This string may contain private information such as calling card numbers. It should not be displayed to the user, 
        /// to prevent inadvertent visibility to unauthorized persons
        /// </summary>
        public readonly string DialableNumber;
        /// <summary>
        /// The destination country if available (may be null).
        /// </summary>
        public readonly Country DestinationCountry;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lto">Translation output</param>
        /// <param name="buff">Raw buffer</param>
        /// <param name="locInfo"><see cref="LocationInformation"/></param>
        internal NumberInfo(LINETRANSLATEOUTPUT lto, byte[] buff, LocationInformation locInfo)
        {
            DisplayNumber = NativeMethods.GetString(buff, lto.dwDisplayableStringOffset, lto.dwDisplayableStringSize, NativeMethods.STRINGFORMAT_UNICODE);
            DialableNumber = NativeMethods.GetString(buff, lto.dwDialableStringOffset, lto.dwDialableStringSize, NativeMethods.STRINGFORMAT_UNICODE);
            DestinationCountry = (lto.dwDestCountry != 0) ? locInfo.GetCountryByCode(lto.dwDestCountry) : null;
        }
    }
}
