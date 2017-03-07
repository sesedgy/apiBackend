using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using finalapp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace finalapp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly DataBaseContext _context;
        private static string _localSalt= "54ef4305e43a47e2822823778f3d27708247";

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

        // GET: api/users/login=log&password=pass
        [HttpGet]
        public string Authorization(string login, string password)
        {
            User user;
            using (var context = _context)
            {
                user = (from u in context.Users
                        where u.Login == login
                        select u).SingleOrDefault();
            }
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

        // GET: api/users/login=log&email=email
        [HttpGet]
        public bool[] IsLoginAndEmailFree(string login, string email)
        {
            User userFromDb;
            User userFromDb2;
            using (var context = _context)
            {
                userFromDb = (from u in context.Users
                              where u.Login == login
                              select u).SingleOrDefault();
                userFromDb2 = (from u in context.Users
                              where u.Email == email
                              select u).SingleOrDefault();
            }
            var isLoginAndEmailFree = new bool[2];
            //isLoginAndEmailFree[0] = userFromDb != null;
            //isLoginAndEmailFree[1] = userFromDb2 != null;
            isLoginAndEmailFree[0] = userFromDb == null;
            isLoginAndEmailFree[1] = userFromDb2 == null;
            return isLoginAndEmailFree;
        }

        // GET: api/users/8e783c7e-717b-4efa-c067-08d455bbcfaa
        [HttpGet]
        public User Get(string id)
        {
            User user;
            using (var context = _context)
            {
                user = (from u in context.Users
                               where u.Id == new Guid(id)
                               select u).SingleOrDefault();
            }
            return user;
        }

        [HttpPost]
        public StatusCodeResult Update(string id, [FromBody]User user)
        {
            User userFromDb;
            using (var context = _context)
            {
                userFromDb = (from u in context.Users
                        where u.Id == new Guid(id)
                        select u).SingleOrDefault();
            }
            if (userFromDb != null)
            {
                _context.Remove(userFromDb);
                _context.Users.Add(user);
                return new StatusCodeResult(204);      
            }
            else
            {
                return new StatusCodeResult(404);       
            }
        }

        public StatusCodeResult Delete(string id)
        {
            User userFromDb;
            using (var context = _context)
            {
                userFromDb = (from u in context.Users
                              where u.Id == new Guid(id)
                              select u).SingleOrDefault();
            }
            if (userFromDb != null)
            {
                _context.Remove(userFromDb);
                return new StatusCodeResult(204);
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

    }
}
