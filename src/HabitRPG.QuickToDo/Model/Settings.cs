using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HabitRPG.QuickToDo.Model
{
  public class Settings : INotifyPropertyChanged
  {
    private Guid? _apiToken;

    private Guid? _userId;

    private string _proxyHost;

    private int _proxyPort;

    public Guid? ApiToken
    {
      get
      {
        return _apiToken;
      }
      set
      {
        _apiToken = value;
        OnPropertyChanged();
      }
    }

    public Guid? UserId
    {
      get
      {
        return _userId;
      }
      set
      {
        _userId = value;
        OnPropertyChanged();
      }
    }
    
    public string ProxyHost
    {
      get
      {
        return _proxyHost;
      }
      set
      {
        _proxyHost = value;
        OnPropertyChanged();
      }
    }

    public int ProxyPort
    {
      get
      {
        return _proxyPort;
      }
      set
      {
        _proxyPort = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;

      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}