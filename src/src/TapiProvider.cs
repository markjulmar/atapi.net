// TapiProvider.cs
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

namespace JulMar.Atapi
{
    /// <summary>
    /// This class represents a single installed service provider
    /// </summary>
    public class TapiProvider
    {
        internal TapiProvider(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// The name of the TSP
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The permanent provider ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Override to return the provider name
        /// </summary>
        /// <returns>String with provider name</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
