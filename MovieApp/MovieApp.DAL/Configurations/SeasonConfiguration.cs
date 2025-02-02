using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Description)
            .HasMaxLength(500); 

        builder.Property(s => s.SeasonNumber)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasOne(s => s.Serie)
            .WithMany(serie => serie.Seasons)
            .HasForeignKey(s => s.SerieId)
            .OnDelete(DeleteBehavior.Cascade);  

        builder.Property(s => s.ReleaseDate)
            .IsRequired();

        builder.HasMany(s => s.Episodes)
            .WithOne(e => e.Season)
            .HasForeignKey(e => e.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.ReleaseDate);
        builder.HasIndex(s => s.SerieId);

    }
}

