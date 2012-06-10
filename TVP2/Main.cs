/*
 * The TJS2 interpreter from kirikirij
 */

/* SCE CONFIDENTIAL
 * PlayStation(R)Suite SDK 0.98.2
 * Copyright (C) 2012 Sony Computer Entertainment Inc.
 * All Rights Reserved.
 */

using System;

using Sce.Pss.Core;
using Sce.Pss.Core.Environment;
using Sce.Pss.Core.Graphics;
using Sce.Pss.Core.Imaging;
using Sce.Pss.Core.Input;

using Kirikiri.Tjs2;

namespace Sample
{
	public class AppMain
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
		
		static protected GraphicsContext graphics;
		
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		public static void Initialize ()
		{
			graphics = new GraphicsContext();
			
			TJS.mStorage = null;
            TJS.Initialize();
            TJS mScriptEngine = new TJS();
            TJS.SetConsoleOutput(new DebugConsoleOutput());
            
            Dispatch2 dsp = mScriptEngine.GetGlobal();
            Variant ret = new Variant();
            mScriptEngine.ExecScript("Debug.message(\"Hello World!\");", ret, dsp, null, 0);
		}

		public static void Update ()
		{
			
		}

		public static void Render ()
		{
			graphics.Clear();
			
			graphics.SwapBuffers();	
		}
	}
}