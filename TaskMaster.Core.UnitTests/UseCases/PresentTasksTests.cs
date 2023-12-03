using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;

using TaskMaster.Core.Repositories.Models;
using TaskMaster.Core.UnitTests.Repositories;
using TaskMaster.Core.UseCases;

using static TaskMaster.Core.UseCases.PresentTask;

namespace TaskMaster.Core.UnitTests.UseCases;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class PresentTasksTests
{
    private readonly TaskRepository _repository;
    private readonly PresentTask _instance;
    private readonly Presenter _presenter;

    protected PresentTasksTests()
    {
        _repository = new TaskRepository();
        _instance = new PresentTask(_repository);
        _presenter = new Presenter();
    }

    private class Presenter : IPresenter
    {
        public IEnumerable<PresentableTask>? TaskList { get; private set; }

        public void Present(IEnumerable<PresentableTask> presentables)
        {
            TaskList = presentables;
        }
    }

    public class WithNoTasks
        :PresentTasksTests
    {
        [Test]
        public async Task PresentEmptyTaskList()
        {
            await _instance.Execute(_presenter);
            Assert.That(_presenter.TaskList, Is.Empty);
        }
    }

    public class WithMultipleTask
        : PresentTasksTests
    {
        [Test]
        public async Task PresentAllTasks()
        {
            const string description = "My first task";
            const string description2 = "My 2nd task";
            
            _repository.Add(new TaskModel { Description = description});
            _repository.Add(new TaskModel { Description = description2});
            
            await _instance.Execute(_presenter);

            Assert.Multiple(
                () =>
                {
                    Assert.That(_presenter.TaskList, Is.Not.Empty);
                    Assert.That(_presenter.TaskList, Has.Exactly(2).Items);
                    Assert.That(_presenter.TaskList, Has.Exactly(1).Items
                        .Matches<PresentableTask>(c => c.Description == description));
                    Assert.That(_presenter.TaskList, Has.Exactly(1).Items
                        .Matches<PresentableTask>(c => c.Description == description2));
                });
        }
    }
}