using Cryptocurrency.Blockchain;
using Cryptocurrency.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Cryptocurrency.Services
{
    public static class BlockValidatorService
    {
        public static void ValidateBlock(Block block)
        {
            // first validate the Nonce matches the difficulty mask
            string mineHash = Sha256Hash.Hash(BitConverter.GetBytes(block.Nonce), block.PreviousBlockHash);

            if (!Regex.IsMatch(mineHash, block.DifficultyMask))
            {
                throw new Exception("Nonce does not match difficulty mask");
            }

            for (int i = 0; i < block.Transactions.Count; i++)
            {
                Transaction t = block.Transactions[i];
                TransactionValidatorService.ValidateTransaction(t);
            }
        }
    }
}
