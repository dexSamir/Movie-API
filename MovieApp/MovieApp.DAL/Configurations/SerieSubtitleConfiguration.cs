using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.DAL.Configurations;
public class SerieSubtitleConfiguration : IEntityTypeConfiguration<SerieSubtitle>
{
    public void Configure(EntityTypeBuilder<SerieSubtitle> builder)
    {
        builder.HasKey(ss => ss.Id);

        builder.HasIndex(ss => ss.SerieId);
        builder.HasIndex(ss => ss.SubtitleId);
        builder.HasIndex(ss => new { ss.SerieId, ss.SubtitleId }).IsUnique();

        builder.HasOne(sg => sg.Serie)
            .WithMany(s => s.SerieSubtitles)
            .HasForeignKey(sg => sg.SerieId)
            .OnDelete(DeleteBehavior.SetNull); 

        builder.HasOne(sg => sg.Subtitle)
            .WithMany(g => g.SerieSubtitles)
            .HasForeignKey(sg => sg.SubtitleId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

