using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities;

namespace MovieApp.DAL.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Content)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(x => x.CreatedTime)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(x => x.UpdatedTime)
            .HasColumnType("timestamp");

        builder.Property(r => r.MovieId)
               .IsRequired(false); 

        builder.Property(r => r.SerieId)
               .IsRequired(false); 

        builder.Property(r => r.EpisodeId)
               .IsRequired(false); 

        builder.Property(r => r.UserId)
               .IsRequired(); 

        builder.Property(r => r.ReviewDate)
               .IsRequired(false); 

        builder.Property(r => r.LikeCount)
               .HasDefaultValue(0); 

        builder.Property(r => r.DislikeCount)
               .HasDefaultValue(0);

        builder.Property(r => r.ParentReviewId)
               .IsRequired(false); 

        builder.HasOne(r => r.Movie)
               .WithMany(m => m.Reviews)
               .HasForeignKey(r => r.MovieId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Serie)
               .WithMany(s => s.Reviews)
               .HasForeignKey(r => r.SerieId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Episode)
               .WithMany(e => e.Reviews)
               .HasForeignKey(r => r.EpisodeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.User)
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ParentReview)
               .WithMany(r => r.Replies)
               .HasForeignKey(r => r.ParentReviewId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => r.MovieId);
        builder.HasIndex(r => r.SerieId);
        builder.HasIndex(r => r.EpisodeId);
        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.ParentReviewId);
    }
}