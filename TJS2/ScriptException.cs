/*
 * TJS2 CSharp
 */

using System;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class ScriptException : Exception
	{
		private const long serialVersionUID = 61567993834917176L;

		public ScriptException()
		{
		}

		public ScriptException(string msg) : base(msg)
		{
		}
	}
}
