using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class FilesController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FilesController(DataBaseContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public IActionResult GetFile([FromBody] string path)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsHr", "IsAdmin" }))
            {
                return Unauthorized();
            }
            try
            {
                string fileType = "application/octet-stream";
                string filePath = _appEnvironment.ContentRootPath + path;

                return PhysicalFile(filePath, fileType);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public string GetImage([FromBody] string path)
        {
            if (!CookieList.GetInstance().CheckCookie(Request, new[] { "IsWorker", "IsHr", "IsAdmin" }))
            {
                return null;
            }
            try
            {
                using (Image image = Image.FromFile(_appEnvironment.ContentRootPath + path))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
