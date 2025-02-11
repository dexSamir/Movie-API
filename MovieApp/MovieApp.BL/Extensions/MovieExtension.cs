using MovieApp.Core.Entities;
using MovieApp.Core.Entities.Relational;

namespace MovieApp.BL.Extensions;
public static class MovieExtension
{
    public static List<MovieActor> ToMovieActors(this IEnumerable<int> actorIds)
    {
        return actorIds?.Select(id => new MovieActor { ActorId = id }).ToList() ?? new List<MovieActor>();
    }

    public static List<MovieSubtitle> ToMovieSubtitles(this IEnumerable<int> subtitleIds)
    {
        return subtitleIds?.Select(id => new MovieSubtitle { SubtitleId = id }).ToList() ?? new List<MovieSubtitle>();
    }

    public static List<MovieGenre> ToMovieGenres(this IEnumerable<int> genreIds)
    {
        return genreIds?.Select(id => new MovieGenre { GenreId = id }).ToList() ?? new List<MovieGenre>();
    }

    public static List<AudioTrack> ToAudioTracks(this IEnumerable<int> audioTrackIds)
    {
        return audioTrackIds?.Select(id => new AudioTrack { Id = id }).ToList() ?? new List<AudioTrack>();
    }
}

