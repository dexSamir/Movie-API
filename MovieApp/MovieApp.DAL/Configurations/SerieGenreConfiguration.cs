using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.DAL.Configurations; 
public class SerieGenreConfiguration : IEntityTypeConfiguration<SerieGenre>
{
    public void Configure(EntityTypeBuilder<SerieGenre> builder)
    {
        builder.HasKey(sg => sg.Id);

        builder.HasIndex(sg => sg.SerieId);
        builder.HasIndex(sg => sg.GenreId);
        builder.HasIndex(sg => new { sg.SerieId, sg.GenreId }).IsUnique();

        builder.HasOne(sg => sg.Serie)
            .WithMany(s => s.Genres)
            .HasForeignKey(sg => sg.SerieId)
            .OnDelete(DeleteBehavior.SetNull); 

        builder.HasOne(sg => sg.Genre)
            .WithMany(g => g.Series)
            .HasForeignKey(sg => sg.GenreId)
            .OnDelete(DeleteBehavior.SetNull); 
    }
}

