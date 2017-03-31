using System;

namespace DataBaseModel.Models
{
    public class Faculty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ChiefId { get; set; }     // Начальник

        public Guid WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
