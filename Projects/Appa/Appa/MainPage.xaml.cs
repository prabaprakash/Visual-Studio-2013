using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using Windows.Storage;
using Microsoft.Phone.Controls;
using System.IO;
using Microsoft.Phone.Tasks;
namespace Appa
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        String AdminName = GlobalMethods.AdminName();
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
        }



        private string Id()
        {


            // get 1st random string 
            string Rand1 = RandomString(4);

            // get 2nd random string 
            string Rand2 = RandomString(4);

            // creat full rand string
            string docNum = Rand1 + "-" + Rand2;
            return docNum;
        }
        private string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String Coupon_Id = Id();
            //await ApplicationData.Current.LocalFolder.CreateFileAsync("Shopping.db3");
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shopping.db3");
            if (Name.Text != "" && Mobile_No.Text != "")
            {
                using (var db = new SQLite.SQLiteConnection(path))
                {

                    int offer = Coupon_List.SelectedIndex;
                    int coupon = 0;
                    switch (offer)
                    {
                        case 0:
                            coupon = 5;
                            break;
                        case 1:
                            coupon = 10;
                            break;
                        case 2:
                            coupon = 25;
                            break;
                        case 3:
                            coupon = 40;
                            break;
                        case 4:
                            coupon = 50;
                            break;

                    }

                    db.Insert(new ShopLists() { Name = Name.Text, Condition = 0, AdminName = AdminName, ExpiryDateTime = Date_Picker.Value.Value, CouponId = Coupon_Id, MobileNo = Mobile_No.Text, Offer = coupon, AddedDateTime = DateTime.Now });


                    SmsComposeTask smsComposeTask = new SmsComposeTask();
                    smsComposeTask.To = Mobile_No.Text;
                    smsComposeTask.Body = "Coupon Code " + Coupon_Id + ",Offer " + coupon + "%.Valid Till " +
                                          Date_Picker.Value.Value;
                    smsComposeTask.Show();
                    db.Commit();
                    Mobile_No.Text = Name.Text = "";
                }


                //string b="";
                //var a = from x in db.Table<Shop>() select x;
                //foreach (var s in a)
                //{
                //    b = b + s.Condition + s.DateTime + s.Id + s.MobileNo + s.Name + s.Offer;
                //}
                //MessageBox.Show(b);
            }
            else
            {
                MessageBox.Show("Fill all the fields");
            }

        }

        private void Copuon_Code_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SelectReports.IsChecked == true)
            {
                Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/Report.xaml?id=1", UriKind.Relative)));
            }
            else
            {
                Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/Report.xaml?id=2", UriKind.Relative)));
            }
        }

        private void CouponCodeValidations(object sender, RoutedEventArgs e)
        {
            try
            {

                // MessageBox.Show(GlobalMethods.AdminName());
                var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shopping.db3");
                using (var db = new SQLite.SQLiteConnection(path))
                {
                    var a = from x in db.Table<ShopLists>() where x.CouponId == CopuonCode.Text && x.Condition == 0 && x.AdminName == AdminName select x;
                    //MessageBox.Show(a.Count().ToString());
                    if (a.Count() == 1)
                    {
                        foreach (var VARIABLE in a)
                        {
                            CouponCheck.Text = "Coupon Valid\n" + VARIABLE.Offer + "% Offer,Valid Till " + VARIABLE.ExpiryDateTime + "\n" +
                                               VARIABLE.CouponId + "\n" + VARIABLE.Name + "\n" + VARIABLE.MobileNo;
                            db.Update(new ShopLists()
                            {
                                Condition = 1,
                                ExpiryDateTime = VARIABLE.ExpiryDateTime,
                                AdminName = VARIABLE.AdminName,
                                CouponId = VARIABLE.CouponId,
                                MobileNo = VARIABLE.MobileNo,
                                Name = VARIABLE.Name,
                                Offer = VARIABLE.Offer,
                                AddedDateTime = DateTime.Now

                            });
                            db.Commit();
                        }
                    }
                    else
                    {
                        var b = from x in db.Table<ShopLists>() where x.CouponId == CopuonCode.Text && x.Condition == 1 && x.AdminName == AdminName select x;
                        if (b.Count() == 1)
                        {
                            foreach (var VARIABLE in b)
                            {
                                CouponCheck.Text = "Sorry,Coupon Already Used on " + VARIABLE.AddedDateTime.ToString() +
                                                    ".\n" + VARIABLE.Offer + "% Offer,Valid Till " +
                                                    VARIABLE.ExpiryDateTime + "\n" +
                                                    VARIABLE.CouponId + "\n" + VARIABLE.Name + "\n" + VARIABLE.MobileNo;
                            }
                        }
                        else
                        {
                            CouponCheck.Text = "Invalid Coupon";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private void MainPage_OnBackKeyPress(object sender, CancelEventArgs e)
        {

            base.OnBackKeyPress(e);
            if (MainPivot.SelectedIndex == 1)
            {
                e.Cancel = true;
                MainPivot.SelectedIndex = 0;
            }
            else if (MainPivot.SelectedIndex == 2)
            {
                e.Cancel = true;
                MainPivot.SelectedIndex = 1;
            }
            else if (MainPivot.SelectedIndex == 0)
            {
                MessageBoxResult result = MessageBox.Show("Logout Now?", "Logout", MessageBoxButton.OKCancel);
                // textbox2.Text = MessageBoxResult.Cancel.ToString();
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    MainPivot.SelectedIndex = 0;
                }
                else
                {

                    GlobalMethods.LocalSetting("");
                }
            }

        }

        private void AboutPage(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void Report(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(() => App.RootFrame.Navigate(new Uri("/Report.xaml", UriKind.Relative)));
        }

        private void Logout(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Logout Now?", "Logout", MessageBoxButton.OKCancel);
            // textbox2.Text = MessageBoxResult.Cancel.ToString();
            if (result == MessageBoxResult.Cancel)
            {
                MainPivot.SelectedIndex = 0;
            }
            else
            {
                GlobalMethods.LocalSetting("");
                Dispatcher.BeginInvoke(() => App.RootFrame.Navigate(new Uri("/Login.xaml", UriKind.Relative)));
            }
        }

        //private void Htprequest(object sender, RoutedEventArgs e)
        //{
        //    var uri = new Uri("http://localhost:4621/api/messages");
        //    HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
        //    webRequest.Method = "GET";
        //    webRequest.Accept = "application/json";
        //    webRequest.BeginGetResponse(httpGetTextCallback, webRequest);
        //}
        //private async void httpGetTextCallback(IAsyncResult result)
        //{
        //    HttpWebRequest request = (HttpWebRequest)result.AsyncState;
        //    try
        //    {
        //        WebResponse response = request.EndGetResponse(result);
        //        Stream responseStream = response.GetResponseStream();
        //        StreamReader reader = new StreamReader(responseStream);
        //        var s = await reader.ReadToEndAsync();
        //        Dispatcher.BeginInvoke(() => { });
        //    }
        //    catch (WebException ex)
        //    {
        //        Dispatcher.BeginInvoke(() => { });
        //    }
        //}
    }
}