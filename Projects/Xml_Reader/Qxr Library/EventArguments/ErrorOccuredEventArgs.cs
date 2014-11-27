using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trezorix.Qxr.Streamer.EventArguments
{
	public class ErrorOccuredEventArgs : System.EventArgs
	{

		internal ErrorOccuredEventArgs(Exception ex)
		{
			m_Exception = ex;
		}

		private Exception m_Exception = null;

		public Exception Exception
		{
			get { return Exception; }
		}

	}
}
