using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Marten.Generic.Driver.Core
{
    public interface IMartenRepository<TEntity> where TEntity : IMartenEntity
    {
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken ct = default);

        List<TEntity> List<TKey>(
            Expression<Func<TEntity, TKey>> order,
            Expression<Func<TEntity, bool>> predicate);

        List<TEntity> List<TKey>(
            Expression<Func<TEntity, TKey>> order,
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize = 12);
    }
}
