﻿using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class CustomListItem : BaseEntity
{
	public int CustomListId { get; set; }
	public CustomList CustomList { get; set; }

    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    public int? SeriesId { get; set; }
    public Serie Serie { get; set; }

    
    public DateTime AddedDate { get; set; }
}

