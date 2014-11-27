using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page,IFileOpenPickerContinuable
    {
        public static MainPage Current;       
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            Current = this;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.List;
                openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                openPicker.FileTypeFilter.Add(".mp3");

                openPicker.PickSingleFileAndContinue();

                // Launch file open picker and caller app is suspended and may be terminated if required
                //  openPicker.PickSingleFileAndContinue();


                //if (a!=null)
                //{
                //    foreach (var VARIABLE in a)
                //    {
                //        Player.SetSource((await VARIABLE.OpenAsync(FileAccessMode.Read)), "");
                //    }

                //}
                //else
                //{
                //    new MessageDialog("fdfd").ShowAsync();
                //}
            }
            catch (Exception ee)
            {
                new MessageDialog(ee.Message + ee.StackTrace).ShowAsync();
            }

        }
        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {

            Button1.Content = "fff";
            IReadOnlyList<StorageFile> files = args.Files;
            if (files.Count > 0)
            {
                StringBuilder output = new StringBuilder("Picked files:\n");
                // Application now has read/write access to the picked file(s)
                foreach (StorageFile file in files)
                {
                    Player.SetSource((await file.OpenAsync(FileAccessMode.Read)), "");
                }


            }
            else
            {

            }
        }

      
    
    }
}
