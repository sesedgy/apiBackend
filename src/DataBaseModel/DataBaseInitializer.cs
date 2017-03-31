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
            //    new User{Id = new Guid(), Login= "sesedgy", Password= "1ffb1885ba2ebca902a894a5c3e316f720af126b9c418672a987e39b57436757", HashSalt = "8e35c2cd3bf6641bdb0e2050b76932cbb2e6034a0ddacc1d9bea82a6ba57f7cf", CreatedDate = DateTime.Now, Email = "Email", IsOnline = false, LastActivityDate = DateTime.Now, Role = "administrator", UpdatedDate = DateTime.Now, WhoUpdate = "whoupdate"},
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
