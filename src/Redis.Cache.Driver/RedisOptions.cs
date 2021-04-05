using System;

namespace Redis.Cache.Driver
{
    public class RedisOptions
    {
        public string Server { get; set; }

        public string Port { get; set; }

        public string InstanceName { get; set; }

        public string Password { get; set; }

        public bool Ssl { get; set; }

        public TimeSpan? AbsoluteExpireTime { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        internal string ConnectionString => $"{Server}:{Port}";
    }

    internal static class RedisOptionsExtensions
    {
        public static RedisOptions ValidateServer(this RedisOptions options)
        {
            if (string.IsNullOrEmpty(options.Server))
                throw new NullReferenceException($"Server address or name is not provided.");

            return options;
        }

        public static RedisOptions ValidatePortNumber(this RedisOptions options)
        {
            if (!int.TryParse(options.Port, out _))
                throw new NullReferenceException($"Port number is not provided or is invalid.");

            return options;
        }
    }
}
