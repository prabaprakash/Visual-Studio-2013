using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using PacketDotNet;

namespace Network_Scanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
           
           // Timer a = new Timer();
//
            //Ping pingSender = new Ping();
            ////for(int i=0;i<30;i++)
            ////{
            //  //  MessageBox.Show(adr);
               String adr ="172.16.16.10";
                IPAddress address = IPAddress.Parse(adr);
            //  //  pingSender.SendAsync(address,250,new PingOptions(5,true));
            // pingSender.SendAsync(address, 250);
           
            //pingSender.PingCompleted += (a, b) =>
            //{
            //    if (b.Reply.Status == IPStatus.Success)
            //    {
            //        MessageBox.Show(b.ToString());
            //    }
            //    else
            //    {
            //        MessageBox.Show("failed");
            //    }

            //};
            //PingReply reply = await pingSender.SendPingAsync(address);

            //if (reply.Status == IPStatus.Success)
            //{
            //    ListBox1.Items.Add(adr);
            //}
            //else
            //{
            //    ListBox2.Items.Add("Failed "+adr); // MessageBox.Show("falied" + adr);
            //}
            //}  
            AddressFamily d=new AddressFamily();

            try
            {
                Ping s=new Ping();
                var a= s.SendPingAsync(address).GetAwaiter();
               a.OnCompleted(() => MessageBox.Show(a.GetResult().Status.ToString()));
               //UdpClient udp=new UdpClient(8080);
               // udp.Connect(address,8080);
               // var a=new byte[]{1,2};
               // await udp.SendAsync(a,44);
               // TcpClient tcp = new TcpClient();
                //await tcp.ConnectAsync(adr, 8080);
               // MessageBox.Show(adr);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

      

        public List<string> list
        {
           get {return  new List<string>()
           {
               "172.16.16.1",
               "172.16.20.1",
               "172.16.24.1",
               "172.16.28.1",
               "172.16.16.5",
           };}
        } 
        private  void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(Box.Text);
            bool outcome = false;
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;
            options.Ttl = 50;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            try
            {
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    MessageBox.Show(reply.Status.ToString());
                }
                else
                {
                    MessageBox.Show("failed");
                }
            }
            catch{}
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IPAddress dst = IPAddress.Parse("172.16.16.64");
            int intAddress = BitConverter.ToInt32(dst.GetAddressBytes(), 0);

            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint) macAddr.Length;
            if (SendARP(intAddress, 0, macAddr, ref macAddrLen) != 0)
                throw new InvalidOperationException("SendARP failed.");

            string[] str = new string[(int) macAddrLen];
            var s = "";
            for (int i = 0; i < macAddrLen; i++)
            {
                str[i] = macAddr[i].ToString("x2");
                s = str[i];
                ListBox1.Items.Add(s);
            }
            

            // dad();
            //Ping ping = new Ping();
            //ping.SendPingAsync(Box.Text, 250, new byte[] { 1 }, new PingOptions() { Ttl = 50 });
            //PingOptions pingOptions = new PingOptions();
            //pingOptions.Ttl = 50;
            //pingOptions.DontFragment = false;
            //ping.PingCompleted += (a, b) => MessageBox.Show(b.Reply.Status.ToString());
        }
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(
            int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);
        public void dad()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint IPep;

            byte[] data = Encoding.ASCII.GetBytes("This is a string of 32 bytes!!!!");



            string subnet = "172.16.16.";

            for (int x = 1; x <= 255; x++)
            {
                try
                {
                    IPep = new IPEndPoint(IPAddress.Parse(subnet + x.ToString()), 1000);

                    sock.SendTo(data, data.Length, SocketFlags.None, IPep);

                    IPep = new IPEndPoint(IPAddress.Parse(subnet + x.ToString()), 445);

                    sock.SendTo(data, data.Length, SocketFlags.None, IPep);

                    IPep = new IPEndPoint(IPAddress.Parse(subnet + x.ToString()), 80);

                    sock.SendTo(data, data.Length, SocketFlags.None, IPep);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
              
            }

            sock.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        //    IPAddress ip = IPAddress.Parse("172.16.16.2");
        ////    PhysicalAddress ss=PhysicalAddress.Parse("172.16.16.2");
        ////   CtsFrame a=new CtsFrame(ss);
        ////    Byte[] f = new byte[]{};
        //    AddressFamily ad=new AddressFamily();
        //    ad
        //    Socket soc = new Socket();
         
          
        }
    }
}
