using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataBaseModel;
using DataBaseModel.Models;
using eRegistration.CommonServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace eRegistration.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public EmployeesController(DataBaseContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: api/abiturients/get/id
        [HttpGet("{id}")]
        public Employee Get(string id)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return null;
            }

            var employee = (from u in _context.Employee
                            where u.EmployeeId == new Guid(id)
                            select u).SingleOrDefault();
            return employee;
        }

        [HttpGet("{id}")]
        public string GetAll(string id)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return null;
            }

            var employees = from u in _context.Employee
                            select new
                            {
                                u.EmployeeId,
                                u.LastName,
                                u.FirstName,
                                u.MiddleName,
                                u.BirthDate,
                                u.MobilePhone,
                                u.Department,
                                u.Position
                            };

            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            var json = JsonConvert.SerializeObject(employees, Formatting.Indented, serializerSettings);

            return json;
        }

        [HttpPost]
        public IActionResult Update([FromBody] Employee employee)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var employeeFromDb = (from u in _context.Employee
                                  where u.EmployeeId == employee.EmployeeId
                                  select u).SingleOrDefault();
            if (employeeFromDb != null)
            {
                //employeeFromDb.LastName = employee.LastName
                //_context.Employee.Remove(employeeFromDb);
                //_context.SaveChanges();
                //_context.E.Add(discipline);
                //_context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // POST: api/abiturients/create
        [HttpPost]
        public IActionResult Create(object[] userAndEmployee)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var user = (User)userAndEmployee[0];
            var employee = (Employee)userAndEmployee[1];
            try
            {
                var usersController = new UsersController(_context);
                if (!usersController.Create(user))
                {
                    _context.Employee.Add(employee);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Delete(string id)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
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

        [HttpPost]
        public IActionResult UploadPhoto(object[] massive)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var uploadedFile = (IFormFile)massive[0];
            var employee = (Employee)massive[1];
            if (uploadedFile != null)
            {
                string path = _appEnvironment.WebRootPath + "eAcademy/Images/Photo/Employees/" + employee.EmployeeId;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
                var employeeFromDb = (from u in _context.Employee
                                      where u.EmployeeId == employee.EmployeeId
                                      select u).SingleOrDefault();
                if (employeeFromDb != null)
                {
                    employeeFromDb.PhotoPath = path;
                    _context.Update(employeeFromDb);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
