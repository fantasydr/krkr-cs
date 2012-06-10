/*
 * TJS2 CSharp
 */

using System.Text;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class Utils
	{
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public static string VariantToReadableString(Variant val)
		{
			return VariantToReadableString(val, 512);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public static string VariantToReadableString(Variant val, int maxlen)
		{
			string ret = null;
			if (val == null || val.IsVoid())
			{
				ret = new string("(void)");
			}
			else
			{
				if (val.IsInteger())
				{
					ret = new string("(int)" + val.AsString());
				}
				else
				{
					if (val.IsReal())
					{
						ret = new string("(real)" + val.AsString());
					}
					else
					{
						if (val.IsString())
						{
							ret = new string("(string)\"" + LexBase.EscapeC(val.AsString()) + "\"");
						}
						else
						{
							if (val.IsOctet())
							{
								ret = new string("(octet)<% " + Variant.OctetToListString(val.AsOctet()) + " %>");
							}
							else
							{
								if (val.IsObject())
								{
									VariantClosure c = (VariantClosure)val.AsObjectClosure();
									StringBuilder str = new StringBuilder(128);
									str.Append("(object)");
									str.Append('(');
									if (c.mObject != null)
									{
										str.Append('[');
										if (c.mObject is NativeClass)
										{
											str.Append(((NativeClass)c.mObject).GetClassName());
										}
										else
										{
											if (c.mObject is InterCodeObject)
											{
												str.Append(((InterCodeObject)c.mObject).GetName());
											}
											else
											{
												if (c.mObject is CustomObject)
												{
													string name = ((CustomObject)c.mObject).GetClassNames();
													if (name != null)
													{
														str.Append(name);
													}
													else
													{
														str.Append(c.mObject.GetType().FullName);
													}
												}
												else
												{
													str.Append(c.mObject.GetType().FullName);
												}
											}
										}
										str.Append(']');
									}
									else
									{
										str.Append("0x00000000");
									}
									if (c.mObjThis != null)
									{
										str.Append('[');
										if (c.mObjThis is NativeClass)
										{
											str.Append(((NativeClass)c.mObjThis).GetClassName());
										}
										else
										{
											if (c.mObjThis is InterCodeObject)
											{
												str.Append(((InterCodeObject)c.mObjThis).GetName());
											}
											else
											{
												if (c.mObjThis is CustomObject)
												{
													string name = ((CustomObject)c.mObjThis).GetClassNames();
													if (name != null)
													{
														str.Append(name);
													}
													else
													{
														str.Append(c.mObjThis.GetType().FullName);
													}
												}
												else
												{
													str.Append(c.mObjThis.GetType().FullName);
												}
											}
										}
										str.Append(']');
									}
									else
									{
										str.Append(":0x00000000");
									}
									str.Append(')');
									ret = str.ToString();
								}
								else
								{
									// native object ?
									ret = new string("(octet) [" + val.GetType().FullName + "]");
								}
							}
						}
					}
				}
			}
			if (ret != null)
			{
				if (ret.Length > maxlen)
				{
					return Sharpen.Runtime.Substring(ret, 0, maxlen);
				}
				else
				{
					return ret;
				}
			}
			return new string(string.Empty);
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public static string VariantToExpressionString(Variant val)
		{
			// convert given variant to string which can be interpret as an expression.
			// this function does not convert objects ( returns empty string )
			if (val.IsVoid())
			{
				return "void";
			}
			else
			{
				if (val.IsInteger())
				{
					return val.AsString();
				}
				else
				{
					if (val.IsReal())
					{
						string s = Variant.RealToHexString(val.AsDouble());
						return s + " /* " + val.AsString() + " */";
					}
					else
					{
						if (val.IsString())
						{
							string s = LexBase.EscapeC(val.AsString());
							return "\"" + s + "\"";
						}
						else
						{
							if (val.IsOctet())
							{
								string s = Variant.OctetToListString(val.AsOctet());
								return "<%" + s + "%>";
							}
							else
							{
								return new string();
							}
						}
					}
				}
			}
		}

		public static string FormatString(string format, Variant[] @params)
		{
			int count = @params.Length;
			object[] args = new object[count];
			for (int i = 0; i < count; i++)
			{
				args[i] = @params[i].ToJavaObject();
				if (args[i] is string)
				{
					int length = ((string)args[i]).Length;
					if (length == 1)
					{
						args[i] = char.ValueOf(((string)args[i])[0]);
					}
				}
			}
			return string.Format(format, args);
		}
	}
}
