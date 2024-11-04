namespace Practice_8.Database.Entities;

[Serializable]
public class CoachEntity : BaseEntity
{
    private int _age;
    
    public string PhoneNumber { get; set; }
    
    public int PositionId { get; set; }

    public int Age
    {
        get => _age;
        set => _age = value < 0 ? 0 : value;
    }

    public CoachEntity(string phoneNumber, int age)
    {
        PhoneNumber = phoneNumber;
        Age = age;
    }

    public static CoachEntity Enter()
    {
        Console.Write("Enter a phone number of coach: ");
        string phoneNumber = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter an age of coach: ");
        int age = Convert.ToInt32(Console.ReadLine());
        return new CoachEntity(phoneNumber, age);
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Phone Number: {PhoneNumber}\n" +
               $"Age: {Age}";
    }
}