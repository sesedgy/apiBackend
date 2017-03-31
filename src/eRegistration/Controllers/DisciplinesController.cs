using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseModel;
using DataBaseModel.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace eRegistration.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DisciplinesController : Controller
    {
        private readonly DataBaseContext _context;
        public DisciplinesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public Discipline Get(string id)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[]{ "IsWorker", "IsAdmin" })){return null;}
            Discipline discipline = (from u in _context.Discipline
                              where u.Id == new Guid(id)
                              select u).SingleOrDefault();
            return discipline;
        }

        [HttpGet("{id}")]
        public List<Discipline> GetAll()
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return null; }
            List<Discipline> disciplines = (from u in _context.Discipline
                               select u).ToList();
            return disciplines;
        }

        [HttpPost]
        public bool Update([FromBody] Discipline discipline)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return false; }
                            
            //TODO Может вылетать из-за того что discipline.Id не GUID    
            Discipline disciplineFromDb = (from u in _context.Discipline
                                    where u.Id == discipline.Id
                                    select u).SingleOrDefault();
            if (disciplineFromDb != null)
            {
                _context.Discipline.Remove(disciplineFromDb);
                //TODO Может вылетать если мы удаляем и сразу добавляем не сохранившись
                _context.Discipline.Add(discipline);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool Create([FromBody] Discipline discipline)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return false; }   

            //TODO Может вылетать из-за того что discipline.Id не GUID
            Discipline disciplineFromDb = (from u in _context.Discipline
                                    where u.Id == discipline.Id
                                    select u).SingleOrDefault();
            if (disciplineFromDb == null)
            {
                _context.Discipline.Add(discipline);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpGet("{id}")]
        public bool Delete(string id)
        {
            if (CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" })) { return false; }  
                         
            //TODO Может вылетать из-за того что discipline.Id не GUID
            Discipline disciplineFromDb = (from u in _context.Discipline
                                    where u.Id == new Guid(id)
                                    select u).SingleOrDefault();
            if (disciplineFromDb != null)
            {
                _context.Discipline.Remove(disciplineFromDb);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
