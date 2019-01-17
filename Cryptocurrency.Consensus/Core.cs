using Cryptocurrency.Consensus.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptocurrency.Consensus
{
    public class Core
    {
        internal Peer2PeerServer P2PServer { get; private set; }

        public Core()
        {
            this.P2PServer = new Peer2PeerServer();
        }

        public void Start()
        {
            this.P2PServer.Start();
        }

        public void Stop()
        {
            this.P2PServer.Stop();

            throw new NotImplementedException();
        }
    }
}
