using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseModel;
using DataBaseModel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eRegistration.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FacultiesController : Controller
    {
        private readonly DataBaseContext _context;
        public FacultiesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public Faculty Get(string id)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[]{ "IsWorker", "IsAdmin" })){return null;}
            Faculty faculty = (from u in _context.Faculty
                              where u.Id == new Guid(id)
                              select u).SingleOrDefault();
            return faculty;
        }

        [HttpGet("{id}")]
        public List<Faculty> GetAll()
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return null; }
            List<Faculty> faculties = (from u in _context.Faculty
                               select u).ToList();
            return faculties;
        }

        [HttpPost]
        public bool Update([FromBody] Faculty faculty)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return false; }
            Faculty facultyFromDb = (from u in _context.Faculty
                                    where u.Id == faculty.Id
                                    select u).SingleOrDefault();
            if (facultyFromDb != null)
            {
                _context.Faculty.Remove(facultyFromDb);
                //TODO Может вылетать если мы удаляем и сразу добавляем не сохранившись
                _context.Faculty.Add(faculty);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool Create([FromBody] Faculty faculty)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return false; }
            Faculty facultyFromDb = (from u in _context.Faculty
                                    where u.Id == faculty.Id
                                    select u).SingleOrDefault();
            if (facultyFromDb == null)
            {
                _context.Faculty.Add(faculty);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpGet("{id}")]
        public bool Delete(string id)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return false; }
            Faculty facultyFromDb = (from u in _context.Faculty
                                    where u.Id == new Guid(id)
                                    select u).SingleOrDefault();
            if (facultyFromDb != null)
            {
                _context.Faculty.Remove(facultyFromDb);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
