using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GovOrdersApp.Data.Orders
{
    public class Order
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Industry { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        [BsonIgnoreIfNull]
        public string Description { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<OrderDocument> Documents { get; set; } = new List<OrderDocument>();

        [BsonIgnoreIfNull]
        public string Designer { get; set; }
        [BsonIgnoreIfNull]
        public string Builder { get; set; }
    }
}
