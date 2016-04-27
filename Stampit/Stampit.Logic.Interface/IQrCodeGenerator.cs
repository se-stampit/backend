using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    /// <summary>
    /// Defines an interface to generate QR codes based on a given content
    /// </summary>
    public interface IQrCodeGenerator
    {
        /// <summary>
        /// Returns a valid url directing to an image containing the specified qrcode with the given content
        /// </summary>
        /// <param name="content">A string representation of the content, the qr code should contain</param>
        /// <param name="width">The width of the requested qrcode, should be the same as the param height to be square formed</param>
        /// <param name="height">The height of the requested qrcode, should be the same as the param width to be square formed</param>
        /// <returns>The url to request the byte array containing the image</returns>
        string GetQrCodeUrl(string content, int width = 300, int height = 300);
    }
}
