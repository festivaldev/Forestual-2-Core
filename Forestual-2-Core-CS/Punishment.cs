using System;
using System.Linq;

namespace F2Core
{
    public class Punishment
    {
        public string AccountId { get; set; }
        public Enumerations.PunishmentType Type { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string CreatorId { get; set; }
        public string Id { get; set; }

        public Punishment() {
            Id = GenerateId(6);
        }

        private static string GenerateId(int length) {
            const string Characters = "abcdefghijklmnopqrstuvwxyz0123456789";
            var Random = new Random();
            return new string(Enumerable.Repeat(Characters, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
