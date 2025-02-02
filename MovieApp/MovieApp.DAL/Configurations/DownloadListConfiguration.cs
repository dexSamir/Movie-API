using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class DownloadListConfiguration : IEntityTypeConfiguration<DownloadList>
{
    public void Configure(EntityTypeBuilder<DownloadList> builder)
    {
        builder.HasKey(dl => dl.Id);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasOne(dl => dl.User)
            .WithMany(u => u.DownloadLists)
            .HasForeignKey(dl => dl.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(dl => dl.Items)
            .WithOne(dli => dli.DownloadList)
            .HasForeignKey(dli => dli.DownloadListId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(dl => dl.UserId);
    }
}

