﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shine.Comman.AppSetting
{
    /// <summary>
    /// 配置文件获取或设置
    /// </summary>
    public static class WebConfigs
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(this string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }
        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(this string key, string value)
        {
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/Configs/system.config"));
            System.Xml.XmlNode xNode;
            System.Xml.XmlElement xElem1;
            System.Xml.XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");

            xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", value);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(HttpContext.Current.Server.MapPath("~/Configs/system.config"));
        }
    }
}
