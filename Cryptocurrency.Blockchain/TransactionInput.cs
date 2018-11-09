using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    public class TransactionInput
    {
        public string PreviousTransactionHash { get; set; }
        public uint PreviousTransactionOutIndex { get; set; }
    }
}
