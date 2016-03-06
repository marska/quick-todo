using System;
using System.Windows;

namespace QuickToDo.Services
{
  public class DialogService : IDialogService
  {
    public void ShowErrorMessage(string message)
    {
      if (message == null)
      {
        throw new ArgumentNullException(nameof(message));
      }

      MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
    }

    public void ShowInformationMessage(string message)
    {
      if (message == null)
      {
        throw new ArgumentNullException(nameof(message));
      }

      MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);
    }

  }
}