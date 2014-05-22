using System;
using System.Threading.Tasks;
using HabitRPG.QuickToDo.Model;

namespace HabitRPG.QuickToDo.Repositories
{
  public interface ITodoRepository
  {
    Task<Guid?> Create(string todoText);
  }
}
