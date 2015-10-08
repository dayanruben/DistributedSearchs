// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Runtime.Serialization;

namespace DHT.MainModule
{
    [DataContract]
    public class FileId : IEquatable<FileId>
    {
        public FileId(string fileHash, string part)
        {
            Part = part;
            FileHash = fileHash;
        }

        [DataMember]
        public string Part { get; private set; }

        [DataMember]
        public string FileHash { get; private set; }

        #region Equality members

        public bool Equals(FileId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FileHash, FileHash);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FileId)) return false;
            return Equals((FileId) obj);
        }

        public override int GetHashCode()
        {
            return (FileHash != null ? FileHash.GetHashCode() : 0);
        }

        public static bool operator ==(FileId left, FileId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileId left, FileId right)
        {
            return !Equals(left, right);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("FileHash: {0}, Part: {1}", FileHash, Part);
        }
    }
}