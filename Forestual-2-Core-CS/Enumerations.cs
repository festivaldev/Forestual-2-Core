#pragma warning disable 1591

namespace F2Core
{
    public class Enumerations
    {
        public enum Action
        {
            Plain,
            Message,
            TryLogin,
            TryBasicLogin,
            TryGuestLogin,
            TryRegister,
            LoginResult,
            TryChannelJoin,
            ChannelJoinResult,
            SetState,
            SetLuvaValues,
            ClearConversation,
            Disconnect,
            GetServerMetaData,
            GetAccountList,
            SetAccountList,
            SetRankList,
            RegisterRecord,
            CreateChannel,
            CloseChannel,
            SetChannelList,
            SetChannel,
            Extension,
            ExtensionTransport,
            GetAccountData,
            SetAccountData,
            SendLuvaNotice,
            ShowLuvaNotice,
            None
        }

        public enum MessageType
        {
            Center,
            Left,
            Right,
            Broadcast
        }

        public enum ChannelJoinMode
        {
            Default,
            Protected,
            Ranked
        }

        public enum PunishmentType
        {
            Mute,
            Bann,
            BannTemporarily
        }

        public enum ClientState
        {
            Default,
            Muted,
            AwayFromKeyboard,
            Hidden,
            Banned
        }
    }
}
