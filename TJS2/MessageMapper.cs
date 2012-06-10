/*
 * TJS2 CSharp
 */

using System.Collections;
using System.Collections.Generic;
using System.Text;
using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public class MessageMapper
	{
		public class MessageHolder
		{
			internal readonly string mName;

			internal readonly string mDefaultMessage;

			internal string mAssignedMessage;

			public MessageHolder(string name, string defmsg)
			{
				//mAssignedMessage = null;
				mDefaultMessage = defmsg;
				mName = name;
				TJS.RegisterMessageMap(mName, this);
			}

			public MessageHolder(string name, string defmsg, bool regist)
			{
				//mAssignedMessage = null;
				mDefaultMessage = defmsg;
				if (regist)
				{
					mName = name;
					TJS.RegisterMessageMap(mName, this);
				}
				else
				{
					mName = null;
				}
			}

			~MessageHolder()
			{
				if (mName != null)
				{
					TJS.UnregisterMessageMap(mName);
				}
				if (mAssignedMessage != null)
				{
					mAssignedMessage = null;
				}
			}

			public virtual void AssignMessage(string msg)
			{
				if (mAssignedMessage != null)
				{
					mAssignedMessage = null;
				}
				mAssignedMessage = msg;
			}

			public virtual string GetMessage()
			{
				return mAssignedMessage != null ? mAssignedMessage : mDefaultMessage;
			}
		}

		internal Dictionary<string, MessageMapper.MessageHolder> mHash;

		public MessageMapper()
		{
			mHash = new Dictionary<string, MessageMapper.MessageHolder>();
		}

		public virtual void Register(string name, MessageMapper.MessageHolder holder)
		{
			mHash.Put(name, holder);
		}

		public virtual void Unregister(string name)
		{
			Sharpen.Collections.Remove(mHash, name);
		}

		public virtual bool AssignMessage(string name, string newmsg)
		{
			MessageMapper.MessageHolder holder = mHash.Get(name);
			if (holder != null)
			{
				holder.AssignMessage(newmsg);
				return true;
			}
			return false;
		}

		public virtual string Get(string name)
		{
			MessageMapper.MessageHolder holder = mHash.Get(name);
			if (holder != null)
			{
				return holder.GetMessage();
			}
			return null;
		}

		public virtual string CreateMessageMapString()
		{
			StringBuilder script = new StringBuilder();
			ICollection ite = mHash.EntrySet();
			for (Iterator i = ite.Iterator(); i.HasNext(); )
			{
				DictionaryEntry entry = (DictionaryEntry)i.Next();
				string name = (string)entry.Key;
				MessageMapper.MessageHolder h = (MessageMapper.MessageHolder)entry.Value;
				script.Append("\tr(\"");
				script.Append(LexBase.EscapeC(name));
				script.Append("\", \"");
				script.Append(LexBase.EscapeC(h.GetMessage()));
				script.Append("\");\n");
			}
			return script.ToString();
		}
	}
}
