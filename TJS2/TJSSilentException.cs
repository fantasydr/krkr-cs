/*
 * TJS2 CSharp
 */

using System;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class TJSSilentException : Exception
	{
		private const long serialVersionUID = 51839351639123183L;

		public TJSSilentException()
		{
		}

		public TJSSilentException(string msg) : base(msg)
		{
		}
	}
}
