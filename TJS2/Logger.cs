/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class Logger
	{
		public static void Log(string tag, string message)
		{
			System.Console.Out.WriteLine(tag + " : " + message);
		}

		// Log.v( tag, message );	// for android
		public static void Log(string message)
		{
			System.Console.Out.WriteLine(message);
		}
		// Log.v( "Logger", message );	// for android
	}
}
