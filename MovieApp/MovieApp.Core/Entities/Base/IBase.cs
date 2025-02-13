namespace MovieApp.Core.Entities.Base;
public interface IBase
{
    int Id { get; set; }
    DateTime CreatedTime { get; set; }
    DateTime? UpdatedTime { get; set; }
    bool IsDeleted { get; set; }
}

