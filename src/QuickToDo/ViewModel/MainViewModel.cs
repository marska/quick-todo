using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QuickToDo.Helpers;
using QuickToDo.Infrastructure;
using QuickToDo.Model;
using QuickToDo.Services;
using QuickToDo.View;

namespace QuickToDo.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    private readonly ITaskService _taskService;
    private readonly IDialogService _dialogService;
    private readonly IAnalyticsTracker _analyticsTracker;

    private TodoTask _todoTask;

    private bool _isBusy;

    private Visibility _tbTodoVisibility;

    public RelayCommand CloseApplicationCommand { get; private set; }

    public RelayCommand AddTodoCommand { get; private set; }

    public RelayCommand AddNextTodoCommand { get; private set; }

    public RelayCommand ShowSettingsViewCommand { get; private set; }

    public RelayCommand WindowLoadedCommand { get; private set; }

    public MainViewModel(ITaskService taskService, IDialogService dialogService, IAnalyticsTracker analyticsTracker)
    {
      _taskService = taskService ?? throw new ArgumentNullException("taskService");
      _dialogService = dialogService ?? throw new ArgumentNullException("dialogService");
      _analyticsTracker = analyticsTracker ?? throw new ArgumentNullException("analyticsTracker");

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

    public bool IsBusy
    {
      get { return _isBusy; }
      set
      {
        _isBusy = value;
        RaisePropertyChanged("IsBusy");
      }
    }

    public Visibility TbTodoVisibility
    {
      get { return _tbTodoVisibility; }
      set
      {
        _tbTodoVisibility = value;
        RaisePropertyChanged("TbTodoVisibility");
      }
    }


    private async Task AddTodoTask()
    {
      try
      {
        await _taskService.Create(_todoTask.Text);
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
    }

    private async void AddTodo()
    {
      ShowProgressRing();
      await AddTodoTask();
      await _analyticsTracker.TrackEventAsync(ViewTitle.Main, "AddTodo");

      Environment.Exit(0);
    }

    private async void AddNextTodo()
    {
      ShowProgressRing();
      await AddTodoTask();
      await _analyticsTracker.TrackEventAsync(ViewTitle.Main, "AddNextTodo");
      HideProgressRing();

      TodoTask = new TodoTask();
    }

    private void CloseApplication()
    {
      Environment.Exit(0);
    }

    private async void WindowLoaded()
    {
      await _analyticsTracker.TrackPageViewAsync(ViewTitle.Main);
    }

    private void ShowProgressRing()
    {
      IsBusy = true;
      TbTodoVisibility = Visibility.Hidden;
    }

    private void HideProgressRing()
    {
      IsBusy = false;
      TbTodoVisibility = Visibility.Visible;
    }
  }
}