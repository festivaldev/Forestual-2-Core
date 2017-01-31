namespace F2Core.Extension
{
    public class ExtensionPool
    {
        public static IClient Client { get; set; }
        public static IServer Server { get; set; }

        public static void RegisterServer(IServer server) {
            Server = server;
        }

        public static void RegisterClient(IClient client) {
            Client = client;
        }
    }
}