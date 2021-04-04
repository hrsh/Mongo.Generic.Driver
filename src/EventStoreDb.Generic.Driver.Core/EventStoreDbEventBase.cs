using System;

namespace EventStoreDb.Generic.Driver.Core
{
    public abstract class EventStoreDbEventBase
    {
        public virtual Guid EventId { get; set; }

        public virtual string Data { get; set; }

        public virtual string MetaData { get; set; }

        public string StreamName { get; set; }

        public string EventType { get; set; }
    }
}
