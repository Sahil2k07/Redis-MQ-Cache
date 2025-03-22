using producer.enums;
using producer.interfaces;
using StackExchange.Redis;

namespace producer.services;

public class QueueService(IRedisService redisService) : IQueueService
{
    private readonly IDatabase _queueDB = redisService.GetQueuesDB();

    public async Task<long> PublishMessage(object message)
    {
        return await _queueDB.ListLeftPushAsync(QueueKeys.USER_NAME.ToString(), message.ToString());
    }
}
