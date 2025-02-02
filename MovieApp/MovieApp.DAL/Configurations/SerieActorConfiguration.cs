using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.DAL.Configurations; 
public class SerieActorConfiguration : IEntityTypeConfiguration<SerieActor>
{
    public void Configure(EntityTypeBuilder<SerieActor> builder)
    {

        builder.HasKey(sa => sa.Id);

        builder.HasIndex(sa => sa.SerieId);
        builder.HasIndex(sa => sa.ActorId);
        builder.HasIndex(sa => new { sa.SerieId, sa.ActorId }).IsUnique();

        builder.HasOne(sa => sa.Actor)
           .WithMany(a => a.Series)
           .HasForeignKey(sa => sa.ActorId)
           .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(sa => sa.Serie)
            .WithMany(s => s.Actors)
            .HasForeignKey(sa => sa.SerieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

