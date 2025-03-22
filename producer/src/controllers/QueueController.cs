using Microsoft.AspNetCore.Mvc;
using producer.interfaces;
using producer.views;

namespace producer.controllers;

[ApiController]
[Route("/api/queue")]
public class QueueController(IProducerService producerService) : Controller
{
    private readonly IProducerService _producerService = producerService;

    [HttpPost("push")]
    public async Task<CommonJsonResponse<long>> PublishMessageInQueue([FromBody] object payload)
    {
        return await _producerService.PublishMessageInQueue(payload);
    }
}
