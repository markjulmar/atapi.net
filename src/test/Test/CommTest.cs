using System;
using System.Text;
using JulMar.Atapi;
using System.IO;

namespace EnumDevices
{
    class CommTest
    {
        static void RunCommTest()
        {
            TapiManager mgr = new TapiManager("EnumDevices");

            mgr.Initialize();

            foreach (TapiLine lineEx in mgr.Lines)
                Console.WriteLine(lineEx.Name);

            TapiLine line = mgr.GetLineByName("Conexant D110 MDC V.92 Modem", true);

            if (line != null)
            {
                line.Open(MediaModes.DataModem);
                TapiCall call = line.Addresses[0].MakeCall("2145551212");

                Console.WriteLine(call.GetCommDevice());

                try
                {
                    using (FileStream fs = call.GetCommStream())
                    {
                        byte[] data = ASCIIEncoding.ASCII.GetBytes("Hello");
                        fs.Write(data, 0, data.Length);
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            Console.WriteLine(sr.ReadToEnd());
                        }
                    }
                    call.Drop();
                }
                catch (Exception ex)
                {
                    call.Drop();
                    while (ex != null)
                    {
                        Console.WriteLine("{0}", ex.ToString());
                        ex = ex.InnerException;
                    }
                }
            }
            else
            {
                Console.WriteLine("Not found.");
            }

            Console.ReadLine();
            mgr.Shutdown();
        }

        private static void ShowCallingLocations(TapiManager mgr, TapiLine line)
        {
            foreach (CallingLocation li in mgr.LocationInformation.CallingLocations)
            {
                Console.WriteLine(li.ToString());
            }

            mgr.LocationInformation.CurrentLocation = mgr.LocationInformation.CallingLocations[1];
            Console.WriteLine("Current location: {0}", mgr.LocationInformation.CurrentLocation.Name);
            Console.WriteLine(line.TranslateNumber("+1 (310) 555-1212", TranslationOptions.CancelCallWaiting).DialableNumber);
        }

        static void DumpForwardingInfo(TapiManager mgr)
        {
            foreach (TapiLine line in mgr.Lines)
            {
                if (line.Capabilities.SupportsForwarding)
                {
                    foreach (TapiAddress addr in line.Addresses)
                    {
                        Console.WriteLine("Forwarding supported: {0} {1} - {2}", line.Name, addr.Address, addr.Capabilities.SupportedForwardingModes);
                        foreach (ForwardInfo fwd in addr.Status.ForwardingInformation)
                            Console.WriteLine("\t{0}", fwd);
                    }
                }
            }
        }
    }
}
