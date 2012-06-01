/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	[System.Serializable]
	public class VariantException : TJSException
	{
		private const long serialVersionUID = -3605064460238917638L;

		public VariantException()
		{
		}

		public VariantException(string msg) : base(msg)
		{
		}
	}
}
