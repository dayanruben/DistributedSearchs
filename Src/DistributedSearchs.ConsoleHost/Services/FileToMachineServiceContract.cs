// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using System.ServiceModel;
using DHT;
using DHT.MainModule;

namespace DistributedSearchs.ConsoleHost.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FileToMachineServiceContract : DHT.MainModule.FileToMachineServiceContract
    {
        public FileToMachineServiceContract(IKadCore<FileId, IEnumerable<FileLocation>> kadCore) : base(kadCore)
        {
        }

        public override HeartBeat<FileId> Ping(NodeIdentifier<FileId> callerIdentifier = null)
        {
            Console.WriteLine("File2Machine -> Ping({0}) : {1}", callerIdentifier, KadCore.NodeIdentifier);
            return base.Ping(callerIdentifier);
        }

        public override void Store(FileId key, IEnumerable<FileLocation> value,
                                   NodeIdentifier<FileId> callerIdentifier = null)
        {
            Console.WriteLine("File2Machine -> Store(key: {0}, value: {1}, callerIdentifier: {2})", key,
                              value.WriteValues(), callerIdentifier);
            base.Store(key, value, callerIdentifier);
        }

        public override bool Remove(FileId key, NodeIdentifier<FileId> callerIdentifier = null)
        {
            Console.WriteLine("File2Machine -> Remove(key: {0}, callerIdentifier: {1})", key, callerIdentifier);
            return base.Remove(key, callerIdentifier);
        }

        public override FindValueResult<FileId, IEnumerable<FileLocation>> FindValue(FileId key,
                                                                                     NodeIdentifier<FileId>
                                                                                         callerIdentifier = null)
        {
            Console.WriteLine("File2Machine -> FindValue(key: {0}, callerIdentifier: {1})", key, callerIdentifier);
            return base.FindValue(key, callerIdentifier);
        }

        public override FindNodeResult<FileId> FindNode(FileId key, NodeIdentifier<FileId> callerIdentifier = null)
        {
            Console.WriteLine("File2Machine -> FindNode(key: {0}, callerIdentifier: {1})", key, callerIdentifier);
            return base.FindNode(key, callerIdentifier);
        }

        public override void StoreInto(FileId key, FileLocation value, NodeIdentifier<FileId> callerIdentifier = null)
        {
            Console.WriteLine("File2Machine -> StoreInto(key: {0}, value: {1}, callerIdentifier: {2})", key, value,
                              callerIdentifier);
            base.StoreInto(key, value, callerIdentifier);
        }

        public override bool RemoveInto(FileId key, FileLocation value, NodeIdentifier<FileId> callerIdentifier = null)
        {
            Console.WriteLine("File2Machine -> RemoveInto(key: {0}, value: {1}, callerIdentifier: {2})", key, value,
                              callerIdentifier);
            return base.RemoveInto(key, value, callerIdentifier);
        }
    }
}