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
            if (!Regex.IsMatch(input, "(-?[0-9]+)\\.(-?[0-9]+)\\.([0-9]+)([a-zA-Z]*) \\[([a-zA-Z0-9]{7})\\]( @([0-9]{2}w[0-9]{1,2}))?")) {
                throw new FormatException();
            }
            var Match = Regex.Match(input, "(-?[0-9]+)\\.(-?[0-9]+)\\.([0-9]+)([a-zA-Z]*) \\[([a-zA-Z0-9]{7})\\]( @([0-9]{2}w[0-9]{1,2}))?");
            var Version = new Version {
                Major = int.Parse(Match.Groups[1].Value),
                Minor = int.Parse(Match.Groups[2].Value),
                Patch = int.Parse(Match.Groups[3].Value),
                Suffix = Match.Groups[4].Value,
                Commit = Match.Groups[5].Value,
                ReleaseDate = Match.Groups[7].Value
            };
            return Version;
        }

        public static bool InRange(this Version version, Version rangeVersion) {
            return version.GreaterThan(Parse(rangeVersion.SupportedVersion));
        }

        //public static bool LessThan(this Version left, Version right) {
        //    if (right.ToShortString() == Highest.ToShortString()) {
        //        return true;
        //    }
        //    if (left.ToShortString() == right.ToShortString()) {
        //        return true;
        //    }
        //    if (left.Major < right.Major) {
        //        return true;
        //    }
        //    if (left.Major == right.Major && left.Minor < right.Minor) {
        //        return true;
        //    }
        //    return left.Major == right.Major && left.Minor == right.Minor && left.Patch < right.Patch;
        //}

        public static bool GreaterThan(this Version left, Version right) {
            if (right.ToShortString() == Lowest.ToShortString()) {
                return true;
            }
            if (left.ToShortString() == right.ToShortString()) {
                return true;
            }
            if (left.Major > right.Major) {
                return true;
            }
            if (left.Major == right.Major && left.Minor > right.Minor) {
                return true;
            }
            return left.Major == right.Major && left.Minor == right.Minor && left.Patch > right.Patch;
        }
    }
}