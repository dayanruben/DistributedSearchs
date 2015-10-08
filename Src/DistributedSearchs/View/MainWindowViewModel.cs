// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using DHT;
using DHT.MainModule;
using DistributedSearchs.MVVM;
using ReactiveUI;
using ReactiveUI.Xaml;
using log4net;
using ILog = log4net.ILog;

namespace DistributedSearchs
{
    [Export]
    public class MainWindowViewModel : ReactiveObject, IViewModel<MainWindow>
    {
        private static readonly ILog Logger = LogManager.GetLogger(
            MethodBase.GetCurrentMethod().DeclaringType);

        private readonly QueryPerformer _queryPerformer;
        [Import(typeof (ICrawler))] private ICrawler _crawler;

        private string _currentQuery;
        private bool _discoveryHasEnded;
        private bool _isBusy = true;

        #region ReactiveAsyncCommand ScanNowCommand

        private ReactiveAsyncCommand _scanNowCommand;

        public ReactiveAsyncCommand ScanNowCommand
        {
            get
            {
                if (_scanNowCommand != null) return _scanNowCommand;

                _scanNowCommand = new ReactiveAsyncCommand();
                _scanNowCommand.RegisterAsyncAction(_ => ScanNowSync());

                return _scanNowCommand;
            }
        }

        private void ScanNowSync()
        {
            foreach (var searchResult in _crawler.Crawl())
            {
                foreach (var fileId in searchResult.Files)
                    FileToMachineHandler.StoreIntoLookUp(fileId, searchResult.FileLocation);

                ReverseIndexHandler.StoreLookUp(searchResult.Word, searchResult.Files);
            }
        }

        #endregion

        #region ReactiveAsyncCommand DiscoverNodesCommand

        private ReactiveAsyncCommand _discoverNodesCommand;

        public ReactiveAsyncCommand DiscoverNodesCommand
        {
            get
            {
                if (_discoverNodesCommand != null) return _discoverNodesCommand;

                _discoverNodesCommand = new ReactiveAsyncCommand();
                _discoverNodesCommand.RegisterAsyncAction(_ => DiscoverNodesSync());

                return _discoverNodesCommand;
            }
        }

        private void DiscoverNodesSync()
        {
            using (var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint()))
            {
                FindResponse fileToMachineEndPoints =
                    discoveryClient.Find(new FindCriteria(typeof (IFileToMachineServiceContract)));
                foreach (EndpointDiscoveryMetadata endpoint in fileToMachineEndPoints.Endpoints)
                {
                    try
                    {
                        var client = new FileToMachineClient(new BasicHttpBinding(), endpoint.Address);
                        FileToMachineHandler.AddToRoutingTable(client.Ping().NodeIdentifier);
                    }
                    catch (CommunicationException e)
                    {
                    }
                }

                FindResponse reverseIndexEndPoints =
                    discoveryClient.Find(new FindCriteria(typeof (IReverseIndexServiceContract)));
                foreach (EndpointDiscoveryMetadata endPoint in reverseIndexEndPoints.Endpoints)
                {
                    try
                    {
                        var client = new ReverseIndexServiceClient(new BasicHttpBinding(), endPoint.Address);
                        ReverseIndexHandler.AddToRoutingTable(client.Ping().NodeIdentifier);
                    }
                    catch (CommunicationException e)
                    {
                    }
                }


            }
        }

        #endregion

        #region DelegateCommand<string> SearchCommand

        private DelegateCommand<string> _searchCommand;

