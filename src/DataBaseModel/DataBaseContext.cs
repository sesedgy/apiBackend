using DataBaseModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Group> Group { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Abiturient>().ToTable("Abiturient");
            modelBuilder.Entity<Specialty>().ToTable("Specialty");
            modelBuilder.Entity<Discipline>().ToTable("Discipline");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<TeachersTypesWork>().ToTable("TeachersTypesWork");
            modelBuilder.Entity<TeachersWork>().ToTable("TeachersWork");
            modelBuilder.Entity<Faculty>().ToTable("Faculty");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Group>().ToTable("Group");
        }
    }
}
