using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class StadiumModel
{
    public int Code { get; private set; }
    public string Address { get; private set; }
    public string PhoneNumber { get; private set; }
    public string HeadMaster { get; private set; }
    public string Notes { get; private set; }
    public StadiumTypeModel? StadiumType { get; private set; }

    public StadiumModel(StadiumEntity entity, StadiumTypeModel? type = null)
    {
        Code = entity.Code;
        Address = entity.Address;
        PhoneNumber = entity.PhoneNumber;
        HeadMaster = entity.HeadMaster;
        Notes = entity.Notes;
        StadiumType = type;
    }

    public override string ToString()
    {
        var type = StadiumType != null ? $"\n{StadiumType}" : string.Empty;
        return "Stadium:\n" +
               $"Code: {Code}\n" +
               $"Address: {Address}\n" +
               $"Phone number: {PhoneNumber}\n" +
               $"Head master: {HeadMaster}\n" +
               $"Notes: {Notes}" +
               $"{type}";
    }
}