// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;

namespace DHT.MainModule
{
    public class FileToMachineCore:SetKadCore<FileId,FileLocation>
    {
        public FileToMachineCore(RoutingTable<FileId> routingTable, Func<NodeIdentifier<FileId>, ISetKadNode<FileId, FileLocation>> createClientFromNodeIdDelegate) : base(routingTable, createClientFromNodeIdDelegate)
        {
        }

        public FileToMachineCore(RoutingTable<FileId> routingTable, KademeliaSettings settings, Func<NodeIdentifier<FileId>, ISetKadNode<FileId, FileLocation>> createClientFromNodeIdDelegate, NodeIdentifier<FileId> nodeIdentifier) : base(routingTable, settings, createClientFromNodeIdDelegate, nodeIdentifier)
        {
        }

        protected override Type GetContractType
        {
            get { return typeof (IReverseIndexServiceContract); }
        }
    }
}