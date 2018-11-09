using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptocurrency.Blockchain.Factories
{
    public static class TransactionFactory
    {
        public static Transaction TransactionFromTxInput (TransactionInput txInput)
        {
            Database db = Database.Instance();

            string previousTx = txInput.PreviousTransactionHash;
            return db.Transactions.FirstOrDefault(y => y.Hash == previousTx);
        }
    }
}
