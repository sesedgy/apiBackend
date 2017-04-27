using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseModel.Models
{
    public sealed class Faculty
    {
        public Guid FacultyId { get; set; }
        public string Name { get; set; }
        //public Employee Chief { get; set; }                     // Начальник

        public ICollection<Discipline> Disciplines { get; set; }
        public ICollection<Teacher> Teachers { get; set; }

        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Faculty()
        {
            Disciplines = new List<Discipline>();
            Teachers = new List<Teacher>();
        }

    }
}
