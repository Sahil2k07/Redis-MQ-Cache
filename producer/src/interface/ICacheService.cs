namespace producer.interfaces;

public interface ICacheService
{
    Task<bool> IsKeyAvailable(string key);

    Task<bool> SetKey(string key, object value);

    Task<bool> SetKeyWithExpiry(string key, object value, TimeSpan time);

    Task<string?> GetKey(string key);

    Task<string?> RemoveKey(string key);

    Task<dynamic> ClearCache();
}
