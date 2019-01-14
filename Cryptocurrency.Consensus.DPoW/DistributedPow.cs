using System;

namespace Cryptocurrency.Consensus.DPoW
{
    public class DistributedPow
    {
        /*

         The concept is each node in the network accepts ownership of a parcel (like pass the parcel) one at a time and they have to unravel one layer
         In order to unravel a layer, it must do a quick and easy PoW puzzle. Once it solves the puzzle, it broadcasts the new parcel to the network for the next node
         randomly, a node will accept a parcel and unwrap it completely. Thus mining a block.
         The node that completely unwraps a parcel determines how many layers the next parcel has as part of the "mining difficulty"

         Problems:
         How to pass the parcel without storing it on the blockchain?
             Let's say the last node to mine a block or unwrap a layer of the parcel goes offline before it broadcasts it to the next node
                 A: The node which sent the parcel waits for a confirmation reply that can only occur after the parcel has been actioned by the next node.
                 If the confirmation never comes back, the original sending node tries another node

         How to make it so the concensus algorithm doesn't require an "entry point"
             A: Similar to how bitcoin works, it's a race to see which parcel can get unravelled first. All nodes are submitting parcels to each other,
             once a node receives a parcel which has been correctly signed by enough nodes, it broadcasts that parcel and the new block to the network
             and then the parcel & new block is validated by all the other nodes.
             Then all nodes start submitting a parcel for the next block

         How to ensure the parcel is not illegally changed as it transfers from node to node?
            Let's say a malicious node wants to change the amount of signatures on a parcel to automatically make themselves win, how to prevent that?
                A: Each step a parcel makes through a node, the node signs the parcel with its private key and its index in the pass the parcel chain.
                Each node in the chain must be unique (can't just bounce between the same two nodes)
            
            What if a malicious actor sets up 100 modified nodes and bounces parcels between them? Or modifies the parcel to show it has signatures
            from nodes that it has generated itself?
                Q: This is why PoW works so well, it doesn't rely on external validation. Once it finds the solution it just broadcasts it and everyone else can validate it
                A: 

        */

        public DpowParcel AcceptWork(DpowParcel parcel)
        {

        }
    }
}
