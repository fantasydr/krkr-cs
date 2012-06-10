/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public interface StorageInterface
	{
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		string ReadText(string name, string modestr);

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		TextWriteStreamInterface CreateTextWriteStream(string name, string modestr);

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		BinaryStream CreateBinaryWriteStream(string name);
	}
}
