using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trezorix.Qxr.Streamer.EventArguments
{
	internal class LineAddedEventArgs : System.EventArgs
	{

		internal LineAddedEventArgs(string Line)
		{
			m_sLine = Line;
		}

		private string m_sLine = null;

		public string Line
		{
			get { return m_sLine; }
		}

	}
}
