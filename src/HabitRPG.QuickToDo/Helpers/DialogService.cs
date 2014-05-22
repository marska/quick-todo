using System;
using System.Windows;

namespace HabitRPG.QuickToDo.Helpers
{
  public class DialogService : IDialogService
  {
    public void ShowErrorMessage(string message)
    {
      if (message == null)
      {
        throw new ArgumentNullException("message");
      }

      MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
    }
  }
}