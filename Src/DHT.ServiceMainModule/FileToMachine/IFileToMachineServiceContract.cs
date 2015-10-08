// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ServiceModel;

namespace DHT.MainModule
{
    [ServiceContract]
    public interface IFileToMachineServiceContract : ISetKadNode<FileId, FileLocation>
    {
    }
}