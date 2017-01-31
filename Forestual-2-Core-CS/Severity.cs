using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2Core
{
    public class Severity
    {
        public string Description { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public string Color { get; set; }
        public int Level { get; set; }
    }
}
