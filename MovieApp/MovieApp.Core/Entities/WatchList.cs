using System;
using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;
namespace MovieApp.Core.Entities;
public class WatchList : BaseEntity
{
	public string? UserId { get; set; }
	public User? User { get; set; }
	public ICollection<Movie>? Movies { get; set; }
	public ICollection<Serie>? Series { get; set; }
    public EWatchListType? ListType { get; set; }
}