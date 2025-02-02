using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        builder.HasKey(fr => fr.Id);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(fr => fr.SentAt)
                .IsRequired()
                .HasColumnType("datetime");

        builder.HasOne(fr => fr.Sender)
            .WithMany(u=> u.SentRequests)  
            .HasForeignKey(fr => fr.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(fr => fr.Receiver)
            .WithMany(u => u.ReceivedRequests)
            .HasForeignKey(fr => fr.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(fr => fr.Status)
                .IsRequired()
                .HasConversion<int>();

        builder.HasIndex(fr => fr.SenderId);

        builder.HasIndex(fr => fr.ReceiverId);

        builder.HasIndex(fr => fr.Status);

        builder.HasIndex(fr => fr.SentAt); 
    }
}

