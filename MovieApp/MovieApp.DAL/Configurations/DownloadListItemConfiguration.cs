using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class DownloadListItemConfiguration : IEntityTypeConfiguration<DownloadListItem>
{
    public void Configure(EntityTypeBuilder<DownloadListItem> builder)
    {
        builder.HasKey(dli => dli.Id);

        builder.Property(dli => dli.DownloadDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(dli => dli.DownloadQuality)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(dli => dli.DownloadList)
            .WithMany(dl => dl.Items)
            .HasForeignKey(dli => dli.DownloadListId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dli => dli.Movie)
            .WithMany()
            .HasForeignKey(dli => dli.MovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(dli => dli.Episode)
            .WithMany()
            .HasForeignKey(dli => dli.EpisodeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(dli => dli.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(dli => dli.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasIndex(dli => dli.DownloadListId);
    }
}

