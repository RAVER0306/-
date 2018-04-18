using Shine.Data.EF.Migrations;
using System.Data.Entity;
using Shine.DataProcessingLogic.Models.OrganizeManager;
using System;
using System.Linq;
using Shine.Comman.Data;
using Shine.DataProcessingLogic.Models.UserManager;
using Shine.Comman;
using Shine.Comman.Secutiry;
using Shine.Comman.Drawing;
using Shine.DataProcessingLogic.Models.HostManager;
using System.Collections.Generic;
using Shine.Comman.Extensions;
using Shine.Comman.Logging;

namespace Shine.DataProcessingLogic.Data
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
            try
            {
                context.Database.Log = new Action<string>(m => { LogManager.GetLogger(GetType()).Info(m); });

                #region 初始化添加组织类型
                //创建Organize<组织类型>表的类型字典目录
                DataItem organizeItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    QueryCoding = "OrganizeType",
                    FullName = "组织类型",
                    IsPublic = false,
                    ParentId = null,
                    IsSystem = true
                };
                organizeItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = organizeItem,
                        IsLocked = false,
                        QueryCoding = "OrganizeType",
                        FullName = "机构",
                        Index = 0,
                        IsSystem = true
                    });
                organizeItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = organizeItem,
                        IsLocked = false,
                        QueryCoding = "OrganizeType",
                        FullName = "项目",
                        Index = 1,
                        IsSystem = true
                    });
                organizeItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = organizeItem,
                        IsLocked = false,
                        QueryCoding = "OrganizeType",
                        FullName = "小组",
                        Index = 2,
                        IsSystem = true
                    });
                context.Set<DataItem>().Add(organizeItem);
                #endregion

                #region 初始化添加黑名单类型
                //创建UserBlackList<黑名单类型>表的类型字典目录
                DataItem userBlackListItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "黑名单类型",
                    IsPublic = false,
                    QueryCoding = "UserBlackListType",
                    IsSystem = true
                };
                userBlackListItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = userBlackListItem,
                        IsLocked = false,
                        QueryCoding = "UserBlackListType",
                        FullName = "主机黑名单",
                        Index = 0,
                        IsSystem = true
                    });
                userBlackListItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = userBlackListItem,
                        IsLocked = false,
                        QueryCoding = "UserBlackListType",
                        FullName = "分控黑名单",
                        Index = 1,
                        IsSystem = true
                    });
                context.Set<DataItem>().Add(userBlackListItem);
                #endregion

                #region 初始化添加信息类型
                //创建Information<信息类型>表的类型字典目录
                DataItem informationItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "信息类型",
                    IsPublic = false,
                    QueryCoding = "InformationType",
                    IsSystem = true
                };

                //主机类型 0-10
                informationItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = informationItem,
                        IsLocked = false,
                        QueryCoding = "InformationType",
                        FullName = "主机断线",
                        Index = 0,
                        IsSystem = true
                    });
                informationItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = informationItem,
                        IsLocked = false,
                        QueryCoding = "InformationType",
                        FullName = "主机电压超压",
                        Index = 1,
                        IsSystem = true
                    });
                informationItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = informationItem,
                        IsLocked = false,
                        QueryCoding = "InformationType",
                        FullName = "主机电压欠压",
                        Index = 2,
                        IsSystem = true
                    });
                informationItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = informationItem,
                        IsLocked = false,
                        QueryCoding = "InformationType",
                        FullName = "主机门被打开",
                        Index = 3,
                        IsSystem = true
                    });
                informationItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = informationItem,
                        IsLocked = false,
                        QueryCoding = "InformationType",
                        FullName = "主机功率异常",
                        Index = 4,
                        IsSystem = true
                     });

                //分控类型11-20
                informationItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = informationItem,
                        IsLocked = false,
                        QueryCoding = "InformationType",
                        FullName = "分控断线",
                        Index = 11,
                        IsSystem = true
                    });
                informationItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = informationItem,
                        IsLocked = false,
                        QueryCoding = "InformationType",
                        FullName = "分机功率异常",
                        Index = 12,
                        IsSystem = true
                    });

                context.Set<DataItem>().Add(informationItem);
                #endregion

                #region 初始化添加主机类型
                //创建Host<主机类型>表的类型字典目录
                DataItem hostItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "主机类型",
                    IsPublic = true,
                    QueryCoding = "HostType",
                    IsSystem = true
                };
                hostItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = hostItem,
                        IsLocked = false,
                        QueryCoding = "HostType",
                        FullName = "主机类型1",
                        Index = 0,
                        IsSystem = true
                    });
                hostItem.DataItemDetailMany.Add(
                   new DataItemDetail
                   {
                       CreatedTime = DateTime.Now,
                       IsPublic = true,
                       DataItemOne = hostItem,
                       IsLocked = false,
                       QueryCoding = "HostType",
                       FullName = "主机类型2",
                       Index = 1,
                       IsSystem = true
                   });
                context.Set<DataItem>().Add(hostItem);
                #endregion

                #region 初始化添加分控类型
                DataItem subItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "分控类型",
                    IsPublic = true,
                    QueryCoding = "SubType",
                    IsSystem = true
                };
                subItem.DataItemDetailMany.Add(
                    new DataItemDetail
                    {
                        CreatedTime = DateTime.Now,
                        IsPublic = true,
                        DataItemOne = subItem,
                        IsLocked = false,
                        QueryCoding = "SubType",
                        FullName = "分控类型1",
                        Index = 0,
                        IsSystem = true
                    });
                subItem.DataItemDetailMany.Add(
                  new DataItemDetail
                  {
                      CreatedTime = DateTime.Now,
                      IsPublic = true,
                      DataItemOne = subItem,
                      IsLocked = false,
                      QueryCoding = "SubType",
                      FullName = "分控类型2",
                      Index = 1,
                      IsSystem = true
                  });

                context.Set<DataItem>().Add(subItem);

                #endregion

                #region 初始化添加灯杆类型
                DataItem lightPoleItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "灯杆类型",
                    IsPublic = true,
                    QueryCoding = "PoleType",
                    IsSystem = true
                };

                lightPoleItem.DataItemDetailMany.Add(new DataItemDetail {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = lightPoleItem,
                    IsLocked = false,
                    QueryCoding = "PoleType",
                    FullName = "灯杆类型1",
                    Index = 0,
                    IsSystem = true
                });

                lightPoleItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = lightPoleItem,
                    IsLocked = false,
                    QueryCoding = "PoleType",
                    FullName = "灯杆类型2",
                    Index = 1,
                    IsSystem = true
                });

                context.Set<DataItem>().Add(lightPoleItem);
                #endregion

                #region 初始化添加灯具类型
                DataItem lightsItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "灯具类型",
                    IsPublic = true,
                    QueryCoding = "LigthsType",
                    IsSystem = true
                };

                lightsItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = lightsItem,
                    IsLocked = false,
                    QueryCoding = "LigthsType",
                    FullName = "灯具类型1",
                    Index = 0,
                    IsSystem = true
                });
                lightsItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = lightsItem,
                    IsLocked = false,
                    QueryCoding = "LigthsType",
                    FullName = "灯具类型2",
                    Index = 1,
                    IsSystem = true
                });
                context.Set<DataItem>().Add(lightsItem);
                #endregion

                #region 初始化光照计划类型
                DataItem lightPlanItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "光照计划类型",
                    IsPublic = true,
                    QueryCoding = "LigthPlanType",
                    IsSystem = true
                };
                lightPlanItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = lightPlanItem,
                    IsLocked = false,
                    QueryCoding = "LigthPlanType",
                    FullName = "标准光照计划",
                    Index = 0,
                    IsSystem = true
                });
                lightPlanItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = lightPlanItem,
                    IsLocked = false,
                    QueryCoding = "LigthPlanType",
                    FullName = "隧道光照计划",
                    Index = 1,
                    IsSystem = true
                });
                context.Set<DataItem>().Add(lightPlanItem);
                #endregion

                #region 初始化升级包的类型
                DataItem PackItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "升级包类型",
                    IsPublic = true,
                    QueryCoding = "PacketType",
                    IsSystem = true
                };
                PackItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = PackItem,
                    IsLocked = false,
                    QueryCoding = "PacketType",
                    FullName = "主机升级包",
                    Index = 0,
                    IsSystem = true
                });
                PackItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = PackItem,
                    IsLocked = false,
                    QueryCoding = "PacketType",
                    FullName = "分控升级包",
                    Index = 1,
                    IsSystem = true
                });
                context.Set<DataItem>().Add(PackItem);
                #endregion

                #region 初始化策略下发状态类型
                DataItem HostPolicyItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "策略下发类型",
                    IsPublic = false,
                    QueryCoding = "PolicyType",
                    IsSystem = true
                };
                HostPolicyItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = HostPolicyItem,
                    IsLocked = false,
                    QueryCoding = "PolicyType",
                    FullName = "策略下发等待中",
                    Index = 0,
                    IsSystem = true
                });
                HostPolicyItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = HostPolicyItem,
                    IsLocked = false,
                    QueryCoding = "PolicyType",
                    FullName = "策略下发已成功",
                    Index = 1,
                    IsSystem = true
                });
                HostPolicyItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = HostPolicyItem,
                    IsLocked = false,
                    QueryCoding = "PolicyType",
                    FullName = "策略下发已失败",
                    Index = 2,
                    IsSystem = true
                });
                context.Set<DataItem>().Add(HostPolicyItem);
                #endregion

                #region 初始化分组的类型
                DataItem GroupItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "分组类型",
                    IsPublic = true,
                    QueryCoding = "GroupType",
                    IsSystem = true
                };
                GroupItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = GroupItem,
                    IsLocked = false,
                    QueryCoding = "GroupType",
                    FullName = "主机上的节点分组",
                    Index = 0,
                    IsSystem = true
                });
                GroupItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = true,
                    DataItemOne = GroupItem,
                    IsLocked = false,
                    QueryCoding = "GroupType",
                    FullName = "未命名分组",
                    Index = 1,
                    IsSystem = true
                });
                context.Set<DataItem>().Add(GroupItem);
                #endregion

                #region 初始化能耗存储类型
                DataItem ECTItem = new DataItem
                {
                    CreatedTime = DateTime.Now,
                    FullName = "能耗存储类型",
                    IsPublic = false,
                    QueryCoding = "ECType",
                    IsSystem = true
                };
                ECTItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = false,
                    DataItemOne = ECTItem,
                    IsLocked = false,
                    QueryCoding = "ECType",
                    FullName = "主机能耗",
                    Index = 0,
                    IsSystem = true
                });

                ECTItem.DataItemDetailMany.Add(new DataItemDetail
                {
                    CreatedTime = DateTime.Now,
                    IsPublic = false,
                    DataItemOne = ECTItem,
                    IsLocked = false,
                    QueryCoding = "ECType",
                    FullName = "分控能耗",
                    Index = 1,
                    IsSystem = true
                });
                context.Set<DataItem>().Add(ECTItem);
                #endregion

                #region 初始化系统内置组织和默认帐号
                Organize organize = new Organize
                {
                    DataItemDetailOne = organizeItem.DataItemDetailMany.First(m => m.QueryCoding == "OrganizeType" && m.Index == 0),
                    Email = "724405566@qq.com",
                    Fax = "0755-29686239",
                    FullName = "系统内建组织",
                    Address = "深圳市宝安区石岩街道洲石路达成工业园",
                    City = "深圳市",
                    Country = "中国",
                    County = "宝安区",
                    CreatedTime = DateTime.Now,
                    Province = "广东省",
                    Remark = "该数据由系统初始化时创建",
                    SortCode = (int)DateTime.Now.GetTimeStamp(),
                    TelePhone = "0755-29686239",
                    OrganizeLogo = Properties.Resources.OrganizeLog.ToBytes()
                };
                string key = new Random().NextLetterString(16).ToUpper();
                UserLogin userLogin = new UserLogin
                {
                    UserName = "leyviewroot",
                    SecretKey = key,
                    Password = ($"12345678").AESEncrypt128(key),
                    AccessFailedCount = 10,
                    IsAdministrator = true,
                    IsLocked = false,
                    Level = 1,
                    OrganizeOne = organize,
                    LoginCount = 0,
                    PermissionList = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30",
                    LockoutEnabled = true,
                };
                userLogin.UserMany.Add(new User
                {
                    CreatedTime = DateTime.Now,
                    Email = "724405566@qq.com",
                    NickName = "系统超级管理员",
                    PhoneNumber = "13102263109",
                    RealName = "莫奕宁",
                    Sex = 1,
                    WeChat = "myining",
                    UserLoginOne = userLogin,
                    HeadIcon = Properties.Resources._default.ToBytes()
                });
                userLogin.UserBlackListMany.Add(new UserBlackList
                {
                    CreatedTime = DateTime.Now,
                    BlackList = "",
                    DataItemDetailOne=userBlackListItem.DataItemDetailMany.First(),                  
                });
                userLogin.UserOrganizeMapMany.Add(new UserOrganizeMap
                {
                    CreatedTime = DateTime.Now,
                    OrganizeOne=organize,                   
                });
                organize.UserLoginMany.Add(userLogin);
                context.Set<Organize>().Add(organize);
                #endregion

                #region 添加初始化测试主机
                Host host = new Host
                {
                    CreatedTime = DateTime.Now,
                    Address = "深圳市宝安区石岩街道洲石路达成工业园",
                    DataItemDetailOne = hostItem.DataItemDetailMany.First(),
                    FullName = "雷雁测试主机",
                    HeartBag = "000000",
                    OrganizeOne = organize,
                    IsEnergySwitch = true,
                    PhoneNum = "13102263109",
                    Remark = "系统初始化生成主机",
                    RegPackage = "000000",
                    IsLocked = false,                                      
                };
                host.HostRealTimeDataMany.Add(new HostRealTimeData
                {
                    Temperature = 20,
                    Voltage = "210,200,243",
                    Power = "220,220,220",
                    Current = "0.78,0.25,0.28",
                    HostOne = host,
                    IsAlarmMark = false,
                    IsDataError = false,
                    IsOnline = true,
                    Latitude = 70,
                    Longitude = 120,
                    LoopState = "1,1,1,1,0,0,1,1",
                    TimeZone = 8,
                    UpdateTime = DateTime.Now
                });
                LightPole pole = new LightPole
                {
                    Address = "深圳市宝安区石岩街道洲石路达成工业园西南角",
                    CreatedTime = DateTime.Now,
                    DataItemDetailOne = lightPoleItem.DataItemDetailMany.First(),
                    PoleName = "灯杆1号",
                    PoleNum = 1,
                    Remark = "3.5m双灯灯杆",
                };
                SubControl sub = new SubControl
                {
                    CreatedTime = DateTime.Now,
                    DataItemDetailOne = subItem.DataItemDetailMany.First(),
                    Remark = "系统初始化生成测试分控",
                    SubName = "测试分控1",
                    SubNum = 1
                };
                sub.SubRealTimeDataMany.Add(
                    new SubRealTimeData
                    {
                        IsAlarmMark = false,
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Current = 0.2,
                        Brightness = 100,
                        DataItemDetailOne = lightsItem.DataItemDetailMany.First(),
                        DimmingPort = 1,
                        Frequency = 9600,
                        Power = 200,
                        Voltage = 230,
                        SubControlOne = sub,
                        Temperature = 20
                    });
                pole.SubControlMany.Add(sub);
                host.LightPoleMany.Add(pole);
                context.Set<Host>().Add(host);
                #endregion

                #region 灯具聚合
                SubAggregation subAggregation = new SubAggregation
                {
                    CreatedTime = DateTime.Now,
                    HostOne = host,
                    SubControlOne = sub,
                    OrganizeOne = organize,
                    LightPoleOne = host.LightPoleMany.First()
                };
                context.Set<SubAggregation>().Add(subAggregation);
                #endregion

                #region 添加初始化测试警报信息
                //context.Set<Information>().AddRange(
                //   new List<Information>()
                //    {
                //        new Information{
                //        CreatedTime = DateTime.Now,
                //        DataItemDetailOne = informationItem.DataItemDetailMany.First(m=>m.Index==0),
                //        IsReaded = false,
                //        ObjectId = host.Id,
                //        OrganizeOne = organize,
                //        UserLoginOne = userLogin,
                //    },
                //    new Information
                //    {
                //        CreatedTime = DateTime.Now,
                //        DataItemDetailOne =  informationItem.DataItemDetailMany.First(m=>m.Index==11),
                //        IsReaded = false,
                //        ObjectId = sub.Id,
                //        OrganizeOne = organize,
                //        UserLoginOne = userLogin,
                //    }
                   //});
                #endregion

                #region 添加组织机构树结构查询存储过程
                string organizeTreeSqlText =
                #region 数据库语句
                @"
