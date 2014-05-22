using System;
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

    public async Task<Guid?> Create(string todoTaskText)
    {
      var todo = new Todo
      {
          Text = todoTaskText
      };

      var response = await _habitRpgClient.CreateTask(todo);

      //var habitTask = new Todo
      //{
      //  Text = todoTaskText,
      //  Attribute = Attribute.Strength,
      //  Priority = Difficulty.Hard,
      //  Value = 0,
      //  Notes = string.Empty,
      //  DateCreated = DateTime.Now,
      //  Id = Guid.NewGuid(),
      //  //Tags = new Dictionary<Guid, bool>()
      //  //{
      //  //  { Guid.Parse("5832e293-1c2e-4930-997a-cd5637c65460"), true } //today  
      //  //},
      //  //Checklist = new List<Checklist>()
      //  //{
      //  //  { new Checklist() { Id = Guid.NewGuid(),Text = "pod zadanie" } }
      //  //}
      //};

      //////todo: zmiecic na wait i zeby zwracal prawdziw guid
      //_habitRpgService.CreateTask(habitTask);

      return response.Id;
    }
  }
}