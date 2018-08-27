using longbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.ViewModels
{
    class RootFolderViewModel : NotificationBase<RootFolder>
    {
        public RootFolderViewModel(RootFolder folder) : base(folder) { }

        public String Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => This.Name = value); }
        }

        public String Path
        {
            get { return This.Path; }
            set { SetProperty(This.Path, value, () => This.Path = value); }
        }

    }
}
