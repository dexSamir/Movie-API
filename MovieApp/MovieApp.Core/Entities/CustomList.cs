using MovieApp.Core.Entities.Base;
namespace MovieApp.Core.Entities;
public class CustomList : BaseEntity
{
	public string? UserId { get; set; }
	public User? User { get; set; }

    public string ListName { get; set; } 
    public string Description { get; set; }

    public ICollection<WatchlistItem> WatchlistItems { get; set; }
}