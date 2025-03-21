﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(TimeOnly.MinValue),
            dt => DateOnly.FromDateTime(dt)
        );

        builder.Property(x => x.ReleaseDate)
            .IsRequired()
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(x => x.LikeCount)
            .HasDefaultValue(0);

        builder.Property(x => x.DislikeCount)
            .HasDefaultValue(0);

        builder.Property(x => x.AvgRating)
            .HasColumnType("decimal(3,1)") 
            .HasDefaultValue(0)             
            .IsRequired();

        builder.HasOne(x => x.Director)
            .WithMany(x => x.Movies)
            .HasForeignKey(x => x.DirectorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(wp => wp.WatchProgresses)
               .WithOne(m => m.Movie)
               .HasForeignKey(wp => wp.MovieId)
               .OnDelete(DeleteBehavior.Cascade);

        // many-to-many
        builder.HasMany(x => x.Actors)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.Genres)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        builder.HasMany(x => x.MovieSubtitles)
            .WithOne(x => x.Movie)
            .HasForeignKey(x => x.MovieId);

        // one-to-many
        builder.HasMany(x => x.Ratings)
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

        builder.HasIndex(m => m.ReleaseDate);
    }
}

