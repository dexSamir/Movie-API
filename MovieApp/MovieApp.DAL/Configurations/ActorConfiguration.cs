using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(x => x.Name)
            .IsUnicode(true)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Surname)
            .IsUnicode(true)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Biography)
            .HasMaxLength(2000);

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("date");
    }
}

