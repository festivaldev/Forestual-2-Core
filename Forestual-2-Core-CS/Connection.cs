using System;
using System.IO;
using System.Net.Sockets;

namespace F2Core
{
    public class Connection : IDisposable
    {
        public Account Owner { get; set; }
        public Channel Channel { get; set; }
        public AesData AesData { get; set; } = new AesData();
        public byte[] HmacKey { get; set; }

        private NetworkStream Stream;
        private StreamWriter SWriter;
        private StreamReader SReader;

        public Connection(NetworkStream baseStream) {
            Stream = baseStream;
            SReader = new StreamReader(Stream);
            SWriter = new StreamWriter(Stream );
        }

        public string GetRawStreamContent() {
            return SReader.ReadLine();
        }

        public void SetRawStreamContent(string content) {
            SWriter.WriteLine(content);
            SWriter.Flush();
        }

        public string GetStreamContent() {
            var StreamContent = GetRawStreamContent().Split('|');
            if (Cryptography.ValidateHmacSignature(StreamContent[1], StreamContent[0], HmacKey))
                return Cryptography.AesDecrypt(StreamContent[0], AesData);
            return "";
        }

        public void SetStreamContent(string content) {
            var StreamContent = Cryptography.AesEncrypt(content, AesData);
            var Signature = Cryptography.CreateHmacSignature(StreamContent, HmacKey);
            SetRawStreamContent(string.Join("|", StreamContent, Signature));
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
