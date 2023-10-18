using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SafeIn_Api.Models
{
    public class AppDbContext : IdentityDbContext<Employee>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Entrance> Entrances { get; set; }
        public DbSet<Department> Departments { get; set; } 
    }
}

