using System.Threading.Tasks;

namespace QuickToDo.Services
{
  public interface ITaskService
  {
    Task Create(string title);
  }
}
