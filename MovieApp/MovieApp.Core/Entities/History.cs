using System;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class History : BaseEntity
{
	public string? UserId { get; set; }
	public User? User { get; set; }
	public ICollection<WatchHistory>? Movies { get; set; } = new HashSet<WatchHistory>(); 
}
