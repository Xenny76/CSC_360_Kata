namespace CSC160_Final
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isAlive)
            {
                return isAlive ? Color.FromArgb("#FFFFFF") : Color.FromArgb("#000000");
            }
            return Color.FromArgb("#000000");
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}