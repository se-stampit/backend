using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.CommonType
{
    public sealed class Tupel<T,U>
    {
        public Tupel(T arg1, U arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }

        public T Arg1 { get; }
        public U Arg2 { get; }
    }
}
