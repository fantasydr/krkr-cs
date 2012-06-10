/*
 * TJS2 CSharp
 */

using System.Reflection;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class NativeJavaClassConstructor : NativeJavaClassMethod
	{
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public NativeJavaClassConstructor(MethodInfo m, int classID) : base(m, classID)
		{
		}
	}
}
