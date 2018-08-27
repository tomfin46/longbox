using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.Models
{
    interface IBoxItem
    {
        Guid Identifier { get; }
        IBoxItem Parent { get; }
        string Name { get; }

        void AddItem(IBoxItem item);
        void AddItems(List<IBoxItem> items);
        void SetParent(IBoxItem parent);
        List<IBoxItem> GetItems();
    }
}
