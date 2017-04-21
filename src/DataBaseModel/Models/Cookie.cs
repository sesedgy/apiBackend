using System;
using System.ComponentModel.DataAnnotations;

namespace DataBaseModel.Models
{
    // Не в БД
    public class Cookie
    {
        public Guid CookieId { get; set; }
        public User User { get; set; }
        public bool IsAbiturient { get; set; }
        public bool IsStudent { get; set; }
        public bool IsStudentLeader { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsWorker { get; set; }
        public bool IsAdmin { get; set; }
    }
}
