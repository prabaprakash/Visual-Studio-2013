using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Xml;
using System.Text;
using System;
using Trezorix.Qxr.Streamer;
using Trezorix.Qxr.Streamer.EventArguments;

namespace Xml_Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rt1.MouseWheel += rt1_MouseWheel;
            //Form1 a = new Form1();
            //a.Show();
            //this.Hide();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.DefaultExt = ".xml";
            op.Filter = "Files|*.xml";
            op.Title = "Open xml file to read";
            var a = op.ShowDialog();
            if (a == true)
            {
                loading(op.FileName);
            }
        }
        public QuickXmlReader m_Stream = null;
        public void loading(String m_sDocumentFilename)
        {
            if (m_Stream != null)
                m_Stream.Dispose();
            m_Stream = new QuickXmlReader(m_sDocumentFilename);
            m_Stream.Reader.ErrorOccured += new QuickStreamReader.ErrorOccuredEventHandler(Reader_ErrorOccured);
            m_Stream.Reader.EndParsing += new QuickStreamReader.EndParsingEventHandler(Reader_EndParsing);
            m_Stream.Reader.ReadyForPresentation += new QuickStreamReader.ReadyForPresentationEventHandler(Reader_ReadyForPresentation);
            m_Stream.Reader.StartReadingDocument();

           
            // MessageBox.Show("ff")
        }

        public void Register()
        {
            m_Stream.Search.FoundItem += Search_FoundItem;
            m_Stream.Search.SearchComplete += Search_SearchComplete;
        }

        public void UnRegister()
        {
            m_Stream.Search.FoundItem -= Search_FoundItem;
            m_Stream.Search.SearchComplete -= Search_SearchComplete;
        }
        private void Reader_ReadyForPresentation(object sender, ReadyForPresentationEventArgs e)
        {
            // throw new NotImplementedException();


        }

        private void Reader_EndParsing(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            try
            {
                // MessageBox.Show(m_Stream.ReadRtfPortion(100));
                // rt1.AppendText(m_Stream.ReadRtfPortion(10));
                //var stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(m_Stream.ReadRtfPortion(100)));
                //stream.Position = 0;
                //rt1.SelectAll();
                //rt1.Selection.Load(stream, DataFormats.Rtf);
                // FlowDocument workDoc = new FlowDocument();
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                {
                    TextRange range = new TextRange(rt1.Document.ContentStart, rt1.Document.ContentEnd);
                    using (var fStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(m_Stream.ReadRtfPortion(0))))
                    {
                        range.Load(fStream, System.Windows.DataFormats.Rtf);
                        //fStream.Close();
                    } documentScroll.Maximum = m_Stream.LineCount;
                    //range.ClearAllProperties();
                });
                //ScrollViewer1.ScrollChanged += ScrollViewer1_ScrollChanged;

                //

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace + exception.TargetSite + exception.Message);
            }
        }

        private void Reader_ErrorOccured(object sender, ErrorOccuredEventArgs e)
        {
            //  throw new NotImplementedException();
        }

        private void rt1_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (documentScroll.IsEnabled)
            {
                int step = -(e.Delta / 10);

                if ((step + documentScroll.Value <= documentScroll.Maximum) && (step + documentScroll.Value >= documentScroll.Minimum))
                    documentScroll.Value += step;
                else
                {
                    if (step + documentScroll.Value > documentScroll.Maximum)
                        documentScroll.Value = documentScroll.Maximum;
                    if (step + documentScroll.Value < documentScroll.Minimum)
                        documentScroll.Value = documentScroll.Minimum;
                }
            }

        }

        private void Documentscroll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            int LineNumber = (int)documentScroll.Value;
            if (m_Stream != null)
            {
                documentScroll.Maximum = m_Stream.LineCount - m_Stream.VisibleLines;
                TextBlock1.Text = "Line " + LineNumber + " of " + m_Stream.LineCount;
                try
                {
                    //rt1.Document.Blocks.Clear();

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        using (var stream =
                            new MemoryStream(ASCIIEncoding.Default.GetBytes(m_Stream.ReadRtfPortion(LineNumber))))
                        {
                            stream.Position = 0;
                            rt1.SelectAll();
                            rt1.Selection.Load(stream, DataFormats.Rtf);
                        }
                    }), DispatcherPriority.Loaded);
                    GC.SuppressFinalize(this);
                    //TextRange range = new TextRange(rt1.Document.ContentStart, rt1.Document.ContentEnd);
                    //this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                    //{

                    //    using (
                    //        var fStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(m_Stream.ReadRtfPortion(LineNumber))))
                    //    {
                    //        range.Load(fStream, System.Windows.DataFormats.Rtf);
                    //        fStream.Close();
                    //    }
                    //});

                    //range.ClearAllProperties();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.StackTrace + exception.TargetSite + exception.Message);
                }
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = ls1.SelectedItem as lists;
            documentScroll.Value = a.Line;
        }


        public class lists
        {
            public String Word { get; set; }
            public int Line { get; set; }
        }
        List<lists> ls = new List<lists>();
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        void Search_SearchComplete(object sender, EventArgs e)
        {
            try
            {
                //ls.Add(new lists() { Word = "fd", Line = 1 });
                //ls.Add(new lists() { Word = "fd", Line = 1 });
                if (ls != null)
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //m_Stream.Search.StopSearching();
                        ls1.ItemsSource = ls;
                        
                    }), DispatcherPriority.Normal);
             
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void Search_FoundItem(object sender, FoundItemEventArgs e)
        {
            try
            {
                ls.Add(new lists() { Line = e.Line, Word = e.Text });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
           
  //  MessageBox.Show(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBox1.Text != null)
                {
                    if (m_Stream != null)
                    {
                       
                            ls.Clear();
                            UnRegister();
                            Register();
                            m_Stream.SearchPhrase(TextBox1.Text);
                       
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
