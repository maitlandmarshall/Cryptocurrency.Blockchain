using Cryptocurrency.Cryptography;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Cryptocurrency.Blockchain
{
    public class Block : BlockchainObject
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
            set => this.hash = value;
        }

        public string PreviousBlockHash { get; set; }
        public UInt16 Nonce { get; set; }
        public string DifficultyMask { get; set; }

        public DateTime Date { get; set; }
        public List<Transaction> Transactions { get; set; }

        public uint BlockIndex { get; set; }

        public Block()
        {
            this.Transactions = new List<Transaction>();
        }

        public string ComputeHash()
        {
            List<byte> allBytes = new List<byte>();
            allBytes.AddRange(ChainParams.Encoder.GetBytes(this.PreviousBlockHash));
            
            foreach(Transaction tx in this.Transactions)
            {
                allBytes.AddRange(ChainParams.Encoder.GetBytes(tx.Hash));
            }

            return Sha256Hash.Hash(allBytes.ToArray());
        }
    }
}
