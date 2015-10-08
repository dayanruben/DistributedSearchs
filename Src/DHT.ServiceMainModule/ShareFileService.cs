// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.IO;

namespace DHT.MainModule
{
    internal class ShareFileService : IShareFileService
    {
        #region Implementation of IShareFileService

        public bool CheckPath(string path)
        {
            return File.Exists(path);
        }

        public IAsyncResult Share(string path, byte[] buffer, int offset, int numBytes)
        {
            throw new NotImplementedException();
        }

        public int Read(string path, byte[] buffer, int offset, int numBytes)
        {
            FileStream str = File.OpenRead(path);

            return str.Read(buffer, offset, numBytes);
        }

        #endregion
    }
}