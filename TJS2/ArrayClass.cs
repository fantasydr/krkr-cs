/*
 * The TJS2 interpreter from kirikirij
 */

using System.Collections.Generic;
using System.Text;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class ArrayClass : NativeClass
	{
		public static int ClassID = -1;

		private static readonly string CLASS_NAME = "Array";

		protected internal override NativeInstance CreateNativeInstance()
		{
			return new ArrayNI();
		}

		// return a native object instance.
		protected internal override Dispatch2 CreateBaseTJSObject()
		{
			return new ArrayObject();
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static int GetArrayElementCount(Dispatch2 dsp)
		{
			// returns array element count
			ArrayNI ni = (ArrayNI)dsp.GetNativeInstance(ClassID);
			if (ni == null)
			{
				throw new TJSException(Error.SpecifyArray);
			}
			return ni.mItems.Count;
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public static int CopyArrayElementTo(Dispatch2 dsp, Variant[] dest, int dest_offset
			, int start, int count)
		{
			// copy array elements to specified variant array.
			// returns copied element count.
			ArrayNI ni = (ArrayNI)dsp.GetNativeInstance(ClassID);
			if (ni == null)
			{
				throw new TJSException(Error.SpecifyArray);
			}
			if (count < 0)
			{
				count = ni.mItems.Count;
			}
			if (start >= ni.mItems.Count)
			{
				return 0;
			}
			int limit = start + count;
			int d = dest_offset;
			for (int i = start; i < limit; i++)
			{
				dest[d] = ni.mItems[i];
				d++;
			}
			return limit - start;
		}

		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public ArrayClass() : base(CLASS_NAME)
		{
			int NCM_CLASSID = TJS.RegisterNativeClass(CLASS_NAME);
			SetClassID(NCM_CLASSID);
			ClassID = NCM_CLASSID;
			// constructor
			RegisterNCM(CLASS_NAME, new _NativeClassConstructor_62(), CLASS_NAME, Interface.nitMethod
				, 0);
			RegisterNCM("load", new _NativeClassMethod_74(), CLASS_NAME, Interface.nitMethod, 
				0);
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			// split to each line
			RegisterNCM("save", new _NativeClassMethod_138(), CLASS_NAME, Interface.nitMethod
				, 0);
			// saves the array into a file.
			// only string and number stuffs are stored.
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("saveStruct", new _NativeClassMethod_171(), CLASS_NAME, Interface.nitMethod
				, 0);
			// saves the array into a file, that can be interpret as an expression.
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("split", new _NativeClassMethod_198(), CLASS_NAME, Interface.nitMethod
				, 0);
			// split string with given delimiters.
			// arguments are : <pattern/delimiter>, <string>, [<reserved>],
			// [<whether ignore empty element>]
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹å�–å¾—
			// Enable REGEX
			// param[0] is regexp
			// delimiter found
			RegisterNCM("join", new _NativeClassMethod_252(), CLASS_NAME, Interface.nitMethod
				, 0);
			// join string with given delimiters.
			// arguments are : <delimiter>, [<reserved>],
			// [<whether ignore empty element>]
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			// join with delimiter
			RegisterNCM("sort", new _NativeClassMethod_286(), CLASS_NAME, Interface.nitMethod
				, 0);
			// sort array items.
			// arguments are : [<sort order/comparison function>], [<whether to do stable sort>]
			// sort order is one of:
			// '+' (default)   :  Normal ascending  (comparison by tTJSVariant::operator < )
			// '-'             :  Normal descending (comparison by tTJSVariant::operator < )
			// '0'             :  Numeric value ascending
			// '9'             :  Numeric value descending
			// 'a'             :  String ascending
			// 'z'             :  String descending
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			//boolean do_stable_sort = false;
			// check first argument
			// comarison function object
			// sort order letter
			// Collections.sortã�Œå¸¸ã�«stable_sortã�ªã�®ã�§ã�“ã‚Œã�¯æ„�å‘³ã�Œã�ªã�„
			// sort
			RegisterNCM("reverse", new _NativeClassMethod_371(), CLASS_NAME, Interface.nitMethod
				, 0);
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("assign", new _NativeClassMethod_381(), CLASS_NAME, Interface.nitMethod
				, 0);
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("assignStruct", new _NativeClassMethod_399(), CLASS_NAME, Interface.nitMethod
				, 0);
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("clear", new _NativeClassMethod_419(), CLASS_NAME, Interface.nitMethod
				, 0);
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("erase", new _NativeClassMethod_428(), CLASS_NAME, Interface.nitMethod
				, 0);
			// remove specified item number from the array
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("remove", new _NativeClassMethod_441(), CLASS_NAME, Interface.nitMethod
				, 0);
			// remove specified item from the array wchich appears first or all
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("insert", new _NativeClassMethod_462(), CLASS_NAME, Interface.nitMethod
				, 0);
			// insert item at specified position
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("add", new _NativeClassMethod_475(), CLASS_NAME, Interface.nitMethod, 
				0);
			// add item at last
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("push", new _NativeClassMethod_488(), CLASS_NAME, Interface.nitMethod
				, 0);
			// add item(s) at last
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("pop", new _NativeClassMethod_500(), CLASS_NAME, Interface.nitMethod, 
				0);
			// pop item from last
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("shift", new _NativeClassMethod_516(), CLASS_NAME, Interface.nitMethod
				, 0);
			// shift item at head
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("unshift", new _NativeClassMethod_532(), CLASS_NAME, Interface.nitMethod
				, 0);
			// add item(s) at head
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("find", new _NativeClassMethod_544(), CLASS_NAME, Interface.nitMethod
				, 0);
			// find item in the array,
			// return an index which points the item that appears first.
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("count", new _NativeClassProperty_575(), CLASS_NAME, Interface.nitProperty
				, 0);
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
			RegisterNCM("length", new _NativeClassProperty_603(), CLASS_NAME, Interface.nitProperty
				, 0);
		}

		private sealed class _NativeClassConstructor_62 : NativeClassConstructor
		{
			public _NativeClassConstructor_62()
			{
			}

			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI _this = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID
					);
				if (_this == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				int hr = _this.Construct(param, objthis);
				if (hr < 0)
				{
					return hr;
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_74 : NativeClassMethod
		{
			public _NativeClassMethod_74()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				if (objthis == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (TJS.mStorage == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				string name = param[0].AsString();
				string mode = null;
				if (param.Length >= 2 && param[1].IsVoid() == false)
				{
					mode = param[1].AsString();
				}
				ni.mItems.Clear();
				string content = TJS.mStorage.ReadText(name, mode);
				int count = content.Length;
				int lines = 0;
				int reamain = 0;
				for (int i = 0; i < count; i++)
				{
					reamain++;
					int ch = content.CodePointAt(i);
					if (ch == '\r' || ch == '\n')
					{
						if ((i + 1) < count)
						{
							if (content.CodePointAt(i + 1) == '\n' && ch == '\r')
							{
								lines++;
								i++;
							}
						}
						reamain = 0;
					}
				}
				if (reamain > 0)
				{
					lines++;
				}
				ni.mItems.Clear();
				ni.mItems.EnsureCapacity(lines);
				lines = 0;
				int start = 0;
				for (int i_1 = 0; i_1 < count; i_1++)
				{
					int ch = content.CodePointAt(i_1);
					if (ch == '\r' || ch == '\n')
					{
						ni.mItems.AddItem(new Variant(Sharpen.Runtime.Substring(content, start, i_1)));
						if ((i_1 + 1) < count)
						{
							if (content.CodePointAt(i_1 + 1) == '\n' && ch == '\r')
							{
								i_1++;
							}
						}
						start = i_1 + 1;
					}
				}
				if (start < count)
				{
					ni.mItems.AddItem(new Variant(Sharpen.Runtime.Substring(content, start, count)));
				}
				if (result != null)
				{
					result.Set(new Variant(objthis, objthis));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_138 : NativeClassMethod
		{
			public _NativeClassMethod_138()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				string name = param[0].AsString();
				string mode = null;
				if (param.Length >= 2 && param[1].IsVoid() == false)
				{
					mode = param[1].AsString();
				}
				TextWriteStreamInterface stream = TJS.mStorage.CreateTextWriteStream(name, mode);
				try
				{
					Iterator<Variant> i = ni.mItems.Iterator();
					while (i.HasNext())
					{
						Variant o = i.Next();
						if (o != null && (o.IsString() || o.IsNumber()))
						{
							stream.Write(o.AsString());
						}
						stream.Write("\n");
					}
				}
				finally
				{
					stream.Destruct();
				}
				if (result != null)
				{
					result.Set(new Variant(objthis, objthis));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_171 : NativeClassMethod
		{
			public _NativeClassMethod_171()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				string name = param[0].AsString();
				string mode = null;
				if (param.Length >= 2 && param[1].IsVoid() == false)
				{
					mode = param[1].AsString();
				}
				TextWriteStreamInterface stream = TJS.mStorage.CreateTextWriteStream(name, mode);
				try
				{
					AList<Dispatch2> stack = new AList<Dispatch2>();
					stack.AddItem(objthis);
					ni.SaveStructuredData(stack, stream, string.Empty);
				}
				finally
				{
					stream.Destruct();
				}
				if (result != null)
				{
					result.Set(new Variant(objthis, objthis));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_198 : NativeClassMethod
		{
			public _NativeClassMethod_198()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 2)
				{
					return Error.E_BADPARAMCOUNT;
				}
				ni.mItems.Clear();
				string @string = param[1].AsString();
				bool purgeempty = false;
				if (param.Length >= 4 && param[3].IsVoid() == false)
				{
					purgeempty = param[3].AsBoolean();
				}
				if (param[0].IsObject())
				{
					RegExpNI re;
					VariantClosure clo = param[0].AsObjectClosure();
					if (clo.mObject != null)
					{
						if ((re = (RegExpNI)clo.mObject.GetNativeInstance(RegExpClass.mClassID)) != null)
						{
							Holder<Dispatch2> array = new Holder<Dispatch2>(objthis);
							re.Split(array, @string, purgeempty);
							if (result != null)
							{
								result.Set(new Variant(objthis, objthis));
							}
							return Error.S_OK;
						}
					}
				}
				string pattern = param[0].AsString();
				int count = @string.Length;
				int start = 0;
				for (int i = 0; i < count; i++)
				{
					int ch = @string.CodePointAt(i);
					if (pattern.IndexOf(ch) != -1)
					{
						if (purgeempty == false || (purgeempty == true && (i - start) != 0))
						{
							ni.mItems.AddItem(new Variant(Sharpen.Runtime.Substring(@string, start, i)));
						}
						start = i + 1;
					}
				}
				if (purgeempty == false || (purgeempty == true && (count - start) >= 0))
				{
					ni.mItems.AddItem(new Variant(Sharpen.Runtime.Substring(@string, start, count)));
				}
				if (result != null)
				{
					result.Set(new Variant(objthis, objthis));
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_252 : NativeClassMethod
		{
			public _NativeClassMethod_252()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				string delimiter = param[0].AsString();
				bool purgeempty = false;
				if (param.Length >= 3 && param[2].IsVoid() != true)
				{
					purgeempty = param[2].AsBoolean();
				}
				bool first = true;
				StringBuilder builer = new StringBuilder(1024);
				int count = ni.mItems.Count;
				for (int i = 0; i < count; i++)
				{
					Variant v = ni.mItems[i];
					if (purgeempty && v.IsVoid())
					{
					}
					else
					{
						if (!first)
						{
							builer.Append(delimiter);
						}
						first = false;
						builer.Append(v.AsString());
					}
				}
				if (result != null)
				{
					result.Set(builer.ToString());
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_286 : NativeClassMethod
		{
			public _NativeClassMethod_286()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				char method = '+';
				VariantClosure closure = null;
				if (param.Length >= 1 && param[0].IsVoid() != true)
				{
					if (param[0].IsObject())
					{
						closure = param[0].AsObjectClosure();
						method = (char)0;
					}
					else
					{
						string me = param[0].AsString();
						method = me[0];
						switch (method)
						{
							case '+':
							case '-':
							case '0':
							case '9':
							case 'a':
							case 'z':
							{
								break;
							}

							default:
							{
								method = '+';
								break;
								break;
							}
						}
					}
				}
				switch (method)
				{
					case '+':
					{
						ni.mItems.Sort(new ArrayClass.ArraySortCompare_NormalAscending());
						break;
					}

					case '-':
					{
						ni.mItems.Sort(new ArrayClass.ArraySortCompare_NormalDescending());
						break;
					}

					case '0':
					{
						ni.mItems.Sort(new ArrayClass.ArraySortCompare_NumericAscending());
						break;
					}

					case '9':
					{
						ni.mItems.Sort(new ArrayClass.ArraySortCompare_NumericDescending());
						break;
					}

					case 'a':
					{
						ni.mItems.Sort(new ArrayClass.ArraySortCompare_StringAscending());
						break;
					}

					case 'z':
					{
						ni.mItems.Sort(new ArrayClass.ArraySortCompare_StringDescending());
						break;
					}

					case 0:
					{
						ni.mItems.Sort(new ArrayClass.ArraySortCompare_Functional(closure));
						break;
					}
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_371 : NativeClassMethod
		{
			public _NativeClassMethod_371()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				Collections.Reverse(ni.mItems);
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_381 : NativeClassMethod
		{
			public _NativeClassMethod_381()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				((ArrayObject)objthis).Clear(ni);
				VariantClosure clo = param[0].AsObjectClosure();
				if (clo.mObjThis != null)
				{
					ni.Assign(clo.mObjThis);
				}
				else
				{
					if (clo.mObject != null)
					{
						ni.Assign(clo.mObject);
					}
					else
					{
						throw new TJSException(Error.NullAccess);
					}
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_399 : NativeClassMethod
		{
			public _NativeClassMethod_399()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				((ArrayObject)objthis).Clear(ni);
				AList<Dispatch2> stack = new AList<Dispatch2>();
				VariantClosure clo = param[0].AsObjectClosure();
				if (clo.mObjThis != null)
				{
					ni.AssignStructure(clo.mObjThis, stack);
				}
				else
				{
					if (clo.mObject != null)
					{
						ni.AssignStructure(clo.mObject, stack);
					}
					else
					{
						throw new TJSException(Error.NullAccess);
					}
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_419 : NativeClassMethod
		{
			public _NativeClassMethod_419()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				((ArrayObject)objthis).Clear(ni);
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_428 : NativeClassMethod
		{
			public _NativeClassMethod_428()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				int num = param[0].AsInteger();
				((ArrayObject)objthis).Erase(ni, num);
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_441 : NativeClassMethod
		{
			public _NativeClassMethod_441()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				bool eraseall;
				if (param.Length >= 2)
				{
					eraseall = param[1].AsBoolean();
				}
				else
				{
					eraseall = true;
				}
				Variant val = param[0];
				int count = ((ArrayObject)objthis).Remove(ni, val, eraseall);
				if (result != null)
				{
					result.Set(count);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_462 : NativeClassMethod
		{
			public _NativeClassMethod_462()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 2)
				{
					return Error.E_BADPARAMCOUNT;
				}
				int num = param[0].AsInteger();
				((ArrayObject)objthis).Insert(ni, param[1], num);
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_475 : NativeClassMethod
		{
			public _NativeClassMethod_475()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				((ArrayObject)objthis).Add(ni, param[0]);
				if (result != null)
				{
					result.Set(ni.mItems.Count - 1);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_488 : NativeClassMethod
		{
			public _NativeClassMethod_488()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				((ArrayObject)objthis).Insert(ni, param, ni.mItems.Count);
				if (result != null)
				{
					result.Set(ni.mItems.Count);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_500 : NativeClassMethod
		{
			public _NativeClassMethod_500()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (ni.mItems.IsEmpty())
				{
					if (result != null)
					{
						result.Clear();
					}
				}
				else
				{
					if (result != null)
					{
						result.Set(ni.mItems[ni.mItems.Count - 1]);
					}
					((ArrayObject)objthis).Erase(ni, ni.mItems.Count - 1);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_516 : NativeClassMethod
		{
			public _NativeClassMethod_516()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (ni.mItems.IsEmpty())
				{
					if (result != null)
					{
						result.Clear();
					}
				}
				else
				{
					if (result != null)
					{
						result.Set(ni.mItems[0]);
					}
					((ArrayObject)objthis).Erase(ni, 0);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_532 : NativeClassMethod
		{
			public _NativeClassMethod_532()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				((ArrayObject)objthis).Insert(ni, param, 0);
				if (result != null)
				{
					result.Set(ni.mItems.Count);
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassMethod_544 : NativeClassMethod
		{
			public _NativeClassMethod_544()
			{
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			protected internal override int Process(Variant result, Variant[] param, Dispatch2
				 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (param.Length < 1)
				{
					return Error.E_BADPARAMCOUNT;
				}
				if (result != null)
				{
					Variant val = param[0];
					int start = 0;
					if (param.Length >= 2)
					{
						start = param[1].AsInteger();
					}
					if (start < 0)
					{
						start += ni.mItems.Count;
					}
					if (start < 0)
					{
						start = 0;
					}
					if (start >= ni.mItems.Count)
					{
						result.Set(-1);
						return Error.S_OK;
					}
					int count = ni.mItems.Count;
					int i;
					for (i = start; i < count; i++)
					{
						Variant v = ni.mItems[i];
						if (val.DiscernCompareInternal(v))
						{
							break;
						}
					}
					if (i == count)
					{
						result.Set(-1);
					}
					else
					{
						result.Set(i);
					}
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassProperty_575 : NativeClassProperty
		{
			public _NativeClassProperty_575()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (result != null)
				{
					result.Set(ni.mItems.Count);
				}
				return Error.S_OK;
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			public override int Set(Variant param, Dispatch2 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				int resize = param.AsInteger();
				int count = ni.mItems.Count;
				if (count < resize)
				{
					int addcount = resize - count;
					ni.mItems.EnsureCapacity(count);
					for (int i = 0; i < addcount; i++)
					{
						ni.mItems.AddItem(new Variant());
					}
				}
				else
				{
					if (count > resize)
					{
						for (int i = count - 1; i >= resize; i--)
						{
							ni.mItems.Remove(i);
						}
					}
				}
				return Error.S_OK;
			}
		}

		private sealed class _NativeClassProperty_603 : NativeClassProperty
		{
			public _NativeClassProperty_603()
			{
			}

			public override int Get(Variant result, Dispatch2 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				if (result != null)
				{
					result.Set(ni.mItems.Count);
				}
				return Error.S_OK;
			}

			/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
			public override int Set(Variant param, Dispatch2 objthis)
			{
				ArrayNI ni = (ArrayNI)objthis.GetNativeInstance(Kirikiri.Tjs2.ArrayClass.ClassID);
				// ã‚¤ãƒ³ã‚¹ã‚¿ãƒ³ã‚¹æ‰€å¾—
				if (ni == null)
				{
					return Error.E_NATIVECLASSCRASH;
				}
				int resize = param.AsInteger();
				int count = ni.mItems.Count;
				if (count < resize)
				{
					int addcount = resize - count;
					ni.mItems.EnsureCapacity(count);
					for (int i = 0; i < addcount; i++)
					{
						ni.mItems.AddItem(new Variant());
					}
				}
				else
				{
					if (count > resize)
					{
						for (int i = count - 1; i >= resize; i--)
						{
							ni.mItems.Remove(i);
						}
					}
				}
				return Error.S_OK;
			}
		}

		public class ArraySortCompare_NormalAscending : IComparer<Variant>
		{
			//ArrayNI.ClassID_Array = NCM_CLASSID;
			public virtual int Compare(Variant lhs, Variant rhs)
			{
				try
				{
					return lhs.GreaterThanForSort(rhs);
				}
				catch (VariantException)
				{
					return 0;
				}
			}
		}

		public class ArraySortCompare_NormalDescending : IComparer<Variant>
		{
			public virtual int Compare(Variant lhs, Variant rhs)
			{
				try
				{
					return lhs.LittlerThanForSort(rhs);
				}
				catch (VariantException)
				{
					return 0;
				}
			}
		}

		public class ArraySortCompare_NumericAscending : IComparer<Variant>
		{
			public virtual int Compare(Variant lhs, Variant rhs)
			{
				try
				{
					if (lhs.IsString() && rhs.IsString())
					{
						Number l = lhs.AsNumber();
						Number r = rhs.AsNumber();
						double ret = l - r;
						if (ret == 0.0)
						{
							return 0;
						}
						else
						{
							if (ret < 0.0)
							{
								return -1;
							}
							else
							{
								return 1;
							}
						}
					}
					return lhs.GreaterThanForSort(rhs);
				}
				catch (VariantException)
				{
					return 0;
				}
			}
		}

		public class ArraySortCompare_NumericDescending : IComparer<Variant>
		{
			public virtual int Compare(Variant lhs, Variant rhs)
			{
				try
				{
					if (lhs.IsString() && rhs.IsString())
					{
						Number l = lhs.AsNumber();
						Number r = rhs.AsNumber();
						double ret = r - l;
						if (ret == 0.0)
						{
							return 0;
						}
						else
						{
							if (ret < 0.0)
							{
								return -1;
							}
							else
							{
								return 1;
							}
						}
					}
					return lhs.LittlerThanForSort(rhs);
				}
				catch (VariantException)
				{
					return 0;
				}
			}
		}

		public class ArraySortCompare_StringAscending : IComparer<Variant>
		{
			public virtual int Compare(Variant lhs, Variant rhs)
			{
				try
				{
					if (lhs.IsString() && rhs.IsString())
					{
						string l = lhs.GetString();
						string r = rhs.GetString();
						return Sharpen.Runtime.CompareOrdinal(l, r);
					}
					else
					{
						string l = lhs.AsString();
						string r = rhs.AsString();
						return Sharpen.Runtime.CompareOrdinal(l, r);
					}
				}
				catch (VariantException)
				{
					return 0;
				}
			}
		}

		public class ArraySortCompare_StringDescending : IComparer<Variant>
		{
			public virtual int Compare(Variant lhs, Variant rhs)
			{
				try
				{
					if (lhs.IsString() && rhs.IsString())
					{
						string l = lhs.GetString();
						string r = rhs.GetString();
						return Sharpen.Runtime.CompareOrdinal(r, l);
					}
					else
					{
						string l = lhs.AsString();
						string r = rhs.AsString();
						return Sharpen.Runtime.CompareOrdinal(r, l);
					}
				}
				catch (VariantException)
				{
					return 0;
				}
			}
		}

		public class ArraySortCompare_Functional : IComparer<Variant>
		{
			private VariantClosure mClosure;

			public ArraySortCompare_Functional(VariantClosure clo)
			{
				mClosure = clo;
			}

			public virtual int Compare(Variant lhs, Variant rhs)
			{
				try
				{
					Variant result = new Variant();
					Variant[] @params = new Variant[2];
					@params[0] = lhs;
					@params[1] = rhs;
					int hr = mClosure.FuncCall(0, null, result, @params, null);
					if (hr < 0)
					{
						Error.ThrowFrom_tjs_error(hr, null);
					}
					bool ret = result.AsBoolean();
					return ret ? -1 : 1;
				}
				catch (VariantException)
				{
					return 0;
				}
				catch (TJSException)
				{
					return 0;
				}
			}
			// ã‚½ãƒ¼ãƒˆã�®æ™‚ä¾‹å¤–ã�®æ‰±ã�„ã�Œå¤‰ã‚�ã‚‹
		}
	}
}
