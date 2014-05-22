using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HabitRPG.QuickToDo.Model
{
  public class TodoTask : INotifyPropertyChanged
  {
    private string _text;
    private string _notes;

    public string Text
    {
      get
      {
        return _text;
      }
      set
      {
        _text = value;
        OnPropertyChanged();
      }
    }

    public string Notes
    {
      get
      {
        return _notes;
      }
      set
      {
        if (_notes == value)
        {
          return;
        }
        _notes = value;
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