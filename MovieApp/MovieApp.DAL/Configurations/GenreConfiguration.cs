using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(true);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasMany(g => g.Movies)
            .WithOne(mg => mg.Genre)
            .HasForeignKey(mg => mg.GenreId)
            .OnDelete(DeleteBehavior.Restrict);  

        builder.HasMany(g => g.Series)
            .WithOne(sg => sg.Genre)
            .HasForeignKey(sg => sg.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(g => g.UserPreferences)
            .WithOne(up => up.Genre)
            .HasForeignKey(up => up.GenreId)
            .OnDelete(DeleteBehavior.Cascade);  

        builder.HasIndex(g => g.Name)
            .IsUnique();
    }
}

