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

        public AppUser GetUser(string id)
        {
            return _context.GetUser(id);
        }

        public List<AppUser> GetUsersByRole(string role)
        {
            return _context.GetUsersByRole(role);
        }
        
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
                res.Token = Guid.NewGuid().ToString();
                _context.UpdateUser(res);
                CurrentUser = res;
                return true;
            } 
            else
            {
                errors.Add("Неверный логин или пароль");
            }
            return false;
        }

        public bool Registration(string login, string email, string password, string repassword, string role, string industry = "")
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
                    if (industry == "")
                    {
                        errors.Add("Выберите отрасль!");
                        return false;
                    }
                    newUser = new CustomerRole()
                    {
                        Industry = industry,
                    };
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
                newUser.Token = Guid.NewGuid().ToString();
                _context.UpdateUser(newUser);
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

        public string GetToken()
        {
            return CurrentUser.Token;
        }

        public void Logout()
        {
            CurrentUser.Token = null;
            _context.UpdateUser(CurrentUser);
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

        public string GetIndustry()
        {
            if (CurrentUser != null && CurrentUser.GetType() == typeof(CustomerRole))
            {
                return ((CustomerRole)CurrentUser).Industry;
            }
            return null;
        }

        public string GetId()
        {
            if (CurrentUser != null)
            {
                return CurrentUser.Id;
            }
            return "";
        }

        public bool UpdateUser(AppUser user)
        {
            if (user.IsValid())
            {
                _context.UpdateUser(user);
                return true;
            }
            errors.Add("Введите верные значения");
            return false;
        }
        
        public bool UpdatePassword(AppUser user, string oldPassword, string newPassword, string renewPassword)
        {
            newPassword = newPassword.Trim();
            renewPassword = renewPassword.Trim();
            if (newPassword == "")
            {
                errors.Add("Пароль не должен быть пустым!");
                return false;
            }
            if (newPassword != renewPassword)
            {
                errors.Add("Пароли не совпадают");
                return false;
            }
            newPassword = sha256_hash(newPassword);
            oldPassword = sha256_hash(oldPassword);
            if (oldPassword != user.Password)
            {
                errors.Add("Неверный пароль");
                return false;
            }
            user.Password = newPassword;
            _context.UpdateUser(user);
            return true;
        }

        public bool CheckToken(string token)
        {
            if (CurrentUser != null || token != "")
            {
                CurrentUser = _context.CheckToken(token);
                if (CurrentUser != null)
                {
                    return true;
                }
            }
            return false;
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
