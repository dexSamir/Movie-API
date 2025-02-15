using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class WatchProgressConfiguration : IEntityTypeConfiguration<WatchProgress>
{
    public void Configure(EntityTypeBuilder<WatchProgress> builder)
    {
        builder.HasKey(wp => wp.Id);

        builder.HasOne(wp => wp.Movie)
               .WithMany(m => m.WatchProgresses)
               .HasForeignKey(wp => wp.MovieId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(wp => wp.Serie)
               .WithMany(s => s.WatchProgresses)
               .HasForeignKey(wp => wp.SerieId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wp => wp.Episode)
               .WithMany(e => e.WatchProgresses)
               .HasForeignKey(wp => wp.EpisodeId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(wp => wp.User)
               .WithMany(u => u.WatchProgresses)
               .HasForeignKey(wp => wp.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.Property(wp => wp.StartTime).IsRequired();
        builder.Property(wp => wp.EndTime).IsRequired(false);
        builder.Property(wp => wp.CurrentTime).IsRequired();
        builder.Property(wp => wp.LastUpdated).IsRequired();
        builder.Property(wp => wp.IsWatching).IsRequired();
        builder.Property(wp => wp.PausedAt).IsRequired(false);

        builder.HasIndex(wp => new { wp.MovieId, wp.UserId });

        builder.HasIndex(wp => new { wp.SerieId, wp.UserId });

        builder.HasIndex(wp => new { wp.EpisodeId, wp.UserId });

        builder.HasIndex(wp => wp.IsWatching);

        builder.HasIndex(wp => wp.LastUpdated);
    }
}

