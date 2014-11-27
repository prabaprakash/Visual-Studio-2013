using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trezorix.Qxr.Streamer.EventArguments;
using System.Threading;

namespace Trezorix.Qxr.Streamer
{
	public class QuickXmlSearch : IDisposable
	{

		#region [ Constructor ]

		public QuickXmlSearch()
		{
		}

		#endregion

		#region [ Member declarations ]

		private bool m_bIsDisposed = false;
		private bool m_bIsDisposing = false;
		private bool m_bIsSearching = false;

		private string m_sSearchString = null;
		private Thread m_thSearching = null;
		private List<string> m_lstLinesToSearch = null;
		private Dictionary<int, string> m_SearchResults = null;

		#endregion

		#region [ Event declaration ]

		/// <summary>
		/// Event handler for the found item event
		/// </summary>
		/// <param name="sender">Reference to the reader which sent the event</param>
		/// <param name="e">Containing information about the found item</param>
		public delegate void FoundItemEventHandler(object sender, FoundItemEventArgs e);
		/// <summary>
		/// Event handler for the search complete event
		/// </summary>
		/// <param name="sender">Reference to the reader which sent the event</param>
		/// <param name="e">Empty event arguments</param>
		public delegate void SearchCompleteEventHandler(object sender, EventArgs e);

		/// <summary>
		/// Raised during the Search process when a match is found
		/// </summary>
		/// <remarks>This event is asynchronous</remarks>
		public event FoundItemEventHandler FoundItem;
		/// <summary>
		/// Raised after the search process completed
		/// </summary>
		/// <remarks>This event is asynchronous</remarks>
		public event SearchCompleteEventHandler SearchComplete;

		/// <summary>
		/// Protected void raises the <see cref="FoundItem"/> event
		/// </summary>
		/// <param name="Text">Containing the text portion of the found match</param>
		/// <param name="Line">Containing the line number where the text was found</param>
		protected virtual void OnFoundItem(string Text, int Line)
		{
			if (FoundItem != null)
			{
				FoundItemEventArgs evtFound = new FoundItemEventArgs(Text, Line);
				FoundItem(this, evtFound);
			}
		}
		/// <summary>
		/// Protected void raises the <see cref="SearchComplete"/> event
		/// </summary>
		protected virtual void OnSearchComplete()
		{
			if (SearchComplete != null)
			{
				SearchComplete(this, EventArgs.Empty);
			}
		}

		#endregion

		#region [ Property Assignments ]

		public Dictionary<int, string> SearchResults
		{
			get
			{
				if (m_SearchResults == null)
					m_SearchResults = new Dictionary<int, string>();
				return m_SearchResults;
			}
		}
		public string SearchPhrase
		{
			get { return m_sSearchString; }
		}
		public bool IsDisposed
		{
			get { return m_bIsDisposed; }
		}
		public bool IsDisposing
		{
			get { return m_bIsDisposing; }
		}
		public bool IsSearching
		{
			get { return m_bIsSearching; }
		}

		#endregion

		#region [ Functions & Procedures ]


		/// <summary>
		/// Starts a new asynchronous event searching for the string provided.
		/// </summary>
		/// <param name="Text">Containing the string to search for.</param>
		/// <remarks>
		/// No wildcards are supported for searching. The seach process is asynchronous. The <see cref="FoundItem"/>
		/// event will be raised if a match is found, and the <see cref="SearchComplete"/> event will be raised as soon
		/// as the search process completes.
		/// </remarks>
		public void StartSearching(string Text, ref List<string> XmlLines)
		{
           
			m_lstLinesToSearch = XmlLines;
			m_sSearchString = Text;
			if (m_SearchResults == null)
				m_SearchResults = new Dictionary< int, string>();
			m_SearchResults.Clear();

			if (!string.IsNullOrEmpty(Text) && (XmlLines != null))
			{

				if (m_bIsSearching)
					StopSearching();
				m_bIsSearching = true;

				ThreadStart tsSearch = StartSearching;
				m_thSearching = new Thread(tsSearch);
				m_thSearching.Name = "Thread_SearchXml";
				m_thSearching.Priority = ThreadPriority.Normal;
				m_thSearching.Start();

			}
		}

		public void StopSearching()
		{
			if (m_thSearching.IsAlive)
			{
				try
				{
					m_thSearching.Abort();
				}
				catch (ThreadAbortException)
				{
					// We may expect this exception, do nothing
				}
			}
		}

		private void StartSearching()
		{
			int iLines = m_lstLinesToSearch.Count;

			for (int iCount = 0; iCount < iLines; iCount++)
			{
				if (m_bIsDisposed || m_bIsDisposing)
					break;

				string stringToSearch = m_lstLinesToSearch[iCount];

				int foundIndex = stringToSearch.IndexOf(m_sSearchString, StringComparison.OrdinalIgnoreCase);
				if (foundIndex >= 0)
				{

					stringToSearch = stringToSearch.Replace("\"", " ");
					stringToSearch = stringToSearch.Replace("<", " ");
					stringToSearch = stringToSearch.Replace(">", " ");

					// Get the last and first ocurrence of a white space character before the word match
					int phraseStartIndex = GetPhraseStartIndex(stringToSearch, " ", foundIndex);
					int phraseEndIndex = GetPhraseEndIndex(stringToSearch, "",  foundIndex);

					string foundPhrase = stringToSearch.Substring(phraseStartIndex, phraseEndIndex - phraseStartIndex);
					foundPhrase = foundPhrase.Replace("\\cf0", string.Empty).Replace("\\cf1", string.Empty).Replace("\\cf2", string.Empty).Replace("\\cf3", string.Empty).Trim();
					AddSearchResult(foundPhrase, iCount + 1);
				}
			}
			OnSearchComplete();
			m_bIsSearching = false;
		}

		private int GetPhraseStartIndex(string Source, string Find, int FoundIndex)
		{
			return Source.LastIndexOf(Find, FoundIndex, FoundIndex) + 1;
		}
		private int GetPhraseEndIndex(string Source, string Find, int FoundIndex)
		{
			return Source.IndexOf(Find, FoundIndex + m_sSearchString.Length, Source.Length - (FoundIndex + m_sSearchString.Length));
		}
		private void AddSearchResult(string FoundPhrase, int LineNumber)
		{
			m_SearchResults.Add(LineNumber, FoundPhrase);
			OnFoundItem(FoundPhrase, LineNumber);
		}

		#endregion

		#region [ Finalizers ]

		~QuickXmlSearch()
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

				if (FoundItem != null)
				{
					foreach (FoundItemEventHandler eventDelegate in FoundItem.GetInvocationList())
						FoundItem -= eventDelegate;
				}

				if (SearchComplete != null)
				{
					foreach (SearchCompleteEventHandler eventDelegate in SearchComplete.GetInvocationList())
						SearchComplete -= eventDelegate;
				}

				FoundItem = null;
				SearchComplete = null;


				m_sSearchString = null;
				if (m_SearchResults != null)
					m_SearchResults.Clear();
				m_SearchResults = null;
			}
			// Dispose unmanaged resources here
			m_bIsDisposed = false;
			m_bIsDisposing = false;
		}

		#endregion

	}
}