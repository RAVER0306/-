using Newtonsoft.Json;
using Shine.Comman.Data;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http;

namespace Shine.WebApi.Controllers.Cmds
{
    [Description("指令下发控制接口")]
    public class SendCmdsController : ApiController
    {
        #region 通用控制项
        /// <summary>
        /// 通用控制项
        /// </summary>
        /// <param name="add">路由名</param>
        /// <param name="Value">参数值</param>
        /// <returns></returns>
        private IHttpActionResult BaseUrl(string add, string Value)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string baseUrl = string.Format("http://127.0.0.1:2500/Send/{0}?Value={1}", add, Value);
                    var response = client.PostAsync(baseUrl, null).Result;
                    string obj = response.Content.ReadAsStringAsync().Result;
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(obj);
                    return Json(result);
                }
            }
            catch
            {
                return Json(new OperationResult(OperationResultType.Error,"连接到服务主程序失败，请检查服务主程序是否启动！" ));
            }
        }
        #endregion

        #region 实时控制

        #region 单灯控制
        /// <summary>
        /// 单灯控制--0x11
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,单灯号,端口,亮度,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/SingleLamp")]
        [HttpPost]
        public IHttpActionResult SingleLamp(string Value) => BaseUrl("SingleLamp", Value);
        #endregion

        #region 群灯控制
        /// <summary>
        /// 群灯控制--0x12
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,端口,亮度,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ManyLamp")]
        [HttpPost]
        public IHttpActionResult ManyLamp(string Value) => BaseUrl("ManyLamp", Value);
        #endregion

        #region 分组控制
        /// <summary>
        /// 分组控制--0x13
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,端口,亮度,分组号,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/Grouping")]
        [HttpPost]
        public IHttpActionResult Grouping(string Value) => BaseUrl("Grouping", Value);
        #endregion

        #region 回路控制
        /// <summary>
        /// 回路控制:--0x14
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,掩码01开、00关,回路值,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/Loops")]
        [HttpPost]
        public IHttpActionResult Loops(string Value) => BaseUrl("Loops", Value);
        #endregion

        #region 分控信息获取
        /// <summary>
        /// 分控信息获取--0x15
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,分控编号,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ReadSubControl")]
        [HttpPost]
        public IHttpActionResult ReadSubControl(string Value) => BaseUrl("ReadSubControl", Value);
        #endregion

        #region 分组信息获取
        /// <summary>
        /// 分组信息获取--0x17
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,分组编号,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/SubGroup")]
        [HttpPost]
        public IHttpActionResult SubGroup(string Value) => BaseUrl("SubGroup", Value);
        #endregion

        #region 分组配置
        /// <summary>
        /// 分组配置--0x19
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,分组编号,分组内容，未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/SubGroupConfig")]
        [HttpPost]
        public IHttpActionResult SubGroupConfig(string Value) => BaseUrl("SubGroupConfig", Value);
        #endregion

        #endregion

        #region 安装调试

        #region 系统参数查询
        /// <summary>
        /// 系统参数查询--0x21
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ReadSystrmParam")]
        [HttpPost]
        public IHttpActionResult ReadSystrmParam(string Value) => BaseUrl("ReadSystrmParam", Value);
        #endregion

        #region 节点配置
        /// <summary> 
        /// 节点配置--0x22
        /// </summary>
        /// <param name="Value">
        /// 参数:注册包,分控编号,分组编号,分控类型0：终端 1：路由,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/NodeCofig")]
        [HttpPost]
        public IHttpActionResult NodeCofig(string Value) => BaseUrl("NodeCofig", Value);
        #endregion

        #region 系统参数设置
        /// <summary>
        /// 系统参数设置--0x24
        /// </summary>
        /// <param name="Value">
        /// ----数组大小12----
        /// 参数:注册包,主控频段-网络号,主控频段-频道号[11,26],
        /// 系统容量-灯杆数(最大500),系统容量-分组数量(最大16),
        /// 系统语言（要注明是主机的）0：英文 1：中文,
        /// 按键声音 0：关 1：开,
        /// 6位开机密码(0-9的数字),
        /// 主机网络选择 1:3G  2：网线  3：wifi,
        /// 互感线圈-初级（1-100A),
        /// 互感线圈-次级（1-20mA）,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/SystemParamSet")]
        [HttpPost]
        public IHttpActionResult SystemParamSet(string Value) => BaseUrl("SystemParamSet", Value);
        #endregion

        #region 获取分控UID
        /// <summary>
        /// 获取分控UID--0x28
        /// </summary>
        /// <param name="value">
        /// 参数:注册包,分控编号,查询个数,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/SubUid")]
        [HttpPost]
        public IHttpActionResult SubUid(string value) => BaseUrl("SubUid", value);
        #endregion

        #region 下载分控UID
        /// <summary>
        /// 下载分控UID--0X2A
        /// </summary>
        /// <param name="value">
        /// 参数 注册包, 下载数量[1 - 10], 分控编号组[1 - 500](用‘.'隔开)，分控UID组(用‘.'隔开),未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/LoadSubUid")]
        [HttpPost]
        public IHttpActionResult LoadSubUid(string value) => BaseUrl("LoadSubUid", value);
        #endregion

        #endregion

        #region 系统设置

        #region 时间同步--0x31
        /// <summary>
        /// 时间同步--0x31
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,时间更新模式(0:经纬度计算机时间 1:时区计算时间),时区，纬度_经度,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/SystemTimeSyn")]
        [HttpPost]
        public IHttpActionResult SystemTimeSyn(string Value) => BaseUrl("SystemTimeSyn", Value);
        #endregion

        #region 恢复出厂设置--0x32
        /// <summary>
        /// 恢复出厂设置--0x32
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/SystemReset")]
        [HttpPost]
        public IHttpActionResult SystemReset(string Value) => BaseUrl("SystemReset", Value);
        #endregion

        #region 主机数据清除--0x33
        /// <summary>
        /// 主机数据清除--0x33
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,清除的内容(   解析： 可选需要清除的内容，（共可设8项内容）如：（以下为10进制加法）
        /// 1：代表清除时间策略 2：代表清除光照策略 4代表清除分控信息 8 16 32 64 128（未定）
        /// 发送 3（1+2） 则代表清除前两项
        /// 发送 5（1+4） 则代表清除第一项和第三项
        /// 发送 6（2+4） 7（1+2+4）), 未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ClearHostData")]
        [HttpPost]
        public IHttpActionResult ClearHostData(string Value) => BaseUrl("ClearHostData",Value);
        #endregion

        #region 主机重启--0x34
        /// <summary>
        /// 主机重启--0x34
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/HostRestart")]
        [HttpPost]
        public IHttpActionResult HostRestart(string Value) => BaseUrl("HostRestart",Value);
        #endregion

        #endregion

        #region 网关参数

        #region 设置身份信息--0x41
        /// <summary>
        /// 设置身份信息--0x41
        ///（该指令需要高级权限，不能让用户随便设置）
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,修改后的注册包,修改后的心跳包,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/IdentitySet")]
        [HttpPost]
        public IHttpActionResult IdentitySet(string Value) => BaseUrl("IdentitySet", Value);
        #endregion

        #region 查询登录信息指令--0x42
        /// <summary>
        /// 查询登录信息指令--0x42
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ReadLogInfor")]
        [HttpPost]
        public IHttpActionResult ReadLogInfor(string Value) => BaseUrl("ReadLogInfor", Value);
        #endregion

        #region 设置登录信息--0x43
        /// <summary>
        /// 设置登录信息--0x43
        /// </summary>
        /// <param name="Value">
        /// 参数:注册包,wifi账号 最长20，
        /// 模式 0：WPAPSK 1：WPAPS2K,
        /// 算法 0：AES    1：TKIP,
        /// wifi密码 最长20,
        /// 服务器IP 20位,
        /// 服务器端口10位,
        /// VPN接入  32位,
        /// 机构id
        /// </param>
        /// <returns></returns>
        [Route("Send/LogInforSet")]
        [HttpPost]
        public IHttpActionResult LogInforSet(string Value) => BaseUrl("LogInforSet", Value);
        #endregion

        #region 设置主机的经纬度--0x45
        /// <summary>
        /// 设置主机的经纬度
        /// </summary>
        /// <param name="Value">参数说明 :主机注册包,经度,纬度，未定义参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Send/SetHostLocation")]
        public IHttpActionResult SetHostLocation(string Value) => BaseUrl("SetHostLocation",Value);
        #endregion

        #region 查询主机的经纬度--0x46
        /// <summary>
        /// 查询主机的经纬度--0x46
        /// </summary>
        /// <param name="Value">参数说明 :主机注册包,未定义参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Send/ReadHostLocation")]
        public IHttpActionResult ReadHostLocation(string Value) => BaseUrl("ReadHostLocation", Value);
        #endregion

        #endregion

        #region 策略管理

        #region 查询策略信息
        /// <summary>
        /// 查询策略信息
        /// </summary>
        /// <param name="Value">
        /// 参数：
        /// 注册包,
        /// 策略编号,
        /// 未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ReadStrategy")]
        [HttpPost]
        public IHttpActionResult ReadStrategy(string Value) => BaseUrl("ReadStrategy", Value);
        #endregion

        #region 策略下发
        /// <summary>
        /// <summary>
        /// 策略下发--0x51
        /// </summary>
        /// <param name="Value">
        /// ---数组长度15---
        /// 参数说明：
        /// 注册包,
        /// 策略编号(1-30),
        /// 策略使能(0关1开),
        /// 策略优先级(1-4 优先级从低到高),
        /// 策略周期(1每天 2每周 3每月 4每年),
        /// 起始年(取年后两位, 如2018取18).月(1-12).日(1-31).周(1-7),
        /// 结束年(取年后两位, 如2018取18).月(1-12).日(1-31).周(1-7),
        /// 参考时间(1标准时间 2日出前 3日落后),
        /// 触发时(0-23).分(0-59),
        /// 策略分组Num1.Num2(16个分组，每八个组组成一个二进制数，1为选中，高位在前低位在后),
        /// 调光使能开关(0关1开),
        /// 调光值(0-100),
        /// 回路开关(1开0关),
        /// 回路掩码(八个回路表示一个二进制数，1为选中, 高位在前低位在后)，
        /// 未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/StrategyLoad")]
        [HttpPost]
        public IHttpActionResult StrategyLoad(string Value) => BaseUrl("StrategyLoad", Value);
        #endregion

        #region 光照计划下发指令
        /// <summary>
        /// 光照计划下发指令:-- 0x52
        /// </summary>
        /// <param name="Value">
        /// ---分组大小8---
        /// 参数：
        /// 注册包,
        /// 光照计划使能（01为开，00为关),
        /// 是否群控开关,（01为开，00为关//开启时默认控制全部回路与分组，关闭时控制下面选择的回路与分组）
        /// 执行光照计划的回路,
        /// 光照计划执行时的亮度
        /// 自动控制开关 0：关闭 （计划按照固定的亮度执行）1：开启（计划按照线性补偿执行）
        /// 保留位 （预留当做光照补偿曲线选择位）
        /// 执行光照计划的分组Num1.Num2,
        /// 阈值 上限（16进制）光照度大于该值时关灯,
        /// 阈值 下限（16进制）光照度低于该值时开灯，
        /// 未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/LightPlanLoad")]
        [HttpPost]
        public IHttpActionResult LightPlanLoad(string Value) => BaseUrl("LightPlanLoad", Value);
        #endregion

        #region  读取主机光照计划指令
        /// <summary>
        /// 读取主机光照计划指令:-- 0x53
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ReadLightPlan")]
        [HttpPost]
        public IHttpActionResult ReadLightPlan(string Value) => BaseUrl("ReadLightPlan", Value);
        #endregion

        #region 隧道计划指令下发
        /// <summary>
        /// 隧道光照计划下发指令:-- 57
        /// </summary>
        /// <param name="value">
        /// 参数：用","隔开
        /// 注册包,
        /// 计划编号 0-16；暂时只用第一个,
        /// 光照使能开关，0：关闭  1：开启,
        /// 比例系数，每个分组都设置一个 共16个(用"."隔开,最大100，最小0),
        /// 阈值 上限（16进制）光照度大于该值时关灯,
        /// 阈值 下限（16进制）光照度低于该值时开灯，
        /// 最高亮度,
        /// 最底亮度
        /// 曲线选择0:线性变化—外界越暗，灯具越亮1;线性变化-外界越暗，灯具越暗 （后续可能继续添加）,
        /// 保留位，填写0
        /// </param>
        /// <returns></returns>
        [Route("Send/TunnelPlanLoad")]
        [HttpPost]
        public IHttpActionResult TunnelPlanLoad(string value) => BaseUrl("TunnelPlanLoad", value);
        #endregion

        #region 读取主机隧道光照计划指令
        /// <summary>
        /// 读取主机隧道光照计划指令:-- 58 
        /// </summary>
        /// <param name="value">
        /// 参数：用","隔开
        /// 注册包，
        /// 保留位
        /// </param>
        /// <returns></returns>
        [Route("Send/ReadTunnelPlanLoad")]
        [HttpPost]
        public IHttpActionResult ReadTunnelPlanLoad(string value) => BaseUrl("ReadTunnelPlanLoad", value);
        #endregion

        #region 查询光照度
        /// <summary>
        /// 查询光照度:-- 0x55
        /// </summary>
        /// <param name="Value">
        /// 参数：注册包,未定义参数
        /// </param>
        /// <returns></returns>
        [Route("Send/ReadLightNum")]
        [HttpPost]
        public IHttpActionResult ReadLightNum(string Value) => BaseUrl("ReadLightNum", Value);
        #endregion

        #endregion
    }
}
