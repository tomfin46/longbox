using longbox.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace longbox.Controllers
{
    class LibraryManagement
    {
        private static readonly List<IBoxItem> _library = new List<IBoxItem>();
        public static List<IBoxItem> Library { get => _library; }

        public static async Task LoadRootFolder (RootFolder rootFolder)
        {
            var folder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(rootFolder.FutureAccessToken);

            var box = await LoadFolderIntoBox(folder, rootFolder);
            _library.Add(box);
        }

        private static async Task<Box> LoadFolderIntoBox(StorageFolder folder, RootFolder root)
        {
            var box = new Box(folder.Name, folder.Path);
            IReadOnlyList<IStorageItem> itemsList = await folder.GetItemsAsync();
            foreach (var item in itemsList)
            {
                if (item is StorageFolder)
                {
                    var subBox = await LoadFolderIntoBox(item as StorageFolder, root);
                    box.AddItem(subBox);
                }
                else
                {
                    var comic = new Comic((item as StorageFile).DisplayName) { Filename = item.Name, RelativeToRoot = Path.GetRelativePath(root.Path, item.Path) };
                    box.AddItem(comic);
                }
            }

            return box;
        }
    }
}
