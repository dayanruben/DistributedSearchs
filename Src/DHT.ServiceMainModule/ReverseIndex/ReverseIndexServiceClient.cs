// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using DHT.Client;

namespace DHT.MainModule
{
    public class ReverseIndexServiceClient : SetKadNodeClient<string, FileId, IReverseIndexServiceContract>,
                                             IReverseIndexServiceContract, ISetKadNode<string, FileId>
    {
        #region Contructors

        public ReverseIndexServiceClient()
        {
        }

        public ReverseIndexServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public ReverseIndexServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ReverseIndexServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ReverseIndexServiceClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public ReverseIndexServiceClient(ServiceEndpoint endpoint) : base(endpoint)
        {
        }

        #endregion
    }
}