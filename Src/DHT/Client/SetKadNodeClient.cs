// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace DHT.Client
{
    public class SetKadNodeClient<TKey, TValue, TKadNode> : KadNodeClient<TKey, IEnumerable<TValue>, TKadNode>,
                                                            ISetKadNode<TKey, TValue>
        where TValue : class
        where TKadNode : class, ISetKadNode<TKey, TValue>
    {
        #region Constructors

        public SetKadNodeClient(ServiceEndpoint endpoint) : base(endpoint)
        {
        }

        public SetKadNodeClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public SetKadNodeClient(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        public SetKadNodeClient(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        public SetKadNodeClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public SetKadNodeClient()
        {
        }

        #endregion

        #region ISetKadNode<TKey,TValue> Members

        public void StoreInto(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null)
        {
            Channel.StoreInto(key, value, callerIdentifier);
        }

        public bool RemoveInto(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null)
        {
            return Channel.RemoveInto(key, value, callerIdentifier);
        }

        public FindValueResult<TKey, IEnumerable<TValue>> FindPagedValue(TKey key, int pageIndex, int pageCount,
                                                                         NodeIdentifier<TKey> callerIdentifier = null)
        {
            return Channel.FindPagedValue(key, pageIndex, pageCount, callerIdentifier);
        }

        #endregion
    }
}