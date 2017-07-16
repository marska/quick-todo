namespace QuickToDo.Infrastructure
{
  public interface ISettingsService
  {
    Model.Settings GetSettings();

    void SetSettings(Model.Settings settings);
  }
}
