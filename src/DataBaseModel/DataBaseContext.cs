using DataBaseModel.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseModel
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
      
        public DbSet<User> Users { get; set; }
        public DbSet<Abiturient> Abiturients { get; set; }
        public DbSet<Specialty> Specialities { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<TeachersTypesWork> TeachersTypesWork { get; set; }
        public DbSet<TeachersWork> TeachersWork { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Abiturient>().ToTable("Abiturient");
            modelBuilder.Entity<Specialty>().ToTable("Specialty");
            modelBuilder.Entity<Discipline>().ToTable("Discipline");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<TeachersTypesWork>().ToTable("TeachersTypesWork");
            modelBuilder.Entity<TeachersWork>().ToTable("TeachersWork");
        }
    }

}
