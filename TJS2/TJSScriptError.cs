/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class TJSScriptError : TJSException
	{
		private const long serialVersionUID = -1727870605938357683L;

		private const int MAX_TRACE_TEXT_LEN = 1500;

		private SourceCodeAccessor mAccessor;

		private int mPosition;

		private string mTrace;

		public virtual SourceCodeAccessor GetAccessor()
		{
			return mAccessor;
		}

		public virtual int GetPosition()
		{
			return mPosition;
		}

		public virtual int GetSourceLine()
		{
			return mAccessor.SrcPosToLine(mPosition) + 1;
		}

		public string GetBlockName()
		{
			string name = mAccessor.GetName();
			return name != null ? name : string.Empty;
		}

		public string GetTrace()
		{
			return mTrace;
		}

		public virtual bool AddTrace(ScriptBlock block, int srcpos)
		{
			int len = mTrace.Length;
			if (len >= MAX_TRACE_TEXT_LEN)
			{
				return false;
			}
			if (len != 0)
			{
				mTrace += " <-- ";
			}
			mTrace += block.GetLineDescriptionString(srcpos);
			return true;
		}

		public virtual bool AddTrace(string data)
		{
			int len = mTrace.Length;
			if (len >= MAX_TRACE_TEXT_LEN)
			{
				return false;
			}
			if (len != 0)
			{
				mTrace += " <-- ";
			}
			mTrace += data;
			return true;
		}

		public TJSScriptError(string Msg, SourceCodeAccessor accessor, int pos) : base(Msg
			)
		{
			mAccessor = accessor;
			mPosition = pos;
			mTrace = new string();
		}

		public TJSScriptError(Kirikiri.Tjs2.TJSScriptError @ref)
		{
			mAccessor = @ref.mAccessor;
			mPosition = @ref.mPosition;
			mTrace = @ref.mTrace;
		}

		public virtual bool AddTrace(InterCodeObject context, int codepos)
		{
			int len = mTrace.Length;
			if (len >= MAX_TRACE_TEXT_LEN)
			{
				return false;
			}
			if (len != 0)
			{
				mTrace += " <-- ";
			}
			mTrace += context.GetPositionDescriptionString(codepos);
			return true;
		}
	}
}
