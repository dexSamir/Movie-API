using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class FAQConfiguration : IEntityTypeConfiguration<FAQ>
{
    public void Configure(EntityTypeBuilder<FAQ> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("date");

        builder.Property(f => f.Question)
            .IsRequired()
            .HasMaxLength(500)
            .IsUnicode(true);

        builder.Property(f => f.Answer)
            .IsRequired()
            .HasMaxLength(2000)
            .IsUnicode(true);

        builder.Property(f => f.CreatedById)
            .IsRequired();

        builder.HasOne(f => f.CreatedBy)
            .WithMany(u => u.CreatedFAQS)
            .HasForeignKey(f => f.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(f => f.CreatedById); 
    }
}

