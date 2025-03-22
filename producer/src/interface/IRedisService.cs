using StackExchange.Redis;

namespace producer.interfaces;

public interface IRedisService
{
    IDatabase GetQueuesDB();

    IDatabase GetCacheDB();
}
