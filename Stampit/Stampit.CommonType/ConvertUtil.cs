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
        /// <summary>
        /// Converts a stream as extension with a given deserializer to a object representation of the stream content
        /// </summary>
        /// <typeparam name="T">The Type of object that is requested to be converted in</typeparam>
        /// <param name="deserializer">The concret serializer to transform the stream content to an object</param>
        /// <returns>The serialized stream content</returns>
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
