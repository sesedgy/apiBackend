using System;

namespace DataBaseModel.Models
{
    public class TeachersWork                   //Нагрузка на преподавателя
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid TeachersTypeWorkId { get; set; }     //Вид нагрузки
        public Guid GroupId { get; set; }
        public string Curs { get; set; }                 //Курс
        public string Semester { get; set; }             //Семестр
        public string HoursWork { get; set; }            //Часы работы

    }
}
