/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	internal abstract class NativeFunction : Dispatch
	{
		public NativeFunction() : base()
		{
		}

		public NativeFunction(string name) : base()
		{
		}

		//this(null);
		// 'name' is just to be used as a label for debugging
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public override int FuncCall(int flag, string membername, Variant result, Variant
			[] param, Dispatch2 objthis)
		{
			if (membername != null)
			{
				return base.FuncCall(flag, membername, result, param, objthis);
			}
			return Process(result, param, objthis);
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

		// override this instead of FuncCall
		protected internal abstract int Process(Variant result, Variant[] param, Dispatch2
			 objthis);
	}
}
