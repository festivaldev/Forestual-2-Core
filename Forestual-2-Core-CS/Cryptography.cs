using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace F2Core
{
    /// <summary>
    /// Provides methods to generate keys and en/decrypt data.
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// Generates random <see cref="AesData"/>.
        /// </summary>
        /// <returns>A random <see cref="AesData"/>.</returns>
        public static AesData GenerateAesData() {
            var Data = new AesData();
            using (var Aes = new AesCryptoServiceProvider()) {
                Data.Key = Aes.Key;
                Data.IV = Aes.IV;
                return Data;
            }
        }

        /// <summary>
        /// Generates a random <see cref="HMAC"/> key.
        /// </summary>
        /// <returns>A random <see cref="HMAC"/> key.</returns>
        public static byte[] GenerateHmacKey() {
            using (var Hmac = new HMACSHA256()) {
                return Hmac.Key;
            }
        }

        /// <summary>
        /// Generates random <see cref="RSA"/> keys.
        /// </summary>
        /// <returns>A <see cref="string"/> array containing the public and private <see cref="RSA"/> keys.</returns>
        public static string[] GenerateRSAKeys() {
            var ServiceProvider = new RSACryptoServiceProvider(4096);
            return new[] { ServiceProvider.ToXmlString(false), ServiceProvider.ToXmlString(true) };
        }

        /// <summary>
        /// Encrypts a <see cref="string"/> using the given <see cref="RSACryptoServiceProvider"/>.
        /// </summary>
        /// <param name="content">The <see cref="string"/> to encrypt.</param>
        /// <param name="serviceProvider">The <see cref="RSACryptoServiceProvider"/> to use.</param>
        /// <returns>The encrypted <see cref="string"/>.</returns>
        public static string RSAEncrypt(string content, RSACryptoServiceProvider serviceProvider) {
            return Convert.ToBase64String(serviceProvider.Encrypt(Encoding.UTF8.GetBytes(content), true));
        }

        /// <summary>
        /// Decrypts a <see cref="string"/> using the given <see cref="RSACryptoServiceProvider"/>.
        /// </summary>
        /// <param name="content">The <see cref="string"/> to decrypt.</param>
        /// <param name="serviceProvider">The <see cref="RSACryptoServiceProvider"/> to use.</param>
        /// <returns>The decrypted <see cref="string"/>.</returns>
        public static string RSADecrypt(string content, RSACryptoServiceProvider serviceProvider) {
            return Encoding.UTF8.GetString(serviceProvider.Decrypt(Convert.FromBase64String(content), true));
        }


        /// <summary>
        /// Encrypts a <see cref="string"/> using the given <see cref="AesData"/>.
        /// </summary>
        /// <param name="content">The <see cref="string"/> to encrypt.</param>
        /// <param name="cryptographicData">The <see cref="AesData"/> to use.</param>
        /// <returns>The encrypted <see cref="string"/>.</returns>
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

        /// <summary>
        /// Decrypts a <see cref="string"/> using the given <see cref="AesData"/>.
        /// </summary>
        /// <param name="content">The <see cref="string"/> to decrypt.</param>
        /// <param name="cryptographicData">The <see cref="AesData"/> to use.</param>
        /// <returns>The decrypted <see cref="string"/>.</returns>
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

        /// <summary>
        /// Creates a <see cref="HMACSHA256"/> signature using the given key.
        /// </summary>
        /// <param name="content">The <see cref="string"/> to sign.</param>
        /// <param name="key">The key to use.</param>
        /// <returns>The <see cref="HMACSHA256"/> signature for the given <see cref="string"/>.</returns>
        public static string CreateHmacSignature(string content, byte[] key) {
            using (var Hmac = new HMACSHA256(key)) {
                return Convert.ToBase64String(Hmac.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }
        }

        /// <summary>
        /// Validates a <see cref="HMACSHA256"/> signature using the given key.
        /// </summary>
        /// <param name="signature">The <see cref="HMACSHA256"/> signature to validate.</param>
        /// <param name="content">The <see cref="string"/> signed by the signature.</param>
        /// <param name="key">The key to use.</param>
        /// <returns>A <see cref="bool"/> determining the validation of the <see cref="HMACSHA256"/> signature.</returns>
        public static bool ValidateHmacSignature(string signature, string content, byte[] key) {
            using (var Hmac = new HMACSHA256(key)) {
                return Convert.ToBase64String(Hmac.ComputeHash(Encoding.UTF8.GetBytes(content))) == signature;
            }
        }

        /// <summary>
        /// Creates a <see cref="SHA512"/> hash.
        /// </summary>
        /// <param name="data">The <see cref="string"/> to hash.</param>
        /// <returns>The computed <see cref="SHA512"/> hash.</returns>
        public static string ComputeHash(string data) {
            using (var ServiceProvider = new SHA512CryptoServiceProvider()) {
                var Bytes = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(ServiceProvider.ComputeHash(Bytes));
            }
        }
    }

    /// <summary>
    /// Contains a key and an initialization vector used for <see cref="Aes"/> en/decryption.
    /// </summary>
    public struct AesData
    {
        /// <summary>
        /// The key of this <see cref="AesData"/>.
        /// </summary>
        public byte[] Key { get; set; }

        /// <summary>
        /// The initialization vector of this <see cref="AesData"/>.
        /// </summary>
        public byte[] IV { get; set; }
    }
}
