using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;
using MovieApp.Core.Helpers.Enums;

namespace MovieApp.DAL.Configurations;
public class WatchRoomInviteConfiguration : IEntityTypeConfiguration<WatchRoomInvite>
{
    public void Configure(EntityTypeBuilder<WatchRoomInvite> builder)
    {
        builder.HasKey(wri => wri.Id);

        builder.Property(wri => wri.InviterId)
            .IsRequired();

        builder.Property(wri => wri.InviteeId)
            .IsRequired();

        builder.Property(wri => wri.WatchRoomId)
            .IsRequired();

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(wri => wri.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(), 
                v => (EWatchRoomInviteStatus)Enum.Parse(typeof(EWatchRoomInviteStatus), v) 
            );

        builder.Property(wri => wri.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime");

        builder.HasOne(wri => wri.Inviter)
            .WithMany()
            .HasForeignKey(wri => wri.InviterId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(wri => wri.Invitee)
            .WithMany()
            .HasForeignKey(wri => wri.InviteeId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(wri => wri.WatchRoom)
            .WithMany(wr => wr.Invites)
            .HasForeignKey(wri => wri.WatchRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(wri => wri.InviterId);
        builder.HasIndex(wri => wri.InviteeId);
        builder.HasIndex(wri => wri.WatchRoomId);
    }
}

