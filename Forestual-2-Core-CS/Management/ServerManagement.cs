namespace F2Core.Management
{
    public class ServerManagement
    {
        public static IServer Server { get; set; }

        public static void RegisterServer(IServer server) {
            Server = server;
        }
    }
}