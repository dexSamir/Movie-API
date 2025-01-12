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
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Surname)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Biography)
            .HasMaxLength(200);

        builder.Property(x => x.BirthDate)
            .IsRequired();
    }
}

