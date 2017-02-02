#pragma warning disable 1591

using System.Windows.Forms;

namespace F2Core.Extension
{
    public interface IClient
    {
        void DisplayForm(Form form);
        void SendPacketToServer(string packet);
        string Serialize(object content, bool indented);
        object Deserialize(string content);
    }
}
