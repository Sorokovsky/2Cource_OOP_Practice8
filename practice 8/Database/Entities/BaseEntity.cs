namespace Practice_8.Database.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}\n";
    }
}