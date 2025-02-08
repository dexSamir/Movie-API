using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(true);

        builder.Property(d => d.Surname)
            .HasMaxLength(50)
            .IsUnicode(true);

        builder.Property(d => d.ImageUrl)
            .HasMaxLength(500);

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(TimeOnly.MinValue),
            dt => DateOnly.FromDateTime(dt)
        );

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");


        builder.Property(d => d.Biography)
            .HasMaxLength(2000);

        builder.HasMany(d => d.Movies)
            .WithOne(m => m.Director)
            .HasForeignKey(m => m.DirectorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(d => d.Series)
            .WithOne(s => s.Director)
            .HasForeignKey(s => s.DirectorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp"); 

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");
    }
}

