using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.HasKey(cm => cm.Id);

        builder.Property(cm => cm.Message)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(cm => cm.SentAt)
            .IsRequired()
            .HasColumnType("datetime");

        builder.HasOne(cm => cm.WatchRoom)
            .WithMany(wr => wr.ChatMessages)
            .HasForeignKey(cm => cm.WatchRoomId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(cm => cm.User)
            .WithMany(u => u.ChatMessages)
            .HasForeignKey(cm => cm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(cm => cm.Content)
            .HasMaxLength(1000);

        builder.Property(cm => cm.ImageUrl)
            .HasMaxLength(500);

        builder.Property(cm => cm.LinkUrl)
            .HasMaxLength(500);

        builder.Property(cm => cm.SystemMessage)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("date");
    }
}

