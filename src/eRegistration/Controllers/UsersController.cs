using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataBaseModel;
using DataBaseModel.Models;
using eRegistration.CommonServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eRegistration.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly DataBaseContext _context;
        private static string _localSalt = "54ef4305e43a47e2822823778f3d27708247";

        public UsersController(DataBaseContext context)
        {
            _context = context;
        }

        public bool Create(User user)
        {
            //Проверка на наличие такого аккаунта
            User userFromDb;
            using (var context = _context)
            {
                userFromDb = (from u in context.Users
                    where u.Login == user.Login
                    select u).SingleOrDefault();
            }
            if (userFromDb == null)
            {
                user.HashSalt = new Guid().ToString().Replace("-", "");
                var modifiedPassword = user.Password + user.HashSalt + _localSalt;
                using (var sha256 = SHA256.Create())
                {
                    // Send a sample text to hash.  
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(modifiedPassword));
                    // Get the hashed string.  
                    user.Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //TODO Шифрование пароля на клиенте
        //api/users/Authorization/login&password
        //correct: (string)guid
        //incorrect: null
        [HttpGet("{login}&{password}")]
        public string[] Authorization(string login, string password)
        {
            User user = (from u in _context.Users
                    where u.Login == login
                    select u).SingleOrDefault();
            if (user != null)
            {
                var modifiedPassword = password + user.HashSalt + _localSalt;
                using (var sha256 = SHA256.Create())
                {
                    // Send a sample text to hash.  
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(modifiedPassword));
                    // Get the hashed string.  
                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    if (user.Password == hash)
                    {
                        return CookieList.GetInstance().AddCookie(user);
                    }
                }
            }
            return null;
        }

        //api/users/isLoginAndEmailFree/login&email
        [HttpGet("{login}&{email}")]
        public bool[] IsLoginAndEmailFree(string login, string email)
        {
            User userFromDb = (from u in _context.Users
                    where u.Login == login
                    select u).SingleOrDefault();
            User userFromDb2 = (from u in _context.Users
                    where u.Email == email
                    select u).SingleOrDefault();
            var isLoginAndEmailFree = new bool[2];
            isLoginAndEmailFree[0] = userFromDb == null;
            isLoginAndEmailFree[1] = userFromDb2 == null;
            return isLoginAndEmailFree;
        }

        //api/users/CheckCookie/cookie&rights, 
        [HttpGet("{cookie}&{rights}")]
        public bool CheckCookie(string cookie, string rights)
        {
            return CookieList.GetInstance().CheckCookie(cookie, rights);
        }

        [HttpGet("{email}")]
        public bool ResetPassword(string email)
        {
            var user = (from u in _context.Users
                         where u.Email == email
                         select u).SingleOrDefault();
            var newPassword = RandomString.GetString(8);
            if (user != null)
            {
                try
                {
                    var modifiedPassword = newPassword + user.HashSalt + _localSalt;
                    string newPasswordhash;
                    using (var sha256 = SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(modifiedPassword));
                        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                        newPasswordhash = hash;
                    }
                    user.Password = newPasswordhash;
                    _context.SaveChanges();

                    string emailBody = "Здравствуйте! \n" +
                                       "Вы запросили восстановление логина и пароля от личного кабинета eAcademy \n" +
                                       "Логин от личного кабинета: " + user.Login + " Пароль: " + newPassword + " \n";
                    EmailService emailService = new EmailService();
                    var sendEmailTask = new Task(async () => await emailService.SendEmailAsync(user.Email, "Восстановление пароля", emailBody));
                    sendEmailTask.Start();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

    }
}
