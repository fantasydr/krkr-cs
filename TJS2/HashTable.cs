/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	/// <summary>
	/// è¿½åŠ é †ã‚’æŒ�ã�£ã�Ÿãƒ�ãƒƒã‚·ãƒ¥ãƒ†ãƒ¼ãƒ–ãƒ«ã€�é…�åˆ—ã�®æ‹¡å¤§ã�¯è¡Œã‚�ã‚Œã�ªã�„
	/// å�¤ã�„ã‚‚ã�®ã‚’å‰Šé™¤ã�§ã��ã‚‹
	/// </summary>
	/// <?></?>
	/// <?></?>
	public class HashTable<Key, Value>
	{
		/// <summary>ãƒ‡ãƒ•ã‚©ãƒ«ãƒˆé…�åˆ—ã‚µã‚¤ã‚º</summary>
		private const int DEFAULT_HASH_SIZE = 64;

		/// <summary>ä½¿ç”¨ä¸­ãƒ•ãƒ©ã‚°</summary>
		private const int HASH_USING = unchecked((int)(0x1));

		/// <summary>é…�åˆ—ã�«ç›´ã�«å…¥ã�£ã�¦ã�„ã‚‹è¦�ç´ ãƒ•ãƒ©ã‚°</summary>
		private const int HASH_LV1 = unchecked((int)(0x2));

		/// <summary>å�„è¦�ç´ </summary>
		/// <?></?>
		/// <?></?>
		internal class Element<Key, Value>
		{
			/// <summary>ãƒ�ãƒƒã‚·ãƒ¥å€¤</summary>
			internal int mHash;

			/// <summary>å†…éƒ¨ã�§ä½¿ç”¨ã�™ã‚‹ãƒ•ãƒ©ã‚°</summary>
			internal int mFlags;

			/// <summary>ã‚­ãƒ¼</summary>
			internal Key mKey;

			/// <summary>æ ¼ç´�ã�™ã‚‹å€¤</summary>
			internal Value mValue;

			/// <summary>ã‚¢ã‚¤ãƒ†ãƒ ãƒ�ã‚§ãƒ¼ãƒ³ã�§å‰�ã�®ã‚¢ã‚¤ãƒ†ãƒ </summary>
			internal HashTable.Element<Key, Value> mPrev;

			/// <summary>ã‚¢ã‚¤ãƒ†ãƒ ãƒ�ã‚§ãƒ¼ãƒ³ã�§æ¬¡ã�®ã‚¢ã‚¤ãƒ†ãƒ </summary>
			internal HashTable.Element<Key, Value> mNext;

			/// <summary>è¿½åŠ é †ã�§ç›´å‰�ã�«è¿½åŠ ã�•ã‚Œã�Ÿã‚¢ã‚¤ãƒ†ãƒ </summary>
			internal HashTable.Element<Key, Value> mNPrev;

			/// <summary>è¿½åŠ é †ã�§ç›´å¾Œã�«è¿½åŠ ã�•ã‚Œã�Ÿã‚¢ã‚¤ãƒ†ãƒ </summary>
			internal HashTable.Element<Key, Value> mNNext;
		}

		/// <summary>è¦�ç´ é…�åˆ—</summary>
		private HashTable.Element<Key, Value>[] mElms;

		/// <summary>å®Ÿè¦�ç´ æ•°</summary>
		private int mCount;

		/// <summary>è¿½åŠ é †ã�§æœ€åˆ�ã�«è¿½åŠ ã�•ã‚Œã�Ÿã‚¢ã‚¤ãƒ†ãƒ </summary>
		private HashTable.Element<Key, Value> mNFirst;

		/// <summary>è¿½åŠ é †ã�§æœ€å¾Œã�«è¿½åŠ ã�•ã‚Œã�Ÿã‚¢ã‚¤ãƒ†ãƒ </summary>
		private HashTable.Element<Key, Value> mNLast;

		/// <summary>
		/// ãƒ‡ãƒ•ã‚©ãƒ«ãƒˆã‚³ãƒ³ã‚¹ãƒˆãƒ©ã‚¯ã‚¿
		/// è¦�ç´ æ•°ã�¯ DEFAULT_HASH_SIZE ã�¨ã�ªã‚‹ã€‚
		/// </summary>
		public HashTable() : this(DEFAULT_HASH_SIZE)
		{
		}

		/// <summary>ã‚³ãƒ³ã‚¹ãƒˆãƒ©ã‚¯ã‚¿</summary>
		/// <param name="initCapacity">åˆ�æœŸã‚µã‚¤ã‚º</param>
		public HashTable(int initCapacity)
		{
			// ã‚µã‚¤ã‚ºã�Œå¿…ã�š2ã�®ç´¯ä¹—å€¤ã�«ã�ªã‚‹ã‚ˆã�†ã�«ã�™ã‚‹
			int capacity = 1;
			while (capacity < initCapacity)
			{
				capacity <<= 1;
			}
			mElms = new HashTable.Element[capacity];
		}

		/// <summary>å…¨è¦�ç´ ã‚’å‰Šé™¤ã�™ã‚‹</summary>
		public virtual void Clear()
		{
			InternalClear();
		}

		/// <summary>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿå€¤ã‚’å¾—ã‚‹</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <returns>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿå€¤</returns>
		public virtual Value Get(Key key)
		{
			if (key == null)
			{
				return null;
			}
			HashTable.Element<Key, Value> e = InternalFindWithHash(key, key.GetHashCode());
			if (e == null)
			{
				return null;
			}
			return e.mValue;
		}

		/// <summary>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿå€¤ã‚’å¾—ã�¤ã�¤ã€�ä¸¦ã�³é †ã�§ä¸€ç•ªæ–°ã�—ã�„ã‚‚ã�®ã�¨ã�™ã‚‹
		/// 	</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <returns>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿå€¤</returns>
		public virtual Value GetAndTouch(Key key)
		{
			if (key == null)
			{
				return null;
			}
			HashTable.Element<Key, Value> e = InternalFindWithHash(key, key.GetHashCode());
			if (e == null)
			{
				return null;
			}
			CheckUpdateElementOrder(e);
			return e.mValue;
		}

		/// <summary>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿè¦�ç´ ã‚’æŽ¢ã�™</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <param name="hash">ãƒ�ãƒƒã‚·ãƒ¥å€¤</param>
		/// <returns>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿè¦�ç´ </returns>
		private HashTable.Element<Key, Value> InternalFindWithHash(Key key, int hash)
		{
			// find key ( hash )
			int mask = mElms.Length - 1;
			HashTable.Element<Key, Value> lv1 = mElms[hash & mask];
			if (lv1 == null)
			{
				return null;
			}
			if (hash == lv1.mHash && (lv1.mFlags & HASH_USING) != 0)
			{
				if (key.Equals(lv1.mKey))
				{
					return lv1;
				}
			}
			HashTable.Element<Key, Value> elm = lv1.mNext;
			while (elm != null)
			{
				if (hash == elm.mHash)
				{
					if (key.Equals(elm.mKey))
					{
						return elm;
					}
				}
				elm = elm.mNext;
			}
			return null;
		}

		// not found
		/// <summary>ã‚­ãƒ¼ã�¨å€¤ã�®ãƒšã‚¢ã‚’æ ¼ç´�ã�™ã‚‹</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <param name="value">æ ¼ç´�ã�™ã‚‹å€¤</param>
		public virtual void Put(Key key, Value value)
		{
			if (key == null)
			{
				return;
			}
			AddWithHash(key, key.GetHashCode(), value);
		}

		/// <summary>ã‚­ãƒ¼ã�¨å€¤ã�®ãƒšã‚¢ã‚’æ ¼ç´�ã�™ã‚‹</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <param name="hash">ãƒ�ãƒƒã‚·ãƒ¥å€¤</param>
		/// <param name="value">å€¤</param>
		private void AddWithHash(Key key, int hash, Value value)
		{
			int mask = mElms.Length - 1;
			int index = hash & mask;
			HashTable.Element<Key, Value> lv1 = mElms[index];
			if (lv1 == null)
			{
				lv1 = new HashTable.Element<Key, Value>();
				mElms[index] = lv1;
				lv1.mFlags = HASH_LV1;
			}
			HashTable.Element<Key, Value> elm = lv1.mNext;
			while (elm != null)
			{
				if (hash == elm.mHash)
				{
					if (key.Equals(elm.mKey))
					{
						CheckUpdateElementOrder(elm);
						elm.mValue = value;
						return;
					}
				}
				elm = elm.mNext;
			}
			if ((lv1.mFlags & HASH_USING) == 0)
			{
				lv1.mKey = key;
				lv1.mValue = value;
				lv1.mFlags |= HASH_USING;
				lv1.mHash = hash;
				lv1.mPrev = null;
				CheckAddingElementOrder(lv1);
				return;
			}
			if (hash == lv1.mHash)
			{
				if (key.Equals(lv1.mHash))
				{
					CheckUpdateElementOrder(lv1);
					lv1.mValue = value;
					return;
				}
			}
			// insert after lv1
			HashTable.Element<Key, Value> newelm = new HashTable.Element<Key, Value>();
			//newelm.mFlags = 0;
			newelm.mKey = key;
			newelm.mValue = value;
			newelm.mFlags |= HASH_USING;
			newelm.mHash = hash;
			if (lv1.mNext != null)
			{
				lv1.mNext.mPrev = newelm;
			}
			newelm.mNext = lv1.mNext;
			newelm.mPrev = lv1;
			lv1.mNext = newelm;
			CheckAddingElementOrder(newelm);
		}

		/// <summary>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿè¦�ç´ ã‚’å‰Šé™¤ã�™ã‚‹</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <returns>å®Ÿéš›ã�«å‰Šé™¤ã�—ã�Ÿã�‹ã�©ã�†ã�‹</returns>
		internal virtual bool Remove(Key key)
		{
			if (key == null)
			{
				return false;
			}
			return DeleteWithHash(key, key.GetHashCode());
		}

		/// <summary>ã‚­ãƒ¼ã�«å¯¾å¿œã�—ã�Ÿè¦�ç´ ã‚’å‰Šé™¤ã�™ã‚‹</summary>
		/// <param name="key">ã‚­ãƒ¼</param>
		/// <param name="hash">ãƒ�ãƒƒã‚·ãƒ¥å€¤</param>
		/// <returns>å®Ÿéš›ã�«å‰Šé™¤ã�—ã�Ÿã�‹ã�©ã�†ã�‹</returns>
		private bool DeleteWithHash(Key key, int hash)
		{
			// delete key ( hash ) and return true if succeeded
			int mask = mElms.Length - 1;
			HashTable.Element<Key, Value> lv1 = mElms[hash & mask];
			if (lv1 == null)
			{
				return false;
			}
			if ((lv1.mFlags & HASH_USING) != 0 && hash == lv1.mHash)
			{
				if (key.Equals(lv1.mKey))
				{
					// delete lv1
					CheckDeletingElementOrder(lv1);
					lv1.mKey = null;
					lv1.mValue = null;
					lv1.mFlags &= ~HASH_USING;
					return true;
				}
			}
			HashTable.Element<Key, Value> prev = lv1;
			HashTable.Element<Key, Value> elm = lv1.mNext;
			while (elm != null)
			{
				if (hash == elm.mHash)
				{
					if (key.Equals(elm.mKey))
					{
						CheckDeletingElementOrder(elm);
						elm.mKey = null;
						elm.mValue = null;
						elm.mFlags &= ~HASH_USING;
						prev.mNext = elm.mNext;
						// sever from the chain
						if (elm.mNext != null)
						{
							elm.mNext.mPrev = prev;
						}
						elm.mNext = null;
						elm.mPrev = null;
						return true;
					}
				}
				prev = elm;
				elm = elm.mNext;
			}
			return false;
		}

		// not found
		public virtual int GetCount()
		{
			return mCount;
		}

		/// <summary>å�¤ã�„ã‚‚ã�®ã�‹ã‚‰æŒ‡å®šå€‹æ•°å‰Šé™¤ã�™ã‚‹</summary>
		/// <param name="count">å‰Šé™¤ã�™ã‚‹æ•°</param>
		/// <returns>å®Ÿéš›ã�«å‰Šé™¤ã�—ã�Ÿæ•°</returns>
		public virtual int ChopLast(int count)
		{
			int ret = 0;
			while (count > 0)
			{
				count--;
				if (mNLast == null)
				{
					break;
				}
				DeleteBytElement(mNLast);
				ret++;
			}
			return ret;
		}

		/// <summary>æŒ‡å®šè¦�ç´ ã‚’å‰Šé™¤ã�™ã‚‹</summary>
		/// <param name="elm">å‰Šé™¤ã�™ã‚‹è¦�ç´ </param>
		/// <returns>é…�åˆ—è¦�ç´ ã�‹ã�©ã�†ã�‹</returns>
		private bool DeleteBytElement(HashTable.Element<Key, Value> elm)
		{
			CheckDeletingElementOrder(elm);
			elm.mKey = null;
			elm.mValue = null;
			elm.mFlags &= ~HASH_USING;
			if ((elm.mFlags & HASH_LV1) != 0)
			{
				// lv1 element
				// nothing to do
				return false;
			}
			else
			{
				// other elements
				if (elm.mPrev != null)
				{
					elm.mPrev.mNext = elm.mNext;
				}
				if (elm.mNext != null)
				{
					elm.mNext.mPrev = elm.mPrev;
				}
				return true;
			}
		}

		/// <summary>è¦�ç´ å‰Šé™¤ã�«ä¼´ã�„ä¸¦ã�³é †ã‚’æ›´æ–°ã�™ã‚‹</summary>
		/// <param name="elm">å‰Šé™¤å¯¾è±¡è¦�ç´ </param>
		private void CheckDeletingElementOrder(HashTable.Element<Key, Value> elm)
		{
			mCount--;
			if (mCount > 0)
			{
				if (elm == mNFirst)
				{
					// deletion of first item
					mNFirst = elm.mNNext;
					mNFirst.mNPrev = null;
				}
				else
				{
					if (elm == mNLast)
					{
						// deletion of last item
						mNLast = elm.mNPrev;
						mNLast.mNNext = null;
					}
					else
					{
						// deletion of intermediate item
						elm.mNPrev.mNNext = elm.mNNext;
						elm.mNNext.mNPrev = elm.mNPrev;
					}
				}
			}
			else
			{
				// when the count becomes zero...
				mNFirst = mNLast = null;
			}
		}

		/// <summary>æŒ‡å®šè¦�ç´ ã�®ä¸¦ã�³é †ã‚’æ›´æ–°ã�™ã‚‹</summary>
		/// <param name="elm">å…ˆé ­ã�«æŒ�ã�£ã�¦ã��ã‚‹è¦�ç´ </param>
		private void CheckUpdateElementOrder(HashTable.Element<Key, Value> elm)
		{
			// move elm to the front of addtional order
			if (elm != mNFirst)
			{
				if (mNLast == elm)
				{
					mNLast = elm.mNPrev;
				}
				elm.mNPrev.mNNext = elm.mNNext;
				if (elm.mNNext != null)
				{
					elm.mNNext.mNPrev = elm.mNPrev;
				}
				elm.mNNext = mNFirst;
				elm.mNPrev = null;
				mNFirst.mNPrev = elm;
				mNFirst = elm;
			}
		}

		/// <summary>è¦�ç´ è¿½åŠ ã�«ä¼´ã�„ä¸¦ã�³é †ã‚’æ›´æ–°ã�™ã‚‹</summary>
		/// <param name="elm">è¿½åŠ ã�™ã‚‹è¦�ç´ </param>
		private void CheckAddingElementOrder(HashTable.Element<Key, Value> elm)
		{
			if (mCount == 0)
			{
				mNLast = elm;
				// first addition
				elm.mNNext = null;
			}
			else
			{
				mNFirst.mNPrev = elm;
				elm.mNNext = mNFirst;
			}
			mNFirst = elm;
			elm.mNPrev = null;
			mCount++;
		}

		/// <summary>åˆ�æœŸåŒ–</summary>
		private void InternalInit()
		{
			mCount = 0;
			mNFirst = null;
			mNLast = null;
		}

		/// <summary>å…¨è¦�ç´ ã‚¯ãƒªã‚¢</summary>
		private void InternalClear()
		{
			int count = mElms.Length;
			for (int i = 0; i < count; i++)
			{
				if (mElms[i] != null)
				{
					HashTable.Element<Key, Value> e = mElms[i].mNext;
					while (e != null)
					{
						e.mKey = null;
						e.mValue = null;
						e.mFlags &= ~HASH_USING;
						HashTable.Element<Key, Value> next = e.mNext;
						e.mPrev = null;
						e.mNext = null;
						e.mNPrev = null;
						e.mNNext = null;
						e = next;
					}
					e = mElms[i];
					if ((e.mFlags & HASH_USING) != 0)
					{
						e.mKey = null;
						e.mValue = null;
						e.mFlags &= ~HASH_USING;
						e.mPrev = null;
						e.mNext = null;
						e.mNPrev = null;
						e.mNNext = null;
					}
				}
			}
			InternalInit();
		}

		public virtual Value GetLastValue()
		{
			if (mNLast != null)
			{
				return mNLast.mValue;
			}
			return null;
		}
	}
}
