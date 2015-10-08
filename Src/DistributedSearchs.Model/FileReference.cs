// Developed by doiTTeam => devdoiTTeam@gmail.com

using Castle.ActiveRecord;

namespace DistributedSearchs.Model
{
    [ActiveRecord]
    public class FileReference : ActiveRecordBase<FileReference>
    {
        [PrimaryKey]
        [BelongsTo(Type = typeof(FileRecord))]
        public int Id { get; set; }

        [Property]
        public string Word { get; set; }

        [Property]
        public string Part { get; set; }
    }
}