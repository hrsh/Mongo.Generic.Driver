using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorRedisCache
{
    public static class Extensions
    {
        public static async Task SetData<T>(
            this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? expireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = expireTime
            };

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);

            //var olderVersion = await cache.GetData<T>(recordId);
        }

        public static async Task<T> GetData<T>(
            this IDistributedCache cache,
            string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData is null) return default;
            await cache.RefreshAsync(recordId);
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public static async Task ClearData(
            this IDistributedCache cache,
            string recordId)
        {
            await cache.RemoveAsync(recordId);
        }
    }
}
