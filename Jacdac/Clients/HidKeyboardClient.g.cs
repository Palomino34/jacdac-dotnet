/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac.Clients {

    /// <summary>
    /// Control a HID keyboard.
     /// 
     /// The codes for the key (selectors) is defined in the [HID Keyboard
     /// specification](https://usb.org/sites/default/files/hut1_21.pdf), chapter 10 Keyboard/Keypad Page, page 81.
     /// Modifiers are in page 87.
     /// 
     /// The device keeps tracks of the key state and is able to clear it all with the clear command.
    /// Implements a client for the HID Keyboard service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/hidkeyboard/" />
    public partial class HidKeyboardClient : Client
    {
        public HidKeyboardClient(JDBus bus, string name)
            : base(bus, name, ServiceClasses.HidKeyboard)
        {
        }


        
        /// <summary>
        /// Clears all pressed keys.
        /// </summary>
        public void Clear()
        {
            this.SendCmd((ushort)HidKeyboardCmd.Clear);
        }

    }
}