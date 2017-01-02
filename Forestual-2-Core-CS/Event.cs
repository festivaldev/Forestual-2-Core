using F2Core.Management;

namespace F2Core
{
    public enum Event
    {
        ServerStarted,
        ClientConnected,
        ClientDisconnected,
        ClientMessageReceived,
        ConsoleInputReceived,
        ServerStopped,
        PunishmentRecorded,
        PunishmentExceeded,
        ChannelCreated,
        ClientChannelChanged,
        ClientRankChanged,
        ChannelClosed,
        ClientBalanceChanged,
        Dynamic
    }

    public class EventArguments
    {
        public string EndpointId { get; set; }
        public string Invoker { get; set; }
        public string EventHandler { get; set; }
        public object[] Parameters { get; set; }
        public object CallbackReturn { get; set; }

        public EventArguments() { }

        public EventArguments(string handler) {
            EventHandler = handler;
        }

        public EventArguments(string handler, string invoker) {
            EventHandler = handler;
            Invoker = invoker;
        }

        public EventArguments(string handler, string invoker, params object[] @params) {
            EventHandler = handler;
            Invoker = invoker;
            Parameters = @params;
        }

        public string ToServer() {
            return string.Join("|", Enumerations.Action.Extension, ClientManagement.Client.Serialize(this, false));
        }

        public string ToClient() {
            return string.Join("|", Enumerations.Action.Extension, ServerManagement.Server.Serialize(this, false));
        }
    }
}
