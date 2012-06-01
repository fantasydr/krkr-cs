/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	/// <summary>æœ€å¤§æ ¼ç´�æ•°åˆ¶é™�ä»˜ã��ãƒ�ãƒƒã‚·ãƒ¥ãƒ†ãƒ¼ãƒ–ãƒ«</summary>
	/// <?></?>
	/// <?></?>
	public class HashCache<Key, Value> : HashTable<Key, Value>
	{
		/// <summary>æ ¼ç´�å�¯èƒ½ã�ªæœ€å¤§è¦�ç´ æ•°</summary>
		private int mMaxCount;

		/// <summary>ã‚³ãƒ³ã‚¹ãƒˆãƒ©ã‚¯ã‚¿</summary>
		/// <param name="maxcount">æ ¼ç´�å�¯èƒ½ã�ªæœ€å¤§æ•°</param>
		public HashCache(int maxcount)
		{
			mMaxCount = maxcount;
		}

		/// <summary>è¦�ç´ ã�®è¿½åŠ ã€�æœ€å¤§æ•°ã‚’è¶…ã�ˆã�Ÿæ™‚ã�¯å�¤ã�„ã‚‚ã�®ã�Œå‰Šé™¤ã�•ã‚Œã‚‹
		/// 	</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <param name="value">æ ¼ç´�ã�®ã�™ã‚‹å€¤</param>
		public override void Put(Key key, Value value)
		{
			base.Put(key, value);
			if (GetCount() > mMaxCount)
			{
				ChopLast(GetCount() - mMaxCount);
			}
		}

		/// <summary>æ ¼ç´�å�¯èƒ½ã�ªæ•°ã�®è¨­å®š</summary>
		/// <param name="maxcount">æ ¼ç´�å�¯èƒ½ã�ªæ•°</param>
		public virtual void SetMaxCount(int maxcount)
		{
			mMaxCount = maxcount;
			if (GetCount() > mMaxCount)
			{
				ChopLast(GetCount() - mMaxCount);
			}
		}

		/// <summary>æ ¼ç´�å�¯èƒ½ã�ªæ•°ã‚’å¾—ã‚‹</summary>
		/// <returns>æ ¼ç´�å�¯èƒ½ã�ªæ•°</returns>
		public virtual int GetMaxCount()
		{
			return mMaxCount;
		}
	}
}
