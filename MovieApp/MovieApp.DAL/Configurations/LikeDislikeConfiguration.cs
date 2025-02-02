using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;
public class LikeDislikeConfiguration : IEntityTypeConfiguration<LikeDislike>
{
    public void Configure(EntityTypeBuilder<LikeDislike> builder)
    {
        builder.HasKey(ld => ld.Id);

        builder.HasOne(ld => ld.User)
                .WithMany()  
                .HasForeignKey(ld => ld.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ld => ld.Movie)
            .WithMany()  
            .HasForeignKey(ld => ld.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ld => ld.Episode)
            .WithMany()  
            .HasForeignKey(ld => ld.EpisodeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ld => ld.Review)
            .WithMany()  
            .HasForeignKey(ld => ld.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ld => ld.IsLike)
            .IsRequired(); 

        builder.HasIndex(ld => new { ld.UserId, ld.MovieId }).IsUnique();
        builder.HasIndex(ld => new { ld.UserId, ld.EpisodeId }).IsUnique();
        builder.HasIndex(ld => new { ld.UserId, ld.ReviewId }).IsUnique();

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");
    }
}

