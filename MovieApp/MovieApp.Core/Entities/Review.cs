using MovieApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Entities;
public class Review : BaseEntity
{
    public string Content { get; set; }
    public int? Score { get; set; }
    public int? MovieId { get; set; }    
    public Movie? Movie { get; set; }
    public int? SerieId { get; set; }
    public Serie? Serie { get; set; }
    public int? EpisodeId { get; set; }
    public Episode? Episode { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public int? RatingId { get; set; }
    public Rating? Rating { get; set; }
}
