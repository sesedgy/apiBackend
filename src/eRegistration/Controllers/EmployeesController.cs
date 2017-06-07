using System;
using System.IO;
using System.Linq;
using DataBaseModel;
using DataBaseModel.Models;
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
        public string Get(string id)
        {
            try
            {
                if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsHr", "IsAdmin"}))
                {
                    return null;
                }

                var employee = (from u in _context.Employee
                    where u.EmployeeId == new Guid(id)
                    select new
                    {
                        u.EmployeeId,
                        u.User,
                        u.LastName,
                        u.FirstName,
                        u.MiddleName,
                        u.BirthDate,       
                        u.MobilePhone,
                        u.Citizenship,
                        u.Sex,
                        u.PassportSeries,
                        u.PassportNumber,
                        u.PassportDate,
                        u.PassportIssueOrg,
                        u.CountryRegistration,
                        u.CityRegistration,
                        u.RegionRegistration,
                        u.DistrictRegistration,
                        u.LocalityRegistration,
                        u.StreetRegistration,
                        u.HouseRegistration,
                        u.BuildingRegistration,
                        u.HousingRegistration,
                        u.FlatRegistration,
                        u.IndexRegistration,                          
                        u.INN,
                        u.SNILS,
                        u.SeriesEducationDocument,
                        u.NumberEducationDocument,
                        u.DateEducationDocument,
                        u.WhoGiveEducationDocument,
                        u.Department,
                        u.Position,
                        u.BeginDate,
                        u.EndDate,
                        u.SalaryPerHour,                  
                        u.PhotoPath,
                        u.Status,
                        u.WhoUpdate,
                        u.CreatedDate,
                        u.UpdatedDate,

                    }).SingleOrDefault();

                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string json = JsonConvert.SerializeObject(employee, Formatting.Indented, serializerSettings);

                return json;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        [HttpGet("{userName}")]
        public Employee GetByUser(string userName)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsHr", "IsAdmin" }))
            {
                return null;
            }

            var employee = (from u in _context.Employee
                            where u.User.Login == userName
                            select u).SingleOrDefault();
            return employee;
        }

        [HttpGet]
        public string GetAll()
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsHr", "IsAdmin" }))
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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsHr", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var employeeFromDb = (from u in _context.Employee
                                  where u.EmployeeId == employee.EmployeeId
                                  select u).SingleOrDefault();
            if (employeeFromDb != null)
            {
                var department = (from u in _context.Department
                                      where u.DepartmentId == employee.Department.DepartmentId
                                      select u).SingleOrDefault();

                employeeFromDb.LastName = employee.LastName;
                employeeFromDb.FirstName = employee.FirstName;
                employeeFromDb.MiddleName = employee.MiddleName;
                employeeFromDb.BirthDate = employee.BirthDate;
                employeeFromDb.MobilePhone = employee.MobilePhone;
                employeeFromDb.Citizenship = employee.Citizenship;
                employeeFromDb.Sex = employee.Sex;
                employeeFromDb.PassportSeries = employee.PassportSeries;
                employeeFromDb.PassportNumber = employee.PassportNumber;
                employeeFromDb.PassportDate = employee.PassportDate;
                employeeFromDb.PassportIssueOrg = employee.PassportIssueOrg;
                employeeFromDb.CountryRegistration = employee.CountryRegistration;
                employeeFromDb.CityRegistration = employee.CityRegistration;
                employeeFromDb.RegionRegistration = employee.RegionRegistration;
                employeeFromDb.DistrictRegistration = employee.DistrictRegistration;
                employeeFromDb.LocalityRegistration = employee.LocalityRegistration;
                employeeFromDb.StreetRegistration = employee.StreetRegistration;
                employeeFromDb.HouseRegistration = employee.HouseRegistration;
                employeeFromDb.BuildingRegistration = employee.BuildingRegistration;
                employeeFromDb.HousingRegistration = employee.HousingRegistration;
                employeeFromDb.FlatRegistration = employee.FlatRegistration;
                employeeFromDb.IndexRegistration = employee.IndexRegistration;
                employeeFromDb.INN = employee.INN;
                employeeFromDb.SNILS = employee.SNILS;
                employeeFromDb.SeriesEducationDocument = employee.SeriesEducationDocument;
                employeeFromDb.NumberEducationDocument = employee.NumberEducationDocument;
                employeeFromDb.DateEducationDocument = employee.DateEducationDocument;
                employeeFromDb.WhoGiveEducationDocument = employee.WhoGiveEducationDocument;
                employeeFromDb.Department = department;
                employeeFromDb.Position = employee.Position;
                employeeFromDb.BeginDate = employee.BeginDate;
                employeeFromDb.EndDate = employee.EndDate;
                employeeFromDb.SalaryPerHour = employee.SalaryPerHour;
                employeeFromDb.PhotoPath = employee.PhotoPath;
                employeeFromDb.Status = employee.Status;
                employeeFromDb.WhoUpdate = employee.WhoUpdate;
                employeeFromDb.CreatedDate = employee.CreatedDate;
                employeeFromDb.UpdatedDate = employee.UpdatedDate;


                _context.Employee.Update(employeeFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsHr", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var user = employee.User;
            try
            {
                var usersController = new UsersController(_context);
                if (usersController.Create(user))
                {
                    var department = (from u in _context.Department
                                      where u.DepartmentId == employee.Department.DepartmentId
                                      select u).SingleOrDefault();
                    employee.Department = department;
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

        [HttpGet("{id}&{idUser}")]
        public IActionResult Delete(string id, string idUser)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsHr", "IsAdmin" }))
            {
                return Unauthorized();
            }

            Employee employeeFromDb = (from u in _context.Employee
                                            where u.EmployeeId == new Guid(id)
                                            select u).SingleOrDefault();
            if (employeeFromDb != null)
            {
                User userFromDb = (from u in _context.Users
                                        where u.UserId == new Guid(idUser)
                                        select u).SingleOrDefault();
                _context.Employee.Remove(employeeFromDb);
                _context.Users.Remove(userFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult UploadPhoto([FromBody] object[] massive)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsHr", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var uploadedFile = (IFormFile)massive[0];
            var id = new Guid((string)massive[1]);
            if (uploadedFile != null)
            {
                string path = _appEnvironment.WebRootPath + "eAcademy/Images/Photo/Employees/" + id;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
                var employeeFromDb = (from u in _context.Employee
                                      where u.EmployeeId == id
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
