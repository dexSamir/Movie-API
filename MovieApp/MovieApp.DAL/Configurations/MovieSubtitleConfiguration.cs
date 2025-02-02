using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.DAL.Configurations;
public class MovieSubtitleConfiguration : IEntityTypeConfiguration<MovieSubtitle>
{
    public void Configure(EntityTypeBuilder<MovieSubtitle> builder)
    {
        builder.HasKey(ms => ms.Id);

        builder.HasIndex(ms => ms.MovieId);
        builder.HasIndex(ms => ms.SubtitleId);
        builder.HasIndex(ms => new { ms.MovieId, ms.SubtitleId }).IsUnique();

        builder.HasOne(ms => ms.Movie)
            .WithMany(m => m.MovieSubtitles)
            .HasForeignKey(ms => ms.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ms => ms.Subtitle)
            .WithMany(s => s.MovieSubtitles)
            .HasForeignKey(ms => ms.SubtitleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

