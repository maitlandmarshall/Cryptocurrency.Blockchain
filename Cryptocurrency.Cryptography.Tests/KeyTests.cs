using Cryptocurrency.Cryptography.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography.Tests
{
    [TestClass]
    public class KeyTests
    {
        [TestMethod]
        public void NewKey()
        {
            EcdsaKeyPair keyPair = new EcdsaKeyPair();

            Assert.AreNotEqual(new byte[] { }, keyPair.Private);
            Assert.IsTrue(!String.IsNullOrEmpty(keyPair.Private.ToString()));

            string publicKey = keyPair.Public.ToString();
            string publicKeyFromFactoryMethod = ((PublicKey)KeyFactory.ECPublicKeyParamsFromPublicKey(keyPair.Public, EcdsaKeyPair.Domain).Q.GetEncoded()).ToString();

            Assert.AreEqual(publicKey, publicKeyFromFactoryMethod);

            string privateKey = keyPair.Private.ToString();
            string privateKeyFromFactoryMethod = ((PrivateKey)KeyFactory.ECPrivateKeyParamsFromPrivateKey(keyPair.Private, EcdsaKeyPair.Domain).D.ToByteArray()).ToString();

            Assert.AreEqual(privateKey, privateKeyFromFactoryMethod);
        }

        [TestMethod]
        public void RestoreKey()
        {
            EcdsaKeyPair keyPair = new EcdsaKeyPair();

            string pubKey = keyPair.Public.ToString();
            PrivateKey privKey = keyPair.Private;

            keyPair = new EcdsaKeyPair(privKey);

            Assert.AreEqual(pubKey, keyPair.Public.ToString());
            Assert.AreEqual(privKey, keyPair.Private);
        }

        [TestMethod]
        public void RestoreGenesisKey()
        {
            EcdsaKeyPair genesis = new EcdsaKeyPair(Globals.Keys.GenesisPrivateKey);

            Assert.AreEqual(genesis.Private.ToString(), Globals.Keys.GenesisPrivateKey);
            Assert.AreEqual(genesis.Public.ToString(), Globals.Keys.GenesisPublicKey);
        }



    }
}
