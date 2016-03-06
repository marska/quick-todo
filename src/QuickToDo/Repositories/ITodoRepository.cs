using System.Threading.Tasks;

namespace QuickToDo.Repositories
{
  public interface ITodoRepository
  {
    Task<string> Create(string todoText);
  }
}
