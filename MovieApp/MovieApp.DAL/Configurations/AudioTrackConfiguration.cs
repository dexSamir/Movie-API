using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class AudioTrackConfiguration : IEntityTypeConfiguration<AudioTrack>
{
    public void Configure(EntityTypeBuilder<AudioTrack> builder)
    {
        builder.HasKey(at => at.Id);

        builder.HasOne(at => at.Movie)
            .WithMany(at => at.AudioTracks)
            .HasForeignKey(at => at.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(at => at.Serie)
            .WithMany(at => at.AudioTracks)
            .HasForeignKey(at => at.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(at => at.Language)
            .WithMany(at => at.AudioTracks)
            .HasForeignKey(at => at.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasIndex(a => new { a.MovieId, a.SerieId, a.LanguageId })
               .IsUnique();
    }
}

