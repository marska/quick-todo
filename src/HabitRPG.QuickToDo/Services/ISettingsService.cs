using HabitRPG.QuickToDo.Model;

namespace HabitRPG.QuickToDo.Services
{
  public interface ISettingsService
  {
    Settings GetSettings();

    void SetSettings(Settings settings);
  }
}
