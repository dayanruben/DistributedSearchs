// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;

namespace DistributedSearchs
{
    public interface ICrawler
    {
        IEnumerable<PublishedResult> Crawl();
    }
}