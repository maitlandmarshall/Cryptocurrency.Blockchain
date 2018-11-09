using Cryptocurrency.Cryptography.Factories;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography.Services
{
    public static class Sha256EcdsaSignerService
    {
        public static byte[] SignData(PrivateKey privateKey, string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return SignData(privateKey, dataBytes);
        }

        public static byte[] SignData(PrivateKey pk, byte[] data)
        {
            ECPrivateKeyParameters privateKeyParams = KeyFactory.ECPrivateKeyParamsFromPrivateKey(pk, SignerFactory.Sha256EcdsaDomain);

            ISigner signer = SignerFactory.Sha256withECDSA();
            signer.Init(true, privateKeyParams);
            signer.BlockUpdate(data, 0, data.Length);

            byte[] signBytes = signer.GenerateSignature();

            return signBytes;
        }

        public static bool VerifySignature(PublicKey publicKey, string signature, string msg)
        {
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
            byte[] sigBytes = Convert.FromBase64String(signature);

            ISigner signer = SignerFactory.Sha256withECDSA();
            signer.Init(false, KeyFactory.ECPublicKeyParamsFromPublicKey(publicKey, SignerFactory.Sha256EcdsaDomain));
            signer.BlockUpdate(msgBytes, 0, msgBytes.Length);

            return signer.VerifySignature(sigBytes);
        }
    }
}
