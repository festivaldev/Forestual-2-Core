using System.Windows.Forms;

namespace F2Core.Management
{
    public interface IClient
    {
        void DisplayForm(Form form);
        void SendToServer(string content);
        string Serialize(object content, bool indented);
        object Deserialize(string content);
    }
}
