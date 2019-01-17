using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cryptocurrency.Cryptography
{
    public interface IKey
    {
        byte[] Data { get; }
    }

    [DataContract]
    public struct PublicKey : IKey
    {
        [DataMember(Order = 1)]
        public byte[] Data { get; internal set; }

        public static implicit operator PublicKey (byte[] bytes)
        {
            return new PublicKey { Data = bytes };
        }

        public static implicit operator byte[] (PublicKey key)
        {
            return key.Data;
        }

        public static implicit operator PublicKey (string key)
        {
            byte[] data = Base58.FromBase58String(key);
            return new PublicKey { Data = data };
        }

        public override string ToString()
        {
            return Base58.ToBase58String(this);
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

        public static implicit operator PrivateKey (string key)
        {
            byte[] data = Base58.FromBase58String(key);
            return new PrivateKey { Data = data };
        }

        public override string ToString()
        {
            return Base58.ToBase58String(this);
        }
    }
}
