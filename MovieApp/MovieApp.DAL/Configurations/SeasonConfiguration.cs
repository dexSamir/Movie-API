using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(r => r.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(r => r.UpdatedTime)
            .HasColumnType("timestamp");

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

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(TimeOnly.MinValue),
            dt => DateOnly.FromDateTime(dt)
        );

        builder.Property(x => x.ReleaseDate)
            .IsRequired()
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");


        builder.HasIndex(s => s.ReleaseDate);
        builder.HasIndex(s => s.SerieId);

    }
}

