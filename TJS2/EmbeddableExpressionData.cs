/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	internal class EmbeddableExpressionData
	{
		public const int START = 0;

		public const int NEXT_IS_STRING_LITERAL = 1;

		public const int NEXT_IS_EXPRESSION = 2;

		public int mState;

		public int mWaitingNestLevel;

		public int mWaitingToken;

		public int mDelimiter;

		public bool mNeedPlus;
	}
}
