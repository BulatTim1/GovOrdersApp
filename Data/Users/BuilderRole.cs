using MongoDB.Bson.Serialization.Attributes;

namespace GovOrdersApp.Data.Users
{
    public class BuilderRole : AppUser
    {
        [BsonIgnoreIfNull]
        public string OGRN { get; set; }
        [BsonIgnoreIfNull]
        public string INN { get; set; }
        [BsonIgnoreIfNull]
        public string KPP { get; set; }
        [BsonIgnoreIfNull]
        public string Manager { get; set; }
        [BsonIgnoreIfNull]
        public string Address { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() && OGRN != "" && OGRN.Length == 13 && INN != "" && INN.Length == 10 &&
                KPP != "" && KPP.Length == 9 && Manager != "" && Address != "";
        }
    }
}
