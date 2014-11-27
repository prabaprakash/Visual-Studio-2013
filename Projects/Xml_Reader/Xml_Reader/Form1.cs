using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using Trezorix.Qxr.Streamer;
using Trezorix.Qxr.Streamer.EventArguments;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Xml_Reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            rt1.MouseWheel += rt1_MouseWheel;
        }

        void rt1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (documentScroll.Enabled)
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

      
        public QuickXmlReader m_Stream = null;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.OpenFileDialog op = new OpenFileDialog();
            op.DefaultExt = ".xml";
            op.Filter = "Files|*.xml";
            op.Title = "Open xml file to read";
            var a = op.ShowDialog();
            if (a == true)
            {
                loading(op.FileName);
            }
        }
        public void loading(String m_sDocumentFilename)
        {
            if (m_Stream != null)
                m_Stream.Dispose();




            m_Stream = new QuickXmlReader(m_sDocumentFilename);

            m_Stream.Reader.ErrorOccured += new QuickStreamReader.ErrorOccuredEventHandler(Reader_ErrorOccured);
            m_Stream.Reader.EndParsing += new QuickStreamReader.EndParsingEventHandler(Reader_EndParsing);
            m_Stream.Reader.ReadyForPresentation += new QuickStreamReader.ReadyForPresentationEventHandler(Reader_ReadyForPresentation);
            m_Stream.Reader.StartReadingDocument();

            // MessageBox.Show("ff");
        }

        private void Reader_ReadyForPresentation(object sender, ReadyForPresentationEventArgs e)
        {

            // throw new NotImplementedException();
            documentScroll.Maximum = m_Stream.LineCount;
            // System.Windows.MessageBox.Show("ff");
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
                rt1.Rtf=m_Stream.ReadRtfPortion(0);
                //rt1.SelectAll();
                //rt1.Selection.Load(stream, DataFormats.Rtf);
                // FlowDocument workDoc = new FlowDocument();
                // TextRange range = new TextRange(rt1.Document.ContentStart, rt1.Document.ContentEnd);
                //  using (var fStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(m_Stream.ReadRtfPortion(100))))
                // {
                // range.Load(fStream, System.Windows.DataFormats.Rtf);
                //fStream.Close();
                // }

                //range.ClearAllProperties();


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace);
            }
        }

        private void Reader_ErrorOccured(object sender, ErrorOccuredEventArgs e)
        {
            //  throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += (a,b) => Application.Exit();
            this.Closing += (a, b) => Application.Exit();
            //MessageBox.Show("fff");
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            ScrollToLine(documentScroll.Value);
        }

        private void ScrollToLine(int LineNumber)
        {
            if (m_Stream != null)
            {
                documentScroll.Maximum = m_Stream.LineCount - m_Stream.VisibleLines;
                label1.Text = "Line " + LineNumber + " of " + m_Stream.LineCount;
                rt1.Rtf = m_Stream.ReadRtfPortion(LineNumber);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void rt1_TextChanged(object sender, EventArgs e)
        {

        }




    }
    
}
