using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasIndex(h => h.UserId);
        builder.HasIndex(h => h.MovieId);
        builder.HasIndex(h => h.SerieId);
        builder.HasIndex(h => h.EpisodeId);

        builder.HasOne(h => h.User)
            .WithMany(u => u.Histories)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.Movie)
            .WithMany()
            .HasForeignKey(h => h.MovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(h => h.Serie)
            .WithMany()
            .HasForeignKey(h => h.SerieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(h => h.Episode)
            .WithMany()
            .HasForeignKey(h => h.EpisodeId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Property(h => h.WatchedAt)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(h => h.StoppedAt)
            .IsRequired(false) 
            .HasColumnType("int");

        builder.Property(h => h.IsCompleted)
            .IsRequired();

        builder.Property(h => h.WatchedDuration)
            .IsRequired()
            .HasColumnType("int");

        builder.HasIndex(h => h.UserId);
    }
}

