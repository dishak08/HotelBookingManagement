using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class InMemoryDatabase : DbContext
    {

        public InMemoryDatabase(DbContextOptions<InMemoryDatabase> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
