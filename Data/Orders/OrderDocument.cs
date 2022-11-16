using MongoDB.Bson.Serialization.Attributes;

namespace GovOrdersApp.Data.Orders
{
    public class OrderDocument
    {
        [BsonId]
        public string Id { get; set; }
        public string Title { get; set; }
        public string FileId { get; set; }
        public bool IsChecked { get; set; } = false;
        public bool IsRequired { get; set; } = false;
        public string OrderId { get; set; }
    }
}
