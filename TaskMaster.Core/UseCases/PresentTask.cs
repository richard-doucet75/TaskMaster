using TaskMaster.Core.Repositories;
using TaskMaster.Core.Repositories.Models;

namespace TaskMaster.Core.UseCases;

public class PresentTask
{
    private readonly ITaskRepository _repository;

    public PresentTask(ITaskRepository repository)
    {
        _repository = repository;
    }

    public interface IPresenter
    {
        void Present(IEnumerable<PresentableTask> presentables);
    }

    public class PresentableTask
    {
        public required string Description { get; init; }
    }

    public async Task Execute(IPresenter presenter)
    {
        await Task.Run(() =>
        {
            presenter.Present(Map2Presentable(_repository.FetchAll()));
        });
    }

    private static IEnumerable<PresentableTask> Map2Presentable(IEnumerable<TaskModel> tasks)
    {
        return tasks.Select(t => new PresentableTask { Description = t.Description });
    }
}