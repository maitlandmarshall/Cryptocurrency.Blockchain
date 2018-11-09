using Cryptocurrency.Cryptography.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography.Tests
{
    [TestClass]
    public class SigningTests
    {
        [TestMethod]
        public void SignAndVerify()
        {
            EcdsaKeyPair kp = new EcdsaKeyPair();
            EcdsaKeyPair kp2 = new EcdsaKeyPair();

            string dataToSign = "helloDawg";
            string signedData = Convert.ToBase64String(Sha256EcdsaSignerService.SignData(kp.Private, dataToSign));

            Assert.IsTrue(Sha256EcdsaSignerService.VerifySignature(kp.Public, signedData, dataToSign));
            Assert.IsFalse(Sha256EcdsaSignerService.VerifySignature(kp2.Public, signedData, dataToSign));
        }
    }
}
