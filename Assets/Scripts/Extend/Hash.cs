using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace HashTool
{
    public static class Hash
    {
        public static Hashtable hashtable = new Hashtable();
        public static bool GetHandleBool(object obj, string key)
        {
            Tuple<object, string> pair = Tuple.Create(obj, key);
            if (!hashtable.ContainsKey(pair))
                return false;

            return (hashtable[pair] as bool?) ?? false;
        }

        public static void SetHandleBool(object obj, string key, bool value)
        {
            Tuple<object, string> pair = Tuple.Create(obj, key);
            hashtable[pair] = value;
        }
    }
}