// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Runtime.Serialization;

namespace DHT.MainModule
{
    [DataContract]
    public class FileLocation
    {
        public FileLocation(string serviceUrl)
        {
            ServiceUrl = serviceUrl;
        }

        [DataMember(Name = "ServiceUrl")]
        public string ServiceUrl { get; private set; }
    }
}