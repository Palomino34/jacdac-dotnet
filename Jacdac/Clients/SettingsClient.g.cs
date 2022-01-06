/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac.Clients {

    /// <summary>
    /// Non-volatile key-value storage interface for storing settings.
    /// Implements a client for the Settings service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/settings/" />
    public partial class SettingsClient : Client
    {
        public SettingsClient(JDBus bus, string name)
            : base(bus, name, ServiceClasses.Settings)
        {
        }

        /// <summary>
        /// Notifies that some setting have been modified.
        /// </summary>
        public event NodeEventHandler Change;


        
        /// <summary>
        /// Get the value of given setting. If no such entry exists, the value returned is empty.
        /// </summary>
        public void Get(string key)
        {
            this.SendCmdPacked((ushort)SettingsCmd.Get, SettingsCmdPack.Get, new object[] { key });
        }

    }
}