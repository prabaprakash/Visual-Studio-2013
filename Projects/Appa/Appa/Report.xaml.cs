using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Storage;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Appa
{
    public partial class Report : PhoneApplicationPage
    {
        string AdminName = GlobalMethods.AdminName();
        public Report()
        {
            InitializeComponent();
            this.Loaded += (c, b) =>
            {
                string pivotIndex = "";
                
                if (NavigationContext.QueryString.TryGetValue("id", out pivotIndex))
                {
                    //-1 because the Pivot is 0-indexed, so pivot item 2 has an index of 1
                    ReportPage.SelectedIndex = Convert.ToInt32(pivotIndex) - 1;
                    
                }
            };
        }
        List<ShopLists> OfferShopList=new List<ShopLists>(); 
        List<ShopLists> RedemptionShopList=new List<ShopLists>(); 
        


        private void OfferLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shopping.db3");

                using (var db = new SQLite.SQLiteConnection(path))
                {
                    var a = from x in db.Table<ShopLists>() where x.AdminName == AdminName && x.Condition == 0 select x;
                    //MessageBox.Show(a.Count().ToString());

                    foreach (var v in a)
                    {
                        OfferShopList.Add(v);
                    }

                }

                OfferList.ItemsSource = OfferShopList;
            }
            catch 
            {
                
              
            }
        }

        private void RemptionLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shopping.db3");

                using (var db = new SQLite.SQLiteConnection(path))
                {
                    var a = from x in db.Table<ShopLists>() where x.AdminName == AdminName && x.Condition == 1 select x;
                    //MessageBox.Show(a.Count().ToString());

                    foreach (var v in a)
                    {
                        RedemptionShopList.Add(v);
                    }

                }

                RedemptionList.ItemsSource = RedemptionShopList;
            }
            catch
            {

            }
        }

        private void Logout(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Logout Now?", "Logout", MessageBoxButton.OKCancel);
            // textbox2.Text = MessageBoxResult.Cancel.ToString();
            if (result == MessageBoxResult.Cancel)
            {
              
            }
            else
            {
                GlobalMethods.LocalSetting("");
                Dispatcher.BeginInvoke(() => App.RootFrame.Navigate(new Uri("/Login.xaml", UriKind.Relative)));
            }
        }

      

        private void AboutPage(object sender, EventArgs e)
        {
          
        }


        private void OfferList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var obj = OfferList.SelectedItem as ShopLists;

            MessageBoxResult result = MessageBox.Show("Resend to "+obj.Name, "Resend Coupon Again", MessageBoxButton.OKCancel);
            // textbox2.Text = MessageBoxResult.Cancel.ToString();
            if (result == MessageBoxResult.Cancel)
            {
               
            }
            else
            {
                SmsComposeTask smsComposeTask = new SmsComposeTask();
                smsComposeTask.To = obj.MobileNo;
                smsComposeTask.Body = "Coupon Code " +obj.CouponId+ ",Offer " + obj.Offer + "%.Valid Till " +
                                      obj.ExpiryDateTime;
                smsComposeTask.Show();
            }
        }
    }
}