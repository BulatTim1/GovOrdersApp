using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Linq;

namespace GovOrdersApp.Data.Users
{
    [BsonIgnoreExtraElements]
    public class AppUser
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [BsonIgnoreIfNull]
        public string FullName { get; set; } = "User";

        [BsonIgnoreIfNull]
        public string Phone { get; set; }

        [BsonIgnoreIfNull]
        public string Token { get; set; }

        public virtual string IsValid()
        {
            if (Login == "") return "Неверный логин!";
            if (Email == "" || !Email.Contains('@')) return "Неверный email!";
            if (Password == "") return "Неверный пароль!";
            return "";
        }
    }
}
