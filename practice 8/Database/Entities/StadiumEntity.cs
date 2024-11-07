namespace Practice_8.Database.Entities;

[Serializable]
public class StadiumEntity : BaseEntity
{
    public int Code { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public string HeadMaster { get; set; }

    public int StadiumTypeId { get; set; }

    public string Notes { get; set; }

    public StadiumEntity(int code, string address, string phoneNumber, string headMaster, string notes)
    {
        Code = code;
        Address = address;
        PhoneNumber = phoneNumber;
        HeadMaster = headMaster;
        Notes = notes;
    }

    public static StadiumEntity Enter()
    {
        Console.Write("Enter a code: ");
        int code = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter a address: ");
        string address = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a phone number: ");
        string phoneNumber = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a head master: ");
        string headMaster = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter a notes: ");
        string notes = Console.ReadLine() ?? string.Empty;
        return new StadiumEntity(code, address, phoneNumber, headMaster, notes);
    }

    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Code: {Code}\n" +
               $"Address: {Address}\n" +
               $"Phone number: {PhoneNumber}\n" +
               $"Head master: {HeadMaster}\n" +
               $"Notes: {Notes}";
    }
}