CREATE PROCEDURE Tree_Organize
	@Id uniqueidentifier,	
	@SPAction bit = 'TRUE' 	
AS

BEGIN
 SET NOCOUNT ON;
 IF @SPAction='TRUE'
 BEGIN
	WITH Cxt_Child (
	Id,
	DataItemDetail_Id,
	OrganizeLogo,
	OrganizeLogoPath,
	ParentId,
	TelePhone,
	Fax,
	FullName,
	Email,
	Country,
	Province,
	City,
	County,
	Address,
	Remark,
	SortCode,
	CreatedTime,
	CreatorUserId,
	LastUpdatedTime,
	LastUpdatorUserId,
    LEVEL) AS
	(
		SELECT *,0 AS LEVEL
		FROM
		dbo.Organize
		WHERE Id=@Id
		UNION ALL	
		SELECT 
		o.*,
		b.LEVEL + 1
		FROM 
		dbo.Organize o
		INNER JOIN Cxt_Child b ON (o.ParentId = b.Id)
	)
 SELECT * FROM Cxt_Child
 END
 
 ELSE
 BEGIN
 WITH Cxt_Child (  Id,DataItemDetail_Id,OrganizeLogo,OrganizeLogoPath,ParentId,TelePhone,Fax,FullName,Email,Country,Province,City,County,Address,Remark,SortCode,CreatedTime,CreatorUserId,LastUpdatedTime,LastUpdatorUserId ) AS
	(
		SELECT *
		FROM
		dbo.Organize
		WHERE Id=@Id
		UNION ALL
		
		SELECT 
		o.*
		FROM 
		dbo.Organize o
		INNER JOIN Cxt_Child b ON (o.Id = b.ParentId)
	)
 SELECT * FROM Cxt_Child
 END
