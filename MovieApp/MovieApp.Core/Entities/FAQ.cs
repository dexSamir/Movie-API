using MovieApp.Core.Entities.Base;

namespace MovieApp.Core.Entities;
public class FAQ : BaseEntity
{
	public string Question { get; set; }
	public string Answer { get; set; }

    public string CreatedById { get; set; }
    public User CreatedBy { get; set; }
}

