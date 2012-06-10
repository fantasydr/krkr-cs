/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public interface TextWriteStreamInterface
	{
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		void Write(string val);

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		void Destruct();
	}
}
