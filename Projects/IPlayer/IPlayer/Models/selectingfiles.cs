using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace IPlayer.Models
{
    public  class selectingfiles
    {
        public DeviceInformation DeviceInfo
        {
            get; 
            set;
        }
        public String DeviceName
        {
            get; set;
        }
    }
}
