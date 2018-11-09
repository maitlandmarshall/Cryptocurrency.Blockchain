using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography
{
    public static class SignerFactory
    {
        public static X9ECParameters Sha256Curve => SecNamedCurves.GetByName("secp256k1");

        public static ECDomainParameters Sha256EcdsaDomain
        {
            get
            {
                X9ECParameters curve = Sha256Curve;
                ECDomainParameters Domain = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
                return Domain;
            }
        }

        public static ISigner Sha256withECDSA() => SignerUtilities.GetSigner("SHA-256withECDSA");

    }
}
