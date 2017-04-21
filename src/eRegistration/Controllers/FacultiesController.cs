using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseModel;
using DataBaseModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return null;
            }
            Faculty faculty = (from u in _context.Faculty
                where u.FacultyId == new Guid(id)
                select u).SingleOrDefault();
            return faculty;
        }

        [HttpGet]
        public string GetAll()
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return null;
            }
            List<Faculty> faculties = (from u in _context.Faculty
                    select u)
                .Include(u => u.Disciplines)
                .Include(u => u.Teachers)
                .ToList();
            var serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var json = JsonConvert.SerializeObject(faculties, Formatting.Indented, serializerSettings);

            return json;
        }

        [HttpPost]
        public IActionResult Update([FromBody] Faculty faculty)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return Unauthorized();
            }
            Faculty facultyFromDb = (from u in _context.Faculty
                where u.FacultyId == faculty.FacultyId
                select u).SingleOrDefault();
            if (facultyFromDb != null)
            {
                facultyFromDb.Name = faculty.Name;
                facultyFromDb.CreatedDate = faculty.CreatedDate;
                facultyFromDb.UpdatedDate = faculty.UpdatedDate;
                facultyFromDb.WhoUpdate = faculty.WhoUpdate;
                _context.Faculty.Update(facultyFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Faculty faculty)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return Unauthorized();
            }
            Faculty facultyFromDb = (from u in _context.Faculty
                where u.FacultyId == faculty.FacultyId
                select u).SingleOrDefault();
            if (facultyFromDb == null)
            {
                _context.Faculty.Add(faculty);
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
            Faculty facultyFromDb = (from u in _context.Faculty
                where u.FacultyId == new Guid(id)
                select u)
                .Include(u => u.Disciplines)
                .Include(u => u.Teachers)
                .SingleOrDefault();
            if (facultyFromDb != null)
            {
                _context.Faculty.Remove(facultyFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

    }
}