using System;

namespace DataBaseModel.Models
{
    public class TeachersTypesWork
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PercentLoad { get; set; } //коэфициент нагрузки
    }
}
