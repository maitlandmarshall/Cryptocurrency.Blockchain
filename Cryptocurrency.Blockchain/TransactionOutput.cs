using Cryptocurrency.Cryptography;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    [DataContract]
    public class TransactionOutput
    {
        [DataMember(Order = 1)]
        public PublicKey To { get; set; }

        [DataMember(Order = 2)]
        public Unit Amount { get; set; }
    }
}
