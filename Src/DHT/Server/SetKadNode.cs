// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace DHT
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SetKadNode<TKey, TValue> : KadNode<TKey, IEnumerable<TValue>>, ISetKadNode<TKey, TValue>
        where TValue : class
    {
        public SetKadNode(IKadCore<TKey, IEnumerable<TValue>> kadCore) : base(kadCore)
        {
        }

        #region ISetKadNode<TKey,TValue> Members

        public override void Store(TKey key, IEnumerable<TValue> value, NodeIdentifier<TKey> callerIdentifier = null)
        {
            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            //** the store methods in this class need to perform a FindValue(key) call first to
            // synchronize the lists, as we may be in the case where a node exits the network
            // and this node only knows about the passed value when it may be others in the network.

            if (DataStore.Contains(key))
                ((IList<TValue>) DataStore[key]).AddRange(value);
            else
            {
                var elements = new List<TValue>();
                try
                {
                    // if the current node doesn't contain the key, then other elements may exist in other nodes
                    // 1. Find other values that may exist in other nodes
                    //                    IEnumerable<TValue> otherNodesValues = KadCore.ValueLookUp(key);

                    // 2. combine the new elements with the existing ones
                    //                    elements.AddRange(otherNodesValues);
                }
                catch (KeyNotFoundException e)
                {
                }
                finally
                {
                    // 2. combine the new elements with the existing ones,
                    // remember that this node searched for the specified id on the network, so the current values
                    // may exist on the list already.
                    elements.AddRange(value);

                    // 3. add the element to the DataStore
                    DataStore.Add(key, elements);
                }
            }
        }

        public virtual void StoreInto(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null)
        {
            Store(key, new[] {value}, callerIdentifier);
        }

        public virtual bool RemoveInto(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null)
        {
            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            return !DataStore.Contains(key) || ((IList<TValue>) DataStore[key]).Remove(value);
        }

        public FindValueResult<TKey, IEnumerable<TValue>> FindPagedValue(TKey key, int pageIndex, int pageCount,
                                                                         NodeIdentifier<TKey> callerIdentifier = null)
        {
            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            return DataStore.Contains(key)
                       ? new FindValueResult<TKey, IEnumerable<TValue>>(
                             DataStore[key].Skip((pageIndex - 1)*pageCount).Take(pageCount))
                       : new FindValueResult<TKey, IEnumerable<TValue>>(GetKClosestContacts(key));
        }

        #endregion
    }
}