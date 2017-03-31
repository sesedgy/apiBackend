﻿using System.Collections.Generic;
using System.Linq;
using DataBaseModel;
using DataBaseModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace eRegistration.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SpecialtiesController : Controller
    {
        private readonly DataBaseContext _context;

        public SpecialtiesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/specialties/getAll
        [HttpGet]
        public List<Specialty> GetAll()
        {
            List<Specialty> specialty = (from u in _context.Specialities
                              select u).ToList();
            return specialty;
        }

        // GET: api/specialties/getAllSpecialties
        [HttpGet]
        public List<Specialty> GetAllSpecialties()
        {
            List<Specialty> specialty = (from u in _context.Specialities
                              select u).Distinct().ToList();
            return specialty;
        }

        // POST: api/abiturients/create
        //[HttpPost]
        //public RedirectResult Create([FromBody] object[] userAndAbiturient)
        //{
        //    var user = (User)userAndAbiturient[0];
        //    var abiturient = (Abiturient)userAndAbiturient[1];
        //    var photo = (IFormFile)userAndAbiturient[2];
        //    user.Role = "IsAbiturient";
        //    try
        //    {
        //        var usersController = new UsersController(_context);
        //        if (!usersController.Create(user))

        //            _context.Abiturients.Add(abiturient);
        //        _context.SaveChanges();
        //        return new RedirectResult("https://www.google.ru/");
        //    }
        //    catch (Exception)
        //    {
        //        return new RedirectResult("https://www.google.ru/");
        //    }
        //}
    }
}
