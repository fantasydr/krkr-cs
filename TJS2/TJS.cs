/*
 * TJS2 CSharp
 */

using System.Collections.Generic;
using System.Text;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class TJS
	{
		public const int VERSION_MAJOR = 2;

		public const int VERSION_MINOR = 4;

		public const int VERSION_RELEASE = 28;

		public const int VERSION_HEX = VERSION_MAJOR * unchecked((int)(0x1000000)) + VERSION_MINOR
			 * unchecked((int)(0x10000)) + VERSION_RELEASE;

		private const int ENV_WINDOWS = 0;

		private const int ENV_ANDROID = 1;

		private const int ENV_JAVA_APPLICATION = 2;

		private const int ENV_JAVA_APPLET = 3;

		private const int GLOBAL_HASH_BITS = 7;

		private const int MEMBERENSURE = unchecked((int)(0x00000200));

		private static GlobalStringMap mGlobalStringMap;

		private static AList<string> mNativeClassNames;

		private static ConsoleOutput mConsoleOutput;

		private static MessageMapper mMessageMapper;

		public static bool mWarnOnNonGlobalEvalOperator;

		public static bool mUnaryAsteriskIgnoresPropAccess;

		public static bool mEvalOperatorIsOnGlobal;

		public static bool EnableDebugMode;

		public static bool IsTarminating;

		public static bool IsLowMemory;

		public static StorageInterface mStorage;

		private static Dispatch2 mArrayClass;

		private static Dispatch2 mDictionayClass;

		private static VariantPool mVAPool;

		public static Variant[] NULL_ARG;

		private Dictionary<string, int> mPPValues;

		private AList<WeakReference<ScriptBlock>> mScriptBlocks;

		private CustomObject mGlobal;

		private ScriptCache mCache;

		//private static final String TAG ="TJS";
		// プリプロセッサでは未定义の时この值が入る
		// create a member if not exists
		//public static int mCompactVariantArrayMagic;
		//public static VariantArrayStack mVariantArrayStack;
		// static 关系はここで初期化
		public static void Initialize()
		{
			// mStorage = null; // 事前に设定されるので、ここで初期化するのはまずい
			NULL_ARG = new Variant[0];
			IsTarminating = false;
			mWarnOnNonGlobalEvalOperator = false;
			mUnaryAsteriskIgnoresPropAccess = false;
			mNativeClassNames = new AList<string>();
			mGlobalStringMap = new GlobalStringMap();
			//mCompactVariantArrayMagic = 0;
			//mVariantArrayStack = new VariantArrayStack();
			mEvalOperatorIsOnGlobal = false;
			EnableDebugMode = true;
			mConsoleOutput = null;
			mMessageMapper = new MessageMapper();
			RandomGeneratorNI.SetRandomBits128(null);
			//ArrayNI.register();
			mVAPool = new VariantPool();
			CompileState.mEnableDicFuncQuickHack = true;
			Variant.Initialize();
			DictionaryObject.Initialize();
			ArrayObject.Initialize();
			ByteCodeLoader.Initialize();
			CustomObject.Initialize();
			MathClass.Initialize();
			LexicalAnalyzer.Initialize();
			try
			{
				mArrayClass = new ArrayClass();
				mDictionayClass = new DictionaryClass();
			}
			catch (VariantException)
			{
			}
			catch (TJSException)
			{
			}
		}

		public static void FinalizeApplication()
		{
			mGlobalStringMap = null;
			if (mNativeClassNames != null)
			{
				mNativeClassNames.Clear();
			}
			mNativeClassNames = null;
			mConsoleOutput = null;
			mMessageMapper = null;
			mStorage = null;
			mArrayClass = null;
			mDictionayClass = null;
			mVAPool = null;
			NULL_ARG = null;
			ArrayObject.FinalizeApplication();
			ByteCodeLoader.FinalizeApplication();
			CustomObject.FinalizeApplication();
			DictionaryObject.FinalizeApplication();
			MathClass.FinalizeApplication();
			Variant.FinalizeApplication();
			LexicalAnalyzer.FinalizeApplication();
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public TJS()
		{
			// create script cache object
			mCache = new ScriptCache(this);
			mPPValues = new Dictionary<string, int>();
			SetPPValue("version", VERSION_HEX);
			SetPPValue("environment", ENV_JAVA_APPLICATION);
			// TODO 适切な值を入れる
			SetPPValue("compatibleSystem", 1);
			// 互换システム true
			mGlobal = new CustomObject(GLOBAL_HASH_BITS);
			mScriptBlocks = new AList<WeakReference<ScriptBlock>>();
			Dispatch2 dsp;
			Variant val;
			// Array
			//dsp = new ArrayClass();
			dsp = mArrayClass;
			val = new Variant(dsp, null);
			mGlobal.PropSet(MEMBERENSURE, "Array", val, mGlobal);
			// Dictionary
			//dsp = new DictionaryClass();
			dsp = mDictionayClass;
			val = new Variant(dsp, null);
			mGlobal.PropSet(MEMBERENSURE, "Dictionary", val, mGlobal);
			// Date //TODO: add date back
			//dsp = new DateClass();
			//val = new Variant(dsp, null);
			//mGlobal.PropSet(MEMBERENSURE, "Date", val, mGlobal);
			{
				// Math
				Dispatch2 math;
				dsp = math = new MathClass();
				val = new Variant(dsp, null);
				mGlobal.PropSet(MEMBERENSURE, "Math", val, mGlobal);
				// Math.RandomGenerator
				dsp = new RandomGeneratorClass();
				val = new Variant(dsp, null);
				math.PropSet(MEMBERENSURE, "RandomGenerator", val, math);
			}
			// Exception
			dsp = new ExceptionClass();
			val = new Variant(dsp, null);
			mGlobal.PropSet(MEMBERENSURE, "Exception", val, mGlobal);
			// RegExp
			dsp = new RegExpClass();
			val = new Variant(dsp, null);
			mGlobal.PropSet(MEMBERENSURE, "RegExp", val, mGlobal);
			
			// TODO: move Debug outside tjs core
			dsp = new DebugClass();
			val = new Variant(dsp, null);
			mGlobal.PropSet(MEMBERENSURE, "Debug", val, mGlobal);
		}

		public virtual void SetPPValue(string name, int value)
		{
			if (name != null)
			{
				mPPValues.Put(name, Sharpen.Extensions.ValueOf(value));
			}
		}

		public virtual int GetPPValue(string name)
		{
			int ret = mPPValues.Get(name);
			if (ret == null)
			{
				return 0;
			}
			return ret;
		}

		public static void OutputExceptionToConsole(string msg)
		{
			OutputToConsole(msg);
		}

		public static void OutputToConsole(string mes)
		{
			if (mConsoleOutput == null)
			{
				Logger.Log(mes);
			}
			else
			{
				mConsoleOutput.Print(mes);
			}
		}

		public static void OutputToConsoleWithCentering(string msg, int width)
		{
			// this function does not matter whether msg includes ZENKAKU characters ...
			if (msg == null)
			{
				return;
			}
			int len = msg.Length;
			int ns = (width - len) / 2;
			if (ns <= 0)
			{
				OutputToConsole(msg);
			}
			else
			{
				StringBuilder builder = new StringBuilder(ns + len + 1);
				while ((ns--) > 0)
				{
					builder.Append(' ');
				}
				builder.Append(msg);
				OutputToConsole(builder.ToString());
				builder = null;
			}
		}

		public static void OutputToConsoleSeparator(string text, int count)
		{
			int len = text.Length;
			StringBuilder builder = new StringBuilder(len * count);
			while (count > 0)
			{
				builder.Append(text);
				count--;
			}
			OutputToConsole(builder.ToString());
		}

		public static string MapGlobalStringMap(string str)
		{
			return mGlobalStringMap.Map(str);
		}

		public static int RegisterNativeClass(string name)
		{
			int count = mNativeClassNames.Count;
			for (int i = 0; i < count; i++)
			{
				if (mNativeClassNames[i].Equals(name))
				{
					return i;
				}
			}
			mNativeClassNames.AddItem(MapGlobalStringMap(name));
			return mNativeClassNames.Count - 1;
		}

		public static int FindNaitveClassID(string name)
		{
			int count = mNativeClassNames.Count;
			for (int i = 0; i < count; i++)
			{
				if (mNativeClassNames[i].Equals(name))
				{
					return i;
				}
			}
			return -1;
		}

		public static string FindNativeClassName(int id)
		{
			if (id < 0 || id >= mNativeClassNames.Count)
			{
				return null;
			}
			return mNativeClassNames[id];
		}

		public static void SetConsoleOutput(ConsoleOutput console)
		{
			mConsoleOutput = console;
		}

		public static ConsoleOutput GetConsoleOutput()
		{
			return mConsoleOutput;
		}

		//public static int getDictionaryClassID() { return DictionaryClass.ClassID; }
		//public static int getArrayClassID() { return ArrayClass.ClassID; }
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		/// <exception cref="Kirikiri.Tjs2.CompileException"></exception>
		public virtual void ExecScript(string script, Variant result, Dispatch2 context, 
			string name, int lineofs)
		{
			if (mCache != null)
			{
				mCache.ExecScript(script, result, context, name, lineofs);
			}
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		/// <exception cref="Kirikiri.Tjs2.CompileException"></exception>
		public virtual void EvalExpression(string expression, Variant result, Dispatch2 context
			, string name, int lineofs)
		{
			if (mCache != null)
			{
				mCache.EvalExpression(expression, result, context, name, lineofs);
			}
		}

		/// <exception cref="Kirikiri.Tjs2.CompileException"></exception>
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void CompileScript(string script, string name, int lineofs, bool isresultneeded
			, BinaryStream output)
		{
			Compiler compiler = new Compiler(this);
			if (name != null)
			{
				compiler.SetName(name, lineofs);
			}
			compiler.Compile(script, false, isresultneeded, output);
			compiler = null;
		}

		/// <exception cref="Kirikiri.Tjs2.CompileException"></exception>
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void ToJavaCode(string script, string name, int lineofs, bool isresultneeded
			)
		{
			Compiler compiler = new Compiler(this);
			if (name != null)
			{
				compiler.SetName(name, lineofs);
			}
			compiler.ToJavaCode(script, false, isresultneeded);
			compiler = null;
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void LoadByteCode(Variant result, Dispatch2 context, string name, 
			BinaryStream input)
		{
			ByteCodeLoader loader = new ByteCodeLoader();
			ScriptBlock block = loader.ReadByteCode(this, name, input);
			block.ExecuteTopLevel(result, context);
			if (block.GetContextCount() == 0)
			{
				RemoveScriptBlock(block);
			}
			block = null;
		}

		public virtual Dispatch2 GetGlobal()
		{
			return mGlobal;
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static Dispatch2 CreateArrayObject()
		{
			return CreateArrayObject(null);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static Dispatch2 CreateArrayObject(Holder<Dispatch2> classout)
		{
			if (classout != null)
			{
				classout.mValue = mArrayClass;
			}
			Holder<Dispatch2> holder = new Holder<Dispatch2>(null);
			mArrayClass.CreateNew(0, null, holder, null, mArrayClass);
			return holder.mValue;
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static Dispatch2 CreateDictionaryObject()
		{
			return CreateDictionaryObject(null);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static Dispatch2 CreateDictionaryObject(Holder<Dispatch2> classout)
		{
			if (classout != null)
			{
				classout.mValue = mDictionayClass;
			}
			Holder<Dispatch2> holder = new Holder<Dispatch2>(null);
			mDictionayClass.CreateNew(0, null, holder, null, mDictionayClass);
			return holder.mValue;
		}

		public virtual void AddScriptBlock(ScriptBlock block)
		{
			mScriptBlocks.AddItem(new WeakReference<ScriptBlock>(block));
		}

		public virtual void RemoveScriptBlock(ScriptBlock block)
		{
			int count = mScriptBlocks.Count;
			for (int i = 0; i < count; i++)
			{
				if (mScriptBlocks[i].Get() == block)
				{
					mScriptBlocks.Remove(i);
					break;
				}
			}
			CompactScriptBlock();
		}

		private void CompactScriptBlock()
		{
			// なくなっているオブジェクトを消す
			int count = mScriptBlocks.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (mScriptBlocks[i].Get() == null)
				{
					mScriptBlocks.Remove(i);
				}
			}
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public virtual void Dump()
		{
			Dump(80);
		}

		// dumps all existing script block
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public virtual void Dump(int width)
		{
			// dumps all existing script block
			string version = string.Format("TJS version %d.%d.%d", VERSION_MAJOR, VERSION_MINOR
				, VERSION_RELEASE);
			OutputToConsoleSeparator("#", width);
			OutputToConsoleWithCentering("TJS Context Dump", width);
			OutputToConsoleSeparator("#", width);
			OutputToConsole(version);
			OutputToConsole(string.Empty);
			// なくなっているオブジェクトを消す
			CompactScriptBlock();
			if (mScriptBlocks.Count > 0)
			{
				string buf = string.Format("Total %d script block(s)", mScriptBlocks.Count);
				OutputToConsole(buf);
				OutputToConsole(string.Empty);
				int totalcontexts = 0;
				int totalcodesize = 0;
				int totaldatasize = 0;
				for (int i = 0; i < mScriptBlocks.Count; i++)
				{
					ScriptBlock b = mScriptBlocks[i].Get();
					if (b == null)
					{
						continue;
					}
					int n;
					string name = b.GetName();
					string title;
					if (name != null)
					{
						title = b.GetNameInfo();
					}
					else
					{
						title = "(no-named script block)";
					}
					string ptr = string.Format(" 0x%08X", b.GetHashCode());
					title += ptr;
					OutputToConsole(title);
					n = b.GetContextCount();
					totalcontexts += n;
					buf = string.Format("\tCount of contexts      : %d", n);
					OutputToConsole(buf);
					n = b.GetTotalVMCodeSize();
					totalcodesize += n;
					buf = string.Format("\tVM code area size      : %d words", n);
					OutputToConsole(buf);
					n = b.GetTotalVMDataSize();
					totaldatasize += n;
					buf = string.Format("\tVM constant data count : %d", n);
					OutputToConsole(buf);
					OutputToConsole(string.Empty);
				}
				buf = string.Format("Total count of contexts      : %d", totalcontexts);
				OutputToConsole(buf);
				buf = string.Format("Total VM code area size      : %d words", totalcodesize);
				OutputToConsole(buf);
				buf = string.Format("Total VM constant data count : %d", totaldatasize);
				OutputToConsole(buf);
				OutputToConsole(string.Empty);
				for (int i_1 = 0; i_1 < mScriptBlocks.Count; i_1++)
				{
					ScriptBlock b = mScriptBlocks[i_1].Get();
					if (b == null)
					{
						continue;
					}
					OutputToConsoleSeparator("-", width);
					string name = b.GetName();
					string title;
					if (name != null)
					{
						title = b.GetNameInfo();
					}
					else
					{
						title = "(no-named script block)";
					}
					string ptr;
					ptr = string.Format(" 0x%08X", b.GetHashCode());
					title += ptr;
					OutputToConsoleWithCentering(title, width);
					OutputToConsoleSeparator("-", width);
					b.Dump();
					OutputToConsole(string.Empty);
					OutputToConsole(string.Empty);
				}
			}
			else
			{
				OutputToConsole(string.Empty);
				OutputToConsole("There are no script blocks in the system.");
			}
		}

		public static void RegisterMessageMap(string name, MessageMapper.MessageHolder holder
			)
		{
			if (mMessageMapper != null)
			{
				mMessageMapper.Register(name, holder);
			}
		}

		public static void UnregisterMessageMap(string name)
		{
			if (mMessageMapper != null)
			{
				mMessageMapper.Unregister(name);
			}
		}

		public static bool AssignMessage(string name, string newmsg)
		{
			if (mMessageMapper != null)
			{
				return mMessageMapper.AssignMessage(name, newmsg);
			}
			return false;
		}

		public static string CreateMessageMapString()
		{
			if (mMessageMapper != null)
			{
				return mMessageMapper.CreateMessageMapString();
			}
			return string.Empty;
		}

		public static string GetMessageMapMessage(string name)
		{
			if (mMessageMapper != null)
			{
				string ret = mMessageMapper.Get(name);
				if (ret != null)
				{
					return ret;
				}
				return string.Empty;
			}
			return string.Empty;
		}

		public virtual void Shutdown()
		{
			//variantArrayStackCompactNow();
			if (mGlobal != null)
			{
				try
				{
					mGlobal.Invalidate(0, null, mGlobal);
				}
				catch (VariantException)
				{
				}
				catch (TJSException)
				{
				}
				mGlobal.Clear();
				mGlobal = null;
			}
			if (mCache != null)
			{
				mCache = null;
			}
		}

		public static Variant AllocateVariant()
		{
			return mVAPool.Allocate();
		}

		public static void ReleaseVariant(Variant v)
		{
			mVAPool.Release(v);
		}

		public static void ReleaseVariant(Variant[] v)
		{
			mVAPool.Release(v);
		}

		public virtual void LexTest(string script)
		{
			Compiler compiler = new Compiler(this);
			int token = 0;
			int token1;
			ScriptLineData lineData = new ScriptLineData(script, 0);
			Lexer lexer = new Lexer(compiler, script, false, false);
			LexicalAnalyzer lex = new LexicalAnalyzer(compiler, script, false, false);
			try
			{
				do
				{
					token = lexer.GetNext();
					token1 = lex.GetNext();
					if (token1 != token)
					{
						int oleline = lineData.GetSrcPosToLine(lexer.GetCurrentPosition());
						int newline = lineData.GetSrcPosToLine(lex.GetCurrentPosition());
						System.Console.Out.Write("error line:" + oleline + ", " + newline + "\n");
					}
					int v1 = lexer.GetValue();
					int v2 = lex.GetValue();
					if (v1 != v2)
					{
						int oleline = lineData.GetSrcPosToLine(lexer.GetCurrentPosition());
						int newline = lineData.GetSrcPosToLine(lex.GetCurrentPosition());
						System.Console.Out.Write("error line:" + oleline + ", " + newline + "\n");
					}
					if (v1 != 0)
					{
						object o1 = lexer.GetValue(v1).ToJavaObject();
						object o2 = lex.GetValue(v2).ToJavaObject();
						if (!o1.Equals(o2))
						{
							int oleline = lineData.GetSrcPosToLine(lexer.GetCurrentPosition());
							int newline = lineData.GetSrcPosToLine(lex.GetCurrentPosition());
							System.Console.Out.Write("o1:" + o1.ToString() + ", o2" + o2.ToString() + "\n");
							System.Console.Out.Write("error line:" + oleline + ", " + newline + "\n");
						}
					}
				}
				while (token != 0);
			}
			catch (CompileException)
			{
			}
			long start = Runtime.CurrentTimeMillis();
			lexer = new Lexer(compiler, script, false, false);
			try
			{
				do
				{
					token = lexer.GetNext();
					lexer.GetValue();
				}
				while (token != 0);
			}
			catch (CompileException)
			{
			}
			long time = Runtime.CurrentTimeMillis() - start;
			System.Console.Out.Write("old lex : " + time + "ms\n");
			start = Runtime.CurrentTimeMillis();
			lex = new LexicalAnalyzer(compiler, script, false, false);
			try
			{
				do
				{
					token = lex.GetNext();
					lex.GetValue();
				}
				while (token != 0);
			}
			catch (CompileException)
			{
			}
			time = Runtime.CurrentTimeMillis() - start;
			System.Console.Out.Write("new lex : " + time + "ms\n");
		}
	}
}
