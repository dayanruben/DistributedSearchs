// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.ServiceModel;
using DHT;
using DHT.MainModule;
using FileToMachineServiceContract = DistributedSearchs.ConsoleHost.Services.FileToMachineServiceContract;
using ReverseIndexServiceContract = DistributedSearchs.ConsoleHost.Services.ReverseIndexServiceContract;

namespace DistributedSearchs.ConsoleHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var stringMetric = new LambdaMetric<string>(string.Compare);
            var reverseIndexCore = new ReverseIndexCore(new RoutingTable<string>(stringMetric),
                                                                  nodeid =>
                                                                  new ReverseIndexServiceClient(new BasicHttpBinding(),
                                                                                                new EndpointAddress(
                                                                                                    nodeid.ServiceUrl)));

            var reverseIndexContract = new ReverseIndexServiceContract(reverseIndexCore);
            var reverseIndexHost = new ServiceHost(reverseIndexContract);

            var fileIdMetric = new LambdaMetric<FileId>((f1, f2) => string.Compare(f1.FileHash, f2.FileHash));
            var file2MachineCore = new FileToMachineCore(new RoutingTable<FileId>(fileIdMetric),
                                                                        nodeId =>
                                                                        new FileToMachineClient(new BasicHttpBinding(),
                                                                                                new EndpointAddress(
                                                                                                    nodeId.ServiceUrl)));
            var file2MachineContract = new FileToMachineServiceContract(file2MachineCore);
            var file2MachineHost = new ServiceHost(file2MachineContract);

            reverseIndexCore.NodeIdentifier =
                new NodeIdentifier<string>(reverseIndexHost.BaseAddresses[0].AbsoluteUri + "/ReverseIndex",
                                           Cryptography.GetRandomSH1());
            file2MachineCore.NodeIdentifier =
                new NodeIdentifier<FileId>(file2MachineHost.BaseAddresses[0].AbsoluteUri + "/File",
                                           new FileId(Cryptography.GetRandomSH1(), ""));
            //            var uniqueFile=new FileId("aloneFile","this file is referenc")
            for (int i = 1; i < 3; i++)
            {
                var file1 = new FileId("a" + i, string.Format("[{0}] -- String containing aaaaas.", i));
                var file2 = new FileId("nb" + i, "other string containing BBBBs.");
                var uniq = new FileId("alone", string.Format("[{0} cadena  b{0}]", i));
                reverseIndexContract.StoreInto("a" + i, file1);
                reverseIndexContract.StoreInto("b", file2);
                reverseIndexContract.StoreInto("b" + i, uniq);

                file2MachineContract.Store(file1,
                                           new[]
                                               {
                                                   new FileLocation("some service address"),
                                                   new FileLocation("some service addres1s"),
                                               });
                file2MachineContract.Store(file2, new[] {new FileLocation("some service address")});
                file2MachineContract.Store(uniq, new[] {new FileLocation("some service address")});
            }

            reverseIndexHost.Open();
            file2MachineHost.Open();

            Console.WriteLine("Both services are up and running...");

            Console.WriteLine("Press <ENTER> to exit.");
            Console.ReadLine();

            reverseIndexHost.Close();
            file2MachineHost.Close();
        }
    }
}