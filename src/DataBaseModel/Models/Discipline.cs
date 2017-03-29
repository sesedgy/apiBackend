using System;

namespace DataBaseModel.Models
{
    public class Discipline
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Faculty { get; set; }
        public string StatusDiscipline { get; set; }

        public Guid WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
