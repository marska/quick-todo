using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuickToDo.Model
{
  public class Settings : INotifyPropertyChanged
  {
    private Guid? _apiToken;

    private Guid? _userId;

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