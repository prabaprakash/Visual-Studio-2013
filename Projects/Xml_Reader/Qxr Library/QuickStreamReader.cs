using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using Trezorix.Qxr.Streamer.EventArguments;
using System.Text.RegularExpressions;

namespace Trezorix.Qxr.Streamer
{

	/// <summary>
	/// A Trezorix class for quickly reading large portions of XML files
	/// </summary>
	public class QuickStreamReader : IDisposable
	{

		#region [ Constructor ]

		/// <summary>
		/// Creates a new stream to the file and tries to read the file as an XML Formatted file
		/// </summary>
		/// <param name="Filename">Containing the full path to a XML file</param>
		public QuickStreamReader(string Filename)
		{
			m_lstXmlLines = new List<string>();
			m_sFilename = Filename;
		}

		#endregion

		#region [ Member declarations ]

		private bool m_bIsDisposed = false;
		private bool m_bIsDisposing = false;
		public List<string> m_lstXmlLines = null;
		private int m_iVisibleLines = 100;
		private string m_sFilename = null;
		private Thread m_thStartReading = null;
		private string m_sIndentation = "  ";
		private TimeSpan m_tsLoadDuration = TimeSpan.MinValue;
		private DateTime m_dtmStartReading = DateTime.Now;


		#region [ File Parsing Events ]

		/// <summary>
		/// Event handler for the start parsing event
		/// </summary>
		/// <param name="sender">Reference to the reader which sent the event</param>
		/// <param name="e">Empty event arguments</param>
		public delegate void StartParsingEventHandler(object sender, EventArgs e);
		/// <summary>
		/// Event handler for the end parsing event
		/// </summary>
		/// <param name="sender">Reference to the reader which sent the event</param>
		/// <param name="e">Empty event arguments</param>
		public delegate void EndParsingEventHandler(object sender, EventArgs e);

		public delegate void ErrorOccuredEventHandler(object sender, ErrorOccuredEventArgs e);

		internal delegate void LineAddedEventHandler(object sender, LineAddedEventArgs e);

		public delegate void ReadyForPresentationEventHandler(object sender, ReadyForPresentationEventArgs e);

		/// <summary>
		/// Raised when a file is opened and the quick reader starts opening the file for read
		/// </summary>
		/// <remarks>This event is asynchronous</remarks>
		public event StartParsingEventHandler StartParsing;
		/// <summary>
		/// Raised when the file is opened and read completely to the end.
		/// </summary>
		/// <remarks>
		/// If an error occurs while reading the file, this even will still be raised.
		/// This event is asynchronous
		/// </remarks>
		public event  EndParsingEventHandler EndParsing;

		public event ErrorOccuredEventHandler ErrorOccured;
		internal event LineAddedEventHandler LineAdded;

		public event ReadyForPresentationEventHandler ReadyForPresentation;



		/// <summary>
		/// Protected void raises the <see cref="StartParsing"/> event
		/// </summary>
		protected virtual void OnStartParsing()
		{
			if (StartParsing != null)
				StartParsing(this, EventArgs.Empty);
		}
		/// <summary>
		/// Protected void raises the <see cref="EndParsing"/> event
		/// </summary>
		protected virtual void OnEndParsing()
		{
			if (EndParsing != null)
				EndParsing(this, EventArgs.Empty);
		}

		protected virtual void OnErrorOccured(Exception ex)
		{
			if (ErrorOccured != null)
			{
				ErrorOccuredEventArgs evtError = new ErrorOccuredEventArgs(ex);
				ErrorOccured(this, evtError);
			}
		}

		protected virtual void OnLineAdded(string Line)
		{
			if (LineAdded != null)
			{
				LineAddedEventArgs evtLine = new LineAddedEventArgs(Line);
				LineAdded(this, evtLine);
			}
		}

		protected virtual void OnReadyForPresentation()
		{
			if (ReadyForPresentation != null)
			{
				ReadyForPresentationEventArgs evtPresentation = new ReadyForPresentationEventArgs(ReadFragment(0));
				ReadyForPresentation(this, evtPresentation);
			}
		}

		#endregion

		#endregion

		#region [ Property declarations ]

