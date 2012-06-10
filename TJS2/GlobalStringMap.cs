/*
 * TJS2 CSharp
 */

using Kirikiri.Tjs2;
using Sharpen;
using System.Collections.Generic;

namespace Kirikiri.Tjs2
{
	internal class GlobalStringMap
	{
		//private HashCache<string, string> mMap;
        private Dictionary<string, string> mMap;

		private const int GLOBAL_STRING_MAP_SIZE = 5000;

		public GlobalStringMap()
		{
			//private HashMap<String,String> mMap;
			//mMap = new HashMap<String,String>(GLOBAL_STRING_MAP_SIZE);
			//mMap = new HashCache<string, string>(GLOBAL_STRING_MAP_SIZE);
            mMap = new Dictionary<string, string>();
		}

		public virtual string Map(string str)
		{
			//String ret = mMap.get( str );
            string ret = null;
			if (mMap.TryGetValue(str, out ret))
			{
				return ret;
			}
			mMap.Put(str, str);
			return mMap.Get(str);
		}
	}
}
