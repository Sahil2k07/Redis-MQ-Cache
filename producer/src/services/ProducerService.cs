using producer.interfaces;
using producer.views;
using StackExchange.Redis;

namespace producer.services;

public class ProducerService : IProducerService
{
    private readonly ICacheService _cacheService;

    private readonly IQueueService _queueService;

    public ProducerService(ICacheService cacheService, IQueueService queueService)
    {
        _cacheService = cacheService;
        _queueService = queueService;
    }

    public async Task<CommonJsonResponse<long>> PublishMessageInQueue(object message)
    {
        var result = await _queueService.PublishMessage(message);

        return new CommonJsonResponse<long>(true, result);
    }

    public async Task<CommonJsonResponse<bool>> IsKeyPresent(string key)
    {
        var result = await _cacheService.IsKeyAvailable(key);

        return new CommonJsonResponse<bool>(true, result);
    }

    public async Task<CommonJsonResponse<string?>> GetKeyValue(string key)
    {
        var value = await _cacheService.GetKey(key);

        var message = "Cache Hit";

        if (value == RedisValue.Null)
        {
            // Simulating the case we don't get a value from the cache and now we have to perform a heavy computation
            await Task.Delay(3000);

            value = "NOT FOUND";

            message = "Cache Miss";
        }

        return new CommonJsonResponse<string?>(true, value, message);
    }

    public async Task<CommonJsonResponse<bool>> SetKey(KeyValuePair<string, object> payload)
    {
        var result = await _cacheService.SetKey(payload.Key, payload.Value);

        string message = result ? "Key-Value was set successfully" : "Key was not set";

        return new CommonJsonResponse<bool>(true, result, message);
    }

    public async Task<CommonJsonResponse<bool>> SetKeyWithExpiry(
        KeyValuePair<string, object> payload
    )
    {
        TimeSpan expiryTime = TimeSpan.FromSeconds(10);

        var result = await _cacheService.SetKeyWithExpiry(payload.Key, payload.Value, expiryTime);

        string message = result ? "Key-Value was set successfully" : "Key was not set";

        return new CommonJsonResponse<bool>(true, result, message);
    }

    public async Task<CommonJsonResponse<string?>> DeleteKey(string key)
    {
        var result = await _cacheService.RemoveKey(key);

        var message = result == RedisValue.Null ? "Key is not present" : "Key removed";

        return new CommonJsonResponse<string?>(true, result, message);
    }

    public async Task<CommonJsonResponse<dynamic>> ClearCache()
    {
        var result = await _cacheService.ClearCache();

        return new CommonJsonResponse<dynamic>(true, result, "Cache cleared");
    }
}
