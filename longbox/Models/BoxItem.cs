using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.Models
{
    class BoxItem : IBoxItem
    {
        private Guid _identifier;
        private IBoxItem _parent;
        private List<IBoxItem> _items;
        private string _name;

        public Guid Identifier { get => _identifier; }
        public IBoxItem Parent { get => _parent; }
        public string Name { get => _name; }

        public BoxItem(string name)
        {
            _identifier = Guid.NewGuid();
            _items = new List<IBoxItem>();
            _name = name;
        }

        public void AddItem(IBoxItem item)
        {
            item.SetParent(this);
            _items.Add(item);
        }

        public void AddItems(List<IBoxItem> items)
        {
            _items.ForEach(AddItem);
        }

        public void SetParent (IBoxItem parent)
        {
            _parent = parent;
        }

        public List<IBoxItem> GetItems()
        {
            return _items;
        }
    }
}
