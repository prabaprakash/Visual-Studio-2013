using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Trezorix.Qxr.Streamer
{
	public class QuickXmlReader : IDisposable
	{

		#region [ Constructor ]

		public QuickXmlReader()
		{
			Initialize();
		}
		public QuickXmlReader(string Filename)
		{
			m_sFilename = Filename;
			Initialize();
		}

		#endregion

		#region [ Member declarations ]

		private string m_sFilename = null;

		private bool m_bIsDisposing = false;
		private bool m_bIsDisposed = false;

		private QuickStreamReader m_Reader = null;
		private QuickXmlSearch m_Search = null;

		#endregion

		#region [ Property declarations ]

		/// <summary>
		/// Gets or sets the filename of the XML file to be opened
		/// </summary>
		public string Filename
		{
			get { return m_sFilename; }
			set
			{
				m_sFilename = value;
				InitializeReader();
			}
		}

		public int VisibleLines
		{
			get { return m_Reader.VisibleLines; }
			set
			{
				if (value > 10)
				m_Reader.VisibleLines = value;
			}
		}

		public int LineCount
		{
			get { return m_Reader.LineCount; }
		}

		public string Indentation
		{
			get { return m_Reader.Indentation; }
			set { m_Reader.Indentation = value; }
		}

		/// <summary>
		/// Gets wether the class is disposed or not
		/// </summary>
		public bool IsDisposed
		{
			get { return m_bIsDisposed; }
		}

		public QuickStreamReader Reader
		{
			get { return m_Reader; }
		}

		public QuickXmlSearch Search
		{
			get { return m_Search; }
		}

		#endregion

		#region [ Functions & Procedures ]

		#region [ Initializing ]

		private void Initialize()
		{
			InitializeReader();
			InitializeSearch();
		}

		private void InitializeReader()
		{
			if (!string.IsNullOrEmpty(m_sFilename) && File.Exists(m_sFilename))
			{
				if (m_Reader != null)
					m_Reader.Dispose();

				m_Reader = new QuickStreamReader(m_sFilename);
				m_Reader.StartParsing += new QuickStreamReader.StartParsingEventHandler(m_Reader_StartParsing);
				m_Reader.EndParsing += new QuickStreamReader.EndParsingEventHandler(m_Reader_EndParsing);
				m_Reader.ErrorOccured += new QuickStreamReader.ErrorOccuredEventHandler(m_Reader_ErrorOccured);
				m_Reader.LineAdded += new QuickStreamReader.LineAddedEventHandler(m_Reader_LineAdded);
			}
		}

		private void InitializeSearch()
		{
			if (m_Search != null)
				m_Search.Dispose();

			m_Search = new QuickXmlSearch();
		}

		#endregion

		public string ReadRtfPortion(int Offset)
		{
			return m_Reader.ReadFragment(Offset);
		}

		public void SearchPhrase(string Phrase)
		{
			m_Search.StartSearching(Phrase, ref m_Reader.m_lstXmlLines);
		}

		#region [ Xml Reader Events ]

		private void m_Reader_StartParsing(object sender, EventArgs e)
		{
		}
		private void m_Reader_LineAdded(object sender, EventArguments.LineAddedEventArgs e)
		{
		}
		private void m_Reader_ErrorOccured(object sender, EventArguments.ErrorOccuredEventArgs e)
		{
		}
		private void m_Reader_EndParsing(object sender, EventArgs e)
		{
		}

		#endregion

		#endregion

		#region [ Finalizers ]

		~QuickXmlReader()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool Disposing)
		{
			m_bIsDisposing = true;
			if ((Disposing) && (!m_bIsDisposed))
			{
				m_Reader.Dispose();
				m_Search.Dispose();
			}
			// Dispose unmanaged resources here
			m_bIsDisposed = true;
			m_bIsDisposing = false;
		}

		#endregion

	}
}