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
        public DbSet<Cookie> Cookies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Cookie>().ToTable("Cookie");
        }
    }

}
