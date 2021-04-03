using Microsoft.Extensions.Options;
using Mongo.Generic.Driver.Core;
using System;
using System.Linq.Expressions;

namespace Mongo.Generic.Driver.WebApi
{
    public class CustomRepo : MongoRepository<Product>
    {
        public CustomRepo(IOptions<MongoOptions> options) : base(options)
        {
        }

        public override Product Find(Expression<Func<Product, bool>> expression)
        {
            return base.Find(expression);
        }
    }
}
