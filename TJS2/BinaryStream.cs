/*
 * The TJS2 interpreter from kirikirij
 */

using System.IO;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	/// <summary>ãƒ�ã‚¤ãƒŠãƒªã‚¹ãƒˆãƒªãƒ¼ãƒ èª­ã�¿æ›¸ã��ã‚¯ãƒ©ã‚¹</summary>
	public abstract class BinaryStream
	{
		/// <summary>èª­ã�¿è¾¼ã�¿ãƒ¢ãƒ¼ãƒ‰</summary>
		public const int READ = 0;

		/// <summary>æ›¸ã��è¾¼ã�¿ãƒ¢ãƒ¼ãƒ‰</summary>
		public const int WRITE = 1;

		/// <summary>è¿½è¨˜ãƒ¢ãƒ¼ãƒ‰</summary>
		public const int APPEND = 2;

		/// <summary>æ›´æ–°ãƒ¢ãƒ¼ãƒ‰</summary>
		public const int UPDATE = 3;

		/// <summary>ã‚¢ã‚¯ã‚»ã‚¹ãƒ¢ãƒ¼ãƒ‰ãƒžã‚¹ã‚¯</summary>
		public const int ACCESS_MASK = unchecked((int)(0x0f));

		/// <summary>å…ˆé ­ã�‹ã‚‰ã�®ã‚·ãƒ¼ã‚¯</summary>
		public const int SEEK_SET = 0;

		/// <summary>ç�¾åœ¨ä½�ç½®ã�‹ã‚‰ã�®ã‚·ãƒ¼ã‚¯</summary>
		public const int SEEK_CUR = 1;

		/// <summary>çµ‚ç«¯ä½�ç½®ã�‹ã‚‰ã�®ã‚·ãƒ¼ã‚¯</summary>
		public const int SEEK_END = 2;

		/// <summary>
		/// ã‚·ãƒ¼ã‚¯ã�™ã‚‹
		/// ã‚¨ãƒ©ãƒ¼æ™‚ã€�ä½�ç½®ã�¯å¤‰æ›´ã�•ã‚Œã�ªã�„
		/// </summary>
		/// <param name="offset">åŸºæº–ä½�ç½®ã�‹ã‚‰ã�®ã‚ªãƒ•ã‚»ãƒƒãƒˆ</param>
		/// <param name="whence">åŸºæº–ä½�ç½®ã€�SEEK_SET, SEEK_CUR, SEEK_END ã�®ã�„ã�šã‚Œã�‹ã‚’æŒ‡å®š
		/// 	</param>
		/// <returns>ã‚·ãƒ¼ã‚¯å¾Œã�®ç�¾åœ¨ä½�ç½®</returns>
		/// <exception cref="TJSException">TJSException</exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public abstract long Seek(long offset, int whence);

		/// <summary>returns actually read size</summary>
		/// <exception cref="TJSException">TJSException</exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public abstract int Read(ByteBuffer buffer);

		/// <summary>returns actually read size</summary>
		/// <exception cref="TJSException">TJSException</exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public abstract int Read(byte[] buffer);

		/// <summary>ã‚¹ãƒˆãƒªãƒ¼ãƒ ã�‹ã‚‰ã�®èª­ã�¿è¾¼ã�¿</summary>
		/// <param name="b">èª­ã�¿è¾¼ã�¿å…ˆbyteé…�åˆ—</param>
		/// <param name="off">é…�åˆ—ã‚ªãƒ•ã‚»ãƒƒãƒˆ</param>
		/// <param name="len">èª­ã�¿è¾¼ã�¿ã‚µã‚¤ã‚º</param>
		/// <returns>å®Ÿéš›ã�«èª­ã�¿è¾¼ã‚“ã� é•·ã�•ã€‚-1 ã�®æ™‚ãƒ•ã‚¡ã‚¤ãƒ«çµ‚ç«¯</returns>
		/// <exception cref="TJSException">TJSException</exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public abstract int Read(byte[] b, int off, int len);

		/// <summary>returns actually written size</summary>
		public abstract int Write(ByteBuffer buffer);

		/// <summary>returns actually written size</summary>
		public abstract int Write(byte[] buffer);

		public abstract void Write(byte[] b, int off, int len);

		/// <summary>
		/// 1 ãƒ�ã‚¤ãƒˆã�Œå‡ºåŠ›ã‚¹ãƒˆãƒªãƒ¼ãƒ ã�«æ›¸ã��è¾¼ã�¾ã‚Œã�¾ã�™ã€‚
		/// æ›¸ã��è¾¼ã�¾ã‚Œã‚‹ãƒ�ã‚¤ãƒˆã�¯ã€�å¼•æ•° b ã�®ä¸‹ä½� 8 ãƒ“ãƒƒãƒˆã�§ã�™ã€‚
		/// b ã�®ä¸Šä½� 24 ãƒ“ãƒƒãƒˆã�¯ç„¡è¦–ã�•ã‚Œã�¾ã�™ã€‚
		/// </summary>
		/// <param name="b"></param>
		public abstract void Write(int b);

		/// <summary>close stream</summary>
		public abstract void Close();

		/// <summary>
		/// the default behavior is raising a exception
		/// if error, raises exception
		/// </summary>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void SetEndOfStorage()
		{
			throw new TJSException(Error.WriteError);
		}

		/// <summary>should re-implement for higher performance</summary>
		/// <exception cref="TJSException">TJSException</exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual long GetSize()
		{
			long orgpos = GetPosition();
			long size = Seek(0, SEEK_END);
			Seek(orgpos, SEEK_SET);
			return size;
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual long GetPosition()
		{
			return Seek(0, SEEK_CUR);
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void SetPosition(long pos)
		{
			if (pos != Seek(pos, SEEK_SET))
			{
				throw new TJSException(Error.SeekError);
			}
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void ReadBuffer(ByteBuffer buffer)
		{
			if (Read(buffer) != -1)
			{
				throw new TJSException(Error.ReadError);
			}
			buffer.Flip();
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual void WriteBuffer(ByteBuffer buffer)
		{
			if (Write(buffer) != -1)
			{
				throw new TJSException(Error.WriteError);
			}
		}

		public abstract InputStream GetInputStream();

		public abstract OutputStream GetOutputStream();

		/// <summary>ã‚¢ãƒ¼ã‚«ã‚¤ãƒ–å†…ã�®ãƒ•ã‚¡ã‚¤ãƒ«ã�‹ã�©ã�†ã�‹åˆ¤å®šã�™ã‚‹</summary>
		/// <returns></returns>
		public virtual bool IsArchive()
		{
			return false;
		}

		public abstract string GetFilePath();

		public virtual FileDescriptor GetFileDescriptor()
		{
			return null;
		}

		public virtual long GetFileOffset()
		{
			return 0;
		}
	}
}
