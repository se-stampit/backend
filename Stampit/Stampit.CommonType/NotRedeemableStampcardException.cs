using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Stampit.CommonType
{
    public class NotRedeemableStampcardException : Exception
    {
        public NotRedeemableStampcardException()
        {
        }

        public NotRedeemableStampcardException(string code) : base($"The code ({code}) is valid but the stampcard of the user is not full and theirfore not redeemable")
        {
        }

        public NotRedeemableStampcardException(string code, Exception innerException) : base($"The code ({code}) is valid but the stampcard of the user is not full and theirfore not redeemable", innerException)
        {
        }

        protected NotRedeemableStampcardException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
