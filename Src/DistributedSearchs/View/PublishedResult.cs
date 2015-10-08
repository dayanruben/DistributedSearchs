// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using DHT.MainModule;

namespace DistributedSearchs
{
    public class PublishedResult
    {
        public PublishedResult(string word, IEnumerable<FileId> files, FileLocation fileLocation)
        {
            Word = word;
            Files = files;
            FileLocation = fileLocation;
        }

        public string Word { get; private set; }

        public IEnumerable<FileId> Files { get; private set; }

        public FileLocation FileLocation { get; private set; }
    }
}