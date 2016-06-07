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

        public static string ToBase64(this byte[] source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return Convert.ToBase64String(source);
        }
    }
}
