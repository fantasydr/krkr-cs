/*
 * TJS2 CSharp
 */

using System;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class MathClass : NativeClass
	{
		public static int mClassID;

		private static Random mRandomGenerator;

		private static readonly string CLASS_NAME = "Math";

		public static void Initialize()
		{
			mRandomGenerator = null;
		}

		public static void FinalizeApplication()
		{
			mRandomGenerator = null;
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public MathClass() : base(CLASS_NAME)
		{
			if (mRandomGenerator == null)
			{
				mRandomGenerator = new Random((int)DateTime.Now.Ticks);
			}
			int NCM_CLASSID = TJS.RegisterNativeClass(CLASS_NAME);
			SetClassID(NCM_CLASSID);
			mClassID = NCM_CLASSID;
			// constructor
			RegisterNCM(CLASS_NAME, new _NativeClassConstructor_30(), CLASS_NAME, Interface.nitMethod
				, 0);
			RegisterNCM("abs", new _NativeClassMethod_37(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("acos", new _NativeClassMethod_46(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("asin", new _NativeClassMethod_55(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("atan", new _NativeClassMethod_64(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("atan2", new _NativeClassMethod_73(), CLASS_NAME, Interface.nitMethod
				, Interface.STATICMEMBER);
			RegisterNCM("ceil", new _NativeClassMethod_82(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("exp", new _NativeClassMethod_91(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("floor", new _NativeClassMethod_100(), CLASS_NAME, Interface.nitMethod
				, Interface.STATICMEMBER);
			RegisterNCM("log", new _NativeClassMethod_109(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("pow", new _NativeClassMethod_118(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("max", new _NativeClassMethod_127(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("min", new _NativeClassMethod_148(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("random", new _NativeClassMethod_169(), CLASS_NAME, Interface.nitMethod
				, Interface.STATICMEMBER);
			RegisterNCM("round", new _NativeClassMethod_177(), CLASS_NAME, Interface.nitMethod
				, Interface.STATICMEMBER);
			RegisterNCM("sin", new _NativeClassMethod_186(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("cos", new _NativeClassMethod_195(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("sqrt", new _NativeClassMethod_204(), CLASS_NAME, Interface.nitMethod
				, Interface.STATICMEMBER);
			RegisterNCM("tan", new _NativeClassMethod_213(), CLASS_NAME, Interface.nitMethod, 
				Interface.STATICMEMBER);
			RegisterNCM("E", new _NativeClassProperty_222(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
			RegisterNCM("LOG2E", new _NativeClassProperty_231(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
			RegisterNCM("LOG10E", new _NativeClassProperty_240(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
			RegisterNCM("LN10", new _NativeClassProperty_249(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
			RegisterNCM("LN2", new _NativeClassProperty_258(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
			RegisterNCM("PI", new _NativeClassProperty_267(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
			RegisterNCM("SQRT1_2", new _NativeClassProperty_276(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
			RegisterNCM("SQRT2", new _NativeClassProperty_285(), CLASS_NAME, Interface.nitProperty
				, Interface.STATICMEMBER);
		}

		private sealed class _NativeClassConstructor_30 : NativeClassConstructor
		{
			public _NativeClassConstructor_30()
			{
			}

			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_37 : NativeClassMethod
		{
			public _NativeClassMethod_37()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Abs(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_46 : NativeClassMethod
		{
			public _NativeClassMethod_46()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Acos(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_55 : NativeClassMethod
		{
			public _NativeClassMethod_55()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Asin(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_64 : NativeClassMethod
		{
			public _NativeClassMethod_64()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Atan(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_73 : NativeClassMethod
		{
			public _NativeClassMethod_73()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 2)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Atan2(param[0].AsDouble(), param[1].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_82 : NativeClassMethod
		{
			public _NativeClassMethod_82()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
                    result.Set(Math.Ceiling(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_91 : NativeClassMethod
		{
			public _NativeClassMethod_91()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Exp(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_100 : NativeClassMethod
		{
			public _NativeClassMethod_100()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Floor(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_109 : NativeClassMethod
		{
			public _NativeClassMethod_109()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Log(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_118 : NativeClassMethod
		{
			public _NativeClassMethod_118()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 2)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Pow(param[0].AsDouble(), param[1].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_127 : NativeClassMethod
		{
			public _NativeClassMethod_127()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (result != null)
				{
					double r = double.NegativeInfinity;
					int count = param.Length;
					for (int i = 0; i < count; i++)
					{
						double v = param[i].AsDouble();
						if (double.IsNaN(v))
						{
							result.Set(double.NaN);
							return Error.S_OK;
						}
						else
						{
							if (Double.Compare(v, r) > 0)
							{
								r = v;
							}
						}
					}
					result.Set(r);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_148 : NativeClassMethod
		{
			public _NativeClassMethod_148()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (result != null)
				{
					double r = double.PositiveInfinity;
					int count = param.Length;
					for (int i = 0; i < count; i++)
					{
						double v = param[i].AsDouble();
						if (double.IsNaN(v))
						{
							result.Set(double.NaN);
							return Error.S_OK;
						}
						else
						{
							if (Double.Compare(v, r) < 0)
							{
								r = v;
							}
						}
					}
					result.Set(r);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_169 : NativeClassMethod
		{
			public _NativeClassMethod_169()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (result != null)
				{
					result.Set(Kirikiri.Tjs2.MathClass.mRandomGenerator.NextDouble());
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_177 : NativeClassMethod
		{
			public _NativeClassMethod_177()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Round(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_186 : NativeClassMethod
		{
			public _NativeClassMethod_186()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Sin(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_195 : NativeClassMethod
		{
			public _NativeClassMethod_195()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Cos(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_204 : NativeClassMethod
		{
			public _NativeClassMethod_204()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Sqrt(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_213 : NativeClassMethod
		{
			public _NativeClassMethod_213()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					result.Set(Math.Tan(param[0].AsDouble()));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassProperty_222 : NativeClassProperty
		{
			public _NativeClassProperty_222()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(2.7182818284590452354);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}

		private sealed class _NativeClassProperty_231 : NativeClassProperty
		{
			public _NativeClassProperty_231()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(1.4426950408889634074);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}

		private sealed class _NativeClassProperty_240 : NativeClassProperty
		{
			public _NativeClassProperty_240()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(0.4342944819032518276);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}

		private sealed class _NativeClassProperty_249 : NativeClassProperty
		{
			public _NativeClassProperty_249()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(2.30258509299404568402);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}

		private sealed class _NativeClassProperty_258 : NativeClassProperty
		{
			public _NativeClassProperty_258()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(0.69314718055994530942);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}

		private sealed class _NativeClassProperty_267 : NativeClassProperty
		{
			public _NativeClassProperty_267()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(3.14159265358979323846);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}

		private sealed class _NativeClassProperty_276 : NativeClassProperty
		{
			public _NativeClassProperty_276()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(0.70710678118654752440);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}

		private sealed class _NativeClassProperty_285 : NativeClassProperty
		{
			public _NativeClassProperty_285()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				result.Set(1.41421356237309504880);
				return Error.S_OK;
			}

			public override int Set(Variant param, Dispatch2 objthis)
			{
				return Error.E_ACCESSDENYED;
			}
		}
	}
}
