// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ServiceModel;

namespace DHT.MainModule
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ReverseIndexServiceContract : SetKadNode<string, FileId>, IReverseIndexServiceContract,
                                               ISetKadNode<string, FileId>
    {
        public ReverseIndexServiceContract(ISetKadCore<string, FileId> kadCore) : base(kadCore)
        {
        }
    }
}