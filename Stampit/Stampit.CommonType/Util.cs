using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.CommonType
{
    public static class Util
    {
        public static IDictionary<TKey,TValue> ToDictionaryFromKeyValuePair<TKey, TValue>(this List<Tupel<TKey,TValue>> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IDictionary<TKey, TValue> result = new Dictionary<TKey,TValue>();
            foreach(var entry in source) result.Add(new KeyValuePair<TKey,TValue>(entry.Arg1, entry.Arg2));

            return result;
        }

        public static IEnumerable<KeyValuePair<int,T>> IndexSequence<T>(this IEnumerable<T> source, int start, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return from idx in Enumerable.Range(start, count)
                   select new KeyValuePair<int,T>(idx, source.ElementAt(idx));
        }
    }
}