END
";
                #endregion
                context.Database.ExecuteSqlCommand(organizeTreeSqlText);
                #endregion

                #region 添加查询月能耗统计存储过程
                string sum_MonthSqlText =
                #region 数据库语句
                    @"
CREATE PROCEDURE [dbo].[Sp_SumYearPower]
@DataItemDetailId uniqueidentifier,							-- 指定能耗类型主键
@ThisYear INT,															-- 指定年能耗
@OrganizeId uniqueidentifier 											-- 指定组织机构
AS
BEGIN
SELECT
dbo.AnnualElectricity.[Year],
dbo.AnnualElectricity.DataItemDetail_Id,
Sum(dbo.AnnualElectricity.M1) AS Month_Sum1,
Sum(dbo.AnnualElectricity.M2) AS Month_Sum2,
Sum(dbo.AnnualElectricity.M3) AS Month_Sum3,
Sum(dbo.AnnualElectricity.M4) AS Month_Sum4,
Sum(dbo.AnnualElectricity.M5) AS Month_Sum5,
Sum(dbo.AnnualElectricity.M6) AS Month_Sum6,
Sum(dbo.AnnualElectricity.M7) AS Month_Sum7,
Sum(dbo.AnnualElectricity.M8) AS Month_Sum8,
Sum(dbo.AnnualElectricity.M9) AS Month_Sum9,
Sum(dbo.AnnualElectricity.M10) AS Month_Sum10,
Sum(dbo.AnnualElectricity.M11) AS Month_Sum11,
Sum(dbo.AnnualElectricity.M12) AS Month_Sum12,
Sum(dbo.AnnualElectricity.YearTotal) AS YearTotal_Sum,
Sum(dbo.AnnualElectricity.Cumulative) AS Cumulative_Sum
FROM
dbo.AnnualElectricity
WHERE
dbo.AnnualElectricity.DataItemDetail_Id=@DataItemDetailId
AND dbo.AnnualElectricity.[Year]=@ThisYear
AND dbo.AnnualElectricity.Organzie_Id=@OrganizeId
GROUP BY 
dbo.AnnualElectricity.[Year],
dbo.AnnualElectricity.DataItemDetail_Id
END
                    ";
                #endregion
                context.Database.ExecuteSqlCommand(sum_MonthSqlText);
                #endregion

                #region 添加查询天能耗统计的存储过程
                string sum_DaySqlText =
                #region 数据库语句
                    @"
