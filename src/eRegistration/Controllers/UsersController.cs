using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataBaseModel;
using DataBaseModel.Models;
using eRegistration.CommonServices;
using Microsoft.AspNetCore.Mvc;

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
            User userFromDb = (from u in _context.Users
                              where u.Login == user.Login
                              select u).SingleOrDefault();
            
            if (userFromDb == null)
            {
                user.HashSalt = Guid.NewGuid().ToString().Replace("-", "");
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

        public User GetUser(Guid id)
        {
            return (from u in _context.Users
                    where u.UserId == id
                    select u).SingleOrDefault();
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

        [HttpGet("{userName}")]
        public string[] GetUserInfo(string userName)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin", "IsTeacher", "IsStudentLeader", "IsStudent", "IsAbiturient" }))
            {
                return null;
            }

            var user = (from u in _context.Users
                    where u.Login == userName
                    select u).SingleOrDefault();
            if (user != null)
            {
                var userMassive = new string[3];
                userMassive[0] = user.Login;
                userMassive[1] = user.Email;
                switch (user.Role)
                {
                    case "IsAbiturient":
                        userMassive[2] = "Абитуриент";
                        break;
                    case "IsStudent":
                        userMassive[2] = "Студент";
                        break;
                    case "IsStudentLeader":
                        userMassive[2] = "Староста";
                        break;
                    case "IsTeacher":
                        userMassive[2] = "Преподаватель";
                        break;
                    case "IsWorker":
                        userMassive[2] = "Работник";
                        break;
                    case "IsAdmin":
                        userMassive[2] = "Администратор";
                        break;
                }
                return userMassive;
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

        [HttpGet("{login}&{newEmail}")]
        public IActionResult ChangeEmail(string login, string newEmail)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin", "IsTeacher", "IsStudentLeader", "IsStudent", "IsAbiturient" }))
            {
                return Unauthorized();
            }
            if (CookieList.GetInstance().GetUser(Request).Login != login)
            {
                return BadRequest();
            }

            User userFromDb = (from u in _context.Users
                               where u.Login == login
                               select u).SingleOrDefault();
            if (userFromDb != null)
            {
                userFromDb.Email = newEmail;
                _context.Update(userFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{email}&{newLogin}")]
        public IActionResult ChangeLogin(string email, string newLogin)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin", "IsTeacher", "IsStudentLeader", "IsStudent", "IsAbiturient" }))
            {
                return Unauthorized();
            }
            if (CookieList.GetInstance().GetUser(Request).Email != email)
            {
                return BadRequest();
            }

            User userFromDb = (from u in _context.Users
                               where u.Email == email
                               select u).SingleOrDefault();
            if (userFromDb != null)
            {
                userFromDb.Login = newLogin;
                _context.Update(userFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }

        [HttpGet("{login}&{newPassword}")]
        public IActionResult ChangePassword(string login, string newPassword)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin", "IsTeacher", "IsStudentLeader", "IsStudent", "IsAbiturient" }))
            {
                return Unauthorized();
            }
            if (CookieList.GetInstance().GetUser(Request).Login != login)
            {
                return BadRequest();
            }

            User userFromDb = (from u in _context.Users
                               where u.Login == login
                               select u).SingleOrDefault();
            if (userFromDb != null)
            {
                try
                {
                    var modifiedPassword = newPassword + userFromDb.HashSalt + _localSalt;
                    string newPasswordhash;
                    using (var sha256 = SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(modifiedPassword));
                        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                        newPasswordhash = hash;
                    }
                    userFromDb.Password = newPasswordhash;
                    _context.SaveChanges();

                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }


    }
}