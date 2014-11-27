using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Network_Scanner
{
  
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();
        }
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(
            int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        public List<NetProject> LiveHostList=new List<NetProject>();
        private async void Processing(object sender, RoutedEventArgs e)
        {
            
            for (int j = 0; j <100; j++)
            {
                ThreadPool.QueueUserWorkItem(DoFast, j);
            }
           
        }

        private async void DoFast(object state)
        {
            await Task.Run(() =>
            {
                var j = (int) state;
                var dst = IPAddress.Parse("172.16.16." + j);
                int intAddress = BitConverter.ToInt32(dst.GetAddressBytes(), 0);

                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;
                var s = "";

                if (SendARP(intAddress, 0, macAddr, ref macAddrLen) != 0)
                {
                    // throw new InvalidOperationException("SendARP failed.");
                }
                else
                {
                    string[] str = new string[(int)macAddrLen];

                    s += macAddr[0].ToString("x2");
                    for (int i = 1; i < macAddrLen; i++)
                    {
                        str[i] = macAddr[i].ToString("x2");
                        s += "-" + str[i];
                    }
                    Application.Current.Dispatcher.Invoke(
                        (Action)
                            (() =>
                                ListView1.Items.Add(new NetProject() {_ipaddress = "172.16.16." + j, _macAddress = s})));

                    // LiveHostList.Add();
                }

            });          
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public class NetProject
        {
            public String _ipaddress;

            public String IpAddress
            {
                get
                {
                    return _ipaddress;
                }
                set
                {
                    _ipaddress = value;
                }
            }

            public String MacAddress
            {
                get { return _macAddress; }
                set { _macAddress = value; }
            }

            public String _macAddress;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           //  ListView1.ItemsSource = LiveHostList;
        }
     
    }
}
