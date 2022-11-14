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

        public override string IsValid()
        {
            if (OGRN == "" || OGRN.Length != 13) return "Неверный ОГРН!";
            if (INN == "" || INN.Length != 10) return "Неверный ИНН!";
            if (KPP == "" || KPP.Length != 9) return "Неверный КПП!";
            if (Manager == "") return "Неверное ФИО руководителя!";
            if (Address == "") return "Неверный адрес!";
            return base.IsValid();
        }
    }
}
