using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(l => l.Id);

        builder.HasIndex(l => l.Code).IsUnique();

        builder.Property(l => l.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(l => l.Icon)
            .IsRequired(false)
            .HasMaxLength(200); 

        builder.HasMany(l => l.AudioTracks)
            .WithOne(a => a.Language)
            .HasForeignKey(a => a.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.Subtitles)
            .WithOne(a => a.Language)
            .HasForeignKey(a => a.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");
    }
}

