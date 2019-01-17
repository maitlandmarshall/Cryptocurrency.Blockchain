using Cryptocurrency.Cryptography;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Cryptocurrency.Blockchain
{
    [DataContract]
    public class Block : BlockchainObject
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
            set => this.hash = value;
        }

        [DataMember(Order = 2)]
        public string PreviousBlockHash { get; set; }

        [DataMember(Order = 3)]
        public UInt16 Nonce { get; set; }

        [DataMember(Order = 4)]
        public string DifficultyMask { get; set; }

        [DataMember(Order = 5)]
        public DateTime Date { get; set; }

        [DataMember(Order = 6)]
        public List<Transaction> Transactions { get; set; }

        [DataMember(Order = 7)]
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
