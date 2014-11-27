using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using Sqlite;
namespace Appa
{
    class GlobalMethods
    {
        public static void LocalSetting(string Name)
        {
            try
            {
                var val = IsolatedStorageSettings.ApplicationSettings;
                val["AdminName"] = Name;
                val.Save();
            }
            catch
            {

            }
        }

        public static string AdminName()
        {
                var val = IsolatedStorageSettings.ApplicationSettings;
                return val["AdminName"].ToString();
        }
    }
    
    
}
