using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
{
    public void Configure(EntityTypeBuilder<UserStatistics> builder)
    {
        builder.HasKey(us => us.Id);

        builder.HasOne(us => us.User)
               .WithOne(u => u.Stats)
               .HasForeignKey<UserStatistics>(us => us.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(us => us.MostWatchedMovieGenre)
               .HasMaxLength(100);

        builder.Property(us => us.GenreWatchTime)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.WeeklyWatchTime)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.MonthlyWatchTime)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.TopWatchedMovies)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.TopWatchedSeries)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.MovieRatings)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<int, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.SerieRatings)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<int, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.MovieLikes)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<int, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.SerieLikes)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<int, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.DownloadedMovies)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.DownloadedSeries)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.DailyActivity)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<DateTime, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.Property(us => us.MonthlyActivity)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null)
               ).HasColumnType("jsonb");

        builder.HasIndex(us => us.UserId).IsUnique();
        builder.HasIndex(us => us.MostWatchedMovieGenre);
        builder.HasIndex(us => us.LastActivity);
        builder.HasIndex(us => us.TotalMoviesWatched);
        builder.HasIndex(us => us.TotalSeriesWatched);
        builder.HasIndex(us => us.TotalWatchTime);

    }
}

