namespace Marten.Generic.Driver.Core
{
    public class DefaultSessionFactory : ISessionFactory
    {
        private readonly IDocumentStore _store;

        public DefaultSessionFactory(IDocumentStore store)
        {
            _store = store;
        }

        public IDocumentSession OpenSession() =>
            _store.LightweightSession(System.Data.IsolationLevel.Serializable);

        public IQuerySession QuerySession() =>
            _store.QuerySession();
    }
}
