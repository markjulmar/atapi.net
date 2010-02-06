// Tones.cs
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
    /// This structure contains information about a tone to be generated through <see cref="TapiCall.GenerateTone(ToneModes, int)"/>.
    /// </summary>
    public struct CustomTone
    {
        private readonly int _frequency;
        private readonly int _cadenceOn;
        private readonly int _cadenceOff;
        private readonly int _volume;

        /// <summary>
        /// Constructor for the CustomTone
        /// </summary>
        /// <param name="frequency">Frequency of the tone</param>
        /// <param name="onDuration">Duration in milliseconds for the "on" portion</param>
        /// <param name="offDuration">Duration in milliseconds for the "off" portion</param>
        /// <param name="volume">Volume</param>
        public CustomTone(int frequency, int onDuration, int offDuration, int volume)
        {
            _frequency = frequency;
            _cadenceOn = onDuration;
            _cadenceOff = offDuration;
            _volume = volume;
        }

        /// <summary>
        /// Frequency of this tone component, in hertz. A service provider may adjust (round up or down) the frequency specified by the application to fit its resolution.
        /// </summary>
        public int Frequency
        {
            get { return _frequency; }
        }

        /// <summary>
        /// Length of the "on" duration of the cadence of the custom tone to be generated, in milliseconds. Zero means no tone is generated. 
        /// </summary>
        public int CadenceOn
        {
            get { return _cadenceOn; }
        }

        /// <summary>
        /// Length of the "off" duration of the cadence of the custom tone to be generated, in milliseconds. Zero means no off time, that is, a constant tone. 
        /// </summary>
        public int CadenceOff
        {
            get { return _cadenceOff; }
        }

        /// <summary>
        /// Volume level at which the tone is to be generated. A value of 0x0000FFFF represents full volume, and a value of 0x00000000 is silence.
        /// </summary>
        public int Volume
        {
            get { return _volume; }
        }

        /// <summary>
        /// Override of the Equals method to provide an efficient implementation
        /// </summary>
        /// <param name="obj">Right side comparison</param>
        /// <returns>True/False</returns>
        public override bool Equals(object obj)
        {
            if (obj is CustomTone)
            {
                var rhs = (CustomTone) obj;
                return (rhs == this);
            }
            return false;
        }

        /// <summary>
        /// Override of GetHashCode
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return (_frequency + _volume + _cadenceOff + _cadenceOn);
        }

        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="lhs">Left-hand side of comparison</param>
        /// <param name="rhs">Right-hand side of comparison</param>
        /// <returns>true/false</returns>
        public static bool operator==(CustomTone lhs, CustomTone rhs)
        {
            return (rhs._frequency == lhs._frequency &&
                rhs._cadenceOff == lhs._cadenceOff &&
                rhs._cadenceOn == lhs._cadenceOn &&
                rhs._volume == lhs._volume);
        }

        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="lhs">Left-hand side of comparison</param>
        /// <param name="rhs">Right-hand side of comparison</param>
        /// <returns>true/false</returns>
        public static bool operator !=(CustomTone lhs, CustomTone rhs)
        {
            return !(lhs == rhs);
        }
    }

    /// <summary>
    /// This structure describes a tone to be monitored.  This is passed to the <see cref="TapiCall.BeginMonitoringTones"/>.
    /// </summary>
    public struct MonitorTone
    {
        private readonly object _key;
        private readonly int _duration;
        private readonly int _frequency1;
        private readonly int _frequency2;
        private readonly int _frequency3;

        /// <summary>
        /// Constructor for the MonitorTone structure
        /// </summary>
        /// <param name="duration">Duration of tone in milliseconds</param>
        /// <param name="frequency">First frequency of tri-tone</param>
        /// <param name="stateKey">State key for application (null for none)</param>
        public MonitorTone(int duration, int frequency, object stateKey) : this(duration, frequency, 0, 0, stateKey) {/* */}

        /// <summary>
        /// Constructor for the MonitorTone structure
        /// </summary>
        /// <param name="duration">Duration of tone in milliseconds</param>
        /// <param name="frequency1">First frequency of tri-tone</param>
        /// <param name="frequency2">Second frequency of tri-tone</param>
        /// <param name="stateKey">State key for application (null for none)</param>
        public MonitorTone(int duration, int frequency1, int frequency2, object stateKey) : this(duration, frequency1, frequency2, 0, stateKey) {/* */}

        /// <summary>
        /// Constructor for the MonitorTone structure
        /// </summary>
        /// <param name="duration">Duration of tone in milliseconds</param>
        /// <param name="frequency1">First frequency of tri-tone</param>
        /// <param name="frequency2">Second frequency of tri-tone</param>
        /// <param name="frequency3">Third frequency of tri-tone</param>
        /// <param name="stateKey">State key for application (null for none)</param>
        public MonitorTone(int duration, int frequency1, int frequency2, int frequency3, object stateKey)
        {
            _key = stateKey;
            _duration = duration;
            _frequency1 = frequency1;
            _frequency2 = frequency2;
            _frequency3 = frequency3;
        }

        /// <summary>
        /// Identifies the tone to the application
        /// </summary>
        public object Key
        {
            get { return _key; }
        }

        /// <summary>
        /// Duration of time during which the tone should be present before a detection is made, in milliseconds. 
        /// </summary>
        public int Duration
        {
            get { return _duration; }
        }

        /// <summary>
        /// First frequency of the tone, in hertz. 
        /// </summary>
        public int Frequency1
        {
            get { return _frequency1; }
        }

        /// <summary>
        /// Second frequency of the tone, in hertz. Can be zero.
        /// </summary>
        public int Frequency2
        {
            get { return _frequency2; }
        }

        /// <summary>
        /// Third frequency of the tone, in hertz. Can be zero.
        /// </summary>
        public int Frequency3
        {
            get { return _frequency3; }
        }

        /// <summary>
        /// Override of the Equals method to provide an efficient implementation
        /// </summary>
        /// <param name="obj">Right side comparison</param>
        /// <returns>True/False</returns>
        public override bool Equals(object obj)
        {
            if (obj is MonitorTone)
            {
                var rhs = (MonitorTone)obj;
                return (rhs == this);
            }
            return false;
        }

        /// <summary>
        /// Override of GetHashCode
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return (_duration + _frequency1 + _frequency2 + _frequency3);
        }

        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="lhs">Left-hand side of comparison</param>
        /// <param name="rhs">Right-hand side of comparison</param>
        /// <returns>true/false</returns>
        public static bool operator ==(MonitorTone lhs, MonitorTone rhs)
        {
            return (rhs._duration == lhs._duration &&
                rhs._frequency1 == lhs._frequency1 &&
                rhs._frequency2 == lhs._frequency2 &&
                rhs._frequency3 == lhs._frequency3 &&
                rhs._key == lhs._key);
        }

        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="lhs">Left-hand side of comparison</param>
        /// <param name="rhs">Right-hand side of comparison</param>
        /// <returns>true/false</returns>
        public static bool operator !=(MonitorTone lhs, MonitorTone rhs)
        {
            return !(lhs == rhs);
        }
    }
}
