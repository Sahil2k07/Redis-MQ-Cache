using producer.views;

namespace producer.interfaces;

public interface IProducerService
{
    Task<CommonJsonResponse<long>> PublishMessageInQueue(object message);

    Task<CommonJsonResponse<bool>> IsKeyPresent(string key);

    Task<CommonJsonResponse<string?>> GetKeyValue(string key);

    Task<CommonJsonResponse<bool>> SetKey(KeyValuePair<string, object> payload);

    Task<CommonJsonResponse<bool>> SetKeyWithExpiry(KeyValuePair<string, object> payload);

    Task<CommonJsonResponse<string?>> DeleteKey(string key);

    Task<CommonJsonResponse<dynamic>> ClearCache();
}
