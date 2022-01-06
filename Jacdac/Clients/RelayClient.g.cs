/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac {

    /// <summary>
    /// A switching relay.
    /// Implements a client for the Relay service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/relay/" />
    public partial class RelayClient : Client
    {
        public RelayClient(JDBus bus, string name)
            : base(bus, ServiceClasses.Relay, name)
        {
        }

        /// <summary>
        /// Indicates whether the relay circuit is currently energized (closed) or not., 
        /// </summary>
        public bool Closed
        {
            get
            {
                return (bool)this.GetRegisterValue((ushort)RelayReg.Closed, RelayRegPack.Closed, 1);
            }
            set
            {
                
                this.SetRegisterValue((ushort)RelayReg.Closed, RelayRegPack.Closed, 1, value);
            }

        }

        /// <summary>
        /// (Optional) Describes the type of relay used., 
        /// </summary>
        public RelayVariant Variant
        {
            get
            {
                return (RelayVariant)this.GetRegisterValue((ushort)RelayReg.Variant, RelayRegPack.Variant, 1);
            }
        }

        /// <summary>
        /// (Optional) Maximum switching current for a resistive load., _: mA
        /// </summary>
        public uint MaxSwitchingCurrent
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)RelayReg.MaxSwitchingCurrent, RelayRegPack.MaxSwitchingCurrent, 1);
            }
        }


    }
}