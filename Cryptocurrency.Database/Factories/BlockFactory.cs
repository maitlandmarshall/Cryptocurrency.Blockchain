using Cryptocurrency.Blockchain;
using Cryptocurrency.Cryptography;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Cryptocurrency.Database.Factories
{
    public static class BlockFactory
    {
        public static Block GenerateBlock()
        {
            using (BlockchainDatabase db = BlockchainDatabase.Create())
            {
                uint lastBlockIndex = (uint)db.Blocks.Max(y => y.BlockIndex).AsInt32;
                Block lastBlock = db.Blocks.FindOne(y => y.BlockIndex == lastBlockIndex);

                Block newBlock = new Block
                {
                    PreviousBlockHash = lastBlock.Hash,
                    Date = DateTime.Now,
                    DifficultyMask = ChainParams.CalculateDifficultyMask(lastBlock),
                    BlockIndex = lastBlockIndex + 1
                };

                return newBlock;
            }
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
            using (BlockchainDatabase db = BlockchainDatabase.Create())
            {
                Block genesisBlock = db.Blocks.FindOne(y => y.PreviousBlockHash == ChainParams.EmptyBlockHash);

                if (genesisBlock != null)
                    return genesisBlock;

                genesisBlock = new Block
                {
                    Date = DateTime.ParseExact("11/01/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Nonce = 4432,
                    DifficultyMask = ChainParams.CalculateDifficultyMask(null),
                    PreviousBlockHash = ChainParams.EmptyBlockHash
                };

                genesisBlock.Transactions.Add(new Transaction
                {
                    Outputs = new List<TransactionOutput>
                    {
                        new TransactionOutput { To = Globals.Keys.GenesisPublicKey, Amount = (decimal)(ChainParams.MaxSupply * ChainParams.PreMinePercentage) }
                    }
                });

                db.Blocks.Insert(genesisBlock);

                return genesisBlock;
            }
        }
    }
}
