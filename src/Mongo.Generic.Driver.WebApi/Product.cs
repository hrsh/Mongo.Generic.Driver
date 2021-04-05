using Marten.Generic.Driver.Core;
using Mongo.Generic.Driver.Core;

namespace Mongo.Generic.Driver.WebApi
{
    //public class Product : MongoEntityBase
    //{
    //    public string Name { get; set; }

    //    public int Price { get; set; }
    //}

    [DocumentName(nameof(Product))]
    public class Product : MongoEntityBase
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        //public string CoverImage { get; set; }

        //public virtual int CategoryId { get; set; }

        //public virtual Category Category { get; set; }
    }
}
