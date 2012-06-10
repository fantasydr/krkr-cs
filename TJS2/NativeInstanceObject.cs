/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class NativeInstanceObject : NativeInstance
	{
		// TJS constructor
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual int Construct(Variant[] param, Dispatch2 tjsObj)
		{
			return Error.S_OK;
		}

		// called before destruction
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void Invalidate()
		{
		}

		// must destruct itself
		public virtual void Destruct()
		{
		}
	}
}
