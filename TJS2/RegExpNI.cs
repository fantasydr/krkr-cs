/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class RegExpNI : NativeInstanceObject
	{
		public Sharpen.Pattern RegEx;

		public int mFlags;

		public int mStart;

		public Variant mArray;

		public int mIndex;

		public string mInput;

		public int mLastIndex;

		public string mLastMatch;

		public string mLastParen;

		public string mLeftContext;

		public string mRightContext;

		public Matcher mMatch;

		private const int globalsearch = (1 << 31);

		public static int RegExpFlagToValue(char ch, int prev)
		{
			// converts flag letter to internal flag value.
			// this returns modified prev.
			// when ch is '\0', returns default flag value and prev is ignored.
			if (ch == 0)
			{
				return 0;
			}
			switch (ch)
			{
				case 'g':
				{
					// global search
					prev |= globalsearch;
					return prev;
				}

				case 'i':
				{
					// ignore case
					prev |= Sharpen.Pattern.CASE_INSENSITIVE | Sharpen.Pattern.UNICODE_CASE;
					return prev;
				}

				case 'l':
				{
					// use localized collation
					//prev &= ~regbase::nocollate; return prev; ç„¡è¦–
					return prev;
				}

				default:
				{
					return prev;
					break;
				}
			}
		}

		public static int GetRegExpFlagsFromString(string @string)
		{
			// returns a flag value represented by string
			int flag = RegExpFlagToValue((char)0, 0);
			int count = @string.Length;
			int i = 0;
			while (i < count && @string[i] != '/')
			{
				flag = RegExpFlagToValue(@string[i], flag);
				i++;
			}
			return flag;
		}

		public RegExpNI()
		{
			mFlags = RegExpFlagToValue((char)0, 0);
			//mStart = 0;
			//mIndex =0;
			//mLastIndex = 0;
			mArray = new Variant();
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void Split(Holder<Dispatch2> array, string target, bool purgeempty
			)
		{
			if (array.mValue == null)
			{
				array.mValue = TJS.CreateArrayObject();
			}
			if (RegEx != null)
			{
				int limit = 0;
				if (purgeempty == false)
				{
					limit = -1;
				}
				string[] strs = RegEx.Split(target, limit);
				int count = strs.Length;
				for (int i = 0; i < count; i++)
				{
					Variant val = new Variant(strs[i]);
					array.mValue.PropSetByNum(Interface.MEMBERENSURE, i, val, array.mValue);
				}
			}
		}
	}
}
