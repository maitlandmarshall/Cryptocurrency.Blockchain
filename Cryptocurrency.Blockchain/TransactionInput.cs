using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    [DataContract]
    public class TransactionInput
    {
        [DataMember(Order = 1)]
        public string PreviousTransactionHash { get; set; }

        [DataMember(Order = 2)]
        public uint PreviousTransactionOutIndex { get; set; }
    }
}
