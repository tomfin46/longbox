using longbox.DataAccess;
using longbox.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace longbox.Controllers
{
    class FolderManagement
    {
        private Database _database; 

        public List<Folder> Folders { get; }

        public FolderManagement()
        {
            _database = new Database();
            Folders = new List<Folder>();
        }

        public async Task LoadFolders()
        {
            var folders = await _database.Folders.GetAll();
            Folders.AddRange(folders);
        }

        public async Task<Folder> AddFolder()
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();

            if (folder != null)
            {
                return await SaveFolder(folder);
            }
            else
            {
                return null;
            }
        }

        private async Task<Folder> SaveFolder(StorageFolder storageFolder)
        {
            var faToken = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(storageFolder);
            Folder folder = new Folder() {
                Name = storageFolder.Name,
                Path = storageFolder.Path,
                FutureAccessToken = faToken
            };
            await _database.Folders.Add(folder);
            Folders.Add(folder);
            return folder;
        }

    }
}