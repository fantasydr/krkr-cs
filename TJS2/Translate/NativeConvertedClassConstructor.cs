/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Kirikiri.Tjs2.Translate;
using Sharpen;

namespace Kirikiri.Tjs2.Translate
{
	public abstract class NativeConvertedClassConstructor : NativeConvertedClassMethod
	{
		public NativeConvertedClassConstructor(TJS owner) : base(owner)
		{
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
			if (result != null)
			{
				result.Clear();
			}
			return Process(result, param, objthis);
		}
	}
}
