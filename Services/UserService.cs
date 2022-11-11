using GovOrdersApp.Data.DB;
using GovOrdersApp.Data.Users;
using System.Security.Cryptography;
using System.Text;

namespace GovOrdersApp.Services
{
    public class UserService
    {
        static UsersContext _context = new UsersContext();
        static AppUser CurrentUser;
        static List<string> errors = new List<string>();
        public bool Authorization(string login, string password)
        {
            login = login.Trim();
            password = password.Trim();
            if (login == "" || password == "")
            {
                errors.Add("Поля не должны быть пустыми!");
                return false;
            }
            password = sha256_hash(password);
            var res = _context.Authenticate(login, password);
            if (res != null)
            {
                //res.CurrentUID = Guid.NewGuid().ToString();
                //_context.UpdateUser(res);
                CurrentUser = res;
                //IsAuthorizated = true;
                return true;
            } 
            else
            {
                errors.Add("Неверный логин или пароль");
            }
            return false;
        }

        public bool Registration(string login, string email, string password, string repassword, string role)
        {
            login = login.Trim();
            email = email.Trim();
            password = password.Trim();
            repassword = repassword.Trim();
            if (login == "" || password == "" || email == "" || repassword == "")
            {
                errors.Add("Поля не должны быть пустыми!");
                return false;
            }
            if (password != repassword)
            {
                errors.Add("Пароли не совпадают");
                return false;
            }
            password = sha256_hash(password);
            AppUser newUser;
            switch(role)
            {
                case "Customer":
                    newUser = new CustomerRole();
                    break;
                case "Builder":
                    newUser = new BuilderRole();
                    break;
                case "Designer":
                    newUser = new DesignerRole();
                    break;
                default:
                    errors.Add("Выберите роль!");
                    return false;
            }
            newUser.Login = login;
            newUser.Password = password;
            newUser.Email = email;
            try
            {
                _context.AddUser(newUser);
                CurrentUser = newUser;
                //res.CurrentUID = Guid.NewGuid().ToString();
                //_context.UpdateUser(res);
                //IsAuthorizated = true;
                return true;
            } catch
            {
                errors.Add("Такой пользователь уже существует!");
                return false;
            }
        }

        public AppUser GetCurrentUser()
        {
            return CurrentUser;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
        
        public string? GetUsername()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.Login;
            }
            return null;
        }

        public string GetRole()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.GetType().ToString().Split('.')[3];
            }
            return "Guest";
        }

        public List<string> GetErrors()
        {
            List<string> temp = new List<string>(errors);
            errors.Clear();
            return temp;
        }

        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
