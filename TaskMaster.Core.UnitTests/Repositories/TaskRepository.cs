using System.Collections.Generic;
using System.Linq;
using TaskMaster.Core.Repositories;
using TaskMaster.Core.Repositories.Models;

namespace TaskMaster.Core.UnitTests.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskModel> _tasks;

    public TaskRepository()
    {
        _tasks = new List<TaskModel>();
    }

    
    public IEnumerable<TaskModel> FetchAll()
    {
        return _tasks.Select(c => (TaskModel) c.Clone());

    }

    public void Add(TaskModel taskModel)
    {
        _tasks.Add((TaskModel)taskModel.Clone());
    }
}