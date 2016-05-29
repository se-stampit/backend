using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.Logic.Interface
{
    public interface IPushNotifier
    {
        void OnScan(string code, bool valid);
    }
}
