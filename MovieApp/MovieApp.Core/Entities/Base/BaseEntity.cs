﻿
namespace MovieApp.Core.Entities.Base;
public abstract class BaseEntity : IBase
{
    public int Id { get; set; } 
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
