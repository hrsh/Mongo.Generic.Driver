using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
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
            var connection = await GetConnection();
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

        private async Task<IEventStoreConnection> GetConnection()
        {
            var settings = ConnectionSettings
                .Create()
                .UseConsoleLogger()
                .EnableVerboseLogging()
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"))
                .Build();

            var connection = EventStoreConnection.Create(
                settings,
                new Uri(_options.ConnectionString),
                "Mazdak");
            await connection.ConnectAsync();
            connection.Connected += (s, e) =>
            {
                _logger.LogInformation(e.RemoteEndPoint.ToString());
            };
            connection.AuthenticationFailed += (s, e) =>
            {
                _logger.LogCritical($"Authentication failed => {e.Reason}.");
            };
            connection.Closed += (s, e) =>
            {
                _logger.LogCritical($"Connection closed => {e.Reason}.");
            };
            return connection;
        }
    }
}
