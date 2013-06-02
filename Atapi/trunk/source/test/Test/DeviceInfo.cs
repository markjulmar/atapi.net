using System;
using System.Data;
using System.Configuration;
using JulMar.Atapi;

namespace EnumDevices
{
    public class DeviceInfo
    {
        public static void MainDI(string[] args)
        {
            TapiManager mgr = new TapiManager("devspec", TapiVersion.V30);
            mgr.Initialize();

            foreach (TapiLine line in mgr.Lines)
            {
                try
                {
                    line.Open(MediaModes.InteractiveVoice);
                    foreach (TapiCall call in line.GetCalls())
                    {
                        Console.WriteLine( call.CalledId.ToString() );
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            mgr.Shutdown();
        }
    }
}
