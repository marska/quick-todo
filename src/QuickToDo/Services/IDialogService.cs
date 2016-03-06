namespace QuickToDo.Services
{
  public interface IDialogService
  {
    void ShowErrorMessage(string message);

    void ShowInformationMessage(string message);
  }
}