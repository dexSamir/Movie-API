using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class CustomListConfiguration :IEntityTypeConfiguration<CustomList>
{
    public void Configure(EntityTypeBuilder<CustomList> builder)
    {
        builder.HasKey(cl => cl.Id);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("date");

        builder.Property(cl => cl.ListName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cl => cl.Description)
            .HasMaxLength(500); 

        builder.HasOne(cl => cl.User)
            .WithMany(u => u.CustomLists)
            .HasForeignKey(cl => cl.UserId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(cl => cl.ListItems)
            .WithOne(cli => cli.CustomList)
            .HasForeignKey(cli => cli.CustomListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

