/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public interface RandomBits128
	{
		// retrives 128-bits (16bytes) random bits for random seed.
		// this can be override application-specified routine, otherwise
		// TJS2 uses current time as a random seed.
		void GetRandomBits128(ByteBuffer buf, int offset);
	}
}
