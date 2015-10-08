// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Reflection;
using log4net;

namespace DHT
{
    public static class KadLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger("KADEMELIA");

        public static void Debug(object message)
        {
            Logger.Debug(message);
        }

        public static void Info(object message)
        {
            Logger.Info(message);
        }

        public static void Info(object message, Exception exception)
        {
            Logger.Info(message, exception);
        }

        public static void Debug(object message, Exception exception)
        {
            Logger.Debug(message, exception);
        }

        public static void Warn(object message)
        {
            Logger.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            Logger.Warn(message, exception);
        }

        public static void Error(object message)
        {
            Logger.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            Logger.Error(message, exception);
        }

        public static void Fatal(object message)
        {
            Logger.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            Logger.Fatal(message, exception);
        }

    }
}