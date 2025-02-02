using MovieApp.Core.Entities.Base;
namespace MovieApp.Core.Entities;
public class UserPreferences : BaseEntity 
{
	public string UserId { get; set; }
	public User User { get; set; }
	public int GenreId { get; set; }
	public Genre Genre { get; set; }
	public int PreferenceWeight { get; set; }
}

