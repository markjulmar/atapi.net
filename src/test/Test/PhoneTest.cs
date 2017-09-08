using System;
using System.Collections.Generic;
using System.Text;
using JulMar.Atapi;

namespace EnumDevices
{
    class PhoneTest
    {
        static void MainPhone(string[] args)
        {
            using (TapiManager mgr = new TapiManager("Test"))
            {
                mgr.Initialize();

                foreach (TapiPhone phone in mgr.Phones)
                {
                    Console.WriteLine(phone.Name);
                    Console.WriteLine(phone.Capabilities.ToString("f"));

                    phone.Open();
                    if (phone.Display != null)
                    {
                        Console.WriteLine(phone.Display.Text);
                    }

                    Console.WriteLine(phone.Status);

                    foreach (PhoneButton pb in phone.Buttons)
                    {
                        Console.WriteLine(pb);
                    }

                    if (phone.Handset != null)
                    {
                        Console.WriteLine(phone.Handset);
                        phone.Handset.Volume = 100;
                        phone.Handset.Gain = 20;
                    }
                    if (phone.Headset != null)
                        Console.WriteLine(phone.Headset);
                    if (phone.Speaker != null)
                        Console.WriteLine(phone.Speaker);

                    phone.Close();
                }
            }
        }
    }
}
