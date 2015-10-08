// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.ServiceModel;

namespace DHT.MainModule
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FileToMachineServiceContract : SetKadNode<FileId, FileLocation>, IFileToMachineServiceContract
    {
        public FileToMachineServiceContract(IKadCore<FileId, IEnumerable<FileLocation>> kadCore) : base(kadCore)
        {
        }
    }
}