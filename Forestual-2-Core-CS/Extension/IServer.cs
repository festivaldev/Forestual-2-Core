#pragma warning disable 1591

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace F2Core.Extension
{
    public interface IServer
    {
        void PrintToConsole(string content, Color color, bool newLine = true);
        string ComposePrefix(string accountId);

        void SendPacketTo(string id, string packet);
        void SendPacketToAll(string packet);
        void SendPacketToChannel(string channelId, string packet);
        void SendPacketToAllExceptTo(string exceptId, string packet);
        void SendPacketToChannelExceptTo(string exceptId, string channelId, string packet);
        void SendMessagePacketTo(string id, MessagePacket packet);
        void SendMessagePacketToAll(MessagePacket packet);
        void SendMessagePacketToAllExceptTo(string exceptId, MessagePacket packet);
        void SendMessagePacketToChannelExceptTo(string exceptId, string channelId, MessagePacket packet);

        void CreatePunishment(Punishment punishment);
        Account GetAccountById(string id);
        Rank GetRankById(string id);
        Punishment GetPunishmentById(string id);
        void InvokeInternalEvent(Event e, params object[] args);
        void InvokeEvent(EventArguments e);
        void CancelInternalMessageHandling();
        string Serialize(object content, bool indented);
        T Deserialize<T>(string content);
        void DisplayForm(Form form);
        List<string> GetAllConnectedIds();
        void Enqueue(MessagePacket messagePacket);
        bool AccountHasLuvaValue(string accountId, string luvaValue);
        void SendLuvaNoticeTo(string accountId, string luvaValue);
        void RegisterSeverity(string luvaValue, int severityLevel);
        void DisposeConnectionById(string accountId);
        void SetAccountState(string accountId, bool online);
    }
}
