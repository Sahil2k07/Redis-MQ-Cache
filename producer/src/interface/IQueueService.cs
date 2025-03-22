namespace producer.interfaces;

public interface IQueueService
{
    Task<long> PublishMessage(object message);
}
