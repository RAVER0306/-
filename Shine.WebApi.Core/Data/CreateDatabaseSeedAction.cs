using Shine.Data.EF.Migrations;
using Shine.WebApi.Core.Models;
using System;
using System.Data.Entity;

namespace Shine.WebApi.Core.Data
{
    public class CreateDatabaseSeedAction : ISeedAction
    {
        #region Implementation of ISeedAction

        /// <summary>
        /// 获取 操作排序，数值越小越先执行
        /// </summary>
        public int Order { get { return 1; } }

        /// <summary>
        /// 定义种子数据初始化过程
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Action(DbContext context)
        {
            UserLogin userLogin = new UserLogin()
            {
                Account = "myining",
                AuthorizationList = "1,2",
                Password = "123456",
                Secretkey = "123456",
            };
            userLogin.UserMany.Add(new User
            {
                RealName = "莫奕宁",
                NickName = "华丽的错觉",
                HeadIcon = "",
                Email = "724405566@qq.com",
                WeChat = "myining",
                PhoneNumber = "13102263109",
                Description = "",
                Language = "",
                Theme = "",
                CreatedTime=DateTime.Now
            });
            context.Set<UserLogin>().Add(userLogin);
        }
        #endregion
    }
}
