/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac {

    /// <summary>
    /// Runs machine learning models.
     /// 
     /// Only models with a single input tensor and a single output tensor are supported at the moment.
     /// Input is provided by Sensor Aggregator service on the same device.
     /// Multiple instances of this service may be present, if more than one model format is supported by a device.
    /// Implements a client for the Model Runner service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/modelrunner/" />
    public partial class ModelRunnerClient : Client
    {
        public ModelRunnerClient(JDBus bus, string name)
            : base(bus, ServiceClasses.ModelRunner, name)
        {
        }

        /// <summary>
        /// When register contains `N > 0`, run the model automatically every time new `N` samples are collected.
        /// Model may be run less often if it takes longer to run than `N * sampling_interval`.
        /// The `outputs` register will stream its value after each run.
        /// This register is not stored in flash., 
        /// </summary>
        public uint AutoInvokeEvery
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)ModelRunnerReg.AutoInvokeEvery, ModelRunnerRegPack.AutoInvokeEvery, 1);
            }
            set
            {
                
                this.SetRegisterValue((ushort)ModelRunnerReg.AutoInvokeEvery, ModelRunnerRegPack.AutoInvokeEvery, 1, value);
            }

        }

        /// <summary>
        /// The time consumed in last model execution., _: us
        /// </summary>
        public uint LastRunTime
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)ModelRunnerReg.LastRunTime, ModelRunnerRegPack.LastRunTime, 1);
            }
        }

        /// <summary>
        /// Number of RAM bytes allocated for model execution., _: B
        /// </summary>
        public uint AllocatedArenaSize
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)ModelRunnerReg.AllocatedArenaSize, ModelRunnerRegPack.AllocatedArenaSize, 1);
            }
        }

        /// <summary>
        /// The size of the model in bytes., _: B
        /// </summary>
        public uint ModelSize
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)ModelRunnerReg.ModelSize, ModelRunnerRegPack.ModelSize, 1);
            }
        }

        /// <summary>
        /// Textual description of last error when running or loading model (if any)., 
        /// </summary>
        public string LastError
        {
            get
            {
                return (string)this.GetRegisterValue((ushort)ModelRunnerReg.LastError, ModelRunnerRegPack.LastError, 1);
            }
        }

        /// <summary>
        /// The type of ML models supported by this service.
        /// `TFLite` is flatbuffer `.tflite` file.
        /// `ML4F` is compiled machine code model for Cortex-M4F.
        /// The format is typically present as first or second little endian word of model file., 
        /// </summary>
        public ModelRunnerModelFormat Format
        {
            get
            {
                return (ModelRunnerModelFormat)this.GetRegisterValue((ushort)ModelRunnerReg.Format, ModelRunnerRegPack.Format, 1);
            }
        }

        /// <summary>
        /// A version number for the format., 
        /// </summary>
        public uint FormatVersion
        {
            get
            {
                return (uint)this.GetRegisterValue((ushort)ModelRunnerReg.FormatVersion, ModelRunnerRegPack.FormatVersion, 1);
            }
        }

        /// <summary>
        /// (Optional) If present and true this service can run models independently of other
        /// instances of this service on the device., 
        /// </summary>
        public bool Parallel
        {
            get
            {
                return (bool)this.GetRegisterValue((ushort)ModelRunnerReg.Parallel, ModelRunnerRegPack.Parallel, 1);
            }
        }


        /// <summary>
        /// Open pipe for streaming in the model. The size of the model has to be declared upfront.
        /// The model is streamed over regular pipe data packets.
        /// The format supported by this instance of the service is specified in `format` register.
        /// When the pipe is closed, the model is written all into flash, and the device running the service may reset.
        /// </summary>
        public void SetModel(uint model_size)
        {
            this.SendCmdPacked((ushort)ModelRunnerCmd.SetModel, ModelRunnerCmdPack.SetModel, new object[] { model_size });
        }

    }
}