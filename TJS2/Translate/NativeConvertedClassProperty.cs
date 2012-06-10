/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Kirikiri.Tjs2.Translate;
using Sharpen;

namespace Kirikiri.Tjs2.Translate
{
	public abstract class NativeConvertedClassProperty : NativeConvertedClassBase
	{
		public NativeConvertedClassProperty(TJS owner) : base(owner)
		{
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public override int IsInstanceOf(int flag, string membername, string classname, Dispatch2
			 objthis)
		{
			if (membername == null)
			{
				if ("Property".Equals(classname))
				{
					return Error.S_TRUE;
				}
			}
			return base.IsInstanceOf(flag, membername, classname, objthis);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public override int PropGet(int flag, string membername, Variant result, Dispatch2
			 objthis)
		{
			if (membername != null)
			{
				return base.PropGet(flag, membername, result, objthis);
			}
			if (objthis == null)
			{
				return Error.E_NATIVECLASSCRASH;
			}
			if (result == null)
			{
				return Error.E_FAIL;
			}
			return Get(result, objthis);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public override int PropSet(int flag, string membername, Variant param, Dispatch2
			 objthis)
		{
			if (membername != null)
			{
				return base.PropSet(flag, membername, param, objthis);
			}
			if (objthis == null)
			{
				return Error.E_NATIVECLASSCRASH;
			}
			if (param == null)
			{
				return Error.E_FAIL;
			}
			return Set(param, objthis);
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public abstract int Get(Variant result, Dispatch2 objthis);

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public abstract int Set(Variant param, Dispatch2 objthis);
	}
}
