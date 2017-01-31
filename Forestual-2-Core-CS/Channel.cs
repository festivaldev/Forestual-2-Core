using System.Collections.Generic;
using F2Core.Extension;

namespace F2Core
{
    /// <summary>
    /// Contains information about a channel.
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// The internal identifier of the <see cref="Channel"/>.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The displayed name of the <see cref="Channel"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The id of the <see cref="Account"/> who owns this <see cref="Channel"/>.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The maximum amount of <see cref="Account"/>s. Accounts with the <c>forestual.exceedChannelLimit</c> Luva value can exceed this limit.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Contains the ids of all <see cref="Account"/>s that have moderation rights in this <see cref="Channel"/>.
        /// </summary>
        public List<string> ModeratorIds { get; set; } = new List<string>();

        /// <summary>
        /// Contains the ids of all <see cref="Account"/>s that are in this <see cref="Channel"/>.
        /// </summary>
        public List<string> MemberIds { get; set; } = new List<string>();

        /// <summary>
        /// Determines whether this <see cref="Channel"/> persists after stopping the <see cref="IServer"/>.
        /// </summary>
        public bool Persistent { get; set; }

        /// <summary>
        /// Determines which restriction applies to joining this <see cref="Channel"/>.
        /// </summary>
        public Enumerations.ChannelJoinMode JoinRestrictionMode { get; set; }

        /// <summary>
        /// Contains additional information about the <see cref="JoinRestrictionMode"/>.
        /// </summary>
        public string Predicate { get; set; }

        public static bool operator ==(Channel first, Channel second) {
            return first?.Id == second?.Id;
        }

        public static bool operator !=(Channel first, Channel second) {
            return first?.Id != second?.Id;
        }
    }
}
