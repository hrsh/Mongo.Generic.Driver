using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.Generic.Driver.Core
{
    public class MongoEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }
    }
}
