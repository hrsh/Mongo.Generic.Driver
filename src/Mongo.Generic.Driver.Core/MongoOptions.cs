using System;

namespace Mongo.Generic.Driver.Core
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }
    }

    internal static class MongoOptionsExtensions
    {
        public static void Guard(this MongoOptions mongoOptions)
        {
            if (string.IsNullOrEmpty(mongoOptions.ConnectionString))
                throw new NullReferenceException("Connection string is not provided!");
        }
    }
}
