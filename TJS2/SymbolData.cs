/*
 * TJS2 CSharp
 */

using System.Text;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	internal class SymbolData
	{
		private const int SYMBOL_INIT = unchecked((int)(0x2));

		private const int SYMBOL_USING = unchecked((int)(0x1));

		public string mName;

		public int mHash;

		public int mSymFlags;

		public int mFlags;

		public Variant mValue;

		public SymbolData mNext;

		public virtual void SelfClear()
		{
			mName = null;
			mHash = 0;
			mFlags = 0;
			mValue = new Variant();
			mNext = null;
			mSymFlags = SYMBOL_INIT;
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		private void SetNameInternal(string name)
		{
			if (name == null)
			{
				throw new TJSException(Error.IDExpected);
			}
			if (name.Length == 0)
			{
				throw new TJSException(Error.IDExpected);
			}
			if (mName != null && mName.Equals(name))
			{
				return;
			}
			//mName = new String( name );
			mName = name;
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void SetName(string name, int hash)
		{
			// setNameInternal(name);
			mHash = hash;
			if (mName != null && mName.Equals(name))
			{
				return;
			}
			if (name == null)
			{
				throw new TJSException(Error.IDExpected);
			}
			if (name.Length == 0)
			{
				throw new TJSException(Error.IDExpected);
			}
			mName = name;
		}

		public string GetName()
		{
			return mName;
		}

		public virtual void PostClear()
		{
			mName = null;
			mValue = null;
			mValue = new Variant();
			mSymFlags &= ~SYMBOL_USING;
		}

		public virtual void Destory()
		{
			mName = null;
			mValue = null;
		}

		public virtual bool NameMatch(string name)
		{
			if (mName == name)
			{
				return true;
			}
			//return mName != null && mName.equals( name );
			return mName.Equals(name);
		}

		public virtual void ReShare()
		{
			// search shared string map using mapGlobalStringMap,
			// and ahsre the name string ( if it can )
			if (mName != null)
			{
				mName = TJS.MapGlobalStringMap(mName);
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder(32);
			if (mName != null)
			{
				builder.Append(mName);
				builder.Append(" : ");
			}
			else
			{
				builder.Append("no name : ");
			}
			if (mValue != null)
			{
				builder.Append(mValue.ToString());
			}
			else
			{
				builder.Append("empty");
			}
			return builder.ToString();
		}
	}
}
