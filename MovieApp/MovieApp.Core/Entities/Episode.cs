﻿using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Episode : BaseEntity
{
	public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public int EpisodeNumber { get; set; }
    public DateOnly ReleaseDate { get; set; }

    public int LikeCount { get; set; } = 0;
    public int DislikeCount { get; set; } = 0;

    public int SeasonId { get; set; }
    public Season Season { get; set; }

    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<WatchProgress> WatchProgresses { get; set; }
}

