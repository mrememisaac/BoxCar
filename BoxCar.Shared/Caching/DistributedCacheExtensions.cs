using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BoxCar.Shared.Caching
{
    public static class DistributedCacheExtensions
    {
        public async static Task<T> GetFromCache<T>(this IDistributedCache cache, string key) where T : class
        {
            T data = null;
            var encodedData = await cache.GetAsync(key);

            if (encodedData != null)
            {
                data = JsonSerializer.Deserialize<T>(encodedData);
            }
            return data;
        }

        public async static Task<T> SaveToCache<T>(this IDistributedCache cache, string key, T data, int slidingExpirationSeconds = 20, int absoluteExpirationSeconds = 100) where T : class
        {
            if (data == null) return data;
            byte[] dataByteArray = JsonSerializer.SerializeToUtf8Bytes(data);


            var distCacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationSeconds))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(absoluteExpirationSeconds));

            await cache.SetAsync(key, dataByteArray, distCacheOptions);
            return data;
        }
    }

}