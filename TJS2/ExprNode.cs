/*
 * The TJS2 interpreter from kirikirij
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class ExprNode
	{
		private int mOp;

		private int mPosition;

		private AList<Kirikiri.Tjs2.ExprNode> mNodes;

		private Variant mVal;

		public ExprNode()
		{
			//mOp = 0;
			//mNodes = null;
			//mVal = null;
			mPosition = -1;
		}

		~ExprNode()
		{
			if (mNodes != null)
			{
				int count = mNodes.Count;
				for (int i = 0; i < count; i++)
				{
					Kirikiri.Tjs2.ExprNode node = mNodes[i];
					if (node != null)
					{
						node.Clear();
					}
				}
				mNodes.Clear();
				mNodes = null;
			}
			if (mVal != null)
			{
				mVal.Clear();
				mVal = null;
			}
		}

		public void SetOpecode(int op)
		{
			mOp = op;
		}

		public void SetPosition(int pos)
		{
			mPosition = pos;
		}

		public void SetValue(Variant val)
		{
			if (mVal == null)
			{
				mVal = new Variant(val);
			}
			else
			{
				mVal.CopyRef(val);
			}
		}

		public void Add(Kirikiri.Tjs2.ExprNode node)
		{
			if (mNodes == null)
			{
				mNodes = new AList<Kirikiri.Tjs2.ExprNode>();
			}
			mNodes.AddItem(node);
		}

		public int GetOpecode()
		{
			return mOp;
		}

		public int GetPosition()
		{
			return mPosition;
		}

		public Variant GetValue()
		{
			return mVal;
		}

		public Kirikiri.Tjs2.ExprNode GetNode(int index)
		{
			if (mNodes == null)
			{
				return null;
			}
			else
			{
				if (index < mNodes.Count)
				{
					return mNodes[index];
				}
				else
				{
					return null;
				}
			}
		}

		public int GetSize()
		{
			if (mNodes == null)
			{
				return 0;
			}
			else
			{
				return mNodes.Count;
			}
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public void AddArrayElement(Variant val)
		{
			string ss_add = "add";
			Variant[] args = new Variant[1];
			args[0] = val;
			mVal.AsObjectClosure().FuncCall(0, ss_add, null, args, null);
		}

		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		public void AddDictionaryElement(string name, Variant val)
		{
			mVal.AsObjectClosure().PropSet(Interface.MEMBERENSURE, name, val, null);
		}

		public void Clear()
		{
			if (mNodes != null)
			{
				int count = mNodes.Count;
				for (int i = 0; i < count; i++)
				{
					Kirikiri.Tjs2.ExprNode node = mNodes[i];
					if (node != null)
					{
						node.Clear();
					}
				}
				mNodes.Clear();
				mNodes = null;
			}
			if (mVal != null)
			{
				mVal.Clear();
				mVal = null;
			}
		}
	}
}
