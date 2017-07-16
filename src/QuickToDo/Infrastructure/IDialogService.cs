namespace QuickToDo.Infrastructure
{
  public interface IDialogService
  {
    void ShowErrorMessage(string message);

    void ShowInformationMessage(string message);
  }
}