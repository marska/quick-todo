using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HabitRPG.QuickToDo.Model;
using HabitRPG.QuickToDo.Services;

namespace HabitRPG.QuickToDo.ViewModel
{
  public class SettingsViewModel : ViewModelBase
  {
    private readonly ISettingsService _settingsService;

    private Settings _settings;

    public RelayCommand<Window> CloseWindowCommand { get; private set; }
    public RelayCommand<Window> SaveSettingsCommand { get; private set; }

    public SettingsViewModel(ISettingsService settingsService)
    {
      _settingsService = settingsService;

      _settings = _settingsService.GetSettings();

      CloseWindowCommand = new RelayCommand<Window>(CloseWindow);

      SaveSettingsCommand = new RelayCommand<Window>(SaveSettings);
    }

    public Settings Settings
    {
      get { return _settings; }
      set
      {
        _settings = value;
        RaisePropertyChanged("Settings");
      }
    }
    private void SaveSettings(Window window)
    {
      _settingsService.SetSettings(_settings);

      if (window != null)
      {
        window.Close();
      }
    }

    private void CloseWindow(Window window)
    {
      if (window != null)
      {
        window.Close();
      }
    }
  }
}