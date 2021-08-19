using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;

namespace vikiProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Drama> Dramas { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        
        public DbSet<DownloadLink> DownloadLinks { get; set; }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     modelBuilder.Entity<Episode>()
        //         .HasKey(e => new {e.EpisodeNumber,e.DramaId});
        //     modelBuilder.Entity<Episode>()
        //         .HasMany(e => e.DownloadLinks)
        //         .WithOne()
        //         .IsRequired();
        //
        // }
    }
}