﻿//social.msdn.microsoft.com/Profile/prabaprakash
//about.me/prabakaran.a
//Prabakaran.A 
//MS Software Engineering (2011-2016) 
//VIT University - Chennai Campus 
//Tamilnadu State 
//India - 600 048 
//www.facebook.com/prabakaran1993 
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.System;
using Callisto.Controls;
using Microsoft.PlayerFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
//using SQLite;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Search;
using Windows.Data.Xml.Dom;
using Windows.Devices.Enumeration;
using Windows.Devices.Portable;
using Windows.Foundation;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace IPlayer.Views
{
    using IPlayer.Models;
    using IPlayer.Contracts;
    using IPlayer.Helpers;
    public sealed partial class MainPage : Common.LayoutAwarePage
    {

        public IReadOnlyList<StorageFile> file;
        public IReadOnlyList<StorageFile> file1;
        public SearchPane SearchPane;
        public String extensions;
        public Storyboard storyboard;
        public double mxheight, mxwidth;
        public ObservableCollection<playlist> Collections = new ObservableCollection<playlist>();

        DispatcherTimer timer6 = new DispatcherTimer();
        #region OnLoad

        bool control = true;
        public MainPage()
        {
            this.InitializeComponent();
            CheckBackGroundStatus();
            ls1.IsActiveView = false;
            Gridview1.IsActiveView = false;
            Gridview1.IsActiveView = false;
            Window.Current.CoreWindow.KeyDown += (a, b) =>
            {

                //if (b.VirtualKey == VirtualKey.Control)
                //{
                //    control = true;
                //}
                if (control)
                {


                    switch (b.VirtualKey)
                    {

                        case VirtualKey.Left:
                            recentvideo_Click(a, new RoutedEventArgs());
                            //control = false;
                            break;
                        case VirtualKey.Right:
                            nextvideo_Click(a, new RoutedEventArgs());
                            //control = false;
                            break;
                        case VirtualKey.Up:
                            if (mediaElement.Volume < 1)
                            {
                                mediaElement.Volume = mediaElement.Volume + 0.1;
                            }
                            slider.Visibility = Visibility.Visible;
                            timer6.Stop();
                            timer6.Interval = new TimeSpan(0, 0, 0, 2);
                            timer6.Start();
                            timer6.Tick += timer6_Tick;
                            //control = false;
                            break;
                        case VirtualKey.Down:
                            if (mediaElement.Volume > 0)
                            {
                                mediaElement.Volume = mediaElement.Volume - 0.1;
                            }
                            slider.Visibility = Visibility.Visible;
                            timer6.Stop();
                            timer6.Interval = new TimeSpan(0, 0, 0, 2);
                            timer6.Start();
                            timer6.Tick += timer6_Tick;
                            //control = false;
                            break;
                        case VirtualKey.O:
                            button1_Click(a, new RoutedEventArgs());
                            //control = false;
                            break;
                        case VirtualKey.F:
                            openfolderasync(a, new RoutedEventArgs());
                            //control = false;
                            break;
                        case VirtualKey.L:
                            bt2_Click(a, new RoutedEventArgs());
                            //control = false;
                            break;
                        case VirtualKey.Space:

                            if (mediaElement.CurrentState.ToString().Equals("Paused"))
                            {
                                mediaElement.Play();

                            }
                            else
                            {
                                mediaElement.Pause();

                            }
                            break;

                    }
                }


            };
            Window.Current.CoreWindow.KeyUp += (c, d) =>
            {

            };

            //fileactivatedeventargs();
            // Storyboard1.Completed+=Storyboard1_Completed;
            mediaElement.IsInteractive = true;
            mediaElement.RightTapped += mediaElement_RightTapped;
            mediaElement.MarkerReached += mediaElement_MarkerReached;

            grid.RightTapped += mediaElement_RightTapped;

            MediaControl.NextTrackPressed += OnNextTrackPressed;
            MediaControl.PreviousTrackPressed += OnPreviousTrackPressed;
            MediaControl.TrackName = "Metro Player";



            SearchPane = SearchPane.GetForCurrentView();
            SearchPane.SearchHistoryEnabled = false;

            Application.Current.Resuming += Current_Resuming;
            Application.Current.Suspending += Current_Suspending;
            Application.Current.UnhandledException += Current_UnhandledException;

            donotturnoffsceen();

            storyboard = Storyboard3;
            visual = visual1;
            Window.Current.SizeChanged += Current_SizeChanged;
            //Window.Current.SizeChanged += (object sender, Windows.UI.Core.WindowSizeChangedEventArgs e) =>
            //    {
            //        ApplicationViewState myViewState = ApplicationView.Value;
            //        if (myViewState == ApplicationViewState.Snapped)
            //        {
            //        }
            //        else
            //        { 
            //        }
            //    };
            //pickplaylist();
            WindowsBounds = Window.Current.Bounds;
            ClearRoamingAppData();
        }

        private void timer6_Tick(object sender, object e)
        {
            slider.Visibility = Visibility.Collapsed;
        }

        private async void CheckBackGroundStatus()
        {
            try
            {



                int c = BackgroundTaskRegistration.AllTasks.Count;
                if (c > 1)
                {
                    foreach (var bTasks in BackgroundTaskRegistration.AllTasks)
                    {
                        bTasks.Value.Unregister(true);
                    }
                }
                else if (c == 0)
                {
                    await BackgroundExecutionManager.RequestAccessAsync();
                    var background = new BackgroundTaskBuilder();
                    background.Name = "Metro Player";
                    background.TaskEntryPoint = "IPlayer_BackgroundTask.Task";
                    var sc = new SystemCondition(SystemConditionType.UserPresent);
                    background.AddCondition(sc);
                    background.SetTrigger(new TimeTrigger(45, false));
                    var btr = background.Register();
                }

                //string s = null;
                //foreach (var VARIABLE in BackgroundTaskRegistration.AllTasks)
                //{
                //    s += VARIABLE.Value.Name + "\n";

                //}
                // new MessageDialog(c.ToString()).ShowAsync();
            }
            catch
            {
                ErrorCorrecting("001");
            }
        }

        public async void ErrorCorrecting(String Message)
        {
        // await new MessageDialog(Message).ShowAsync();
        }
        public async void ClearRoamingAppData()
        {
            try
            {
                await ApplicationData.Current.ClearAsync(ApplicationDataLocality.Roaming);
            }
            catch
            {
                //  new MessageDialog("002").ShowAsync();
            }
        }

        ////protected async void OnFileActivated(FileActivatedEventArgs args) 
        ////{
        ////    StorageFile fi = args.Files[0] as StorageFile;
        ////    mediaElement.SetSource(await fi.OpenAsync(FileAccessMode.Read), "");
        ////    futurelocation =
        ////        Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(fi);
        ////    extensions = fi.FileType;
        ////    savestate(futurelocation);

        ////}

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            fileactivatedeventargs();

        }
        private async void fileactivatedeventargs()
        {
            //if (fileactivatedfile != null)
            //{
            //    textBlock.Visibility=Visibility.Collapsed;
            //    mediaElement.SetSource(await fileactivatedfile.OpenAsync(FileAccessMode.Read),"");
            //    futurelocation =
            //        Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(fileactivatedfile);
            //    extensions = fileactivatedfile.FileType;
            //    savestate(futurelocation);
            //}
            if (file1 != null)
            {

                Storyboard1.Stop();
                textBlock.Visibility = Visibility.Collapsed;
                // ls1.Items.Clear();
                Collections.Clear();
                file = file1;
                a = 0;


                // StringBuilder output = new StringBuilder("Picked files:\n");
                // Application now has read/write access to the picked file(s)
                try
                {
                    await pickingfiles();
                }
                catch
                {
                    ErrorCorrecting("003");
                }

            }
            else
            {

                Storyboard1.Begin();
            }
        }
        public List<JsonClass> jList = new List<JsonClass>();
        public class JsonClass
        {
            public string Futurelist { get; set; }
        }
        public async Task JSerialize(List<JsonClass> jL)
        {
            try
            {
                String g = JsonConvert.SerializeObject(jL, Formatting.Indented);
                StorageFile cFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("jsonFile.txt", CreationCollisionOption.ReplaceExisting);
                using (var sWriter = new StreamWriter(await cFile.OpenStreamForWriteAsync()))
                {
                    sWriter.Write(g);
                    await sWriter.FlushAsync();
                }
            }
            catch (Exception)
            {
                ErrorCorrecting("004");
            }

        }
        public async Task<List<StorageFile>> JDeSerialize()
        {
            var rFile = await ApplicationData.Current.LocalFolder.GetFileAsync("jsonFile.txt");
            String fString = null;
            using (var sReader = new StreamReader(await rFile.OpenStreamForReadAsync()))
            {
                fString = await sReader.ReadToEndAsync();
            }
            List<StorageFile> storageFiles = new List<StorageFile>();
            if (fString != null)
            {
                var ls2 = JsonConvert.DeserializeObject<List<JsonClass>>(fString);

                foreach (var jsonClass in ls2)
                {
                    //Below Code Try/Catch Need For Permission Purpose
                    try
                    {
                        storageFiles.Add(await StorageApplicationPermissions.FutureAccessList.GetFileAsync(jsonClass.Futurelist));
                    }
                    catch
                    {

                    }
                }
                // Debug.WriteLine("Well !!!... Its Working......");
            }
            return storageFiles;
        }
        private async Task pickingfiles()
        {

            //await Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
            //    {
            ProgressRing();
            textBlock.Visibility = Visibility.Collapsed;
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Clear();


            Collections.Clear();
            jList.Clear();
            //createtable();
            //var paths = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "base.db3");
            //using (SQLiteConnection db = new SQLiteConnection(paths))
            //{
            //db.BeginTransaction();

            //   ls1.Items.Add(files.DisplayName);
            //  new MessageDialog(  Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.MaximumItemsAllowed.ToString()).ShowAsync();
            //new MessageDialog(
            //   Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Entries.Count.ToString()).ShowAsync();

            int co = 0;
            var formats = new List<string>
                            {
                                ".mp3",
                                ".aac",
                                ".adt",
                                ".adts",
                                ".wav",
                                ".ac3",
                                ".ec3"
                            };

            foreach (StorageFile files in file)
            {
                if (co < 1000)
                {
                    //db.Insert(new playlistbase
                    //    {
                    //        playlistdata =
                    //            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList
                    //                   .Add(
                    //                       files),
                    //    });

                    String duration = null;
                    String album = "Unknown";
                    StorageFile files1 = files;
                    var ff = from x in formats where x.Equals(files1.FileType.ToLower()) select x;
                    int count = ff.Count();
                    if (count >= 1)
                    {
                        var properties = await files.Properties.GetMusicPropertiesAsync();
                        duration = properties.Duration.ToString(@"hh\:mm\:ss");
                        album = properties.Album == "" ? "Unknown Album" : properties.Album;
                    }
                    else
                    {
                        var proper = await files.Properties.GetVideoPropertiesAsync();
                        duration = proper.Duration.ToString(@"hh\:mm\:ss");
                    }
                    var playingking = new playlist
                        {
                            _displayname = files.DisplayName,
                            _contenttype = files.ContentType,
                            _datacreated = duration,
                            _mediafile = files,
                            _album = album,
                            _thumburi = new Uri("ms-appdata:///roaming/" + files.DisplayName + ".png"),
                        };

                    Collections.Add(playingking);
                }
                else
                {
                    break;
                }
                //    co = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Entries.Count();
                co = co + 1;
                block1.Text = co.ToString();
            }
            //  playlist.Files.Add(files);
            //db.Commit();
            //db.Dispose();
            //ls1.ItemsSource = p;
            ItemSource();
            Task.Run(async () =>
            {

                foreach (var files in file)
                {
                    jList.Add(new JsonClass
                    {
                        Futurelist =
                            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(files),
                    });
                    albumart(files);
                }
                await JSerialize(jList);
            });


            HideProgressGrid();
            //ls1.DataContext = new filename(files.DisplayName);
            //ls1.Items.Add(new filename(files.DisplayName.ToString())); ;
            // mediaElement.SetSource((await files.OpenAsync(FileAccessMode.Read)),"");
            //}

            #region playlist

            //  await playlist.SaveAsAsync(KnownFolders.MusicLibrary, "rocks", NameCollisionOption.ReplaceExisting,PlaylistFormat.WindowsMedia);

            #endregion

            ls1.SelectedIndex = 0;
            ////});

        }



        public DisplayRequest dispRequest = new DisplayRequest();
        private void donotturnoffsceen()
        {
            try
            {
                //if (dispRequest == null)
                //{

                // Activate a display-required request. If successful, the screen is 
                // guaranteed not to turn off automatically due to user inactivity.

                dispRequest.RequestActive();
                // Insert your own code here to start the video
                //}
            }
            catch
            {
                ErrorCorrecting("005");
            }

            ////if (dispRequest != null)
            ////{

            ////    // Deactivate the display request and set the var to null.
            ////    dispRequest.RequestRelease();
            ////    dispRequest = null;

            ////}
        }

        void storyboard_Completed(object sender, object e)
        {
            storyboard.Begin();
        }
        #endregion
        #region MediaStates
        public void mediaElement_MarkerReached(object sender, TimelineMarkerRoutedEventArgs e)
        {

            Caption.Text = e.Marker.Text;
        }
        #endregion

        #region App States
        private void Current_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {


        }
        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {

        }

        private void Current_Resuming(object sender, object e)
        {
        }
        #endregion

        #region Background Playback
        private async void OnPreviousTrackPressed(object sender, object e)
        {
            try
            {

                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;

                await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    RoutedEventArgs a = new RoutedEventArgs();
                    recentvideo_Click(sender, a);
                });

            }
            catch
            {
                ErrorCorrecting("006");
            }
        }

        private async void OnNextTrackPressed(object sender, object e)
        {
            try
            {

                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;

                await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    RoutedEventArgs a = new RoutedEventArgs();
                    nextvideo_Click(sender, a);
                });

            }
            catch
            {
                ErrorCorrecting("007");
            }
        }
        #endregion
        public CoreDispatcher dis;
        private DataTransferManager dtm;
        private Windows.Media.PlayTo.PlayToManager ptm;
        public SettingsPane SettingsPane;

        #region Contracts
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dis = Window.Current.CoreWindow.Dispatcher;
            ptm = Windows.Media.PlayTo.PlayToManager.GetForCurrentView();
            ptm.SourceRequested += sourceRequestHandler;

            try
            {


                dtm = DataTransferManager.GetForCurrentView();
                dtm.DataRequested += dtm_DataRequested;

                SearchPane.SuggestionsRequested += SearchPane_SuggestionsRequested;
                SearchPane.QuerySubmitted += SearchPane_QuerySubmitted;

                SettingsPane.GetForCurrentView().CommandsRequested += SettingsPane_CommandsRequested;

                //var mouse = new MouseCapabilities();
                //if (mouse.MousePresent >= 1)
                //    this.PointerMoved += OnMoved;
            }
            catch
            {
                ErrorCorrecting("008");
            }

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            dtm.DataRequested -= dtm_DataRequested;
            ptm.SourceRequested -= sourceRequestHandler;
            SettingsPane.GetForCurrentView().CommandsRequested -= SettingsPane_CommandsRequested;
            Window.Current.SizeChanged -= Current_SizeChanged;
        }
        public void SettingsPane_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
           
            SettingsCommand settings = new SettingsCommand("Settings", "Music Info",async handle =>
            {
               await DynamicSettings(new SettingsFlyLayout(), 346);
            });
            var shortcuts = new SettingsCommand("Shortcuts", "Shortcuts",async handle =>
            {
               await SettingControls(new Shortcuts(), 346);
            });
            args.Request.ApplicationCommands.Add(settings);
            args.Request.ApplicationCommands.Add(shortcuts);
        }

        public Rect WindowsBounds;
        public void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            WindowsBounds = Window.Current.Bounds;
        }

        private Popup _settingPopup;
        private async Task DynamicSettings(UserControl user,Double width)
        {
            if (futurelocation != null)
            {
                _settingPopup = new Popup();
                _settingPopup.Closed += _settingPopup_Closed;
                Window.Current.Activated += Current_Activated;
                _settingPopup.IsLightDismissEnabled = true;
                _settingPopup.Width = width;
                _settingPopup.Height = WindowsBounds.Height;

                _settingPopup.ChildTransitions = new TransitionCollection();

                _settingPopup.ChildTransitions.Add(
                    new PaneThemeTransition()
                    {
                        Edge =
                            (SettingsPane.Edge == SettingsEdgeLocation.Right)
                                ? EdgeTransitionLocation.Right
                                : EdgeTransitionLocation.Left
                    });

                SettingsFlyLayout settingsFlyLayout = new SettingsFlyLayout();

                settingsFlyLayout.StorageFile =
                    await
                        Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(
                            futurelocation);

                settingsFlyLayout.Width =width;
                settingsFlyLayout.Height = WindowsBounds.Height;

                _settingPopup.Child = settingsFlyLayout;
                _settingPopup.SetValue(
                    Canvas.LeftProperty,
                    SettingsPane.Edge == SettingsEdgeLocation.Right ? (WindowsBounds.Width - width) : 0
                    );
                _settingPopup.SetValue(Canvas.TopProperty, 0);
                _settingPopup.IsOpen = true;
            }
        }
        private async Task SettingControls(UserControl user, Double width)
        {
           
                _settingPopup = new Popup();
                _settingPopup.Closed += _settingPopup_Closed;
                Window.Current.Activated += Current_Activated;
                _settingPopup.IsLightDismissEnabled = true;
                _settingPopup.Width = width;
                _settingPopup.Height = WindowsBounds.Height;

                _settingPopup.ChildTransitions = new TransitionCollection();

                _settingPopup.ChildTransitions.Add(
                    new PaneThemeTransition()
                    {
                        Edge =
                            (SettingsPane.Edge == SettingsEdgeLocation.Right)
                                ? EdgeTransitionLocation.Right
                                : EdgeTransitionLocation.Left
                    });

             
               user.Width = width;
                user.Height = WindowsBounds.Height;

                _settingPopup.Child = user;
                _settingPopup.SetValue(
                    Canvas.LeftProperty,
                    SettingsPane.Edge == SettingsEdgeLocation.Right ? (WindowsBounds.Width - width) : 0
                    );
                _settingPopup.SetValue(Canvas.TopProperty, 0);
                _settingPopup.IsOpen = true;
           
        }

        void Current_Activated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                _settingPopup.IsOpen = false;
            }
        }

        public void _settingPopup_Closed(object sender, object e)
        {
            Window.Current.Activated -= Current_Activated;
        }



        //  private FrameworkElement _frameworkElement;
        private async void sourceRequestHandler(Windows.Media.PlayTo.PlayToManager sender, Windows.Media.PlayTo.PlayToSourceRequestedEventArgs e)
        {
            await dis.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    Windows.Media.PlayTo.PlayToSourceRequest sr = e.SourceRequest;
                    Windows.Media.PlayTo.PlayToSource controller = null;
                    Windows.Media.PlayTo.PlayToSourceDeferral deferral = e.SourceRequest.GetDeferral();

                    //_frameworkElement = mediaElement;
                    controller = mediaElement.PlayToSource;

                    sr.SetSource(controller);
                    deferral.Complete();
                }
                catch (InvalidTimeZoneException)
                {

                }
                catch (InvalidOperationException)
                {

                }
                catch (InvalidCastException)
                {

                }
                catch (Exception)
                {

                }
            });
        }
        async void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataPackage dp = args.Request.Data;

            if (ls1.SelectedItem != null)
            {
                DataRequestDeferral waiter = args.Request.GetDeferral();
                try
                {
                    dp.Properties.ApplicationName = "Metro Player";
                    dp.Properties.Title = "Metro Player";
                    dp.Properties.Description = "Share Files";
                    object f = ls1.SelectedItem;
                    playlist g = f as playlist;
                    dp.SetText(g.DisplayName);
                    var h = await g.MediaFile.GetBasicPropertiesAsync();
                    var size = h.Size / (1024 * 1024);
                    if (size <= 19.0)
                    {
                        List<IStorageItem> ist = new List<IStorageItem>();
                        ist.Add(g.MediaFile);
                        dp.SetStorageItems(ist);
                    }
                }
                catch
                {
                    ErrorCorrecting("009");
                }
                finally
                {
                    waiter.Complete();
                }

            }
            else
            {
                args.Request.FailWithDisplayText("Nothing To Share Now");
            }
        }


        public string medianame;
        async void SearchPane_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {




            //   new MessageDialog(SearchPane.QueryText.ToString()).ShowAsync();
            foreach (StorageFile files in file)
            {
                if (files.DisplayName == args.QueryText.ToString())
                {
                    mediaElement.SetSource(await files.OpenAsync(FileAccessMode.Read), "");
                    //
                    medianame = files.DisplayName;

                    extensions = files.FileType.ToLower();

                    //
                    futurelocation =
                        Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(files);
                    savestate(futurelocation);
                    if (args.QueryText.ToString() != "")
                    {
                        timer4.Interval = new TimeSpan(0, 0, 0, 1);
                        timer4.Start();
                        timer4.Tick += timer4_Tick;

                        updatetile();
                    }
                    //using (StorageItemThumbnail storageItemThumbnail = await files.GetThumbnailAsync(ThumbnailMode.SingleItem, 200, ThumbnailOptions.ResizeThumbnail))
                    //using (IInputStream inputStreamAt = storageItemThumbnail.GetInputStreamAt(0))
                    //using (var dataReader = new DataReader(inputStreamAt))
                    //{
                    //    uint u = await dataReader.LoadAsync((uint)storageItemThumbnail.Size);
                    //    IBuffer readBuffer = dataReader.ReadBuffer(u);

                    //    var tempFolder = ApplicationData.Current.LocalFolder;
                    //    var imageFile = await tempFolder.CreateFileAsync(filename+".png", CreationCollisionOption.ReplaceExisting);

                    //    using (IRandomAccessStream randomAccessStream = await imageFile.OpenAsync(FileAccessMode.ReadWrite))
                    //    using (IOutputStream outputStreamAt = randomAccessStream.GetOutputStreamAt(0))
                    //    {
                    //        await outputStreamAt.WriteAsync(readBuffer);
                    //        await outputStreamAt.FlushAsync();
                    //    }
                    //}

                }
            }


        }
        void SearchPane_SuggestionsRequested(SearchPane sender, SearchPaneSuggestionsRequestedEventArgs args)
        {
            try
            {
                //var q = from c in file
                //        where c.DisplayName.ToLower().Contains(args.QueryText.ToLower())
                //        select c.DisplayName;
                var g = (from x in Collections
                         where x.DisplayName.ToLower().Contains(args.QueryText.ToLower())
                         select x.DisplayName).Distinct();




                foreach (var s in g)
                {
                    args.Request.SearchSuggestionCollection.AppendQuerySuggestion(s);
                }
            }
            catch
            {
                ErrorCorrecting("010");
            }





        }
        #endregion
        void timer4_Tick(object sender, object e)
        {
            ToastNotification(true);
            timer4.Stop();
        }
        DispatcherTimer timer4 = new DispatcherTimer();
        #region Contracts
        private void ToastNotification(bool then)
        {
            ToastTemplateType toastType = ToastTemplateType.ToastImageAndText03;
            XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(toastType);
            XmlNodeList toastText = toastXML.GetElementsByTagName("text");
            XmlNodeList toastImages = toastXML.GetElementsByTagName("image");
            toastText[0].InnerText = "Now Playing                                   " + medianame;
            toastText[1].InnerText = "Duration : " + mediaElement.Duration.ToString(@"hh\:mm\:ss");
            ((XmlElement)toastImages[0]).SetAttribute("src", "ms-appdata:///roaming/" + medianame + ".png");


            ToastNotification toast = new ToastNotification(toastXML);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
        #endregion




        public double x, y;
        public void OnMoved(object sender, PointerRoutedEventArgs e)
        {
            //if (e.Pointer.PointerDeviceType != PointerDeviceType.Mouse)
            //    return;

            //this.PointerMoved -= OnMoved;

            //PointerPoint pp = e.GetCurrentPoint(null); // or pass an element for relative coords

            //x = pp.Position.X;
            //y = pp.Position.Y;
        }
        #region MediaStates

        async void mediaElement_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {

            Rightbar.IsOpen = true;
            Leftbar.IsOpen = true;
            Bottombar.IsOpen = true;
            Topbar.IsOpen = true;
            try
            {
                //var button = (MediaElement)sender;
                //var transform = button.TransformToVisual(this);
                //var point = transform.TransformPoint(new Windows.Foundation.Point(0, 0));

                PopupMenu p = new PopupMenu();
                p.Commands.Add(new UICommand("Play", null, 0));
                p.Commands.Add(new UICommand("Pause", null, 1));
                p.Commands.Add(new UICommand("Next", null, 2));
                p.Commands.Add(new UICommand("Previous", null, 3));
                p.Commands.Add(new UICommandSeparator());
                p.Commands.Add(new UICommand("Open", null, 4));

                var selectedCommand = await p.ShowAsync(_pos);
                if (selectedCommand != null)
                {


                    switch ((int)selectedCommand.Id)
                    {
                        case 0:
                            mediaElement.Play();
                            break;
                        case 1:
                            mediaElement.Pause();
                            // mediaElement.SeekWhileScrubbing = true;

                            break;
                        case 2:
                            nextvideo_Click(sender, e);
                            break;
                        case 3:
                            recentvideo_Click(sender, e);
                            break;
                        case 4:
                            button1_Click(sender, e);
                            break;
                    }
                }
            }
            catch
            {
                ErrorCorrecting("011");
            }

        }

        #endregion
        //public class playlistbase
        //{
        //    [MaxLength(100), PrimaryKey]
        //    public String playlistdata { get; set; }
        //}

        // public Playlist playlist=new Playlist();
        public string futurelocation;
        #region Open Media Files
        private async void button1_Click(object sender, RoutedEventArgs e)
        {




            ApplicationViewState myViewState = ApplicationView.Value;

            if (myViewState == ApplicationViewState.Snapped)
            {
                Windows.UI.ViewManagement.ApplicationView.TryUnsnap();
            }

            FileOpenPicker openPicker = new FileOpenPicker();

            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            openPicker.FileTypeFilter.Add(".3g2");
            openPicker.FileTypeFilter.Add(".3gp2");
            openPicker.FileTypeFilter.Add(".3gp");
            openPicker.FileTypeFilter.Add(".3gpp");
            openPicker.FileTypeFilter.Add(".m4a");
            openPicker.FileTypeFilter.Add(".m4v");
            openPicker.FileTypeFilter.Add(".mp4v");
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".mov");
            openPicker.FileTypeFilter.Add(".m2ts");
            openPicker.FileTypeFilter.Add(".asf");
            openPicker.FileTypeFilter.Add(".wm");
            openPicker.FileTypeFilter.Add(".vob");
            openPicker.FileTypeFilter.Add(".wmv");
            openPicker.FileTypeFilter.Add(".wma");
            openPicker.FileTypeFilter.Add(".aac");
            openPicker.FileTypeFilter.Add(".adt");
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".wav");
            openPicker.FileTypeFilter.Add(".avi");
            openPicker.FileTypeFilter.Add(".ac3");
            openPicker.FileTypeFilter.Add(".ec3");



            openPicker.ViewMode = PickerViewMode.List;

            mediaElement.Pause();

            file1 = await openPicker.PickMultipleFilesAsync();
            // ls1.Visibility = Visibility.Visible;



            if (file1.Count > 0)
            {
                ProgressRing();


                Storyboard1.Stop();
                textBlock.Visibility = Visibility.Collapsed;
                // ls1.Items.Clear();
                Collections.Clear();
                jList.Clear();
                file = file1;
                a = 0;

                //  IStorageItem dc = file[0];

                //  var list = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(dc);
                //StorageFile f=await  Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(list);


                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Clear();


                int co = 0;
                List<string> formats = new List<string> { ".mp3", ".aac", ".adt", ".adts", ".wav", ".ac3", ".ec3" };
                // StringBuilder output = new StringBuilder("Picked files:\n");
                // Application now has read/write access to the picked file(s)
                try
                {
                    //createtable();
                    //var paths = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "base.db3");
                    //using (SQLiteConnection db = new SQLiteConnection(paths))
                    //{
                    //    db.BeginTransaction();
                    foreach (StorageFile files in file)
                    {
                        //   ls1.Items.Add(files.DisplayName);
                        if (co < 999)
                        {
                            //db.Insert(new playlistbase
                            //    {
                            //        playlistdata =
                            //            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(
                            //                files),
                            //    });

                            jList.Add(new JsonClass
                            {
                                Futurelist = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(files),
                            });

                            albumart(files);

                            String duration = null;
                            String album = "Unknown";

                            StorageFile files1 = files;
                            var ff = from x in formats where x.Equals(files1.FileType.ToLower()) select x;
                            int count = ff.Count();
                            if (count >= 1)
                            {
                                var properties = await files.Properties.GetMusicPropertiesAsync();
                                duration = properties.Duration.ToString(@"hh\:mm\:ss");
                                album = properties.Album == "" ? "Unknown Album" : properties.Album;
                            }
                            else
                            {
                                var proper = await files.Properties.GetVideoPropertiesAsync();
                                duration = proper.Duration.ToString(@"hh\:mm\:ss");
                            }
                            var playingking = new playlist
                                {
                                    _displayname = files.DisplayName,
                                    _contenttype = files.ContentType,
                                    _thumburi = new Uri("ms-appdata:///roaming/" + files.DisplayName + ".png"),
                                    _datacreated = duration,
                                    _mediafile = files,
                                    _album = album,
                                };

                            Collections.Add(playingking);
                        }
                        else
                        {

                            break;
                        }

                        co = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Entries.Count();
                        block1.Text = co.ToString();

                    }
                    ////  playlist.Files.Add(files);
                    //db.Commit();
                    //db.Dispose();

                    //ls1.ItemsSource = p;

                    await JSerialize(jList);
                    ItemSource();

                    //ls1.DataContext = new filename(files.DisplayName);
                    //ls1.Items.Add(new filename(files.DisplayName.ToString())); ;
                    // mediaElement.SetSource((await files.OpenAsync(FileAccessMode.Read)),"");
                    //}


                    #region playlist
                    //  await playlist.SaveAsAsync(KnownFolders.MusicLibrary, "rocks", NameCollisionOption.ReplaceExisting,PlaylistFormat.WindowsMedia);
                    #endregion
                    ls1.SelectedIndex = 0;
                    HideProgressGrid();
                }
                catch
                {

                    ErrorCorrecting("012");
                }

            }
            else
            {
                mediaElement.Play();
                ls1.Visibility = Visibility.Collapsed;

            }
        }

        private void ItemSource()
        {
            if (Combosearchbox.SelectedIndex == 0)
            {
                var Results = from x in Collections.AsParallel().AsOrdered()
                              group x by x.DisplayName.Substring(0, 1).ToUpper()
                                  into g
                                  orderby g.Key
                                  select new { Key = g.Key, Items = g };




                Cvs.Source = Results.AsParallel().AsOrdered();
                var listViewBase = Sematiczoom1.ZoomedOutView as ListViewBase;

                listViewBase.ItemsSource = Cvs.View.CollectionGroups.AsParallel();


                var f = (from x in Collections
                         select x.DisplayName).Distinct();
                Searchsongs.ItemsSource = f.ToList();
            }
            else
            {
                Combosearchbox.SelectedIndex = 0;
            }

            //await ls1.LoadMoreItemsAsync();
            //await Gridview1.LoadMoreItemsAsync();
            //Gridview1.ItemsSource = Collections;
            //Gridview2.ItemsSource = Results;
        }
        //private async void ItemSource()
        //{
        //    var aw = Sorting().GetAwaiter();
        //    aw.OnCompleted(() =>
        //        {
        //            Cvs.Source = aw.GetResult().AsParallel();
        //            var listViewBase = Sematiczoom1.ZoomedOutView as ListViewBase;

        //            listViewBase.ItemsSource = Cvs.View.CollectionGroups.AsParallel();
        //        });
        //}

        //public Task<ParallelQuery> Sorting()
        //{
        //    return Task.Run(() =>
        //        {
        //            var Results = from x in Collections.AsParallel().AsOrdered()
        //                          group x by x._contenttype
        //                          into g
        //                          orderby g.Key
        //                          select new { Key = g.Key, Items = g };
        //        return (ParallelQuery)Results;
        //    });
        //}
        //protected override void OnDrop(DragEventArgs e)
        //{
        //    base.OnDrop(e);

        //}
        //protected override void OnDragEnter(DragEventArgs e)
        //{
        //    base.OnDragEnter(e);

        //}
        //protected override void OnDragLeave(DragEventArgs e)
        //{
        //    base.OnDragLeave(e);

        //}
        //protected override void OnDragOver(DragEventArgs e)
        //{
        //    base.OnDragOver(e);

        //}

        private void ProgressRing()
        {
            progressgrid.Visibility = Visibility.Visible;
            progressring.IsActive = true;
        }
        private void HideProgressGrid()
        {
            progressring.IsActive = false;
            progressgrid.Visibility = Visibility.Collapsed;
        }
        public async void albumart(StorageFile fil)
        {
            using (StorageItemThumbnail storageItemThumbnail = await fil.GetThumbnailAsync(ThumbnailMode.SingleItem, 200, ThumbnailOptions.ResizeThumbnail))

            using (IInputStream inputStreamAt = storageItemThumbnail.GetInputStreamAt(0))
            using (var dataReader = new DataReader(inputStreamAt))
            {
                uint u = await dataReader.LoadAsync((uint)storageItemThumbnail.Size);
                IBuffer readBuffer = dataReader.ReadBuffer(u);

                var tempFolder = ApplicationData.Current.RoamingFolder;
                try
                {
                    var imageFile = await tempFolder.CreateFileAsync(fil.DisplayName + ".png", CreationCollisionOption.OpenIfExists);

                    using (IRandomAccessStream randomAccessStream = await imageFile.OpenAsync(FileAccessMode.ReadWrite))
                    using (IOutputStream outputStreamAt = randomAccessStream.GetOutputStreamAt(0))
                    {
                        await outputStreamAt.WriteAsync(readBuffer);
                        await outputStreamAt.FlushAsync();
                    }
                }
                catch
                {

                    //  ErrorCorrecting("013");

                }

            }
        }

        //private static void createtable()
        //{
        //    var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "base.db3");
        //    using (SQLiteConnection db = new SQLiteConnection(path))
        //    {
        //        if (db.Table<playlistbase>() != null)
        //        {
        //            db.CreateTable<playlistbase>();
        //        }
        //        db.DeleteAll<playlistbase>();
        //        db.Commit();
        //    }
        //}

        #endregion
        #region Controls
        private void button_Click(object sender, RoutedEventArgs e)
        {

            Windows.UI.ViewManagement.ApplicationView.TryUnsnap();

        }
        #endregion
        //public async void pickplaylist()
        //{
        //    try
        //    {
        //        FileOpenPicker picker = new FileOpenPicker();
        //        picker.SuggestedStartLocation=PickerLocationId.MusicLibrary;
        //        picker.FileTypeFilter.Add(".wpl");
        //        StorageFile fileplay = await picker.PickSingleFileAsync();

        //        Playlist lay = null;

        //        if (fileplay != null)
        //        {
        //            await Playlist.LoadAsync(fileplay);
        //        }

        //        if (lay.Files.Count == 0)
        //        {

        //        }
        //        else
        //        {

        //            List<StorageFile> stlist = new List<StorageFile>();
        //            foreach (StorageFile fiii in lay.Files)
        //            {
        //                stlist.Add(fiii);
        //                ls1.Items.Add(fiii.DisplayName);
        //            }
        //            file = stlist;
        //        }
        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        new MessageDialog("No Access").ShowAsync();
        //    }
        //   catch(NullReferenceException)
        //    {}
        //}



        #region ListView Activities
        private void ls1_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            //  KeyboardControl();
            //Caption.Text = "";
            ////  var ggg = ls1.ItemsSource.GetType().GetElementType("DisplayName").ToString();
            //object objectlist = ls1.SelectedItem;
            //playlist playlistlist = objectlist as playlist;

            try
            {

                if (file != null)
                {
                    if (Gridview1.SelectedIndex == ls1.SelectedIndex)
                    {

                    }
                    else
                    {
                        Gridview1.SelectedIndex = ls1.SelectedIndex;
                    }
                    ////foreach (StorageFile files in file)
                    ////{
                    ////    if (files.DisplayName == playlistlist.DisplayName.ToString())
                    ////    {
                    ////String json = Newtonsoft.Json.JsonConvert.SerializeObject(playlistlist.MediaFile);
                    ////StorageFile fil = (StorageFile)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    ////MemoryStream ms=new MemoryStream();
                    ////DataContractSerializer dataContractSerializer=new DataContractSerializer(typeof(StorageFile));
                    ////dataContractSerializer.WriteObject(ms,playlistlist.MediaFile);
                    ////  StorageFile fi;
                    ////  fi = (StorageFile) dataContractSerializer.ReadObject(ms);

                    ////StorageFile f=await StorageFile.GetFileFromPathAsync("String");

                    //mediaElement.SetSource((await playlistlist.MediaFile.OpenAsync(FileAccessMode.Read)), "");

                    //MediaControl.TrackName = playlistlist.MediaFile.DisplayName;

                    //futurelocation = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(playlistlist.MediaFile);

                    //savestate(futurelocation);

                    //extensions = playlistlist.MediaFile.FileType.ToLower();

                    //mediaalbumart();
                    //updatetile();

                    //// MediaControl.AlbumArt=new Uri("ms-appx:///v.png");
                    ////    }
                    ////}
                }
            }
            catch
            {
                //  ErrorCorrecting("014");
            }
        }
        #endregion
        #region Album Art
        public async void mediaalbumart()
        {
            try
            {
                StorageFile media =
                await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(futurelocation);
                string filename = "thumbnail" + ".png";
                //var  ThumbUri = new Uri("ms-appdata:///local/" + filename);

                // Store the file thumbnail in local applicatin storage
                using (
                    StorageItemThumbnail storageItemThumbnail =
                        await media.GetThumbnailAsync(ThumbnailMode.SingleItem, 200, ThumbnailOptions.ResizeThumbnail))
                using (IInputStream inputStreamAt = storageItemThumbnail.GetInputStreamAt(0))
                using (var dataReader = new DataReader(inputStreamAt))
                {
                    uint u = await dataReader.LoadAsync((uint)storageItemThumbnail.Size);
                    IBuffer readBuffer = dataReader.ReadBuffer(u);

                    var tempFolder = ApplicationData.Current.LocalFolder;
                    var imageFile = await tempFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

                    using (IRandomAccessStream randomAccessStream = await imageFile.OpenAsync(FileAccessMode.ReadWrite))
                    using (IOutputStream outputStreamAt = randomAccessStream.GetOutputStreamAt(0))
                    {
                        await outputStreamAt.WriteAsync(readBuffer);
                        await outputStreamAt.FlushAsync();
                    }
                }
                MediaControl.AlbumArt = new Uri("ms-appdata:///local/" + filename);
            }
            catch
            {

                //   ErrorCorrecting("015");
            }

        }
        #endregion
        #region Controls
        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            //if (ls1.Items.Count > 0)
            //{

            //    if (ls1.Visibility == Visibility.Collapsed)
            //    {

            //        ls1.Visibility = Visibility.Visible;


            //    }
            //    else
            //    {
            //        ls1.Visibility = Visibility.Collapsed;
            //    }

            //}
            UpdateLayout();
            if (ls1.Visibility == Visibility.Visible)
            {
                ls1.Visibility = Visibility.Collapsed;
                Rightbar.IsOpen = false;
                Leftbar.IsOpen = false;
                Bottombar.IsOpen = false;
                Topbar.IsOpen = false;
            }
            if (Gridview1.Items.Count > 0)
            {

                if (Groupinggrid.Visibility == Visibility.Collapsed)
                {

                    Groupinggrid.Visibility = Visibility.Visible;
                    ls1.IsActiveView = false;
                    Gridview1.IsActiveView = true;
                    Gridview1.IsActiveView = true;

                }
                else
                {
                    Groupinggrid.Visibility = Visibility.Collapsed;
                }

            }

        }
        #endregion
        public Int32 a = 0;
        #region MediaStates
        private void mediaElement_MediaEnded(object sender, MediaPlayerActionEventArgs e)
        {
            try
            {
                //Uri source = mediaElement.Source;
                //mediaElement.Source = null;
                //mediaElement.AutoLoad = true;
                //mediaElement.Source = source;
                Int32 b = ls1.Items.Count;
                a = ls1.SelectedIndex;
                if (a < b - 1)
                {


                    //  txt1.Text = "a is" + a + "b is" + b.ToString();
                    a = a + 1;
                    ls1.SelectedIndex = a;


                }

                else
                {
                    ls1.SelectedIndex = 0;

                }
                ToastNotification();
                updatetile();
            }
            catch
            {
                ErrorCorrecting("016");
            }

        }
        #endregion
        private async System.Threading.Tasks.Task Playnextbefore()
        {

            if (file.Count > 0)
            {
                object o = ls1.SelectedItem;
                playlist king = o as playlist;

                foreach (StorageFile files in file)
                {
                    if (files.DisplayName == king.DisplayName)
                    {
                        futurelocation = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(files);
                        savestate(futurelocation);
                        //  var a=await files.OpenAsync(FileAccessMode.Read);
                        mediaElement.SetSource((await files.OpenAsync(FileAccessMode.Read)), "");
                        MediaControl.TrackName = files.DisplayName;
                        extensions = files.FileType.ToLower();
                        mediaalbumart();
                        //  ls1.Visibility = Visibility.Collapsed;

                    }
                }
            }
        }
        #region MediaStates

        private void mediaElement_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            Window.Current.CoreWindow.PointerCursor = null;
            ls1.Visibility = Visibility.Collapsed;
            Rightbar.IsOpen = false;
            Leftbar.IsOpen = false;
            Bottombar.IsOpen = false;
            Topbar.IsOpen = false;
            button1.Visibility = Visibility.Collapsed;
            bt2.Visibility = Visibility.Collapsed;
            nextvideo.Visibility = Visibility.Collapsed;
            recentvideo.Visibility = Visibility.Collapsed;
            button2.Visibility = Visibility.Collapsed;
            Mutemedia.Visibility = Visibility.Collapsed;
            button3.Visibility = Visibility.Collapsed;
            option.Visibility = Visibility.Collapsed;
            stackPanel.Visibility = Visibility.Collapsed;
            Subtitlebutton.Visibility = Visibility.Collapsed;
            volumea.Visibility = Visibility.Collapsed;
            SearchButton.Visibility = Visibility.Collapsed;
            ringslice.Visibility = Visibility.Collapsed;
            openfolder.Visibility = Visibility.Collapsed;
            FileSendButton.Visibility = Visibility.Collapsed;
            button4.Visibility = Visibility.Collapsed;

        }

        #endregion

        #region ListView Activities
        private void nextvideo_Click(object sender, RoutedEventArgs e)
        {
            //if (ls1.Items != null && ls1.Items.Count > 0)
            //{
            //    ls1.Visibility = Visibility.Visible;

            //}


            try
            {

                Int32 b = ls1.Items.Count;
                a = ls1.SelectedIndex;
                if (a < b - 1)
                {


                    //  txt1.Text = "a is" + a + "b is" + b.ToString();
                    a = a + 1;
                    ls1.SelectedIndex = a;



                }

                else
                {
                    ls1.SelectedIndex = 0;

                }
                //  ToastNotification();

            }
            catch
            {
                ErrorCorrecting("017");
            }




        }
        #endregion



        #region contracts
        private async void ToastNotification()
        {
            try
            {
                if (ls1.SelectedItem != null)
                {
                    object o = ls1.SelectedItem;
                    playlist king = o as playlist;


                    ToastTemplateType toastType = ToastTemplateType.ToastImageAndText03;
                    XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(toastType);
                    XmlNodeList toastText = toastXML.GetElementsByTagName("text");
                    XmlNodeList toastImages = toastXML.GetElementsByTagName("image");
                    toastText[0].InnerText = "Now Playing                                   " + king.DisplayName;
                    toastText[1].InnerText = "Duration : " + king.DateCreated;
                    ((XmlElement)toastImages[0]).SetAttribute("src",
                                                              "ms-appdata:///roaming/" + king.DisplayName + ".png");


                    ToastNotification toast = new ToastNotification(toastXML);
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                }
                else
                {
                    var local = ApplicationData.Current.LocalSettings;
                    object mediastatefile = local.Values["mediastatefile"];
                    object mediastatetime = local.Values["mediastatetime"];
                    lastposition = Convert.ToDouble(mediastatetime) / 60;
                    StorageFile filedd = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(mediastatefile.ToString());
                    if (filedd != null)
                    {
                        ToastTemplateType toastType = ToastTemplateType.ToastImageAndText03;
                        XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(toastType);
                        XmlNodeList toastText = toastXML.GetElementsByTagName("text");
                        XmlNodeList toastImages = toastXML.GetElementsByTagName("image");
                        toastText[0].InnerText = "Now Playing                                   " + filedd.DisplayName;
                        toastText[1].InnerText = "Duration : " + lastposition;
                        ((XmlElement)toastImages[0]).SetAttribute("src",
                                                                   "ms-appdata:///roaming/" + filedd.DisplayName + ".png");

                        ToastNotification toast = new ToastNotification(toastXML);
                        ToastNotificationManager.CreateToastNotifier().Show(toast);
                    }
                }

            }
            catch (Exception)
            {

                ErrorCorrecting("018");
            }

        }
        #endregion
        #region ListView Activities
        private void recentvideo_Click(object sender, RoutedEventArgs e)
        {
            //if (ls1.Items.Count > 0)
            //{
            //    ls1.Visibility = Visibility.Visible;
            //}
            try
            {
                Int32 b = ls1.Items.Count;
                a = ls1.SelectedIndex;


                if (a > 0 && a <= b - 1)
                {


                    //  txt1.Text = "a is" + a + "b is" + b.ToString();
                    a = a - 1;
                    ls1.SelectedIndex = a;

                    //await Playnextbefore();
                }
                else if (a == 0)
                {
                    ls1.SelectedIndex = b - 1;
                    //await Playnextbefore();
                }

                //ToastNotification();

            }
            catch
            {
                ErrorCorrecting("019");
            }
        }
        #endregion

        #region Storyboard
        private void Storyboard1_Completed(object sender, object e)
        {

            try
            {
                onappload();
            }
            catch
            {

                ErrorCorrecting("020");
            }


        }
        #endregion





        #region Controls
        private void optionflyout(object sender, RoutedEventArgs e)
        {
            try
            {

                Flyout fly = new Flyout();
                fly.PlacementTarget = sender as UIElement;
                fly.Placement = PlacementMode.Top;
                fly.Closed += (x, y) =>
                {

                };

                MenuItem menuitem1 = new MenuItem();
                MenuItem menuitem2 = new MenuItem();
                MenuItem menuitem3 = new MenuItem();
                MenuItem menuitem4 = new MenuItem();
                MenuItem menuitem5 = new MenuItem();

                Menu menu = new Menu();

                menuitem5.Tag = "Change Subtitle";
                menuitem5.Text = "Change Subtitle";
                menuitem5.Tapped += async (j, k) =>
                    {
                        if (mediaElement.AvailableCaptions.Count > 0)
                        {


                            Flyout fl = new Flyout();
                            fl.PlacementTarget = sender as UIElement;
                            fl.Placement = PlacementMode.Top;
                            fl.Closed += (x, y) =>
                            {

                            };

                            ListView la = new ListView();
                            la.Width = 220;
                            la.Height = 170;
                            la.BorderBrush = new SolidColorBrush(Colors.BlueViolet);
                            la.Background = new SolidColorBrush(Colors.Lavender);
                            la.Foreground = new SolidColorBrush(Colors.Black);
                            foreach (var f in mediaElement.AvailableCaptions)
                            {
                                la.Items.Add(f.ToString());
                            }
                            la.SelectionChanged += (g, b) =>
                                {
                                    int co = 0;
                                    foreach (var fff in mediaElement.AvailableCaptions)
                                    {

                                        if (la.SelectedItem != null && fff.ToString() == la.SelectedItem.ToString())
                                        {
                                            //   await new MessageDialog("No Subtitle Found In Media !!!Load Manually"+co.ToString()).ShowAsync();
                                            mediaElement.SelectedCaption = fff;
                                        }
                                        co = co + 1;
                                    }
                                };
                            fl.Content = la;
                            fl.IsOpen = true;
                            UpdateLayout();

                        }
                        else
                        {
                            await new MessageDialog("No Subtitle Found In Media !!!Load Manually").ShowAsync();
                        }


                    };
                menuitem5.MenuTextMargin = new Thickness(28, 10, 28, 12);
                menuitem1.Tag = "Change Audio";
                menuitem1.Text = "Change Audio";
                menuitem1.Tapped += (a, b) =>
                    {
                        mediaElement.InvokeAudioSelection();
                    };
                menuitem1.MenuTextMargin = new Thickness(28, 10, 28, 12);


                menuitem2.Tag = "Visualization 1";
                menuitem2.Text = "Visualization 1";
                menuitem2.Tapped += (c, d) =>
                    {
                        visual.Visibility = Visibility.Collapsed;
                        visual = visual1;
                        visual.Visibility = Visibility.Visible;
                        storyboard = Storyboard3;
                        storyboard.Begin();
                        storyboard.SpeedRatio = 1;
                        storyboard.Completed += storyboard_Completed;
                    };
                menuitem2.MenuTextMargin = new Thickness(28, 10, 28, 12);

                menuitem3.Tag = "Visualization 2";
                menuitem3.Text = "Visualization 2";
                menuitem3.Tapped += (c, d) =>
                {
                    visual.Visibility = Visibility.Collapsed;
                    visual = Visual2;
                    visual.Visibility = Visibility.Visible;
                    storyboard = Storyboard2;
                    storyboard.Begin();
                    storyboard.SpeedRatio = 0.2;
                    storyboard.Completed += storyboard_Completed;
                };
                menuitem3.MenuTextMargin = new Thickness(28, 10, 28, 12);

                menuitem4.Tag = "Total Media in List";
                menuitem4.Text = "Total Media";
                menuitem4.Tapped += async (j, f) =>
                    {
                        //mediaElement.RemoveAllEffects();
                        //  mediaElement.Balance =0;
                        // await new MessageDialog(mediaElement.Balance.ToString()).ShowAsync();
                        await new MessageDialog("Total Number Of Media's In Now Playing List is " + Collections.Count().ToString()).ShowAsync();
                        //var configuration = new PropertySet();
                        //configuration.Add(string.Format("eff{0}ect", "ARG0"),"Warp");
                        //mediaElement.AddVideoEffect("InvertTransform.InvertEffect", true, null);
                        ////mediaElement.RemoveAllEffects();
                        ////mediaElement.AddVideoEffect(Windows.Media.VideoEffects.VideoStabilization, true, null);


                    };
                menuitem4.MenuTextMargin = new Thickness(28, 10, 28, 12);


                menu.Items.Add(menuitem1);
                menu.Items.Add(menuitem5);
                menu.Items.Add(menuitem2);
                menu.Items.Add(menuitem3);
                menu.Items.Add(menuitem4);
                fly.HostMargin = new Thickness(0);
                fly.Content = menu;
                fly.IsOpen = true;

                UpdateLayout();
            }
            catch
            {
                ErrorCorrecting("021");
            }


            // UpdateLayout();
        }
        #endregion

        #region ListView Activities
        private void ls1_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            KeyboardControl();
        }

        private void ls1_KeyDown_1(object sender, KeyRoutedEventArgs e)
        {

            KeyboardControl();
            if (ls1.SelectedIndex == ls1.Items.Count - 1)
            {
                ls1.SelectedIndex = 0;
            }

        }
        #endregion
        Point _pos;
        public DispatcherTimer timer2 = new DispatcherTimer();
        #region Grid Pointer
        private void grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
            ringslice.Visibility = Visibility.Visible;
            SearchButton.Visibility = Visibility.Visible;
            button1.Visibility = Visibility.Visible;
            bt2.Visibility = Visibility.Visible;
            nextvideo.Visibility = Visibility.Visible;
            recentvideo.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            option.Visibility = Visibility.Visible;
            stackPanel.Visibility = Visibility.Visible;
            Subtitlebutton.Visibility = Visibility.Visible;
            volumea.Visibility = Visibility.Visible;
            Mutemedia.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            openfolder.Visibility = Visibility.Visible;
            FileSendButton.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Visible;
            _pos = e.GetCurrentPoint(this).Position;
            x = _pos.X - 50;
            y = _pos.Y - 50;


            timer2.Stop();
            timer2.Interval = new TimeSpan(0, 0, 0, 3);
            timer2.Start();
            timer2.Tick += timer2_Tick;
        }

        public void timer2_Tick(object sender, object e)
        {
            //ls1.Visibility = Visibility.Collapsed;
            //Rightbar.IsOpen = false;
            //Leftbar.IsOpen = false;
            //Bottombar.IsOpen = false;
            //Topbar.IsOpen = false;
            Window.Current.CoreWindow.PointerCursor = null;
            button4.Visibility = Visibility.Collapsed;
            button1.Visibility = Visibility.Collapsed;
            bt2.Visibility = Visibility.Collapsed;
            nextvideo.Visibility = Visibility.Collapsed;
            recentvideo.Visibility = Visibility.Collapsed;
            FileSendButton.Visibility = Visibility.Collapsed;
            button2.Visibility = Visibility.Collapsed;
            option.Visibility = Visibility.Collapsed;
            openfolder.Visibility = Visibility.Collapsed;
            stackPanel.Visibility = Visibility.Collapsed;
            Subtitlebutton.Visibility = Visibility.Collapsed;
            volumea.Visibility = Visibility.Collapsed;
            SearchButton.Visibility = Visibility.Collapsed;
            ringslice.Visibility = Visibility.Collapsed;
            Mutemedia.Visibility = Visibility.Collapsed;
            button3.Visibility = Visibility.Collapsed;
            timer2.Stop();
        }
        #endregion
        #region MediaStates

        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var a = new DispatcherTimer();
            a.Stop();
            a.Tick += (c, b) =>
            {
                nextvideo_Click(sender, e);
                a.Stop();
            };
            a.Interval = new TimeSpan(0, 0, 1);
            a.Start();
        }
        #endregion





        //private void keydowncontrol(object sender, KeyRoutedEventArgs e)
        //{
        //   if (e.Key == Windows.System.VirtualKey.Space)
        //    {

        //   mediaElement.Pause();
        //   }
        //}
        //}



        #region List
        private void Button2_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateLayout();
            if (Groupinggrid.Visibility == Visibility.Visible)
            {
                Groupinggrid.Visibility = Visibility.Collapsed;
            }
            if (Rightbar.IsOpen == false)
            {
                if (ls1.Items.Count > 0)
                {
                    ls1.IsActiveView = true;
                    Gridview1.IsActiveView = false;
                    Gridview1.IsActiveView = false;
                    ls1.Visibility = Visibility.Visible;
                    Rightbar.IsOpen = true;
                    Leftbar.IsOpen = true;
                    Bottombar.IsOpen = true;
                    Topbar.IsOpen = true;
                }
            }
            else
            {
                ls1.Visibility = Visibility.Collapsed;
                Rightbar.IsOpen = false;
                Leftbar.IsOpen = false;
                Bottombar.IsOpen = false;
                Topbar.IsOpen = false;
            }
        }
        #endregion
        public Slider Slide;
        #region Controls
        private void VolumeManager(object sender, RoutedEventArgs e)
        {

            var f = new Flyout();

            Border s = new Border
            {

                Height = 40,
                Width = 150,


            };

            Slide = new Slider
                    {
                        Minimum = 1,
                        Maximum = 100,
                        Value = mediaElement.Volume * 100,


                        Orientation = Orientation.Horizontal,

                    };

            s.Child = Slide;
            Slide.ValueChanged += Slide_ValueChanged;


            f.Content = s;

            f.Placement = PlacementMode.Bottom;
            f.PlacementTarget = sender as UIElement;

            f.IsOpen = true;


        }

        private void Slide_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // double volume = Slide.Value;
            mediaElement.Volume = Slide.Value / 100;
            //  Slide.Value = Slide.Value/100;

        }
        #endregion

        public StorageFile subfile;
        #region Subtitle
        private async void Subtitlebutton_OnClick(object sender, RoutedEventArgs e)
        {


            ApplicationViewState myViewState = ApplicationView.Value;

            if (myViewState == ApplicationViewState.Snapped)
            {
                Windows.UI.ViewManagement.ApplicationView.TryUnsnap();
            }

            //   FileOpenPicker openPicker = new FileOpenPicker();

            //   openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;


            //   openPicker.FileTypeFilter.Add(".srt");



            //   openPicker.ViewMode = PickerViewMode.List;

            //   mediaElement.Pause();

            //   var subs = await openPicker.PickSingleFileAsync();

            //var caps=new Caption();
            //  // caps.Source("ms-appx:///frt.srt");

            //   mediaElement.SelectedCaption = caps;
            var fop = new FileOpenPicker()
            {
                FileTypeFilter = { ".srt" }

            };
            //    fop.SuggestedStartLocation = PickerLocationId.TryParse(futurelocation, out true);
            subfile = await fop.PickSingleFileAsync();
            if (file != null)
            {
                SetSubs(subfile, mediaElement);
            }
            //  StorageFile filedd = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(futurelocation);
            //  mediaElement.SetSource((await filedd.OpenAsync(FileAccessMode.Read)), "");

        }
        async public void SetSubs(StorageFile file, MediaPlayer mediaaElement)
        {
            try
            {
                var text = await FileIO.ReadTextAsync(file);
                SubtitleCaption.ConvertSrtToTimlineMarker(text, mediaaElement);
            }
            catch
            {

                ErrorCorrecting("022");
            }

        }
        #endregion
        #region Video Effect
        private void threedvideopraba_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //mediaElement.Stereo3DVideoPackingMode = Stereo3DVideoPackingMode.SideBySide;
                //mediaElement.Stereo3DVideoRenderMode = Stereo3DVideoRenderMode.Stereo;
                //ActualStereo3DVideoPackingMode


                //Stereo3DVideoPackingMode

                //Stereo3DVideoRenderingMode

                //
                //
                //

            }
            catch (PlatformNotSupportedException)
            {
                //  new MessageDialog(ex.Message).ShowAsync();
            }

        }

        private async void Videostablepraba_Click(object sender, RoutedEventArgs e)
        {
            //    Setter a = new Setter(Button, true);



            if (Videostablepraba.Style == (Style)Application.Current.Resources["VideoAppBarButtonStyle"])
            {

                //    MessageDialog dd;
                //    dd = new MessageDialog(Videostablepraba.Style.GetValue(Button.NameProperty).);
                //    dd.ShowAsync();
                Videostablepraba.Style = (Style)Application.Current.Resources["deVideoAppBarButtonStyle"];
                mediaElement.RemoveAllEffects();
                mediaElement.AddVideoEffect(Windows.Media.VideoEffects.VideoStabilization, true, null);
                await new MessageDialog("Video Stabilizer is Switched On!!!Play Once Again").ShowAsync();


                mediaElement.Replay();
            }
            else
            {
                Videostablepraba.Style = (Style)Application.Current.Resources["VideoAppBarButtonStyle"];
                mediaElement.RemoveAllEffects();
                await new MessageDialog("Video Stabilizer is Switched Off!!!Play Once Again To Go to Normal").ShowAsync();


                mediaElement.Replay();
            }


        }

        private void Videolatent_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.RealTimePlayback = true;
            mediaElement.Visibility = Visibility.Visible;

        }




        private void Videooutside_Click(object sender, RoutedEventArgs e)
        {
            //mediaElement.Width =Window.Current.Bounds.Width;
            //mediaElement.Height = Window.Current.Bounds.Height;
            //grid.Height = Window.Current.Bounds.Height;
            //grid.Width = Window.Current.Bounds.Width;
            //     mediaElement.AutoLoadPlugins = true;

            //  SettingsPane.Show();
            // ptm.SourceRequested += sourceRequestHandler;
        }
        #endregion

        #region Controls
        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {

            SearchPane.Show();
        }
        #endregion
        ////   private void storyboard_Completed(object sender, object e)
        ////   {
        ////       storyboard.Begin();
        //////  new MessageDialog(mediaElement.Position.TotalSeconds.ToString()).ShowAsync();
        ////   //    new MessageDialog(mediaElement.Volume.ToString()).ShowAsync();

        ////   }



        ////private void MediaElement_OnPlayerStateChanged(object sender, RoutedPropertyChangedEventArgs<PlayerState> e)
        ////{
        ////      Random aa = new Random();
        ////      int ddd = aa.Next(1, 360);
        ////      ringslice.EndAngle = ddd;
        ////}/
        #region MediaStates

        private void MediaElement_OnTimeRemainingChanged(object sender, RoutedPropertyChangedEventArgs<TimeSpan> e)
        {


            Double totalduration = mediaElement.Duration.TotalSeconds;
            Double currentposition = mediaElement.Position.TotalSeconds;
            Double these = (currentposition / totalduration) * 100 * 3.6;
            Random aa = new Random();
            int ddd = aa.Next(1, 360);
            ringslice.EndAngle = these;


            var local = Windows.Storage.ApplicationData.Current.LocalSettings;
            local.Values["mediastatetime"] = currentposition;


        }

        #endregion
        #region Controls
        public void savestate(string state)
        {
            var local = Windows.Storage.ApplicationData.Current.LocalSettings;
            local.Values["mediastatefile"] = state;
        }
        #endregion
        public Double lastposition;
        #region OnLoad
        public async void onappload()
        {

            try
            {
                ProgressRing();




                //var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "base.db3");
                //using (SQLiteConnection db = new SQLiteConnection(path))
                //{
                //    db.BeginTransaction();
                //List<StorageFile> filedata = new List<StorageFile>();
                //var d = from x in db.Table<playlistbase>() select x;
                //foreach (var sd in d)
                //{
                //    StorageFile str = await
                //                      Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(
                //                          sd.playlistdata);
                //    filedata.Add(str);
                //}


                //file = filedata;
                file = await JDeSerialize();
                Collections.Clear();
                if (file != null)
                {

                    //var c = from x in file orderby x.Name select x;
                    int co = 0;
                    foreach (var files in file)
                    {
                       // albumart(files);

                        String duration = null;
                        String album = "Unknown";

                        int count = 0;
                        List<string> formats = new List<string>
                                {
                                    ".mp3",
                                    ".aac",
                                    ".adt",
                                    ".adts",
                                    ".wav",
                                    ".ac3",
                                    ".ec3"
                                };
                        var ff = from x in formats where x.Equals(files.FileType.ToLower()) select x;
                        foreach (var s in ff)
                        {
                            count = count + 1;
                        }
                        if (count >= 1)
                        {
                            var Properties = await files.Properties.GetMusicPropertiesAsync();
                            duration = Properties.Duration.ToString(@"hh\:mm\:ss");
                            album = Properties.Album == "" ? "Unknown Album" : Properties.Album;

                        }
                        else
                        {
                            var proper = await files.Properties.GetVideoPropertiesAsync();
                            duration = proper.Duration.ToString(@"hh\:mm\:ss");
                        }
                        playlist playingking = new playlist
                            {
                                _displayname = files.DisplayName,
                                _contenttype = files.ContentType,
                                _mediafile = files,
                                _datacreated = duration,
                                _album = album,
                                _thumburi = new Uri("ms-appdata:///roaming/" + files.DisplayName + ".png"),
                            };

                        Collections.Add(playingking);
                        co = co + 1;
                        block1.Text = co.ToString();

                    }
                    Task.Run(async () =>
                    {

                        foreach (var files in file)
                        {
                            albumart(files);
                        }
                       
                    });
                    ItemSource();
                    //db.Dispose();
                    //db.Close();
                    try
                    {
                        var local = ApplicationData.Current.LocalSettings;
                        object mediastatefile = local.Values["mediastatefile"];
                        object mediastatetime = local.Values["mediastatetime"];
                        lastposition = Convert.ToDouble(mediastatetime);
                        futurelocation = mediastatefile.ToString();
                        StorageFile filedd = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(mediastatefile.ToString());
                        if (filedd != null)
                        {
                            textBlock.Visibility = Visibility.Collapsed;
                            extensions = filedd.FileType.ToLower();
                            mediaElement.AutoPlay = false;
                            mediaElement.SetSource((await filedd.OpenAsync(FileAccessMode.Read)), "");
                        }
                        //Double minutes = Convert.ToDouble(mediastatetime.ToString())/60;
                        //Double seconds = Convert.ToDouble(mediastatetime.ToString())%60;
                        //TimelineMarker tlm=new TimelineMarker();
                        //  tlm.Time=new TimeSpan(0,0,0,30);
                        //  mediaElement.Markers.Add(tlm);
                        timer3.Interval = new TimeSpan(0, 0, 0, 1);
                        timer3.Start();
                        timer3.Tick += timer3_Tick;
                        // mediaElement.Position = TimeSpan.FromSeconds(lastposition);
                    }
                    catch
                    {
                    }

                    HideProgressGrid();
                }

                //}
            }
            catch
            {
                HideProgressGrid();
                //  ErrorCorrecting("023");
                // Debug.WriteLine("Error ->\n"+ex.Message);
                // new MessageDialog(ex.Message).ShowAsync();
            }
        }
        public DispatcherTimer timer3 = new DispatcherTimer();
        void timer3_Tick(object sender, object e)
        {

            mediaElement.Position = TimeSpan.FromSeconds(lastposition);
            mediaElement.Play();
            mediaElement.AutoPlay = true;
            timer3.Stop();
        }

        #endregion
        private Grid visual;
        #region MediaStates

        private void MediaElement_OnCurrentStateChanged(object sender, RoutedEventArgs e)
        {
            try
            {

                int count = 0;
                List<string> formats = new List<string>();
                formats.Add(".mp3");
                formats.Add(".aac");
                formats.Add(".adt");
                formats.Add(".adts");
                formats.Add(".wav");
                formats.Add(".ac3");
                formats.Add(".ec3");
                formats.Add(".wma");
                var ff = from x in formats where x.Equals(extensions.ToLower()) select x;
                foreach (var s in ff)
                {
                    count = count + 1;
                }
                if (count >= 1)
                {
                    grid.Background = new SolidColorBrush(Color.FromArgb(255, 124, 112, 112));

                    storyboard.Begin();
                    storyboard.Completed += storyboard_Completed;
                    storyboard.AutoReverse = true;
                    visual.Visibility = Visibility.Visible;
                    mediaElement.AutoHide = false;
                    //Start Avoid These It Affect BG Playback
                    //dispRequest.RequestRelease();
                    //End
                }
                else
                {
                    grid.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                    storyboard.Stop();
                    visual.Visibility = Visibility.Collapsed;
                    mediaElement.AutoHide = true;
                    dispRequest.RequestActive();
                }

                Caption.Text = "";
                mediaElement.Markers.Clear();

            }
            catch
            {

                ErrorCorrecting("024");
            }
        }

        private void MediaElement_OnSeekCompleted(object sender, RoutedEventArgs e)
        {
            Caption.Text = "";
        }
        #endregion
        #region Tiles
        public void updatetile()
        {

            #region LargeTile

            string til;

            if (ls1.SelectedItem != null)
            {
                object o = ls1.SelectedItem;
                playlist king = o as playlist;

                til = king.DisplayName;




                XmlDocument xml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideImageAndText01);
                XmlNodeList txt = xml.GetElementsByTagName("text");
                XmlNodeList img = xml.GetElementsByTagName("image");
                txt[0].InnerText = til;
                ((XmlElement)img[0]).SetAttribute("src", "ms-appx:///Assets/WideLogo.png");

            #endregion

                #region SmallTile

                XmlDocument tileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareBlock);
                //Then we grab a reference to the node we want to update.
                XmlNodeList textData = tileData.GetElementsByTagName("text");
                textData[0].InnerText = "Now";
                textData[1].InnerText = til;

                #endregion

                //#region WideImageCollection

                //XmlDocument wideimage = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideImageCollection);
                //XmlNodeList imgg = wideimage.GetElementsByTagName("image");
                //((XmlElement)imgg[0]).SetAttribute("src", "ms-appdata:///local/"+ls1.SelectedItem.ToString()+".png");
                //((XmlElement)imgg[1]).SetAttribute("src", "ms-appdata:///local/" + ls1.SelectedItem.ToString() + ".png");
                //((XmlElement)imgg[2]).SetAttribute("src", "ms-appdata:///local/" + ls1.SelectedItem.ToString() + ".png");
                //((XmlElement)imgg[3]).SetAttribute("src", "ms-appdata:///local/" + ls1.SelectedItem.ToString() + ".png");
                //((XmlElement)imgg[4]).SetAttribute("src", "ms-appdata:///local/" + ls1.SelectedItem.ToString() + ".png");
                //#endregion

                #region Mix Two Tile

                IXmlNode newNode = xml.ImportNode(tileData.GetElementsByTagName("binding").Item(0), true);
                // IXmlNode newNod=xml.ImportNode(wideimage.GetElementsByTagName("binding").Item(1),true);
                xml.GetElementsByTagName("visual").Item(0).AppendChild(newNode);
                //xml.GetElementsByTagName("visual").Item(1).AppendChild(newNod);
                //Then we create a TileNotification object with that data.

                TileNotification notification = new TileNotification(xml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
                notification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);
                //TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);



                //Int16 time = 5;
                //DateTime dateTime = DateTime.Now.AddSeconds(time);

                //ScheduledTileNotification scheduled = new ScheduledTileNotification(xml, dateTime);
                //TileUpdateManager.CreateTileUpdaterForApplication().AddToSchedule(scheduled);
                #endregion

                #region Badge
                BackgroundAccessStatus status = BackgroundExecutionManager.GetAccessStatus();

                if ((status == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity) ||
                    (status == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity))
                {
                    // badge.LoadXml(string.Format("<badge value={0}/>", ls1.Items.Count));
                    XmlDocument badge = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
                    XmlNodeList number = badge.GetElementsByTagName("badge");

                    badge.LoadXml(string.Format("<badge value=\"{0}\"/>", ls1.Items.Count));
                    ((XmlElement)number[0]).SetAttribute("value", "Playing");
                    BadgeNotification bdn = new BadgeNotification(badge);
                    BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(bdn);
                }

                #endregion

                //((XmlElement)smallTileImage[0]).SetAttribute("src", "ms-appx:///Assets/9-XAML-CatImageSmall.png");
            }
        }
        #endregion
        #region Controls
        private void Mutemedia_Click(object sender, RoutedEventArgs e)
        {
            if (Mutemedia.Style == (Style)Application.Current.Resources["MuteAppBarButtonStyle"])
            {
                mediaElement.IsMuted = true;
                Mutemedia.Style = (Style)Application.Current.Resources["UnMuteAppBarButtonStyle"];
            }
            else
            {
                mediaElement.IsMuted = false;
                Mutemedia.Style = (Style)Application.Current.Resources["MuteAppBarButtonStyle"];
            }
        }
        #endregion
        #region Open Media Files
        private async void Loadmediafromdevice(object sender, RoutedEventArgs e)
        {
            Flyout fly = new Flyout();
            fly.PlacementTarget = sender as UIElement;
            fly.Placement = PlacementMode.Top;
            fly.Closed += (x, y) =>
            {

            };
            ObservableCollection<selectingfiles> oc = new ObservableCollection<selectingfiles>();
            ListView a = new ListView();
            a.ItemTemplate = (DataTemplate)Application.Current.Resources["DeviceTemplate"];

            a.Width = 220;
            a.Height = 50;
            a.BorderBrush = new SolidColorBrush(Colors.BlueViolet);
            a.Background = new SolidColorBrush(Colors.Lavender);
            a.Foreground = new SolidColorBrush(Colors.Black);

            DeviceInformationCollection _device;
            _device = null;
            _device = await DeviceInformation.FindAllAsync(StorageDevice.GetDeviceSelector());
            oc.Clear();
            foreach (DeviceInformation dv in _device)
            {
                var dd = StorageDevice.FromId(dv.Id);
                selectingfiles g = new selectingfiles
                {

                    DeviceInfo = dv,
                    DeviceName = dd.DisplayName,
                };
                oc.Add(g);
            }
            if (oc.Count > 0)
            {
                a.Height = oc.Count * 50;
                a.ItemsSource = oc;
                fly.Content = a;
                fly.IsOpen = true;
                UpdateLayout();
            }
            else
            {
                fly.IsOpen = false;
                UpdateLayout();
                try
                {
                    await new MessageDialog("Removeable Devices Not Found !!!").ShowAsync();
                }
                catch (Exception)
                {


                }

            }

            //var removableStorages = await KnownFolders.RemovableDevices.GetFoldersAsync();

            //if (removableStorages.Count > 0)
            //{
            //    // Display each storage device
            //    foreach (StorageFolder storage in removableStorages)
            //    {
            //        a.Items.Add(storage.DisplayName);
            //    }
            //}





            a.SelectionChanged += async (object se, SelectionChangedEventArgs f) =>
            {

                object ff = a.SelectedItem;
                selectingfiles ss = ff as selectingfiles;

                fly.IsOpen = false;
                var storage = StorageDevice.FromId(ss.DeviceInfo.Id);
                //var storageName = deviceInfoElement.Name;

                // Construct the query for image files
                var QueryOptions = new QueryOptions
                    (
                    CommonFileQuery.OrderByName, new List<string> { ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".m4v", ".mp4v", ".mp4", ".mov", ".m2ts", ".asf", ".wm", ".vob", ".wmv", ".wma", ".aac", ".adt", ".mp3", ".wav", ".avi", ".ac3", ".ec3" }
                    );
                var FileQuery = storage.CreateFileQueryWithOptions(QueryOptions);

                // Run the query for image files

                var Files = await FileQuery.GetFilesAsync();
                file = Files;
                try
                {
                    await pickingfiles();
                }
                catch
                {

                    ErrorCorrecting("025");
                }
                ////if (Files.Count > 0)
                ////{
                ////    var imageFile = Files[0];

                ////}

            };

        }

        private async void openfolderasync(object sender, RoutedEventArgs e)
        {
            ApplicationViewState viewstate = new ApplicationViewState();
            viewstate = ApplicationView.Value;
            if (viewstate == ApplicationViewState.Snapped)
            {
                ApplicationView.TryUnsnap();
            }

            FolderPicker ff = new FolderPicker();

            var ls = new List<string>
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
            foreach (var s in ls)
            {
                ff.FileTypeFilter.Add(s);
            }

            ff.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            StorageFolder folder = await ff.PickSingleFolderAsync();
            if (folder != null)
            {


                var QueryOptions = new QueryOptions
                      (
                      CommonFileQuery.OrderByName, new List<string> { ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".m4v", ".mp4v", ".mp4", ".mov", ".m2ts", ".asf", ".wm", ".vob", ".wmv", ".wma", ".aac", ".adt", ".mp3", ".wav", ".avi", ".ac3", ".ec3" }
                      );
                var FileQuery = folder.CreateFileQueryWithOptions(QueryOptions);
                var ass = await FileQuery.GetFilesAsync();
                file = ass;
                try
                {
                    await pickingfiles();
                }
                catch
                {

                    ErrorCorrecting("026");
                }


            }
        }
        #endregion
        #region Send File
        private async void SendFileToStorage(object sender, RoutedEventArgs e)
        {
            Flyout fly = new Flyout();
            fly.PlacementTarget = sender as UIElement;
            fly.Placement = PlacementMode.Top;
            fly.Closed += (x, y) =>
            {

            };
            ObservableCollection<selectingfiles> oc = new ObservableCollection<selectingfiles>();
            ListView a = new ListView();
            a.ItemTemplate = (DataTemplate)Application.Current.Resources["DeviceTemplate"];

            a.Width = 220;
            a.Height = 50;
            a.BorderBrush = new SolidColorBrush(Colors.BlueViolet);
            a.Background = new SolidColorBrush(Colors.Lavender);
            a.Foreground = new SolidColorBrush(Colors.Black);

            DeviceInformationCollection _device;
            _device = null;
            _device = await DeviceInformation.FindAllAsync(StorageDevice.GetDeviceSelector());
            oc.Clear();
            foreach (DeviceInformation dv in _device)
            {
                var dd = StorageDevice.FromId(dv.Id);
                selectingfiles g = new selectingfiles
                {

                    DeviceInfo = dv,
                    DeviceName = dd.DisplayName,
                };
                oc.Add(g);
            }
            if (oc.Count > 0)
            {
                a.Height = oc.Count * 50;
                a.ItemsSource = oc;
                fly.Content = a;
                fly.IsOpen = true;
                UpdateLayout();
            }
            else
            {
                fly.IsOpen = false;
                UpdateLayout();
                try
                {
                    await new MessageDialog("Removable Devices Not Found !!!").ShowAsync();
                }
                catch (Exception)
                {


                }

            }

            //var removableStorages = await KnownFolders.RemovableDevices.GetFoldersAsync();

            //if (removableStorages.Count > 0)
            //{
            //    // Display each storage device
            //    foreach (StorageFolder storage in removableStorages)
            //    {
            //        a.Items.Add(storage.DisplayName);
            //    }
            //}



            a.SelectionChanged += async (object se, SelectionChangedEventArgs f) =>
            {

                object ff = a.SelectedItem;
                selectingfiles ss = ff as selectingfiles;
                object ls = ls1.SelectedItem;
                playlist pl = ls as playlist;


                fly.IsOpen = false;
                var storage = StorageDevice.FromId(ss.DeviceInfo.Id);

                if (ls1.SelectedItem != null)
                {

                    try
                    {
                        progressring.IsActive = true;

                        //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        //  {
                        //      await
                        //          pl.MediaFile.CopyAsync(storage, pl.MediaFile.Name,
                        //                                 NameCollisionOption.GenerateUniqueName);
                        //  });
                        //progressring.IsActive = false;


                        var s = pl.MediaFile.CopyAsync(storage, pl.MediaFile.Name,
                                                  NameCollisionOption.GenerateUniqueName).GetAwaiter();
                        //var dresult = s.GetAwaiter();

                        s.OnCompleted(async() =>
                        {
                           await new MessageDialog("File Sended Successfully").ShowAsync();
                            progressring.IsActive = false;
                        });

                    }
                    catch
                    {
                        ErrorCorrecting("027");
                    }

                }
                else
                {
                    new MessageDialog("First Select The Media in List!!!Then Send To Removable Device");
                }
            };


        }
        #endregion
        #region ListView Activities
        private async void Gridview1_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyboardControl();
            Caption.Text = "";
            //  var ggg = ls1.ItemsSource.GetType().GetElementType("DisplayName").ToString();
            object objectlist = Gridview1.SelectedItem;
            playlist playlistlist = objectlist as playlist;

            try
            {

                if (file.Count > 0)
                {
                    ls1.SelectedIndex = Gridview1.SelectedIndex;
                    //foreach (StorageFile files in file)
                    //{
                    //    if (files.DisplayName == playlistlist.DisplayName.ToString())
                    //    {
                    //String json = Newtonsoft.Json.JsonConvert.SerializeObject(playlistlist.MediaFile);
                    //StorageFile fil = (StorageFile)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    //MemoryStream ms=new MemoryStream();
                    //DataContractSerializer dataContractSerializer=new DataContractSerializer(typeof(StorageFile));
                    //dataContractSerializer.WriteObject(ms,playlistlist.MediaFile);
                    //  StorageFile fi;
                    //  fi = (StorageFile) dataContractSerializer.ReadObject(ms);

                    //StorageFile f=await StorageFile.GetFileFromPathAsync("String");
                    //await Dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
                    //    {
                    mediaElement.SetSource(
                        (await
                         playlistlist.MediaFile
                                     .OpenAsync(
                                         FileAccessMode
                                             .Read)),
                        "");
                    //});
                    MediaControl.TrackName = playlistlist.MediaFile.DisplayName;


                    futurelocation = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(playlistlist.MediaFile);

                    savestate(futurelocation);

                    extensions = playlistlist.MediaFile.FileType.ToLower();

                    mediaalbumart();
                    updatetile();

                    // MediaControl.AlbumArt=new Uri("ms-appx:///v.png");
                    //    }
                    //}
                }
            }
            catch
            {
                //new MessageDialog("028").ShowAsync();
            }
        }
        #endregion
        private void Combosearchbox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Combosearchbox != null)
                switch (Combosearchbox.SelectedIndex)
                {
                    case 0:
                        var results0 = from x in Collections.AsParallel().AsOrdered()
                                       group x by x.DisplayName.Substring(0, 1).ToUpper()
                                           into g
                                           orderby g.Key
                                           select new { Key = g.Key, Items = g };


                        //ls1.ItemsSource = Collections;
                        Cvs.Source = results0.AsParallel().AsOrdered();
                        var listViewBase0 = Sematiczoom1.ZoomedOutView as ListViewBase;

                        listViewBase0.ItemsSource = Cvs.View.CollectionGroups.AsParallel();

                        break;
                    case 1:
                        var results1 = from x in Collections.AsParallel().AsOrdered()
                                       group x by x.MediaFile.DateCreated.ToString(@"yyyy\/MM\/dd")
                                           into g
                                           orderby g.Key
                                           select new { Key = g.Key, Items = g };
                        //var lresult1 = from x in Collections
                        //               orderby x.MediaFile.DateCreated.ToString(@"yyyy\/MM\/dd")
                        //               select x;


                        //ls1.ItemsSource = lresult1;
                        Cvs.Source = results1.AsParallel().AsOrdered();
                        var listViewBase1 = Sematiczoom1.ZoomedOutView as ListViewBase;

                        listViewBase1.ItemsSource = Cvs.View.CollectionGroups.AsParallel();

                        break;
                    case 2:
                        var results2 = from x in Collections.AsParallel().AsOrdered()
                                       group x by x.Album
                                           into g
                                           orderby g.Key
                                           select new { Key = g.Key, Items = g };
                        //var lresult2 = from x in Collections
                        //               orderby x.Album
                        //               select x;


                        //ls1.ItemsSource = lresult2;
                        Cvs.Source = results2.AsParallel().AsOrdered();
                        var listViewBase2 = Sematiczoom1.ZoomedOutView as ListViewBase;

                        listViewBase2.ItemsSource = Cvs.View.CollectionGroups.AsParallel();
                        break;
                    case 3:
                        var results3 = from x in Collections.AsParallel().AsOrdered()
                                       group x by x.MediaFile.FileType
                                           into g
                                           orderby g.Key
                                           select new { Key = g.Key, Items = g };
                        //var lresult3 = from x in Collections
                        //               orderby x.MediaFile.FileType
                        //               select x;


                        //ls1.ItemsSource = lresult3;
                        Cvs.Source = results3.AsParallel().AsOrdered();
                        var listViewBase3 = Sematiczoom1.ZoomedOutView as ListViewBase;

                        listViewBase3.ItemsSource = Cvs.View.CollectionGroups.AsParallel().AsOrdered();
                        break;
                }
        }
        #region ListViewActivities
        private void Searchsongs_OnTextChanged(object sender, TextChangedEventArgs e)
        {

            control = false;
            timer5.Stop();
            timer5.Interval = new TimeSpan(0, 0, 0, 2);
            timer5.Start();
            timer5.Tick += timer5_Tick;

        }

        void timer5_Tick(object sender, object e)
        {
            control = true;
        }
        public DispatcherTimer timer5 = new DispatcherTimer();

        private async void Ls2_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (StorageFile files in file)
            {

                if (Searchsongs.SelectedItem != null && files.DisplayName == Searchsongs.SelectedItem.ToString())
                {

                    mediaElement.SetSource(await files.OpenAsync(FileAccessMode.Read), "");
                    //
                    medianame = files.DisplayName;

                    extensions = files.FileType.ToLower();


                    //
                    futurelocation =
                        Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(files);
                    savestate(futurelocation);
                    updatetile();
                }

            }
        }
        #endregion

        private async void MusicInfo(object sender, RoutedEventArgs e)
        {
            await DynamicSettings(new SettingsFlyLayout(), 346);
        }

        private void Searchsongs_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            KeyboardControl();
        }

        private void KeyboardControl()
        {
            control = false;
            // timer5.Stop();
            timer5.Interval = new TimeSpan(0, 0, 0, 1);
            timer5.Start();
            timer5.Tick += timer5_Tick;
        }

        private void Searchsongs_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            KeyboardControl();
        }

        private void Slider_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            slider.Visibility = Visibility.Visible;
            timer6.Stop();
            timer6.Interval = new TimeSpan(0, 0, 0, 2);
            timer6.Start();
            timer6.Tick += timer6_Tick;
        }
    }
}
//social.msdn.microsoft.com/Profile/prabaprakash
//Prabakaran.A 
//MS Software Engineering (2011-2016) 
//VIT University - Chennai Campus 
//Tamilnadu State 
//India - 600 048 
//www.facebook.com/prabakaran1993 