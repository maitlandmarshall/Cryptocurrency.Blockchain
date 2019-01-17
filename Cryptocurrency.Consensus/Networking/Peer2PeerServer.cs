using Cryptocurrency.Globals;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cryptocurrency.Consensus.Networking
{
    public class Peer2PeerServer
    {
        internal TcpListener Server { get; private set; }
        internal List<Peer> Peers { get; private set; }

        private CancellationTokenSource cancellationToken;

        public Peer2PeerServer()
        {
            this.Server = new TcpListener(IPAddress.Any, Keys.DefaultPort);
            this.Peers = new List<Peer>();
        }

        public void Start()
        {
            this.Server.Start();
            this.ServerLoopFactory();
        }

        private void ServerLoopFactory()
        {
            this.cancellationToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (!this.cancellationToken.IsCancellationRequested)
                {
                    Peer peer = new Peer(this.Server.AcceptTcpClient());
                    this.Peers.Add(peer);
                }
            });            
        }


        private void PeerDiscoveryHandler()
        {

        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

    }
}
