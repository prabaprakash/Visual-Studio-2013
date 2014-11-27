using System.Text;
using GalaSoft.MvvmLight;
using English.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;

namespace English.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
                RaisePropertyChanged(WelcomeTitlePropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }
                    WelcomeTitle = item.Title;
                }); 
        }
        public ICommand ButtonClick
        {
            get
            {
                return new RelayCommand(() => { 
                
                
                                //OpenFileDialog op = new OpenFileDialog();
                                //op.DefaultExt = ".txt";
                                //op.Filter = "Files|*.txt";
                                //op.Title = "Open xml file to read";
                                //var a = op.ShowDialog();
                                //if (a == true)
                                //{
                                //    FileStream fs = new FileStream(op.FileName, FileMode.Open, FileAccess.ReadWrite);
                                //    List<string> f = new List<string>();
                                //    using (StreamReader sr = new StreamReader(fs))
                                //    {
                                //        String dd = "";
                                //          while ((dd = sr.ReadLine()) != null)
                                //          {
                                //              var aa=
                                //                  (from x in f where x.Equals(dd) select x);
                                //              //WelcomeTitle += aa.Count();
                                //              if (!aa.Any())
                                //              {
                                //                  f.Add(dd);
                                //              }
                                //          }
                                       
                                //    }
                                //    String ss = "";
                                //    var ff=from y in f orderby y select y;
                                //    foreach (var s in ff)
                                //    {
                                //        ss+=s.ToLower()+"\n";
                                //    }
                                //    WelcomeTitle = ss;
                                  
                                //}



                    OpenFileDialog op = new OpenFileDialog();
                    op.DefaultExt = ".txt";
                    op.Filter = "Files|*.txt";
                    op.Title = "Open xml file to read";
                    var a = op.ShowDialog();
                    if (a == true)
                    {
                        FileStream fs = new FileStream(op.FileName, FileMode.Open, FileAccess.ReadWrite);
                        List<string> f = new List<string>();
                        using (StreamReader sr = new StreamReader(fs))
                        {
                         
                            String dd = "";
                            while ((dd = sr.ReadLine()) != null)
                            {
                               
                                var g = dd.Split('\t');

                                if (g[1] != "1")
                                {
                                    f.Add(dd);
                                }
                            }

                        }
                       StringBuilder sb=new StringBuilder();
                        foreach (var s in f)
                        {
                            sb.Append(s + "\n");
                        }
                        WelcomeTitle = sb.ToString();

                    }
                
                });
            }
        }
       
        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}