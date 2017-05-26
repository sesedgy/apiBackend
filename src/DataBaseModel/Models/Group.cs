using System;
using System.Collections.Generic;

namespace DataBaseModel.Models
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public virtual Specialty Specialty { get; set; }            //Информация по направлению обучения
        public List<Student> Students { get; set; }
        public string Semester { get; set; }                        //Семестр
        public string Status { get; set; }
        public DateTime Begin { get; set; }
        public List<TeachersWork> TeachersWorks { get; set; }

        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
