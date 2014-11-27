using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace IPlayer.Models
{
    public class Info
    {
        public StorageFile _mediaFile;
        private StorageFile _mediaFile1;

        public StorageFile MediaFile
        {
            get { return _mediaFile1; }
            set { _mediaFile1 = value; }
        }
    }
}
