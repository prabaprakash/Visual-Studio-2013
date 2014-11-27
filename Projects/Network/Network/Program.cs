using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class Program
    {
        static void Main(string[] args)
        {

            int ch;
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create("http://www.google.com");
            HttpWebResponse resp = (HttpWebResponse) req.GetResponse();
            Stream isp = resp.GetResponseStream();
            for (int i = 0;; i++)
            {
                ch = isp.ReadByte();
                if(ch==-1)break;
                Console.Write((char)ch);
                //if ((i%400) == 0)
                //{
                //    Console.WriteLine("Press Enter");
                //    Console.ReadLine();
                //}
               
            }
            Console.ReadLine();
        }
    }
}
