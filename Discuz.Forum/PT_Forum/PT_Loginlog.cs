using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public partial class PrivateBT
    {
        /// <summary>
        /// 添加用户登录记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ip"></param>
        /// <param name="type">1.cookie；2.password；3.CNGI</param>
        /// <param name="ok">1.pass；2.fail</param>
        /// <param name="time"></param>
        /// <param name="url"></param>
        /// <param name="agent"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int AddUserLoginRecord(int uid, string ip, int type, int ok, DateTime time, string url, string agent, string msg)
        {
            return DatabaseProvider.GetInstance().AddUserLoginRecord(uid, ip, type, ok, time, url, agent, msg);
        }
        /// <summary>
        /// 添加访问记录
        /// </summary>
        /// <param name="uid">用户ID，无ID记为-1</param>
        /// <param name="username">登录名</param>
        /// <param name="usergroup">用户组</param>
        /// <param name="type">访问方式，Cookie维持1，CNGI维持3，Cookie登陆11，CNGI登陆13，SSO登陆15，密码登陆21，CNGI绑定登陆23，SSO绑定登陆25，后台登陆31，邀请注册登陆41，CNGI注册43，SSO注册45</param>
        /// <param name="result">访问结果，成功1，失败2</param>
        /// <param name="time">时间</param>
        /// <param name="ip">IP地址</param>
        /// <param name="agent">浏览器</param>
        /// <param name="url">访问地址</param>
        /// <param name="md5">用户凭证 截取前5位</param>
        /// <returns></returns>
        public static int AddUserAccessLog(int uid, string username, int usergroup, int type, int result, DateTime date, string ip, string agent,string domain, string url, string md5, string rkey, bool ispost)
        {
            //访问级别，普通用户1，荣誉版主2，实习版主3，版主4，超级版主5，管理员6，后台访问+50，危险访问+100
            int acclevel = 1;
            switch (usergroup)
            {
                case 39:acclevel = 2;break;
                case 37:acclevel = 3;break;
                case 3:acclevel = 4;break;
                case 2:acclevel = 5;break;
                case 1:acclevel = 6;break;
                default:acclevel = 1;break;
            }
            if (ispost) acclevel += 10;

            //下载种子的访问
            if (url.IndexOf("/downloadseed.aspx?seedid=") > -1) acclevel += 10;

            //管理访问，危险访问
            if (url.IndexOf("admin/") > -1) acclevel += 50;
            if (url.IndexOf("'") > -1) acclevel += 100;

            //时间
            TimeSpan time = date.TimeOfDay;

            //IP地址
            string ipheader = "";
            string iptail = "";
            int ipregion = 0;
            if (PTTools.SplitIP(ip, out ipheader, out iptail) > -1)
            {
                ipregion = GetUserIPRegionId(ipheader);
            }
            else
            {
                iptail = ip;
                ipregion = -100;
            }

            //agent
            int agentid = GetUserAgentId(agent);

            //domain域名
            int domainid = GetUserDomainNameId(domain);

            //return 1;
            return DatabaseProvider.GetInstance().InsertUserAccessLog(uid, username, acclevel, result, type, date, time, ipregion, iptail, agentid, domainid, url, md5, rkey);
        }


        /// <summary>
        /// 获取访问级别说明字符串
        /// </summary>
        /// <param name="acclevel"></param>
        /// <returns></returns>
        public static string GetUserAccessLogLevel(int acclevel)
        {
            string le = "";
            if (acclevel > 100)
            {
                le += "#危险#";
                acclevel -= 100;
            }
            if (acclevel > 50)
            {
                le += "*后台*";
                acclevel -= 50;
            }
            switch(acclevel)
            {
                case 1: return "普通用户" + le;
                case 2: return "荣誉版主" + le;
                case 3: return "实习版主" + le;
                case 4: return "版主" + le;
                case 5: return "超级版主" + le;
                case 6: return "管理员" + le;
                default: return "未知用户" + le;
            }
        }
        public static string GetUserAccessLogType(int acctype)
        {
            string le = "";
            if (acctype % 2 == 0)
            {
                le += " SSL ";
                acctype -= 1;
            }
            switch (acctype)
            {
                case 1: return le + "Cookie维持 访问";
                case 3: return le + "CNGI维持 访问";
                case 11: return le + "Cookie登陆 访问";
                case 13: return le + "CNGI登陆 访问";
                case 15: return le + "SSO登陆 访问";
                case 21: return le + "密码登陆 访问";
                case 23: return le + "CNGI绑定登陆 访问";
                case 25: return le + "SSO绑定登陆 访问";
                case 31: return le + "后台登陆 访问";
                case 41: return le + "邀请注册登陆 访问";
                case 43: return le + "CNGI注册 访问";
                case 45: return le + "SSO注册 访问";
                default: return le + "未知 访问";
            }
        }
        public static string CleanedLog = "";
        public static DateTime CleanStart = new DateTime(1990, 4, 4);
        public static void ResetCleanStartTime()
        {
            CleanStart = DateTime.Now;
        }

        /// <summary>
        /// 清理用户访问记录
        /// </summary>
        public static void DoAccessLogCleanTaskEvent()
        {
            PrivateBT.ResetCleanStartTime();
            PrivateBT.CleanUserAccessLogScheuleTask(1, 1, 202, 205, 5000);
            PrivateBT.CleanUserAccessLogScheuleTask(1, 1, 4, 1, 5000);
            PrivateBT.CleanUserAccessLogScheuleTask(1, 1, 3, 1, 5000);
            PrivateBT.CleanUserAccessLogScheuleTask(1, 1, 2, 1, 5000);
            PrivateBT.CleanUserAccessLogScheuleTask(1, 1, 1, 1, 5000);
        }

        /// <summary>
        /// 计划任务调用函数，清除用户访问记录，在程序执行期内（进程回收之前）若执行清除数量为0，则不再清除。
        /// </summary>
        /// <param name="mounth">清除几个月之前的访问记录，最低为3</param>
        /// <param name="acclevel">访问级别，普通用户1，荣誉版主2，实习版主3，版主4，超级版主5，管理员6，后台访问+50，危险访问+100</param>
        /// <param name="acctype">访问方式，Cookie维持1，CNGI维持3，Cookie自维持5，CNGI自维持7，Cookie登陆11，CNGI登陆13，SSO登陆15，密码登陆21，CNGI绑定登陆23，SSO绑定登陆25，后台登陆31，邀请注册登陆41，CNGI注册43，SSO注册45</param>
        /// <param name="accresult">访问结果，成功1，失败2</param>
        /// <param name="count">每次清楚的数量，最多为5000</param>
        /// <returns></returns>
        public static int CleanUserAccessLogScheuleTask(int mounth, int acclevel, int acctype, int accresult, int count)
        {
            if (mounth < 1) mounth = 1;
            if (count > 5000) count = 5000;

            string logid = string.Format("DEL_{0}_{1}_{2}_S", acclevel, acctype, accresult);
            string recordstr = "删除" + mounth + "月前的 " + GetUserAccessLogLevel(acclevel) + GetUserAccessLogType(acctype) + ((accresult == 1) ? " 成功 " : " 失败 ") + "记录"; 

            //删除过期用户访问记录，仅在夜间操作
            if (DateTime.Now.Hour > 0 && DateTime.Now.Hour < 7 && CleanedLog.IndexOf(logid) < 0)
            {
                DateTime sttime = DateTime.Now;
                int rst = 0, rstadd = 1, ccount = 0;
                for (int i = 0; i < 1200 && rstadd > 0; i++)
                {
                    try
                    {
                        rstadd = PrivateBT.CleanUserAccessLog(mounth, acclevel, acctype, accresult, count);
                    }
                    catch (System.Exception ex)
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Exception, logid, recordstr + "异常 [已删除：" + rst + "条，耗时：" + ((DateTime.Now - sttime).TotalSeconds - 0.1 * ccount).ToString("0.00") + "秒]：" + ex.ToString());
                        rstadd = 1;
                        continue;
                    }
                    rst += rstadd;
                    ccount++;
                    System.Threading.Thread.Sleep(100);

                    if (rstadd < 0)
                    {
                        //CleanedLog += logid;
                        PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Normal, logid, recordstr + "清理遇到问题，代码" + rstadd);
                    }
                    else if (rstadd == 0)
                    {
                        //再次删除尝试确认，是否已经清楚完毕
                        try
                        {
                            rstadd = PrivateBT.CleanUserAccessLog(mounth, acclevel, acctype, accresult, count);
                        }
                        catch (System.Exception ex)
                        {
                            PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Exception, logid, recordstr + "异常 [已删除：" + rst + "条，耗时：" + ((DateTime.Now - sttime).TotalSeconds - 0.1 * ccount).ToString("0.00") + "秒]：" + ex.ToString());
                            rstadd = 1;
                            continue;
                        }
                        if (rstadd == 0)
                        {
                            CleanedLog += logid;
                            PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Normal, logid, recordstr + "已经清理完毕");
                        }
                    }
                    if (i % 60 == 0)
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Normal, logid, "[进行中] " + recordstr + rst + " 条，耗时" + ((DateTime.Now - sttime).TotalSeconds - 0.1 * ccount).ToString("0.00") + "秒");
                    }
                    if ((DateTime.Now - CleanStart).TotalSeconds > 280) break;
                }
                PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Normal, logid, recordstr + rst + " 条，耗时" + ((DateTime.Now - sttime).TotalSeconds - 0.1 * ccount).ToString("0.00") + "秒");
            }
            return 0;
        }


        /// <summary>
        /// 删除指定条件的用户访问记录
        /// </summary>
        /// <param name="mounth"></param>
        /// <param name="acclevel"></param>
        /// <param name="acctype"></param>
        /// <param name="accresult"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int CleanUserAccessLog(int mounth, int acclevel, int acctype, int accresult, int count)
        {
            if (mounth < 1) return -2;
            if (acclevel < 1) return -3;
            if (acctype < 1) return -4;
            if (accresult < 1) return -5;

            if (count > 30000) count = 30000;

            return DatabaseProvider.GetInstance().DeleteUserAccessLog(DateTime.Now.AddMonths(-mounth), acclevel, acctype, accresult, count);
        }



        private static object SynObjectDomainName = new object();
        /// <summary>
        /// 获取用户DomainName的记录ID
        /// </summary>
        /// <param name="ipheader"></param>
        public static int GetUserDomainNameId(string DomainName)
        {
            int DomainNameId = DatabaseProvider.GetInstance().GetUserDomainId(DomainName);
            if (DomainNameId < 1)
            {
                try
                {
                    lock (SynObjectDomainName)
                    {
                        DomainNameId = DatabaseProvider.GetInstance().GetUserDomainId(DomainName);
                        if (DomainNameId < 1) DomainNameId = DatabaseProvider.GetInstance().InsertUserDomainName(DomainName);

                        if (DomainNameId > 1) return DomainNameId;
                    }
                    PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Error, "DomainInsert", "插入DomainName失败1：" + DomainName);
                    return -105;
                }
                catch(Exception ex)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Exception, "DomainInsert", string.Format("插入DomainName异常：{0}-{1}", DomainName, ex));
                    System.Threading.Thread.Sleep(100);
                    DomainNameId = DatabaseProvider.GetInstance().GetUserDomainId(DomainName);
                    if (DomainNameId < 1) DomainNameId = DatabaseProvider.GetInstance().InsertUserDomainName(DomainName);

                    if (DomainNameId > 1) return DomainNameId;
                    else
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Error, "DomainInsert", "插入DomainName失败2：" + DomainName);
                        return -106;
                    }
                }
            }
            else
                return DomainNameId;
        }


        private static object SynObjectUserAgent = new object();
        /// <summary>
        /// 获取用户UserAgent的记录ID
        /// </summary>
        /// <param name="ipheader"></param>
        public static int GetUserAgentId(string UserAgent)
        {
            if (UserAgent == null) return -998;
            if (UserAgent.Trim() == "") return -999;

            int UserAgentId = DatabaseProvider.GetInstance().GetUserAgentId(UserAgent);
            if (UserAgentId < 1)
            {
                lock (SynObjectUserAgent)
                {
                    UserAgentId = DatabaseProvider.GetInstance().GetUserAgentId(UserAgent);
                    if (UserAgentId < 1)
                    {
                        try
                        {
                            UserAgentId = DatabaseProvider.GetInstance().InsertUserAgent(UserAgent);
                        }
                        catch (System.Exception ex)
                        {
                            ex.ToString();
                            UserAgentId = DatabaseProvider.GetInstance().GetUserAgentId(UserAgent);
                            if (UserAgentId < 1)
                            {
                                UserAgentId = -102;
                            }
                        }
                    }
                }

                if (UserAgentId > 1) return UserAgentId;
                PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Error, "UserAgentInsert", UserAgentId + "插入UserAgent失败" + UserAgent);
                return -101;           
            }
            else
                return UserAgentId;
        }


        private static object SynObjectIPRegion = new object();
        /// <summary>
        /// 获取用户IP的记录ID
        /// </summary>
        /// <param name="ipheader"></param>
        public static int GetUserIPRegionId(string ipheader)
        {
            int ipregion = DatabaseProvider.GetInstance().GetUserIPRegionId(ipheader);
            if (ipregion < 1)
            {
                try
                {
                    lock (SynObjectIPRegion)
                    {
                        ipregion = DatabaseProvider.GetInstance().GetUserIPRegionId(ipheader);
                        if (ipregion < 1) ipregion = DatabaseProvider.GetInstance().InsertUserIPRegion(ipheader);

                        if (ipregion > 1) return ipregion;
                    }
                    PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Error, "IPRegionInsert", "插入IP失败1：" + ipheader);
                    return -103;
                }
                catch
                {
                    System.Threading.Thread.Sleep(1000);
                    ipregion = DatabaseProvider.GetInstance().GetUserIPRegionId(ipheader);

                    if (ipregion > 1) return ipregion;
                    else
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.UserAccessLog, PTLog.LogStatus.Error, "IPRegionInsert", "插入IP失败2：" + ipheader);
                        return -104;
                    }
                }
            }
            else
                return ipregion;
        }

    }
}