CREATE PROCEDURE [dbo].[Sp_SumMonthPower]
  @Month AS int ,
  @OrganizeId AS uniqueidentifier ,
  @ItemId AS uniqueidentifier,
  @Year AS INT 
AS
BEGIN
SELECT
Sum(t1.D1) AS Sum_D1,
Sum(t1.D2) AS Sum_D2,
Sum(t1.D3) AS Sum_D3,
Sum(t1.D4) AS Sum_D4,
Sum(t1.D5) AS Sum_D5,
Sum(t1.D6) AS Sum_D6,
Sum(t1.D7) AS Sum_D7,
Sum(t1.D8) AS Sum_D8,
Sum(t1.D9) AS Sum_D9,
Sum(t1.D10) AS Sum_D10,
Sum(t1.D11) AS Sum_D11,
Sum(t1.D12) AS Sum_D12,
Sum(t1.D13) AS Sum_D13,
Sum(t1.D14) AS Sum_D14,
Sum(t1.D15) AS Sum_D15,
Sum(t1.D16) AS Sum_D16,
Sum(t1.D17) AS Sum_D17,
Sum(t1.D18) AS Sum_D18,
Sum(t1.D19) AS Sum_D19,
Sum(t1.D20) AS Sum_D20,
Sum(t1.D21) AS Sum_D21,
Sum(t1.D22) AS Sum_D22,
Sum(t1.D23) AS Sum_D23,
Sum(t1.D24) AS Sum_D24,
Sum(t1.D25) AS Sum_D25,
Sum(t1.D26) AS Sum_D26,
Sum(t1.D27) AS Sum_D27,
Sum(t1.D28) AS Sum_D28,
Sum(t1.D29) AS Sum_D29,
Sum(t1.D30) AS Sum_D30,
Sum(t1.D31) AS Sum_D31,
SUM(t1.MonthTotal) AS Sum_MonthTotal
t1.DataItemDetail_Id,
t1.[Month]
FROM
(SELECT
dbo.MonthElectricity.*,
dbo.AnnualElectricity.DataItemDetail_Id
FROM
dbo.MonthElectricity
INNER JOIN dbo.AnnualElectricity 
ON dbo.MonthElectricity.AnnualElectricity_Id = dbo.AnnualElectricity.Id
WHERE
dbo.AnnualElectricity.DataItemDetail_Id = @ItemId
AND dbo.AnnualElectricity.Organzie_Id = @OrganizeId
AND dbo.MonthElectricity.[Month]=@Month
AND dbo.AnnualElectricity.[Year]=@Year) AS t1
GROUP BY 
t1.DataItemDetail_Id,
t1.[Month]
END
                    ";
                #endregion
                context.Database.ExecuteSqlCommand(sum_DaySqlText);
                #endregion

                #region 添加查询按小时统计能耗的存储过程
                string sum_HourSqlText =
                #region 数据库语句
                    @"
