using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using SharpPcap;
using PacketDotNet;
//static class Module1
//{


//    private static List<Ping> pingers = new List<Ping>();
//    private static int instances = 0;

//    private static object @lock = new object();

//    private static int result = 0;
//    private static int timeOut = 250;

//    private static int ttl = 5;

//    public static void Main()
//    {
//        string baseIP = "172.16.16.";
//        int classD = 1;

//        Console.WriteLine("Pinging 255 destinations of D-class in {0}*", baseIP);

//        CreatePingers(255);

//        PingOptions po = new PingOptions(ttl, true);
//        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
//        byte[] data = enc.GetBytes("abababababababababababababababab");

//        SpinWait wait = new SpinWait();
//        int cnt = 1;

//        Stopwatch watch = Stopwatch.StartNew();

//        foreach (Ping p in pingers)
//        {
//            lock (@lock)
//            {
//                instances += 1;
//            }

//            p.SendAsync(string.Concat(baseIP, cnt.ToString()), timeOut, data, po);
//            cnt += 1;
//        }

//        while (instances > 0)
//        {
//            wait.SpinOnce();
//        }

//        watch.Stop();

//        DestroyPingers();

//        Console.WriteLine("Finished in {0}. Found {1} active IP-addresses.", watch.Elapsed.ToString(), result);
//        Console.ReadKey();

//    }


//    public static void Ping_completed(object s, PingCompletedEventArgs e)
//    {
//        lock (@lock)
//        {
//            instances -= 1;
//        }

//        if (e.Reply.Status == IPStatus.Success)
//        {
//            Console.WriteLine(string.Concat("Active IP: ", e.Reply.Address.ToString()));
//            result += 1;
//        }
//        else
//        {
//            //Console.WriteLine(String.Concat("Non-active IP: ", e.Reply.Address.ToString()))
//        }

//    }


//    private static void CreatePingers(int cnt)
//    {
//        for (int i = 1; i <= cnt; i++)
//        {
//            Ping p = new Ping();
//            p.PingCompleted += Ping_completed;
//            pingers.Add(p);
//        }
//    }

//    private static void DestroyPingers()
//    {
//        foreach (Ping p in pingers)
//        {
//            p.PingCompleted -= Ping_completed;
//            p.Dispose();
//        }

//        pingers.Clear();

//    }

   
//    public class trait
//    {
      
      
//        public void hello1()
//        {
            
//        }
//        public void hello2() { }
//        public void hello3() { }
//        public void hello4() { }
//    }

//    public  class MyClass
//    {
//        public  trait trait;
//    }
   

//    public static void ff()
//    {
//        MyClass f=new MyClass();
//        f.trait.hello1();
//        f.trait.hello2();
//        f.trait.hello3();
//        f.trait.hello4();
//    }
//}
using SharpPcap;
using  PacketDotNet;
using SharpPcap.AirPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;

static class praba
{
    public static void Main(String[] args)
    {
      try
      {
          String i = "255.255.0.0";
          string j = "255.255.255.255";
          double k = Convert.ToInt32("255", 2);

      }
        catch(Exception exception)
        {
            Console.WriteLine(exception.StackTrace);
           
        }
        Console.Read();
    }

    public static void C2B(int b)
    {
       

        int[] c = new int[10];
        int a = 0;
        int i = 0;
        while (b > 0)
        {
            c[i] = b % 2;
            a = b / 2;
            b = a;
            i++;
        }
        
        int g = 0;
        for (int j = i - 1; j >= 0; j--)
        {
            //System.out.println(c[j]);
            if (c[j] == 1)
            {
                g++;
            }
            else
            {
                
            }
           
        }
       
    }
}