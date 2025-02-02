using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class AnalyticsConfiguration : IEntityTypeConfiguration<Analytics>
{
    public void Configure(EntityTypeBuilder<Analytics> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalUsers)
            .IsRequired();

        builder.Property(x => x.TotalMovies)
            .IsRequired();

        builder.Property(x => x.TotalSeries)
            .IsRequired();

        builder.Property(a => a.TotalRentals)
            .IsRequired();

        builder.Property(a => a.TotalSubscriptions)
            .IsRequired();

        builder.Property(a => a.TotalViews)
            .IsRequired();

        builder.Property(a => a.TotalFavorites)
            .IsRequired();

        builder.Property(a => a.TotalReviews)
            .IsRequired();

        builder.Property(a => a.TotalRatings)
            .IsRequired();

        builder.Property(a => a.TotalDownloads)
            .IsRequired();

        builder.Property(a => a.ActiveUsers)
            .IsRequired();

        builder.Property(a => a.NewUsers)
            .IsRequired();

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(a => a.AnalyticsDate)
            .HasColumnType("date");

        builder.Property(a => a.TotalRevenue)
           .IsRequired()
           .HasColumnType("decimal(18, 2)");

        builder.Property(a => a.AverageRating)
            .IsRequired()
            .HasColumnType("decimal(4,2)");

        builder.Property(a => a.AnalyticsDate)
            .IsRequired();

        builder.HasOne(a => a.MostPopularMovie)
           .WithMany()
           .HasForeignKey(a => a.MostPopularMovieId)
           .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostRentedMovie)
            .WithMany()
            .HasForeignKey(a => a.MostRentedMovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostDownloadedMovie)
            .WithMany()
            .HasForeignKey(a => a.MostDownloadedMovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostReviewedMovie)
            .WithMany()
            .HasForeignKey(a => a.MostReviewedMovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostRatedMovie)
            .WithMany()
            .HasForeignKey(a => a.MostRatedMovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostPopularSerie)
            .WithMany()
            .HasForeignKey(a => a.MostPopularSerieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostRentedSerie)
            .WithMany()
            .HasForeignKey(a => a.MostRentedSerieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostDownloadedSerie)
            .WithMany()
            .HasForeignKey(a => a.MostDownloadedSerieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostReviewedSerie)
            .WithMany()
            .HasForeignKey(a => a.MostReviewedSerieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.MostRatedSerie)
            .WithMany()
            .HasForeignKey(a => a.MostRatedSerieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(a => new { a.AnalyticsDate, a.AnalyticsType })
               .IsUnique();
    }
}