		public string Filename
		{
			get { return m_sFilename; }
		}

		internal int LineCount
		{
			get
			{
				int numberOfLines = 0;
				numberOfLines = m_lstXmlLines.Count;
				return numberOfLines;
			}
		}

		/// <summary>
		/// Gets or sets the amount of lines to display when scrolling new text in the viewport
		/// </summary>
		internal int VisibleLines
		{
			get { return m_iVisibleLines; }
			set { m_iVisibleLines = value; }
		}

		/// <summary>
		/// Gets wether the class is disposed or not
		/// </summary>
		public bool IsDisposed
		{
			get { return m_bIsDisposed; }
		}

		public bool IsDisposing
		{
			get { return m_bIsDisposing; }
		}

		internal string Indentation
		{
			get { return m_sIndentation; }
			set { m_sIndentation = value; }
		}

		public TimeSpan LoadDuration
		{
			get { return m_tsLoadDuration; }
		}


		#endregion

		#region [ Functions & Procedures ]

		#region [ Public's ]

		/// <summary>
		/// Reads a fragment of the XML file and returns is as a string
		/// </summary>
		/// <param name="Offset"></param>
		/// <returns></returns>
		public string ReadFragment(int Offset)
		{
			StringBuilder sbFragment = new StringBuilder();
			sbFragment.Append("{\\rtf1\\ansi\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 Courier New;}}\r\n{\\colortbl ;\\red150\\green0\\blue0;\\red100\\green100\\blue100;\\red0\\green0\\blue155;}\r\n\\viewkind4\\uc1\\pard\\lang1043\\f0\\fs20 ");

			int iReadLines = 0;
			for (int iCount = Offset; iCount < (Offset + m_iVisibleLines ); iCount++)
			{
				if (iCount >= m_lstXmlLines.Count)
					break;
				sbFragment.Append(m_lstXmlLines[iCount]);
				sbFragment.Append("\\line");
				sbFragment.Append(Environment.NewLine);
				iReadLines++;
			}
			sbFragment.Append("\r\n}\r\n");
			return sbFragment.ToString();
		}


		/// <summary>
		/// Starts reading the provided File (Filename)
		/// </summary>
		/// <remarks>
		/// This is an async process. The <see cref="StartParsing"/>, <see cref="DisplayParsed"/>
		/// and <see cref="EndParsing"/> events will notify you about the reading progress.
		/// </remarks>
		public void StartReadingDocument()
		{
			m_dtmStartReading = DateTime.Now;
			if ((!m_bIsDisposing) && (!m_bIsDisposed))
			{
				ThreadStart tsReadDocument = ReadXmlContent;
				m_thStartReading = new Thread(tsReadDocument);
				m_thStartReading.Name = "Thread_ReadDocument";
				m_thStartReading.Priority = ThreadPriority.AboveNormal;
				m_thStartReading.Start();
			}
		}


		#endregion

		#region [ Private's ]

