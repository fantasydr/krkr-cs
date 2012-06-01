/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class Error
	{
		public static readonly string InternalError = "å†…éƒ¨ã‚¨ãƒ©ãƒ¼ã�Œç™ºç”Ÿã�—ã�¾ã�—ã�Ÿ";

		public static readonly string Warning = "è­¦å‘Š: ";

		public static readonly string WarnEvalOperator = "ã‚°ãƒ­ãƒ¼ãƒ�ãƒ«ã�§ã�ªã�„å ´æ‰€ã�§å¾Œç½® ! æ¼”ç®—å­�ã�Œä½¿ã‚�ã‚Œã�¦ã�„ã�¾ã�™(ã�“ã�®æ¼”ç®—å­�ã�®æŒ™å‹•ã�¯TJS2 version 2.4.1 ã�§å¤‰ã‚�ã‚Šã�¾ã�—ã�Ÿã�®ã�§ã�”æ³¨æ„�ã��ã� ã�•ã�„)";

		public static readonly string NarrowToWideConversionError = "ANSI æ–‡å­—åˆ—ã‚’ UNICODE æ–‡å­—åˆ—ã�«å¤‰æ�›ã�§ã��ã�¾ã�›ã‚“ã€‚ç�¾åœ¨ã�®ã‚³ãƒ¼ãƒ‰ãƒšãƒ¼ã‚¸ã�§è§£é‡ˆã�§ã��ã�ªã�„æ–‡å­—ã�Œå�«ã�¾ã‚Œã�¦ã�¾ã�™ã€‚æ­£ã�—ã�„ãƒ‡ãƒ¼ã‚¿ã�ŒæŒ‡å®šã�•ã‚Œã�¦ã�„ã‚‹ã�‹ã‚’ç¢ºèª�ã�—ã�¦ã��ã� ã�•ã�„ã€‚ãƒ‡ãƒ¼ã‚¿ã�Œç ´æ��ã�—ã�¦ã�„ã‚‹å�¯èƒ½æ€§ã‚‚ã�‚ã‚Šã�¾ã�™";

		public static readonly string VariantConvertError = "%1 ã�‹ã‚‰ %2 ã�¸åž‹ã‚’å¤‰æ�›ã�§ã��ã�¾ã�›ã‚“";

		public static readonly string VariantConvertErrorToObject = "%1 ã�‹ã‚‰ Object ã�¸åž‹ã‚’å¤‰æ�›ã�§ã��ã�¾ã�›ã‚“ã€‚Object åž‹ã�Œè¦�æ±‚ã�•ã‚Œã‚‹æ–‡è„ˆã�§ Object åž‹ä»¥å¤–ã�®å€¤ã�Œæ¸¡ã�•ã‚Œã‚‹ã�¨ã�“ã�®ã‚¨ãƒ©ãƒ¼ã�Œç™ºç”Ÿã�—ã�¾ã�™";

		public static readonly string IDExpected = "è­˜åˆ¥å­�ã‚’æŒ‡å®šã�—ã�¦ã��ã� ã�•ã�„";

		public static readonly string SubstitutionInBooleanContext = "è«–ç�†å€¤ã�Œæ±‚ã‚�ã‚‰ã‚Œã�¦ã�„ã‚‹å ´æ‰€ã�§ = æ¼”ç®—å­�ã�Œä½¿ç”¨ã�•ã‚Œã�¦ã�„ã�¾ã�™(== æ¼”ç®—å­�ã�®é–“é�•ã�„ã�§ã�™ã�‹ï¼Ÿä»£å…¥ã�—ã�Ÿä¸Šã�§ã‚¼ãƒ­ã�¨å€¤ã‚’æ¯”è¼ƒã�—ã�Ÿã�„å ´å�ˆã�¯ã€�(A=B) != 0 ã�®å½¢å¼�ã‚’ä½¿ã�†ã�“ã�¨ã‚’ã�Šå‹§ã‚�ã�—ã�¾ã�™)";

		public static readonly string CannotModifyLHS = "ä¸�æ­£ã�ªä»£å…¥ã�‹ä¸�æ­£ã�ªå¼�ã�®æ“�ä½œã�§ã�™";

		public static readonly string InsufficientMem = "ãƒ¡ãƒ¢ãƒªã�Œè¶³ã‚Šã�¾ã�›ã‚“";

		public static readonly string CannotGetResult = "ã�“ã�®å¼�ã�‹ã‚‰ã�¯å€¤ã‚’å¾—ã‚‹ã�“ã�¨ã�Œã�§ã��ã�¾ã�›ã‚“";

		public static readonly string NullAccess = "null ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã�«ã‚¢ã‚¯ã‚»ã‚¹ã�—ã‚ˆã�†ã�¨ã�—ã�¾ã�—ã�Ÿ";

		public static readonly string MemberNotFound = "ãƒ¡ãƒ³ãƒ� \"%1\" ã�Œè¦‹ã�¤ã�‹ã‚Šã�¾ã�›ã‚“";

		public static readonly string MemberNotFoundNoNameGiven = "ãƒ¡ãƒ³ãƒ�ã�Œè¦‹ã�¤ã�‹ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotImplemented = "å‘¼ã�³å‡ºã��ã�†ã�¨ã�—ã�Ÿæ©Ÿèƒ½ã�¯æœªå®Ÿè£…ã�§ã�™";

		public static readonly string InvalidParam = "ä¸�æ­£ã�ªå¼•æ•°ã�§ã�™";

		public static readonly string BadParamCount = "å¼•æ•°ã�®æ•°ã�Œä¸�æ­£ã�§ã�™";

		public static readonly string InvalidType = "é–¢æ•°ã�§ã�¯ã�ªã�„ã�‹ãƒ—ãƒ­ãƒ‘ãƒ†ã‚£ã�®ç¨®é¡žã�Œé�•ã�„ã�¾ã�™";

		public static readonly string SpecifyDicOrArray = "Dictionary ã�¾ã�Ÿã�¯ Array ã‚¯ãƒ©ã‚¹ã�®ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã‚’æŒ‡å®šã�—ã�¦ã��ã� ã�•ã�„";

		public static readonly string SpecifyArray = "Array ã‚¯ãƒ©ã‚¹ã�®ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã‚’æŒ‡å®šã�—ã�¦ã��ã� ã�•ã�„";

		public static readonly string StringDeallocError = "æ–‡å­—åˆ—ãƒ¡ãƒ¢ãƒªãƒ–ãƒ­ãƒƒã‚¯ã‚’è§£æ”¾ã�§ã��ã�¾ã�›ã‚“";

		public static readonly string StringAllocError = "æ–‡å­—åˆ—ãƒ¡ãƒ¢ãƒªãƒ–ãƒ­ãƒƒã‚¯ã‚’ç¢ºä¿�ã�§ã��ã�¾ã�›ã‚“";

		public static readonly string MisplacedBreakContinue = "\"break\" ã�¾ã�Ÿã�¯ \"continue\" ã�¯ã�“ã�“ã�«æ›¸ã��ã�“ã�¨ã�¯ã�§ã��ã�¾ã�›ã‚“";

		public static readonly string MisplacedCase = "\"case\" ã�¯ã�“ã�“ã�«æ›¸ã��ã�“ã�¨ã�¯ã�§ã��ã�¾ã�›ã‚“";

		public static readonly string MisplacedReturn = "\"return\" ã�¯ã�“ã�“ã�«æ›¸ã��ã�“ã�¨ã�¯ã�§ã��ã�¾ã�›ã‚“";

		public static readonly string StringParseError = "æ–‡å­—åˆ—å®šæ•°/æ­£è¦�è¡¨ç�¾/ã‚ªã‚¯ãƒ†ãƒƒãƒˆå�³å€¤ã�Œçµ‚ã‚�ã‚‰ã�ªã�„ã�¾ã�¾ã‚¹ã‚¯ãƒªãƒ—ãƒˆã�®çµ‚ç«¯ã�«é�”ã�—ã�¾ã�—ã�Ÿ";

		public static readonly string NumberError = "æ•°å€¤ã�¨ã�—ã�¦è§£é‡ˆã�§ã��ã�¾ã�›ã‚“";

		public static readonly string UnclosedComment = "ã‚³ãƒ¡ãƒ³ãƒˆã�Œçµ‚ã‚�ã‚‰ã�ªã�„ã�¾ã�¾ã‚¹ã‚¯ãƒªãƒ—ãƒˆã�®çµ‚ç«¯ã�«é�”ã�—ã�¾ã�—ã�Ÿ";

		public static readonly string InvalidChar = "ä¸�æ­£ã�ªæ–‡å­—ã�§ã�™ : \'%1\'";

		public static readonly string Expected = "%1 ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string SyntaxError = "æ–‡æ³•ã‚¨ãƒ©ãƒ¼ã�§ã�™(%1)";

		public static readonly string PPError = "æ�¡ä»¶ã‚³ãƒ³ãƒ‘ã‚¤ãƒ«å¼�ã�«ã‚¨ãƒ©ãƒ¼ã�Œã�‚ã‚Šã�¾ã�™";

		public static readonly string CannotGetSuper = "ã‚¹ãƒ¼ãƒ‘ãƒ¼ã‚¯ãƒ©ã‚¹ã�Œå­˜åœ¨ã�—ã�ªã�„ã�‹ã‚¹ãƒ¼ãƒ‘ãƒ¼ã‚¯ãƒ©ã‚¹ã‚’ç‰¹å®šã�§ã��ã�¾ã�›ã‚“";

		public static readonly string InvalidOpecode = "ä¸�æ­£ã�ª VM ã‚³ãƒ¼ãƒ‰ã�§ã�™";

		public static readonly string RangeError = "å€¤ã�Œç¯„å›²å¤–ã�§ã�™";

		public static readonly string AccessDenyed = "èª­ã�¿è¾¼ã�¿å°‚ç”¨ã�‚ã‚‹ã�„ã�¯æ›¸ã��è¾¼ã�¿å°‚ç”¨ãƒ—ãƒ­ãƒ‘ãƒ†ã‚£ã�«å¯¾ã�—ã�¦è¡Œã�ˆã�ªã�„æ“�ä½œã‚’ã�—ã‚ˆã�†ã�¨ã�—ã�¾ã�—ã�Ÿ";

		public static readonly string NativeClassCrash = "å®Ÿè¡Œã‚³ãƒ³ãƒ†ã‚­ã‚¹ãƒˆã�Œé�•ã�„ã�¾ã�™";

		public static readonly string InvalidObject = "ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã�¯ã�™ã�§ã�«ç„¡åŠ¹åŒ–ã�•ã‚Œã�¦ã�„ã�¾ã�™";

		public static readonly string CannotOmit = "\"...\" ã�¯é–¢æ•°å¤–ã�§ã�¯ä½¿ã�ˆã�¾ã�›ã‚“";

		public static readonly string CannotParseDate = "ä¸�æ­£ã�ªæ—¥ä»˜æ–‡å­—åˆ—ã�®å½¢å¼�ã�§ã�™";

		public static readonly string InvalidValueForTimestamp = "ä¸�æ­£ã�ªæ—¥ä»˜ãƒ»æ™‚åˆ»ã�§ã�™";

		public static readonly string ExceptionNotFound = "\"Exception\" ã�Œå­˜åœ¨ã�—ã�ªã�„ã�Ÿã‚�ä¾‹å¤–ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã‚’ä½œæˆ�ã�§ã��ã�¾ã�›ã‚“";

		public static readonly string InvalidFormatString = "ä¸�æ­£ã�ªæ›¸å¼�æ–‡å­—åˆ—ã�§ã�™";

		public static readonly string DivideByZero = "0 ã�§é™¤ç®—ã‚’ã�—ã‚ˆã�†ã�¨ã�—ã�¾ã�—ã�Ÿ";

		public static readonly string NotReconstructiveRandomizeData = "ä¹±æ•°ç³»åˆ—ã‚’åˆ�æœŸåŒ–ã�§ã��ã�¾ã�›ã‚“(ã�Šã��ã‚‰ã��ä¸�æ­£ã�ªãƒ‡ãƒ¼ã‚¿ã�Œæ¸¡ã�•ã‚Œã�¾ã�—ã�Ÿ)";

		public static readonly string Symbol = "è­˜åˆ¥å­�";

		public static readonly string CallHistoryIsFromOutOfTJS2Script = "[TJSã‚¹ã‚¯ãƒªãƒ—ãƒˆç®¡ç�†å¤–]";

		public static readonly string NObjectsWasNotFreed = "å�ˆè¨ˆ %1 å€‹ã�®ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã�Œè§£æ”¾ã�•ã‚Œã�¦ã�„ã�¾ã�›ã‚“";

		public static readonly string ObjectCreationHistoryDelimiter = "\n                     ";

		public static readonly string ObjectWasNotFreed = "ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆ %1 [%2] ã�Œè§£æ”¾ã�•ã‚Œã�¦ã�„ã�¾ã�›ã‚“ã€‚ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆä½œæˆ�æ™‚ã�®å‘¼ã�³å‡ºã�—å±¥æ­´ã�¯ä»¥ä¸‹ã�®é€šã‚Šã�§ã�™:\n                     %3";

		public static readonly string GroupByObjectTypeAndHistory = "ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã�®ã‚¿ã‚¤ãƒ—ã�¨ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆä½œæˆ�æ™‚ã�®å±¥æ­´ã�«ã‚ˆã‚‹åˆ†é¡ž";

		public static readonly string GroupByObjectType = "ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã�®ã‚¿ã‚¤ãƒ—ã�«ã‚ˆã‚‹åˆ†é¡ž";

		public static readonly string ObjectCountingMessageGroupByObjectTypeAndHistory = 
			"%1 å€‹ : [%2]\n                     %3";

		public static readonly string ObjectCountingMessageTJSGroupByObjectType = "%1 å€‹ : [%2]";

		public static readonly string WarnRunningCodeOnDeletingObject = "%4: å‰Šé™¤ä¸­ã�®ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆ %1[%2] ä¸Šã�§ã‚³ãƒ¼ãƒ‰ã�Œå®Ÿè¡Œã�•ã‚Œã�¦ã�„ã�¾ã�™ã€‚ã�“ã�®ã‚ªãƒ–ã‚¸ã‚§ã‚¯ãƒˆã�®ä½œæˆ�æ™‚ã�®å‘¼ã�³å‡ºã�—å±¥æ­´ã�¯ä»¥ä¸‹ã�®é€šã‚Šã�§ã�™:\n                     %3";

		public static readonly string WriteError = "æ›¸ã��è¾¼ã�¿ã‚¨ãƒ©ãƒ¼ã�Œç™ºç”Ÿã�—ã�¾ã�—ã�Ÿ";

		public static readonly string ReadError = "èª­ã�¿è¾¼ã�¿ã‚¨ãƒ©ãƒ¼ã�Œç™ºç”Ÿã�—ã�¾ã�—ã�Ÿã€‚ãƒ•ã‚¡ã‚¤ãƒ«ã�Œç ´æ��ã�—ã�¦ã�„ã‚‹å�¯èƒ½æ€§ã‚„ã€�ãƒ‡ãƒ�ã‚¤ã‚¹ã�‹ã‚‰ã�®èª­ã�¿è¾¼ã�¿ã�«å¤±æ•—ã�—ã�Ÿå�¯èƒ½æ€§ã�Œã�‚ã‚Šã�¾ã�™";

		public static readonly string SeekError = "ã‚·ãƒ¼ã‚¯ã‚¨ãƒ©ãƒ¼ã�Œç™ºç”Ÿã�—ã�¾ã�—ã�Ÿã€‚ãƒ•ã‚¡ã‚¤ãƒ«ã�Œç ´æ��ã�—ã�¦ã�„ã‚‹å�¯èƒ½æ€§ã‚„ã€�ãƒ‡ãƒ�ã‚¤ã‚¹ã�‹ã‚‰ã�®èª­ã�¿è¾¼ã�¿ã�«å¤±æ•—ã�—ã�Ÿå�¯èƒ½æ€§ã�Œã�‚ã‚Šã�¾ã�™";

		public static readonly string TooManyErrors = "Too many errors";

		public static readonly string ConstDicDelimiterError = "å®šæ•°è¾žæ›¸(const Dictionary)ã�§è¦�ç´ å��ã�¨å€¤ã�®åŒºåˆ‡ã‚Šã�Œä¸�æ­£ã�§ã�™";

		public static readonly string ConstDicValueError = "å®šæ•°è¾žæ›¸(const Dictionary)ã�®è¦�ç´ å€¤ã�Œä¸�æ­£ã�§ã�™";

		public static readonly string ConstArrayValueError = "å®šæ•°é…�åˆ—(const Array)ã�®è¦�ç´ å€¤ã�Œä¸�æ­£ã�§ã�™";

		public static readonly string ConstDicArrayStringError = "å®šæ•°è¾žæ›¸ã‚‚ã�—ã��ã�¯é…�åˆ—ã�§(const)æ–‡å­—ã�Œä¸�æ­£ã�§ã�™";

		public static readonly string ConstDicLBRACKETError = "å®šæ•°è¾žæ›¸(const Dictionary)ã�§(const)%ã�®å¾Œã�«\"[\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string ConstArrayLBRACKETError = "å®šæ•°é…�åˆ—(const Array)ã�§(const)ã�®å¾Œã�«\"[\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string DicDelimiterError = "è¾žæ›¸(Dictionary)ã�§è¦�ç´ å��ã�¨å€¤ã�®åŒºåˆ‡ã‚Šã�Œä¸�æ­£ã�§ã�™";

		public static readonly string DicError = "è¾žæ›¸(Dictionary)ã�Œä¸�æ­£ã�§ã�™";

		public static readonly string DicLBRACKETError = "è¾žæ›¸(Dictionary)ã�§%ã�®å¾Œã�«\"[\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string DicRBRACKETError = "è¾žæ›¸(Dictionary)ã�®çµ‚ç«¯ã�«\"]\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string ArrayRBRACKETError = "é…�åˆ—(Array)ã�®çµ‚ç«¯ã�«\"]\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundRegexError = "æ­£è¦�è¡¨ç�¾ã�Œè¦�æ±‚ã�•ã‚Œã‚‹æ–‡è„ˆã�§æ­£è¦�è¡¨ç�¾ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSymbolAfterDotError = "\".\"ã�®å¾Œã�«ã‚·ãƒ³ãƒœãƒ«ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundDicOrArrayRBRACKETError = "é…�åˆ—ã‚‚ã�—ã��ã�¯è¾žæ›¸è¦�ç´ ã‚’æŒ‡ã�™å¤‰æ•°ã�®çµ‚ç«¯ã�«\"]\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundRPARENTHESISError = "\")\"ã�Œè¦�æ±‚ã�•ã‚Œã‚‹æ–‡è„ˆã�§\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSemicolonAfterThrowError = "throwã�®å¾Œã�®\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundRPARENTHESISAfterCatchError = "catchã�®å¾Œã�®\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundCaseOrDefaultError = "caseã�‹defaultã�Œè¦�æ±‚ã�•ã‚Œã‚‹æ–‡è„ˆã�§caseã�‹defaultã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundWithLPARENTHESISError = "withã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundWithRPARENTHESISError = "withã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSwitchLPARENTHESISError = "switchã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSwitchRPARENTHESISError = "switchã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSemicolonAfterReturnError = "returnã�®å¾Œã�®\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundPropGetRPARENTHESISError = "property getterã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundPropSetLPARENTHESISError = "property setterã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundPropSetRPARENTHESISError = "property setterã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundPropError = "propertyã�®å¾Œã�«\"getter\"ã‚‚ã�—ã��ã�¯\"setter\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSymbolAfterPropError = "propertyã�®å¾Œã�«ã‚·ãƒ³ãƒœãƒ«ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundLBRACEAfterPropError = "propertyã�®å¾Œã�«\"{\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundRBRACEAfterPropError = "propertyã�®å¾Œã�«\"}\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundFuncDeclRPARENTHESISError = "é–¢æ•°å®šç¾©ã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundFuncDeclSymbolError = "é–¢æ•°å®šç¾©ã�«ã‚·ãƒ³ãƒœãƒ«å��ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSymbolAfterVarError = "å¤‰æ•°å®£è¨€ã�«ã‚·ãƒ³ãƒœãƒ«ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundForLPARENTHESISError = "forã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundForRPARENTHESISError = "forã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundForSemicolonError = "forã�®å�„ç¯€ã�®åŒºåˆ‡ã‚Šã�«\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundIfLPARENTHESISError = "ifã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundIfRPARENTHESISError = "ifã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundDoWhileLPARENTHESISError = "do-whileã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundDoWhileRPARENTHESISError = "do-whileã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundDoWhileError = "do-whileæ–‡ã�§whileã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundDoWhileSemicolonError = "do-whileæ–‡ã�§whileã�®å¾Œã�«\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundWhileLPARENTHESISError = "whileã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundWhileRPARENTHESISError = "whileã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundLBRACEAfterBlockError = "ãƒ–ãƒ­ãƒƒã‚¯ã�Œè¦�æ±‚ã�•ã‚Œã‚‹æ–‡è„ˆã�§\"{\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundRBRACEAfterBlockError = "ãƒ–ãƒ­ãƒƒã‚¯ã�Œè¦�æ±‚ã�•ã‚Œã‚‹æ–‡è„ˆã�§\"}\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSemicolonError = "æ–‡ã�®çµ‚ã‚�ã‚Šã�«\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSemicolonOrTokenTypeError = "æ–‡ã�®çµ‚ã‚�ã‚Šã�«\";\"ã�Œã�ªã�„ã�‹ã€�äºˆç´„èªžã�®ã‚¿ã‚¤ãƒ—ãƒŸã‚¹ã�§ã�™";

		public static readonly string NotFoundBlockRBRACEError = "ãƒ–ãƒ­ãƒƒã‚¯ã�®çµ‚ã‚�ã‚Šã�«\"}\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundCatchError = "tryã�®å¾Œã�«catchã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundFuncCallLPARENTHESISError = "é–¢æ•°å‘¼ã�³å‡ºã�—ã�®å¾Œã�«\"(\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundFuncCallRPARENTHESISError = "é–¢æ•°å‘¼ã�³å‡ºã�—ã�®å¾Œã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundVarSemicolonError = "å¤‰æ•°å®£è¨€ã�®å¾Œã�«\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFound3ColonError = "æ�¡ä»¶æ¼”ç®—å­�ã�®\":\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundCaseColonError = "caseã�®å¾Œã�«\":\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundDefaultColonError = "defaultã�®å¾Œã�«\":\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundSymbolAfterClassError = "classã�®å¾Œã�«ã‚·ãƒ³ãƒœãƒ«ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundPropSetSymbolError = "property setterã�®å¼•æ•°ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundBreakSemicolonError = "breakã�®å¾Œã�«\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundContinueSemicolonError = "continueã�®å¾Œã�«\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundBebuggerSemicolonError = "debuggerã�®å¾Œã�«\";\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string NotFoundAsteriskAfterError = "é–¢æ•°å‘¼ã�³å‡ºã�—ã€�é–¢æ•°å®šç¾©ã�®é…�åˆ—å±•é–‹(*)ã�Œä¸�æ­£ã�‹ã€�ä¹—ç®—ã�Œä¸�æ­£ã�§ã�™";

		public static readonly string EndOfBlockError = "ãƒ–ãƒ­ãƒƒã‚¯ã�®å¯¾å¿œã�Œå�–ã‚Œã�¦ã�„ã�¾ã�›ã‚“ã€‚\"}\"ã�Œå¤šã�„ã�§ã�™";

		public static readonly string NotFoundPreprocessorRPARENTHESISError = "ãƒ—ãƒªãƒ—ãƒ­ã‚»ãƒƒã‚µã�«\")\"ã�Œã�‚ã‚Šã�¾ã�›ã‚“";

		public static readonly string PreprocessorZeroDiv = "ãƒ—ãƒªãƒ—ãƒ­ã‚»ãƒƒã‚µã�®ã‚¼ãƒ­é™¤ç®—ã‚¨ãƒ©ãƒ¼";

		public static readonly string ByteCodeBroken = "ãƒ�ã‚¤ãƒˆã‚³ãƒ¼ãƒ‰ãƒ•ã‚¡ã‚¤ãƒ«èª­ã�¿è¾¼ã�¿ã‚¨ãƒ©ãƒ¼ã€‚ãƒ•ã‚¡ã‚¤ãƒ«ã�Œå£Šã‚Œã�¦ã�„ã‚‹ã�‹ãƒ�ã‚¤ãƒˆã‚³ãƒ¼ãƒ‰ã�¨ã�¯ç•°ã�ªã‚‹ãƒ•ã‚¡ã‚¤ãƒ«ã�§ã�™";

		public const int S_OK = 0;

		public const int S_TRUE = 1;

		public const int S_FALSE = 2;

		public const int E_FAIL = -1;

		public const int E_MEMBERNOTFOUND = -1001;

		public const int E_NOTIMPL = -1002;

		public const int E_INVALIDPARAM = -1003;

		public const int E_BADPARAMCOUNT = -1004;

		public const int E_INVALIDTYPE = -1005;

		public const int E_INVALIDOBJECT = -1006;

		public const int E_ACCESSDENYED = -1007;

		public const int E_NATIVECLASSCRASH = -1008;

		private static readonly string EXCEPTION_NAME = "Exception";

		/// <summary>TJSGetExceptionObject : retrieves TJS 'Exception' object</summary>
		/// <exception cref="TJSException">TJSException</exception>
		/// <exception cref="VariantException">VariantException</exception>
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static void GetExceptionObject(TJS tjs, Variant res, Variant msg, Variant 
			trace)
		{
			if (res == null)
			{
				return;
			}
			// not prcess
			// retrieve class "Exception" from global
			Dispatch2 global = tjs.GetGlobal();
			Variant val = new Variant();
			int hr = global.PropGet(0, EXCEPTION_NAME, val, global);
			if (hr < 0)
			{
				throw new TJSException(ExceptionNotFound);
			}
			// create an Exception object
			Holder<Dispatch2> excpobj = new Holder<Dispatch2>(null);
			VariantClosure clo = val.AsObjectClosure();
			Variant[] pmsg = new Variant[1];
			pmsg[0] = msg;
			hr = clo.CreateNew(0, null, excpobj, pmsg, clo.mObjThis);
			if (hr < 0)
			{
				throw new TJSException(ExceptionNotFound);
			}
			Dispatch2 disp = excpobj.mValue;
			if (trace != null)
			{
				string trace_name = "trace";
				disp.PropSet(Interface.MEMBERENSURE, trace_name, trace, disp);
			}
			res.Set(disp, disp);
			excpobj = null;
		}

		public static void ReportExceptionSource(string msg, InterCodeObject context, int
			 codepos)
		{
			if (TJS.EnableDebugMode)
			{
				TJS.OutputExceptionToConsole(msg + " at " + context.GetPositionDescriptionString(
					codepos));
			}
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static void ThrowFrom_tjs_error(int hr, string name)
		{
			switch (hr)
			{
				case Error.E_MEMBERNOTFOUND:
				{
					// raise an exception descripted as tjs_error
					// name = variable name ( otherwide it can be NULL )
					if (name != null)
					{
						string str = Error.MemberNotFound.Replace("%1", name);
						throw new TJSException(str);
					}
					else
					{
						throw new TJSException(Error.MemberNotFoundNoNameGiven);
					}
					goto case Error.E_NOTIMPL;
				}

				case Error.E_NOTIMPL:
				{
					throw new TJSException(Error.NotImplemented);
				}

				case Error.E_INVALIDPARAM:
				{
					throw new TJSException(Error.InvalidParam);
				}

				case Error.E_BADPARAMCOUNT:
				{
					throw new TJSException(Error.BadParamCount);
				}

				case Error.E_INVALIDTYPE:
				{
					throw new TJSException(Error.InvalidType);
				}

				case Error.E_ACCESSDENYED:
				{
					throw new TJSException(Error.AccessDenyed);
				}

				case Error.E_INVALIDOBJECT:
				{
					throw new TJSException(Error.InvalidObject);
				}

				case Error.E_NATIVECLASSCRASH:
				{
					throw new TJSException(Error.NativeClassCrash);
				}

				default:
				{
					if (hr < 0)
					{
						string buf = string.Format("Unknown failure : %08X", hr);
						throw new TJSException(buf);
					}
					break;
				}
			}
		}
	}
}
