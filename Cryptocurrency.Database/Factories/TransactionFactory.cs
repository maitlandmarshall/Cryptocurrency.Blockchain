using Cryptocurrency.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptocurrency.Database.Factories
{
    public static class TransactionFactory
    {
        public static Transaction TransactionFromTxInput (TransactionInput txInput)
        {
            using (BlockchainDatabase db = BlockchainDatabase.Create())
            {
                string previousTx = txInput.PreviousTransactionHash;
                Block blockWithTransaction = db.Blocks.FindOne(y => y.Transactions.Exists(z => z.Hash == previousTx));

                if (blockWithTransaction == null)
                    return null;

                return blockWithTransaction.Transactions.FirstOrDefault(y => y.Hash == previousTx);
            }
        }
    }
}
