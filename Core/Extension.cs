using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    static class Extension
    {
        public static int ToInt<T>(this T obj, int def = 0)
        {
            return obj.ToIntWithNull() ?? def;
        }

        public static int? ToIntWithNull<T>(this T obj)
        {
            try { return obj.ToIntWithEx(); } catch { return null; }
        }

        public static int ToIntWithEx<T>(this T obj)
        {
            return Convert.ToInt32(obj);
        }


        public static long ToLong<T>(this T obj, long def = 0)
        {
            return obj.ToLongWithNull() ?? def;
        }

        public static long? ToLongWithNull<T>(this T obj)
        {
            try { return obj.ToLongWithEx(); } catch { return null; }
        }

        public static long ToLongWithEx<T>(this T obj)
        {
            return Convert.ToInt64(obj);
        }
    }
}
