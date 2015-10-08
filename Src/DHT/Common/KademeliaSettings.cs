// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;

namespace DHT
{
    public class KademeliaSettings
    {
        public KademeliaSettings(TimeSpan clientTimeOut, int k, int cita)
        {
            ClientTimeOut = clientTimeOut;
            K = k;
            Cita = cita;
        }

        /// <summary>
        ///   Total milliseconds to wait before assuming that a client is offline
        /// </summary>
        public TimeSpan ClientTimeOut { get; private set; }

        /// <summary>
        ///   this should be a number that k machines have very low probability to fail
        ///   simultaneously in the system. 20 is recommended by the Kademlia specs.
        ///   this is a system constant
        /// </summary>
        public int K { get; private set; }

        /// <summary>
        ///   Maxium number of simultaneous request service request made by this node when searching
        ///   for this node closest brothers. this is a system constant
        /// </summary>
        public int Cita { get; private set; }

        public static KademeliaSettings Default
        {
            get { return new KademeliaSettings(TimeSpan.FromMilliseconds(1500), 20, 5); }
        }
    }
}