using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class WatchRoomConfiguration : IEntityTypeConfiguration<WatchRoom>
{
    public void Configure(EntityTypeBuilder<WatchRoom> builder)
    {
        builder.HasKey(wr => wr.Id);

        builder.Property(wr => wr.RoomName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(wr => wr.ShareCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(wr => wr.Password)
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp");

        builder.HasIndex(wr => wr.RoomName);
        builder.HasIndex(wr => wr.ShareCode).IsUnique();
        builder.HasIndex(wr => wr.HostUserId);
        builder.HasIndex(wr => wr.MovieId);
        builder.HasIndex(wr => wr.SerieId);

        builder.HasOne(wr => wr.HostUser)
            .WithMany(u => u.HostedRooms)
            .HasForeignKey(wr => wr.HostUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(wr => wr.Movie)
            .WithMany()
            .HasForeignKey(wr => wr.MovieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(wr => wr.Serie)
            .WithMany()
            .HasForeignKey(wr => wr.SerieId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(wr => wr.Participants)
            .WithOne(wru => wru.WatchRoom)
            .HasForeignKey(wru => wru.WatchRoomId);

        builder.HasMany(wr => wr.ChatMessages)
            .WithOne(cm => cm.WatchRoom)
            .HasForeignKey(cm => cm.WatchRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(wr => wr.Invites)
            .WithOne(wru => wru.WatchRoom)
            .HasForeignKey(wru => wru.WatchRoomId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}

