/*
 * The TJS2 interpreter from kirikirij
 */

using Sharpen;

namespace Kirikiri.Tjs2
{
	public class LongQue
	{
		private const int DEFAULT_SIZE = 16;

		private long[] mItems;

		private int mFront;

		private int mTail;

		public LongQue()
		{
			mItems = new long[DEFAULT_SIZE];
		}

		public virtual bool IsEmpty()
		{
			return (mFront == mTail);
		}

		public virtual long Front()
		{
			return mItems[mFront];
		}

		public virtual long Pop_front()
		{
			long ret = mItems[mFront];
			if (mFront == mTail)
			{
				return ret;
			}
			else
			{
				// ãƒ‡ãƒ¼ã‚¿ã�Œç©ºã�£ã�½
				int mask = mItems.Length - 1;
				// 16ã�®å€�æ•°ã�«ã�ªã‚‹ã�®ã�§-1ã�§ãƒžã‚¹ã‚¯ã�Œä½œã‚Œã‚‹
				mFront = (mFront + 1) & mask;
			}
			return ret;
		}

		public virtual void Push_back(long v)
		{
			int mask = mItems.Length - 1;
			// 16ã�®å€�æ•°ã�«ã�ªã‚‹ã�®ã�§-1ã�§ãƒžã‚¹ã‚¯ã�Œä½œã‚Œã‚‹
			int tail = (mTail + 1) & mask;
			if (tail == mFront)
			{
				// æº¢ã‚Œã‚‹
				int count = mItems.Length << 1;
				long[] newArray = new long[count];
				if (mTail < mFront)
				{
					int copySize = mItems.Length - mFront;
					System.Array.Copy(mItems, mFront, newArray, 0, copySize);
					// front ï½ž æœ«å°¾ã�¾ã�§ã‚³ãƒ”ãƒ¼
					System.Array.Copy(mItems, 0, newArray, copySize, mTail);
					// å…ˆç«¯ ï½ž tailã�¾ã�§ã‚³ãƒ”ãƒ¼
					mTail = copySize + mTail;
				}
				else
				{
					// mFront == 0
					System.Array.Copy(mItems, 0, newArray, 0, mTail);
					// å…ˆç«¯ ï½ž tailã�¾ã�§ã‚³ãƒ”ãƒ¼
					mTail = mTail;
				}
				mFront = 0;
				mItems = newArray;
				mItems[mTail] = v;
				mTail++;
			}
			else
			{
				mItems[mTail] = v;
				mTail = tail;
			}
		}
	}
}
