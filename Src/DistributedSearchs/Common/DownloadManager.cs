// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using DHT.MainModule;
using Microsoft.Win32;

namespace DistributedSearchs
{
    public class DownloadManager
    {
        /// <summary>
        ///   this class manages the correct download of the file <paramref name = "fileId" /> from
        ///   <paramref name = "locations" />.
        /// </summary>
        /// <param name = "fileId"></param>
        /// <param name = "locations"></param>
        public DownloadManager(FileId fileId, IEnumerable<FileLocation> locations)
        {
            FileId = fileId;
            Locations = locations;
        }

        public FileId FileId { get; private set; }
        public IEnumerable<FileLocation> Locations { get; private set; }

        /// <summary>
        ///   Starts the download, first asks for a path to save the file.
        /// </summary>
        public void Start()
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                // todo Download manager from file service developed by eric... the path is above
            }
        }
    }
}