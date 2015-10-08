// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.ServiceModel;

namespace DHT.MainModule
{
    [ServiceContract]
    public interface IShareFileService
    {
        [OperationContract]
        bool CheckPath(string path);

        [OperationContract]
        IAsyncResult Share(string path, byte[] buffer, int offset, int numBytes);
    }
}