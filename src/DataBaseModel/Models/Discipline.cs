using System;
using System.ComponentModel.DataAnnotations;

namespace DataBaseModel.Models
{
    public class Discipline
    {
        public Guid DisciplineId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        [Required]
        public virtual Faculty Faculty { get; set; }
        public string StatusDiscipline { get; set; }

        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
