#pragma warning disable 1591

using System.Collections.Generic;

namespace F2Core
{
    /// <summary>
    /// Contains information about a user.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// The internal identifier of the <see cref="Account"/>.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The displayed name of the <see cref="Account"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The hashed password of the <see cref="Account"/>.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The id of the <see cref="Rank"/> the <see cref="Account"/> is assigned to.
        /// </summary>
        public string RankId { get; set; }

        /// <summary>
        /// The assigned security level to this <see cref="Account"/>. Important for Lighthouse.
        /// </summary>
        public int SecurityLevel { get; set; }

        /// <summary>
        /// The deposit of this <see cref="Account"/>.
        /// </summary>
        public long Deposit { get; set; }

        /// <summary>
        /// Contains all Luva values assigned to this <see cref="Account"/>.
        /// </summary>
        public List<string> LuvaValues { get; set; } = new List<string>();

        /// <summary>
        /// Determines whether the <see cref="Account"/> is connected or not.
        /// </summary>
        public bool Online { get; set; }

        public static bool operator ==(Account first, Account second) {
            return first?.Id == second?.Id;
        }

        public static bool operator !=(Account first, Account second) {
            return first?.Id != second?.Id;
        }
    }
}
