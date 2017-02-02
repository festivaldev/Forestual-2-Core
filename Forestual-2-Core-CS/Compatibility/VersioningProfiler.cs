#pragma warning disable 1591

using System;
using System.Text.RegularExpressions;

namespace F2Core.Compatibility
{
    public static class VersioningProfiler
    {
        public static Version Lowest => new Version {
            Major = 0,
            Minor = -1,
            Patch = 0,
            Commit = "lowest0"
        };

        public static Version Highest => new Version {
            Major = -1,
            Minor = 0,
            Patch = 0,
            Commit = "highest"
        };

        public static Version Parse(string input) {
            if (!Regex.IsMatch(input, "(-?[0-9]+)\\.(-?[0-9]+)\\.([0-9]+)(|-snapshot|-alpha|-beta|-rc) \\[([a-zA-Z0-9]{7})\\]( @([0-9]{2}w[0-9]{1,2}))?", RegexOptions.IgnoreCase)) {
                throw new FormatException();
            }
            var Match = Regex.Match(input, "(-?[0-9]+)\\.(-?[0-9]+)\\.([0-9]+)(|-snapshot|-alpha|-beta|-rc) \\[([a-zA-Z0-9]{7})\\]( @([0-9]{2}w[0-9]{1,2}))?", RegexOptions.IgnoreCase);

            var Suffix = Match.Groups[4].Value;
            Suffix = Suffix == "" ? "none" : Suffix.Remove(0, 1);

            var Version = new Version {
                Major = int.Parse(Match.Groups[1].Value),
                Minor = int.Parse(Match.Groups[2].Value),
                Patch = int.Parse(Match.Groups[3].Value),
                Suffix = (Suffixes) Enum.Parse(typeof(Suffixes), Suffix.ToLower()),
                Commit = Match.Groups[5].Value,
                ReleaseDate = Match.Groups[7].Value
            };
            return Version;
        }

        public static bool InRange(this Version version, Version rangeVersion) {
            return version.GreaterThan(Parse(rangeVersion.SupportedVersion));
        }

        public static bool GreaterThan(this Version left, Version right) {
            if (right.ToShortString() == Lowest.ToShortString()) {
                return true;
            }
            if (left.ToMediumString() == right.ToMediumString()) {
                return true;
            }
            if (left.Major > right.Major) {
                return true;
            }
            if (left.Major == right.Major && left.Minor > right.Minor) {
                return true;
            }
            if (left.Major == right.Major && left.Minor == right.Minor && left.Patch > right.Patch) {
                return true;
            }
            return left.Major == right.Major && left.Minor == right.Minor && left.Patch == right.Patch && left.Suffix > right.Suffix;
        }

        public enum Suffixes
        {
            snapshot,
            alpha,
            beta,
            rc,
            none
        }
    }
}