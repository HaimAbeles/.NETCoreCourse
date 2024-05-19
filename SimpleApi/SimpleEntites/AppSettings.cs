using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEntites
{
    public class AppSettings
    {
        public Price Price { get; set; }
        public bool boolTest { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string Simple { get; set; }
    }

    public class Price
    {
        public int min { get; set; }
        public string max { get; set; }
    }
}
