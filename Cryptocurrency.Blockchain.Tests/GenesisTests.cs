using Cryptocurrency.Blockchain.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Blockchain.Tests
{
    [TestClass]
    public class GenesisTests
    {
        [TestMethod]
        public void GenesisBlock()
        {
            Block block = BlockFactory.GenesisBlock();
            Assert.IsNotNull(block);
        }
    }
}
