using System.Collections.Generic;

namespace F2Core
{
    public class Rank
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<string> MemberIds { get; set; } = new List<string>();
        public List<string> LuvaValues { get; set; } = new List<string>();
    }
}
