using longbox.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.ViewModels
{
    class FolderViewModel : NotificationBase
    {
        ObservableCollection<RootFolderViewModel> _folders = new ObservableCollection<RootFolderViewModel>();
        public ObservableCollection<RootFolderViewModel> Folders
        {
            get { return _folders; }
            set { SetProperty(ref _folders, value); }
        }

        public async Task LoadFolder()
        {
            var rootFolder = await FolderManagement.AddFolder();
            await LibraryManagement.LoadRootFolder(rootFolder);
            Folders.Add(new RootFolderViewModel(rootFolder));
        }

    }
}
