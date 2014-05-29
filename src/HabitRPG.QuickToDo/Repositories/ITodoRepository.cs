using System.Threading.Tasks;

namespace HabitRPG.QuickToDo.Repositories
{
  public interface ITodoRepository
  {
    Task<string> Create(string todoText);
  }
}
