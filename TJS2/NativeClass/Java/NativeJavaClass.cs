/*
 * TJS2 CSharp
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class NativeJavaClass : NativeClass
	{
		private Type mJavaClass;

		private int mClassID;

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public NativeJavaClass(string name, Type c) : base(name)
		{
			mJavaClass = c;
			string classname = name;
			mClassID = TJS.RegisterNativeClass(classname);
			try
			{
				HashSet<string> registProp = new HashSet<string>();
				// set/getで重复しないようにチェック
				MethodInfo[] methods = c.GetMethods();
				foreach (MethodInfo m in methods)
				{
					string methodName = m.Name;
					int flag = 0;
					if (m.IsStatic)
					{
						flag |= Interface.STATICMEMBER;
					}
					if ("constructor".Equals(methodName))
					{
						// コンストラクタ
						RegisterNCM(classname, new NativeJavaClassConstructor(m, mClassID), classname, Interface
							.nitMethod, flag);
					}
					else
					{
						if (methodName.StartsWith("prop_"))
						{
							// プロパティ prop_ で始まるものはプロパティとみなす
							Type[] @params = Sharpen.Runtime.GetParameterTypes(m);
							MethodInfo setMethod = null;
							MethodInfo getMethod = null;
							string propName = null;
							if (methodName.StartsWith("prop_set_"))
							{
								if (@params.Length == 1)
								{
									setMethod = m;
									propName = Sharpen.Runtime.Substring(methodName, "prop_set_".Length);
									if (registProp.Contains(propName) == false)
									{
										string getMethodName = "prop_get_" + propName;
										foreach (MethodInfo getm in methods)
										{
											if (getm.Name.Equals(getMethodName))
											{
												Type[] p = Sharpen.Runtime.GetParameterTypes(getm);
												if (p.Length == 0 && getm.ReturnType.Equals(typeof(void)) != true)
												{
													getMethod = getm;
													break;
												}
											}
										}
									}
								}
							}
							else
							{
								if (methodName.StartsWith("prop_get_"))
								{
									if (@params.Length == 0 && m.ReturnType.Equals(typeof(void)) != true)
									{
										getMethod = m;
										propName = Sharpen.Runtime.Substring(methodName, "prop_get_".Length);
										if (registProp.Contains(propName) == false)
										{
											string setMethodName = "prop_set_" + propName;
											foreach (MethodInfo setm in methods)
											{
												if (setm.Name.Equals(setMethodName))
												{
													Type[] p = Sharpen.Runtime.GetParameterTypes(setm);
													if (p.Length == 1)
													{
														setMethod = setm;
														break;
													}
												}
											}
										}
									}
								}
							}
							if (propName != null && registProp.Contains(propName) == false)
							{
								if (setMethod != null || getMethod != null)
								{
									RegisterNCM(propName, new NativeJavaClassProperty(getMethod, setMethod, mClassID)
										, classname, Interface.nitProperty, flag);
									registProp.AddItem(propName);
								}
							}
						}
						else
						{
							// 通常メソッド
							RegisterNCM(methodName, new NativeJavaClassMethod(m, mClassID), classname, Interface
								.nitMethod, flag);
						}
					}
				}
				registProp = null;
			}
			catch (SecurityException e)
			{
				throw new TJSException(Error.InternalError + e.ToString());
			}
		}

		/// <summary>
		/// 引数があるコンストラクタには未对应
		/// TODO エラー时エラー表示するようにした方がいいかも
		/// </summary>
		protected internal override NativeInstance CreateNativeInstance()
		{
			object obj;
			try
			{
				obj = System.Activator.CreateInstance(mJavaClass);
			}
			catch (InstantiationException e)
			{
				TJS.OutputExceptionToConsole(e.ToString());
				return null;
			}
			catch (MemberAccessException e)
			{
				TJS.OutputExceptionToConsole(e.ToString());
				return null;
			}
			if (obj != null)
			{
				return new NativeJavaInstance(obj);
			}
			return null;
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public static object VariantToJavaObject(Variant param, Type type)
		{
			if (type.IsPrimitive)
			{
				// プリミティブタイプの场合
				if (type.Equals(typeof(int)))
				{
					return Sharpen.Extensions.ValueOf(param.AsInteger());
				}
				else
				{
					if (type.Equals(typeof(double)))
					{
						return (param.AsDouble());
					}
					else
					{
						if (type.Equals(typeof(bool)))
						{
							return Sharpen.Extensions.ValueOf(param.AsInteger() != 0 ? true : false);
						}
						else
						{
							if (type.Equals(typeof(float)))
							{
								return ((float)param.AsDouble());
							}
							else
							{
								if (type.Equals(typeof(long)))
								{
									return Sharpen.Extensions.ValueOf(param.AsInteger());
								}
								else
								{
									if (type.Equals(typeof(char)))
									{
										return ((char)param.AsInteger());
									}
									else
									{
										if (type.Equals(typeof(byte)))
										{
											return (unchecked((byte)param.AsInteger()));
										}
										else
										{
											if (type.Equals(typeof(short)))
											{
												return ((short)param.AsInteger());
											}
											else
											{
												// may be Void.TYPE
												return null;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (type.Equals(typeof(string)))
				{
					return param.AsString();
				}
				else
				{
					if (type.Equals(typeof(ByteBuffer)))
					{
						return param.AsOctet();
					}
					else
					{
						if (type.Equals(typeof(Variant)))
						{
							return param;
						}
						else
						{
							if (type.Equals(typeof(VariantClosure)))
							{
								return param.AsObjectClosure();
							}
							else
							{
								if (type.Equals(typeof(Dispatch2)))
								{
									return param.AsObject();
								}
								else
								{
									if (type.Equals(param.ToJavaObject().GetType()))
									{
										return param.ToJavaObject();
									}
									else
									{
										// その他 のクラス
										return null;
									}
								}
							}
						}
					}
				}
			}
		}

		public static void JavaObjectToVariant(Variant result, Type type, object src)
		{
			if (result == null)
			{
				return;
			}
			if (type.IsPrimitive)
			{
				// プリミティブタイプの场合
				if (type.Equals(typeof(int)))
				{
					result.Set(((int)src));
				}
				else
				{
					if (type.Equals(typeof(double)))
					{
						result.Set(((double)src));
					}
					else
					{
						if (type.Equals(typeof(bool)))
						{
							result.Set(((bool)src) ? 1 : 0);
						}
						else
						{
							if (type.Equals(typeof(float)))
							{
								result.Set(((float)src));
							}
							else
							{
								if (type.Equals(typeof(long)))
								{
									result.Set(((long)src));
								}
								else
								{
									if (type.Equals(typeof(char)))
									{
										result.Set((int)((char)src));
									}
									else
									{
										if (type.Equals(typeof(byte)))
										{
											result.Set(((byte)src));
										}
										else
										{
											if (type.Equals(typeof(short)))
											{
												result.Set(((short)src));
											}
											else
											{
												// may be Void.TYPE
												result.Clear();
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (type.Equals(typeof(string)))
				{
					result.Set((string)src);
				}
				else
				{
					if (type.Equals(typeof(ByteBuffer)))
					{
						result.Set((ByteBuffer)src);
					}
					else
					{
						if (type.Equals(typeof(Variant)))
						{
							result.Set((Variant)src);
						}
						else
						{
							if (type.Equals(typeof(VariantClosure)))
							{
								result.Set(((VariantClosure)src).mObject, ((VariantClosure)src).mObjThis);
							}
							else
							{
								if (type.Equals(typeof(Dispatch2)))
								{
									result.Set((Dispatch2)src);
								}
								else
								{
									// その他 のクラス, 直接入れてしまう
									result.SetJavaObject(src);
								}
							}
						}
					}
				}
			}
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public static object[] VariantArrayToJavaObjectArray(Variant[] @params, Type[] types
			)
		{
			if (types.Length == 0)
			{
				return null;
			}
			// 元々引数不要
			if (@params.Length < types.Length)
			{
				return null;
			}
			// パラメータが少ない
			int count = types.Length;
			object[] ret = new object[count];
			for (int i = 0; i < count; i++)
			{
				Type type = types[i];
				Variant param = @params[i];
				if (type.IsPrimitive)
				{
					// プリミティブタイプの场合
					if (type.Equals(typeof(int)))
					{
						ret[i] = Sharpen.Extensions.ValueOf(param.AsInteger());
					}
					else
					{
						if (type.Equals(typeof(double)))
						{
							ret[i] = (param.AsDouble());
						}
						else
						{
							if (type.Equals(typeof(bool)))
							{
								ret[i] = Sharpen.Extensions.ValueOf(param.AsInteger() != 0 ? true : false);
							}
							else
							{
								if (type.Equals(typeof(float)))
								{
									ret[i] = ((float)param.AsDouble());
								}
								else
								{
									if (type.Equals(typeof(long)))
									{
										ret[i] = Sharpen.Extensions.ValueOf(param.AsInteger());
									}
									else
									{
										if (type.Equals(typeof(char)))
										{
											ret[i] = ((char)param.AsInteger());
										}
										else
										{
											if (type.Equals(typeof(byte)))
											{
												ret[i] = (unchecked((byte)param.AsInteger()));
											}
											else
											{
												if (type.Equals(typeof(short)))
												{
													ret[i] = ((short)param.AsInteger());
												}
												else
												{
													// may be Void.TYPE
													ret[i] = null;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					if (type.Equals(typeof(string)))
					{
						ret[i] = param.AsString();
					}
					else
					{
						if (type.Equals(typeof(ByteBuffer)))
						{
							ret[i] = param.AsOctet();
						}
						else
						{
							if (type.Equals(typeof(Variant)))
							{
								ret[i] = param;
							}
							else
							{
								if (type.Equals(typeof(VariantClosure)))
								{
									ret[i] = param.AsObjectClosure();
								}
								else
								{
									if (type.Equals(typeof(Dispatch2)))
									{
										ret[i] = param.AsObject();
									}
									else
									{
										if (type.Equals(param.ToJavaObject().GetType()))
										{
											ret[i] = param.ToJavaObject();
										}
										else
										{
											// その他 のクラス
											ret[i] = null;
										}
									}
								}
							}
						}
					}
				}
			}
			return ret;
		}
	}
}
