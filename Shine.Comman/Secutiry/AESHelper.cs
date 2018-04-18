using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shine.Comman.Secutiry
{
    /// <summary>
    /// AES 加密解密
    /// </summary>
    public static class AESHelper
    {
        /// <summary>
        /// AES128加密
        /// </summary>
        /// <param name="value">准备要加密的字符串</param>
        /// <param name="key">加密的密匙</param>
        /// <param name="iv">加密向量</param>
        /// <returns>加密后的字符串</returns>
        public static string AESEncrypt128(this string value, string key="leyviewstreetlamp", string iv="tjszgdgxnncdymyn")
        {
            try
            {
                value.CheckNotNull("value");
                RijndaelManaged rijndaelCipher = new RijndaelManaged
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 128,
                    BlockSize = 128,
                    Key = Encoding.UTF8.GetBytes(key.Length > 16 ? key.Substring(0, 16) : key),
                    IV  = Encoding.UTF8.GetBytes(iv.Length > 16 ? iv.Substring(0, 16) : iv)
                };
                ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(value);
                byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in cipherBytes)
                {
                    stringBuilder.AppendFormat("{0:X2}", b);
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AES128解密
        /// </summary>
        /// <param name="value">准备要解密的字符</param>
        /// <param name="key">解密密匙</param>
        /// <param name="iv">解密向量</param>
        /// <returns>解密后的字符串</returns>
        public static string AESDecrypt128(this string value, string key= "leyviewstreetlamp", string iv = "tjszgdgxnncdymyn")
        {
            try
            {
                value.CheckNotNull("value");
                int len = value.Length / 2;
                byte[] inputByteArray = new byte[len];
                for (int n = 0; n < len; n++)
                {
                    int i = Convert.ToInt32(value.Substring(n * 2, 2), 16);
                    inputByteArray[n] = (byte)i;
                }

                RijndaelManaged rijndaelCipher = new RijndaelManaged
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 128,
                    BlockSize = 128,
                    Key = Encoding.UTF8.GetBytes(key.Length > 16 ? key.Substring(0, 16) : key),
                    IV = Encoding.UTF8.GetBytes(iv.Length > 16 ? iv.Substring(0, 16) : iv)
                };
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);
                return Encoding.UTF8.GetString(plainText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AES简单加密
        /// </summary>
        /// <param name="toEncrypt">准备加密的字符串</param>
        /// <param name="key">加密用的密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string AESEncrypt(this string toEncrypt, string key= "aesleyviewmmming")
        {
            toEncrypt.CheckNotNullOrEmpty("toEncrypt");
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in resultArray)
            {
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// AES简单解密
        /// </summary>
        /// <param name="toDecrypt">准备解密的字符串</param>
        /// <param name="key">解密用的密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string AESDecrypt(this string toDecrypt, string key= "aesleyviewmmming")
        {
            toDecrypt.CheckNotNullOrEmpty("toDecrypt");

            int len = toDecrypt.Length / 2;
            byte[] toEncryptArray = new byte[len];
            for (int n = 0; n < len; n++)
            {
                int i = Convert.ToInt32(toDecrypt.Substring(n * 2, 2), 16);
                toEncryptArray[n] = (byte)i;
            }

            byte[] keyArray = Encoding.UTF8.GetBytes(key);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
