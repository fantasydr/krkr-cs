/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class CompileException : TJSScriptError
	{
		/// <summary>TODO ä¸€åº¦ CompileException ã‚’å‘¼ã‚“ã�§ã�„ã‚‹ã�¨ã�“ã‚�ã‚’è¦‹ç›´ã�—ã�Ÿæ–¹ã�Œè‰¯ã�„
		/// 	</summary>
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
