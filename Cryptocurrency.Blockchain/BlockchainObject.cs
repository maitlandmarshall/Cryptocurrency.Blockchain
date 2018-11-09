using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    public abstract class BlockchainObject
    {
        protected static SecureRandom SecureRand = new SecureRandom();

        public byte[] Id { get; private set; }

        public BlockchainObject()
        {
            this.Id = SecureRand.GenerateSeed(ChainParams.IdSizeInBytes);
        }
    }
}
