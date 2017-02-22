using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalapp.Models;

namespace finalapp
{
    public class DataBaseInitializer
    {
        public static void Initialize(DataBaseContext context)
        {
            context.Database.EnsureCreated();

            var students = new User[]
            {
                new User{Id = new Guid(), Login= "sesedgy", Password= "123456", CreatedDate = DateTime.Now, Email = "Email", IsOnline = false, LastActivityDate = DateTime.Now, Role = "administrator", UpdatedDate = DateTime.Now, WhoUpdate = "whoupdate"},
            };
            foreach (User s in students)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

        }

    }
}
