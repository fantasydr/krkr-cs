/*
 * The TJS2 interpreter from kirikirij
 */

using Sharpen;

namespace Kirikiri.Tjs2
{
	internal class TokenPair
	{
		internal int token;

		internal int value;

		internal TokenPair(int t, int v)
		{
			token = t;
			value = v;
		}
	}
}
