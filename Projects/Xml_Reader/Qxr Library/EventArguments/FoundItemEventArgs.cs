using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trezorix.Qxr.Streamer.EventArguments
{

	/// <summary>
	/// Event arguments containing information about the found item
	/// </summary>
	public class FoundItemEventArgs : System.EventArgs
	{

		internal FoundItemEventArgs(string Text, int Line)
		{
			m_sText = Text;
			m_iLine = Line;
		}

		private string m_sText = null;
		private int m_iLine = -1;

		/// <summary>
		/// Gets a portion of the text where a match was found
		/// </summary>
		public string Text
		{
			get { return m_sText; }
		}

		/// <summary>
		/// Gets the line number where the text was found
		/// </summary>
		public int Line
		{
			get { return m_iLine; }
		}

	}
}
