using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(100); 

        builder.Property(u => u.Surname)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasOne(u => u.Stats)
               .WithOne(us => us.User)
               .HasForeignKey<UserStatistics>(us => us.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Ratings)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.SentRequests)
               .WithOne(f => f.Sender)
               .HasForeignKey(f => f.SenderId)
               .OnDelete(DeleteBehavior.NoAction); 

        builder.HasMany(u => u.ReceivedRequests)
               .WithOne(f => f.Receiver)
               .HasForeignKey(f => f.ReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Friends) 
            .WithOne(f => f.User) 
            .HasForeignKey(f => f.UserId) 
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.SentRequests) 
            .WithOne(fr => fr.Sender) 
            .HasForeignKey(fr => fr.SenderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.ReceivedRequests)
            .WithOne(fr => fr.Receiver)
            .HasForeignKey(fr => fr.ReceiverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.ChatMessages)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.UserPreferences)
               .WithOne(up => up.User)
               .HasForeignKey(up => up.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.SupportTickets)
               .WithOne(s => s.User)
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.AssignedTickets)
               .WithOne(s => s.AssignedUser)
               .HasForeignKey(s => s.AssignedUserId)
               .OnDelete(DeleteBehavior.SetNull); 

        builder.HasMany(u => u.CreatedFAQs)
               .WithOne(f => f.CreatedBy)
               .HasForeignKey(f => f.CreatedById)
               .OnDelete(DeleteBehavior.SetNull); 

        builder.HasMany(u => u.CustomLists)
               .WithOne(cl => cl.User)
               .HasForeignKey(cl => cl.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.DownloadLists)
               .WithOne(dl => dl.User)
               .HasForeignKey(dl => dl.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Reviews)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Subscriptions)
               .WithOne(s => s.User)
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Recommendations)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Rentals)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Payments)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Notifications)
               .WithOne(n => n.User)
               .HasForeignKey(n => n.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.Logs)
               .WithOne(l => l.User)
               .HasForeignKey(l => l.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.HostedRooms)
               .WithOne(w => w.HostUser)
               .HasForeignKey(w => w.HostUserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasMany(u => u.JoinedRooms)
               .WithOne(wu => wu.User)
               .HasForeignKey(wu => wu.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasIndex(u => u.Email).IsUnique(); 
        builder.HasIndex(u => u.UserName).IsUnique();
    }
}

