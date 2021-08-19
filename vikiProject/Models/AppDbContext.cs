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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Episode>()
                .HasKey(e => new {e.EpisodeNumber, e.DramaId});
            modelBuilder.Entity<DownloadLink>()
                .HasOne(d => d.Episode)
                .WithMany(e => e.DownloadLinks)
                .HasForeignKey(d => new {d.EpisodeNumber, d.DramaId});
            modelBuilder.Entity<Episode>()
                .HasOne(e => e.Drama)
                .WithMany(d => d.Episodes)
                .HasForeignKey(e => e.DramaId);
            modelBuilder.Entity<DownloadLink>()
                .HasKey(d => new {d.DramaId, d.EpisodeNumber,d.Quality});
            // base.OnModelCreating(modelBuilder);
            
            
            //drama
            modelBuilder.Entity<Drama>()
                .HasKey(d => d.DramaId);
            modelBuilder.Entity<Drama>()
                .Property(d => d.DramaId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Drama>()
                .Property(d => d.ImageSource)
                .IsRequired();
            modelBuilder.Entity<Drama>()
                .Property(d => d.NoOfEpisodes)
                .IsRequired();
            modelBuilder.Entity<Drama>()
                .Property(d => d.MainName)
                .IsRequired();
            modelBuilder.Entity<Drama>()
                .HasIndex(d => d.MainName)
                .IsUnique();
            
            
            //episodes
            modelBuilder.Entity<Episode>()
                .Property(e =>e.EpisodeNumber)
                .IsRequired();
            modelBuilder.Entity<Episode>()
                .Property(e =>e.EpisodeSource)
                .IsRequired();
            modelBuilder.Entity<Episode>()
                .Property(e =>e.ImageSource)
                .IsRequired();
            
            //downloadLinks
            modelBuilder.Entity<DownloadLink>()
                .Property(l => l.Quality)
                .IsRequired();
            modelBuilder.Entity<DownloadLink>()
                .Property(l => l.AddedTime);
                
            modelBuilder.Entity<DownloadLink>()
                .Property(l => l.AudioLink);

            modelBuilder.Entity<DownloadLink>()
                .Property(l => l.VideoLink);

        }
    }
}