
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace IPlayer.Helpers
{
    public  class LoadFolder
    {
        public static async Task<List<StorageFile>> LoadFolderFiles(StorageFolder storageFolder)
        {
            var queryOptions = new QueryOptions
                       (
                       CommonFileQuery.OrderByName, new List<string> { ".3g2", ".3gp2", ".3gp", ".3gpp", ".m4a", ".m4v", ".mp4v", ".mp4", ".mov", ".m2ts", ".asf", ".wm", ".vob", ".wmv", ".wma", ".aac", ".adt", ".mp3", ".wav", ".avi", ".ac3", ".ec3" }
                       );

            var fileQuery = storageFolder.CreateFileQueryWithOptions(queryOptions);

            var ass = await fileQuery.GetFilesAsync();

            return ass.ToList();
        }
    }
}
