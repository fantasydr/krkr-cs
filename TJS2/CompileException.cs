/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class CompileException : TJSScriptError
	{
		/// <summary>TODO 一度 CompileException を呼んでいるところを见直した方が良い</summary>
		private const long serialVersionUID = 560827963479780060L;

		public CompileException(string msg) : base(msg, null, 0)
		{
		}

		public CompileException(string msg, SourceCodeAccessor accessor, int pos) : base(
			msg, accessor, pos)
		{
		}
	}
}
