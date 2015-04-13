using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{

    public class PTLog
    {
        private class PTLDescription : Attribute
        {
            public string Text;
            public PTLDescription(string text) { Text = text; }
        }

        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memberInfo = type.GetMember(en.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(PTLDescription), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((PTLDescription)attrs[0]).Text;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return en.ToString();

        }

        public enum LogType
        {
            //
            [PTLDescription("数据层 DataHelper")]
            DataHelper = 0,

            //
            [PTLDescription("用户访问记录 UserAccessLog")]
            UserAccessLog = 1,
            [PTLDescription("计划任务_种子 SeedTaskEvent")]
            SeedTaskEvent = 2,
            [PTLDescription("计划任务_Abt种子 AbtSeedTaskEvent")]
            AbtSeedTaskEvent = 3,
            [PTLDescription("用户密码 UserPassword")]
            UserPassword = 4,
            [PTLDescription("计划任务_节点 PeerTaskEvent")]
            PeerTaskEvent = 5,
            [PTLDescription("计划任务_统计 StatisticsEvent")]
            StatisticsEvent = 6,
            [PTLDescription("计划任务_十大 TopTenEvent")]
            TopTenEvent = 7,
            [PTLDescription("计划任务_用户 UserEvent")]
            UserEvent = 8,
            [PTLDescription("计划任务_邀请 InviteEvent")]
            InviteEvent = 9,
            [PTLDescription("种子处理 SeedProcess")]
            SeedProcess = 10,

            //
            [PTLDescription("基础页面 PageBaseLog")]
            PageBaseLog = 11,
            [PTLDescription("HTTP请求 HttpReq")]
            HttpReq = 12,
            [PTLDescription("Tracker判断IP地址 PeerInvidIP")]
            PeerInvidIP = 13,
            [PTLDescription("Tracker判断IP地址 PeerOutIPv4")]
            PeerOutIPv4 = 14,
            [PTLDescription("发布种子 SeedPublish")]
            SeedPublish = 15,
            [PTLDescription("Tracker地址限制 PeerIPLimit")]
            PeerIPLimit = 16,
            [PTLDescription("地址格式 IPFormat")]
            IPFormat = 17,
            [PTLDescription("基础页面验证 PageBaseValidate")]
            PageBaseValidate = 18,
            [PTLDescription("动态页面 SessionAJAXPage")]
            SessionAJAXPage = 19,
            [PTLDescription("基础页面初始化 PageBaseOnInit")]
            PageBaseOnInit = 20,

            //
            [PTLDescription("禁封用户_2周 BanUser2Week")]
            BanUser2Week = 21,
            [PTLDescription("禁封用户_2月 BanUser2Month")]
            BanUser2Month = 22,
            [PTLDescription("通知_重定向 NoticeTransfer")]
            NoticeTransfer = 23,
            [PTLDescription("在线用户 OnlineUser")]
            OnlineUser = 24,
            [PTLDescription("计划任务 ScheduledEvent")]
            ScheduledEvent = 25,
            [PTLDescription("Tracker计划任务 ScheduledEventTracker")]
            ScheduledEventTracker = 26,
            [PTLDescription("Web计划任务 ScheduledEventWeb")]
            ScheduledEventWeb = 27,
            [PTLDescription("午夜任务 MoonEvent")]
            MoonEvent = 28,


            //
            [PTLDescription("新增Abt种子 AbtSeedInsert")]
            AbtSeedInsert = 41,

            //
            [PTLDescription("举报格式 ReportFormat")]
            ReportFormat = 51,

            //
            [PTLDescription("复活用户 ReActiveUser")]
            ReActiveUser = 61,

            //
            [PTLDescription("Tracker新增节点 TrackerInsertPeer")]
            TrackerInsertPeer = 70,
            [PTLDescription("Tracker等待执行 TrackerWaitUS")]
            TrackerWaitUS = 71,
            [PTLDescription("Tracker更新流量 TrackerUpdateTraffic")]
            TrackerUpdateTraffic = 72,

            //
            [PTLDescription("删除节点 PeerDelete")]
            PeerDelete = 81,

            //
            [PTLDescription("首页 ForumIndex")]
            ForumIndex = 91,
            [PTLDescription("搜索 SearchPage")]
            SearchPage = 92,
            [PTLDescription("帖子 PostMessage")]
            PostMessage = 93,

            //
            [PTLDescription("种子文件 TorrentFile")]
            TorrentFile = 101,

            //
            [PTLDescription("论坛聚合 Aggregation")]
            Aggregation = 111,
            [PTLDescription("热门新种 HotNewSeed")]
            HotNewSeed = 112,

            //
            [PTLDescription("RSS发送文本 RssSendText")]
            RssSendText = 121,
            [PTLDescription("RSS发送空RSS RssSendRss")]
            RssSendRss = 122,
            [PTLDescription("RSS种子发送文本 RssDSeedSendText")]
            RssDSeedSendText = 123,
            [PTLDescription("RSS种子发送种子 RssDSeedSendTorrent")]
            RssDSeedSendTorrent = 124,

            //
            [PTLDescription("调试测试 DebugTEST")]
            DebugTEST = 240,
            [PTLDescription("Tracker调试 TrackerDebug")]
            TrackerDebug = 241,

                        //
            [PTLDescription("系统日志 SystemLog")]
            SystemLog = 254,
            //
            [PTLDescription("管理日志 AdminLog")]
            AdminLog = 255
        };
        public enum LogStatus
        {
            [PTLDescription("<span style=\"color:#666;font-weight:bold\">详细</span>")]
            Detail = 0,
            [PTLDescription("<span style=\"color:#060;font-weight:bold\">正常</span>")]
            Normal = 1,
            [PTLDescription("<span style=\"color:#960;font-weight:bold\">警告</span>")]
            Warning = 51,
            [PTLDescription("<span style=\"color:#C00;font-weight:bold\">错误</span>")]
            Error = 101,
            [PTLDescription("<span style=\"color:#F00;font-weight:bold\">严重错误</span>")]
            Critical = 151,
            [PTLDescription("<span style=\"color:#F09;font-weight:bold\">致命异常</span>")]
            Exception = 201,
        };

        public enum BuaaSSOLogType
        {
            SSOLogin = 1,
            SSORegister = 2,
            SSOToken = 3,
            SSOLink = 100,
        }

        public enum CNGILogType
        {
            CNGILogin = 1,
            CNGIRegister = 2,
            CNGILink = 100,
        }

        public static int CleanSystemLog()
        {
            try
            {
                int c = 0;

                DateTime st = DateTime.Now;
                c = DatabaseProvider.GetInstance().ClearnSystemLog();
                InsertSystemLog(LogType.SystemLog, LogStatus.Normal, "CleanSystemLog", string.Format("清理SystemLog：{0} 耗时：{1}", c, (DateTime.Now - st).TotalSeconds));

                st = DateTime.Now;
                c = DatabaseProvider.GetInstance().ClearnSystemDebugLog();
                InsertSystemLog(LogType.SystemLog, LogStatus.Normal, "CleanSystemLog", string.Format("清理SystemDebugLog：{0} 耗时：{1}", c, (DateTime.Now - st).TotalSeconds));

                return c;
            }
            catch (System.Exception ex)
            {
                InsertSystemLog(LogType.SystemLog, LogStatus.Exception, "CleanSystemLog", "清理SystemLog异常：" + ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public static int InsertBuaaSSOLog(int ssouid, BuaaSSOLogType logtype, LogStatus logstatus, string logaction, string message)
        {
            return DatabaseProvider.GetInstance().InsertBuaaSSOLog(ssouid, (short)logtype, (short)logstatus, logaction, Utils.HtmlEncode(message));
        }
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <param name="errortypeid"></param>
        /// <param name="errortype"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int InsertCNGILog(string cngischool, string cnginame, CNGILogType logtype, LogStatus logstatus, string logaction, string message)
        {
            return DatabaseProvider.GetInstance().InsertCNGILog(cngischool, cnginame, (short)logtype, (short)logstatus, logaction, Utils.HtmlEncode(message));
        }

        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public static int InsertSystemLog_WithPageIP(LogType logtype, LogStatus logstatus, string logaction, string message)
        {
            return InsertSystemLog_WithPageIP(logtype, logstatus, logaction, message, false);
        }
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public static int InsertSystemLog_WithPageIP(LogType logtype, LogStatus logstatus, string logaction, string message, bool e)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string page = System.Web.HttpContext.Current.Request.RawUrl;
            string messagenew = string.Format(e ? "{2} -IP:{0} -PAGE:{1}" : "IP:{0} -PAGE:{1} -MESSAGE:{2}", ip, page, message);
            return DatabaseProvider.GetInstance().InsertSystemLog((short)logtype, (short)logstatus, logaction, Utils.HtmlEncode(messagenew));
        }
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public static int InsertSystemLog(LogType logtype, LogStatus logstatus, string logaction, string message)
        {
            return DatabaseProvider.GetInstance().InsertSystemLog((short)logtype, (short)logstatus, logaction, Utils.HtmlEncode(message));
        }

                /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public static int InsertSystemLogDebug_WithPageIP(LogType logtype, LogStatus logstatus, string logaction, string message, bool e)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string page = System.Web.HttpContext.Current.Request.RawUrl;
            string messagenew = string.Format(e ? "{2} -IP:{0} -PAGE:{1}" : "IP:{0} -PAGE:{1} -MESSAGE:{2}", ip, page, message);
            return DatabaseProvider.GetInstance().InsertSystemLogDebug((short)logtype, (short)logstatus, logaction, Utils.HtmlEncode(messagenew));
        }
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public static int InsertSystemLogDebug_WithPageIP(LogType logtype, LogStatus logstatus, string logaction, string message)
        {
            return InsertSystemLogDebug_WithPageIP(logtype, logstatus, logaction, message, false);
        }
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public static int InsertSystemLogDebug(LogType logtype, LogStatus logstatus, string logaction, string message)
        {
            return DatabaseProvider.GetInstance().InsertSystemLogDebug((short)logtype, (short)logstatus, logaction, Utils.HtmlEncode(message));
        }
        
        
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public static List<PTLogMessage> GetSysLogTable(int pageindex, int numperpage, int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            List<PTLogMessage> logmessagelist = new List<PTLogMessage>();
            DataTable dt = DatabaseProvider.GetInstance().GetSysLogTable(pageindex, numperpage, logtype, logstatus, logaction, stabove, timelimit, start, end);
            foreach (DataRow dr in dt.Rows)
            {
                PTLogMessage logmessage = new PTLogMessage();
                logmessage.Id = TypeConverter.ObjectToInt(dr["id"]);
                logmessage.Logtime = TypeConverter.ObjectToDateTime(dr["time"]);

                LogType t = (LogType)TypeConverter.ObjectToInt(dr["logtype"]);
                LogStatus s = (LogStatus)TypeConverter.ObjectToInt(dr["logstatus"]);

                logmessage.LogData1 = PTLog.GetDescription(t);
                logmessage.LogData2 = PTLog.GetDescription(s);
                logmessage.LogData3 = dr["logaction"].ToString().Trim();
                logmessage.LogMessage = dr["message"].ToString().Trim();

                if (logmessage.Id > 0) logmessagelist.Add(logmessage);
            }
            dt.Clear();
            dt.Dispose();
            return logmessagelist;
        }
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public static int GetSysLogCount(int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            return DatabaseProvider.GetInstance().GetSysLogCount(logtype, logstatus, logaction, stabove, timelimit, start, end);
        }
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public static List<PTLogMessage> GetSysDebugLogTable(int pageindex, int numperpage, int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            List<PTLogMessage> logmessagelist = new List<PTLogMessage>();
            DataTable dt = DatabaseProvider.GetInstance().GetSysDebugLogTable(pageindex, numperpage, logtype, logstatus, logaction, stabove, timelimit, start, end);
            foreach (DataRow dr in dt.Rows)
            {
                PTLogMessage logmessage = new PTLogMessage();
                logmessage.Id = TypeConverter.ObjectToInt(dr["id"]);
                logmessage.Logtime = TypeConverter.ObjectToDateTime(dr["time"]);

                LogType t = (LogType)TypeConverter.ObjectToInt(dr["logtype"]);
                LogStatus s = (LogStatus)TypeConverter.ObjectToInt(dr["logstatus"]);

                logmessage.LogData1 = PTLog.GetDescription(t);
                logmessage.LogData2 = PTLog.GetDescription(s);
                logmessage.LogData3 = dr["logaction"].ToString().Trim();
                logmessage.LogMessage = dr["message"].ToString().Trim();

                if (logmessage.Id > 0) logmessagelist.Add(logmessage);
            }
            dt.Clear();
            dt.Dispose();
            return logmessagelist;
        }
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public static int GetSysDebugLogCount(int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            return DatabaseProvider.GetInstance().GetSysDebugLogCount(logtype, logstatus, logaction, stabove, timelimit, start, end);
        }

    }
}