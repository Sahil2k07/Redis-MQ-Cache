using producer.interfaces;
using StackExchange.Redis;

namespace producer.services;

public class CacheService(IRedisService redisService) : ICacheService
{
    private readonly IDatabase _cacheDB = redisService.GetCacheDB();

    public async Task<bool> IsKeyAvailable(string key) => await _cacheDB.KeyExistsAsync(key);

    public async Task<bool> SetKey(string key, object value)
    {
        return await _cacheDB.StringSetAsync(key, value.ToString());
    }

    public async Task<bool> SetKeyWithExpiry(string key, object value, TimeSpan time)
    {
        return await _cacheDB.StringSetAsync(key, value.ToString(), time);
    }

    public async Task<string?> GetKey(string key) => await _cacheDB.StringGetAsync(key);

    public async Task<string?> RemoveKey(string key) => await _cacheDB.StringGetDeleteAsync(key);

    public async Task<dynamic> ClearCache() => await _cacheDB.ExecuteAsync("FLUSHDB");
}
