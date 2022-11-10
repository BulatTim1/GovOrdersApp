using MongoDB.Bson.Serialization.Attributes;
using System.Linq;

namespace GovOrdersApp.Data
{
    [BsonIgnoreExtraElements]
    public class AppUser
    {
        [BsonId]
        public string Id { get; set; }

        public string Login { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }

        [BsonElement("_t")]
        public string Role { get; set; }
        
        [BsonIgnoreIfNull]
        public string FullName { get; set; }
        
        [BsonIgnoreIfNull]
        public string Phone { get; set; }

        public virtual bool IsValid()
        {
            return Login != "" && Email != "" && Email.Contains('@') && Password != "" &&
                Role != "" && Role != "AppUser";
        }
    }
}
