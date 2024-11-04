namespace Practice_8.Database.Entities;

[Serializable]
public class GoalEntity : BaseEntity
{
    private int _count;
    
    public int GameId { get; set; }
    public int PlayerId { get; set; }

    public int Count
    {
        get => _count;
        set => _count = value < 0 ? 0 : value;
    }
}