		private void ReadXmlContent()
		{
			OnStartParsing();
			try
			{
				bool switchText = false;
				using (FileStream m_fsStream = new FileStream(m_sFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (XmlReader xrdr = XmlReader.Create(m_fsStream))
					{
						StringBuilder sbMarkedUp = new StringBuilder();
						xrdr.MoveToElement();
						while (xrdr.Read())
						{

							if ((m_bIsDisposing) || (m_bIsDisposed))
								break;

							switch (xrdr.NodeType)
							{
								case XmlNodeType.XmlDeclaration:
								case XmlNodeType.Element:
									bool emptyElement = xrdr.IsEmptyElement;
									if (sbMarkedUp.Length > 0)
										AddLineAndClearString(ref sbMarkedUp);
										Indent(ref sbMarkedUp, xrdr.Depth);
									sbMarkedUp.Append("\\cf1 <");
									sbMarkedUp.Append( xrdr.Name);
									if (xrdr.HasAttributes)
									{
										xrdr.MoveToFirstAttribute();
										sbMarkedUp.Append(" \\cf2 ");
										sbMarkedUp.Append(xrdr.Name);
										sbMarkedUp.Append("=\"\\cf3 ");
										sbMarkedUp.Append(EscapeRTF(xrdr.Value));
										sbMarkedUp.Append("\\cf2\"");
										while (xrdr.MoveToNextAttribute())
										{
											sbMarkedUp.Append(" ");
											sbMarkedUp.Append(xrdr.Name);
											sbMarkedUp.Append("=\"\\cf3 ");
											sbMarkedUp.Append(EscapeRTF(xrdr.Value));
											sbMarkedUp.Append("\\cf2\"");
										}
									}

									if (emptyElement)
									{
										sbMarkedUp.Append("\\cf1 />\\cf0 ");
										AddLineAndClearString(ref sbMarkedUp);
										switchText = false;
									}
									else
									{
										sbMarkedUp.Append("\\cf1>\\cf0 ");
										switchText = true;
									}
									break;
								case XmlNodeType.Attribute:
									sbMarkedUp.Append(" ");
									sbMarkedUp.Append(xrdr.Name);
									sbMarkedUp.Append("=\"");
									sbMarkedUp.Append(xrdr.Value);
									sbMarkedUp.Append("\"");
									break;
								case XmlNodeType.Text:
									sbMarkedUp.Append(EscapeRTF(xrdr.Value.Replace("\r\n", string.Empty)));
									break;
								case XmlNodeType.EndElement:

									if (!switchText)
									{
										Indent(ref sbMarkedUp, xrdr.Depth);
									}
									sbMarkedUp.Append("\\cf1 </");
									sbMarkedUp.Append(xrdr.Name);
									sbMarkedUp.Append("> \\cf0");
									AddLineAndClearString(ref sbMarkedUp);
									switchText = false;
									break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				OnErrorOccured(ex);
			}
			m_tsLoadDuration = DateTime.Now - m_dtmStartReading;
			OnEndParsing();
		}

		private void AddLineAndClearString(ref StringBuilder XmlLine)
		{
			AddLine(XmlLine.ToString());
			XmlLine.Clear();
		}
		private void AddLine(string Line)
		{
			m_lstXmlLines.Add(Line.ToString());
			OnLineAdded(Line);
			if (m_lstXmlLines.Count == 10000)
				OnReadyForPresentation();
		}

		private void Indent(ref StringBuilder XmlLine, int Depth)
		{
			for (int iDepthCount = 0; iDepthCount < Depth; iDepthCount++)
				XmlLine.Append(m_sIndentation);
		}
		public string EscapeRTF(string Input)
		{
			string sResult = Regex.Replace(Input, "\\\\", "\\'5c", RegexOptions.Compiled);
			sResult = Regex.Replace(sResult, "\\{", "\\'7b", RegexOptions.Compiled);
			sResult = Regex.Replace(sResult, "\\}", "\\'7d", RegexOptions.Compiled);
			return sResult;
		}



		#endregion

		#endregion

		#region [ Finalizers ]

		/// <summary>
		/// Cleans up the quick reader resources
		/// </summary>
		~QuickStreamReader()
		{
			Dispose(false);
		}

		/// <summary>
		/// Disposes the quick reader object
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void UnsubscribeEvents()
		{
			if (StartParsing != null)
			{
				foreach (StartParsingEventHandler eventDelegate in StartParsing.GetInvocationList())
					StartParsing -= eventDelegate;
			}

			if (EndParsing != null)
			{
				foreach (EndParsingEventHandler eventDelegate in EndParsing.GetInvocationList())
					EndParsing -= eventDelegate;
			}

			StartParsing = null;
			EndParsing = null;

		}

		private void Dispose(bool Disposing)
		{
			m_bIsDisposing = true;
			if ((Disposing) && (!m_bIsDisposed))
			{
				// Make running threads exit
				UnsubscribeEvents();

				if (m_lstXmlLines != null)
					m_lstXmlLines.Clear();
				m_lstXmlLines = null;

				m_sFilename=null;
				m_sIndentation = null;

				if (m_thStartReading.IsAlive)
				{
					try
					{
						m_thStartReading.Abort();
					}
					catch (ThreadAbortException)
					{
						// Expected exception
					}
				}

			}
			// Dispose unmanaged resources here
			m_bIsDisposed = true;
			m_bIsDisposing = false;
		}

		#endregion

	}
}

