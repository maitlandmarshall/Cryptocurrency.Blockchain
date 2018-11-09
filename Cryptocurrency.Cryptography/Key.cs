using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Cryptography
{
    public interface IKey
    {
        byte[] Data { get; }
    }

    public struct PublicKey : IKey
    {
        public byte[] Data { get; internal set; }

        public static implicit operator PublicKey (byte[] bytes)
        {
            return new PublicKey { Data = bytes };
        }

        public static implicit operator byte[] (PublicKey key)
        {
            return key.Data;
        }

        public override string ToString()
        {
            return Convert.ToBase64String(this);
        }
    }

    public struct PrivateKey : IKey
    {
        public byte[] Data { get; internal set; }

        public static implicit operator PrivateKey (byte[] bytes)
        {
            return new PrivateKey { Data = bytes };
        }

        public static implicit operator byte[] (PrivateKey key)
        {
            return key.Data;
        }

        public override string ToString()
        {
            return Convert.ToBase64String(this);
        }
    }
}
