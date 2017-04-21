using System;
using System.ComponentModel.DataAnnotations;

namespace DataBaseModel.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
