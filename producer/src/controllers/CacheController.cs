using Microsoft.AspNetCore.Mvc;
using producer.interfaces;
using producer.views;

namespace producer.controllers;

[ApiController]
[Route("/api/cache")]
public class CacheController(IProducerService producerService) : Controller
{
    private readonly IProducerService _producerService = producerService;

    [HttpGet("check/{key}")]
    public async Task<CommonJsonResponse<bool>> IsKeyPresent([FromRoute] string key)
    {
        return await _producerService.IsKeyPresent(key);
    }

    [HttpDelete("clear")]
    public async Task<CommonJsonResponse<dynamic>> ClearCache()
    {
        return await _producerService.ClearCache();
    }

    [HttpDelete]
    public async Task<CommonJsonResponse<string?>> RemoveKey([FromQuery] string key)
    {
        return await _producerService.DeleteKey(key);
    }

    [HttpGet]
    public async Task<CommonJsonResponse<string?>> GetKey([FromQuery] string key)
    {
        return await _producerService.GetKeyValue(key);
    }

    [HttpPost]
    public async Task<CommonJsonResponse<bool>> SetKey(
        [FromBody] KeyValuePair<string, object> payload
    )
    {
        return await _producerService.SetKey(payload);
    }

    [HttpPost("expiry")]
    public async Task<CommonJsonResponse<bool>> SetKeyWithExpiry(
        [FromBody] KeyValuePair<string, object> payload
    )
    {
        return await _producerService.SetKeyWithExpiry(payload);
    }
}
