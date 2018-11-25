using Cryptocurrency.Blockchain;
using Cryptocurrency.Database;
using Cryptocurrency.Database.Factories;

namespace Cryptocurrency
{
    public class Core
    {
        public Block GenesisBlock { get; private set; }
        
        public Core()
        {
            this.InstantiateBlockchain();
        }

        private void InstantiateBlockchain()
        {
            this.GenesisBlock = BlockFactory.GenesisBlock();
        }
    }
}
