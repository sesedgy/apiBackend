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
    public class DepartmentsController : Controller
    {
        private readonly DataBaseContext _context;

        public DepartmentsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public Department Get(string id)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return null;
            }
            Department department = (from u in _context.Department
                where u.DepartmentId == new Guid(id)
                select u).SingleOrDefault();
            return department;
        }

        [HttpGet]
        public string GetAll()
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return null;
            }
            List<Department> departments = (from u in _context.Department
                    select u)
                .Include(u => u.Employees)
                .ToList();
            var serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var json = JsonConvert.SerializeObject(departments, Formatting.Indented, serializerSettings);

            return json;
        }

        [HttpPost]
        public IActionResult Update([FromBody] Department department)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return Unauthorized();
            }
            Department departmentFromDb = (from u in _context.Department
                where u.DepartmentId == department.DepartmentId
                select u).SingleOrDefault();
            if (departmentFromDb != null)
            {
                departmentFromDb.Name = department.Name;
                departmentFromDb.CreatedDate = department.CreatedDate;
                departmentFromDb.UpdatedDate = department.UpdatedDate;
                departmentFromDb.WhoUpdate = department.WhoUpdate;
                _context.Department.Update(departmentFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Department department)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
            {
                return Unauthorized();
            }
            Department departmentFromDb = (from u in _context.Department
                where u.DepartmentId == department.DepartmentId
                select u).SingleOrDefault();
            if (departmentFromDb == null)
            {
                _context.Department.Add(department);
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
            Department departmentFromDb = (from u in _context.Department
                where u.DepartmentId == new Guid(id)
                select u)
                .Include(u => u.Employees)
                .SingleOrDefault();
            if (departmentFromDb != null)
            {
                _context.Department.Remove(departmentFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

    }
}