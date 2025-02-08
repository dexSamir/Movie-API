using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            .HasMaxLength(50);

        builder.Property(x => x.Biography)
            .HasMaxLength(2000);

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(TimeOnly.MinValue), 
            dt => DateOnly.FromDateTime(dt)        
        );

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasConversion(dateOnlyConverter) 
            .HasColumnType("date");

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");
    }
}

