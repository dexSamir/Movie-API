using MovieApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Entities;
public  class Genre : BaseEntity
{
    public string Name { get; set; }
    public ICollection<MovieGenre>? Movies { get; set; } 
}
