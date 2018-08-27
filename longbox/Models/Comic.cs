using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.Models
{
    class Comic : BoxItem
    {
        public string Filename { get; set; }
        public ArchiveType ArchiveType { get; set; }
        public string RelativeToRoot { get; set; }

        public Comic(string name) : base(name)
        {
        }
    }
}
