using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Data;

namespace Discuz.Common
{
    /// <summary>
    /// BT工具类：此工具类不能依赖于数据库、专有变量类型等存在
    /// </summary>
    public class QHtml
    {

        public static string HR_SendPM(int userid)
        {
            return string.Format(" href=\"usercppostpm.aspx?msgtoid={0}\" onclick=\"showWindow('postpm', this.href, 'get',0);doane(event);\"", userid);
        }

        public static string HR_ShowTopic(int topicid)
        {
            return HR_ShowTopic(topicid, true);
        }

        public static string HR_ShowTopic(int topicid, bool newpage)
        {
            if(newpage)
                return string.Format(" href=\"showtopic-{0}.aspx\" target=\"_blank\"", topicid);
            else
                return string.Format(" href=\"showtopic-{0}.aspx\"", topicid);
        }

        public static string HR_DownloadSeed(int seedid)
        {
            return string.Format(" href=\"downloadseed.aspx?seedid={0}\"", seedid);
        }


        public static string HR_ShowUser(int userid)
        {
            return HR_ShowUser(userid, true);
        }

        public static string HR_ShowUser(int userid, bool newpage)
        {
            if (newpage)
                return string.Format(" href=\"userinfo-{0}.aspx\" target=\"_blank\"", userid);
            else
                return string.Format(" href=\"userinfo-{0}.aspx\"", userid);
        }


        public static string TS_RedBold { get { return TxtStyle("Red", "Bold"); } }
        public static string TS_BlueBold { get { return TxtStyle("Blue", "Bold"); } }
        public static string TS_GrayBold { get { return TxtStyle("Gray", "Bold"); } }
        public static string TS_PurpleBold { get { return TxtStyle("Purple", "Bold"); } }
        public static string TS_GreenBold { get { return TxtStyle("Green", "Bold"); } }

        public static string TxtStyle(string color, string weight)
        {
            return string.Format(" style=\"{0}{1}\"",
                color == "" ? "" : "color:" + color + ";",
                weight == "" ? "" : "font-weight:" + weight + ";"
                );
        }
    }
}
