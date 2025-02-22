using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Movie)
            .WithMany(m => m.Ratings)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Serie)
            .WithMany(s => s.Ratings)
            .HasForeignKey(r => r.SerieId)
            .OnDelete(DeleteBehavior.SetNull); 

        builder.HasOne(r => r.Episode)
            .WithMany(e => e.Ratings)
            .HasForeignKey(r => r.EpisodeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasIndex(r => new { r.UserId, r.MovieId }).IsUnique();
        builder.HasIndex(r => new { r.UserId, r.SerieId }).IsUnique();
        builder.HasIndex(r => new { r.UserId, r.EpisodeId }).IsUnique();

        builder.Property(r => r.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(r => r.UpdatedTime)
            .HasColumnType("timestamp");
    }
}

