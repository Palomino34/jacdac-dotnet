﻿
using GHIElectronics.TinyCLR.Devices.Uart;
using GHIElectronics.TinyCLR.Pins;
using System.Diagnostics;
using System.Threading;
using Jacdac;

namespace Jacdac_RgbLed
{
    internal class Program
    {
        static void Main()
        {
            // Display enable
            Display.Enable();


            DoTestJacdacBlink();

            //DoTestJacdacAdafruit();
        }

        static void DoTestJacdacBlink()
        {
            // jacdac
            Display.WriteLine("Configuration Jacdac....");
            var transport = new UartTransport(new GHIElectronics.TinyCLR.Devices.Jacdac.JacdacController(SC20260.UartPort.Uart4, new UartSetting { SwapTxRxPin = true }));
            transport.PacketReceived += JacdacController_PacketReceived;
            transport.ErrorReceived += JacdacController_ErrorReceived;

            var bus = new JDBus(transport);
            bus.DeviceConnected += Bus_DeviceConnected;
            bus.DeviceDisconnected += Bus_DeviceDisconnected;

            Display.WriteLine("Waiting for Jacdac...");
            Blink(transport);
        }

        private static void Bus_DeviceDisconnected(JDNode sensor, DeviceEventArgs e)
        {
            Display.WriteLine($"{e.Device} disconnected");
        }

        private static void Bus_DeviceConnected(JDNode sensor, DeviceEventArgs e)
        {
            Display.WriteLine($"{e.Device} connected");
        }

        private static void Blink(UartTransport transport)
        {
            var ledOnPacket = Packet.FromBinary(new byte[] { 0xa3, 0x2f, 0x08, 0x01, 0x46, 0x2e, 0xcd, 0xca, 0x66, 0xca, 0x4d, 0x19, 0x04, 0x01, 0x80, 0x00, 0x7f, 0x7f, 0x7f, 0x0 });
            var ledOffPacket = Packet.FromBinary(new byte[] { 0x9c, 0xa8, 0x08, 0x01, 0x46, 0x2e, 0xcd, 0xca, 0x66, 0xca, 0x4d, 0x19, 0x04, 0x01, 0x80, 0x00, 0x00, 0x00, 0x00, 0x0 });

            Display.WriteLine("led is blinking...");

            while (true)
            {
                transport.SendPacket(ledOnPacket);

                Thread.Sleep(250);

                transport.SendPacket(ledOffPacket);
                Thread.Sleep(250);
            }
        }

        private static void JacdacController_ErrorReceived(Transport sender, TransportErrorReceivedEventArgs args)
        {
            switch (args.Error)
            {
                case TransportError.Frame:
                    if (args.Data != null)
                    {
                        var str = "Frame error: ";

                        for (var i = 0; i < args.Data.Length; i++)
                        {
                            str += args.Data[i].ToString("x2");
                        }

                        Debug.WriteLine(str);
                    }
                    break;

                case TransportError.BufferFull:
                    (sender as UartTransport).controller.ClearReadBuffer();
                    Debug.WriteLine("Buffer full");
                    break;

                case TransportError.Overrun:
                    Debug.WriteLine("Overrun");
                    break;


            }            
        }

        private static void JacdacController_PacketReceived(Transport sender, Packet packet)
        {
            Debug.WriteLine("=>>>>>>>>>>>>>>>>>>>>>>>>>> New packet >>>>>>>>>>>>>>>>>>>>>>>>");            
            Debug.WriteLine("packet crc             = " + packet.Crc.ToString("x2"));
            Debug.WriteLine("device_identifier      = " + packet.DeviceId);
            //Debug.WriteLine("size                   = " + packet.Size);
            //Debug.WriteLine("frame_flags            = " + packet.FrameFlags);
            //Debug.WriteLine("is_command             = " + packet.IsCommand);
            //Debug.WriteLine("is_report              = " + packet.IsReport);
            //Debug.WriteLine("multicommand_class     = " + packet.MulticommandClass.ToString("x2"));
            //Debug.WriteLine("requires_ack           = " + packet.IsRequiresAck);
            //Debug.WriteLine("isEvent                = " + packet.IsEvent);
            //Debug.WriteLine("event_code             = " + packet.EventCode.ToString("x2"));
            //Debug.WriteLine("reg_code               = " + packet.RegisterCode.ToString("x2"));
            //Debug.WriteLine("eventCounter           = " + packet.EventCounter.ToString("x2"));

            Debug.WriteLine(" ");
        }
    }


}