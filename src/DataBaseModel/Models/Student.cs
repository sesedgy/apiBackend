using System;
using System.ComponentModel.DataAnnotations;

namespace DataBaseModel.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        [Required]
        public virtual User User { get; set; }

        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
