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
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(x => x.Director)
            .WithMany(x => x.Movies)
            .HasForeignKey(x => x.DirectorId);

        builder.HasOne(x => x.WatchList)
            .WithMany(x => x.Movies)
            .HasForeignKey(x => x.WatchListId);

    }
}

