/*
 * TJS2 CSharp
 */

using Sharpen;

namespace Kirikiri.Tjs2
{
	public class MersenneTwisterData
	{
		private const int MT_N = 624;

		public int left;

		public int next;

		public LongBuffer state;

		public MersenneTwisterData()
		{
			// index a value in 'state'
			// the array for the state vector
			ByteBuffer buff = ByteBuffer.AllocateDirect(MT_N * 8);
            buff.Order(ByteBuffer.NativeOrder());
			state = buff.AsLongBuffer();
			state.Clear();
			state.Position(0);
			state.Limit(MT_N);
		}
	}
}
