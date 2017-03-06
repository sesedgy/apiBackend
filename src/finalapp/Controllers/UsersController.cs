using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using finalapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace finalapp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly DataBaseContext _context;

        public  UsersController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/users/login=log&password=pass
        [HttpPost]
        public string Get(string login, string password)
        {
            User user;
            using (var context = _context)
            {
                user = (from u in context.Users
                        where u.Login == login
                        where u.Password == password
                        select u).SingleOrDefault();
            }
            if (user != null)
            {
                return CookieList.GetInstance().AddCookie(user);
            }
            return "null";
        }

        // GET: api/users/8e783c7e-717b-4efa-c067-08d455bbcfaa
        [HttpGet("{id}")]
        public User Get(string id)
        {
            var user = new User();
            using (var context = _context)
            {
                user = (from u in context.Users
                               where u.Id == new Guid(id)
                               select u).SingleOrDefault();
            }
            return user;
        }

        // POST: api/users
        [HttpPost]
        public StatusCodeResult Create(User user)
        {
            try
            {
                user.Id = new Guid();
                _context.Users.Add(user);
                _context.SaveChanges();
                return new StatusCodeResult(204);
            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }
        }

        // POST: api/users/8e783c7e-717b-4efa-c067-08d455bbcfaa
        [HttpPost("{id}")]
        public StatusCodeResult Update(string id, [FromBody]User user)
        {
            User userFromDB = null;
            using (var context = _context)
            {
                userFromDB = (from u in context.Users
                        where u.Id == new Guid(id)
                        select u).SingleOrDefault();
            }
            if (userFromDB != null)
            {
                _context.Remove(userFromDB);
                _context.Users.Add(user);
                return new StatusCodeResult(204);      
            }
            else
            {
                return new StatusCodeResult(404);       
            }
        }

        // DELETE: api/users/8e783c7e-717b-4efa-c067-08d455bbcfaa
        [HttpDelete("{id}")]
        public StatusCodeResult Delete(string id)
        {
            User userFromDB = null;
            using (var context = _context)
            {
                userFromDB = (from u in context.Users
                              where u.Id == new Guid(id)
                              select u).SingleOrDefault();
            }
            if (userFromDB != null)
            {
                _context.Remove(userFromDB);
                return new StatusCodeResult(204);
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

    }
}