        public DelegateCommand<string> SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new DelegateCommand<string>(_ => SearchAsync())); }
        }

        private void SearchAsync()
        {
            Observable.Return(CurrentQuery)
                .ObserveOnDispatcher()
                .Do(_ => IsBusy = true)
                .Do(q => _queryPerformer.Query(q))
                .Subscribe(_ => { }, () => IsBusy = false);
        }

        #endregion

        #region DelegateCommand FolderManagementCommand

        private DelegateCommand _folderManagementCommand;

        public DelegateCommand FolderManagementCommand
        {
            get { return _folderManagementCommand ?? (_folderManagementCommand = new DelegateCommand(FolderManagement)); }
        }

        private void FolderManagement()
        {
            (new FolderManagementWindow {Owner = View}).ShowDialog();
        }

        #endregion

        public MainWindowViewModel()
        {
            Logger.Info("Initializing MainWindow ViewModel");
            InitDiscovery();

            var metric = new LambdaMetric<string>(string.Compare);

            // todo?? how can i know the address where these services are hosted from the config file??
            string reverseIndexHostedAddress = "http://localhost:8020/ReverseIndex";
            string fileToMachineHostedAddress = "http://localhost:8021/FileToMachine";

            ReverseIndexHandler = new ReverseIndexHandler(metric)
                {
                    NodeIdentifier = new NodeIdentifier<string>(reverseIndexHostedAddress,
                                                    Cryptography.GetRandomSH1())
                };

            var fileIdMetric = new LambdaMetric<FileId>((f1, f2) => string.Compare(f1.FileHash, f2.FileHash));
            FileToMachineHandler = new FileToMachineHandler(fileIdMetric)
                {
                    NodeIdentifier = new NodeIdentifier<FileId>(fileToMachineHostedAddress,
                                                    new FileId(Cryptography.GetRandomSH1(), ""))
                };

            _queryPerformer = new QueryPerformer(ReverseIndexHandler, FileToMachineHandler);

            this.ObservableForProperty(model => model.IsHostStarted)
                .Select(change=>change.Value)
                .DistinctUntilChanged()
                .Subscribe(host =>
                    {
                        if (host)
                            StartService();
                        else CloseService();
                    });
            
            Logger.Info("The reverse index service node identifier is: " + ReverseIndexHandler.NodeIdentifier);
            Logger.Info("The file2machine service node identifier is: " + FileToMachineHandler.NodeIdentifier);
        }

        /// <summary>
        ///   after the discovery process has finished if no other nodes are found on the network,
        ///   then this property returns true.
        /// </summary>
        public bool DiscoveryHasEnded
        {
            get { return _discoveryHasEnded; }
            private set { this.RaiseAndSetIfChanged(model => model.DiscoveryHasEnded, ref _discoveryHasEnded, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { this.RaiseAndSetIfChanged(model => model.IsBusy, ref _isBusy, value); }
        }

        public string CurrentQuery
        {
            get { return _currentQuery; }
            set { this.RaiseAndSetIfChanged(model => model.CurrentQuery, ref _currentQuery, value); }
        }

        public IEnumerable<SearchResult> Files
        {
            get { return _queryPerformer.Results; }
        }

        protected FileToMachineHandler FileToMachineHandler { get; private set; }

        protected ReverseIndexHandler ReverseIndexHandler { get; private set; }

        /// <summary>
        ///   returns true if this client is able to make querys
        /// </summary>
        private bool IsActive { get; set; }

        #region IViewModel<MainWindow> Members

        [Import]
        public MainWindow View { get; private set; }

        #endregion

        private void InitDiscovery()
        {
            Logger.Info("Starting the (Initial) Discovery proccess...");
            int discoveredReverseIndex = 0, discoveredFileToMachine = 0;
            var finder = new KadNodeFinder();
            finder.FileToMachineEndpointDiscovered += (_, e) =>
                {
                    discoveredReverseIndex++;
                    FileToMachineHandler.AddToRoutingTable(e.NodeIdentifier);
                    Logger.Info("New Reverse Index service node found: " + e.NodeIdentifier);
                };
            finder.ReverseIndexServiceEndpointDiscovered += (_, e) =>
                {
                    discoveredFileToMachine++;
                    ReverseIndexHandler.AddToRoutingTable(e.NodeIdentifier);
                    Logger.Info("New File2Machine service node found: " + e.NodeIdentifier);
                };
            finder.DiscoveryFinished += (_, __) => NodeDiscoveryCompleted();

            // when minNodes are discovered, ActivateAndJoin()
            const int minKnownNodesBeforeJoiningAsClient = 1;
            finder.FileToMachineEndpointDiscovered +=
                (_, __) =>
                    {
                        if (discoveredReverseIndex >= minKnownNodesBeforeJoiningAsClient &&
                            discoveredFileToMachine >= minKnownNodesBeforeJoiningAsClient &&
                            !IsActive)
                            ActivateAndJoin();
                    };
            finder.InitDiscovery();
        }

        /// <summary>
        ///   this method is called after the node discovery has finished
        /// </summary>
        private void NodeDiscoveryCompleted()
        {
            // on the init screen after the discovery process has finished:
            // if other nodes are found on the network, then the application joins to those nodes automatically
            // and if no other nodes are found, the application should notify the user and allow the creation of
            // a new Kademlia Node
            DiscoveryHasEnded = true;
            IsBusy = false;

            Logger.Info("Initial Node discovery completed.");
        }

        private bool _isHostStarted;
        public bool IsHostStarted
        {
            get { return _isHostStarted; }
            set { this.RaiseAndSetIfChanged(model => model.IsHostStarted, ref _isHostStarted, value); }
        }

        public void StartService()
        {
            Logger.Info("Starting services.");

            ReverseIndexHandler.StartService();
            FileToMachineHandler.StartService();
            
            Logger.Info("Services started");
        }

        public void CloseService()
        {
            Logger.Info("Closing services");

            ReverseIndexHandler.CloseService();
            FileToMachineHandler.CloseService();

            Logger.Info("Services closed.");
        }

        /// <summary>
        ///   Activates the search queries and allow the user to perform queries against the known nodes.
        /// </summary>
        private void ActivateAndJoin()
        {
            Logger.Info("Activating and joining to the net, the user is now able to perform querys.");
            IsActive = true;
            IsBusy = false;
        }
    }
}