/*
 * The TJS2 interpreter from kirikirij
 */

using System.Text;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class ScriptBlock : SourceCodeAccessor
	{
		private const bool D_IS_DISASSEMBLE = false;

		private static readonly string NO_SCRIPT = "no script";

		private TJS mOwner;

		private InterCodeObject mTopLevelObject;

		private AList<WeakReference<InterCodeObject>> mInterCodeObjectList;

		private string mName;

		private int mLineOffset;

		private string mScript;

		private ScriptLineData mLineData;

		public ScriptBlock(TJS owner, string name, int lineoffset, string script, ScriptLineData
			 linedata)
		{
			// a class for managing the script block
			// ä»¥ä¸‹ã�®4ã�¤ã�¯å®Ÿè¡Œæ™‚ã�«ã�„ã‚‹ã�‹ã�ªã€�å��å‰�ä»¥å¤–ã�¯ã‚¨ãƒ©ãƒ¼ç™ºç”Ÿæ™‚ã�«å¿…è¦�ã�«ã�ªã‚‹ã� ã�‘ã� ã‚�ã�†ã�‘ã�©ã€‚
			mOwner = owner;
			mName = name;
			mLineOffset = lineoffset;
			mScript = script;
			mLineData = linedata;
			mOwner.AddScriptBlock(this);
		}

		public virtual void SetObjects(InterCodeObject toplevel, AList<InterCodeObject> objs
			)
		{
			mTopLevelObject = toplevel;
			mInterCodeObjectList = new AList<WeakReference<InterCodeObject>>(objs.Count);
			int count = objs.Count;
			for (int i = 0; i < count; i++)
			{
				mInterCodeObjectList.AddItem(new WeakReference<InterCodeObject>(objs[i]));
			}
		}

		public virtual void SetObjects(InterCodeObject toplevel, InterCodeObject[] objs, 
			int count)
		{
			mTopLevelObject = toplevel;
			mInterCodeObjectList = new AList<WeakReference<InterCodeObject>>(objs.Length);
			for (int i = 0; i < count; i++)
			{
				mInterCodeObjectList.AddItem(new WeakReference<InterCodeObject>(objs[i]));
				objs[i] = null;
			}
		}

		public ScriptBlock(TJS owner)
		{
			mOwner = owner;
			// Java ã�§åˆ�æœŸå€¤ã�¨ã�ªã‚‹åˆ�æœŸåŒ–ã�¯çœ�ç•¥
			//mScript = null;
			//mName = null;
			//mInterCodeContext = null;
			//mTopLevelContext = null;
			//mLexicalAnalyzer = null;
			//mUsingPreProcessor = false;
			//mLineOffset = 0;
			//mCompileErrorCount = 0;
			//mNode = null;
			mOwner.AddScriptBlock(this);
		}

		~ScriptBlock()
		{
			if (mTopLevelObject != null)
			{
				mTopLevelObject = null;
			}
			mOwner.RemoveScriptBlock(this);
			if (mScript != null)
			{
				mScript = null;
			}
			if (mName != null)
			{
				mName = null;
			}
			try
			{
				base.Finalize();
			}
			catch
			{
			}
		}

		public virtual void Compact()
		{
			if (TJS.IsLowMemory)
			{
				mScript = null;
				mLineData = null;
				int count = mInterCodeObjectList.Count;
				for (int i = 0; i < count; i++)
				{
					InterCodeObject v = mInterCodeObjectList[i].Get();
					if (v != null)
					{
						v.Compact();
					}
				}
			}
		}

		public virtual int SrcPosToLine(int pos)
		{
			if (mLineData == null)
			{
				return 0;
			}
			return mLineData.GetSrcPosToLine(pos);
		}

		public virtual TJS GetTJS()
		{
			return mOwner;
		}

		public virtual string GetName()
		{
			return mName;
		}

		public virtual int GetLineOffset()
		{
			return mLineOffset;
		}

		private void CompactInterCodeObjectList()
		{
			// ã�ªã��ã�ªã�£ã�¦ã�„ã‚‹ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã‚’æ¶ˆã�™
			int count = mInterCodeObjectList.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (mInterCodeObjectList[i].Get() == null)
				{
					mInterCodeObjectList.Remove(i);
				}
			}
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		/// <exception cref="Kirikiri.Tjs2.CompileException"></exception>
		public virtual void ExecuteTopLevel(Variant result, Dispatch2 context)
		{
			// compiles text and executes its global level scripts.
			// the script will be compiled as an expression if isexpressn is true.
			// é€†ã‚¢ã‚»ãƒ³ãƒ–ãƒ«
			// execute global level script
			ExecuteTopLevelScript(result, context);
			int context_count = mInterCodeObjectList.Count;
			if (context_count != 1)
			{
				// this is not a single-context script block
				// (may hook itself)
				// release all contexts and global at this time
				InterCodeObject toplevel = mTopLevelObject;
				if (mTopLevelObject != null)
				{
					mTopLevelObject = null;
				}
				Remove(toplevel);
			}
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void ExecuteTopLevelScript(Variant result, Dispatch2 context)
		{
			if (mTopLevelObject != null)
			{
				mTopLevelObject.FuncCall(0, null, result, null, context);
			}
		}

		public virtual string GetLineDescriptionString(int pos)
		{
			// get short description, like "mainwindow.tjs(321)"
			// pos is in character count from the first of the script
			StringBuilder builer = new StringBuilder(512);
			int line = SrcPosToLine(pos) + 1;
			if (mName != null)
			{
				builer.Append(mName);
			}
			else
			{
				builer.Append("anonymous@");
				builer.Append(ToString());
			}
			builer.Append('(');
			builer.Append(line.ToString());
			builer.Append(')');
			return builer.ToString();
		}

		public virtual int LineToSrcPos(int line)
		{
			if (mLineData == null)
			{
				return 0;
			}
			// assumes line is added by LineOffset
			line -= mLineOffset;
			return mLineData.GetLineToSrcPos(line);
		}

		public virtual string GetLine(int line)
		{
			if (mLineData == null)
			{
				return NO_SCRIPT;
			}
			// note that this function DOES matter LineOffset
			line -= mLineOffset;
			return mLineData.GetLine(line);
		}

		public virtual bool IsReusable()
		{
			return GetContextCount() == 1 && mTopLevelObject != null;
		}

		public virtual int GetContextCount()
		{
			return mInterCodeObjectList.Count;
		}

		public virtual void Add(InterCodeObject obj)
		{
			mInterCodeObjectList.AddItem(new WeakReference<InterCodeObject>(obj));
		}

		public virtual void Remove(InterCodeObject obj)
		{
			int count = mInterCodeObjectList.Count;
			for (int i = 0; i < count; i++)
			{
				if (mInterCodeObjectList[i].Get() == obj)
				{
					mInterCodeObjectList.Remove(i);
					break;
				}
			}
			CompactInterCodeObjectList();
		}

		public virtual int GetObjectIndex(InterCodeObject obj)
		{
			return mInterCodeObjectList.IndexOf(obj);
		}

		public virtual InterCodeObject GetCodeObject(int index)
		{
			if (index >= 0 && index < mInterCodeObjectList.Count)
			{
				return mInterCodeObjectList[index].Get();
			}
			else
			{
				return null;
			}
		}

		public virtual string GetNameInfo()
		{
			if (mLineOffset == 0)
			{
				return new string(mName);
			}
			else
			{
				return mName + "(line +" + mLineOffset.ToString() + ")";
			}
		}

		public virtual int GetTotalVMCodeSize()
		{
			CompactInterCodeObjectList();
			int size = 0;
			int count = mInterCodeObjectList.Count;
			for (int i = 0; i < count; i++)
			{
				InterCodeObject obj = mInterCodeObjectList[i].Get();
				if (obj != null)
				{
					size += obj.GetCodeSize();
				}
			}
			return size;
		}

		public virtual int GetTotalVMDataSize()
		{
			CompactInterCodeObjectList();
			int size = 0;
			int count = mInterCodeObjectList.Count;
			for (int i = 0; i < count; i++)
			{
				InterCodeObject obj = mInterCodeObjectList[i].Get();
				if (obj != null)
				{
					size += obj.GetDataSize();
				}
			}
			return size;
		}

		public static void ConsoleOutput(string msg, Kirikiri.Tjs2.ScriptBlock blk)
		{
			TJS.OutputToConsole(msg);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public virtual void Dump()
		{
			CompactInterCodeObjectList();
			int count = mInterCodeObjectList.Count;
			for (int i = 0; i < count; i++)
			{
				InterCodeObject v = mInterCodeObjectList[i].Get();
				if (v != null)
				{
					ConsoleOutput(string.Empty, this);
					string ptr = string.Format(" 0x%08X", v.GetHashCode());
					ConsoleOutput("(" + v.GetContextTypeName() + ") " + v.GetName() + ptr, this);
					v.Disassemble(this, 0, 0);
				}
			}
		}

		public virtual string GetScript()
		{
			if (mScript != null)
			{
				return mScript;
			}
			else
			{
				return NO_SCRIPT;
			}
		}

		public virtual int CodePosToSrcPos(int codepos)
		{
			return 0;
		}
		// allways 0, åŸºæœ¬çš„ã�«ä½¿ã‚�ã‚Œã�ªã�„
	}
}
