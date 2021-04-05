using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mongo.Generic.Driver.Core
{
    public interface IMongoRepository<TEntity> where TEntity : MongoEntityBase
    {
        TEntity Find(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken ct = default);

        List<TEntity> List<TKey>(
            Expression<Func<TEntity, TKey>> order,
            DocumentSortOrder sortOrder,
            Expression<Func<TEntity, bool>> predicate = null);

        List<TEntity> List<TKey>(
            Expression<Func<TEntity, TKey>> order,
            DocumentSortOrder sortOrder,
            int pageIndex,
            int pageSize = 12,
            Expression<Func<TEntity, bool>> predicate = null);

        void Create(TEntity model);

        void Update(
            Expression<Func<TEntity, bool>> expression,
            TEntity model);

        void Delete(Expression<Func<TEntity, bool>> expression);
    }
}
