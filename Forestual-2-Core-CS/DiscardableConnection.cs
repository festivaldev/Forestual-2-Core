using System;
using System.IO;
using System.Net.Sockets;

namespace F2Core
{
    /// <summary>
    /// A short-time <see cref="Connection"/> used for pre-login network communication.
    /// </summary>
    public class DiscardableConnection : IDisposable
    {
        private NetworkStream Stream;
        private StreamWriter SWriter;
        private StreamReader SReader;

        /// <summary>
        /// Initializes a new connection using the given <see cref="NetworkStream"/>.
        /// </summary>
        /// <param name="baseStream">The <see cref="NetworkStream"/> this <see cref="Connection"/> uses for communication.</param>
        public DiscardableConnection(NetworkStream baseStream) {
            Stream = baseStream;
            SReader = new StreamReader(Stream);
            SWriter = new StreamWriter(Stream);
        }

        /// <summary>
        /// Returns the unhandled and raw <see cref="NetworkStream"/> content.
        /// </summary>
        /// <returns>The unhandled and raw <see cref="NetworkStream"/> content.</returns>
        public string GetRawStreamContent() {
            return SReader.ReadLine();
        }

        /// <summary>
        /// Sets the <see cref="NetworkStream"/> content without encryption.
        /// </summary>
        /// <param name="content">The content to write to the <see cref="NetworkStream"/>.</param>
        public void SetRawStreamContent(string content) {
            SWriter.WriteLine(content);
            SWriter.Flush();
        }

        /// <summary>
        /// Disposes this <see cref="DiscardableConnection"/> and all it's components.
        /// </summary>
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
