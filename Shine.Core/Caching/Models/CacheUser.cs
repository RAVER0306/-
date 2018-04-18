using System;

namespace Shine.Core.Caching.Models
{
    /// <summary>
    /// 用户信息缓存
    /// </summary>
    public class CacheUser 
    {
        public CacheUser()
        {
        }

        public CacheUser(Guid id, string userName, string secretKey, bool isAdministrator, int level, Guid organize_Id)
        {
            Id = id;
            UserName = userName;
            SecretKey = secretKey;
            IsAdministrator = isAdministrator;
            Level = level;
            Organize_Id = organize_Id;
        }

        public Guid Id { set; get; }
        public string UserName { set; get; }
        public string SecretKey { set; get; }

        public bool IsAdministrator { set; get; }

        public int Level { set; get; }

        public Guid Organize_Id { set; get; }
    }
}
