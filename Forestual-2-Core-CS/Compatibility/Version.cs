namespace F2Core.Compatibility
{
    public class Version
    {
        public virtual int Major { get; set; } = 1;
        public virtual int Minor { get; set; } = 0;
        public virtual int Patch { get; set; } = 0;
        public virtual string Suffix { get; set; } = "";
        public virtual string ReleaseDate { get; set; } = "17w1";
        public virtual string Commit { get; set; } = "d8923d0";
        public virtual string SupportedVersion { get; set; }

        public string ToShortString() {
            return $"{Major}.{Minor}.{Patch}";
        }

        public string ToMediumString() {
            return $"{Major}.{Minor}.{Patch}{Suffix} [{Commit}]";
        }

        public string ToLongString() {
            return $"{ToMediumString()} @{ReleaseDate}";
        }
    }
}
