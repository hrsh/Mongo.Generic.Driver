using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Redis.Cache.Driver
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _cache;
        private readonly IOptions<RedisOptions> _options;

        public RedisCache(
            IDistributedCache cache,
            IOptions<RedisOptions> options)
        {
            _cache = cache;
            _options = options;
        }

        public virtual async Task SetData<T>(
                    string recordId,
                    T data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _options.Value.AbsoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = _options.Value.SlidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(recordId, jsonData, options);
        }

        public virtual async Task<T> GetData<T>(string recordId)
        {
            var jsonData = await _cache.GetStringAsync(recordId);
            if (jsonData is null) return default;
            await _cache.RefreshAsync(recordId);
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public virtual async Task ClearData(string recordId)
        {
            await _cache.RemoveAsync(recordId);
        }
    }
}
