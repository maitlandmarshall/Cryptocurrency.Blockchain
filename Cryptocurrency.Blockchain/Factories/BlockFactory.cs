using Cryptocurrency.Cryptography;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Cryptocurrency.Blockchain.Factories
{
    public static class BlockFactory
    {
        public static Block GenerateBlock()
        {
            Database db = Database.Instance();
            Block lastBlock = db.Blocks.LastOrDefault();

            Block newBlock = new Block
            {
                PreviousBlockHash = lastBlock.Hash,
                Date = DateTime.Now,
                DifficultyMask = ChainParams.CalculateDifficultyMask(lastBlock)
            };

            return newBlock;
        }

        public static Block GenerateBlock(PublicKey minedBy)
        {
            Block block = GenerateBlock();

            block.Transactions.Add(new Transaction
            {
                Outputs = new List<TransactionOutput>
                {
                    new TransactionOutput
                    {
                        Amount = ChainParams.CalculateBlockReward(block),
                        To = minedBy
                    }
                }
            });

            return block;
        }

        public static Block GenesisBlock()
        {
            const string GenesisPreMinePublicKey = "BGTlPlXjRDQiIAEGKaANVajGnjSjzopKmjtizE7GJQxgoL7IoEXqBo+/+RuPTMDlBSqPkCMgSK/pIM3ceUGWsFw=";

            Block genesis = new Block
            {
                Date = DateTime.ParseExact("11/01/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Nonce = 4432,
                DifficultyMask = ChainParams.CalculateDifficultyMask(null),
                PreviousBlockHash = ChainParams.EmptyBlockHash
            };

            genesis.Transactions.Add(new Transaction
            {
                Outputs = new List<TransactionOutput>
                {
                    new TransactionOutput { To = (PublicKey)Convert.FromBase64String(GenesisPreMinePublicKey), Amount = (decimal)(ChainParams.MaxSupply * ChainParams.PreMinePercentage) }
                }
            });

            return genesis;
        }
    }
}
