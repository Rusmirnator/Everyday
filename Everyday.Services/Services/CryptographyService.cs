using Everyday.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using Everyday.Core.Dictionaries;

namespace Everyday.Services.Services
{
    public class CryptographyService : ICryptographyService
    {
        #region Fields && Properties
        private readonly IConfiguration config;
        private readonly ILogger<CryptographyService> logger;

        public string AESKey { get; }

        #endregion

        #region CTOR
        public CryptographyService(IConfiguration config, ILogger<CryptographyService> logger)
        {
            this.config = config;
            this.logger = logger;
            AESKey = this.config["Encryption:AESKey"];
        }
        #endregion

        #region Public API
        public string Encrypt(string rawText)
        {
            return EncryptAES(rawText, true, AesType.AES256, AESKey);
        }

        public string Decrypt(string encryptedText)
        {
            return DecryptAES(encryptedText, true, AesType.AES256, AESKey);
        }
        #endregion

        #region Private API
        public string DecryptAES(string encryptedText, bool useHashing, AesType aesType, string key = "", string iv = "")
        {
            string result = "";

            byte[] encryptedbytes = Convert.FromBase64String(encryptedText);
            byte[] keybytes = PrepareAESKey(useHashing, aesType, key);
            byte[] ivbytes = PrepareVector(keybytes, iv);

            using (Aes aes = Aes.Create())
            {
                switch (aesType)
                {
                    case AesType.AES128:
                        {
                            aes.KeySize = 128;
                            if (string.IsNullOrEmpty(key))
                            {
                                aes.Key = keybytes;
                            }
                        }
                        break;

                    case AesType.AES192:
                        {
                            aes.KeySize = 192;
                            aes.Key = keybytes;
                        }
                        break;

                    case AesType.AES256:
                        {
                            aes.KeySize = 256;
                            aes.Key = keybytes;
                        }
                        break;
                }

                aes.IV = ivbytes;

                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decrypted = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);

                result = Encoding.UTF8.GetString(decrypted);
            }

            ClearTable(keybytes);
            ClearTable(ivbytes);

            return result;
        }

        private string EncryptAES(string rawText, bool useHashing, AesType aesType, string key = "", string iv = "")
        {
            string result = "";

            byte[] textbytes = Encoding.UTF8.GetBytes(rawText);
            byte[] keybytes = PrepareAESKey(useHashing, aesType, key);
            byte[] ivbytes = PrepareVector(keybytes, iv);

            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = 128;

                switch (aesType)
                {
                    case AesType.AES128:
                        {
                            aes.KeySize = 128;
                            aes.Key = keybytes;
                        }
                        break;

                    case AesType.AES192:
                        {
                            aes.KeySize = 192;
                            aes.Key = keybytes;
                        }
                        break;

                    case AesType.AES256:
                        {
                            aes.KeySize = 256;
                            aes.Key = keybytes;
                        }
                        break;
                }

                aes.IV = ivbytes;

                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] encrypted = crypto.TransformFinalBlock(textbytes, 0, textbytes.Length);
                crypto.Dispose();

                result = Convert.ToBase64String(encrypted, 0, encrypted.Length);
            }

            ClearTable(keybytes);
            ClearTable(ivbytes);

            return result;
        }

        private byte[] PrepareAESKey(bool useHashing, AesType aesType, string key = "")
        {
            byte[] keybytes;

            if (useHashing)
            {
                SHA256 hashsha256 = SHA256.Create();
                switch (aesType)
                {
                    case AesType.AES128:
                        {
                            keybytes = new byte[16];
                            Buffer.BlockCopy(hashsha256.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, keybytes, 0, 16);
                        }
                        break;
                    case AesType.AES192:
                        {
                            keybytes = new byte[24];
                            Buffer.BlockCopy(hashsha256.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, keybytes, 0, 24);
                        }
                        break;
                    case AesType.AES256:
                        {
                            keybytes = hashsha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                        }
                        break;
                    default:
                        {
                            throw new ArgumentException("Unrecognized AES type!", nameof(aesType));
                        }

                }
                hashsha256.Clear();
            }
            else
            {
                keybytes = Encoding.UTF8.GetBytes(key);
            }

            try
            {
                switch (aesType)
                {
                    case AesType.AES128:
                        {
                            if ((!string.IsNullOrEmpty(key)) && (keybytes.Length != 16))
                            {
                                throw new ArgumentException("Wrong key length (AES128) - 16 bytes allowed!", nameof(key));
                            }
                        }
                        break;
                    case AesType.AES192:
                        {
                            if ((!string.IsNullOrEmpty(key)) && (keybytes.Length != 24))
                            {
                                throw new ArgumentException("Wrong key length (AES192) - 24 bytes allowed!", nameof(key));
                            }
                        }
                        break;
                    case AesType.AES256:
                        {
                            if ((!string.IsNullOrEmpty(key)) && (keybytes.Length != 32))
                            {
                                throw new ArgumentException("Wrong key length (AES256) - 32 bytes allowed!", nameof(key));
                            }
                        }
                        break;
                    default:
                        {
                            throw new ArgumentException("Wrong AES type!", nameof(aesType));
                        }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            return keybytes;
        }

        /// <summary>
        /// Wyznaczenie wektora inicjującego
        /// </summary>
        /// <param name="keybytes"></param>
        /// <param name="aIV"></param>
        /// <returns></returns>
        private static byte[] PrepareVector(byte[] keybytes, string aIV = "")
        {
            byte[] ivbytes;

            if (string.IsNullOrEmpty(aIV))
            {
                ivbytes = new byte[16];
                Buffer.BlockCopy(keybytes, 0, ivbytes, 0, 16);
            }
            else
            {
                ivbytes = Encoding.UTF8.GetBytes(aIV);
            }

            if (ivbytes.Length != 16)
            {
                throw new ArgumentException("Błędna długość IV (powinna wynosić 16 bajtów)", "aIV");
            }

            return ivbytes;
        }
        private static void ClearTable(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        #endregion
    }
}
