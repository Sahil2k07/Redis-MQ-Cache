using producer.configs;
using producer.interfaces;
using producer.middlewares;
using producer.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddSingleton<IRedisService, RedisService>();

builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IProducerService, ProducerService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHttpMiddleware();
app.MapControllers();

app.Run();
