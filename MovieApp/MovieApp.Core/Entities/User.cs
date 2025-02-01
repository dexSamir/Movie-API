using Microsoft.AspNetCore.Identity;
namespace MovieApp.Core.Entities;
public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsVisible { get; set; }

    public int UserStatisticsId { get; set; }
    public UserStatistics Stats { get; set; }

    public ICollection<Friendship> Friends { get; set; }
    public ICollection<ChatMessage> ChatMessages { get; set; }
    public ICollection<UserPreferences> UserPreferences { get; set; } 
    public ICollection<SupportTicket> SupportTicket { get; set; } 
    public ICollection<FAQ> CreatedBy { get; set; }
    public ICollection<UserPreferences> AssignedTicket { get; set; }
    public ICollection<CustomList>? CustomLists { get; set; } = new HashSet<CustomList>();
    public ICollection<DownloadList>? DownloadLists { get; set; } = new HashSet<DownloadList>();
    public ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
    public ICollection<Subscription> Subscriptions { get; set; }
    public ICollection<Recommendation> Recommendations { get; set; }
    public ICollection<Rental> Rentals { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<ActivityLog> Logs { get; set; }
    public ICollection<History> Histories { get; set; }
    public ICollection<WatchRoom> HostedRooms { get; set; }
    public ICollection<WatchRoomUser> JoinedRooms { get; set; }
}

