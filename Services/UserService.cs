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

        public AppUser? GetUser(string id)
        {
            if (id != "") return _context.GetUser(id);
            return null;
        }

        public List<AppUser> GetUsersByRole(string role)
        {
            return _context.GetUsersByRole(role);
        }

        public string Authorization(string login, string password)
        {
            login = login.Trim();
            password = password.Trim();
            if (login == "" || password == "")
            {
                return "Поля не должны быть пустыми!";
            }
            password = sha256_hash(password);
            var res = _context.Authenticate(login, password);
            if (res != null)
            {
                res.Token = Guid.NewGuid().ToString();
                _context.UpdateUser(res);
                CurrentUser = res;
                return "";
            }
            return "Неверный логин или пароль";
        }

        public string Registration(string login, string email, string password, string repassword, string role, string industry = "")
        {
            login = login.Trim();
            email = email.Trim();
            password = password.Trim();
            repassword = repassword.Trim();
            if (login == "" || password == "" || email == "" || repassword == "")
            {
                return "Поля не должны быть пустыми!";
            }
            if (password != repassword)
            {
                return "Пароли не совпадают";
            }
            password = sha256_hash(password);
            AppUser newUser;
            switch (role)
            {
                case "Customer":
                    if (industry == "")
                    {
                        return "Выберите отрасль!";
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
                    return "Выберите роль!";
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
                return "";
            }
            catch
            {
                return "Такой пользователь уже существует!";
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

        public string UpdateUser(AppUser user)
        {
            string error = user.IsValid();
            if (error == "")
            {
                _context.UpdateUser(user);
                return "";
            }
            return error;
        }

        public string UpdatePassword(AppUser user, string newPassword, string renewPassword)
        {
            newPassword = newPassword.Trim();
            renewPassword = renewPassword.Trim();
            if (newPassword == "")
            {
                return "Пароль не должен быть пустым!";
            }
            if (newPassword != renewPassword)
            {
                return "Пароли не совпадают";
            }
            newPassword = sha256_hash(newPassword);
            user.Password = newPassword;
            _context.UpdateUser(user);
            return "";
        }

        public string UpdatePassword(AppUser user, string oldPassword, string newPassword, string renewPassword)
        {
            newPassword = newPassword.Trim();
            renewPassword = renewPassword.Trim();
            if (newPassword == "")
            {
                return "Пароль не должен быть пустым!";
            }
            if (newPassword != renewPassword)
            {
                return "Пароли не совпадают";
            }
            newPassword = sha256_hash(newPassword);
            oldPassword = sha256_hash(oldPassword);
            if (oldPassword != user.Password)
            {
                return "Неверный пароль";
            }
            user.Password = newPassword;
            _context.UpdateUser(user);
            return "";
        }

        public bool CheckToken(string token)
        {
            if (token != null && token != "")
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
