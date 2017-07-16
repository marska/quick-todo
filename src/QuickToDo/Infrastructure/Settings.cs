using System;
using QuickToDo.Model;

namespace QuickToDo.Infrastructure
{
  public class Settings : ISettingsService
  {
    public Model.Settings GetSettings()
    {
      var settings = new Model.Settings
      {
        ApiToken = Properties.Settings.Default.ApiToken,
        UserId = Properties.Settings.Default.UserId
      };
      
      Enum.TryParse(Properties.Settings.Default.ServiceType, out ServiceTypes serviceType);
      settings.ServiceType = serviceType;

      return settings;
    }

    public void SetSettings(Model.Settings settings)
    {
      Properties.Settings.Default.ApiToken = settings?.ApiToken ?? throw new ArgumentNullException(nameof(settings));
      Properties.Settings.Default.UserId = settings.UserId;
      Properties.Settings.Default.ServiceType = settings.ServiceType.ToString();

      Properties.Settings.Default.Save();
    }
  }
}
