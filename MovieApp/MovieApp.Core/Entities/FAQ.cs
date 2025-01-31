using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class FAQ : BaseEntity
{
	public string Question { get; set; }
	public string Answer { get; set; }

    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }
}

