using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.CommonType
{
    public class IllegalCodeException : Exception
    {
        public IllegalCodeException()
        {
        }

        public IllegalCodeException(string code) : base($"The given code {code} does not exist in the storage and is therefore invalid")
        {
        }

        public IllegalCodeException(string code, Exception innerException) : base($"The given code {code} does not exist in the storage and is therefore invalid", innerException)
        {
        }

        protected IllegalCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
