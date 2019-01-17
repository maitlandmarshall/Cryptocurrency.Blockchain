using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cryptocurrency.Blockchain
{
    [DataContract]
    public class Unit
    {
        [DataMember(Order = 1)]
        private byte[] ValueBytes
        {
            get
            {
                return this.Value?.ToByteArray();
            }
            set
            {
                this.Value = new BigInteger(value);
            }
        }

        public BigInteger Value { get; set; }

        public static implicit operator byte[] (Unit unit)
        {
            return unit.Value.ToByteArray();
        }

        public static implicit operator decimal (Unit unit)
        {
            decimal valueAsDecimal = unit.Value.LongValue;
            return valueAsDecimal / ChainParams.UnitsInSingleCoin;
        }

        public static implicit operator Unit (decimal val)
        {
            return new Unit
            {
                Value = new BigInteger((val * ChainParams.UnitsInSingleCoin).ToString())
            };
        }
    }
}
