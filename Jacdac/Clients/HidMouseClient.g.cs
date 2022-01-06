/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac {

    /// <summary>
    /// Controls a HID mouse.
    /// Implements a client for the HID Mouse service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/hidmouse/" />
    public partial class HidMouseClient : Client
    {
        public HidMouseClient(JDBus bus, string name)
            : base(bus, ServiceClasses.HidMouse, name)
        {
        }


        /// <summary>
        /// Sets the up/down state of one or more buttons.
        /// A `Click` is the same as `Down` followed by `Up` after 100ms.
        /// A `DoubleClick` is two clicks with `150ms` gap between them (that is, `100ms` first click, `150ms` gap, `100ms` second click).
        /// </summary>
        public void SetButton(HidMouseButton buttons, HidMouseButtonEvent event)
        {
            this.SendCmdPacked((ushort)HidMouseCmd.SetButton, HidMouseCmdPack.SetButton, new object[] { buttons, event });
        }

        /// <summary>
        /// Moves the mouse by the distance specified.
        /// If the time is positive, it specifies how long to make the move.
        /// </summary>
        public void Move(int dx, int dy, uint time)
        {
            this.SendCmdPacked((ushort)HidMouseCmd.Move, HidMouseCmdPack.Move, new object[] { dx, dy, time });
        }

        /// <summary>
        /// Turns the wheel up or down. Positive if scrolling up.
        /// If the time is positive, it specifies how long to make the move.
        /// </summary>
        public void Wheel(int dy, uint time)
        {
            this.SendCmdPacked((ushort)HidMouseCmd.Wheel, HidMouseCmdPack.Wheel, new object[] { dy, time });
        }

    }
}