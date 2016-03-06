using QuickToDo.Model;

namespace QuickToDo.Services
{
  public interface ISettingsService
  {
    Settings GetSettings();

    void SetSettings(Settings settings);
  }
}
