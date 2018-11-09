using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    public class ChainParams
    {
        const int Million = 1000000;

        public const uint MaxSupply = 21 * Million;

        public static uint UnitsInSingleCoin = (uint)Math.Pow(10, 8);
        public const uint BaseBlockReward = 1000;

        public static Encoding Encoder = Encoding.UTF8;
        public const int IdSizeInBytes = 32;

        public const double PreMinePercentage = 0.1;

        public static uint CalculateBlockReward() {
            return BaseBlockReward;
        }

        public static uint CalculateBlockReward(Block block)
        {
            return BaseBlockReward;
        }

        public static string CalculateDifficultyMask(Block block)
        {
            return "00$";
        }

        public static string EmptyBlockHash => BitConverter.ToString(new byte[32]).Replace("-", "").ToLower();
    }
}
