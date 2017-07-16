using System;
using System.Windows.Data;

namespace QuickToDo.Helpers
{
  public class EnumToBoolConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return parameter.Equals(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return parameter;
    }
  }
}
