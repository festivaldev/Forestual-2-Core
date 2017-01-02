using System.Collections.Generic;

namespace F2Core
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string RankId { get; set; }
        public int SecurityLevel { get; set; }
        public long Deposit { get; set; }
        public List<Enumerations.Flag> Flags { get; set; } = new List<Enumerations.Flag>();
        public bool Online { get; set; }

        public static bool operator ==(Account first, Account second) {
            return first?.Id == second?.Id;
        }

        public static bool operator !=(Account first, Account second) {
            return first?.Id != second?.Id;
        }
    }
}
