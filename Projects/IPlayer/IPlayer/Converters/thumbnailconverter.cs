using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace IPlayer.Converters
{
    class thumbnailconverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                //var thumbnailStream = (IRandomAccessStream)value;
                //var image = new BitmapImage();
                //image.SetSource(thumbnailStream);
                //return image;
                RandomAccessStreamReference rasf = RandomAccessStreamReference.CreateFromUri((Uri)value);
                BitmapImage bitmapImage = new BitmapImage();
                var f = Task.Run(async () =>
                {
                    var ras = await rasf.OpenReadAsync();
                    return ras;
                });
                f.GetAwaiter().OnCompleted(() => bitmapImage.SetSource(f.Result));
               
                return bitmapImage;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
