// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using DHT;
using DHT.MainModule;

namespace DistributedSearchs
{
    /// <summary>
    ///   this class tries to discover all services in the local network matching: <see cref = "IReverseIndexServiceContract" />
    ///   and <see cref = "IFileToMachineServiceContract" />, using WCF Discovery
    /// </summary>
    public class KadNodeFinder
    {
        public void InitDiscovery()
        {
            IObservable<ServiceEndpointDiscoveredEventArgs<string>> reverseIndexObservable = Observable.Start(
                () => GetServiceDiscoveryData<IReverseIndexServiceContract>())
                .Merge()
                .Select(discoveryMetadata =>
                    {
                        try
                        {
                            var client = new ReverseIndexServiceClient(new BasicHttpBinding(), discoveryMetadata.Address);
                            return new ServiceEndpointDiscoveredEventArgs<string>(client.Ping().NodeIdentifier,
                                                                                  discoveryMetadata);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    })
                .Where(discoveredEventArgs => discoveredEventArgs != null)
                .Do(OnReverseIndexServiceEndpointDiscovered);

            IObservable<ServiceEndpointDiscoveredEventArgs<FileId>> fileToMachineObservable =
                Observable.Start(() => GetServiceDiscoveryData<IFileToMachineServiceContract>())
                    .Merge()
                    .Select(discoveryMetadata =>
                        {
                            try
                            {
                                var client = new FileToMachineClient(new BasicHttpBinding(), discoveryMetadata.Address);
                                return new ServiceEndpointDiscoveredEventArgs<FileId>(client.Ping().NodeIdentifier,
                                                                                      discoveryMetadata);
                            }
                            catch (Exception)
                            {
                                return null;
                            }
                        })
                    .Where(discoveredEventArgs => discoveredEventArgs != null)
                    .Do(OnFileToMachineEndpointDiscovered);

            reverseIndexObservable.Merge<object>(fileToMachineObservable)
                .ObserveOnDispatcher()
                .Subscribe(_ => { }, OnDiscoveryFinished);
        }

        public static IObservable<EndpointDiscoveryMetadata> GetServiceDiscoveryData<TServiceContract>()
        {
            var result = new Subject<EndpointDiscoveryMetadata>();
            var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

            discoveryClient.FindProgressChanged += (_, e) => result.OnNext(e.EndpointDiscoveryMetadata);
            discoveryClient.FindCompleted += (_, e) =>
                {
                    result.OnCompleted();
                    discoveryClient.Close();
                };

            discoveryClient.FindAsync(new FindCriteria(typeof (TServiceContract)));
            return result;
        }

        public event EventHandler<ServiceEndpointDiscoveredEventArgs<string>> ReverseIndexServiceEndpointDiscovered =
            delegate { };

        public void OnReverseIndexServiceEndpointDiscovered(ServiceEndpointDiscoveredEventArgs<string> e)
        {
            EventHandler<ServiceEndpointDiscoveredEventArgs<string>> handler = ReverseIndexServiceEndpointDiscovered;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ServiceEndpointDiscoveredEventArgs<FileId>> FileToMachineEndpointDiscovered =
            delegate { };

        public void OnFileToMachineEndpointDiscovered(ServiceEndpointDiscoveredEventArgs<FileId> e)
        {
            EventHandler<ServiceEndpointDiscoveredEventArgs<FileId>> handler = FileToMachineEndpointDiscovered;
            if (handler != null) handler(this, e);
        }

        public event EventHandler DiscoveryFinished = delegate { };

        public void OnDiscoveryFinished()
        {
            EventHandler handler = DiscoveryFinished;
            if (handler != null) handler(this, new EventArgs());
        }
    }

    public class ServiceEndpointDiscoveredEventArgs<TKey> : EventArgs
    {
        public ServiceEndpointDiscoveredEventArgs(NodeIdentifier<TKey> nodeIdentifier,
                                                  EndpointDiscoveryMetadata metadata)
        {
            NodeIdentifier = nodeIdentifier;
            Metadata = metadata;
        }

        public EndpointDiscoveryMetadata Metadata { get; private set; }

        public NodeIdentifier<TKey> NodeIdentifier { get; private set; }
    }
}