using System;
using JulMar.Atapi;

namespace EnumTapiLines
{
    class Program
    {
        static void Main(string[] args)
        {
            using(TapiManager mgr = new TapiManager("EnumTapiLines"))
            {
                try
                {
                    if (!mgr.Initialize())
                        Console.WriteLine("TAPI failed to find any lines or phones to manage.");

                    if (mgr.Lines.Length > 0)
                    {
                        int count = 1;
                        foreach (TapiLine line in mgr.Lines)
                        {
                            Console.WriteLine("{0}: \"{1}\" ppid={2}\n{3}\n--Status--\n{4}",
                                              count++, line.Name, line.PermanentId,
                                              line.Capabilities.ToString("f"),
                                              line.Status.ToString("f"));
                        }
                    }
                    else
                        Console.WriteLine("No Tapi lines found.");

                    if (mgr.Phones.Length > 0)
                    {
                        int count = 1;
                        foreach (TapiPhone phone in mgr.Phones)
                        {
                            Console.WriteLine("{0}: \"{1}\" ppid={2}\n{3}\n--Status--\n{4}",
                                              count++, phone.Name, phone.PermanentId,
                                              phone.Capabilities.ToString("f"),
                                              phone.Status.ToString("f"));
                        }
                    }
                    else
                        Console.WriteLine("No Tapi phones found.");
                }
                catch (TapiException ex)
                {
                    Console.WriteLine(ex);
                }
                
            }
        }
    }
}
