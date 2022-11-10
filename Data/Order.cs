using MongoDB.Bson.Serialization.Attributes;

namespace GovOrdersApp.Data
{
    public class Order
    {
        [BsonId]
        public string Id { get; set; }

        public string Industry { get; set; }
        
        public string Author { get; set; }

        public string Title { get; set; }

        [BsonIgnoreIfNull]
        public string Description { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<OrderDocument> Documents { get; set; } = new List<OrderDocument>();

        [BsonIgnoreIfNull]
        public string DesignerId { get; set; }
        [BsonIgnoreIfNull]
        public string BuilderId { get; set; }
    }
}
