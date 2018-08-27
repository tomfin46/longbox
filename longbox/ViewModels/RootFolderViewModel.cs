using longbox.Model;

namespace longbox.ViewModels
{
    class RootFolderViewModel : NotificationBase<Folder>
    {
        public RootFolderViewModel(Folder folder) : base(folder) { }

        public string Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => This.Name = value); }
        }

        public string Path
        {
            get { return This.Path; }
            set { SetProperty(This.Path, value, () => This.Path = value); }
        }
    }
}