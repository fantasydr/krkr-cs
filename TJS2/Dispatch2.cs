/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;

namespace Kirikiri.Tjs2
{
	public interface Dispatch2
	{
		// function invocation
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int FuncCall(int flag, string memberName, Variant result, Variant[] param, Dispatch2
			 objThis);

		// function invocation by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int FuncCallByNum(int flag, int num, Variant result, Variant[] param, Dispatch2 objThis
			);

		// property get
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int PropGet(int flag, string memberName, Variant result, Dispatch2 objThis);

		// property get by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int PropGetByNum(int flag, int num, Variant result, Dispatch2 objThis);

		// property set
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int PropSet(int flag, string memberName, Variant param, Dispatch2 objThis);

		// property set by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int PropSetByNum(int flag, int num, Variant param, Dispatch2 objThis);

		// get member count
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int GetCount(IntWrapper result, string memberName, Dispatch2 objThis);

		// get member count by index number ( result is Integer )
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int GetCountByNum(IntWrapper result, int num, Dispatch2 objThis);

		// enumerate members
		//public int enumMembers( int flag, VariantClosure callback, Dispatch2 objThis ) throws VariantException, TJSException;
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int EnumMembers(int flags, EnumMembersCallback callback, Dispatch2 objthis);

		// delete member
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int DeleteMember(int flag, string memberName, Dispatch2 objThis);

		// delete member by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int DeleteMemberByNum(int flag, int num, Dispatch2 objThis);

		// invalidation
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int Invalidate(int flag, string memberName, Dispatch2 objThis);

		// invalidation by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int InvalidateByNum(int flag, int num, Dispatch2 objThis);

		// get validation, returns true or false
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int IsValid(int flag, string memberName, Dispatch2 objThis);

		// get validation by index number, returns true or false
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int IsValidByNum(int flag, int num, Dispatch2 objThis);

		// create new object
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int CreateNew(int flag, string memberName, Holder<Dispatch2> result, Variant[] param
			, Dispatch2 objThis);

		// create new object by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int CreateNewByNum(int flag, int num, Holder<Dispatch2> result, Variant[] param, 
			Dispatch2 objThis);

		// reserved1 not use
		// class instance matching returns false or true
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int IsInstanceOf(int flag, string memberName, string className, Dispatch2 objThis
			);

		// class instance matching by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int IsInstanceOfByNum(int flag, int num, string className, Dispatch2 objThis);

		// operation with member
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int Operation(int flag, string memberName, Variant result, Variant param, Dispatch2
			 objThis);

		// operation with member by index number
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		/// <exception cref="Kirikiri.Tjs2.TJSException"></exception>
		int OperationByNum(int flag, int num, Variant result, Variant param, Dispatch2 objThis
			);

		// support for native instance
		int NativeInstanceSupport(int flag, int classid, Holder<NativeInstance> pointer);

		// support for class instance infomation
		/// <exception cref="Kirikiri.Tjs2.VariantException"></exception>
		int ClassInstanceInfo(int flag, int num, Variant value);

		// special funcsion
		int AddClassInstanveInfo(string name);

		// special funcsion
		NativeInstance GetNativeInstance(int classid);

		// special funcsion
		int SetNativeInstance(int classid, NativeInstance ni);
		// reserved2
		// reserved3
	}
}
