﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Action)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp"); 

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(x => x.LogDate)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Logs)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId); 
    }
}

