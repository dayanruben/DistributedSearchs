// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Linq;

namespace DistributedSearchs.Model
{
    [ActiveRecord]
    public class FileRecord : ActiveRecordLinqBase<FileRecord>
    {
        [PrimaryKey]
        public string Hash { get; set; }

        [Property]
        public string Extension { get; set; }

        [Property]
        public int Size { get; set; }

        [Property]
        public string LocalPath { get; set; }

        /// <summary>
        ///   It's better if the references are calculated only when a new file is discovered and then
        ///   stored in the database, instead of recalculating them every  time. This has to deal also
        ///   with file modifications, but in any the file analysis could be minimized.
        /// </summary>
        [HasMany]
        public IList<FileReference> References { get; set; }
    }
}