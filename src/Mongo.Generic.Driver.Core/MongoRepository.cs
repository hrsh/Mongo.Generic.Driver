using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mongo.Generic.Driver.Core
{
    public class MongoRepository<TEntity>
        : IMongoRepository<TEntity>
        where TEntity : MongoEntityBase
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepository(IOptions<MongoOptions> options)
        {
            if (options.Value is null) throw new NullReferenceException();

            options.Value.Guard();

            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.Database);

            _collection = database.GetCollection<TEntity>(options.Value.Document);
        }

        public virtual void Create(TEntity model)
            => _collection.InsertOne(model);

        public virtual void Delete(Expression<Func<TEntity, bool>> expression)
            => _collection.DeleteOne(expression);

        public virtual TEntity Find(Expression<Func<TEntity, bool>> expression) =>
            _collection.Find(expression).FirstOrDefault();

        public virtual async Task<TEntity> FindAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken ct = default) =>
            await _collection.Find(expression).FirstOrDefaultAsync(ct);

        public virtual List<TEntity> List<TKey>(Expression<Func<TEntity, TKey>> order) =>
            _collection
                .AsQueryable()
                .OrderBy(order)
                .ToList();

        public virtual List<TEntity> List<TKey>(
            Expression<Func<TEntity, TKey>> order, 
            int pageIndex, 
            int pageSize = 12)
        {
            var t = _collection
                .AsQueryable()
                .OrderBy(order)
                .Skip(SkipCount())
                .Take(PageSize())
                .ToList();

            int PageSize() => pageSize <= 0 ? 1 : pageSize;
            int SkipCount() => ((pageIndex <= 1 ? 1 : pageIndex) - 1) * PageSize();

            return t;
        }

        public virtual void Update(Expression<Func<TEntity, bool>> expression, TEntity model) =>
            _collection.ReplaceOne(expression, model);
    }
}
