namespace TaskMaster.Core.Repositories.Models;

public class TaskModel : ICloneable
{
    public required string Description { get; init; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}