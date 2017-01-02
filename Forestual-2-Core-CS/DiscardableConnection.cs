using System;
using System.IO;
using System.Net.Sockets;

namespace F2Core
{
    public class DiscardableConnection : IDisposable
    {
        private NetworkStream Stream;
        private StreamWriter SWriter;
        private StreamReader SReader;

        public DiscardableConnection(NetworkStream baseStream) {
            Stream = baseStream;
            SReader = new StreamReader(Stream);
            SWriter = new StreamWriter(Stream);
        }

        public string GetRawStreamContent() {
            return SReader.ReadLine();
        }

        public void SetRawStreamContent(string content) {
            SWriter.WriteLine(content);
            SWriter.Flush();
        }

        public void Dispose() {
            Stream.Close();
            Stream.Dispose();
            SWriter.Close();
            SWriter.Dispose();
            SReader.Close();
            SReader.Dispose();
        }
    }
}
