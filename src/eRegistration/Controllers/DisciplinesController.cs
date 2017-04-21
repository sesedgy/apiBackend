using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseModel;
using DataBaseModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return null;
            }
            Discipline discipline = (from u in _context.Discipline
                where u.DisciplineId == new Guid(id)
                select u).SingleOrDefault();
            return discipline;
        }

        [HttpGet]
        public string GetAll()
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return null;
            }
            List<Discipline> disciplines = (from u in _context.Discipline
                select u).Include(u => u.Faculty).ToList();

            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            string json = JsonConvert.SerializeObject(disciplines, Formatting.Indented, serializerSettings);

            return json;
        }

        [HttpPost]
        public IActionResult Update([FromBody] Discipline discipline)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return Unauthorized();
            }

            Discipline disciplineFromDb = (from u in _context.Discipline
                where u.DisciplineId == discipline.DisciplineId
                select u).SingleOrDefault();
            if (disciplineFromDb != null)
            {
                _context.Discipline.Remove(disciplineFromDb);
                _context.SaveChanges();
                _context.Discipline.Add(discipline);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Discipline discipline)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return Unauthorized();
            }

            Discipline disciplineFromDb = (from u in _context.Discipline
                where u.DisciplineId == discipline.DisciplineId
                select u).SingleOrDefault();
            Faculty facultyFromDb = (from u in _context.Faculty
                where u.FacultyId == discipline.Faculty.FacultyId
                select u).SingleOrDefault();
            if (disciplineFromDb == null)
            {
                _context.Discipline.Add(discipline);
                facultyFromDb.Disciplines.Add(discipline);
                _context.Faculty.Update(facultyFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Delete(string id)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return Unauthorized();
            }

            Discipline disciplineFromDb = (from u in _context.Discipline
                where u.DisciplineId == new Guid(id)
                select u).SingleOrDefault();
            if (disciplineFromDb != null)
            {
                _context.Discipline.Remove(disciplineFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("")]
        public object Test()
        {
            List<Discipline> disciplines = (from u in _context.Discipline
                                            select u)
                                            .Include(u => u.Faculty)
                                            .ToList();
            if (disciplines != null)
            {
                //var a = new Discipline();
                //{
                //    DisciplineId = new Guid(), CreatedDate = disciplineFromDb.CreatedDate,
                //    UpdatedDate = disciplineFromDb.UpdatedDate, Name = disciplineFromDb.Name, ShortName = disciplineFromDb.ShortName
                //};
                //var c = new List<Discipline>();
                //_context.Discipline.Add(a);
                //_context.Discipline.Update(disciplines[1]);
                //_context.Faculty.Update(facultyFromDb);
                //_context.SaveChanges();
                //disciplineFromDb.Faculty = facultyFromDb;
                //_context.SaveChanges();
                string json;
                try
                {
                    var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                    json = JsonConvert.SerializeObject(disciplines, Formatting.Indented, serializerSettings);
                }
                catch (Exception exception)
                {
                    return "Ошибон";

                }
                return json;
            }
            return null;
        }

    }
}
