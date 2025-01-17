/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac.Clients {

    /// <summary>
    /// A sensor measuring humidity of outside environment.
    /// Implements a client for the Humidity service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/humidity/" />
    public partial class HumidityClient : SensorClient
    {
        public HumidityClient(JDBus bus, string name)
            : base(bus, name, ServiceClasses.Humidity)
        {
        }

        /// <summary>
        /// Reads the <c>humidity</c> register value.
        /// The relative humidity in percentage of full water saturation., _: %RH
        /// </summary>
        public float Humidity
        {
            get
            {
                return (float)this.GetRegisterValue((ushort)HumidityReg.Humidity, HumidityRegPack.Humidity);
            }
        }

        /// <summary>
        /// Tries to read the <c>humidity_error</c> register value.
        /// The real humidity is between `humidity - humidity_error` and `humidity + humidity_error`., _: %RH
        /// </summary>
        bool TryGetHumidityError(out float value)
        {
            object[] values;
            if (this.TryGetRegisterValues((ushort)HumidityReg.HumidityError, HumidityRegPack.HumidityError, out values)) 
            {
                value = (float)values[0];
                return true;
            }
            else
            {
                value = default(float);
                return false;
            }
        }

        /// <summary>
        /// Reads the <c>min_humidity</c> register value.
        /// Lowest humidity that can be reported., _: %RH
        /// </summary>
        public float MinHumidity
        {
            get
            {
                return (float)this.GetRegisterValue((ushort)HumidityReg.MinHumidity, HumidityRegPack.MinHumidity);
            }
        }

        /// <summary>
        /// Reads the <c>max_humidity</c> register value.
        /// Highest humidity that can be reported., _: %RH
        /// </summary>
        public float MaxHumidity
        {
            get
            {
                return (float)this.GetRegisterValue((ushort)HumidityReg.MaxHumidity, HumidityRegPack.MaxHumidity);
            }
        }


    }
}