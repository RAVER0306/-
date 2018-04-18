namespace Shine.Comman.Net
{
    /// <summary>
    /// 表示纯真IP数据库的位置信息
    /// </summary>
    public class IPLocation
    {
        #region 静态字段、成员字段
        /// <summary>
        /// 表示空信息的 IPLocation 实例
        /// </summary>
        public static readonly IPLocation Empty;
        string country, area;
        #endregion

        #region 构造方法
        static IPLocation()
        {
            IPLocation.Empty = new IPLocation(string.Empty, string.Empty);
        }

        private IPLocation(string country, string area)
        {
            this.country = country;
            this.area = area;
        }
        #endregion

        #region Country、Area 属性
        /// <summary>
        /// 获取国家信息的 System.String
        /// </summary>
        public string Country
        {
            get { return this.country; }
        }

        /// <summary>
        /// 获取地区信息的 System.String
        /// </summary>
        public string Area
        {
            get { return this.area; }
        }
        #endregion

        #region IsEmpty 属性。获取一个值指示 QQWryLocation 实例是否是空信息
        /// <summary>
        /// 获取一个值指示 IPLocation 实例是否是空信息
        /// </summary>
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(this.country) && string.IsNullOrEmpty(this.area); }
        }
        #endregion

        #region ToString 方法。返回当前实例的字符串表现形式
        /// <summary>
        /// 已重写。返回当前实例的字符串表现形式
        /// </summary>
        /// <returns>当前实例的字符串表现形式</returns>
        public override string ToString()
        {
            return IPLocation.ToString(this, ", ", "Empty");
        }

        /// <summary>
        /// 已重写。返回当前实例的字符串表现形式
        /// </summary>
        /// <param name="separator">分隔国家地区信息的分隔符</param>
        /// <param name="emptyText">信息不存在时的默认文本</param>
        /// <returns>当前实例的字符串表现形式</returns>
        public string ToString(string separator, string emptyText)
        {
            return IPLocation.ToString(this, separator, emptyText);
        }

        static string ToString(IPLocation obj, string separator, string emptyText)
        {
            bool empty1 = string.IsNullOrEmpty(obj.country);
            bool empty2 = string.IsNullOrEmpty(obj.area);

            if (empty1 && empty2)
                return emptyText;
            else if (empty1)
                return obj.area;
            else if (empty2)
                return obj.country;
            else
                return obj.country + separator + obj.area;
        }
        #endregion

        #region Equals 方法。确定指定的 System.Object 是否等于当前对象的值
        /// <summary>
        /// 已重写。确定指定的 System.Object 是否等于当前对象的值
        /// </summary>
        /// <param name="obj">与当前对象进行比较的 System.Object</param>
        /// <returns>如果指定的 System.Object 等于当前对象则为 true，否则为 false</returns>
        public override bool Equals(object obj)
        {
            return IPLocation.Equals(this, obj);
        }

        static bool Equals(IPLocation obj, object x)
        {
            IPLocation compare = x as IPLocation;
            if (compare == null)
                return false;
            else
                return string.Compare(compare.country, obj.country, true) == 0 &&
                    string.Compare(compare.area, obj.area, true) == 0;
        }
        #endregion

        #region Create 方法。创建 QQWryLocation 实例
        /// <summary>
        /// 创建 IPLocation 实例
        /// </summary>
        /// <param name="country">国家信息</param>
        /// <param name="area">地区信息</param>
        /// <returns>IPLocation 实例</returns>
        internal static IPLocation Create(string country, string area)
        {
            if (string.IsNullOrEmpty(country) && string.IsNullOrEmpty(area))
                return IPLocation.Empty;

            country = country ?? string.Empty;
            area = area ?? string.Empty;

            // 通过正则去除可能出现的空格以及控制字符（ASCII 小于 32 字符）、移除 cz88.net（作者网站） 之类字符串
            country = System.Text.RegularExpressions.Regex.Replace(country, "[\\x00-\\x20\\s]s*", string.Empty);
            area = System.Text.RegularExpressions.Regex.Replace(area, "[\\x00-\\x20\\s]*", string.Empty);
            country = System.Text.RegularExpressions.Regex.Replace(country, "\\s*cz88\\.net\\s*", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            area = System.Text.RegularExpressions.Regex.Replace(area, "\\s*cz88\\.net\\s*", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return new IPLocation(country, area);
        }
        #endregion

        #region GetHashCode 方法。用作特定类型的哈希函数
        /// <summary>
        /// 已重写。用作特定类型的哈希函数 GetHashCode 适合在哈希算法和数据结构（如哈希表）中使用。
        /// </summary>
        /// <returns>当前对象的哈希代码</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion

        #region 运算符重载
        /// <summary>
        /// 比较两个 IPLocation 实例内容是否相等
        /// </summary>
        /// <param name="x">要被比较的对象</param>
        /// <param name="y">要比较的对象</param>
        /// <returns>两个 IPLocation 实例内容不等则返回 true 否则返回 false</returns>
        public static bool operator ==(IPLocation x, IPLocation y)
        {
            if (object.ReferenceEquals(x, null) && object.ReferenceEquals(y, null))
                return true;
            else if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;
            else
                return x.Equals(y);
        }

        /// <summary>
        /// 比较两个 IPLocation 实例内容是否不等
        /// </summary>
        /// <param name="x">要被比较的对象</param>
        /// <param name="y">要比较的对象</param>
        /// <returns>两个 IPLocation 实例内容不等则返回 true 否则返回 false</returns>
        public static bool operator !=(IPLocation x, IPLocation y)
        {
            return !(x == y);
        }

        /// <summary>
        /// 将 Yunzhigu.Xiangshi.IO.IPLocation 实例隐式转换为 System.String 类型的文本表现形式
        /// </summary>
        /// <param name="x">要被转换的 Yunzhigu.Xiangshi.IO.IPLocation 实例</param>
        /// <returns>Yunzhigu.Xiangshi.IO.IPLocation 实例的文本表现形式</returns>
        public static implicit operator string(IPLocation x)
        {
            return x.ToString();
        }
        #endregion
    }
}