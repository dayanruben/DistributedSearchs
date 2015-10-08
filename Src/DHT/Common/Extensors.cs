// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;

namespace DHT
{
    public static class Extensors
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
                collection.Add(item);
        }

        public static string WriteValues<T>(this IEnumerable<T> items)
        {
            string result = "";
            foreach (T item in items)
            {
                result += ", " + item;
            }
            return result;
        }

        public static int Xor(this int value, int other)
        {
            throw new Exception();
        }
    }
}