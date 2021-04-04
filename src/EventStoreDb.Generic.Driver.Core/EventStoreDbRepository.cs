using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreDb.Generic.Driver.Core
{
    public class EventStoreDbRepository<TEvent> : IEventStoreDbRepository<TEvent>
        where TEvent : EventStoreDbEventBase
    {
        private readonly EventStoreDbOptions _options;
        private readonly ILogger<Core.EventStoreDbRepository<TEvent>> _logger;

        public EventStoreDbRepository(
            IOptions<EventStoreDbOptions> options,
            ILogger<EventStoreDbRepository<TEvent>> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task AppendAsync(TEvent @event, CancellationToken ct = default)
        {
            var connection = await Connection();
            _logger.LogInformation("Start EventStoreDb connection ...");

            var eventPayload = new EventData(
                eventId: @event.EventId,
                type: @event.EventType,
                isJson: _options.IsJson,
                data: Encoding.UTF8.GetBytes(@event.Data),
                metadata: Encoding.UTF8.GetBytes(@event.MetaData));
            try
            {
                var writeResult = await connection
                        .AppendToStreamAsync(@event.StreamName, ExpectedVersion.Any, eventPayload);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
            }

        }

        private async Task<IEventStoreConnection> Connection()
        {
            var settings = ConnectionSettings.Create();
            settings.UseConsoleLogger();
            settings.EnableVerboseLogging();
            settings.SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));
            var connection = EventStoreConnection.Create(
                settings.Build(),
                new Uri(_options.ConnectionString),
                nameof(EventStoreDb));
            await connection.ConnectAsync();
            return connection;
        }
    }
}
