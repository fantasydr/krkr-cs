/*
 * The TJS2 interpreter from kirikirij
 */

using System.Collections.Generic;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class VectorWrap<E> : AList<E>
	{
		private const long serialVersionUID = -8162877809635079713L;

		public VectorWrap() : base()
		{
		}

		public VectorWrap(ICollection<E> c) : base(c)
		{
		}

		public VectorWrap(int initialCapacity) : base(initialCapacity)
		{
		}

		// vector ã‚’ ArrayList ã�§ç½®ã��æ�›ã�ˆã‚‹ã�Ÿã‚�ã�®ã‚¯ãƒ©ã‚¹
		public virtual E LastElement()
		{
			return this[Count - 1];
		}

		public virtual E Back()
		{
			return this[Count - 1];
		}

		public virtual void Pop_back()
		{
			Remove(Count - 1);
		}
	}
}