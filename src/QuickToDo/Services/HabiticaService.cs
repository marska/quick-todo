using System;
using HabitRPG.Client;
using HabitRPG.Client.Model;
using Task = System.Threading.Tasks.Task;

namespace QuickToDo.Services
{
  public class HabiticaService : ITaskService
  {
    private readonly IUserClient _userClient;

    public HabiticaService(IUserClient userClient)
    {
      _userClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
    }

    public async Task Create(string todoTaskText)
    {
      var todo = new Todo
      {
        Id = Guid.NewGuid().ToString(),
        Text = todoTaskText
      };

      await _userClient.CreateTaskAsync(todo);
    }
  }
}