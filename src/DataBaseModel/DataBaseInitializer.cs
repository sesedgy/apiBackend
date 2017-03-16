using System;
using DataBaseModel.Models;

namespace DataBaseModel
{
    public class DataBaseInitializer
    {
        public static void Initialize(DataBaseContext context)
        {
            context.Database.EnsureCreated();

            //var students = new User[]
            //{
            //    new User{Id = new Guid(), Login= "sesedgy", Password= "123456", CreatedDate = DateTime.Now, Email = "Email", IsOnline = false, LastActivityDate = DateTime.Now, Role = "administrator", UpdatedDate = DateTime.Now, WhoUpdate = "whoupdate"},
            //};
            //foreach (User s in students)
            //{
            //    context.Users.Add(s);
            //}
            var specialities = new Specialty[]
            {
                new Specialty(){Id = new Guid(), FormOfEducation = "Очная", NameSpecialty = "Пожарная безопасность", Qualification = "Специалист"},
                new Specialty(){Id = new Guid(), FormOfEducation = "Очная", NameSpecialty = "Техносферная безопасность", Qualification = "Бакалавр"},
            };
            foreach (Specialty s in specialities)
            {
                context.Specialities.Add(s);
            }
            context.SaveChanges();

        }

    }
}
