using longbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace longbox.Controllers
{
    class FolderManagement
    {

        private static readonly List<RootFolder> _folders = new List<RootFolder>();
        public static List<RootFolder> Folders { get => _folders; }
        
        public static async Task<RootFolder> AddFolder ()
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();

            if (folder != null)
            {
                return SaveFolder(folder);
            } else
            {
                return null;
            }
        }

        private static RootFolder SaveFolder (StorageFolder storageFolder)
        {
            var faToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(storageFolder);
            RootFolder rootFolder = new RootFolder(faToken) { Name = storageFolder.Name, Path = storageFolder.Path };
            _folders.Add(rootFolder);
            return rootFolder;
        }
        
    }
}
