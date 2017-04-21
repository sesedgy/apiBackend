using System;

namespace DataBaseModel.Models
{
    public class TeachersWork                                       //Нагрузка на преподавателя
    {
        public Guid TeachersWorkId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual TeachersTypesWork TeachersTypesWork { get; set; }    //Вид нагрузки
        public virtual Group Group { get; set; }
        public string Curs { get; set; }                            //Курс
        public string Semester { get; set; }                        //Семестр
        public string HoursWork { get; set; }                       //Часы работы

    }
}
