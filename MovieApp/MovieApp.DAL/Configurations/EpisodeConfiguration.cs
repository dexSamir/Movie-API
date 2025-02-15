using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(true);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(500);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Duration)
            .IsRequired();

        builder.Property(e => e.EpisodeNumber)
            .IsRequired();

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(TimeOnly.MinValue),
            dt => DateOnly.FromDateTime(dt)
        );

        builder.Property(x => x.ReleaseDate)
            .IsRequired()
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");

        builder.Property(e => e.LikeCount)
            .HasDefaultValue(0);

        builder.Property(e => e.DislikeCount)
            .HasDefaultValue(0);

        builder.Property(e => e.SeasonId)
            .IsRequired();

        builder.HasMany(wp => wp.WatchProgresses)
               .WithOne(m => m.Episode)
               .HasForeignKey(wp => wp.EpisodeId)
               .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(e => e.Season)
            .WithMany(s => s.Episodes)
            .HasForeignKey(e => e.SeasonId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(e => e.Reviews)
            .WithOne(r => r.Episode)
            .HasForeignKey(r => r.EpisodeId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(e => e.Ratings)
            .WithOne(r => r.Episode)
            .HasForeignKey(r => r.EpisodeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.SeasonId);
    }
}

