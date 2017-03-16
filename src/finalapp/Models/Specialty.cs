using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace finalapp.Models
{
    public class Specialty
    {
        public Guid Id { get; set; }
        public string NameSpecialty { get; set; }
        public string FormOfEducation { get; set; }
        public string Qualification { get; set; }
    }
}
