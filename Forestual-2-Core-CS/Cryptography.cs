using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace F2Core
{
    public class Cryptography
    {
        public static AesData GenerateAesData() {
            var Data = new AesData();
            using (var Aes = new AesCryptoServiceProvider()) {
                Data.Key = Aes.Key;
                Data.IV = Aes.IV;
                return Data;
            }
        }

        public static byte[] GenerateHmacKey() {
            using (var Hmac = new HMACSHA256()) {
                return Hmac.Key;
            }
        }

        public static string[] GenerateRSAKeys() {
            var ServiceProvider = new RSACryptoServiceProvider(4096);
            return new[] { ServiceProvider.ToXmlString(false), ServiceProvider.ToXmlString(true) };
        }

        public static string RSAEncrypt(string content, RSACryptoServiceProvider serviceProvider) {
            return Convert.ToBase64String(serviceProvider.Encrypt(Encoding.UTF8.GetBytes(content), true));
        }

        public static string RSADecrypt(string content, RSACryptoServiceProvider serviceProvider) {
            return Encoding.UTF8.GetString(serviceProvider.Decrypt(Convert.FromBase64String(content), true));
        }

        public static string AesEncrypt(string content, AesData cryptographicData) {
            var Aes = new AesCryptoServiceProvider();
            var Transform = Aes.CreateEncryptor(cryptographicData.Key, cryptographicData.IV);
            byte[] Bytes = null;
            var MStream = new MemoryStream();
            using (var CStream = new CryptoStream(MStream, Transform, CryptoStreamMode.Write)) {
                using (var SWriter = new StreamWriter(CStream)) {
                    SWriter.Write(content);
                }
            }
            Bytes = MStream.ToArray();
            MStream.Dispose();
            Transform.Dispose();
            Aes.Dispose();
            return Convert.ToBase64String(Bytes);
        }

        public static string AesDecrypt(string content, AesData cryptographicData) {
            var Aes = new AesCryptoServiceProvider();
            var Transform = Aes.CreateDecryptor(cryptographicData.Key, cryptographicData.IV);
            var MStream = new MemoryStream(Convert.FromBase64String(content));
            var CStream = new CryptoStream(MStream, Transform, CryptoStreamMode.Read);
            var SReader = new StreamReader(CStream);
            var Data = SReader.ReadToEnd();
            SReader.Dispose();
            CStream.Dispose();
            MStream.Dispose();
            Transform.Dispose();
            Aes.Dispose();
            return Data;
        }

        public static string CreateHmacSignature(string content, byte[] key) {
            using (var Hmac = new HMACSHA256(key)) {
                return Convert.ToBase64String(Hmac.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }
        }

        public static bool ValidateHmacSignature(string signature, string content, byte[] key) {
            using (var Hmac = new HMACSHA256(key)) {
                return Convert.ToBase64String(Hmac.ComputeHash(Encoding.UTF8.GetBytes(content))) == signature;
            }
        }

        public static string ComputeHash(string data) {
            using (var ServiceProvider = new SHA512CryptoServiceProvider()) {
                var Bytes = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(ServiceProvider.ComputeHash(Bytes));
            }
        }
    }

    public struct AesData
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
