// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;

namespace DistributedSearchs
{
    public class Extensors
    {
        public static void After<T>(params IObservable<T>[] observables)
        {
            //             observables.Merge().Subscribe()
        }
    }
}