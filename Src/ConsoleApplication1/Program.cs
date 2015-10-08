// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using DHT;
using DHT.MainModule;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ContractDescription contract = ContractDescription.GetContract(typeof (IReverseIndexServiceContract));
            Binding binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://localhost:8020/ReverseIndex");

            var endpoint = new ServiceEndpoint(contract, binding, address);

            using (var client = new ReverseIndexServiceClient(endpoint))
            {
                HeartBeat<string> res = client.Ping(new NodeIdentifier<string>("asd", "asdasd"));
                Console.WriteLine(res);
            }
            Console.ReadLine();
        }
    }
}