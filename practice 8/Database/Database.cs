using Practice_8.Database.Entities;
using Practice_8.Database.Security;

namespace Practice_8.Database;

public class DbContext
{
    public Repository<User> Users { get; } = new Repository<User>("users.dat");
    public Repository<CoachEntity> Coaches { get; } = new Repository<CoachEntity>("coaches.dat");
    public Repository<GameEntity> Games { get; } = new Repository<GameEntity>("games.dat");
    public Repository<GoalEntity> Goals { get; } = new Repository<GoalEntity>("goals.dat");
    public Repository<PlayerEntity> Players { get; } = new Repository<PlayerEntity>("players.dat");
    public Repository<PositionEntity> Positions { get; } = new Repository<PositionEntity>("positions.dat");
    public Repository<StadiumEntity> Stadiums { get; } = new Repository<StadiumEntity>("stadiums.dat");
    public Repository<StadiumTypeEntity> StadiumTypes { get; } = new Repository<StadiumTypeEntity>("stadium-types.dat");
    public Repository<TeamEntity> Teams { get; } = new Repository<TeamEntity>("teams.dat");
}