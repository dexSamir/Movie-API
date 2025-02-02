using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class ActorMovieConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Actor)
            .WithMany(x => x.Movies)
            .HasForeignKey(x => x.ActorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Movie)
            .WithMany(x => x.Actors)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ma => new { ma.MovieId, ma.ActorId }) 
               .IsUnique();

    } 
}

