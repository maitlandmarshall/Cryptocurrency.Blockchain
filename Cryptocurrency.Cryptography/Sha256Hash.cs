using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography
{
    public class Sha256Hash
    {
        public static SecureRandom SecureRand = new SecureRandom();

        public static string Hash(byte[] bytes, string key = null)
        {
            HMac hmac = new HMac(new Sha256Digest());

            if (String.IsNullOrEmpty(key))
                hmac.Init(new KeyParameter(Encoding.UTF8.GetBytes(key ?? "")));

            hmac.BlockUpdate(bytes, 0, bytes.Length);

            byte[] comp = new byte[hmac.GetMacSize()];
            hmac.DoFinal(comp, 0);

            return BitConverter.ToString(comp).Replace("-", "").ToLower();            
        }

        public static string Random()
        {
            return Hash(SecureRand.GenerateSeed(256));
        }
    }
}

