/*
 * The TJS2 interpreter from kirikirij
 */

using System;
using System.Text;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class LexBase
	{
		protected internal StringStream mStream;

		public LexBase(string str)
		{
			//private static final String TAG = "TokenStraem";
			//private static final boolean LOGD = false;
			mStream = new StringStream(str);
		}

		public LexBase()
		{
		}

		//mStream = null;
		private bool ParseExtractNumber(int basebits)
		{
			bool point_found = false;
			bool exp_found = false;
			int offset = mStream.GetOffset();
			int c = mStream.Next();
			while (c != -1)
			{
				if (c == '.' && point_found == false && exp_found == false)
				{
					point_found = true;
					c = mStream.Next();
				}
				else
				{
					if ((c == 'p' || c == 'P') && exp_found == false)
					{
						exp_found = true;
						mStream.SkipSpace();
						c = mStream.Next();
						if (c == '+' || c == '-')
						{
							mStream.SkipSpace();
							c = mStream.Next();
						}
					}
					else
					{
						if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
						{
							if (basebits == 3)
							{
								if (c < '0' || c > '7')
								{
									break;
								}
							}
							else
							{
								if (basebits == 1)
								{
									if (c != '0' && c != '1')
									{
										break;
									}
								}
							}
							c = mStream.Next();
						}
						else
						{
							break;
						}
					}
				}
			}
			mStream.SetOffset(offset);
			return point_found || exp_found;
		}

		private const int TJS_IEEE_D_SIGNIFICAND_BITS = 52;

		private const int TJS_IEEE_D_EXP_MIN = -1022;

		private const int TJS_IEEE_D_EXP_MAX = 1023;

		private const long TJS_IEEE_D_EXP_BIAS = 1023;

		// base
		// 16é€²æ•° : 4
		// 2é€²æ•° : 1
		// 8é€²æ•° : 3
		private double ParseNonDecimalReal(bool sign, int basebits)
		{
			long main = 0;
			int exp = 0;
			int numsignif = 0;
			bool pointpassed = false;
			int c = mStream.GetC();
			while (c != -1)
			{
				if (c == '.')
				{
					pointpassed = true;
				}
				else
				{
					if (c == 'p' || c == 'P')
					{
						mStream.SkipSpace();
						c = mStream.Next();
						bool biassign = false;
						if (c == '+')
						{
							biassign = false;
							mStream.SkipSpace();
							c = mStream.Next();
						}
						if (c == '-')
						{
							biassign = true;
							mStream.SkipSpace();
							c = mStream.Next();
						}
						int bias = 0;
						while (c >= '0' && c <= '9')
						{
							bias *= 10;
							bias += c - '0';
							c = mStream.Next();
						}
						if (biassign)
						{
							bias = -bias;
						}
						exp += bias;
						break;
					}
					else
					{
						int n = -1;
						if (basebits == 4)
						{
							if (c >= '0' && c <= '9')
							{
								n = c - '0';
							}
							else
							{
								if (c >= 'a' && c <= 'f')
								{
									n = c - 'a' + 10;
								}
								else
								{
									if (c >= 'A' && c <= 'F')
									{
										n = c - 'A' + 10;
									}
									else
									{
										break;
									}
								}
							}
						}
						else
						{
							if (basebits == 3)
							{
								if (c >= '0' && c <= '7')
								{
									n = c - '0';
								}
								else
								{
									break;
								}
							}
							else
							{
								if (basebits == 1)
								{
									if (c == '0' || c == '1')
									{
										n = c - '0';
									}
									else
									{
										break;
									}
								}
							}
						}
						if (numsignif == 0)
						{
							int b = basebits - 1;
							while (b >= 0)
							{
								if (((1 << b) & n) != 0)
								{
									break;
								}
								b--;
							}
							b++;
							if (b != 0)
							{
								// n is not zero
								numsignif = b;
								main |= ((long)n << (64 - numsignif));
								if (pointpassed)
								{
									exp -= (basebits - b + 1);
								}
								else
								{
									exp = b - 1;
								}
							}
							else
							{
								// n is zero
								if (pointpassed)
								{
									exp -= basebits;
								}
							}
						}
						else
						{
							// append to main
							if ((numsignif + basebits) < 64)
							{
								numsignif += basebits;
								main |= ((long)n << (64 - numsignif));
							}
							if (pointpassed == false)
							{
								exp += basebits;
							}
						}
					}
				}
				c = mStream.Next();
			}
			if (c != -1)
			{
				mStream.UngetC();
			}
			main = (long)(((ulong)main) >> (64 - 1 - TJS_IEEE_D_SIGNIFICAND_BITS));
			if (main == 0)
			{
				return double.ValueOf(0.0);
			}
			main &= ((1L << TJS_IEEE_D_SIGNIFICAND_BITS) - 1L);
			if (exp < TJS_IEEE_D_EXP_MIN)
			{
				return double.ValueOf(0.0);
			}
			if (exp > TJS_IEEE_D_EXP_MAX)
			{
				if (sign)
				{
					return double.ValueOf(double.NegativeInfinity);
				}
				else
				{
					return double.ValueOf(double.PositiveInfinity);
				}
			}
			// compose IEEE double
			//double d = Double.longBitsToDouble(0x8000000000000000L | ((exp + TJS_IEEE_D_EXP_BIAS) << 52) | main);
			double d = double.LongBitsToDouble((((long)exp + TJS_IEEE_D_EXP_BIAS) << 52) | main
				);
			if (sign)
			{
				d = -d;
			}
			return double.ValueOf(d);
		}

		private int ParseNonDecimalInteger16(bool sign)
		{
			long v = 0;
			int c = mStream.Next();
			while (c != -1)
			{
				int n = -1;
				if (c >= '0' && c <= '9')
				{
					n = c - '0';
				}
				else
				{
					if (c >= 'a' && c <= 'f')
					{
						n = c - 'a' + 10;
					}
					else
					{
						if (c >= 'A' && c <= 'F')
						{
							n = c - 'A' + 10;
						}
						else
						{
							mStream.UngetC();
							break;
						}
					}
				}
				v <<= 4;
				v += n;
				c = mStream.Next();
			}
			if (sign)
			{
				return Sharpen.Extensions.ValueOf((int)-v);
			}
			else
			{
				return Sharpen.Extensions.ValueOf((int)v);
			}
		}

		private int ParseNonDecimalInteger8(bool sign)
		{
			long v = 0;
			int c = mStream.Next();
			while (c != -1)
			{
				int n = -1;
				if (c >= '0' && c <= '7')
				{
					n = c - '0';
				}
				else
				{
					mStream.UngetC();
					break;
				}
				v <<= 3;
				v += n;
				c = mStream.Next();
			}
			if (sign)
			{
				return Sharpen.Extensions.ValueOf((int)-v);
			}
			else
			{
				return Sharpen.Extensions.ValueOf((int)v);
			}
		}

		private int ParseNonDecimalInteger2(bool sign)
		{
			long v = 0;
			int c = mStream.Next();
			while (c != -1)
			{
				if (c == '1')
				{
					v <<= 1;
					v++;
				}
				else
				{
					if (c == '0')
					{
						v <<= 1;
					}
					else
					{
						mStream.UngetC();
						break;
					}
				}
				c = mStream.Next();
			}
			if (sign)
			{
				return Sharpen.Extensions.ValueOf((int)-v);
			}
			else
			{
				return Sharpen.Extensions.ValueOf((int)v);
			}
		}

		private Number ParseNonDecimalNumber(bool sign, int @base)
		{
			bool is_real = ParseExtractNumber(@base);
			if (is_real)
			{
				return ParseNonDecimalReal(sign, @base);
			}
			else
			{
				switch (@base)
				{
					case 4:
					{
						return ParseNonDecimalInteger16(sign);
					}

					case 3:
					{
						return ParseNonDecimalInteger8(sign);
					}

					case 1:
					{
						return ParseNonDecimalInteger2(sign);
					}
				}
			}
			return null;
		}

		protected internal virtual bool IsWAlpha(int c)
		{
			if ((c & unchecked((int)(0xFF00))) != 0)
			{
				return true;
			}
			else
			{
				if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
				{
					return true;
				}
			}
			return false;
		}

		protected internal virtual bool IsWDigit(int c)
		{
			if ((c & unchecked((int)(0xFF00))) != 0)
			{
				return false;
			}
			else
			{
				if (c >= '0' && c <= '9')
				{
					return true;
				}
			}
			return false;
		}

		// @return : Integer or Double or null
		public virtual Number ParseNumber()
		{
			int num = 0;
			bool sign = false;
			bool skipNum = false;
			int c = mStream.GetC();
			if (c == '+')
			{
				sign = false;
				c = mStream.GetC();
			}
			else
			{
				if (c == '-')
				{
					sign = true;
					c = mStream.GetC();
				}
			}
			if (c == 't')
			{
				if (mStream.EqualString("rue"))
				{
					return Sharpen.Extensions.ValueOf(1);
				}
			}
			else
			{
				if (c == 'f')
				{
					if (mStream.EqualString("alse"))
					{
						return Sharpen.Extensions.ValueOf(0);
					}
				}
				else
				{
					if (c == 'N')
					{
						if (mStream.EqualString("aN"))
						{
							return double.ValueOf(double.NaN);
						}
					}
					else
					{
						if (c == 'I')
						{
							if (mStream.EqualString("nfinity"))
							{
								if (sign)
								{
									return double.ValueOf(double.NegativeInfinity);
								}
								else
								{
									return double.ValueOf(double.PositiveInfinity);
								}
							}
						}
					}
				}
			}
			//int save = mStream.getOffset();
			// 10é€²æ•°ä»¥å¤–ã�‹èª¿ã�¹ã‚‹
			if (c == '0')
			{
				c = mStream.GetC();
				if (c == 'x' || c == 'X')
				{
					// hexadecimal
					return ParseNonDecimalNumber(sign, 4);
				}
				else
				{
					if (c == 'b' || c == 'B')
					{
						// binary
						return ParseNonDecimalNumber(sign, 1);
					}
					else
					{
						if (c == '.')
						{
							skipNum = true;
						}
						else
						{
							if (c == 'e' || c == 'E')
							{
								skipNum = true;
							}
							else
							{
								if (c == 'p' || c == 'P')
								{
									// 2^n exp
									return null;
								}
								else
								{
									if (c >= '0' && c <= '7')
									{
										// octal
										mStream.UngetC();
										return ParseNonDecimalNumber(sign, 3);
									}
								}
							}
						}
					}
				}
			}
			if (skipNum == false)
			{
				while (c != -1)
				{
					if (c < '0' || c > '9')
					{
						break;
					}
					num = num * 10 + (c - '0');
					c = mStream.GetC();
				}
			}
			if (c == '.' || c == 'e' || c == 'E')
			{
				double figure = 1.0;
				int decimal = 0;
				if (c == '.')
				{
					while (c != -1)
					{
						c = mStream.GetC();
						if (c < '0' || c > '9')
						{
							break;
						}
						decimal = decimal * 10 + (c - '0');
						figure *= 10;
					}
				}
				bool expSign = false;
				int expValue = 0;
				if (c == 'e' || c == 'E')
				{
					c = mStream.GetC();
					if (c == '-')
					{
						expSign = true;
						c = mStream.GetC();
					}
					while (c != -1)
					{
						if (c < '0' || c > '9')
						{
							break;
						}
						expValue = expValue * 10 + (c - '0');
						c = mStream.GetC();
					}
					mStream.UngetC();
				}
				else
				{
					mStream.UngetC();
				}
				double number = (double)num + ((double)decimal / figure);
				if (expValue != 0)
				{
					if (expSign == false)
					{
						number *= Math.Pow(10, expValue);
					}
					else
					{
						number /= Math.Pow(10, expValue);
					}
				}
				if (sign)
				{
					number = -number;
				}
				return double.ValueOf(number);
			}
			else
			{
				mStream.UngetC();
				if (sign)
				{
					num = -num;
				}
				return Sharpen.Extensions.ValueOf(num);
			}
		}

		public static string EscapeC(char c)
		{
			StringBuilder ret = new StringBuilder(16);
			switch (c)
			{
				case unchecked((int)(0x07)):
				{
					ret.Append("\\a");
					break;
				}

				case unchecked((int)(0x08)):
				{
					ret.Append("\\b");
					break;
				}

				case unchecked((int)(0x0c)):
				{
					ret.Append("\\f");
					break;
				}

				case unchecked((int)(0x0a)):
				{
					ret.Append("\\n");
					break;
				}

				case unchecked((int)(0x0d)):
				{
					ret.Append("\\r");
					break;
				}

				case unchecked((int)(0x09)):
				{
					ret.Append("\\t");
					break;
				}

				case unchecked((int)(0x0b)):
				{
					ret.Append("\\v");
					break;
				}

				case '\\':
				{
					ret.Append("\\\\");
					break;
				}

				case '\'':
				{
					ret.Append("\\\'");
					break;
				}

				case '\"':
				{
					ret.Append("\\\"");
					break;
				}

				default:
				{
					if (c < unchecked((int)(0x20)))
					{
						ret.Append("\\x");
						ret.Append(Sharpen.Extensions.ToHexString((int)c));
					}
					else
					{
						ret.Append(c);
					}
					break;
				}
			}
			return ret.ToString();
		}

		public static string EscapeC(string str)
		{
			int count = str.Length;
			StringBuilder ret = new StringBuilder(count * 2);
			for (int i = 0; i < count; i++)
			{
				char c = str[i];
				switch (c)
				{
					case unchecked((int)(0x07)):
					{
						ret.Append("\\a");
						break;
					}

					case unchecked((int)(0x08)):
					{
						ret.Append("\\b");
						break;
					}

					case unchecked((int)(0x0c)):
					{
						ret.Append("\\f");
						break;
					}

					case unchecked((int)(0x0a)):
					{
						ret.Append("\\n");
						break;
					}

					case unchecked((int)(0x0d)):
					{
						ret.Append("\\r");
						break;
					}

					case unchecked((int)(0x09)):
					{
						ret.Append("\\t");
						break;
					}

					case unchecked((int)(0x0b)):
					{
						ret.Append("\\v");
						break;
					}

					case '\\':
					{
						ret.Append("\\\\");
						break;
					}

					case '\'':
					{
						ret.Append("\\\'");
						break;
					}

					case '\"':
					{
						ret.Append("\\\"");
						break;
					}

					default:
					{
						if (c < unchecked((int)(0x20)))
						{
							ret.Append("\\x");
							ret.Append(Sharpen.Extensions.ToHexString((int)c));
						}
						else
						{
							ret.Append(c);
						}
						break;
					}
				}
			}
			return ret.ToString();
		}
	}
}
