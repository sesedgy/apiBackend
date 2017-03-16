using System;

namespace eRegistration.Models
{
    public class Cookie
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsAbiturient { get; set; }
        public bool IsStudent { get; set; }
        public bool IsStudentLeader { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsWorker { get; set; }
        public bool IsAdmin { get; set; }
    }
}
