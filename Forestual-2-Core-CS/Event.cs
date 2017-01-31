using System;
using F2Core.Extension;

namespace F2Core
{
    public enum Event
    {
        /// <summary>
        /// Gets fired when the <see cref="IServer"/> starts.
        /// </summary>
        ServerStarted,

        /// <summary>
        /// Gets fired when the <see cref="IServer"/> receives a new <see cref="Connection"/>.
        /// </summary>
        ConnectionEstablished,

        /// <summary>
        /// Gets fired when a new <see cref="Account"/> is about to connect.
        /// </summary>
        ClientConnect,

        /// <summary>
        /// Gets fired when a new <see cref="Account"/> connected.
        /// </summary>
        ClientConnected,

        /// <summary>
        /// Gets fired when a new <see cref="Account"/> disconnected.
        /// </summary>
        ClientDisconnected,

        /// <summary>
        /// Gets fired when an <see cref="Account"/> send a message.
        /// </summary>
        ClientMessageReceived,

        /// <summary>
        /// Gets fired when the <see cref="IServer"/> registered a console input.
        /// </summary>
        ConsoleInputReceived,

        /// <summary>
        /// Gets fired when the <see cref="IServer"/> stopped.
        /// </summary>
        ServerStopped,

        PunishmentRecorded,
        PunishmentExceeded,
        ChannelCreated,
        ClientChannelChanged,
        ClientRankChanged,
        ChannelClosed,
        ClientBalanceChanged,

        /// <summary>
        /// Gets fired when external events are invoked.
        /// </summary>
        Dynamic
    }

    /// <summary>
    /// Contains information about a <see cref="Event.Dynamic"/> event.
    /// </summary>
    public class EventArguments
    {
        /// <summary>
        /// The internal identifier of the <see cref="Account"/> invoking this event.
        /// </summary>
        public string EndpointId { get; set; }

        /// <summary>
        /// The <see cref="IExtension.Namespace"/> of the <see cref="IExtension"/> invoking this event.
        /// </summary>
        public string Invoker { get; set; }

        /// <summary>
        /// The handler of this event.
        /// </summary>
        public string EventHandler { get; set; }

        /// <summary>
        /// The parameters of this event.
        /// </summary>
        public object[] Parameters { get; set; }

        /// <summary>
        /// Used to store answers from the listener.
        /// </summary>
        [Obsolete]
        public object CallbackReturn { get; set; }

        /// <summary>
        /// Empty constructor used for deserialization.
        /// </summary>
        public EventArguments() { }

        /// <summary>
        /// Creates new <see cref="EventArguments"/>.
        /// </summary>
        /// <param name="handler">The handler of this event.</param>
        public EventArguments(string handler) {
            EventHandler = handler;
        }

        /// <summary>
        /// Creates new <see cref="EventArguments"/>.
        /// </summary>
        /// <param name="handler">The handler of this event.</param>
        /// <param name="invoker">The invoker of this event.</param>
        public EventArguments(string handler, string invoker) {
            EventHandler = handler;
            Invoker = invoker;
        }

        /// <summary>
        /// Creates new <see cref="EventArguments"/>.
        /// </summary>
        /// <param name="handler">The handler of this event.</param>
        /// <param name="invoker">The invoker of this event.</param>
        /// <param name="params">The parameters of this event.</param>
        public EventArguments(string handler, string invoker, params object[] @params) {
            EventHandler = handler;
            Invoker = invoker;
            Parameters = @params;
        }


        /// <summary>
        /// Converts this event into a <see cref="string"/> eligable to be sent from an <see cref="IClient"/>.
        /// </summary>
        /// <returns></returns>
        public string ToServer() {
            return string.Join("|", Enumerations.Action.Extension, ExtensionPool.Client.Serialize(this, false));
        }

        /// <summary>
        /// Converts this event into a <see cref="string"/> eligable to be sent from an <see cref="IServer"/>.
        /// </summary>
        /// <returns></returns>
        public string ToClient() {
            return string.Join("|", Enumerations.Action.Extension, ExtensionPool.Server.Serialize(this, false));
        }
    }
}
