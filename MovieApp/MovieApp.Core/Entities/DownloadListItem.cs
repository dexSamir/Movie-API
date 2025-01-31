using MovieApp.Core.Entities.Base;
namespace MovieApp.Core.Entities;
public class DownloadListItem : BaseEntity
{
    public int DownloadListId { get; set; }
    public DownloadList DownloadList { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int EpisodeId { get; set; }
    public Episode Episode { get; set; }

    public DateTime DownloadDate { get; set; }
    public string DownloadQuality { get; set; }

}

