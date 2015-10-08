// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;

namespace DHT.MainModule
{
    public class ReverseIndexCore:SetKadCore<string,FileId>
    {
        public ReverseIndexCore(RoutingTable<string> routingTable, Func<NodeIdentifier<string>, ISetKadNode<string, FileId>> createClientFromNodeIdDelegate) : base(routingTable, createClientFromNodeIdDelegate)
        {
        }

        public ReverseIndexCore(RoutingTable<string> routingTable, KademeliaSettings settings, Func<NodeIdentifier<string>, ISetKadNode<string, FileId>> createClientFromNodeIdDelegate, NodeIdentifier<string> nodeIdentifier) : base(routingTable, settings, createClientFromNodeIdDelegate, nodeIdentifier)
        {
        }

        protected override Type GetContractType
        {
            get { return typeof (IReverseIndexServiceContract); }
        }
    }
}