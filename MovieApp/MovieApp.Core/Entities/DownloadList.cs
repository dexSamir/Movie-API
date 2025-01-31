using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class DownloadList : BaseEntity
{
	public string UserId { get; set; }
	public User User { get; set; }

	public ICollection<DownloadListItem> Items { get; set; }
}

