using MongoDB.Bson.Serialization.Attributes;

namespace GovOrdersApp.Data
{
    public class OrderDocument
    {
        [BsonId]
        public string Id { get; set; }
        public string Title { get; set; }
        [BsonIgnoreIfDefault]
        public string Description { get; set; }
        [BsonIgnoreIfDefault]
        public List<Comment> Comments { get; set; }
        public bool IsChecked { get; set; } = false;
        public bool IsRequired { get; set; } = false;
        public string OrderId { get; set; }
    }
}
