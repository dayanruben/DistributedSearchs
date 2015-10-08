// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using DHT.Client;

namespace DHT.MainModule
{
    public class FileToMachineClient : SetKadNodeClient<FileId, FileLocation, IFileToMachineServiceContract>,
                                       IFileToMachineServiceContract
    {
        #region Constructors

        public FileToMachineClient(ServiceEndpoint endpoint) : base(endpoint)
        {
        }

        public FileToMachineClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public FileToMachineClient(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        public FileToMachineClient(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        public FileToMachineClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public FileToMachineClient()
        {
        }

        #endregion
    }
}