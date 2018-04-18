using Shine.Comman;
using Shine.DataProcessingLogic.Dtos.HostManager.In;
using Shine.DataProcessingLogic.Dtos.OrganzieManager.In;
using Shine.WebApi.Utils;
using System.ComponentModel;
using System.Web.Http;

namespace Shine.WebApi.Controllers.Service
{
    /// <summary>
    /// 主程序数据服务更新接口
    /// </summary>
    [Description("主程序数据服务更新接口")]
    public class ServiceController : ThisBaseApiController
    {
        #region 更新主机实时数据信息
        /// <summary>
        /// 更新主机实时数据信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Service/Host_0x62")]
        [Description("更新主机实时数据")]
        public IHttpActionResult Host_0x62([FromBody]params HostRealTimeDataInputDto[] datas) => Json(HostService.TryCatchAction(m =>
        {
            return m.UpdatedHostTimeDatas(datas);
        }));
        #endregion

        #region 更新分控实时数据信息
        /// <summary>
        /// 更新分控实时数据信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Service/Host_0x16")]
        [Description("更新分控实时数据")]
        public IHttpActionResult Host_0x16([FromBody]params SubRealTimeData_0x16_In[] datas) => Json(SubControlService.TryCatchAction(m =>
        {
            return m.UpdateSubReadTimeData_0x16(datas);
        }));
        #endregion

        #region 更新主机分组信息
        [HttpPost]
        [Route("Service/Host_0x18")]
        [Description("更新主机分组信息")]
        public IHttpActionResult Host_0x18([FromBody] GroupControl_0x18_In data) => Json(GroupControlService.TryCatchAction(m =>
          {
              return m.GroupControls_0x18(data);
          }));
        #endregion

        #region 更新主机分控UID
        [HttpPost]
        [Route("Service/Host_0x29")]
        [Description("更新分控UID信息")]
        public IHttpActionResult Host_0x29([FromBody] SubControl_0x29_In data) => Json(SubControlService.TryCatchAction(m =>
          {
              return m.UpdatedHost_0x29(data);
          }));
        #endregion

        #region 更新主机标准光照计划信息
        [HttpPost]
        [Route("Service/Host_0x54")]
        [Description("更新主机标准光照计划信息")]
        public IHttpActionResult Host_0x54([FromBody] LightPlan_0x54_In data) => Json(LightPlanService.TryCatchAction(m =>
        {
            return m.LightPlan_0x54(data);
        }));
        #endregion

        #region 更新主机隧道光照计划信息
        [HttpPost]
        [Route("Service/Host_0x59")]
        [Description("更新主机隧道光照计划信息")]
        public IHttpActionResult Host_0x59([FromBody] LightPlan_0x59_In data) => Json(LightPlanService.TryCatchAction(m =>
        {
            return m.LightPlan_0x59(data);
        }));
        #endregion

        #region 更新系统参数数据信息
        [HttpPost]
        [Route("Service/Host_0x25")]
        [Description("更新系统参数数据信息")]
        public IHttpActionResult Host_0x25([FromBody] HostParameter_0x25_In data) => Json(HostService.TryCatchAction(m =>
        {
            return m.UpdatedHostParameter_0x25(data);
        }));
        #endregion

        #region 更新主机登陆信息
        [HttpPost]
        [Route("Service/Host_0x44")]
        [Description("更新主机登陆信息")]
        public IHttpActionResult Host_0x44([FromBody] HostLogin_0x44_In data) => Json(HostService.TryCatchAction(m =>
        {
            return m.UpdatedHostLogin_0x44(data);
        }));
        #endregion

        #region 更新主机应答数据
        [HttpPost]
        [Route("Service/Host_0x61")]
        [Description("更新主机应答数据")]
        public IHttpActionResult Host_0x61([FromBody] Host_0x61_In data) => Json(HostService.TryCatchAction(m =>
        {
            return m.UpdatedHost_0x61(data);
        }));
        #endregion

        #region 更新主机光照计划-当前光照度
        [HttpPost]
        [Route("Service/Host_0x56")]
        [Description("更新主机光照计划-当前光照度")]
        public IHttpActionResult Host_0x56(string pack,int brightness) => Json(LightPlanService.TryCatchAction(m =>
        {
            return m.LightPlan_0x56(pack,brightness);
        }));
        #endregion

        #region 更新主机策略信息
        [HttpPost]
        [Route("Service/Host_0x5B")]
        [Description("更新主机策略信息")]
        public IHttpActionResult Host_0x5B([FromBody]HostPolicy_0x5B_In x5B_In) => Json(HostPolicyService.TryCatchAction(m =>
        {
            return m.Updataed_0x5B(x5B_In);
        }));
        #endregion

        #region 警报信息添加
        /// <summary>
        /// 添加警报信息
        /// </summary>
        /// <param name="aleart">警报信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Service/AddAlert")]
        public IHttpActionResult AddAlert([FromBody]params Aleart[] aleart) => Json(UserLoginService.TryCatchAction(
            action: m =>
            {
                aleart.CheckNotNull("aleart");
                return m.AddAlert(aleart);
            }));
        public class Aleart
        {
            /// <summary>
            /// 注册包
            /// </summary>
            public string RegPack { set; get; }

            /// <summary>
            /// 分控编号
            /// </summary>
            public int SubNum { set; get; }

            /// <summary>
            /// 警报类型
            /// 1	超压  超过报警电压
            /// 2	欠压 低于报警电压
            /// 3	开门检测 霍尔传感器
            /// 4	分机功率异常 比对实际功率与额定功率
            /// 5	主机断线 断线时间超过预设的报警时间
            /// 6	分机断线 断线时间超过预设的报警时间
            /// 7	运行日志 存储所有操作日志
            /// 8	主机功率异常 回路检测
            /// </summary>
            public int InType { set; get; }
        }
        #endregion

        #region 获取离线分控
        [HttpGet]
        [Route("Service/GetOffineSubs")]
        public IHttpActionResult GetOffineSubs() => Json(SubControlService.TryCatchAction(
           action: m => {
               return m.GetOffineSubs();
           }));
        #endregion

        #region 获取离线主机
        [HttpGet]
        [Route("Service/GetOffineHosts")]
        public IHttpActionResult GetOffineHosts() => Json(HostService.TryCatchAction(
           action: m => {
               return m.GetOffineHosts();
           }));
        #endregion
    }
}