CREATE PROCEDURE [dbo].[Sp_SumDayPower]
  @Day INT,
  @OrganizeId uniqueidentifier ,
  @ItemId uniqueidentifier,
	@Year INT,
  @Month INT
AS
BEGIN
SELECT
Sum(t1.H1) AS Sum_H1,
Sum(t1.H2) AS Sum_H2,
Sum(t1.H3) AS Sum_H3,
Sum(t1.H4) AS Sum_H4,
Sum(t1.H5) AS Sum_H5,
Sum(t1.H6) AS Sum_H6,
Sum(t1.H7) AS Sum_H7,
Sum(t1.H8) AS Sum_H8,
Sum(t1.H9) AS Sum_H9,
Sum(t1.H10) AS Sum_H10,
Sum(t1.H11) AS Sum_H11,
Sum(t1.H12) AS Sum_H12,
Sum(t1.H13) AS Sum_H13,
Sum(t1.H14) AS Sum_H14,
Sum(t1.H15) AS Sum_H15,
Sum(t1.H16) AS Sum_H16,
Sum(t1.H17) AS Sum_H17,
Sum(t1.H18) AS Sum_H18,
Sum(t1.H19) AS Sum_H19,
Sum(t1.H20) AS Sum_H20,
Sum(t1.H21) AS Sum_H21,
Sum(t1.H22) AS Sum_H22,
Sum(t1.H23) AS Sum_H23,
Sum(t1.H24) AS Sum_H24,
Sum(t1.DayTotal) AS Sum_DayTotal,
t1.DataItemDetail_Id
FROM
(SELECT
dbo.DayElectricity.*,
dbo.AnnualElectricity.DataItemDetail_Id,
dbo.AnnualElectricity.Organzie_Id
FROM
dbo.DayElectricity
INNER JOIN dbo.MonthElectricity ON dbo.DayElectricity.MonthElectricity_Id = dbo.MonthElectricity.Id
INNER JOIN dbo.AnnualElectricity ON dbo.MonthElectricity.AnnualElectricity_Id = dbo.AnnualElectricity.Id
WHERE
dbo.AnnualElectricity.DataItemDetail_Id = @ItemId
AND dbo.AnnualElectricity.Organzie_Id = @OrganizeId
AND dbo.AnnualElectricity.[Year]=@Year
AND dbo.MonthElectricity.[Month]=@Month
AND dbo.DayElectricity.Today=@Day
) AS t1
GROUP BY 
t1.DataItemDetail_Id
END
                    ";
                #endregion
                context.Database.ExecuteSqlCommand(sum_HourSqlText);
                #endregion

                #region 添加掉线主机查询视图
                string GetOffineHosts =
                #region 数据库语句
                    @"
