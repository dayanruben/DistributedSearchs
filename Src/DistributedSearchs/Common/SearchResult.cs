// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using DHT.MainModule;

namespace DistributedSearchs
{
    public class SearchResult : INotifyPropertyChanged
    {
        public SearchResult(FileId fileId, IEnumerable<FileLocation> locations)
            : this(fileId, locations, Enumerable.Empty<string>())
        {
        }

        public SearchResult(FileId fileId, IEnumerable<FileLocation> locations, IEnumerable<string> parts)
            : this(fileId, locations, parts, Enumerable.Empty<string>())
        {
        }

        public SearchResult(FileId fileId, IEnumerable<FileLocation> locations, IEnumerable<string> parts,
                            IEnumerable<string> words)
        {
            FileId = fileId;
            Locations = locations;
            Preview = CreateDocument(words, parts);
        }

        public FileId FileId { get; private set; }
        public IEnumerable<FileLocation> Locations { get; private set; }

        public IEnumerable<Inline> Preview { get; private set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private static IEnumerable<Inline> CreateDocument(IEnumerable<string> words, IEnumerable<string> parts)
        {
            return from part in parts
                   from mark in MarkWords(part, words).Concat(new[] {new Run("... ")})
                   select mark;
        }

        private static IEnumerable<Inline> MarkWords(string part, IEnumerable<string> words)
        {
            if (!words.Any(part.Contains))
                return new[] {new Run(part),};

            return from word in words
                   let index = part.IndexOf(word)
                   where index > -1
                   let leftString = part.Substring(0, index)
                   let rightString = part.Substring(index + word.Length)
                   let leftPart = MarkWords(leftString, words)
                   let rightPart = MarkWords(rightString, words)
                   from res in leftPart.Concat(new[] {new Run(word) {FontWeight = FontWeights.Bold}}).Concat(rightPart)
                   select res;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region DelegateCommand DownloadCommand

        private DelegateCommand _downloadCommand;

        public DelegateCommand DownloadCommand
        {
            get { return _downloadCommand ?? (_downloadCommand = new DelegateCommand(StartDownload)); }
        }

        private void StartDownload()
        {
            var downloadManager = new DownloadManager(FileId, Locations);
            downloadManager.Start();
        }

        #endregion

        #region DelegateCommand MoreInfoCommand

        private DelegateCommand _moreInfoCommand;

        public DelegateCommand MoreInfoCommand
        {
            get { return _moreInfoCommand ?? (_moreInfoCommand = new DelegateCommand(MoreInfoRequested)); }
        }

        /// <summary>
        ///   this method downloads information more specific to tis file, this has to be a different operation on
        ///   the FileService. Maybe the client wants to see some information about the file (such as extension, size, icon,...)
        ///   before downloading it.
        /// </summary>
        private void MoreInfoRequested()
        {
            // todo get more info for a specific file
            MessageBox.Show("<TODO>");
        }

        #endregion
    }
}