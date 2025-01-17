﻿using Jacdac.Clients;
using System;

namespace Jacdac.Samples
{
    internal class ButtonTracker : ISample
    {
        public uint ProductIdentifier => 0x36ad4f2b;

        public void Run(JDBus bus)
        {
            var button = new ButtonClient(bus, "btn");
            button.Down += (sender, args) => Console.WriteLine("button down");
            button.Up += (sender, args) => Console.WriteLine("button up");
            button.Hold += (sender, args) => Console.WriteLine("button hold");
        }
    }
}
