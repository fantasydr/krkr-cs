/*
 * The TJS2 interpreter from kirikirij
 */

using System.Collections.Generic;
using System.Text;
using Kirikiri.Tjs2;
using Sharpen;
using System;

namespace Kirikiri.Tjs2
{
	public class DebugClass : NativeClass
	{
		public static int ClassID = -1;

		private static readonly string CLASS_NAME = "Debug";

		protected internal override NativeInstance CreateNativeInstance()
		{
			return null;
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public DebugClass() : base(CLASS_NAME)
		{
			int NCM_CLASSID = TJS.RegisterNativeClass(CLASS_NAME);
			SetClassID(NCM_CLASSID);
			ClassID = NCM_CLASSID;
			// constructor
			RegisterNCM(CLASS_NAME, new ReturnOKConstructor(), CLASS_NAME, Interface.nitMethod
				, 0);
			RegisterNCM("finalize", new ReturnOKMethod(), CLASS_NAME, Interface.nitMethod, 
				0);
			
			RegisterNCM("message", new DebugMessageMethod(), CLASS_NAME, Interface.nitMethod, 
				0);
		}

		private sealed class ReturnOKConstructor : NativeClassConstructor
		{
			public ReturnOKConstructor()
			{
			}

			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				return Error.S_OK;
			}
		}

		private sealed class ReturnOKMethod : NativeClassMethod
		{
			public ReturnOKMethod()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				return Error.S_OK;
			}
		}

		private sealed class DebugMessageMethod : NativeClassMethod
		{
			public DebugMessageMethod()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if( param.Length < 1 ) return Error.E_BADPARAMCOUNT;
				if( param.Length == 1 ) {
					Console.WriteLine( param[0].AsString() );
				} else {
					// display the arguments separated with ", "
					StringBuilder builder = new StringBuilder(512);
					for( int i = 0; i < param.Length; i++ ) {
						if( i != 0 ) builder.Append(", ");
						builder.Append( param[i].AsString() );
					}
					Console.WriteLine( builder.ToString() );
				}
				return Error.S_OK;
			}
		}
	}
}
