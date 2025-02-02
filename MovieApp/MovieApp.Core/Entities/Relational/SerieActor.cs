namespace MovieApp.Core.Entities.Relational;
public class SerieActor 
{
    public int Id { get; set; }
    public int? ActorId { get; set; }
    public Actor? Actor { get; set; }
    public int? SerieId { get; set; }
    public Serie? Serie { get; set; }
}

