using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    public class Database : IDisposable
    {
        private static Database instance;
        public static Database Instance()
        {
            if (instance != null)
                return instance;

            Database db = new Database();
            instance = db;

            return instance;
        }

        public List<Block> Blocks { get; set; } = new List<Block>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        private Database() { }

        public void Dispose()
        {
            
        }
    }
}
