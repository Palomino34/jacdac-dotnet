/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac {

    /// <summary>
    /// A matrix of buttons connected as a keypad
    /// Implements a client for the Matrix Keypad service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/matrixkeypad/" />
    public partial class MatrixKeypadClient : Client
    {
        public MatrixKeypadClient(JDBus bus, string name)
            : base(bus, ServiceClasses.MatrixKeypad, name)
        {
        }

        /// <summary>
        /// Number of rows in the matrix, _: #
        /// </summary>
        public uint Rows
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)MatrixKeypadReg.Rows, MatrixKeypadRegPack.Rows, 1);
            }
        }

        /// <summary>
        /// Number of columns in the matrix, _: #
        /// </summary>
        public uint Columns
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)MatrixKeypadReg.Columns, MatrixKeypadRegPack.Columns, 1);
            }
        }

        /// <summary>
        /// (Optional) The type of physical keypad. If the variant is ``ElastomerLEDPixel``
        /// and the next service on the device is a ``LEDPixel`` service, it is considered
        /// as the service controlling the LED pixel on the keypad., 
        /// </summary>
        public MatrixKeypadVariant Variant
        {
            get
            {
                return (MatrixKeypadVariant)this.GetRegisterValue((ushort)MatrixKeypadReg.Variant, MatrixKeypadRegPack.Variant, 1);
            }
        }

        /// <summary>
        /// Emitted when a key, at the given index, goes from inactive (`pressed == 0`) to active.
        /// </summary>
        public event NodeEventHandler Down;

        /// <summary>
        /// Emitted when a key, at the given index, goes from active (`pressed == 1`) to inactive.
        /// </summary>
        public event NodeEventHandler Up;

        /// <summary>
        /// Emitted together with `up` when the press time was not longer than 500ms.
        /// </summary>
        public event NodeEventHandler Click;

        /// <summary>
        /// Emitted together with `up` when the press time was more than 500ms.
        /// </summary>
        public event NodeEventHandler LongClick;


    }
}