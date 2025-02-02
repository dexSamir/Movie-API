using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class WatchRoomUserConfiguration : IEntityTypeConfiguration<WatchRoomUser>
{
    public void Configure(EntityTypeBuilder<WatchRoomUser> builder)
    {
        builder.HasKey(wru => wru.Id);

        builder.Property(wru => wru.JoinedAt)
            .IsRequired();

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasIndex(wru => wru.UserId);
        builder.HasIndex(wru => wru.WatchRoomId);
        builder.HasIndex(wru => new { wru.UserId, wru.WatchRoomId }).IsUnique();

        builder.HasOne(wru => wru.User)
            .WithMany(u => u.JoinedRooms)
            .HasForeignKey(wru => wru.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wru => wru.WatchRoom)
            .WithMany(wr => wr.Participants)
            .HasForeignKey(wru => wru.WatchRoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

