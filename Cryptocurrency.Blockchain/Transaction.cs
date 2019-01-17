using Cryptocurrency.Cryptography;
using Cryptocurrency.Cryptography.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cryptocurrency.Blockchain
{
    [DataContract]
    public class Transaction : BlockchainObject
    {
        private string hash;

        [DataMember(Order = 1)]
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
                this.hash = value;
            }
        }

        [DataMember(Order = 2)]
        public string Signature { get; set; }

        [DataMember(Order = 3)]
        public List<TransactionInput> Inputs { get; set; } = new List<TransactionInput>();

        [DataMember(Order = 4)]
        public List<TransactionOutput> Outputs { get; set; } = new List<TransactionOutput>();

        public void Sign(PrivateKey privateKey)
        {
            this.Signature = Convert.ToBase64String(Sha256EcdsaSignerService.SignData(privateKey, this.Hash));
        }

        public bool VerifySignature(PublicKey publicKey)
        {
            return Sha256EcdsaSignerService.VerifySignature(publicKey, this.Signature, this.Hash);
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
    }
}
