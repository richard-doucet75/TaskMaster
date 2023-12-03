using TaskMaster.Core.Repositories.Models;

namespace TaskMaster.Core.Repositories;

public interface ITaskRepository
{
    IEnumerable<TaskModel> FetchAll();
}