using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class SerieConfiguration : IEntityTypeConfiguration<Serie>
{
    public void Configure(EntityTypeBuilder<Serie> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(150); 

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(s => s.PosterUrl)
            .HasMaxLength(500);  

        builder.Property(s => s.TrailerUrl)
            .HasMaxLength(500);

        builder.HasOne(s => s.History)
            .WithMany()
            .HasForeignKey(h => h.HistoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(s => s.Director)
            .WithMany(d => d.Series)
            .HasForeignKey(s => s.DirectorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(s => s.SerieSubtitles)
            .WithOne(ss => ss.Serie)
            .HasForeignKey(ss => ss.SerieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Seasons)
            .WithOne(s=> s.Serie)
            .HasForeignKey(s => s.SerieId)
            .OnDelete(DeleteBehavior.Cascade);  

        builder.HasMany(s => s.Actors)
            .WithOne(a => a.Serie)
            .HasForeignKey(s => s.SerieId)
            .OnDelete(DeleteBehavior.Cascade);  

        builder.HasMany(s => s.Genres)
            .WithOne(g => g.Serie)
            .HasForeignKey(s => s.SerieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Ratings)
            .WithOne(r => r.Serie)
            .HasForeignKey(r => r.SerieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Recommendations)
            .WithOne(r => r.Serie)
            .HasForeignKey(r => r.SerieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.AudioTracks)
            .WithOne(at => at.Serie)
            .HasForeignKey(at => at.SerieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Reviews)
            .WithOne(r => r.Serie)
            .HasForeignKey(s => s.SerieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.Title);
        builder.HasIndex(s => s.DirectorId);
    }
}

