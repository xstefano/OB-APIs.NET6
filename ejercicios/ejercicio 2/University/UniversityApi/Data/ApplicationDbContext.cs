using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;

namespace UniversityApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}