CREATE VIEW [dbo].[View_HostOffines] AS 
SELECT DISTINCT
dbo.UserLogin.UserName,
dbo.[User].NickName,
dbo.[User].Email,
dbo.Organize.FullName AS OrgName,
dbo.Host.RegPackage,
dbo.Host.FullName AS HostName,
dbo.HostRealTimeData.Voltage,
dbo.HostRealTimeData.[Current],
dbo.HostRealTimeData.Power,
dbo.HostRealTimeData.Temperature,
dbo.HostRealTimeData.UpdateTime,
[Level]
FROM
dbo.UserLogin
INNER JOIN dbo.[User] ON dbo.[User].UserLogin_Id = dbo.UserLogin.Id
INNER JOIN dbo.UserOrganizeMap ON dbo.UserOrganizeMap.UserLogin_Id = dbo.UserLogin.Id
INNER JOIN dbo.Organize ON dbo.UserLogin.Organize_Id = dbo.Organize.Id AND dbo.UserOrganizeMap.Organize_Id = dbo.Organize.Id
INNER JOIN dbo.Host ON dbo.Host.Organize_Id = dbo.Organize.Id
INNER JOIN dbo.HostRealTimeData ON dbo.HostRealTimeData.Host_Id = dbo.Host.Id
WHERE
DATEDIFF(mi,dbo.HostRealTimeData.UpdateTime,GETDATE()) >= 30 AND
DATEDIFF(d, HostRealTimeData.UpdateTime, GETDATE()) <= 1 AND
dbo.[User].Email IS NOT NULL AND
dbo.[User].IsAlarm =1
GO
                    ";
                #endregion
                context.Database.ExecuteSqlCommand(GetOffineHosts);
                #endregion

                #region 添加掉线分控查询视图
                string GetOffineSubs =
                #region 数据库语句
                    @"
