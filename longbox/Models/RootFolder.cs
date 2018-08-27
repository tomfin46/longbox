using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace longbox.Models
{
    class RootFolder
    {
        public Guid Identifier { get; }

        public string FutureAccessToken { get; }

        public string Name { get; set; }

        public string Path { get; set; }

        public RootFolder(string futureAccessToken)
        {
            Identifier = Guid.NewGuid();
            FutureAccessToken = futureAccessToken;
        }
    }
}
