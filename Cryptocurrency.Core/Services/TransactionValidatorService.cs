using Cryptocurrency.Blockchain;
using Cryptocurrency.Database.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptocurrency.Services
{
    public static class TransactionValidatorService
    {
        public static void ValidateTransaction(Transaction transaction)
        {
            bool isCoinbase = transaction.Inputs.Count == 0;

            decimal
                totalInput = 0,
                totalOutput = 0;

            // verify the inputs are able to be spent with the public key the transaction is signed with
            for (int i = 0; i < transaction.Inputs.Count; i++)
            {
                TransactionInput txIn = transaction.Inputs[i];

                Transaction lastTxInTrans = TransactionFactory.TransactionFromTxInput(txIn);
                TransactionOutput lastTxInOutput = lastTxInTrans.Outputs.ElementAt((int)txIn.PreviousTransactionOutIndex);

                if (!transaction.VerifySignature(lastTxInOutput.To))
                    throw new Exception("Transaction sign chain is invalid");

                TransactionValidatorService.ValidateTransaction(lastTxInTrans);

                totalInput += lastTxInOutput.Amount;
            }

            if (isCoinbase)
            {
                if (transaction.Inputs.Count != 1 && transaction.Outputs.Count != 1)
                    throw new Exception("Coinbase transaction must have one input and one output");

                return;
            }

            for (int i = 0; i < transaction.Outputs.Count; i++)
            {
                TransactionOutput txOut = transaction.Outputs[i];
                totalOutput += txOut.Amount;
            }

            if (!isCoinbase && totalOutput > totalInput)
                throw new Exception("Output must not exceed input");
        }
    }
}
