using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(mg => mg.Id);

        builder.Property(mg => mg.MovieId)
            .IsRequired(false); 

        builder.Property(mg => mg.GenreId)
            .IsRequired(false); 

        builder.HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(mg => mg.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(mg => mg.GenreId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(mg => new { mg.MovieId, mg.GenreId })
            .IsUnique(); 
    }
}

