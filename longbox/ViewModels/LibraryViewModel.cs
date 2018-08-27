using longbox.Controllers;
using longbox.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.ViewModels
{
    class LibraryViewModel : NotificationBase
    {
        private bool _isHome;

        private Box _box;
        public Box CurrentBox { get => _box; set { SetProperty(ref _box, value); } }

        private ObservableCollection<IBoxItem> _items;
        public ObservableCollection<IBoxItem> Items { get => _items; set { SetProperty(ref _items, value); } }
        
        public void Initialise ()
        {
            SetItems(LibraryManagement.Library);
            _isHome = true;
            CurrentBox = new Box("Home");
        }

        public void OpenBox(Box box)
        {
            if (_isHome)
            {
                _isHome = false;
            }

            SetBox(box);
        }

        public void CloseBox ()
        {
            if (_box.Parent != null)
            {
                SetBox(_box.Parent as Box);
            }
            else if(!_isHome)
            {
                Initialise();
            }
        }

        private void SetBox(Box box)
        {
            CurrentBox = box;
            SetItems(_box.GetItems());
        }

        private void SetItems(IEnumerable<IBoxItem> items)
        {
            Items = new ObservableCollection<IBoxItem>(items);
        }
    }
}
