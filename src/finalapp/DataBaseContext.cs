using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalapp.Models;
using Microsoft.EntityFrameworkCore;

namespace finalapp
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
      
        public DbSet<User> Users { get; set; }
        public DbSet<Abiturient> Abiturients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Abiturient>().ToTable("Abiturient");
            modelBuilder.Entity<Specialty>().ToTable("Specialty");
        }
    }

}
