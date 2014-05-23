using System;
using HabitRPG.QuickToDo.Model;

namespace HabitRPG.QuickToDo.Services
{
  public class SettingsService : ISettingsService
  {
    public Settings GetSettings()
    {
      var settings = new Settings();

      if (!Guid.Empty.Equals(Properties.Settings.Default.ApiToken))
      {
        settings.ApiToken = Properties.Settings.Default.ApiToken;
      }

      if (!Guid.Empty.Equals(Properties.Settings.Default.ApiToken))
      {
        settings.UserId = Properties.Settings.Default.UserId;
      }

      return settings;
    }

    public void SetSettings(Settings settings)
    {
      if (settings == null)
      {
        throw new ArgumentNullException("settings");
      }

      if (settings.ApiToken.HasValue)
      {
        Properties.Settings.Default.ApiToken = settings.ApiToken.Value;
      }

      if (settings.UserId.HasValue)
      {
        Properties.Settings.Default.UserId = settings.UserId.Value;
      }

      Properties.Settings.Default.Save();
    }
  }
}
