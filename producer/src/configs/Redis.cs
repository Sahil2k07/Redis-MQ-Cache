using producer.interfaces;
using StackExchange.Redis;

namespace producer.configs;

public class RedisService : IRedisService
{
    private readonly Lazy<ConnectionMultiplexer> _redis;

    public RedisService()
    {
        _redis = new Lazy<ConnectionMultiplexer>(GetRedisConfigs);
    }

    private ConnectionMultiplexer GetRedisConfigs()
    {
        var configs = new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" },
            User = "",
            Password = "",
        };

        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configs);

        return redis;
    }

    // Using [0, 1] for Queues
    public IDatabase GetQueuesDB() => _redis.Value.GetDatabase();

    // Using [2] for Cacheing
    public IDatabase GetCacheDB() => _redis.Value.GetDatabase(2);
}
