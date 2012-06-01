/*
 * The TJS2 interpreter from kirikirij
 */

using System;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class TJSException : Exception
	{
		private const long serialVersionUID = 1942890050230766470L;

		public TJSException()
		{
		}

		public TJSException(string msg) : base(msg)
		{
		}
	}
}
