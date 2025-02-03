using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;
using MovieApp.Core.Helpers.Enums;

namespace MovieApp.DAL.Configurations;
public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
        builder.HasKey(st => st.Id);

        builder.Property(st => st.Title)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(st => st.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(st => st.User)  
            .WithMany(u => u.SupportTickets)  
            .HasForeignKey(st => st.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(st => st.AssignedUser)
            .WithMany(u => u.AssignedTickets)
            .HasForeignKey(st => st.AssignedUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(st => st.ResolvedAt)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(st => st.Status)
            .HasConversion(
                status => status.ToString(),
                status => (ESupportTicket)Enum.Parse(typeof(ESupportTicket), status));
    }
}

