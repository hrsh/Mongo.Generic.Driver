using Mongo.Generic.Driver.Core;

namespace Mongo.Generic.Driver.WebApi
{
    public class Product : MongoEntityBase
    {
        public string Name { get; set; }

        public int Price { get; set; }
    }
}
