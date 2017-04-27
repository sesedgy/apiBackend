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
            //    new User{UserId = new Guid(), Login= "sesedgy", Password= "bb520b241507b1ff3de47a41697ad405c2c7752fe26fa23419160e2e479c6aab", HashSalt = "3f3ef786b34d6dd716e1812c8b74a7a0e1f05aa5f3230588f6f5bcd00c6c8392", CreatedDate = DateTime.Now, Email = "Email", IsOnline = false, LastActivityDate = DateTime.Now, Role = "IsAdmin", UpdatedDate = DateTime.Now, WhoUpdate = "whoupdate"},
            //};
            //foreach (User s in students)
            //{
            //    context.Users.Add(s);
            //}
            var specialities = new Specialty[]
            {
                new Specialty(){SpecialtyId = new Guid(), FormOfEducation = "Очная", NameSpecialty = "Пожарная безопасность", Qualification = "Специалист"},
                new Specialty(){SpecialtyId = new Guid(), FormOfEducation = "Очная", NameSpecialty = "Техносферная безопасность", Qualification = "Бакалавр"},
            };
            foreach (Specialty s in specialities)
            {
                context.Specialities.Add(s);
            }
            context.SaveChanges();

        }

    }
}
