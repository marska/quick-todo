using System;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuickToDo.Helpers;
using QuickToDo.Infrastructure;
using QuickToDo.Model;
using QuickToDo.Services;
using Settings = QuickToDo.Model.Settings;

namespace QuickToDo.ViewModel
{
  public class SettingsViewModel : ViewModelBase
  {
    private readonly IDialogService _dialogService;
    private readonly ISettingsService _settingsService;
    private readonly IAnalyticsTracker _analyticsTracker;

    private Settings _settings;

    public RelayCommand<Window> CloseWindowCommand { get; }
    
    public RelayCommand<Window> SaveSettingsCommand { get; }

    public RelayCommand NavigateToHabitRpgComCommand { get; }

    public SettingsViewModel(IDialogService dialogService, ISettingsService settingsService, IAnalyticsTracker analyticsTracker)
    {
      _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
      _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
      _analyticsTracker = analyticsTracker ?? throw new ArgumentNullException(nameof(analyticsTracker));

      _settings = _settingsService.GetSettings();

      CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
      SaveSettingsCommand = new RelayCommand<Window>(SaveSettings);
      NavigateToHabitRpgComCommand = new RelayCommand(NavigateToHabitRpgCom);
    }

    public Settings Settings
    {
      get => _settings;
      set
      {
        _settings = value;
        RaisePropertyChanged("Settings");
      }
    }

    private void SaveSettings(Window window)
    {
      _settingsService.SetSettings(_settings);
      _dialogService.ShowInformationMessage("Please restart application to apply changes.");
      _analyticsTracker.TrackEventAsync(ViewTitle.Settings, "SaveSettings");

      if (window != null)
      {
        window.Close();
      }
    }

    private void NavigateToHabitRpgCom()
    {
      Process.Start(new ProcessStartInfo("https://habitrpg.com/#/options/settings/api"));
    }

    private void CloseWindow(Window window)
    {
      window?.Close();
    }
  }
}