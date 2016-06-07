using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.CommonType
{
    public static class ConvertUtil
    {
        public static async Task<T> ConvertStreamToObject<T>(this Stream stream, Func<string,T> deserializer)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (deserializer == null) throw new ArgumentNullException(nameof(deserializer));

            using (var sr = new StreamReader(stream))
            {
                var json = await sr.ReadToEndAsync();
                return deserializer(json);
            }
        }
    }
}
