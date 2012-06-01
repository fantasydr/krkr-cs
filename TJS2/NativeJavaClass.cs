/*
 * The TJS2 interpreter from kirikirij
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using Kirikiri.Tjs2;
using Sharpen;
using Sharpen.Reflect;

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
				// set/getã�§é‡�è¤‡ã�—ã�ªã�„ã‚ˆã�†ã�«ãƒ�ã‚§ãƒƒã‚¯
				MethodInfo[] methods = c.GetMethods();
				foreach (MethodInfo m in methods)
				{
					string methodName = m.Name;
					int modif = m.GetModifiers();
					int flag = 0;
					if (Modifier.IsStatic(modif))
					{
						flag |= Interface.STATICMEMBER;
					}
					if ("constructor".Equals(methodName))
					{
						// ã‚³ãƒ³ã‚¹ãƒˆãƒ©ã‚¯ã‚¿
						RegisterNCM(classname, new NativeJavaClassConstructor(m, mClassID), classname, Interface
							.nitMethod, flag);
					}
					else
					{
						if (methodName.StartsWith("prop_"))
						{
							// ãƒ—ãƒ­ãƒ‘ãƒ†ã‚£ prop_ ã�§å§‹ã�¾ã‚‹ã‚‚ã�®ã�¯ãƒ—ãƒ­ãƒ‘ãƒ†ã‚£ã�¨ã�¿ã�ªã�™
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
							// é€šå¸¸ãƒ¡ã‚½ãƒƒãƒ‰
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
		/// å¼•æ•°ã�Œã�‚ã‚‹ã‚³ãƒ³ã‚¹ãƒˆãƒ©ã‚¯ã‚¿ã�«ã�¯æœªå¯¾å¿œ
		/// TODO ã‚¨ãƒ©ãƒ¼æ™‚ã‚¨ãƒ©ãƒ¼è¡¨ç¤ºã�™ã‚‹ã‚ˆã�†ã�«ã�—ã�Ÿæ–¹ã�Œã�„ã�„ã�‹ã‚‚
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
		public static object VariantToJavaObject<_T0>(Variant param, Type<_T0> type)
		{
			if (type.IsPrimitive)
			{
				// ãƒ—ãƒªãƒŸãƒ†ã‚£ãƒ–ã‚¿ã‚¤ãƒ—ã�®å ´å�ˆ
				if (type.Equals(typeof(int)))
				{
					return Sharpen.Extensions.ValueOf(param.AsInteger());
				}
				else
				{
					if (type.Equals(typeof(double)))
					{
						return double.ValueOf(param.AsDouble());
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
								return float.ValueOf((float)param.AsDouble());
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
										return char.ValueOf((char)param.AsInteger());
									}
									else
									{
										if (type.Equals(typeof(byte)))
										{
											return byte.ValueOf(unchecked((byte)param.AsInteger()));
										}
										else
										{
											if (type.Equals(typeof(short)))
											{
												return short.ValueOf((short)param.AsInteger());
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
										// ã��ã�®ä»– ã�®ã‚¯ãƒ©ã‚¹
										return null;
									}
								}
							}
						}
					}
				}
			}
		}

		public static void JavaObjectToVariant<_T0>(Variant result, Type<_T0> type, object
			 src)
		{
			if (result == null)
			{
				return;
			}
			if (type.IsPrimitive)
			{
				// ãƒ—ãƒªãƒŸãƒ†ã‚£ãƒ–ã‚¿ã‚¤ãƒ—ã�®å ´å�ˆ
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
									// ã��ã�®ä»– ã�®ã‚¯ãƒ©ã‚¹, ç›´æŽ¥å…¥ã‚Œã�¦ã�—ã�¾ã�†
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
			// å…ƒã€…å¼•æ•°ä¸�è¦�
			if (@params.Length < types.Length)
			{
				return null;
			}
			// ãƒ‘ãƒ©ãƒ¡ãƒ¼ã‚¿ã�Œå°‘ã�ªã�„
			int count = types.Length;
			object[] ret = new object[count];
			for (int i = 0; i < count; i++)
			{
				Type type = types[i];
				Variant param = @params[i];
				if (type.IsPrimitive)
				{
					// ãƒ—ãƒªãƒŸãƒ†ã‚£ãƒ–ã‚¿ã‚¤ãƒ—ã�®å ´å�ˆ
					if (type.Equals(typeof(int)))
					{
						ret[i] = Sharpen.Extensions.ValueOf(param.AsInteger());
					}
					else
					{
						if (type.Equals(typeof(double)))
						{
							ret[i] = double.ValueOf(param.AsDouble());
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
									ret[i] = float.ValueOf((float)param.AsDouble());
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
											ret[i] = char.ValueOf((char)param.AsInteger());
										}
										else
										{
											if (type.Equals(typeof(byte)))
											{
												ret[i] = byte.ValueOf(unchecked((byte)param.AsInteger()));
											}
											else
											{
												if (type.Equals(typeof(short)))
												{
													ret[i] = short.ValueOf((short)param.AsInteger());
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
											// ã��ã�®ä»– ã�®ã‚¯ãƒ©ã‚¹
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
