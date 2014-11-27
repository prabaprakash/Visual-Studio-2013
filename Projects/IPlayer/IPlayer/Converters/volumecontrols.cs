using Windows.UI.Xaml.Data;

namespace IPlayer.Converters
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class Volumecontrols :IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the volumecontrols class.
        /// </summary>
        public Volumecontrols()
        {
        }

        public object Convert(object value, System.Type targetType, object parameter, string language)
        {
            double i = (double) value;
            return i*100;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language)
        {
            double i = (double)value;
            return i/100;
        }
    }
}