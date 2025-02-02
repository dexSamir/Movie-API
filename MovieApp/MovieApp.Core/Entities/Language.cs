using MovieApp.Core.Entities.Base;
namespace MovieApp.Core.Entities;
public class Language : BaseEntity 
{
	public string Code { get; set; }
	public string Icon { get; set; }
	public string Name { get; set; }

    public ICollection<Subtitle> Subtitles { get; set; } = new HashSet<Subtitle>(); 
    public ICollection<AudioTrack> AudioTracks { get; set; }
}

