using System;
using System.Collections.Generic;

namespace DataBaseModel.Models
{
    public class Specialty
    {
        public Guid SpecialtyId { get; set; }
        public string NameSpecialty { get; set; }
        public string FormOfEducation { get; set; }
        public string Qualification { get; set; }
        public string CodeSpecialty { get; set; }
        public List<Group> Groups { get; set; }
    }
}
