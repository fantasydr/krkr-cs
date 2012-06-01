/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class NativeJavaInstance : NativeInstance
	{
		private object mSelf;

		public NativeJavaInstance(object self)
		{
			mSelf = self;
		}

		// TJS constructor
		public virtual int Construct(Variant[] param, Dispatch2 tjsObj)
		{
			return Error.S_OK;
		}

		// called before destruction
		public virtual void Invalidate()
		{
		}

		// must destruct itself
		public virtual void Destruct()
		{
		}

		public virtual object GetNativeObject()
		{
			return mSelf;
		}
	}
}
