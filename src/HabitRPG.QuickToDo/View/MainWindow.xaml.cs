using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace HabitRPG.QuickToDo.View
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    //hack: It is only view logic. It's imposible add it in xaml only.
    private void WindowMouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
      {
        DragMove();
      }
    }

    private void TbTodo_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if (Visibility == Visibility.Visible)
      {
        Dispatcher.BeginInvoke((Action)(() => Keyboard.Focus(TbTodo)), DispatcherPriority.Render);
      }
    }
  }
}
