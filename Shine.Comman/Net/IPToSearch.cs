using System;
using System.IO;
using System.Net;
using System.Text;

namespace Shine.Comman.Net
{
    /// <summary>
    /// IP数据库搜索器
    /// </summary>
    public class IPToSearch
    {
        private static IPToSearch _IPSearch;

        /// <summary>
        /// <see cref="IPToSearch"/>实例对象
        /// </summary>
        public static IPToSearch Instance => _IPSearch ?? (_IPSearch = new IPToSearch());

        #region 常量、成员字段
        const int REDIRECT_MODE_ONE = 1;
        const int REDIRECT_MODE_TWO = 2;
        const int RECORD_ITEM_SIZE = 7;

        Stream stream;
        uint beginPtr, endPtr;
        #endregion

        #region 构造方法。初始化 QQWrySearch 新实例
        /// <summary>
        /// 初始化 IPToSearch 新实例
        /// </summary>
        private IPToSearch()
        {
            this.stream = new MemoryStream(Properties.Resources.iplibrary);
            this.beginPtr = UIntOf(this.stream, 0, 4, true);    // 6064823
            this.endPtr = UIntOf(this.stream, 4, 4, true);      // 9066759
        }
        #endregion

        #region Search 方法搜索指定的 IP 地址
        /// <summary>
        /// 搜索指定的 IP 地址
        /// </summary>
        /// <param name="address">表示 IP 地址信息的 System.String</param>
        /// <returns>指定的 IP 地址信息的 IPLocation 实例</returns>
        public IPLocation Search(string address)
        {
            IPAddress.TryParse(address, out IPAddress ipp);
            return Search(this,ipp);
        }

        /// <summary>
        /// 搜索指定的 IP 地址，并返回国家地区信息的 IPLocation 实例
        /// </summary>
        /// <param name="address">表示 IP 地址信息的 System.Int32</param>
        /// <returns>指定的 IP 地址信息的 IPLocation 实例</returns>
        public IPLocation Search(int address)
        {
            return Search(this, new IPAddress(ToBytes(address)));
        }

        /// <summary>
        /// 搜索指定的 IP 地址，并返回国家地区信息的 IPLocation 实例
        /// </summary>
        /// <param name="address">表示 IP 地址信息的 System.UInt32</param>
        /// <returns>指定的 IP 地址信息的 IPLocation 实例</returns>
        public IPLocation Search(uint address)
        {
            return Search(this, new IPAddress(ToBytes(address)));
        }

        /// <summary>
        /// 搜索指定的 IP 地址，并返回国家地区信息的 IPLocation 实例
        /// </summary>
        /// <param name="address">表示 IP 地址信息的 System.Byte[]</param>
        /// <returns>指定的 IP 地址信息的 IPLocation 实例</returns>
        public IPLocation Search(byte[] address)
        {
            return Search(this, new IPAddress(address));
        }

        static byte[] ToBytes(long address)
        {
            byte[] buffer = new byte[4];

            buffer[0] = (byte)((address >> 24) & 0xff);
            buffer[1] = (byte)((address >> 16) & 0xff);
            buffer[2] = (byte)((address >> 8) & 0xff);
            buffer[3] = (byte)(address & 0xff);

            return buffer;
        }


        /// <summary>
        /// 搜索指定的 IP 地址，并返回国家地区信息的 IPLocation 实例
        /// </summary>
        /// <param name="address">表示 IP 地址信息的 System.Net.IPAddress</param>
        /// <returns>指定的 IP 地址信息的 IPLocation 实例</returns>
        public IPLocation Search(IPAddress address)
        {
            return Search(this, address);
        }

        static IPLocation Search(IPToSearch obj, IPAddress address)
        {
            uint ptr;

            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                throw new ArgumentException(obj.GetType().FullName + " 不支持 Internet Protocol v6 版本。", "address");
            }

            byte[] bytes = address.GetAddressBytes();
            if (bytes[0] == 255 && bytes[1] == 255 && bytes[2] == 255)
            {
                return IPLocation.Create("IANA 保留地址", null);
            }
            else if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0)
            {
                return IPLocation.Create("IANA 保留地址", null);
            }

            DateTime now = DateTime.Now;
            ptr = Find(obj.stream, UIntOf(address.GetAddressBytes(), false), obj.beginPtr, obj.endPtr);

