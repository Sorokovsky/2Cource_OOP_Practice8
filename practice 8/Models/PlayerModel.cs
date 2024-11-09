using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class PlayerModel
{
    public string Surname { get; private set; }
    public string Name { get; set; }
    public string SecondName { get; private set; }
    public DateTime Birthday { get; private set; }
    public double High { get; private set; }
    public int Weight { get; private set; }
    public string Amplua { get; private set; }
    public int Number { get; private set; }
    public TeamModel? Team { get; private set; }

    public PlayerModel(PlayerEntity entity, TeamModel? team = null)
    {
        Surname = entity.Surname;
        Name = entity.Name;
        SecondName = entity.SecondName;
        Birthday = entity.Birthday;
        High = entity.Hight;
        Weight = entity.Weight;
        Amplua = entity.Amplua;
        Number = entity.Number;
        Team = team;
    }

    public override string ToString()
    {
        var team = Team != null ? $"\n{Team}" : string.Empty;
        return $"Player: \n" +
               $"Surname: {Surname}\n" +
               $"Name: {Name}\n" +
               $"Second name: {SecondName}\n" +
               $"Birthday: {Birthday}\n" +
               $"High: {High}m\n" +
               $"Weight: {Weight}kg\n" +
               $"Amplua: {Amplua}\n" +
               $"Number: {Number}" +
               team;
    }
}