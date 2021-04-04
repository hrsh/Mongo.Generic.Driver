using System.Threading;
using System.Threading.Tasks;

namespace EventStoreDb.Generic.Driver.Core
{
    public interface IEventStoreDbRepository<TEvent>
        where TEvent : EventStoreDbEventBase
    {
        Task AppendAsync(TEvent @event, CancellationToken ct = default);
    }
}
