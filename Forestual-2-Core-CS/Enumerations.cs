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
            SetFlags,
            ClearConversation,
            Disconnect,
            GetServerMetaData,
            GetAccountList,
            SetAccountList,
            RegisterRecord,
            CreateChannel,
            RemoveChannel,
            SetChannelList,
            SetChannel,
            Extension,
            ExtensionTransport
        }

        public enum Flag
        {
            Wildcard,
            None,
            CanControlServer,
            CanGlobalTalk,
            CanHiddenTalk,
            CanMuteUser,
            CanDemuteUser,
            CanBannUser,
            CanBannUserPermanently,
            CanDebannUser,
            CanForce,
            CanForceMultiple,
            CanCreateChannel,
            CanEditChannel,
            CanVanish,
            CanChangeIdentity,
            CanCreateAccounts,
            CanEditAccounts,
            CanHyper,
            CannotBeImitated,
            CannotBeForced,
            CannotBeEdited,
            CannotBeMuted,
            CannotBeBanned
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
