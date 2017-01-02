using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace F2Core.Management
{
    public interface IServer
    {
        void PrintToConsole(string content, Color color, bool newLine = true);
        string ComposePrefix(string accountId);
        void SendTo(string id, string content);
        void SendToAll(string content);
        void SendToChannel(string id, string content);
        void SendMessageTo(string id, Message message);
        void SendMessageToAll(Message message);
        void SendMessageToAllExceptTo(string accountId, Message message, string channelId);
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
        void Enqueue(Message message);
    }
}
