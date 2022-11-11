using GovOrdersApp.Data.Users;
using MongoDB.Driver;

namespace GovOrdersApp.Data.DB
{
    public class UsersContext
    {
        private IMongoCollection<AppUser> users = DBConnection.users;

        public AppUser GetUser(string id)
        {
            return users.Find(user => user.Id == id).FirstOrDefault();
        }

        public AppUser GetUserByEmail(string email)
        {
            return users.Find(user => user.Email == email).FirstOrDefault();
        }

        public void AddUser(AppUser user)
        {
            users.InsertOne(user);
        }

        public void UpdateUser(AppUser user)
        {
            users.ReplaceOne(u => u.Id == user.Id, user);
        }

        public void DeleteUser(string id)
        {
            users.DeleteOne(user => user.Id == id);
        }

        public void DeleteUserByEmail(string email)
        {
            users.DeleteOne(user => user.Email == email);
        }

        public List<AppUser> GetAllUsers()
        {
            return users.Find(user => true).ToList();
        }

        public AppUser Authenticate(string login, string password)
        {
            return users.Find(user => user.Login == login && user.Password == password).FirstOrDefault();
        }
    }
}
