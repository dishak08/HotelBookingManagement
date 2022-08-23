using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace UserMicroservice.Models
{
    public class Database : DbContext
    {
        protected readonly IConfiguration Configuration;

        public Database(DbContextOptions<Database> options/*, IConfiguration configuration*/) : base(options)
        {
            /*Configuration = configuration;*/
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable("User");

            modelBuilder.Entity<AppUser>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<AppUser>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<AppUser>().Property(u => u.Role).HasConversion<int>();
        }

        #nullable enable
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
