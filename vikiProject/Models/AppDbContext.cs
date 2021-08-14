using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;

namespace vikiProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Drama> Dramas { get; set; }
        public DbSet<Episode> Episodes { get; set; } 

    }
}