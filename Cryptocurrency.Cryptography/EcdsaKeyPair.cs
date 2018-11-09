using Cryptocurrency.Cryptography.Factories;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography
{
    public class EcdsaKeyPair
    {
        public static ECDomainParameters Domain = SignerFactory.Sha256EcdsaDomain;
        private static SecureRandom SecureRand = new SecureRandom();

        public PrivateKey Private { get; private set; }
        public PublicKey Public { get; private set; }

        public EcdsaKeyPair()
        {
            ECKeyPairGenerator generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(new ECKeyGenerationParameters(Domain, SecureRand));

            AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();
            ECPrivateKeyParameters priv = keyPair.Private as ECPrivateKeyParameters;
            ECPublicKeyParameters pub = keyPair.Public as ECPublicKeyParameters;

            this.Private = priv.D.ToByteArray();
            this.Public = pub.Q.GetEncoded();
        }

        public EcdsaKeyPair(PrivateKey pk)
        {
            this.Private = pk;
            this.Public = KeyFactory.ECPublicKeyParamsFromPrivateKey(pk, Domain).Q.GetEncoded();
        }
    }
}
