namespace F2Core.Management
{
    public class ClientManagement
    {
        public static IClient Client { get; set; }

        public static void RegisterClient(IClient client) {
            Client = client;
        }
    }
}