using F2Core.Compatibility;

namespace F2Core
{
    public class ServerMetaData
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string OwnerId { get; set; }
        public string OperatorWebsiteUrl { get; set; }
        public bool RequiresAuthentification { get; set; }
        public bool AcceptsGuests { get; set; }
        public bool GuestsCanChooseName { get; set; }
        public bool AcceptsRegistration { get; set; }
        public bool RequiresInvitation { get; set; }
        public bool AccountsInstantlyActivated { get; set; }
        public Version ServerVersion { get; set; }
        public string ServerCoreVersion { get; set; }
    }
}
