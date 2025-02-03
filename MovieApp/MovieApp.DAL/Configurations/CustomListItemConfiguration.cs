using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class CustomListItemConfiguration : IEntityTypeConfiguration<CustomListItem>
{
    public void Configure(EntityTypeBuilder<CustomListItem> builder)
    {
        builder.HasKey(cli => cli.Id);

        builder.Property(cli => cli.AddedDate)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.HasOne(cli => cli.CustomList)
            .WithMany(cl => cl.ListItems)
            .HasForeignKey(cli => cli.CustomListId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cli => cli.Movie)
            .WithMany()
            .HasForeignKey(cli => cli.MovieId)
            .OnDelete(DeleteBehavior.SetNull); 

        builder.HasOne(cli => cli.Serie)
            .WithMany()
            .HasForeignKey(cli => cli.SeriesId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasIndex(cli => cli.CustomListId);
        builder.HasIndex(cli => cli.MovieId);
        builder.HasIndex(cli => cli.SeriesId);
    }
}

