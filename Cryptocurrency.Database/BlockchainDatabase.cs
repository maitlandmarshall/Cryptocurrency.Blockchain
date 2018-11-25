using Cryptocurrency.Blockchain;
using LiteDB;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Database
{
    public class BlockchainDatabase : LiteDB.LiteDatabase
    {
        static BlockchainDatabase()
        {
            BsonMapper.Global.RegisterType<BigInteger>(
                serialize: (bigInt) => bigInt.ToByteArray(),
                deserialize: (bson) => new BigInteger(bson.AsBinary)
            );
        }

        public static BlockchainDatabase Create()
        {
            BlockchainDatabase db = new BlockchainDatabase();
            return db;
        }

        internal BlockchainDatabase() : base("blockchain.db") { }

        public LiteCollection<Block> Blocks
        {
            get => this.GetCollection<Block>(nameof(Block));
        }

        public LiteCollection<Transaction> Transactions
        {
            get => this.GetCollection<Transaction>(nameof(Transaction));
        }
    }
}
