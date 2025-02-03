using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");
        builder.HasKey(n => n.Id);

        builder.HasOne(n => n.User)
            .WithMany() 
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(n => n.Message)
            .IsRequired()
            .HasMaxLength(1000) 
            .IsUnicode(true);

        builder.Property(n => n.SendDate)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(n => n.IsRead)
            .IsRequired()
            .HasDefaultValue(false); 

        builder.HasIndex(n => n.UserId);

    }
}

