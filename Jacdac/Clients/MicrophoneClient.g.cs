/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac.Clients {

    /// <summary>
    /// A single-channel microphone.
    /// Implements a client for the Microphone service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/microphone/" />
    public partial class MicrophoneClient : Client
    {
        public MicrophoneClient(JDBus bus, string name)
            : base(bus, name, ServiceClasses.Microphone)
        {
        }

        /// <summary>
        /// Reads the <c>sampling_period</c> register value.
        /// Get or set microphone sampling period.
        /// Sampling rate is `1_000_000 / sampling_period Hz`., _: us
        /// </summary>
        public uint SamplingPeriod
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)MicrophoneReg.SamplingPeriod, MicrophoneRegPack.SamplingPeriod);
            }
            set
            {
                
                this.SetRegisterValue((ushort)MicrophoneReg.SamplingPeriod, MicrophoneRegPack.SamplingPeriod, value);
            }

        }


    }
}