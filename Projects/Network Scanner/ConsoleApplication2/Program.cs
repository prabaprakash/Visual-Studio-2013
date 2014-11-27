using System;
using System.Collections.Generic;
using System.Linq;

namespace consoleApplication2
{
   
    //public class ArpTest
    //{
    //    public static void Main(string[] args)
    //    {
    //        // Print SharpPcap version
    //        string ver = SharpPcap.Version.VersionString;
    //        Console.WriteLine("SharpPcap {0}, Example2.ArpResolve.cs\n", ver);

    //        // Retrieve the device list
    //        var devices = LibPcapLiveDeviceList.Instance;

    //        // If no devices were found print an error
    //        if (devices.Count < 1)
    //        {
    //            Console.WriteLine("No devices were found on this machine");
    //            return;
    //        }

    //        Console.WriteLine("The following devices are available on this machine:");
    //        Console.WriteLine("----------------------------------------------------");
    //        Console.WriteLine();

    //        int i = 0;

    //        // Print out the available devices
    //        foreach (var dev in devices)
    //        {
    //            Console.WriteLine("{0}) {1} {2}", i, dev.Name, dev.Description);
    //            i++;
    //        }

    //        Console.WriteLine();
    //        Console.Write("-- Please choose a device for sending the ARP request: ");
    //        i = int.Parse(Console.ReadLine());

    //        var device = devices[i];

    //        System.Net.IPAddress ip;

    //        // loop until a valid ip address is parsed
    //        while (true)
    //        {
    //            Console.Write("-- Please enter IP address to be resolved by ARP: ");
    //            if (System.Net.IPAddress.TryParse(Console.ReadLine(), out ip))
    //                break;
    //            Console.WriteLine("Bad IP address format, please try again");
    //        }

    //        // Create a new ARP resolver
    //        ARP arper = new ARP(device);

    //        // print the resolved address or indicate that none was found
    //        var resolvedMacAddress = arper.Resolve(ip);
    //        if (resolvedMacAddress == null)
    //        {
    //            Console.WriteLine("Timeout, no mac address found for ip of " + ip);
    //        }
    //        else
    //        {
    //            Console.WriteLine(ip + " is at: " + arper.Resolve(ip));
    //        }
    //    }
    //}
    public  class FindPrefix 
    {
        public static void Main(String[] args)
        {
            List<internet> internets = new List<internet>()
            {
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            1,
                            9,
                            17,
                            25
                        },
                    Subnetmask = 128,
                    Incorder = 128
                },
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            2,
                            10,
                            18,
                            26
                        },
                    Subnetmask = 192,
                    Incorder = 64
                },
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            3,
                            11,
                            19,
                            27
                        },
                    Subnetmask = 224,
                    Incorder = 32
                },
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            4,
                            12,
                            20,
                            28
                        },
                    Subnetmask = 240,
                    Incorder = 16
                },
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            5,
                            13,
                            21,
                            29
                        },
                    Subnetmask = 248,
                    Incorder = 8
                },
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            6,
                            14,
                            22,
                            30
                        },
                    Subnetmask = 252,
                    Incorder = 4
                },
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            7,
                            15,
                            23,
                            31
                        },
                    Subnetmask = 254,
                    Incorder = 2
                },
                new internet()
                {
                    Octal =
                        new List<int>()
                        {
                            8,
                            16,
                            24,
                            32
                        },
                    Subnetmask = 255,
                    Incorder = 1
                },
            };
           
            while (true)
            {
                Console.WriteLine("Enter The Subnet Mask Address");
                Console.WriteLine("---------------------------------------------");
                String sub = Console.ReadLine();
                var s = sub.Split('.');
                //Console.WriteLine(s.Count());
                if (s.Count() != 4)

                {
                    Console.WriteLine("Wrong Address");
                    goto l1;
                }
                if (s[0] != "0" && s[1] == "0" && s[2] == "0" && s[3] == "0")
                {
                    Console.WriteLine("---------------------------------------------");
                   Console.WriteLine("Sunbet Mask -  Class A");
                    GetOctal(s, internets,0);
                }
                if (s[0] != "0" && s[1] != "0" && s[2] == "0" && s[3] == "0")
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Sunbet Mask - Class B");
                    GetOctal(s, internets, 1);
                }
                if (s[0] != "0" && s[1] != "0" && s[2] != "0" && s[3] == "0")
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Sunbet Mask - Class B");
                    GetOctal(s, internets, 2);
                }
                if (s[0] != "0" && s[1] != "0" && s[2] != "0" && s[3] != "0")
                {
                    Console.WriteLine("Sunbet Mask - Class B");
                    GetOctal(s, internets, 3);
                }
                if (s[0] == "0" && s[1] == "0" && s[2] == "0" && s[3] == "0")
                {
                    //GetOctal(s, internets, 0);
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Prefix : 0");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Total HostAddress  "+Math.Pow(2,32));
                    Console.WriteLine("---------------------------------------------");
                }
               
            l1:;
            }
        }

        private static void GetOctal(string[] s, List<internet> internets,int i)
        {

            int d = Convert.ToInt32(s[i]);
            var classa = from x in internets
                from y in x.Octal
                where x.Subnetmask == d
                select y;
          
            if (classa.Count() != 0)
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Prefix : "+classa.ToArray()[i]);
                Console.WriteLine("---------------------------------------------");
               Console.WriteLine("Total HostAddress "+Math.Pow(2,(32-classa.ToArray()[i])));
               Console.WriteLine("---------------------------------------------");
            }
            else
            {
                Console.WriteLine("Wrong Subnet Mask");
            }
        }
    }

   public  class internet
   {
     
        public List<int> Octal { get; set; }

        public int Subnetmask { get; set; }

        public int Incorder { get; set; }
    }

    
}
