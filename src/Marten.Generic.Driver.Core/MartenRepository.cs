using Marten.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Marten.Generic.Driver.Core
{
    public class MartenRepository<TEntity> :
        IMartenRepository<TEntity>
        where TEntity : IMartenEntity
    {
        private readonly IDocumentStore _store;

        private IDocumentSession _session;

        public MartenRepository(IDocumentStore store)
        {
            _store = store;
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            using var session = _store.LightweightSession();
            var t = session.Query<TEntity>().FirstOrDefault(predicate);
            return t;
        }

        public async Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken ct = default)
        {
            using var session = _store.LightweightSession();
            var t = await session.Query<TEntity>().FirstOrDefaultAsync(predicate, ct);
            return t;
        }

        public List<TEntity> List<TKey>(
            Expression<Func<TEntity, TKey>> order, 
            Expression<Func<TEntity, bool>> predicate)
        {
            using var session = _store.LightweightSession();
            var t = session.Query<TEntity>().Where(predicate).OrderBy(order).ToList();
            return t;
        }

        public List<TEntity> List<TKey>(
            Expression<Func<TEntity, TKey>> order, 
            Expression<Func<TEntity, bool>> predicate, 
            int pageIndex, 
            int pageSize = 12)
        {
            using var session = _store.LightweightSession();
            var t = session
                .Query<TEntity>()
                .Where(predicate)
                .OrderBy(order)
                .Skip(SkipCount())
                .Take(PageSize())
                .ToList();
            int PageSize() => pageSize <= 0 ? 1 : pageSize;
            int SkipCount() => ((pageIndex <= 1 ? 1 : pageIndex) - 1) * PageSize();
            return t;
        }
    }
}
