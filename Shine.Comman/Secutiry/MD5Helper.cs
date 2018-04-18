using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shine.Comman.Secutiry
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value">准备要加密的字符</param>
        /// <param name="digits">加密位数16/32</param>
        /// <returns>MD5加密后的字符串</returns>
        public static string Md5Encryptn(this string value, int digits=32)
        {
            value.CheckNotNullOrEmpty("value");

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();        
            if (digits == 32)
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(value))).Replace("-", "");
            }
            else
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(value)), 4, 8).Replace("-", "");
            }
        }
    }
}
