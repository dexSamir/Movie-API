using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;

namespace MovieApp.Core.Entities;
public class DownloadListItem : BaseEntity
{
    public int DownloadListId { get; set; }
    public DownloadList DownloadList { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int EpisodeId { get; set; }
    public Episode Episode { get; set; }

    public EDownloadStatus Status { get;  set; }
    public DateTime DownloadDate { get; set; }
    public string DownloadQuality { get; set; }

}

