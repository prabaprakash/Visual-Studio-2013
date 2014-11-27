using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.PlayerFramework;

namespace Player.ViewModel
{
    public class MainViewModel : ViewModelBase, IFileOpenPickerContinuable
    {
        private string _helloWorld;
        MainPage rootPage = MainPage.Current;
        public string HelloWorld
        {
            get
            {
                return _helloWorld;
            }
            set
            {
                Set(() => HelloWorld, ref _helloWorld, value);
            }
        }

        public ICommand ButtonClick
        {
            get
            {
                return new RelayCommand(()=>
                {
                    add();
                
                });
            } set{}
            
        }
        public MainViewModel()
        {
            HelloWorld = IsInDesignMode
                ? "Runs in design mode"
                : "Runs in runtime mode";
        }
  
        async void add()
        {
            try
            {


                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.List;
                openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                openPicker.FileTypeFilter.Add("*");

                openPicker.PickMultipleFilesAndContinue();
              
                // Launch file open picker and caller app is suspended and may be terminated if required
                //openPicker.PickSingleFileAndContinue();


            }
            catch (Exception e)
            {
                new MessageDialog(e.Message + e.StackTrace).ShowAsync();
            }

        }

        public Uri MediaFile
        {
            get { return _uri; }
        }

        public Uri _uri;
        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            
         new MessageDialog("gfgr").ShowAsync();
            IReadOnlyList<StorageFile> files = args.Files;
            if (files.Count > 0)
            {
             
                foreach (StorageFile file in files)
                {
                    _uri=new Uri(file.Path);
                }
                
             
            }
            else
            {
              
            }
        }
    }
}
