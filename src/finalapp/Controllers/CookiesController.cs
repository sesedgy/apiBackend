using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Versioning;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace finalapp.Controllers
{
    [Route("api/[controller]")]
    public class CookiesController : Controller
    {
        private readonly DataBaseContext _context;

        public CookiesController(DataBaseContext context)
        {
            _context = context;
        }

        // Запрос на аутентификацию: удаляет просроченные куки из бд и выдает новые
        // GET: api/Cookies
        [HttpGet]
        public Cookie Get(string login, string password, string oldCookie)
        {
            User correctUser = null;
            using (var context = _context)
            {
                correctUser = (from user in context.Users
                                  where user.Login == login && user.Password == password
                                  select user).SingleOrDefault();
            }
            if (correctUser != null)
            {
                if (oldCookie != null)
                {
                    using (var context = _context)
                    {
                        var deleteCookie = (from i in context.Cookies
                                       where i.UserId ==  new Guid(oldCookie)
                                       select i).SingleOrDefault();
                        if (deleteCookie != null){context.Cookies.Remove(deleteCookie);}
                    } 
                }
                var cookie = new Cookie() {Id = new Guid(), UserId = new Guid()};
                _context.Cookies.Add(cookie);
                _context.SaveChanges();
                return cookie;
            }
            else
            {
                return new Cookie()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000000"),
                    UserId = new Guid("00000000-0000-0000-0000-000000000000")
                };
            }
        }

    }
}
