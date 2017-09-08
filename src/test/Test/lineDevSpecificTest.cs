using System;
using System.Collections.Generic;
using System.Text;
using JulMar.Atapi;

namespace EnumDevices
{
    class lineDevSpecificTest
    {
        static void MainDS(string[] args)
        {
            TapiManager mgr = new TapiManager("devspec", TapiVersion.V30);
            mgr.Initialize();

            foreach (TapiLine line in mgr.Lines)
            {
                line.Monitor();

                byte[] arr = new byte[]
                {
                    08,
                    00,
                    80,
                    00
                };

                line.Addresses[0].DeviceSpecific(arr);

            }
        }
    }
}
