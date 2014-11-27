using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace IPlayer
{
   public class searchdata:IValueConverter
   {
       public object Convert(object value, Type targetType, object parameter, string language)
       {
           //if (value != null)
           //{
           //    var thumnail = (IRandomAccessStream)value;
           //    var image = new BitmapImage();
           //    image.SetSource(thumnail);
           //    return image;
           //}
           //return DependencyProperty.UnsetValue;

             if (value != null)
           {
               ImageSource im;
               im = new BitmapImage
                   {
                        UriSource = (Uri)value,
                   };
               return im;
           }
           return DependencyProperty.UnsetValue;
       }

       public object ConvertBack(object value, Type targetType, object parameter, string language)
       {
           throw new NotImplementedException();
       }
       public StorageFile _mediafile;

       public StorageFile Mediafile
       {
           get { return _mediafile; }
           set { _mediafile = value; }
       }
   }
}
