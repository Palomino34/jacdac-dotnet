/** Autogenerated file. Do not edit. */
using Jacdac;
using System;

namespace Jacdac {

    /// <summary>
    /// A roving robot.
    /// Implements a client for the Rover service.
    /// </summary>
    /// <seealso cref="https://microsoft.github.io/jacdac-docs/services/rover/" />
    public partial class RoverClient : SensorClient
    {
        public RoverClient(JDBus bus, string name)
            : base(bus, ServiceClasses.Rover, name)
        {
        }

        /// <summary>
        /// The current position and orientation of the robot., x: cm,y: cm,vx: cm/s,vy: cm/s,heading: °
        /// </summary>
        public (float, float, float, float, float) Kinematics
        {
            get
            {
                return ((float, float, float, float, float))this.GetRegisterValue((ushort)RoverReg.Kinematics, RoverRegPack.Kinematics, 1);
            }
        }


    }
}