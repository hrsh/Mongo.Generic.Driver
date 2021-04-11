using System;

namespace Mongo.Generic.Driver.Core
{
    public class MongoOptions
    {
        [Obsolete(message: "This property is obsolete and will remove in next release, please use Host instead!")]
        public string ConnectionString { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string DbUser { get; set; }

        public string DbPassword { get; set; }

        [Obsolete(message: "This property is obsolete and will remove in next release, please use Host instead!")]
        public string Database { get; set; }

        public string SecuredConnectionString => $"mongodb://{DbUser}:{DbPassword}@{Host}:{Port}";

        public string NonSecuredConnectionString => $"mongodb://{Host}:{Port}";
    }

    internal static class MongoOptionsExtensions
    {
        public static void Guard(this MongoOptions mongoOptions)
        {
            if (string.IsNullOrEmpty(mongoOptions.Host))
                throw new NullReferenceException("The host is not provided!");

            if (string.IsNullOrEmpty(mongoOptions.Port))
                throw new NullReferenceException("The port number is not provided!");
        }
    }
}
