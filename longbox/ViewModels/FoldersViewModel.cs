//using longbox.Controllers;
using longbox.Controllers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace longbox.ViewModels
{
    class FoldersViewModel : NotificationBase
    {
        private FolderManagement _manager = new FolderManagement();

        private ObservableCollection<RootFolderViewModel> _folders = new ObservableCollection<RootFolderViewModel>();
        public ObservableCollection<RootFolderViewModel> Folders
        {
            get { return _folders; }
            set { SetProperty(ref _folders, value); }
        }

        public async Task LoadFolders ()
        {
            await _manager.LoadFolders();
            _manager.Folders.ForEach(folder => _folders.Add(new RootFolderViewModel(folder)));
        }

        public async Task AddFolder()
        {
            var rootFolder = await _manager.AddFolder();
            Folders.Add(new RootFolderViewModel(rootFolder));
        }

    }
}