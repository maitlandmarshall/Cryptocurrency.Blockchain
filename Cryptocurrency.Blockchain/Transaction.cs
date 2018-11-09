using Cryptocurrency.Blockchain.Factories;
using Cryptocurrency.Cryptography;
using Cryptocurrency.Cryptography.Services;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    public class Transaction : BlockchainObject
    {
        private string hash;
        public string Hash
        {
            get
            {
                if (!String.IsNullOrEmpty(this.hash))
                    return this.hash;

                return this.ComputeHash();
            }
            set
            {
                if (value != this.ComputeHash())
                    throw new Exception();

                this.hash = value;
            }
        }

        internal string signature { get; set; }

        public List<TransactionInput> Inputs { get; set; } = new List<TransactionInput>();
        public List<TransactionOutput> Outputs { get; set; } = new List<TransactionOutput>();

        public void Sign(PrivateKey privateKey)
        {
            this.signature = Convert.ToBase64String(Sha256EcdsaSignerService.SignData(privateKey, this.Hash));
        }

        private bool VerifySignature(PublicKey publicKey)
        {
            return Sha256EcdsaSignerService.VerifySignature(publicKey, this.signature, this.Hash);
        }

        internal string ComputeHash()
        {
            List<byte> allBytes = new List<byte>();

            foreach (TransactionInput txIn in this.Inputs)
            {
                allBytes.AddRange(ChainParams.Encoder.GetBytes(txIn.PreviousTransactionHash));
            }

            foreach (TransactionOutput txOut in this.Outputs)
            {
                allBytes.AddRange((byte[])txOut.Amount);
                allBytes.AddRange(ChainParams.Encoder.GetBytes(Sha256Hash.Hash(txOut.To)));
            }

            return Sha256Hash.Hash(allBytes.ToArray());
        }

        public void ValidateTransaction()
        {
            bool isCoinbase = this.Inputs.Count == 0;

            if (!isCoinbase && String.IsNullOrEmpty(this.signature))
                throw new Exception("Transaction is not signed");

            decimal
                totalInput = 0,
                totalOutput = 0;

            // verify the inputs are able to be spent with the public key this transaction is signed with
            for (int i = 0; i < this.Inputs.Count; i++)
            {
                TransactionInput txIn = this.Inputs[i];

                Transaction lastTxInTrans = TransactionFactory.TransactionFromTxInput(txIn);
                TransactionOutput lastTxInOutput = lastTxInTrans.Outputs.ElementAt((int)txIn.PreviousTransactionOutIndex);

                if (!this.VerifySignature(lastTxInOutput.To))
                    throw new Exception("Transaction sign chain is invalid");

                lastTxInTrans.ValidateTransaction();

                totalInput += lastTxInOutput.Amount;
            }

            if (isCoinbase)
            {
                if (this.Inputs.Count != 1 && this.Outputs.Count != 1)
                    throw new Exception("Coinbase transaction must have one input and one output");

                return;
            }

            for (int i = 0; i < this.Outputs.Count; i++)
            {
                TransactionOutput txOut = this.Outputs[i];
                totalOutput += txOut.Amount;
            }

            if (!isCoinbase && totalOutput > totalInput)
                throw new Exception("Output must not exceed input");
        }
    }
}
