using System;
using System.Runtime.InteropServices;

namespace Shine.Comman.HPSocket
{
    public class Common
    {
        /// <summary>
        /// <see cref="IntPtr"/>类型转换成<see cref="String"/>
        /// </summary>
        /// <param name="ptr"><see cref="IntPtr"/>数据</param>
        public static string PtrToAnsiString(IntPtr ptr)
        {
            string str = string.Empty;
            try
            {
                if (ptr != IntPtr.Zero)
                {
                    str = Marshal.PtrToStringAnsi(ptr);
                }
            }
            catch{}
            return str;
        }
    }
}
