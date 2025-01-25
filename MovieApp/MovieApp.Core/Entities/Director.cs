using MovieApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Entities;
public class Director : BaseEntity
{
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public string Surname { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Biography { get; set; }
    public ICollection<Movie>? Movies { get; set; } = new HashSet<Movie>(); 
    public ICollection<Serie>? Series { get; set; } = new HashSet<Serie>();
}
