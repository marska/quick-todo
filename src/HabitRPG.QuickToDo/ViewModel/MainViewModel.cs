using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

    public RelayCommand ShowSettingsViewCommand { get; private set; }

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

    private async void AddTodo()
    {
      try
      {
        Guid? result = await _todoRepository.Create(_todoTask.Text);

        if (result.HasValue && result.Value != Guid.Empty)
        {
          Environment.Exit(0);
        }
        else
        {
          _dialogService.ShowErrorMessage("Error: Something goes wrong");
        }
      }
      catch (HttpRequestException ex)
      {
        if (ex.InnerException != null)
        {
          var inner = ex.InnerException as WebException;

          if (inner != null)
          {
            _dialogService.ShowErrorMessage(inner.Message);
            return;
          }

        }

        _dialogService.ShowErrorMessage(ex.Message);
      }
      catch (Exception ex)
      {
        _dialogService.ShowErrorMessage(ex.Message);
      }
    }

    private void CloseApplication()
    {

      Environment.Exit(0);
    }
  }
}