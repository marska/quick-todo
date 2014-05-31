using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GoogleAnalyticsTracker.Simple;
using HabitRPG.QuickToDo.Helpers;
using HabitRPG.QuickToDo.Model;
using HabitRPG.QuickToDo.Repositories;
using HabitRPG.QuickToDo.View;

namespace HabitRPG.QuickToDo.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    private readonly ITodoRepository _todoRepository;
    private readonly IDialogService _dialogService;

    private TodoTask _todoTask;

    public RelayCommand CloseApplicationCommand { get; private set; }

    public RelayCommand AddTodoCommand { get; private set; }

    public RelayCommand AddNextTodoCommand { get; private set; }

    public RelayCommand ShowSettingsViewCommand { get; private set; }

    public RelayCommand WindowLoadedCommand { get; private set; }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel(ITodoRepository todoRepository, IDialogService dialogService)
    {
      if (todoRepository == null)
      {
        throw new ArgumentNullException("todoRepository");
      }

      if (dialogService == null)
      {
        throw new ArgumentNullException("dialogService");
      }

      _todoRepository = todoRepository;
      _dialogService = dialogService;

      _todoTask = new TodoTask();

      CloseApplicationCommand = new RelayCommand(CloseApplication);
      ShowSettingsViewCommand = new RelayCommand(ShowSettingsView);
      AddTodoCommand = new RelayCommand(AddTodo);
      AddNextTodoCommand = new RelayCommand(AddNextTodo);
      WindowLoadedCommand = new RelayCommand(WindowLoaded);
    }

    private void ShowSettingsView()
    {
      var settingView = new SettingsWindow
      {
        Owner = Application.Current.MainWindow
      };

      settingView.ShowDialog();
    }

    public TodoTask TodoTask
    {
      get { return _todoTask; }
      set
      {
        _todoTask = value;
        RaisePropertyChanged("TodoTask");
      }
    }

    private async Task<string> AddTodoTask()
    {
      try
      {
        return await _todoRepository.Create(_todoTask.Text);
      }
      catch (HttpRequestException ex)
      {
        if (ex.InnerException != null)
        {
          var inner = ex.InnerException as WebException;

          if (inner != null)
          {
            _dialogService.ShowErrorMessage(inner.Message);
          }
        }

        _dialogService.ShowErrorMessage(ex.Message);
      }
      catch (Exception ex)
      {
        _dialogService.ShowErrorMessage(ex.Message);
      }

      return string.Empty;
    }

    private async void AddTodo()
    {
      await AddTodoTask();

      using (var simpleTracker = new SimpleTracker("UA-51469561-2", string.Empty))
      {
        simpleTracker.ThrowOnErrors = true;
        await simpleTracker.TrackEventAsync("MainWindow", "AddTodo");
      }

      Environment.Exit(0);
    }

    private async void AddNextTodo()
    {
      await AddTodoTask();

      using (var simpleTracker = new SimpleTracker("UA-51469561-2", string.Empty))
      {
        simpleTracker.ThrowOnErrors = true;
        await simpleTracker.TrackEventAsync("MainWindow", "AddNextTodo");
      }

      TodoTask = new TodoTask();
    }

    private void CloseApplication()
    {
      Environment.Exit(0);
    }

    private async void WindowLoaded()
    {
      using (var simpleTracker = new SimpleTracker("UA-51469561-2", string.Empty))
      {
        await simpleTracker.TrackPageViewAsync("Main", "View/MainWindow");
      }
    }
  }
}