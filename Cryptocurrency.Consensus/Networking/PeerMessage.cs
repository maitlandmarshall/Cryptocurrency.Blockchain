using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Consensus.Networking
{
    [ProtoContract]
    public class PeerMessage
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string Message { get; set; }

        [ProtoMember(3)]
        public byte[] Data { get; set; }

        public Stream DataStream
        {
            get => new MemoryStream(this.Data, 0, this.Data.Length);
        }

        public PeerMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
