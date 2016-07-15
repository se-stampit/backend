using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.CommonType
{
    public static class ImageUtil
    {
        /// <summary>
        /// Returns an image as bytes from a given filepath
        /// </summary>
        /// <param name="filePath">The filepath to the image</param>
        /// <returns>The transformed bytes of the image file</returns>
        public static Task<byte[]> GetImageFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new ArgumentException($"The given file ({filePath}) does not exist", nameof(filePath));
            
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return GetImageFromStream(fs);
            }
        }

        /// <summary>
        /// Returns the bytes of an image or other multimedia data of a given stream
        /// </summary>
        /// <param name="stream">The stream which contains the bytes</param>
        /// <returns>The bytes of the content of the stream</returns>
        public static async Task<byte[]> GetImageFromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Returns an image of the given url
        /// </summary>
        public static async Task<byte[]> GetImageFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                url = Uri.EscapeUriString(url);
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    throw new ArgumentException($"The given url ({url}) is not well formed", nameof(url));
            }
            try
            {
                var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.Proxy = null;
                using (var httpWebReponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
                {
                    using (var imgStream = httpWebReponse.GetResponseStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            await imgStream.CopyToAsync(ms);
                            return ms.ToArray();
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a byte array to base64
        /// </summary>
        /// <param name="source">The bytes to be converted</param>
        /// <returns>The base64 representation</returns>
        public static string ToBase64(this byte[] source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return Convert.ToBase64String(source);
        }
    }
}
