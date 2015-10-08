// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace DHT.Client
{
    public class KadNodeClient<TKey, TValue, TKadNode> : ClientBase<TKadNode>, IKadNode<TKey, TValue>
        where TValue : class
        where TKadNode : class, IKadNode<TKey, TValue>
    {
        #region Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "T:System.ServiceModel.ClientBase`1" /> class using the specified  
        ///   <see cref = "T:System.ServiceModel.Description.ServiceEndpoint" />.
        /// </summary>
        /// <param name = "endpoint">The endpoint for a service that allows clients to find and communicate with the service.</param>
        public KadNodeClient(ServiceEndpoint endpoint) : base(endpoint)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "T:System.ServiceModel.ClientBase`1" /> class using the specified binding and target address.
        /// </summary>
        /// <param name = "binding">The binding with which to make calls to the service.</param>
        /// <param name = "remoteAddress">The address of the service endpoint.</param>
        /// <exception cref = "T:System.ArgumentNullException">The binding is null.</exception>
        /// <exception cref = "T:System.ArgumentNullException">The remote address is null.</exception>
        public KadNodeClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "T:System.ServiceModel.ClientBase`1" /> class 
        ///   using the specified target address and endpoint information.
        /// </summary>
        /// <param name = "endpointConfigurationName">The name of the endpoint in the application configuration file.</param>
        /// <param name = "remoteAddress">The address of the service.</param>
        /// <exception cref = "T:System.ArgumentNullException">The endpoint is null.</exception>
        /// <exception cref = "T:System.ArgumentNullException">The remote address is null.</exception>
        /// <exception cref = "T:System.InvalidOperationException">The endpoint cannot be found or the endpoint contract is not valid.</exception>
        public KadNodeClient(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "T:System.ServiceModel.ClientBase`1" /> class.
        /// </summary>
        /// <param name = "endpointConfigurationName">The name of the endpoint in the application configuration file.</param>
        /// <param name = "remoteAddress">The address of the service.</param>
        /// <exception cref = "T:System.ArgumentNullException">The endpoint is null.</exception>
        /// <exception cref = "T:System.ArgumentNullException">The remote address is null.</exception>
        /// <exception cref = "T:System.InvalidOperationException">The endpoint cannot be found or the endpoint contract is not valid.</exception>
        public KadNodeClient(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "T:System.ServiceModel.ClientBase`1" /> class using the configuration information specified in the application configuration file by <paramref
        ///    name = "endpointConfigurationName" />.
        /// </summary>
        /// <param name = "endpointConfigurationName">The name of the endpoint in the application configuration file.</param>
        /// <exception cref = "T:System.ArgumentNullException">The specified endpoint information is null.</exception>
        /// <exception cref = "T:System.InvalidOperationException">The endpoint cannot be found or the endpoint contract is not valid.</exception>
        public KadNodeClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "T:System.ServiceModel.ClientBase`1" /> class using the default target endpoint from the application configuration file.
        /// </summary>
        /// <exception cref = "T:System.InvalidOperationException">Either there is no default endpoint information in the configuration file, more than one endpoint in the file, or no configuration file.</exception>
        public KadNodeClient()
        {
        }

        #endregion

        #region IKadNode<TKey,TValue> Members

        public HeartBeat<TKey> Ping(NodeIdentifier<TKey> callerIdentifier = null)
        {
            return Channel.Ping(callerIdentifier);
        }

        public void Store(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null)
        {
            Channel.Store(key, value, callerIdentifier);
        }

        public FindNodeResult<TKey> FindNode(TKey key, NodeIdentifier<TKey> callerIdentifier = null)
        {
            return Channel.FindNode(key, callerIdentifier);
        }

        public FindValueResult<TKey, TValue> FindValue(TKey key, NodeIdentifier<TKey> callerIdentifier = null)
        {
            return Channel.FindValue(key, callerIdentifier);
        }

        public bool Remove(TKey key, NodeIdentifier<TKey> callerIdentifier = null)
        {
            return Channel.Remove(key, callerIdentifier);
        }

        #endregion
    }
}