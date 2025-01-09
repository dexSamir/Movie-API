using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class Rating : BaseEntity
{
    public int Score { get; set; }
    public ICollection<MovieRating> Movies { get; set; } 
    public string UserId { get; set; }
    public User User { get; set; }

}
