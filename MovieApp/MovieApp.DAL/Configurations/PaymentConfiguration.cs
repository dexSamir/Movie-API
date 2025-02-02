using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.PaymentDate)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(p => p.PaymentMethod)
            .IsRequired()
            .HasConversion<string>(); 

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.PaymentDate);

        builder.HasOne(p => p.User)
            .WithMany(u => u.Payments)  
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

