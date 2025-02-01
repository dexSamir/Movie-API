using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(true);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(true);

        builder.Property(x => x.PosterUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Duration)
            .IsRequired();

        builder.Property(x => x.ReleaseDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("date");

        builder.Property(x => x.LikeCount)
            .HasDefaultValue(0);

        builder.Property(x => x.DislikeCount)
            .HasDefaultValue(0);

        builder.HasOne(x => x.Director)
            .WithMany(x => x.Movies)
            .HasForeignKey(x => x.DirectorId)
            .OnDelete(DeleteBehavior.SetNull);

        // many-to-many
        builder.HasMany(x => x.Actors)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.Genres)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.MovieSubtitles)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.Movie);

        // one-to-many
        builder.HasMany(x => x.Ratings)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.Histories)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.Reviews)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.Rentals)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.AudioTracks)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.Recommendations)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

    }
}

