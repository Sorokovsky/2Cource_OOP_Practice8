namespace Practice_8.Database.Entities;

[Serializable]
public class PlayerEntity : BaseEntity
{
    private double _hight;
    private int _weight;
    
    public string Surname { get; set; }
    public string Name { get; set; }
    public string SecondName { get; set; }
    public DateTime Birthday { get; set; }
    public string Amplua { get; set; }
    public int Number { get; set;}
    public int TeamId { get; set; }
    
    public double Hight
    {
        get => _hight;
        set => _hight = value < 0 ? 0 : value;
    }
    
    public int Weight
    {
        get => _weight;
        set => _weight = value < 0 ? 0 : value;
    }

    public PlayerEntity(double hight, int weight, string surname, string name, string secondName, DateTime birthday, string amplua, int number)
    {
        _hight = hight;
        _weight = weight;
        Surname = surname;
        Name = name;
        SecondName = secondName;
        Birthday = birthday;
        Amplua = amplua;
        Number = number;
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Surname: {Surname}\n" +
               $"Name: {Name}\n" +
               $"Second name: {SecondName}\n" +
               $"Birthday: {Birthday}\n" +
               $"Amplua: {Amplua}\n" +
               $"Number: {Number}\n" +
               $"Hight: {Hight}\n" +
               $"Weight: {Weight}";
    }

    public static PlayerEntity Enter()
    {
        Console.Write("Enter a surname: ");
        var surname = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a name: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a second name: ");
        var secondName = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a year of birthday: ");
        var year = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a number of month of birthday: ");
        var month = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a number of day of birthday: ");
        var day = Convert.ToInt32(Console.ReadLine());
        DateTime dateTime = new DateTime(year, month, day);
        Console.Write("Enter an amplua: ");
        var amplua = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a number of player: ");
        int number = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a hight in meter: ");
        var hight = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter a weight in kg: ");
        var weight = Convert.ToInt32(Console.ReadLine());
        return new PlayerEntity(hight, weight, surname, name, secondName, dateTime, amplua, number);
    }
}