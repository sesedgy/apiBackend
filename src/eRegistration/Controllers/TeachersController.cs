using System;
using System.Collections.Generic;
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
    public class TeachersController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public TeachersController(DataBaseContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            try
            {
                if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsAdmin"}))
                {
                    return null;
                }

                var teacher = (from u in _context.Teacher
                    where u.TeacherId == new Guid(id)
                    select new
                    {
                        u.TeacherId,
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
                        u.Faculty,
                        u.Speciality,
                        u.Scientist,
                        u.TeachersWorks,
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
                string json = JsonConvert.SerializeObject(teacher, Formatting.Indented, serializerSettings);

                return json;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        [HttpGet("{userName}")]
        public Teacher GetByUser(string userName)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return null;
            }

            var teacher = (from u in _context.Teacher
                            where u.User.Login == userName
                            select u).SingleOrDefault();
            return teacher;
        }

        [HttpGet]
        public string GetAll()
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return null;
            }

            var teachers = from u in _context.Teacher
                            select new
                            {
                                u.TeacherId,
                                u.LastName,
                                u.FirstName,
                                u.MiddleName,
                                u.Sex,
                                u.BirthDate,
                                u.MobilePhone,
                                u.Faculty,
                                u.Scientist
                            };

            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            var json = JsonConvert.SerializeObject(teachers, Formatting.Indented, serializerSettings);

            return json;
        }

        [HttpPost]
        public IActionResult Update([FromBody] Teacher teacher)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var teacherFromDb = (from u in _context.Teacher
                                  where u.TeacherId == teacher.TeacherId
                                  select u).SingleOrDefault();
            if (teacherFromDb != null)
            {
                var faculty = (from u in _context.Faculty
                                      where u.FacultyId == teacher.Faculty.FacultyId
                                      select u).SingleOrDefault();

                teacherFromDb.LastName = teacher.LastName;
                teacherFromDb.FirstName = teacher.FirstName;
                teacherFromDb.MiddleName = teacher.MiddleName;
                teacherFromDb.BirthDate = teacher.BirthDate;
                teacherFromDb.MobilePhone = teacher.MobilePhone;
                teacherFromDb.Citizenship = teacher.Citizenship;
                teacherFromDb.Sex = teacher.Sex;
                teacherFromDb.PassportSeries = teacher.PassportSeries;
                teacherFromDb.PassportNumber = teacher.PassportNumber;
                teacherFromDb.PassportDate = teacher.PassportDate;
                teacherFromDb.PassportIssueOrg = teacher.PassportIssueOrg;
                teacherFromDb.CountryRegistration = teacher.CountryRegistration;
                teacherFromDb.CityRegistration = teacher.CityRegistration;
                teacherFromDb.RegionRegistration = teacher.RegionRegistration;
                teacherFromDb.DistrictRegistration = teacher.DistrictRegistration;
                teacherFromDb.LocalityRegistration = teacher.LocalityRegistration;
                teacherFromDb.StreetRegistration = teacher.StreetRegistration;
                teacherFromDb.HouseRegistration = teacher.HouseRegistration;
                teacherFromDb.BuildingRegistration = teacher.BuildingRegistration;
                teacherFromDb.HousingRegistration = teacher.HousingRegistration;
                teacherFromDb.FlatRegistration = teacher.FlatRegistration;
                teacherFromDb.IndexRegistration = teacher.IndexRegistration;
                teacherFromDb.INN = teacher.INN;
                teacherFromDb.SNILS = teacher.SNILS;
                teacherFromDb.SeriesEducationDocument = teacher.SeriesEducationDocument;
                teacherFromDb.NumberEducationDocument = teacher.NumberEducationDocument;
                teacherFromDb.DateEducationDocument = teacher.DateEducationDocument;
                teacherFromDb.WhoGiveEducationDocument = teacher.WhoGiveEducationDocument;
                teacherFromDb.Faculty = faculty;
                teacherFromDb.Scientist = teacher.Scientist;
                teacherFromDb.BeginDate = teacher.BeginDate;
                teacherFromDb.EndDate = teacher.EndDate;
                teacherFromDb.SalaryPerHour = teacher.SalaryPerHour;
                teacherFromDb.PhotoPath = teacher.PhotoPath;
                teacherFromDb.Status = teacher.Status;
                teacherFromDb.WhoUpdate = teacher.WhoUpdate;
                teacherFromDb.CreatedDate = teacher.CreatedDate;
                teacherFromDb.UpdatedDate = teacher.UpdatedDate;


                _context.Teacher.Update(teacherFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Teacher teacher)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return Unauthorized();
            }

            var user = teacher.User;
            try
            {
                var usersController = new UsersController(_context);
                if (usersController.Create(user))
                {
                    var faculty = (from u in _context.Faculty
                                      where u.FacultyId == teacher.Faculty.FacultyId
                                      select u).SingleOrDefault();
                    teacher.Faculty = faculty;
                    _context.Teacher.Add(teacher);
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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return Unauthorized();
            }

            Teacher teacherFromDb = (from u in _context.Teacher
                                            where u.TeacherId == new Guid(id)
                                            select u).SingleOrDefault();
            if (teacherFromDb != null)
            {
                User userFromDb = (from u in _context.Users
                                        where u.UserId == new Guid(idUser)
                                        select u).SingleOrDefault();
                _context.Teacher.Remove(teacherFromDb);
                _context.Users.Remove(userFromDb);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult UploadPhoto([FromBody] object[] massive)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
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
