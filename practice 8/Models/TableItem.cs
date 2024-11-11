using Practice_8.Database.Entities;

namespace Practice_8.Models;

public class TableItem
{
    public int Mark { get; set; }
    public TeamEntity Team { get; set; }

    public TableItem(TeamEntity team, int mark)
    {
        Mark = mark;
        Team = team;
    }
}