/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public interface ConsoleOutput
	{
		void ExceptionPrint(string msg);

		void Print(string msg);
	}
}
