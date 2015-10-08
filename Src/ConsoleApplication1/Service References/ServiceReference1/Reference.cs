// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using DHT;
using DHT.MainModule;

namespace ConsoleApplication1.ServiceReference1
{
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName = "ServiceReference1.IReverseIndexServiceContract")]
    public interface IReverseIndexServiceContract
    {
        // CODEGEN: Generating message contract since the wrapper namespace (DHT) of message PingRequest does not match the default value (http://tempuri.org/)
        [OperationContract(Action = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/Ping",
            ReplyAction = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/PingResponse")]
        PingResponse Ping(PingRequest request);

        // CODEGEN: Generating message contract since the wrapper namespace (DHT) of message StoreRequest does not match the default value (http://tempuri.org/)
        [OperationContract(Action = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/Store",
            ReplyAction = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/StoreResponse")]
        StoreResponse Store(StoreRequest request);

        // CODEGEN: Generating message contract since the wrapper namespace (DHT) of message FindNodeRequest does not match the default value (http://tempuri.org/)
        [OperationContract(Action = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/FindNode",
            ReplyAction = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/FindNodeResponse")]
        [ServiceKnownType(typeof (FindValueResult<string, FileId[]>))]
        FindNodeResponse FindNode(FindNodeRequest request);

        // CODEGEN: Generating message contract since the wrapper namespace (DHT) of message FindValueRequest does not match the default value (http://tempuri.org/)
        [OperationContract(Action = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/FindValue",
            ReplyAction = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/FindValueResponse")]
        FindValueResponse FindValue(FindValueRequest request);

        // CODEGEN: Generating message contract since the wrapper namespace (DHT) of message RemoveRequest does not match the default value (http://tempuri.org/)
        [OperationContract(Action = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/Remove",
            ReplyAction = "DHT/IKadNodeOf_String_IEnumerableOf_FileId/RemoveResponse")]
        RemoveResponse Remove(RemoveRequest request);

        [OperationContract(Action = "http://tempuri.org/ISetKadNodeOf_String_FileId/StoreInto",
            ReplyAction = "http://tempuri.org/ISetKadNodeOf_String_FileId/StoreIntoResponse")]
        void StoreInto(string key, FileId value, NodeIdentifier<string> callerIdentifier);

        [OperationContract(Action = "http://tempuri.org/ISetKadNodeOf_String_FileId/RemoveInto",
            ReplyAction = "http://tempuri.org/ISetKadNodeOf_String_FileId/RemoveIntoResponse")]
        bool RemoveInto(string key, FileId value, NodeIdentifier<string> callerIdentifier);

        [OperationContract(Action = "http://tempuri.org/ISetKadNodeOf_String_FileId/FindPagedValue",
            ReplyAction = "http://tempuri.org/ISetKadNodeOf_String_FileId/FindPagedValueResponse")]
        FindValueResult<string, FileId[]> FindPagedValue(string key, int pageIndex, int pageCount,
                                                         NodeIdentifier<string> callerIdentifier);
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "Ping", WrapperNamespace = "DHT", IsWrapped = true)]
    public class PingRequest
    {
        [MessageBodyMember(Namespace = "DHT", Order = 0)] public NodeIdentifier<string> callerIdentifier;

        public PingRequest()
        {
        }

        public PingRequest(NodeIdentifier<string> callerIdentifier)
        {
            this.callerIdentifier = callerIdentifier;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "PingResponse", WrapperNamespace = "DHT", IsWrapped = true)]
    public class PingResponse
    {
        [MessageBodyMember(Namespace = "DHT", Order = 0)] public HeartBeat<string> PingResult;

        public PingResponse()
        {
        }

        public PingResponse(HeartBeat<string> PingResult)
        {
            this.PingResult = PingResult;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "Store", WrapperNamespace = "DHT", IsWrapped = true)]
    public class StoreRequest
    {
        [MessageBodyMember(Namespace = "DHT", Order = 2)] public NodeIdentifier<string> callerIdentifier;

        [MessageBodyMember(Namespace = "DHT", Order = 0)] public string key;

        [MessageBodyMember(Namespace = "DHT", Order = 1)] public FileId[] value;

        public StoreRequest()
        {
        }

        public StoreRequest(string key, FileId[] value, NodeIdentifier<string> callerIdentifier)
        {
            this.key = key;
            this.value = value;
            this.callerIdentifier = callerIdentifier;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "StoreResponse", WrapperNamespace = "DHT", IsWrapped = true)]
    public class StoreResponse
    {
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "FindNode", WrapperNamespace = "DHT", IsWrapped = true)]
    public class FindNodeRequest
    {
        [MessageBodyMember(Namespace = "DHT", Order = 1)] public NodeIdentifier<string> callerIdentifier;

        [MessageBodyMember(Namespace = "DHT", Order = 0)] public string key;

        public FindNodeRequest()
        {
        }

        public FindNodeRequest(string key, NodeIdentifier<string> callerIdentifier)
        {
            this.key = key;
            this.callerIdentifier = callerIdentifier;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "FindNodeResponse", WrapperNamespace = "DHT", IsWrapped = true)]
    public class FindNodeResponse
    {
        [MessageBodyMember(Namespace = "DHT", Order = 0)] public FindNodeResult<string> FindNodeResult;

        public FindNodeResponse()
        {
        }

        public FindNodeResponse(FindNodeResult<string> FindNodeResult)
        {
            this.FindNodeResult = FindNodeResult;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "FindValue", WrapperNamespace = "DHT", IsWrapped = true)]
    public class FindValueRequest
    {
        [MessageBodyMember(Namespace = "DHT", Order = 1)] public NodeIdentifier<string> callerIdentifier;

        [MessageBodyMember(Namespace = "DHT", Order = 0)] public string key;

        public FindValueRequest()
        {
        }

        public FindValueRequest(string key, NodeIdentifier<string> callerIdentifier)
        {
            this.key = key;
            this.callerIdentifier = callerIdentifier;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "FindValueResponse", WrapperNamespace = "DHT", IsWrapped = true)]
    public class FindValueResponse
    {
        [MessageBodyMember(Namespace = "DHT", Order = 0)] public FindValueResult<string, FileId[]> FindValueResult;

        public FindValueResponse()
        {
        }

        public FindValueResponse(FindValueResult<string, FileId[]> FindValueResult)
        {
            this.FindValueResult = FindValueResult;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "Remove", WrapperNamespace = "DHT", IsWrapped = true)]
    public class RemoveRequest
    {
        [MessageBodyMember(Namespace = "DHT", Order = 1)] public NodeIdentifier<string> callerIdentifier;

        [MessageBodyMember(Namespace = "DHT", Order = 0)] public string key;

        public RemoveRequest()
        {
        }

        public RemoveRequest(string key, NodeIdentifier<string> callerIdentifier)
        {
            this.key = key;
            this.callerIdentifier = callerIdentifier;
        }
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(WrapperName = "RemoveResponse", WrapperNamespace = "DHT", IsWrapped = true)]
    public class RemoveResponse
    {
        [MessageBodyMember(Namespace = "DHT", Order = 0)] public bool RemoveResult;

        public RemoveResponse()
        {
        }

        public RemoveResponse(bool RemoveResult)
        {
            this.RemoveResult = RemoveResult;
        }
    }

    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface IReverseIndexServiceContractChannel : IReverseIndexServiceContract, IClientChannel
    {
    }

    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class ReverseIndexServiceContractClient : ClientBase<IReverseIndexServiceContract>,
                                                     IReverseIndexServiceContract
    {
        public ReverseIndexServiceContractClient()
        {
        }

        public ReverseIndexServiceContractClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public ReverseIndexServiceContractClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ReverseIndexServiceContractClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ReverseIndexServiceContractClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        #region IReverseIndexServiceContract Members

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        PingResponse IReverseIndexServiceContract.Ping(PingRequest request)
        {
            return base.Channel.Ping(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        StoreResponse IReverseIndexServiceContract.Store(StoreRequest request)
        {
            return base.Channel.Store(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        FindNodeResponse IReverseIndexServiceContract.FindNode(FindNodeRequest request)
        {
            return base.Channel.FindNode(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        FindValueResponse IReverseIndexServiceContract.FindValue(FindValueRequest request)
        {
            return base.Channel.FindValue(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        RemoveResponse IReverseIndexServiceContract.Remove(RemoveRequest request)
        {
            return base.Channel.Remove(request);
        }

        public void StoreInto(string key, FileId value, NodeIdentifier<string> callerIdentifier)
        {
            base.Channel.StoreInto(key, value, callerIdentifier);
        }

        public bool RemoveInto(string key, FileId value, NodeIdentifier<string> callerIdentifier)
        {
            return base.Channel.RemoveInto(key, value, callerIdentifier);
        }

        public FindValueResult<string, FileId[]> FindPagedValue(string key, int pageIndex, int pageCount,
                                                                NodeIdentifier<string> callerIdentifier)
        {
            return base.Channel.FindPagedValue(key, pageIndex, pageCount, callerIdentifier);
        }

        #endregion

        public HeartBeat<string> Ping(NodeIdentifier<string> callerIdentifier)
        {
            var inValue = new PingRequest();
            inValue.callerIdentifier = callerIdentifier;
            PingResponse retVal = ((IReverseIndexServiceContract) (this)).Ping(inValue);
            return retVal.PingResult;
        }

        public void Store(string key, FileId[] value, NodeIdentifier<string> callerIdentifier)
        {
            var inValue = new StoreRequest();
            inValue.key = key;
            inValue.value = value;
            inValue.callerIdentifier = callerIdentifier;
            StoreResponse retVal = ((IReverseIndexServiceContract) (this)).Store(inValue);
        }

        public FindNodeResult<string> FindNode(string key, NodeIdentifier<string> callerIdentifier)
        {
            var inValue = new FindNodeRequest();
            inValue.key = key;
            inValue.callerIdentifier = callerIdentifier;
            FindNodeResponse retVal = ((IReverseIndexServiceContract) (this)).FindNode(inValue);
            return retVal.FindNodeResult;
        }

        public FindValueResult<string, FileId[]> FindValue(string key, NodeIdentifier<string> callerIdentifier)
        {
            var inValue = new FindValueRequest();
            inValue.key = key;
            inValue.callerIdentifier = callerIdentifier;
            FindValueResponse retVal = ((IReverseIndexServiceContract) (this)).FindValue(inValue);
            return retVal.FindValueResult;
        }

        public bool Remove(string key, NodeIdentifier<string> callerIdentifier)
        {
            var inValue = new RemoveRequest();
            inValue.key = key;
            inValue.callerIdentifier = callerIdentifier;
            RemoveResponse retVal = ((IReverseIndexServiceContract) (this)).Remove(inValue);
            return retVal.RemoveResult;
        }
    }
}