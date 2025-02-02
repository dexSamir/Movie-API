using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class UserPreferencesConfiguration : IEntityTypeConfiguration<UserPreferences>
{
    public void Configure(EntityTypeBuilder<UserPreferences> builder)
    {
        builder.HasKey(up => up.Id);

        builder.HasOne(up => up.User)  
            .WithMany(u => u.UserPreferences)  
            .HasForeignKey(up => up.UserId)  
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Genre)
            .WithMany()
            .HasForeignKey(up => up.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(up => up.PreferenceWeight)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasIndex(up => new { up.UserId, up.GenreId })
               .IsUnique();

        builder.HasIndex(up => up.PreferenceWeight);
    }
}

