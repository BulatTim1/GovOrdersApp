using MongoDB.Bson.Serialization.Attributes;

namespace GovOrdersApp.Data.Users
{
    public class CustomerRole : AppUser
    {
        public string Industry { get; set; }
        [BsonIgnoreIfNull]
        public string Position { get; set; }

        public override string IsValid()
        {
            if (Industry == "") return "Неверная отрасль!";
            return base.IsValid();
        }
    }
}
