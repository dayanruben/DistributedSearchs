// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using DHT.MainModule;
using DistributedSearchs.Tests.Common;

namespace DistributedSearchs.Tests
{
    public static class Mother
    {
        public static QueryPerformer CreateQueryPerformer()
        {
            var reverseIndex = new LamdaValueLookUpPerformer<string, IEnumerable<FileId>>(word =>
                {
                    switch (word)
                    {
                        case "a":
                            return new[] {new FileId("a1", "a1"),};
                        case "aa":
                            return new[] {new FileId("a1", "aa1"),};
                        default:
                            throw new KeyNotFoundException();
                    }
                });
            var fileToMachine = new LamdaValueLookUpPerformer<FileId, IEnumerable<FileLocation>>(file =>
                {
                    switch (file.FileHash)
                    {
                        case "a1":
                            return new[] {new FileLocation("a1"),};
                        default:
                            throw new KeyNotFoundException();
                    }
                });
            var target = new QueryPerformer(reverseIndex, fileToMachine);
            return target;
        }
    }
}