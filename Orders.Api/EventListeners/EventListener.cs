using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Orders.Api.Events;
using Orders.Api.Events.EventHandlers;
using Orders.Api.Models;
using StackExchange.Redis;
namespace Orders.Api.EventListeners
{
    public class EventListener(
        IDatabase redisDatabase,
        IOptions<RedisConfig> redisConfig,
        ILogger logger,
        IServiceProvider serviceProvider)
        : IEventListener
    {
        private readonly IDatabase _redisDatabase = redisDatabase;
        private readonly ILogger _logger = logger;
        private readonly IOptions<RedisConfig> _redisConfig = redisConfig;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task Listen(CancellationToken token)
        {
            try
            {
                var lastId = "_";
                while (!token.IsCancellationRequested)
                {
                    var result = await _redisDatabase.StreamRangeAsync(_redisConfig.Value.StreamName, lastId, "+");
                    if(!result.Any() || lastId==result.Last().Id)continue;
                    lastId = result.Last().Id;
                    foreach (var entry in result)
                    {
                        foreach (var field in entry.Values)
                        {
                            var type = Type.GetType(field.Name!);
                            var body = (IEvent)JsonConvert.DeserializeObject(field.Value!, type!)!;

                            var messageHandlerType = typeof(IEventHandler<>).MakeGenericType(type!);
                            using var scope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                            var handler = scope.ServiceProvider.GetRequiredService(messageHandlerType);

                            handler.GetType().GetMethod("HandleAsync", [type!])?.Invoke(handler, [body]);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "an error occured processing events.");
            }
        }
    }
}
