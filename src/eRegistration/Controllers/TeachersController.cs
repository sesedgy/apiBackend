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
using OfficeOpenXml;


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
                if (!CookieList.GetInstance().CheckCookie(Request, new[] {"IsWorker", "IsHr", "IsAdmin"}))
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
                        u.AcademicDegree,
                        u.AcademicTitle,
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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsTeacher", "IsAdmin" }))
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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsHr", "IsAdmin" }))
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
                                u.AcademicDegree,
                                u.AcademicTitle
                            };

            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            var json = JsonConvert.SerializeObject(teachers, Formatting.Indented, serializerSettings);

            return json;
        }

        [HttpPost]
        public IActionResult Update([FromBody] Teacher teacher)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsHr", "IsAdmin" }))
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
                teacherFromDb.AcademicDegree = teacher.AcademicDegree;
                teacherFromDb.AcademicTitle = teacher.AcademicTitle;
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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsHr", "IsAdmin" }))
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
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsHr", "IsAdmin" }))
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

        [HttpPost]
        public IActionResult UploadWorkVolume(IFormFileCollection files)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsAdmin" }))
            {
                return Unauthorized();
            }

            if (files != null)
            {
                try
                {
                    var volumeFile = files[0];
                    var groupTeacherFile = files[1];
                    var filePath1 = _appEnvironment.ContentRootPath + "\\Files\\Temporary\\" + volumeFile.FileName;
                    using (var fileStream = new FileStream(filePath1, FileMode.Create))
                    {
                        volumeFile.CopyTo(fileStream);
                    }
                    var filePath2 = _appEnvironment.ContentRootPath + "\\Files\\Temporary\\" + groupTeacherFile.FileName;
                    using (var fileStream = new FileStream(filePath2, FileMode.Create))
                    {
                        groupTeacherFile.CopyTo(fileStream);
                    }
                    try
                    {
                        var groupFromFileName = volumeFile.FileName.Replace(".xlsx", "").Replace(".xls", "").Split(',');
                        var package = new ExcelPackage(new FileInfo(filePath1));
                        var package2 = new ExcelPackage(new FileInfo(filePath2));


                        foreach (var semester in package.Workbook.Worksheets)
                        {
                            var semesterString =
                                semester.Cells[4, 3].Value.ToString()
                                    .Substring(7).Replace(" семестр", "").Trim();
                            var curs = semester.Cells[4, 3].Value.ToString().Substring(0, 1);
                            for (int j = 7; j <= semester.Dimension.End.Row - 2; j++)
                            {
                                var discipline = semester.Cells[j, 2].Value.ToString();
                                /////////////////////////////////////////////////////// 
                                // Парсим документ с преподавателями и группами
                                var teacher = new Teacher();     
                                var groupsList = new List<Group>();
                                ExcelWorksheet list = null;
                                foreach (var semester2 in package2.Workbook.Worksheets)
                                {
                                    list = semester2;
                                    break;
                                }
                                var flagForExit = false;
                                for (int i2 = 1; i2 < list.Dimension.End.Row; i2++)
                                {
                                    //Ищем все дисциплины что совпадают с текущей
                                    if (list.Cells[i2, 5].Value == null) { continue; }
                                    if (discipline == list.Cells[i2, 5].Value.ToString())
                                    {
                                        //Выделяем группы занимающиеся по этой дисцплине
                                        var groupsFromList = list.Cells[i2, 3].Value.ToString().Replace(" ", "").Split(',');  //TODO проверить в условиях одной группы и нескольких
                                        for (int iGr = 0; iGr < groupFromFileName.Length; iGr++)
                                        {
                                            for (int iGr2 = 0; iGr2 < groupsFromList.Length; iGr2++)
                                            {
                                                //Проверяем соответствует ли хоть одна группа выбранным группам
                                                if (groupFromFileName[iGr] == groupsFromList[iGr2])
                                                {
                                                    groupsList.Add((from u in _context.Group
                                                               where u.Name == groupFromFileName[iGr]
                                                               select u).SingleOrDefault());
                                                    teacher = (from u in _context.Teacher
                                                               where u.LastName + " " + u.FirstName + " " + u.MiddleName == list.Cells[i2, 4].Value.ToString().Trim()
                                                               select u).SingleOrDefault();
                                                    flagForExit = true;
                                                }
                                            }    
                                        }
                                    }
                                    if(flagForExit) { break;}                                  
                                }
                                //////////////////////////////////////////////////////////
                                for (int i = 5; i <= 20; i++)
                                {
                                    if (semester.Cells[j, i].Value != null)
                                    {
                                        string typeWorkFromDoc = semester.Cells[6, i].Value.ToString().Trim();
                                        if (typeWorkFromDoc.IndexOf('(') != -1)
                                        {
                                            typeWorkFromDoc = typeWorkFromDoc.Remove(typeWorkFromDoc.IndexOf('(') - 1);// Будет ошибка если в названии типа занятий будут еще скобки помимо коэффициента
                                        }
                                        typeWorkFromDoc = char.ToUpper(typeWorkFromDoc[0]) + typeWorkFromDoc.Substring(1);
                                        switch (typeWorkFromDoc)
                                        {
                                            case "Из них лекционные":
                                                typeWorkFromDoc = "Лекции";
                                                break;
                                        }
                                        var typeWork = (from u in _context.TeachersTypesWork
                                                        where u.Name == typeWorkFromDoc
                                                        select u).SingleOrDefault();
                                        for (int k = 0; k < groupsList.Count; k++)
                                        {
                                            var teacherWork = new TeachersWork()
                                            {
                                                TeachersWorkId = Guid.NewGuid(),
                                                Teacher = teacher,
                                                TeachersTypesWork = typeWork,
                                                Group = groupsList[k],
                                                Semester = semesterString,
                                                Curs = curs,
                                                HoursWork = semester.Cells[j, i].Value.ToString(),
                                                WhoUpdate = "auto",
                                                CreatedDate = DateTime.Now,
                                                UpdatedDate = DateTime.Now
                                            };
                                            _context.TeachersWork.Add(teacherWork);               
                                        }
                                        _context.SaveChanges();//TODO Проверить возможно ли множественное добавление
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        System.IO.File.Delete(filePath1);
                        System.IO.File.Delete(filePath2);
                        return BadRequest();
                    }

                    System.IO.File.Delete(filePath1);
                    System.IO.File.Delete(filePath2);
                }
                catch (Exception)
                {
                    return BadRequest();

                }
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public string GetTeachersWorks(string id)
        {
            try
            {
                if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsHr", "IsAdmin" }))
                {
                    return null;
                }

                var teacherWork = (from u in _context.TeachersWork
                                   where u.Teacher.TeacherId == new Guid(id)
                                   select new
                                   {
                                       u.TeachersWorkId,
                                       u.Teacher,
                                       u.TeachersTypesWork,
                                       u.Group,
                                       u.Curs,
                                       u.Semester,
                                       u.HoursWork,
                                       u.WhoUpdate,
                                       u.CreatedDate,
                                       u.UpdatedDate
                                   }).ToList();

                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                string json = JsonConvert.SerializeObject(teacherWork, Formatting.Indented, serializerSettings);

                return json;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

    }
}
