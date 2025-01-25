using MovieApp.Core.Entities.Base;
using MovieApp.Core.Entities.Relational;
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
    public ICollection<SerieGenre>? Series { get; set; }
}
