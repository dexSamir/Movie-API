using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.DAL.Context;
public class AppDbContext : IdentityDbContext<User> 
{
    // Relational
	public DbSet<MovieActor> MovieActors { get; set; }
	public DbSet<MovieGenre> MovieGenres { get; set; }
	public DbSet<MovieSubtitle> MovieSubtitles { get; set; }
    public DbSet<SerieSubtitle> SerieSubtitles { get; set; }
    public DbSet<SerieGenre> SerieGenres { get; set; }
    public DbSet<SerieActor> SerieActors { get; set; }
    // End Relational

    // Main 
    public DbSet<Movie> Movies { get; set; }
	public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
	public DbSet<Genre> Genres { get; set; }
    public DbSet<Episode> Episodes{ get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Serie> Series { get; set; }
    // End Main

    // Ratings 
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<LikeDislike> LikeDislikes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    // End Ratings

    // Language 
    public DbSet<AudioTrack> AudioTracks { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Subtitle> Subtitles { get; set; }
    // End Language

    // Lists and Items 
    public DbSet<CustomList> CustomLists { get; set; }
    public DbSet<CustomListItem> CustomListItems { get; set; }
    public DbSet<DownloadList> DownloadLists { get; set; }
    public DbSet<DownloadListItem> DownloadListItems { get; set; }
    // End Lists and Items

    // Room
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<WatchRoom> WatchRooms { get; set; }
    public DbSet<WatchRoomUser> WatchRoomUsers { get; set; }
    public DbSet<WatchRoomInvite> WatchRoomInvites { get; set; }
    // End Room 

    // Payments
	public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    // End Payments 

	public DbSet<Analytics> Analytics { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<UserPreferences> UserPreferences { get; set; }

	public DbSet<History> Histories { get; set; }
    public DbSet<FAQ> FAQs { get; set; }

    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Friendship> Friendships { get; set; }

    public DbSet<Notification> Notifications{ get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<SupportTicket> SupportTickets { get; set; }
    public DbSet<UserStatistics> UserStatistics { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); 
        base.OnModelCreating(builder);
    }
}

