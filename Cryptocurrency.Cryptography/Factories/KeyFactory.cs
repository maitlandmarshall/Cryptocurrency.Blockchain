using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography.Factories
{
    public static class KeyFactory
    {
        public static ECPrivateKeyParameters ECPrivateKeyParamsFromPrivateKey(PrivateKey pk, ECDomainParameters domain)
        {
            BigInteger d = new BigInteger(pk);
            ECPrivateKeyParameters paras = new ECPrivateKeyParameters(d, domain);

            return paras;
        }

        public static ECPublicKeyParameters ECPublicKeyParamsFromPrivateKey(PrivateKey pk, ECDomainParameters domain)
        {
            BigInteger d = new BigInteger(pk);
            ECPoint q = domain.G.Multiply(d);

            return new ECPublicKeyParameters(q, domain);
        }

        public static ECPublicKeyParameters ECPublicKeyParamsFromPublicKey(PublicKey pk, ECDomainParameters domain)
        {
            return new ECPublicKeyParameters(SignerFactory.Sha256Curve.Curve.DecodePoint(pk), domain);
        }
    }
}
