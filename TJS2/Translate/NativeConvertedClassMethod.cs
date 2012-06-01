/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Kirikiri.Tjs2.Translate;
using Sharpen;

namespace Kirikiri.Tjs2.Translate
{
	public abstract class NativeConvertedClassMethod : NativeConvertedClassBase
	{
		public NativeConvertedClassMethod(TJS owner) : base(owner)
		{
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public override int IsInstanceOf(int flag, string membername, string classname, Dispatch2
			 objthis)
		{
			if (membername == null)
			{
				if ("Function".Equals(classname))
				{
					return Error.S_TRUE;
				}
			}
			return base.IsInstanceOf(flag, membername, classname, objthis);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public override int FuncCall(int flag, string membername, Variant result, Variant
			[] param, Dispatch2 objthis)
		{
			if (membername != null)
			{
				return base.FuncCall(flag, membername, result, param, objthis);
			}
			if (objthis == null)
			{
				return Error.E_NATIVECLASSCRASH;
			}
			if (result != null)
			{
				result.Clear();
			}
			return Process(result, param, objthis);
		}

		// override this instead of FuncCall
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		protected internal abstract int Process(Variant result, Variant[] param, Dispatch2
			 objthis);
	}
}
