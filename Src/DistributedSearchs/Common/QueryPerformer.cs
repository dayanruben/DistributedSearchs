// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DHT;
using DHT.MainModule;

namespace DistributedSearchs
{
    public interface IPerformValueLookUp<TKey, TValue>
    {
        TValue ValueLookUp(TKey key);
    }

    public class QueryPerformer
    {
        private readonly ObservableCollection<SearchResult> _results = new ObservableCollection<SearchResult>();

        private readonly IPerformValueLookUp<FileId, IEnumerable<FileLocation>> fileToMachineValueLookUp;
        private readonly IPerformValueLookUp<string, IEnumerable<FileId>> reverseIndexValueLookUp;

        public QueryPerformer(IPerformValueLookUp<string, IEnumerable<FileId>> reverseIndexValueLookUp,
                              IPerformValueLookUp<FileId, IEnumerable<FileLocation>> fileToMachineValueLookUp)
        {
            this.fileToMachineValueLookUp = fileToMachineValueLookUp;
            this.reverseIndexValueLookUp = reverseIndexValueLookUp;
        }

        public IEnumerable<SearchResult> Results
        {
            get { return _results; }
        }

        public void Query(string query)
        {
            _results.Clear();

            IEnumerable<SearchResult> ss = from word in query.Split(' ').Distinct()
                                           let fileIds = ReverseIndexValueLookUp(word)
                                           from fileId in fileIds
                                           group new {word, fileId} by fileId
                                           into files
                                           let fileLocations = FileToMachineValueLookUp(files.Key)
                                           let parts = (from ff in files select ff.fileId.Part).Distinct()
                                           let words = from ff in files select ff.word
                                               // select new {files.Key.FileHash,Parts= parts,Locations= fileLocations};
                                           select new SearchResult(files.Key, fileLocations, parts, words);

            _results.AddRange(ss);
        }

        private IEnumerable<FileLocation> FileToMachineValueLookUp(FileId key)
        {
            try
            {
                return fileToMachineValueLookUp.ValueLookUp(key);
            }
            catch (Exception)
            {
                return Enumerable.Empty<FileLocation>();
            }
        }

        private IEnumerable<FileId> ReverseIndexValueLookUp(string word)
        {
            try
            {
                return reverseIndexValueLookUp.ValueLookUp(word);
            }
            catch (Exception)
            {
                return Enumerable.Empty<FileId>();
            }
        }
    }
}