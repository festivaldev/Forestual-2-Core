using System.Collections.Generic;

namespace F2Core
{
    public class Channel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Account Owner { get; set; }
        public List<string> ModeratorIds { get; set; } = new List<string>();
        public List<string> MemberIds { get; set; } = new List<string>();
        public bool Persistent { get; set; }
        public Enumerations.ChannelJoinMode JoinRestrictionMode { get; set; }
        public string Predicate { get; set; }

        public static bool operator ==(Channel first, Channel second) {
            return first?.Id == second?.Id;
        }

        public static bool operator !=(Channel first, Channel second) {
            return first?.Id != second?.Id;
        }
    }
}
