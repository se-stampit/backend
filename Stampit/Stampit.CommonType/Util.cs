using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.CommonType
{
    public static class Util
    {
        public static IDictionary<TKey,TValue> ToDictionaryFromKeyValuePair<TKey, TValue>(this IEnumerable<KeyValuePair<TKey,TValue>> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IDictionary<TKey, TValue> result = new Dictionary<TKey,TValue>();
            foreach(var entry in source) result.Add(entry);

            return result;
        }
    }
}
