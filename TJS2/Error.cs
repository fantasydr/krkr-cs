/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class Error
	{
		public static readonly string InternalError = "内部エラーが発生しました";

		public static readonly string Warning = "警告: ";

		public static readonly string WarnEvalOperator = "グローバルでない场所で后置 ! 演算子が使われています(この演算子の举动はTJS2 version 2.4.1 で变わりましたのでご注意ください)";

		public static readonly string NarrowToWideConversionError = "ANSI 文字列を UNICODE 文字列に变换できません。现在のコードページで解释できない文字が含まれてます。正しいデータが指定されているかを确认してください。データが破损している可能性もあります";

		public static readonly string VariantConvertError = "%1 から %2 へ型を变换できません";

		public static readonly string VariantConvertErrorToObject = "%1 から Object へ型を变换できません。Object 型が要求される文脉で Object 型以外の值が渡されるとこのエラーが発生します";

		public static readonly string IDExpected = "识别子を指定してください";

		public static readonly string SubstitutionInBooleanContext = "论理值が求められている场所で = 演算子が使用されています(== 演算子の间违いですか？代入した上でゼロと值を比较したい场合は、(A=B) != 0 の形式を使うことをお劝めします)";

		public static readonly string CannotModifyLHS = "不正な代入か不正な式の操作です";

		public static readonly string InsufficientMem = "メモリが足りません";

		public static readonly string CannotGetResult = "この式からは值を得ることができません";

		public static readonly string NullAccess = "null オブジェクトにアクセスしようとしました";

		public static readonly string MemberNotFound = "メンバ \"%1\" が见つかりません";

		public static readonly string MemberNotFoundNoNameGiven = "メンバが见つかりません";

		public static readonly string NotImplemented = "呼び出そうとした机能は未实装です";

		public static readonly string InvalidParam = "不正な引数です";

		public static readonly string BadParamCount = "引数の数が不正です";

		public static readonly string InvalidType = "关数ではないかプロパティの种类が违います";

		public static readonly string SpecifyDicOrArray = "Dictionary または Array クラスのオブジェクトを指定してください";

		public static readonly string SpecifyArray = "Array クラスのオブジェクトを指定してください";

		public static readonly string StringDeallocError = "文字列メモリブロックを解放できません";

		public static readonly string StringAllocError = "文字列メモリブロックを确保できません";

		public static readonly string MisplacedBreakContinue = "\"break\" または \"continue\" はここに书くことはできません";

		public static readonly string MisplacedCase = "\"case\" はここに书くことはできません";

		public static readonly string MisplacedReturn = "\"return\" はここに书くことはできません";

		public static readonly string StringParseError = "文字列定数/正规表现/オクテット即值が终わらないままスクリプトの终端に达しました";

		public static readonly string NumberError = "数值として解释できません";

		public static readonly string UnclosedComment = "コメントが终わらないままスクリプトの终端に达しました";

		public static readonly string InvalidChar = "不正な文字です : \'%1\'";

		public static readonly string Expected = "%1 がありません";

		public static readonly string SyntaxError = "文法エラーです(%1)";

		public static readonly string PPError = "条件コンパイル式にエラーがあります";

		public static readonly string CannotGetSuper = "スーパークラスが存在しないかスーパークラスを特定できません";

		public static readonly string InvalidOpecode = "不正な VM コードです";

		public static readonly string RangeError = "值が范围外です";

		public static readonly string AccessDenyed = "读み迂み专用あるいは书き迂み专用プロパティに对して行えない操作をしようとしました";

		public static readonly string NativeClassCrash = "实行コンテキストが违います";

		public static readonly string InvalidObject = "オブジェクトはすでに无效化されています";

		public static readonly string CannotOmit = "\"...\" は关数外では使えません";

		public static readonly string CannotParseDate = "不正な日付文字列の形式です";

		public static readonly string InvalidValueForTimestamp = "不正な日付?时刻です";

		public static readonly string ExceptionNotFound = "\"Exception\" が存在しないため例外オブジェクトを作成できません";

		public static readonly string InvalidFormatString = "不正な书式文字列です";

		public static readonly string DivideByZero = "0 で除算をしようとしました";

		public static readonly string NotReconstructiveRandomizeData = "乱数系列を初期化できません(おそらく不正なデータが渡されました)";

		public static readonly string Symbol = "识别子";

		public static readonly string CallHistoryIsFromOutOfTJS2Script = "[TJSスクリプト管理外]";

		public static readonly string NObjectsWasNotFreed = "合计 %1 个のオブジェクトが解放されていません";

		public static readonly string ObjectCreationHistoryDelimiter = "\n                     ";

		public static readonly string ObjectWasNotFreed = "オブジェクト %1 [%2] が解放されていません。オブジェクト作成时の呼び出し履历は以下の通りです:\n                     %3";

		public static readonly string GroupByObjectTypeAndHistory = "オブジェクトのタイプとオブジェクト作成时の履历による分类";

		public static readonly string GroupByObjectType = "オブジェクトのタイプによる分类";

		public static readonly string ObjectCountingMessageGroupByObjectTypeAndHistory = 
			"%1 个 : [%2]\n                     %3";

		public static readonly string ObjectCountingMessageTJSGroupByObjectType = "%1 个 : [%2]";

		public static readonly string WarnRunningCodeOnDeletingObject = "%4: 削除中のオブジェクト %1[%2] 上でコードが实行されています。このオブジェクトの作成时の呼び出し履历は以下の通りです:\n                     %3";

		public static readonly string WriteError = "书き迂みエラーが発生しました";

		public static readonly string ReadError = "读み迂みエラーが発生しました。ファイルが破损している可能性や、デバイスからの读み迂みに失败した可能性があります";

		public static readonly string SeekError = "シークエラーが発生しました。ファイルが破损している可能性や、デバイスからの读み迂みに失败した可能性があります";

		public static readonly string TooManyErrors = "Too many errors";

		public static readonly string ConstDicDelimiterError = "定数辞书(const Dictionary)で要素名と值の区切りが不正です";

		public static readonly string ConstDicValueError = "定数辞书(const Dictionary)の要素值が不正です";

		public static readonly string ConstArrayValueError = "定数配列(const Array)の要素值が不正です";

		public static readonly string ConstDicArrayStringError = "定数辞书もしくは配列で(const)文字が不正です";

		public static readonly string ConstDicLBRACKETError = "定数辞书(const Dictionary)で(const)%の后に\"[\"がありません";

		public static readonly string ConstArrayLBRACKETError = "定数配列(const Array)で(const)の后に\"[\"がありません";

		public static readonly string DicDelimiterError = "辞书(Dictionary)で要素名と值の区切りが不正です";

		public static readonly string DicError = "辞书(Dictionary)が不正です";

		public static readonly string DicLBRACKETError = "辞书(Dictionary)で%の后に\"[\"がありません";

		public static readonly string DicRBRACKETError = "辞书(Dictionary)の终端に\"]\"がありません";

		public static readonly string ArrayRBRACKETError = "配列(Array)の终端に\"]\"がありません";

		public static readonly string NotFoundRegexError = "正规表现が要求される文脉で正规表现がありません";

		public static readonly string NotFoundSymbolAfterDotError = "\".\"の后にシンボルがありません";

		public static readonly string NotFoundDicOrArrayRBRACKETError = "配列もしくは辞书要素を指す变数の终端に\"]\"がありません";

		public static readonly string NotFoundRPARENTHESISError = "\")\"が要求される文脉で\")\"がありません";

		public static readonly string NotFoundSemicolonAfterThrowError = "throwの后の\";\"がありません";

		public static readonly string NotFoundRPARENTHESISAfterCatchError = "catchの后の\")\"がありません";

		public static readonly string NotFoundCaseOrDefaultError = "caseかdefaultが要求される文脉でcaseかdefaultがありません";

		public static readonly string NotFoundWithLPARENTHESISError = "withの后に\"(\"がありません";

		public static readonly string NotFoundWithRPARENTHESISError = "withの后に\")\"がありません";

		public static readonly string NotFoundSwitchLPARENTHESISError = "switchの后に\"(\"がありません";

		public static readonly string NotFoundSwitchRPARENTHESISError = "switchの后に\")\"がありません";

		public static readonly string NotFoundSemicolonAfterReturnError = "returnの后の\";\"がありません";

		public static readonly string NotFoundPropGetRPARENTHESISError = "property getterの后に\")\"がありません";

		public static readonly string NotFoundPropSetLPARENTHESISError = "property setterの后に\"(\"がありません";

		public static readonly string NotFoundPropSetRPARENTHESISError = "property setterの后に\")\"がありません";

		public static readonly string NotFoundPropError = "propertyの后に\"getter\"もしくは\"setter\"がありません";

		public static readonly string NotFoundSymbolAfterPropError = "propertyの后にシンボルがありません";

		public static readonly string NotFoundLBRACEAfterPropError = "propertyの后に\"{\"がありません";

		public static readonly string NotFoundRBRACEAfterPropError = "propertyの后に\"}\"がありません";

		public static readonly string NotFoundFuncDeclRPARENTHESISError = "关数定义の后に\")\"がありません";

		public static readonly string NotFoundFuncDeclSymbolError = "关数定义にシンボル名がありません";

		public static readonly string NotFoundSymbolAfterVarError = "变数宣言にシンボルがありません";

		public static readonly string NotFoundForLPARENTHESISError = "forの后に\"(\"がありません";

		public static readonly string NotFoundForRPARENTHESISError = "forの后に\")\"がありません";

		public static readonly string NotFoundForSemicolonError = "forの各节の区切りに\";\"がありません";

		public static readonly string NotFoundIfLPARENTHESISError = "ifの后に\"(\"がありません";

		public static readonly string NotFoundIfRPARENTHESISError = "ifの后に\")\"がありません";

		public static readonly string NotFoundDoWhileLPARENTHESISError = "do-whileの后に\"(\"がありません";

		public static readonly string NotFoundDoWhileRPARENTHESISError = "do-whileの后に\")\"がありません";

		public static readonly string NotFoundDoWhileError = "do-while文でwhileがありません";

		public static readonly string NotFoundDoWhileSemicolonError = "do-while文でwhileの后に\";\"がありません";

		public static readonly string NotFoundWhileLPARENTHESISError = "whileの后に\"(\"がありません";

		public static readonly string NotFoundWhileRPARENTHESISError = "whileの后に\")\"がありません";

		public static readonly string NotFoundLBRACEAfterBlockError = "ブロックが要求される文脉で\"{\"がありません";

		public static readonly string NotFoundRBRACEAfterBlockError = "ブロックが要求される文脉で\"}\"がありません";

		public static readonly string NotFoundSemicolonError = "文の终わりに\";\"がありません";

		public static readonly string NotFoundSemicolonOrTokenTypeError = "文の终わりに\";\"がないか、予约语のタイプミスです";

		public static readonly string NotFoundBlockRBRACEError = "ブロックの终わりに\"}\"がありません";

		public static readonly string NotFoundCatchError = "tryの后にcatchがありません";

		public static readonly string NotFoundFuncCallLPARENTHESISError = "关数呼び出しの后に\"(\"がありません";

		public static readonly string NotFoundFuncCallRPARENTHESISError = "关数呼び出しの后に\")\"がありません";

		public static readonly string NotFoundVarSemicolonError = "变数宣言の后に\";\"がありません";

		public static readonly string NotFound3ColonError = "条件演算子の\":\"がありません";

		public static readonly string NotFoundCaseColonError = "caseの后に\":\"がありません";

		public static readonly string NotFoundDefaultColonError = "defaultの后に\":\"がありません";

		public static readonly string NotFoundSymbolAfterClassError = "classの后にシンボルがありません";

		public static readonly string NotFoundPropSetSymbolError = "property setterの引数がありません";

		public static readonly string NotFoundBreakSemicolonError = "breakの后に\";\"がありません";

		public static readonly string NotFoundContinueSemicolonError = "continueの后に\";\"がありません";

		public static readonly string NotFoundBebuggerSemicolonError = "debuggerの后に\";\"がありません";

		public static readonly string NotFoundAsteriskAfterError = "关数呼び出し、关数定义の配列展开(*)が不正か、乘算が不正です";

		public static readonly string EndOfBlockError = "ブロックの对应が取れていません。\"}\"が多いです";

		public static readonly string NotFoundPreprocessorRPARENTHESISError = "プリプロセッサに\")\"がありません";

		public static readonly string PreprocessorZeroDiv = "プリプロセッサのゼロ除算エラー";

		public static readonly string ByteCodeBroken = "バイトコードファイル读み迂みエラー。ファイルが坏れているかバイトコードとは异なるファイルです";

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
