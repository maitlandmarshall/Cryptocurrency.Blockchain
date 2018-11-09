using Cryptocurrency.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    public class TransactionOutput
    {
        public PublicKey To { get; set; }
        public Unit Amount { get; set; }
    }
}
