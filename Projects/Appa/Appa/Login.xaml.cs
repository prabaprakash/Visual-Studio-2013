using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Navigation;
using Windows.Storage;
using Appa.ServiceReference1;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using SQLite;

namespace Appa
{
    public partial class Login : PhoneApplicationPage
    {

        public Login()
        {
            InitializeComponent();
            Login_Loaded();
        }

        async void Login_Loaded()
        {
            try
            {
                await ApplicationData.Current.LocalFolder.CreateFileAsync("Shopping.db3");
                var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shopping.db3");
                using (var db = new SQLite.SQLiteConnection(path))
                {
                    // Create the tables if they don't exist
                    db.CreateTable<ShopLists>();
                    db.CreateTable<ShopAdmins>();
                    db.Commit();
                    db.Dispose();
                    db.Close();
                }
                ServiceReference1.SoapServiceSoapClient sc=new SoapServiceSoapClient();
                sc.AllCouponsCompleted += (a, b) => MessageBox.Show(b.Result);
                sc.AllCouponsAsync("hacking");
              
            }
            catch (Exception ex)
            {
                FaultHandling(ex.StackTrace);
            }
        }

        public void FaultHandling(String fault)
        {
            MessageBox.Show(fault);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IUsername.Text != "" && IPassword.Password != "")
                {
                    var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shopping.db3");

                    using (var db = new SQLite.SQLiteConnection(path))
                    {
                        var a = from x in db.Table<ShopAdmins>()
                            where x.AdminName == IUsername.Text && x.Password == IPassword.Password
                            select x;
                        if (a.Count() == 1)
                        {
                            GlobalMethods.LocalSetting(IUsername.Text);
                            Dispatcher.BeginInvoke(
                                () => App.RootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)));
                        }
                        else
                        {
                            MessageBox.Show("Login Failed!!!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please fill the Username and Password");
                }

            }
            catch
            {


            }

        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SConfirmPassword.Password != "" && SPassword.Password != "" && SUsername.Text != "")
                {
                    if (SConfirmPassword.Password == SPassword.Password)
                    {
                        var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Shopping.db3");
                        using (var db = new SQLiteConnection(path))
                        {
                            var a =
                                (from x in db.Table<ShopAdmins>() where x.AdminName == SUsername.Text select x).Count();
                            if (a == 1)
                            {
                                MessageBox.Show("Username already used,enter different username");
                            }
                            else
                            {
                                db.Insert(new ShopAdmins() {AdminName = SUsername.Text, Password = SPassword.Password});
                                db.Commit();
                                MessageBox.Show("Account Created!!!Sign in Now...");
                                MainPivot.SelectedIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password and confirm password not matched ! re-enter again");
                        SConfirmPassword.Password = "";
                        SPassword.Password = "";

                    }
                }
                else
                {
                    MessageBox.Show("Please fill all fields");
                }


            }
            catch
            {
            }

        }

        private void Login_OnBackKeyPress(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if (MainPivot.SelectedIndex == 1)
            {
                MainPivot.SelectedIndex = 0;
            }
            else if(MainPivot.SelectedIndex==0)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    var result = MessageBox.Show
                        ("Are you sure you want to exit?", "Confirm exit",
                            MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        OnNavigatedFrom(new NavigationEventArgs(null, new Uri("app://external/"),
                            NavigationMode.Back, false));
                        var app = App.Current as App;
                        app.Application_Closing(PhoneApplicationService.Current, new ClosingEventArgs());
                        app.Terminate();
                    }
                });
            }
           
        }
    }

}