SELECT DISTINCT
dbo.UserLogin.UserName,
dbo.[User].NickName,
dbo.[User].Email,
dbo.Organize.FullName AS OrgName,
dbo.Host.FullName AS HostName,
dbo.Host.RegPackage,
dbo.LightPole.PoleNum,
dbo.LightPole.PoleName AS PoleName,
dbo.SubControl.SubNum,
dbo.SubControl.SubName,
dbo.SubRealTimeData.DimmingPort,
dbo.SubRealTimeData.UpdateTime,
[Level]
FROM
dbo.UserLogin
INNER JOIN dbo.[User] ON dbo.[User].UserLogin_Id = dbo.UserLogin.Id
INNER JOIN dbo.UserOrganizeMap ON dbo.UserOrganizeMap.UserLogin_Id = dbo.UserLogin.Id
INNER JOIN dbo.Organize ON dbo.UserLogin.Organize_Id = dbo.Organize.Id AND dbo.UserOrganizeMap.Organize_Id = dbo.Organize.Id
INNER JOIN dbo.Host ON dbo.Host.Organize_Id = dbo.Organize.Id
INNER JOIN dbo.LightPole ON dbo.LightPole.Host_Id = dbo.Host.Id
INNER JOIN dbo.SubControl ON dbo.SubControl.LigthPoleOne_Id = dbo.LightPole.Id
INNER JOIN dbo.SubRealTimeData ON dbo.SubRealTimeData.SubControl_Id = dbo.SubControl.Id
WHERE
	DATEDIFF(mi,dbo.SubRealTimeData.UpdateTime,GETDATE()) >= 60
AND DATEDIFF(d,dbo.SubRealTimeData.UpdateTime,GETDATE()) <= 1
AND dbo.[User].Email IS NOT NULL 
AND dbo.[User].IsAlarm=1
GO
                    ";
                #endregion
                #endregion
                context.Database.ExecuteSqlCommand(GetOffineSubs);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(GetType()).Fatal(ex);
            }         
        }
        #endregion
    }
}
