using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.Models
{
    class Box : BoxItem
    {
        private string _path;

        public string Path { get => _path; }

        public Box(string name) : base(name) { }

        public Box(string name, string path) : base(name)
        {
            _path = path;
        }
        
    }
}
