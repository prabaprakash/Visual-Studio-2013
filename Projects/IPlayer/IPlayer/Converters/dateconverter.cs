using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace IPlayer.Converters
{
    class dateconverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value != null)
                {
                    var a = (StorageFile)value;
                    return a.DateCreated;
                }
                return DependencyProperty.UnsetValue;
            }
            catch 
            {
                return DependencyProperty.UnsetValue;
            }
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
