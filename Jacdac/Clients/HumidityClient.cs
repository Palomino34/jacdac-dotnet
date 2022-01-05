﻿namespace Jacdac.Clients
{
    public sealed partial class HumidityClient : SensorClient
    {
        readonly float missingHumidityValue;

        public HumidityClient(JDBus bus, string name, float missingHumidityValue = 0)
            : base(bus, name, Jacdac.HumidityConstants.ServiceClass)
        {
            this.missingHumidityValue = missingHumidityValue;
        }

        /// <summary>
        /// The relative humidity in percentage of full water saturation., _: %RH
        /// </summary>
        public float Humidity
        {
            get
            {
                this.RefreshReading();
                var value = this.GetRegister((ushort)Jacdac.HumidityReg.Humidity)?.Value(this.missingHumidityValue);
                return (float)value;
            }
        }

        /// <summary>
        /// (Optional) The real humidity is between `humidity - humidity_error` and `humidity + humidity_error`., _: %RH
        /// </summary>
        public bool TryGetHumidityError(out float value)
        {
            var v = this.GetRegister((ushort)Jacdac.HumidityReg.HumidityError)?.Value();
            if (v == null)
            {
                value = default(float);
                return false;
            }
            else
            {
                value = (float)v;
                return true;
            }
        }
    }
}