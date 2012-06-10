/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	/// <summary>TJS2 バイトコードを读み迂んで、ScriptBlock を返す</summary>
	public class ByteCodeLoader
	{
		private const bool LOAD_SRC_POS = false;

		public const int FILE_TAG_LE = ('T') | ('J' << 8) | ('S' << 16) | ('2' << 24);

		public const int VER_TAG_LE = ('1') | ('0' << 8) | ('0' << 16) | (0 << 24);

		private const int OBJ_TAG_LE = ('O') | ('B' << 8) | ('J' << 16) | ('S' << 24);

		private const int DATA_TAG_LE = ('D') | ('A' << 8) | ('T' << 16) | ('A' << 24);

		private const int TYPE_VOID = 0;

		private const int TYPE_OBJECT = 1;

		private const int TYPE_INTER_OBJECT = 2;

		private const int TYPE_STRING = 3;

		private const int TYPE_OCTET = 4;

		private const int TYPE_REAL = 5;

		private const int TYPE_BYTE = 6;

		private const int TYPE_SHORT = 7;

		private const int TYPE_INTEGER = 8;

		private const int TYPE_LONG = 9;

		private const int TYPE_INTER_GENERATOR = 10;

		private const int TYPE_UNKNOWN = -1;

		private static byte[] mByteArray;

		private static short[] mShortArray;

		private static int[] mIntArray;

		private static long[] mLongArray;

		private static double[] mDoubleArray;

		private static long[] mDoubleTmpArray;

		private static string[] mStringArray;

		private static ByteBuffer[] mByteBufferArray;

		private static short[] mVariantTypeData;

		private const int MIN_BYTE_COUNT = 64;

		private const int MIN_SHORT_COUNT = 64;

		private const int MIN_INT_COUNT = 64;

		private const int MIN_DOUBLE_COUNT = 8;

		private const int MIN_LONG_COUNT = 8;

		private const int MIN_STRING_COUNT = 1024;

		private static bool mDeleteBuffer;

		private static byte[] mReadBuffer;

		private const int MIN_READ_BUFFER_SIZE = 160 * 1024;

		internal class ObjectsCache
		{
			public InterCodeObject[] mObjs;

			public AList<ByteCodeLoader.VariantRepalace> mWork;

			public int[] mParent;

			public int[] mPropSetter;

			public int[] mPropGetter;

			public int[] mSuperClassGetter;

			public int[][] mProperties;

			private const int MIN_COUNT = 500;

			// temporary
			//static private final int MIN_VARIANT_DATA_COUNT = 400*2;
			public virtual void Create(int count)
			{
				if (count < MIN_COUNT)
				{
					count = MIN_COUNT;
				}
				if (mWork == null)
				{
					mWork = new AList<ByteCodeLoader.VariantRepalace>();
				}
				mWork.Clear();
				if (mObjs == null || mObjs.Length < count)
				{
					mObjs = new InterCodeObject[count];
					mParent = new int[count];
					mPropSetter = new int[count];
					mPropGetter = new int[count];
					mSuperClassGetter = new int[count];
					mProperties = new int[count][];
				}
			}

			public virtual void Release()
			{
				mWork = null;
				mObjs = null;
				mParent = null;
				mPropSetter = null;
				mPropGetter = null;
				mSuperClassGetter = null;
				mProperties = null;
			}
		}

		private static ByteCodeLoader.ObjectsCache mObjectsCache;

		public static void Initialize()
		{
			mDeleteBuffer = false;
			mReadBuffer = null;
			mByteArray = null;
			mShortArray = null;
			mIntArray = null;
			mLongArray = null;
			mDoubleArray = null;
			mDoubleTmpArray = null;
			mStringArray = null;
			mByteBufferArray = null;
			mObjectsCache = new ByteCodeLoader.ObjectsCache();
			mVariantTypeData = null;
		}

		public static void FinalizeApplication()
		{
			mDeleteBuffer = true;
			mReadBuffer = null;
			mByteArray = null;
			mShortArray = null;
			mIntArray = null;
			mLongArray = null;
			mDoubleArray = null;
			mDoubleTmpArray = null;
			mStringArray = null;
			mByteBufferArray = null;
			mObjectsCache = null;
			mVariantTypeData = null;
		}

		public static void AllwaysFreeReadBuffer()
		{
			mDeleteBuffer = true;
			mReadBuffer = null;
			mByteArray = null;
			mShortArray = null;
			mIntArray = null;
			mLongArray = null;
			mDoubleArray = null;
			mDoubleTmpArray = null;
			mStringArray = null;
			mByteBufferArray = null;
			mObjectsCache.Release();
			mVariantTypeData = null;
		}

		public ByteCodeLoader()
		{
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		public virtual ScriptBlock ReadByteCode(TJS owner, string name, BinaryStream input
			)
		{
			try
			{
				int size = (int)input.GetSize();
				if (mReadBuffer == null || mReadBuffer.Length < size)
				{
					int buflen = size < MIN_READ_BUFFER_SIZE ? MIN_READ_BUFFER_SIZE : size;
					mReadBuffer = new byte[buflen];
				}
				byte[] databuff = mReadBuffer;
				input.Read(databuff);
				input.Close();
				input = null;
				// TJS2
				int tag = (databuff[0] & unchecked((int)(0xff))) | (databuff[1] & unchecked((int)
					(0xff))) << 8 | (databuff[2] & unchecked((int)(0xff))) << 16 | (databuff[3] & unchecked(
					(int)(0xff))) << 24;
				if (tag != FILE_TAG_LE)
				{
					return null;
				}
				// 100'\0'
				int ver = (databuff[4] & unchecked((int)(0xff))) | (databuff[5] & unchecked((int)
					(0xff))) << 8 | (databuff[6] & unchecked((int)(0xff))) << 16 | (databuff[7] & unchecked(
					(int)(0xff))) << 24;
				if (ver != VER_TAG_LE)
				{
					return null;
				}
				int filesize = (databuff[8] & unchecked((int)(0xff))) | (databuff[9] & unchecked(
					(int)(0xff))) << 8 | (databuff[10] & unchecked((int)(0xff))) << 16 | (databuff[11
					] & unchecked((int)(0xff))) << 24;
				if (filesize != size)
				{
					return null;
				}
				//// DATA
				tag = (databuff[12] & unchecked((int)(0xff))) | (databuff[13] & unchecked((int)(0xff
					))) << 8 | (databuff[14] & unchecked((int)(0xff))) << 16 | (databuff[15] & unchecked(
					(int)(0xff))) << 24;
				if (tag != DATA_TAG_LE)
				{
					return null;
				}
				size = (databuff[16] & unchecked((int)(0xff))) | (databuff[17] & unchecked((int)(
					0xff))) << 8 | (databuff[18] & unchecked((int)(0xff))) << 16 | (databuff[19] & unchecked(
					(int)(0xff))) << 24;
				ReadDataArea(databuff, 20, size);
				int offset = 12 + size;
				// これがデータエリア后の位置
				// OBJS
				tag = (databuff[offset] & unchecked((int)(0xff))) | (databuff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (databuff[offset + 2] & unchecked((int)(0xff))) << 16 | (databuff
					[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				if (tag != OBJ_TAG_LE)
				{
					return null;
				}
				//int objsize = ibuff.get();
				int objsize = (databuff[offset] & unchecked((int)(0xff))) | (databuff[offset + 1]
					 & unchecked((int)(0xff))) << 8 | (databuff[offset + 2] & unchecked((int)(0xff))
					) << 16 | (databuff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				ScriptBlock block = new ScriptBlock(owner, name, 0, null, null);
				ReadObjects(block, databuff, offset, objsize);
				return block;
			}
			finally
			{
				if (mDeleteBuffer)
				{
					mReadBuffer = null;
					mByteArray = null;
					mShortArray = null;
					mIntArray = null;
					mLongArray = null;
					mDoubleArray = null;
					mDoubleTmpArray = null;
					mStringArray = null;
					mByteBufferArray = null;
					mObjectsCache.Release();
					mVariantTypeData = null;
				}
			}
		}

		/// <summary>InterCodeObject へ置换するために一时的に觉えておくクラス</summary>
		internal class VariantRepalace
		{
			public Variant Work;

			public int Index;

			public VariantRepalace(Variant w, int i)
			{
				Work = w;
				Index = i;
			}
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		private void ReadObjects(ScriptBlock block, byte[] buff, int offset, int size)
		{
			string[] strarray = mStringArray;
			ByteBuffer[] bbarray = mByteBufferArray;
			double[] dblarray = mDoubleArray;
			byte[] barray = mByteArray;
			short[] sarray = mShortArray;
			int[] iarray = mIntArray;
			long[] larray = mLongArray;
			int toplevel = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
				(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			int objcount = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
				(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			//Log.v("test","count:"+objcount);
			mObjectsCache.Create(objcount);
			InterCodeObject[] objs = mObjectsCache.mObjs;
			AList<ByteCodeLoader.VariantRepalace> work = mObjectsCache.mWork;
			int[] parent = mObjectsCache.mParent;
			int[] propSetter = mObjectsCache.mPropSetter;
			int[] propGetter = mObjectsCache.mPropGetter;
			int[] superClassGetter = mObjectsCache.mSuperClassGetter;
			int[][] properties = mObjectsCache.mProperties;
			for (int o = 0; o < objcount; o++)
			{
				int tag = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				if (tag != FILE_TAG_LE)
				{
					throw new TJSException(Error.ByteCodeBroken);
				}
				//int objsize = (buff[offset]&0xff) | (buff[offset+1]&0xff) << 8 | (buff[offset+2]&0xff) << 16 | (buff[offset+3]&0xff) << 24;
				offset += 4;
				parent[o] = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int name = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int contextType = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int maxVariableCount = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1
					] & unchecked((int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) <<
					 16 | (buff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int variableReserveCount = (buff[offset] & unchecked((int)(0xff))) | (buff[offset
					 + 1] & unchecked((int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff)
					)) << 16 | (buff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int maxFrameCount = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] &
					 unchecked((int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16
					 | (buff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int funcDeclArgCount = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1
					] & unchecked((int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) <<
					 16 | (buff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int funcDeclUnnamedArgArrayBase = (buff[offset] & unchecked((int)(0xff))) | (buff
					[offset + 1] & unchecked((int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int
					)(0xff))) << 16 | (buff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int funcDeclCollapseBase = (buff[offset] & unchecked((int)(0xff))) | (buff[offset
					 + 1] & unchecked((int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff)
					)) << 16 | (buff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				propSetter[o] = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				propGetter[o] = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				superClassGetter[o] = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1]
					 & unchecked((int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) <<
					 16 | (buff[offset + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
					(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				LongBuffer srcpos;
				// codePos/srcPos は今のところ使ってない、ソート济みなので、longにする必要はないが……
				offset += count << 3;
				srcpos = null;
				count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
					int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				short[] code = new short[count];
				for (int i = 0; i < count; i++)
				{
					code[i] = (short)((buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
						(int)(0xff))) << 8);
					offset += 2;
				}
				offset += (count & 1) << 1;
				count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
					int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int vcount = count << 1;
				if (mVariantTypeData == null || mVariantTypeData.Length < vcount)
				{
					mVariantTypeData = new short[vcount];
				}
				short[] data = mVariantTypeData;
				for (int i_1 = 0; i_1 < vcount; i_1++)
				{
					data[i_1] = (short)((buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] &
						 unchecked((int)(0xff))) << 8);
					offset += 2;
				}
				Variant[] vdata = new Variant[count];
				int datacount = count;
				Variant tmp;
				for (int i_2 = 0; i_2 < datacount; i_2++)
				{
					int pos = i_2 << 1;
					int type = data[pos];
					int index = data[pos + 1];
					switch (type)
					{
						case TYPE_VOID:
						{
							vdata[i_2] = new Variant();
							// null
							break;
						}

						case TYPE_OBJECT:
						{
							vdata[i_2] = new Variant(null, null);
							// null Array Dictionary はまだサポートしていない TODO
							break;
						}

						case TYPE_INTER_OBJECT:
						{
							tmp = new Variant();
							work.AddItem(new ByteCodeLoader.VariantRepalace(tmp, index));
							vdata[i_2] = tmp;
							break;
						}

						case TYPE_INTER_GENERATOR:
						{
							tmp = new Variant();
							work.AddItem(new ByteCodeLoader.VariantRepalace(tmp, index));
							vdata[i_2] = tmp;
							break;
						}

						case TYPE_STRING:
						{
							vdata[i_2] = new Variant(strarray[index]);
							break;
						}

						case TYPE_OCTET:
						{
							vdata[i_2] = new Variant(bbarray[index]);
							break;
						}

						case TYPE_REAL:
						{
							vdata[i_2] = new Variant(dblarray[index]);
							break;
						}

						case TYPE_BYTE:
						{
							vdata[i_2] = new Variant(barray[index]);
							break;
						}

						case TYPE_SHORT:
						{
							vdata[i_2] = new Variant(sarray[index]);
							break;
						}

						case TYPE_INTEGER:
						{
							vdata[i_2] = new Variant(iarray[index]);
							break;
						}

						case TYPE_LONG:
						{
							vdata[i_2] = new Variant(larray[index]);
							break;
						}

						case TYPE_UNKNOWN:
						default:
						{
							vdata[i_2] = new Variant();
							// null;
							break;
							break;
						}
					}
				}
				count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
					int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				int[] scgetterps = new int[count];
				for (int i_3 = 0; i_3 < count; i_3++)
				{
					scgetterps[i_3] = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
						(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
						 + 3] & unchecked((int)(0xff))) << 24;
					offset += 4;
				}
				// properties
				count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
					int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
					 + 3] & unchecked((int)(0xff))) << 24;
				offset += 4;
				if (count > 0)
				{
					int pcount = count << 1;
					int[] props = new int[pcount];
					for (int i_4 = 0; i_4 < pcount; i_4++)
					{
						props[i_4] = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
							(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
							 + 3] & unchecked((int)(0xff))) << 24;
						offset += 4;
					}
					properties[o] = props;
				}
				//IntVector superpointer = IntVector.wrap( scgetterps );
				InterCodeObject obj = new InterCodeObject(block, mStringArray[name], contextType, 
					code, vdata, maxVariableCount, variableReserveCount, maxFrameCount, funcDeclArgCount
					, funcDeclUnnamedArgArrayBase, funcDeclCollapseBase, true, srcpos, scgetterps);
				//objs.add(obj);
				objs[o] = obj;
			}
			Variant val = new Variant();
			for (int o_1 = 0; o_1 < objcount; o_1++)
			{
				InterCodeObject parentObj = null;
				InterCodeObject propSetterObj = null;
				InterCodeObject propGetterObj = null;
				InterCodeObject superClassGetterObj = null;
				if (parent[o_1] >= 0)
				{
					parentObj = objs[parent[o_1]];
				}
				if (propSetter[o_1] >= 0)
				{
					propSetterObj = objs[propSetter[o_1]];
				}
				if (propGetter[o_1] >= 0)
				{
					propGetterObj = objs[propGetter[o_1]];
				}
				if (superClassGetter[o_1] >= 0)
				{
					superClassGetterObj = objs[superClassGetter[o_1]];
				}
				objs[o_1].SetCodeObject(parentObj, propSetterObj, propGetterObj, superClassGetterObj
					);
				if (properties[o_1] != null)
				{
					InterCodeObject obj = parentObj;
					// objs.get(o).mParent;
					int[] prop = properties[o_1];
					int length = (int)(((uint)prop.Length) >> 1);
					for (int i = 0; i < length; i++)
					{
						int pos = i << 1;
						int pname = prop[pos];
						int pobj = prop[pos + 1];
						val.Set(objs[pobj]);
						obj.PropSet(Interface.MEMBERENSURE | Interface.IGNOREPROP, mStringArray[pname], val
							, obj);
					}
					properties[o_1] = null;
				}
			}
			int count_1 = work.Count;
			for (int i_5 = 0; i_5 < count_1; i_5++)
			{
				ByteCodeLoader.VariantRepalace w = work[i_5];
				w.Work.Set(objs[w.Index]);
			}
			work.Clear();
			InterCodeObject top = null;
			if (toplevel >= 0)
			{
				top = objs[toplevel];
			}
			block.SetObjects(top, objs, objcount);
		}

		private void ReadDataArea(byte[] buff, int offset, int size)
		{
			int count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
				(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			if (count > 0)
			{
				if (mByteArray == null || mByteArray.Length < count)
				{
					int c = count < MIN_BYTE_COUNT ? MIN_BYTE_COUNT : count;
					mByteArray = new byte[c];
				}
				System.Array.Copy(buff, offset, mByteArray, 0, count);
				int stride = (int)(((uint)(count + 3)) >> 2);
				offset += stride << 2;
			}
			count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
				int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			if (count > 0)
			{
				// load short
				if (mShortArray == null || mShortArray.Length < count)
				{
					int c = count < MIN_SHORT_COUNT ? MIN_SHORT_COUNT : count;
					mShortArray = new short[c];
				}
				short[] tmp = mShortArray;
				for (int i = 0; i < count; i++)
				{
					tmp[i] = (short)((buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
						(int)(0xff))) << 8);
					offset += 2;
				}
				offset += (count & 1) << 1;
			}
			count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
				int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			if (count > 0)
			{
				if (mIntArray == null || mIntArray.Length < count)
				{
					int c = count < MIN_INT_COUNT ? MIN_INT_COUNT : count;
					mIntArray = new int[c];
				}
				int[] tmp = mIntArray;
				for (int i = 0; i < count; i++)
				{
					tmp[i] = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
						(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
						 + 3] & unchecked((int)(0xff))) << 24;
					offset += 4;
				}
			}
			count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
				int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			if (count > 0)
			{
				// load long
				if (mLongArray == null || mLongArray.Length < count)
				{
					int c = count < MIN_LONG_COUNT ? MIN_LONG_COUNT : count;
					mLongArray = new long[c];
				}
				long[] tmp = mLongArray;
				for (int i = 0; i < count; i++)
				{
					tmp[i] = (long)(buff[offset] & unchecked((int)(0xff))) | (long)(buff[offset + 1] 
						& unchecked((int)(0xff))) << 8 | (long)(buff[offset + 2] & unchecked((int)(0xff)
						)) << 16 | (long)(buff[offset + 3] & unchecked((int)(0xff))) << 24 | (long)(buff
						[offset + 4] & unchecked((int)(0xff))) << 32 | (long)(buff[offset + 5] & unchecked(
						(int)(0xff))) << 40 | (long)(buff[offset + 6] & unchecked((int)(0xff))) << 48 | 
						(long)(buff[offset + 7] & unchecked((int)(0xff))) << 56;
					offset += 8;
				}
			}
			count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
				int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			if (count > 0)
			{
				// load double
				if (mDoubleArray == null || mDoubleArray.Length < count)
				{
					int c = count < MIN_DOUBLE_COUNT ? MIN_DOUBLE_COUNT : count;
					mDoubleArray = new double[c];
				}
				if (mDoubleTmpArray == null || mDoubleTmpArray.Length < count)
				{
					int c = count < MIN_DOUBLE_COUNT ? MIN_DOUBLE_COUNT : count;
					mDoubleTmpArray = new long[c];
				}
				long[] tmp = mDoubleTmpArray;
				for (int i = 0; i < count; i++)
				{
					tmp[i] = (long)(buff[offset] & unchecked((int)(0xff))) | (long)(buff[offset + 1] 
						& unchecked((int)(0xff))) << 8 | (long)(buff[offset + 2] & unchecked((int)(0xff)
						)) << 16 | (long)(buff[offset + 3] & unchecked((int)(0xff))) << 24 | (long)(buff
						[offset + 4] & unchecked((int)(0xff))) << 32 | (long)(buff[offset + 5] & unchecked(
						(int)(0xff))) << 40 | (long)(buff[offset + 6] & unchecked((int)(0xff))) << 48 | 
						(long)(buff[offset + 7] & unchecked((int)(0xff))) << 56;
					offset += 8;
				}
				for (int i_1 = 0; i_1 < count; i_1++)
				{
					mDoubleArray[i_1] = Double.LongBitsToDouble(tmp[i_1]);
				}
			}
			count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
				int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			if (count > 0)
			{
				if (mStringArray == null || mStringArray.Length < count)
				{
					int c = count < MIN_STRING_COUNT ? MIN_STRING_COUNT : count;
					mStringArray = new string[c];
				}
				for (int i = 0; i < count; i++)
				{
					int len = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
						(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
						 + 3] & unchecked((int)(0xff))) << 24;
					offset += 4;
					char[] ch = new char[len];
					for (int j = 0; j < len; j++)
					{
						ch[j] = (char)((buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
							(int)(0xff))) << 8);
						offset += 2;
					}
					mStringArray[i] = TJS.MapGlobalStringMap(new string(ch));
					offset += (len & 1) << 1;
				}
			}
			count = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked((
				int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
				 + 3] & unchecked((int)(0xff))) << 24;
			offset += 4;
			if (count > 0)
			{
				if (mByteBufferArray == null || mByteBufferArray.Length < count)
				{
					mByteBufferArray = new ByteBuffer[count];
				}
				for (int i = 0; i < count; i++)
				{
					int len = (buff[offset] & unchecked((int)(0xff))) | (buff[offset + 1] & unchecked(
						(int)(0xff))) << 8 | (buff[offset + 2] & unchecked((int)(0xff))) << 16 | (buff[offset
						 + 3] & unchecked((int)(0xff))) << 24;
					offset += 4;
					byte[] tmp = new byte[len];
					System.Array.Copy(buff, offset, tmp, 0, len);
					mByteBufferArray[i] = ByteBuffer.Wrap(tmp);
					mByteBufferArray[i].Position(len);
					offset += ((int)(((uint)(len + 3)) >> 2)) << 2;
				}
			}
		}
	}
}
