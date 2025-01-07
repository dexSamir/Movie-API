using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Entities.Base;

public class BaseEntity
{
    public int Id{ get; set; }
    public DateTime CreatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
