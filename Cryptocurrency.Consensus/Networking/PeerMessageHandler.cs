using Cryptocurrency.Blockchain;
using Cryptocurrency.Database.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrency.Consensus.Networking
{
    public partial class Peer
    {
        public void SubmitTransactions(params Transaction[] transactions)
        {
            this.SubmitMessage(
                this.BuildMessage(this.TransactionsSubmitted, transactions)
            );
        }

        private void TransactionsSubmitted(Transaction[] transactions)
        {

        }

        public void SubmitBlock(Block block)
        {
            this.SubmitMessage(
                this.BuildMessage(this.BlockSubmitted, block)
            );
        }

        private void BlockSubmitted(Block block)
        {

        }

        public async Task<Block[]> RequestBlocks(int blockHeight)
        {
            PeerMessage msg = this.BuildMessage(this.BlocksRequested, blockHeight);

            TaskCompletionSource<PeerMessage> task = new TaskCompletionSource<PeerMessage>();
            this.tasks.Add(msg.Id, task);
            this.SubmitMessage(msg);

            PeerMessage result = await task.Task;

            return ProtoBuf.Serializer.Deserialize<Block[]>(result.DataStream);
        }

        private Block[] BlocksRequested(int blockHeight)
        {
            return new Block[] { BlockFactory.GenesisBlock() };
        }
    }
}