            if (ptr > 0)
            {
                return GetLocation(obj.stream, ptr);
            }
            else
            {
                return IPLocation.Empty;
            }
        }
        #endregion

        #region Find 方法。查找 IP 地址所在偏移
        static uint Find(Stream stream, uint address, uint beginPosition, uint endPosition)
        {
            uint compareAddr = UIntOf(stream, beginPosition, 4, true);

            if (address == compareAddr)
                return beginPosition;
            else if (compareAddr > address)
                return 0;

            uint middle = 0;

            for (uint offset = beginPosition, endOffset = endPosition; offset < endOffset; )
            {
                middle = MidOf(offset, endOffset, RECORD_ITEM_SIZE);
                compareAddr = UIntOf(stream, middle, 4, true);

                if (address > compareAddr)
                    offset = middle;
                else if (address == compareAddr)
                    break;
                else if (middle == endOffset)
                    middle = endOffset = (endOffset - RECORD_ITEM_SIZE);
                else
                    endOffset = middle;
            }

            uint ptr = UIntOf(stream, middle + 4, 3, true);
            compareAddr = UIntOf(stream, ptr, 4, true);

            if (address <= compareAddr)
                return ptr;
            else
                return 0;
        }
        #endregion

        #region GetLocation 方法
        static IPLocation GetLocation(Stream stream, uint position)
        {
            string country, area;
            stream.Position = position + 4;
            int flags = stream.ReadByte();

            if (flags == REDIRECT_MODE_ONE)
            {
                uint offset1 = UIntOf(stream, stream.Position, 3, true);

                stream.Position = offset1;
                flags = ByteOf(stream, offset1);

                if (flags == REDIRECT_MODE_TWO)
                {
                    uint offset2 = UIntOf(stream, stream.Position, 3, true);
                    country = GetString(stream, offset2);

                    stream.Position = offset1 + 4;
                }
                else
                {
                    country = GetString(stream, offset1);
                }

                area = GetArea(stream, stream.Position);
            }
            else if (flags == REDIRECT_MODE_TWO)
            {
                uint offset = UIntOf(stream, stream.Position, 3, true);
                country = GetString(stream, offset);
                area = GetArea(stream, position + 8);
            }
            else
            {
                country = GetString(stream, --stream.Position);
                area = GetString(stream, stream.Position);
            }

            return IPLocation.Create(country, area);
        }

        static string GetArea(Stream stream, long position)
        {
            stream.Position = position;
            int flags = stream.ReadByte();

            if (flags == REDIRECT_MODE_ONE || flags == REDIRECT_MODE_TWO)
                position = UIntOf(stream, position + 1, 3, true);

            if (position == 0)
                return string.Empty;
            else
                return GetString(stream, position);
        }

        static string GetString(Stream stream, long position)
        {
            byte[] buffer = new byte[100];
            int index = 0, value = 0;

            stream.Position = position;

            while ((value = stream.ReadByte()) > 0 && index < buffer.Length)
                buffer[index++] = (byte)value;

            return Encoding.Default.GetString(buffer, 0, index);
        }
        #endregion

        #region MidOf 方法。获取 min - max 参数的中间值
        static uint MidOf(uint min, uint max, int step)
        {
            uint count = (max - min) / (uint)step;

            count >>= 1;
            count = count == 0 ? 1 : count;

            return min + count * (uint)step;
        }
        #endregion

        #region UIntOf 方法。从流或二进制数据中读取四个字节，并转换为 System.UInt32
        static uint UIntOf(Stream stream, long position, int count, bool reverse)
        {
            byte[] buffer = new byte[4];
            stream.Position = position;
            stream.Read(buffer, 4 - count, count);

            uint result = UIntOf(buffer, reverse) >> ((4 - count) * 8);
            return result;
        }

        static uint UIntOf(byte[] buffer, bool reverse)
        {
            if (reverse)
                return ((((uint)buffer[3])) << 24) |
                        ((((uint)buffer[2])) << 16) |
                        ((((uint)buffer[1])) << 8) |
                        ((uint)buffer[0]);
            else
                return ((((uint)buffer[0])) << 24) |
                    ((((uint)buffer[1])) << 16) |
                    ((((uint)buffer[2])) << 8) |
                    ((uint)buffer[3]);
        }
        #endregion

        #region ByteOf 方法
        static int ByteOf(Stream stream, uint position)
        {
            stream.Position = position;
            return stream.ReadByte();
        }
        #endregion
    }
}