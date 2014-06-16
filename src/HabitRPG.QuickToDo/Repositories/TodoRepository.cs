using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitRPG.Client;
using HabitRPG.Client.Model;

namespace HabitRPG.QuickToDo.Repositories
{
  public class TodoRepository : ITodoRepository
  {
    private readonly IHabitRPGClient _habitRpgClient;

    public TodoRepository(IHabitRPGClient habitRpgClient)
    {
      if (habitRpgClient == null)
      {
        throw new ArgumentNullException("habitRpgClient");
      }

      _habitRpgClient = habitRpgClient;
    }

    public async Task<string> Create(string todoTaskText)
    {
      var todo = new Todo
      {
        Id = Guid.NewGuid().ToString(),
        Text = todoTaskText,
        Tags = new Dictionary<Guid, bool>(),
        //Priority = Difficulty.Easy
      };
      
      var response = await _habitRpgClient.CreateTask(todo);

      return response.Id;
    }
  }
}