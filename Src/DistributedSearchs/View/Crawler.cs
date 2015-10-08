// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using DHT.MainModule;
using DistributedSearchs.Model;

namespace DistributedSearchs
{
    [Export(typeof (ICrawler))]
    public class Crawler : ICrawler
    {
        #region ICrawler Members

        public IEnumerable<PublishedResult> Crawl()
        {
            yield return new PublishedResult("pepe", new[]
                                                         {
                                                             new FileId("hash1", "pepe tiene hambre."),
                                                             new FileId("hash2", "pepe hace la comida"),
                                                         }, new FileLocation("this machine"));
            yield return new PublishedResult("ruth", new[]
                                                         {
                                                             new FileId("hash1", "ruth tambien tiene hambre"),
                                                             new FileId("hash2", "ruth no hace comida"),
                                                         }, new FileLocation("this machine"));
        }

        #endregion
    }

    /// <summary>
    ///   this should be the definitive crawler
    /// </summary>
    public class FileRecordCrawler : ICrawler
    {
        #region ICrawler Members

        public IEnumerable<PublishedResult> Crawl()
        {
            // todo how the crawler could know where is the file service hosted.
            // this property should be retrieved from the config file.
            var localFileServiceLocation = new FileLocation("find this one...");

            return from file in FileRecord.Queryable
                   from reference in file.References
                   group new {reference, file.Hash} by reference.Word
                   into references
                   let files = from rr in references select new FileId(rr.Hash, rr.reference.Part)
                   select new PublishedResult(references.Key, files, localFileServiceLocation);
        }

        #endregion
    }
}