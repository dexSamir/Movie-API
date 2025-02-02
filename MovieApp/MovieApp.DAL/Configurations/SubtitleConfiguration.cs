using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class SubtitleConfiguration : IEntityTypeConfiguration<Subtitle>
{
    public void Configure(EntityTypeBuilder<Subtitle> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.LanguageId)
               .IsRequired();

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasOne(s => s.Language)
               .WithMany(l => l.Subtitles) 
               .HasForeignKey(s => s.LanguageId)
               .OnDelete(DeleteBehavior.Cascade);  

        builder.HasMany(s => s.SerieSubtitles)
               .WithOne(ss => ss.Subtitle)
               .HasForeignKey(ss => ss.SubtitleId)
               .OnDelete(DeleteBehavior.Cascade);  

        builder.HasMany(s => s.MovieSubtitles)
               .WithOne(ms => ms.Subtitle)
               .HasForeignKey(ms => ms.SubtitleId)
               .OnDelete(DeleteBehavior.Cascade);  

        builder.HasIndex(s => s.LanguageId);
        builder.HasIndex(s => new { s.LanguageId, s.Id }); 
    }
}

