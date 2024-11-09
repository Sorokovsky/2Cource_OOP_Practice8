using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class PositionModel
{
    public string Name { get; private set; }
    public string Requirements { get; private set; }
    public int Salary { get; private set; }

    public PositionModel(PositionEntity entity)
    {
        Name = entity.Name;
        Requirements = entity.Requirements;
        Salary = entity.Salary;
    }

    public override string ToString()
    {
        return $"Position: \n" +
               $"Name: {Name}\n" +
               $"Requirements: {Requirements}\n" +
               $"Salary: {Salary}uah";
    }
}