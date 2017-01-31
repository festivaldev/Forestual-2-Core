using System;
using System.IO;
using System.Net.Sockets;
using F2Core.Extension;

namespace F2Core
{
    /// <summary>
    /// Contains information about a connection and functions to handle network communication.
    /// </summary>
    public class Connection : IDisposable
    {
        /// <summary>
        /// The internal identifier of this <see cref="Connection"/>.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// The assigned <see cref="Account"/> to this <see cref="Connection"/>.
        /// </summary>
        public Account Owner { get; set; }

        /// <summary>
        /// The <see cref="F2Core.Channel"/> the <see cref="Owner"/> of this <see cref="Connection"/> is in.
        /// </summary>
        public Channel Channel { get; set; }

        /// <summary>
        /// The assigned <see cref="AesData"/> used to en/decrypt the network communication of this <see cref="Connection"/>.
        /// </summary>
        public AesData AesData { get; set; } = new AesData();

        /// <summary>
        /// The assigned key used to hash the encrypted contents of this <see cref="Connection"/>.
        /// </summary>
        public byte[] HmacKey { get; set; }

        /// <summary>
        /// Determines whether this <see cref="Connection"/> should be disposed by the <see cref="IServer"/> or not.
        /// </summary>
        public bool Disposable { get; set; }
        
        private NetworkStream Stream;
        private StreamWriter SWriter;
        private StreamReader SReader;

        /// <summary>
        /// Initializes a new connection using the given <see cref="NetworkStream"/>.
        /// </summary>
        /// <param name="baseStream">The <see cref="NetworkStream"/> this <see cref="Connection"/> uses for communication.</param>
        public Connection(NetworkStream baseStream) {
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
        /// Returns the decrypted <see cref="NetworkStream"/> content.
        /// </summary>
        /// <returns>The decrypted <see cref="NetworkStream"/> content.</returns>
        public string GetStreamContent() {
            var StreamContent = GetRawStreamContent().Split('|');
            if (Cryptography.ValidateHmacSignature(StreamContent[1], StreamContent[0], HmacKey))
                return Cryptography.AesDecrypt(StreamContent[0], AesData);
            return "";
        }

        /// <summary>
        /// Sets the <see cref="NetworkStream"/> content with encryption.
        /// </summary>
        /// <param name="content">The content to encrypt and write to the <see cref="NetworkStream"/>.</param>
        public void SetStreamContent(string content) {
            var StreamContent = Cryptography.AesEncrypt(content, AesData);
            var Signature = Cryptography.CreateHmacSignature(StreamContent, HmacKey);
            SetRawStreamContent(string.Join("|", StreamContent, Signature));
        }

        /// <summary>
        /// Disposes this <see cref="Connection"/> and all it's components.
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
