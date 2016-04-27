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
        public static async Task<byte[]> GetImageFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                url = Uri.EscapeUriString(url);
                if(!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    throw new ArgumentException($"The given url ({url}) is not well formed", nameof(url));
            }
            try
            {
                var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.Proxy = null;
                using (var httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
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
    }
}
