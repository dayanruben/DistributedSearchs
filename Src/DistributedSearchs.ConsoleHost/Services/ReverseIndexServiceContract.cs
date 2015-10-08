// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using DHT;
using DHT.MainModule;

//using DHT.MainModule;

namespace DistributedSearchs.ConsoleHost.Services
{
    public class ReverseIndexServiceContract : DHT.MainModule.ReverseIndexServiceContract
    {
        public ReverseIndexServiceContract(ISetKadCore<string, FileId> kadCore) : base(kadCore)
        {
        }

        public override HeartBeat<string> Ping(NodeIdentifier<string> callerIdentifier = null)
        {
            Console.WriteLine("ReverseIndex -> Ping({0}) : {1}", callerIdentifier, KadCore.NodeIdentifier);
            return base.Ping(callerIdentifier);
        }

        public override void Store(string key, IEnumerable<FileId> value, NodeIdentifier<string> callerIdentifier = null)
        {
            Console.WriteLine("ReverseIndex -> Store(key: {0}, value: {1}, callerIdentifier: {2})", key,
                              value.WriteValues(), callerIdentifier);
            base.Store(key, value, callerIdentifier);
        }

        public override bool Remove(string key, NodeIdentifier<string> callerIdentifier = null)
        {
            Console.WriteLine("ReverseIndex -> Remove(key: {0}, callerIdentifier: {1})", key, callerIdentifier);
            return base.Remove(key, callerIdentifier);
        }

        public override FindValueResult<string, IEnumerable<FileId>> FindValue(string key,
                                                                               NodeIdentifier<string> callerIdentifier =
                                                                                   null)
        {
            Console.WriteLine("ReverseIndex -> FindValue(key: {0}, callerIdentifier: {1})", key, callerIdentifier);
            return base.FindValue(key, callerIdentifier);
        }

        public override FindNodeResult<string> FindNode(string key, NodeIdentifier<string> callerIdentifier = null)
        {
            Console.WriteLine("ReverseIndex -> FindNode(key: {0}, callerIdentifier: {1})", key, callerIdentifier);
            return base.FindNode(key, callerIdentifier);
        }

        public override void StoreInto(string key, FileId value, NodeIdentifier<string> callerIdentifier = null)
        {
            Console.WriteLine("ReverseIndex -> StoreInto(key: {0}, value: {1}, callerIdentifier: {2})", key, value,
                              callerIdentifier);
            base.StoreInto(key, value, callerIdentifier);
        }

        public override bool RemoveInto(string key, FileId value, NodeIdentifier<string> callerIdentifier = null)
        {
            Console.WriteLine("ReverseIndex -> RemoveInto(key: {0}, value: {1}, callerIdentifier: {2})", key, value,
                              callerIdentifier);
            return base.RemoveInto(key, value, callerIdentifier);
        }
    }
}