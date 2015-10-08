// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Security.Cryptography;
using System.Text;

namespace DHT
{
    public static class Cryptography
    {
        private static readonly Random random = new Random();

        /// <summary>
        ///   Hash an input string and return the hash as a 32 character hexadecimal string.
        /// </summary>
        /// <param name = "input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            return GetStringFromByteArray(data);
        }

        /// <summary>
        ///   Verify a hash against a string.
        /// </summary>
        /// <param name = "input"></param>
        /// <param name = "hash"></param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        /// <summary>
        ///   Verify a hash against a string. That was encrypted using the SH1 algorithm
        /// </summary>
        /// <param name = "input"></param>
        /// <param name = "hash"></param>
        /// <returns></returns>
        public static bool VerifySH1Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetSH1Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        /// <summary>
        ///   Hash an input string and return the hash as a 32 character hexadecimal string. Using SH1.
        /// </summary>
        /// <param name = "input"></param>
        /// <returns></returns>
        public static string GetSH1Hash(string input)
        {
            return GetSH1Hash(Encoding.Default.GetBytes(input));
        }

        public static string GetSH1Hash(byte[] input)
        {
            SHA1 shaM = new SHA1Managed();

            byte[] data = shaM.ComputeHash(input);

            return GetStringFromByteArray(data);
        }

        private static string GetStringFromByteArray(byte[] data)
        {
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data   
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string GetRandomSH1()
        {
            var bytes = new byte[160];
            random.NextBytes(bytes);
            return GetSH1Hash(bytes);
        }
    }
}