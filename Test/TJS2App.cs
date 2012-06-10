/*
 * The TJS2 interpreter from kirikirij
 */

using Sharpen;
using Test;
using Kirikiri.Tjs2;
using System;

namespace Test
{
	public class TJS2App
	{
        public class DebugConsoleOutput : ConsoleOutput
        {
            public void ExceptionPrint(string msg)
            {
                Console.Write("Error:");
                Console.WriteLine(msg);
            }

            public void Print(string msg)
            {
                Console.Write("OUT:");
                Console.WriteLine(msg);
            }
        }

		public static void Main(string[] args)
		{
            TJS.mStorage = null;
            TJS.Initialize();
            TJS mScriptEngine = new TJS();
            TJS.SetConsoleOutput(new DebugConsoleOutput());
            
            Dispatch2 dsp = mScriptEngine.GetGlobal();
            Variant ret = new Variant();
            mScriptEngine.ExecScript("Debug.message(\"Hello, world!\");", ret, dsp, null, 0);

			System.Console.Out.WriteLine("Hello World!");
		}
		// Display the string.
	}
}
