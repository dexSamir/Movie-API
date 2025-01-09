using MovieApp.Core.Entities.Base;
using MovieApp.Core.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Entities; 
public class Subscription : BaseEntity 
{
    public DateTime EndTime { get; set; }
    public bool IsActive => EndTime > DateTime.UtcNow; 
    public Plan Plan { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}
