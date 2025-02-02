using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id); 

        builder.Property(s => s.StartTime)
               .IsRequired();

        builder.Property(s => s.EndTime)
               .IsRequired();

        builder.Property(s => s.IsActive)
               .HasDefaultValue(true);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.HasOne(s => s.Plan)
               .WithMany(p=> p.Subscriptions) 
               .HasForeignKey(s => s.PlanId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(s => s.User)
               .WithMany(u => u.Subscriptions) 
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.UserId);
        builder.HasIndex(s => s.PlanId);
        builder.HasIndex(s => s.EndTime);
    }
}

