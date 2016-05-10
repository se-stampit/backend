using Stampit.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic
{
    public class GoogleQrCodeGenerator : IQrCodeGenerator
    {
        public string GetQrCodeUrl(string content, int width = 300, int height = 300)
        {
            return $"http://chart.apis.google.com/chart?cht=qr&chs={width}x{height}&chl={content}&chld=H|0";
        }
    }
}
