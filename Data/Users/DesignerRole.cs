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

        public override string IsValid()
        {
            if (OGRN == "" || OGRN.Length != 13) return "Неверный ОГРН!";
            if (INN == "" || INN.Length != 10) return "Неверный ИНН!";
            if (KPP == "" || KPP.Length != 9) return "Неверный КПП!";
            if (Director == "") return "Неверное ФИО директора!";
            if (GIP == "") return "Неверное ФИО главного инженера!";
            if (Address == "") return "Неверный адрес!";
            return base.IsValid();
        }
    }
}
