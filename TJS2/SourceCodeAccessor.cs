/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public interface SourceCodeAccessor
	{
		/// <summary>ãƒ�ã‚¤ãƒˆã‚³ãƒ¼ãƒ‰ä½�ç½®ã�‹ã‚‰ã‚½ãƒ¼ã‚¹ã‚³ãƒ¼ãƒ‰ä½�ç½®ã‚’å¾—ã‚‹</summary>
		/// <param name="codepos">ãƒ�ã‚¤ãƒˆã‚³ãƒ¼ãƒ‰ä½�ç½®</param>
		/// <returns>ã‚½ãƒ¼ã‚¹ã‚³ãƒ¼ãƒ‰ä½�ç½®</returns>
		int CodePosToSrcPos(int codepos);

		/// <summary>ã‚½ãƒ¼ã‚¹ã‚³ãƒ¼ãƒ‰ä½�ç½®ã�‹ã‚‰è¡Œæ•°ã‚’å¾—ã‚‹</summary>
		/// <param name="srcpos">ã‚½ãƒ¼ã‚¹ã‚³ãƒ¼ãƒ‰ä½�ç½®</param>
		/// <returns>è¡Œæ•°</returns>
		int SrcPosToLine(int srcpos);

		/// <summary>æŒ‡å®šãƒ©ã‚¤ãƒ³ã�®æ–‡å­—åˆ—ã‚’å¾—ã‚‹</summary>
		/// <param name="line">è¡Œæ•°</param>
		/// <returns>æŒ‡å®šè¡Œã�®æ–‡å­—åˆ—</returns>
		string GetLine(int line);

		/// <summary>ã‚¹ã‚¯ãƒªãƒ—ãƒˆã‚’å…¨æ–‡å�–å¾—ã�™ã‚‹</summary>
		/// <returns>ã‚¹ã‚¯ãƒªãƒ—ãƒˆæ–‡</returns>
		string GetScript();

		/// <summary>ã‚¹ã‚¯ãƒªãƒ—ãƒˆé–‹å§‹ã‚ªãƒ•ã‚»ãƒƒãƒˆã‚’å�–å¾—ã�™ã‚‹</summary>
		/// <returns>ã‚ªãƒ•ã‚»ãƒƒãƒˆ</returns>
		int GetLineOffset();

		/// <summary>ã‚¹ã‚¯ãƒªãƒ—ãƒˆå��ã‚’å�–å¾—ã�™ã‚‹</summary>
		/// <returns>ã‚¹ã‚¯ãƒªãƒ—ãƒˆå��</returns>
		string GetName();
	}
}
