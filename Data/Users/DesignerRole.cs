using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GovOrdersApp.Data.Users
{
    public class DesignerRole : AppUser
    {
        [BsonIgnoreIfNull]
        public string OGRN { get; set; }
        [BsonIgnoreIfNull]
        public string INN { get; set; }
        [BsonIgnoreIfNull]
        public string KPP { get; set; }
        [BsonIgnoreIfNull]
        public string Director { get; set; }
        [BsonIgnoreIfNull]
        public string Address { get; set; }
        [BsonIgnoreIfNull]
        public string GIP { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() && OGRN != "" && OGRN.Length == 13 && INN != "" && INN.Length == 10 &&
                KPP != "" && KPP.Length == 9 && Director != "" && Address != "" && GIP != "";
        }
    }
}
