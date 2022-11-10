using MongoDB.Bson.Serialization.Attributes;

namespace GovOrdersApp.Data
{
    public class CustomerRole : AppUser
    {
        public string Industry { get; set; }
        [BsonIgnoreIfNull]
        public string Position { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() && Industry != "";
        }
    }
}
