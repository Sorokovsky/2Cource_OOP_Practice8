using Practice_8.Database.Entities;
using Practice_8.Database.IndexSystem;
using Practice_8.Database.Security;
using Index = Practice_8.Database.IndexSystem.Index;

namespace Practice_8.Database;

public class DbContext
{
    private static DbContext? _instance;
    private readonly Indexing _indexing;
    private const string Folder = "database/";

    public static DbContext Singleton()
    {
        if (_instance == null)
        {
            _instance = new DbContext();
        }
        return _instance;
    }

    private DbContext()
    {
        _indexing = new Indexing(this);
        if (!Directory.Exists(Folder))
        {
            Directory.CreateDirectory(Folder);
        }
    }

    public List<Index> GetDependenceOnTypes(BaseEntity entity) => _indexing.GetDependenceOnTypes(entity);
    
    public bool CanDelete(BaseEntity entity)
    {
        return _indexing.GetDependencies(entity).Count == 0;
    }
    
    public Repository<User> Users { get; } = new(Folder + "users.dat");
    public Repository<CoachEntity> Coaches { get; } = new(Folder + "coaches.dat");
    public Repository<GameEntity> Games { get; } = new(Folder + "games.dat");
    public Repository<GoalEntity> Goals { get; } = new(Folder + "goals.dat");
    public Repository<PlayerEntity> Players { get; } = new(Folder + "players.dat");
    public Repository<PositionEntity> Positions { get; } = new(Folder + "positions.dat");
    public Repository<StadiumEntity> Stadiums { get; } = new(Folder + "stadiums.dat");
    public Repository<StadiumTypeEntity> StadiumTypes { get; } = new(Folder + "stadium-types.dat");
    public Repository<TeamEntity> Teams { get; } = new(Folder + "teams.dat");
}