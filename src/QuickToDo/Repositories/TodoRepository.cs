using System;
using System.Threading.Tasks;
using HabitRPG.Client;
using HabitRPG.Client.Model;

namespace QuickToDo.Repositories
{
  public class TodoRepository : ITodoRepository
  {
    private readonly IUserClient _userClient;

    public TodoRepository(IUserClient userClient)
    {
      if (userClient == null)
      {
        throw new ArgumentNullException("userClient");
      }

      _userClient = userClient;
    }

    public async Task<string> Create(string todoTaskText)
    {
      var todo = new Todo
      {
        Id = Guid.NewGuid().ToString(),
        Text = todoTaskText
      };

      var response = await _userClient.CreateTaskAsync(todo);

      return response.Id;
    }
  }
}