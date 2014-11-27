//Prabakaran.A 
//MS Software Engineering (2011-2016) 
//VIT University - Chennai Campus 
//Tamilnadu State 
//India - 600 048 
//www.facebook.com/prabakaran1993 
using System;
using System.Collections.Generic;
using System.Linq;
using IPlayer.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace IPlayer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
           
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                     // await SuspensionManager.RestoreAsync();
                    //var dispRequest = new DisplayRequest();
                    //dispRequest.RequestActive();
                    
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(Views.MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
          //  await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        protected override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
            //// TODO: Register the Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().QuerySubmitted
            //// event in OnWindowCreated to speed up searches once the application is already running

            //// If the Window isn't already using Frame navigation, insert our own Frame
            //var previousContent = Window.Current.Content;
            //var frame = previousContent as Frame;

            //// If the app does not contain a top-level frame, it is possible that this 
            //// is the initial launch of the app. Typically this method and OnLaunched 
            //// in App.xaml.cs can call a common method.
            //if (frame == null)
            //{
            //    // Create a Frame to act as the navigation context and associate it with
            //    // a SuspensionManager key
            //    frame = new Frame();
            //    IPlayer.Common.SuspensionManager.RegisterFrame(frame, "AppFrame");

            //    if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            //    {
            //        // Restore the saved session state only when appropriate
            //        try
            //        {
            //            await IPlayer.Common.SuspensionManager.RestoreAsync();
            //        }
            //        catch (IPlayer.Common.SuspensionManagerException)
            //        {
            //            //Something went wrong restoring state.
            //            //Assume there is no state and continue
            //        }
            //    }
            //}

            //frame.Navigate(typeof(MainPage), args);
            //Window.Current.Content = frame;

            //// Ensure the current window is active
            //Window.Current.Activate();

            Frame f = (Frame) Window.Current.Content;
            if (f == null)
            {


                Frame frame = new Frame();
                frame.Navigate(typeof (Views.MainPage), args);
                Window.Current.Content = frame;

                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        public int count = 0;
        protected override async void OnFileActivated(FileActivatedEventArgs args)
        {


            
           
                //if (count == 0)
                //{


                //    var rootFrame = new Frame();
                //    rootFrame.Navigate(typeof (MainPage));
                //    Window.Current.Content = rootFrame;
                //    MainPage p = rootFrame.Content as MainPage;
                //    Window.Current.Activate();


                //    StorageFile file = args.Files[0] as StorageFile;
                //    p.fileactivatedfile = file;
                //    count = count + 1;
                //}
                //else
                //{
                    
                //    //MainPage a=new MainPage();
                //    //StorageFile file = args.Files[0] as StorageFile;
                //    //a.SetSource(file);
                //    //Window.Current.Content = a;
                    
                //    //  await   new MessageDialog("Open Media via App").ShowAsync();
                //}

          
          
       //Method 2
            //////var rootFrame = new Frame();
            //////rootFrame.Navigate(typeof(MainPage));
            //////Window.Current.Content = rootFrame;


            //////MainPage p = rootFrame.Content as MainPage;
            //////Window.Current.Activate();

            //////await Windows.UI.Xaml.Window.Current.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
            //////{
            //////    StorageFile file = args.Files[0] as StorageFile;
            //////    p.SetSource(file);
            //////}));
                                                                                  
          //method 3
            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;

            // If the app does not contain a top-level frame, it is possible that this 
            // is the initial launch of the app. Typically this method and OnLaunched 
            // in App.xaml.cs can call a common method.
            if (frame == null)
            {
                // Create a Frame to act as the navigation context and associate it with
                // a SuspensionManager key
                frame = new Frame();
                IPlayer.Common.SuspensionManager.RegisterFrame(frame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await IPlayer.Common.SuspensionManager.RestoreAsync();
                    }
                    catch (IPlayer.Common.SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }
            }

            frame.Navigate(typeof(Views.MainPage), args);
            Views.MainPage p = frame.Content as Views.MainPage;

            //var QueryOptions = new QueryOptions
            //          (
            //          CommonFileQuery.OrderByName, new List<string> { ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".m4v", ".mp4v", ".mp4", ".mov", ".m2ts", ".asf", ".wm", ".vob", ".wmv", ".wma", ".aac", ".adt", ".mp3", ".wav", ".avi", ".ac3", ".ec3" }
            //          );
            //var FileQuery = folder.CreateFileQueryWithOptions(QueryOptions);
            //var ass = await FileQuery.GetFilesAsync();

            var list = new List<string>
                {
                    ".3g2",
                    ".3gp2",
                    ".3gp",
                    ".3gpp",
                    ".m4a",
                    ".m4v",
                    ".mp4v",
                    ".mp4",
                    ".mov",
                    ".m2ts",
                    ".asf",
                    ".wm",
                    ".vob",
                    ".wmv",
                    ".wma",
                    ".aac",
                    ".adt",
                    ".mp3",
                    ".wav",
                    ".avi",
                    ".ac3",
                    ".ec3"
                };
            List<StorageFile> strlis=new List<StorageFile>(); 


            //foreach (StorageFile fi in args.Files)
            //{
            //    if (list.Any(x=>x.Contains(fi.FileType.ToLower()))==true)
            //    {
            //        strlis.Add(fi);
            //    }
               
            //}

         // StorageFile file = args.Files[0] as StorageFile;

            if (args.Verb.Equals("play"))
            {
                ////var QueryOptions = new QueryOptions
                ////     (
                ////     CommonFileQuery.OrderByName, new List<string> { ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".m4v", ".mp4v", ".mp4", ".mov", ".m2ts", ".asf", ".wm", ".vob", ".wmv", ".wma", ".aac", ".adt", ".mp3", ".wav", ".avi", ".ac3", ".ec3" }
                ////     );
                ////var FileQuery = ((StorageFolder)args.Files[0]).CreateFileQueryWithOptions(QueryOptions);
                ////var ass = await FileQuery.GetFilesAsync();
                ////foreach (var storage in ass)
                ////{
                ////    strlis.Add(storage);
                ////}
             // await  new MessageDialog("hi").ShowAsync();


              IReadOnlyList<IStorageItem> files = args.Files as IReadOnlyList<IStorageItem>;
                foreach (var storageItem in files)
                {
                    strlis.Add(storageItem as StorageFile);
                }
            }
            else
            {
                foreach (var fi in args.Files)
                {
                    if (fi is StorageFile)
                    {
                        if (list.Any(x => x.Contains((fi as StorageFile).FileType.ToLower())))
                        {

                            strlis.Add(fi as StorageFile);

                        }
                    }
                }
                p.file1 = strlis;
            }
            Window.Current.Content = frame;

            // Ensure the current window is active
            Window.Current.Activate();
        }

      
      
    }
}
//Prabakaran.A 
//MS Software Engineering (2011-2016) 
//VIT University - Chennai Campus 
//Tamilnadu State 
//India - 600 048 
//www.facebook.com/prabakaran1993 