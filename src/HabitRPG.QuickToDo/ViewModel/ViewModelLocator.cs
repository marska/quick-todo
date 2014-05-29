/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:HabitRPG.QuickToDo"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using System.Net;
using GalaSoft.MvvmLight.Ioc;
using HabitRPG.Client;
using HabitRPG.QuickToDo.Helpers;
using HabitRPG.QuickToDo.Repositories;
using HabitRPG.QuickToDo.Services;
using Microsoft.Practices.ServiceLocation;

namespace HabitRPG.QuickToDo.ViewModel
{
  /// <summary>
  /// This class contains static references to all the view models in the
  /// application and provides an entry point for the bindings.
  /// </summary>
  public class ViewModelLocator
  {
    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class.
    /// </summary>
    public ViewModelLocator()
    {
      ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

      var proxy = WebRequest.DefaultWebProxy;
      proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

      SimpleIoc.Default.Register(
        () =>
          new HabitRpgConfiguration()
          {
            ApiToken = Properties.Settings.Default.ApiToken,
            ServiceUri = new Uri(Properties.Settings.Default.ServiceUri),
            UserId = Properties.Settings.Default.UserId,
            Proxy = proxy
          });

      SimpleIoc.Default.Register<ITodoRepository, TodoRepository>();
      SimpleIoc.Default.Register<IHabitRPGClient, HabitRPGClient>();
      SimpleIoc.Default.Register<ISettingsService, SettingsService>();
      SimpleIoc.Default.Register<IDialogService, DialogService>();
      SimpleIoc.Default.Register<MainViewModel>();
      SimpleIoc.Default.Register<SettingsViewModel>();
    }

    public MainViewModel Main
    {
      get
      {
        return ServiceLocator.Current.GetInstance<MainViewModel>();
      }
    }

    public SettingsViewModel Settings
    {
      get
      {
        return ServiceLocator.Current.GetInstance<SettingsViewModel>();
      }
    }

    public static void Cleanup()
    {
      // TODO Clear the ViewModels
    }
  }
}