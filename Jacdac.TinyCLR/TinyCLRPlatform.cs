﻿using GHIElectronics.TinyCLR.Data.Json;
using GHIElectronics.TinyCLR.Devices.Jacdac.Transport;
using GHIElectronics.TinyCLR.Native;
using Jacdac.TinyCLR.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Jacdac
{
    public partial class Platform
    {
        static Platform()
        {
            Platform.Crc16 = JacdacSerialWireController.Crc;
            var id = DeviceInformation.GetUniqueId();
            // TODO: compress device id into 8 bytes
            var jid = Util.Slice(id, 0, 8);
            Platform.DeviceId = jid;
            for (var i = 8; i < id.Length; ++i)
            {
                jid[i % jid.Length] |= (byte)(id[i] << 4);
            }
            Platform.DeviceId = jid;
            Platform.DeviceDescription = DeviceInformation.DeviceName;
            var version = DeviceInformation.Version;
            var major = (ushort)((version >> 48) & 0xFFFF);
            var minor = (ushort)((version >> 32) & 0xFFFF);
            var build = (ushort)((version >> 16) & 0xFFFF);
            var revision = (ushort)((version >> 0) & 0xFFFF);
            Platform.FirmwareVersion = major + "." + minor + "." + build + "." + revision;
            Platform.RealTimeClock = RealTimeClockVariant.Crystal;
            Platform.CreateClock = () =>
            {
                var start = DateTime.Now;
                return () => DateTime.Now - start;
            };
        }
    }

    public partial class ServiceSpecificationCatalog
    {
        static ServiceSpecificationCatalog()
        {
            SpecificationReader = ServiceReader;
            SpecificationResolver = ServiceResolver;
        }

        static ServiceSpec ServiceReader(byte[] buffer)
        {
            try
            {
                var text = System.Text.UTF8Encoding.UTF8.GetString(buffer);
                Debug.WriteLine($"parse\n{text}");

                var jobject = JsonConverter.Deserialize(text) as JObject;
                var res = new ServiceSpec();
                var sc = jobject["serviceClass"].Value as JValue;
                res.serviceClass = (uint)(ulong)sc.Value;
                res.name = (jobject["name"].Value as JValue).Value.ToString();
                var regs = jobject["registers"].Value as JArray;
                res.registers = new ServiceRegisterSpec[regs.Length];
                for (var i = 0; i < regs.Length; i++)
                {
                    var jreg = regs.Items[i] as JObject;
                    var reg = res.registers[i] = new ServiceRegisterSpec();
                    reg.code = (ushort)(ulong)(jreg["code"].Value as JValue).Value;
                    reg.name = (jreg["name"].Value as JValue).Value.ToString();
                    reg.flags = (ServiceRegisterFlag)(ulong)(jreg["flags"].Value as JValue).Value;
                    reg.packf = (jreg["packf"].Value as JValue).Value.ToString();
                    var jfields = jreg["fields"]?.Value as JArray;
                    if (jfields != null)
                    {
                        var fields = reg.fields = new string[jfields.Length];
                        for (var j = 0; j < fields.Length; j++)
                        {
                            var jfield = jfields.Items[j] as JValue;
                            fields[j] = jfield.Value.ToString();
                        }
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        static byte[] ServiceResolver(string url)
        {
            var certificates = Resources.GetBytes(Resources.BinaryResources.GitHubCertificate);
            var certx509 = new X509Certificate[] { new X509Certificate(certificates) };
            using (var req = HttpWebRequest.Create(url) as HttpWebRequest)
            {
                req.KeepAlive = false;
                req.Accept = "application/json";
                req.ReadWriteTimeout = 5000;
                req.HttpsAuthentCerts = certx509;
                using (var res = req.GetResponse() as HttpWebResponse)
                {
                    Debug.WriteLine($"{url} -> {res.StatusCode}");
                    if (res.StatusCode == HttpStatusCode.OK)
                        using (var stream = res.GetResponseStream())
                        {
                            var mem = new MemoryStream();
                            var read = 0;
                            var buf = new byte[1024];
                            do
                            {
                                read = stream.Read(buf, 0, buf.Length);
                                mem.Write(buf, 0, read);
                            } while (read > 0);
                            return mem.ToArray();
                        }
                }
            }
            return null;
        }
    }
}