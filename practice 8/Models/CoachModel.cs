using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class CoachModel
{
    public string PhoneNumber { get; private set; }
    public int Age { get; private set; }
    public PositionModel? Position { get; private set; }

    public CoachModel(CoachEntity entity, PositionModel? position = null)
    {
        PhoneNumber = entity.PhoneNumber;
        Age = entity.Age;
        Position = position;
    }

    public override string ToString()
    {
        var position = Position != null ? $"\n{Position}" : string.Empty;
        return $"Coach: \n" +
               $"Phone number: {PhoneNumber}\n" +
               $"Age: {Age}" +
               $"{position}";
    }
}