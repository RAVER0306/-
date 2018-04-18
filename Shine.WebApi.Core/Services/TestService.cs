using Shine.Core.Data;
using Shine.WebApi.Core.Contracts;
using Shine.WebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine.WebApi.Core.Services
{

    public class TestService : ITestContract
    {
        /// <summary>
        /// 获取或设置 用户仓储对象
        /// </summary>
        public IRepository<UserLogin, Guid> UserRepository { get; set; }


        #region Implementation of ITestContract

        public void Test()
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
                CreatedTime=DateTime.Now,        
            });
            int count = UserRepository.Insert(userLogin);
        }

        #endregion
    }
}
