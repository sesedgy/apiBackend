using System;
using System.Linq;
using finalapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalapp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AbiturientsController : Controller
    {
        private readonly DataBaseContext _context;

        public AbiturientsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/abiturients/get/id
        [HttpGet("{id}")]
        public Abiturient Get(string id)
        {
            Abiturient abiturient;
            using (var context = _context)
            {
                abiturient = (from u in context.Abiturients
                    where u.Id == new Guid(id)
                    select u).SingleOrDefault();
            }
            return abiturient;
        }

        // POST: api/abiturients/create
        [HttpPost]
        public RedirectResult Create([FromBody] object[] userAndAbiturient)
        {
            //TODO Отправка письма на почту
            //TODO Сохранение фотки         
            var user = (User)userAndAbiturient[0];
            var abiturient = (Abiturient)userAndAbiturient[1];
            var photo = (IFormFile)userAndAbiturient[2];
            user.Role = "IsAbiturient";
            try
            {
                var usersController = new UsersController(_context);
                if (!usersController.Create(user))

                _context.Abiturients.Add(abiturient);
                _context.SaveChanges();
                //TODO ссылку на страницу академии
                return new RedirectResult("https://www.google.ru/");
            }
            catch (Exception)
            {
                //TODO ссылку на страницу академии c ошибкой и просьбой пройти регистрацию еще раз 
                return new RedirectResult("https://www.google.ru/");
            }
        }

        // POST: api/users/8e783c7e-717b-4efa-c067-08d455bbcfaa
        //[HttpPost("{id}")]
        //public StatusCodeResult Update(string id, [FromBody]Abiturient abiturient)
        //{
        //    User userFromDb;
        //    using (var context = _context)
        //    {
        //        userFromDb = (from u in context.Users
        //                      where u.Id == new Guid(id)
        //                      select u).SingleOrDefault();
        //    }
        //    if (userFromDb != null)
        //    {
        //        _context.Remove(userFromDb);
        //        _context.Users.Add(userFromDb);
        //        return new StatusCodeResult(204);
        //    }
        //    else
        //    {
        //        return new StatusCodeResult(404);
        //    }
        //}

        // DELETE: api/users/8e783c7e-717b-4efa-c067-08d455bbcfaa
        //[HttpDelete("{id}")]
        //public StatusCodeResult Delete(string id)
        //{
        //    User userFromDb;
        //    using (var context = _context)
        //    {
        //        userFromDb = (from u in context.Users
        //                      where u.Id == new Guid(id)
        //                      select u).SingleOrDefault();
        //    }
        //    if (userFromDb != null)
        //    {
        //        _context.Remove(userFromDb);
        //        return new StatusCodeResult(204);
        //    }
        //    else
        //    {
        //        return new StatusCodeResult(404);
        //    }
        //}
    }
}
