using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuickToDo.Model
{
  public class Settings : INotifyPropertyChanged
  {
    private string _apiToken;

    private string _userId;
    
    private ServiceTypes _serviceType;
    
    public string ApiToken
    {
      get => _apiToken;
      set
      {
        _apiToken = value;
        OnPropertyChanged();
      }
    }

    public string UserId
    {
      get => _userId;
      set
      {
        _userId = value;
        OnPropertyChanged();
      }
    }

    public ServiceTypes ServiceType
    {
      get => _serviceType;
      set
      {
        _serviceType = value;
        OnPropertyChanged();
      }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}