using System;
using System.Collections.Generic;

namespace DataBaseModel.Models
{
    public class TeachersTypesWork
    {
        public Guid TeachersTypesWorkId { get; set; }
        public string Name { get; set; }
        public string PercentLoad { get; set; } //коэфициент нагрузки
        public List<TeachersWork> TeachersWorks { get; set; }

        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
