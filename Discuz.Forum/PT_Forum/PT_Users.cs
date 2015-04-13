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
    public class PTUsers
    {
        public static int CreateUserReActiveVFCode(int uid)
        {
            string vfcode = PTTools.GetRandomString(20);

            System.Web.HttpContext.Current.Response.Cookies["ra_vfcode"].Value = vfcode;
            System.Web.HttpContext.Current.Response.Cookies["ra_vfcode"].Expires = DateTime.MaxValue;
            System.Web.HttpContext.Current.Response.Cookies["ra_vfcode"].HttpOnly = true;

            return DatabaseProvider.GetInstance().CreateUserReActiveVFCode(uid, vfcode);
        }

        public static int VerifyUserReActiveVFCode(int uid, int invitecodeid)
        {
            string vfcode = System.Web.HttpContext.Current.Request.Cookies["ra_vfcode"].Value;

            if (vfcode == null || vfcode.Length != 20) return -2;
            else if (invitecodeid < 0) return -3;
            else return DatabaseProvider.GetInstance().VerifyUserReActiveVFCode(uid, vfcode, invitecodeid);
        }
        public static int ReActiveUser(int uid)
        {
            return DatabaseProvider.GetInstance().ReActiveUser(uid);
        }

        /// <summary>
        /// 执行签到
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="lastcheckincount"></param>
        /// <param name="lastcheckintime"></param>
        /// <returns></returns>
        public static int DoCheckIn(int uid, int lastcheckincount, DateTime lastcheckintime, int newcheckincount, DateTime newcheckintime)
        {
            return DatabaseProvider.GetInstance().DoCheckIn(uid, lastcheckincount, lastcheckintime, newcheckincount, newcheckintime);
        }
        
        /// <summary>
        /// 添加签到日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int AddCheckInLog(int uid, DateTime date, DateTime time, int count)
        {
            try
            {
                return DatabaseProvider.GetInstance().AddCheckInLog(uid, date, time, count);
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.Aggregation, PTLog.LogStatus.Exception, "CheckIn", string.Format("异常：UID:{0} -EX:{1}", uid, ex));
                return -1;
            }
            
        }
                /// <summary>
        /// 添加签到日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int GetCheckInRank(int uid, DateTime date, DateTime time, int count)
        {
            return DatabaseProvider.GetInstance().GetCheckInRank(uid, date, time, count);
        }

                /// <summary>
        /// 获得今天签到次序
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int GetCheckInRank(int uid)
        {
            return DatabaseProvider.GetInstance().GetCheckInRank(uid);
        }


        /// <summary>
        /// 更新用户的RatioProtection
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratioprotection"></param>
        /// <returns></returns>
        public static int SetUserRatioProtection(int uid, int ratioprotection)
        {
            if (uid < 1 || ratioprotection < 0) return -1;
            return DatabaseProvider.GetInstance().SetUserRatioProtection(uid, ratioprotection);
        }

        /// <summary>
        /// 检查该Passkey是否存在，返回该Passkey存在数量
        /// </summary>
        /// <param name="passkey"></param>
        /// <returns></returns>
        public static int CheckPasskey(string passkey)
        {
            return DatabaseProvider.GetInstance().CheckPasskey(passkey);
        }
        /// <summary>
        /// 给指定uid的用户重置passkey，返回1为成功，0为失败
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static int ResetPasskey(int uid)
        {
            for (int i = 0; i < 5; i++)
            {
                string newpasskey = PTTools.GetRandomString(32);
                if (CheckPasskey(newpasskey) < 1)
                {
                    DatabaseProvider.GetInstance().UpdatePasskey(newpasskey, uid);
                    return 1;
                }
            }
            return 0;
        }
        /// <summary>
        /// 返回指定用户的简短信息
        /// 用于基础页面
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public static PTUserInfo GetBtUserInfoForPagebase(int uid)
        {
            PTUserInfo userinfo = new PTUserInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetPTUserInfoToReaderForPagebase(uid);

            if (reader.Read())
            {
                userinfo.Vip = Int32.Parse(reader["vip"].ToString());

                userinfo.Ratio = (float)(Math.Floor(TypeConverter.ObjectToFloat(reader["ratio"].ToString()) * 100.0) / 100.0); //共享率向下取整到0.00位
                
                userinfo.RatioProtection = TypeConverter.StrToInt(reader["ratioprotection"].ToString());

                userinfo.UploadCount = TypeConverter.ObjectToInt(reader["uploadcount"]);
                userinfo.DownloadCount = TypeConverter.ObjectToInt(reader["downloadcount"]);
                userinfo.FinishCount = TypeConverter.ObjectToInt(reader["finishcount"]);
                userinfo.PublishCount = TypeConverter.ObjectToInt(reader["publishcount"]);

                userinfo.Credits = TypeConverter.ObjectToDecimal(reader["credits"]);
                userinfo.Extcredits1 = TypeConverter.ObjectToDecimal(reader["extcredits1"]);
                userinfo.Extcredits2 = TypeConverter.ObjectToDecimal(reader["extcredits2"]);
                userinfo.Extcredits3 = TypeConverter.ObjectToDecimal(reader["extcredits3"]);
                userinfo.Extcredits4 = TypeConverter.ObjectToDecimal(reader["extcredits4"]);
                userinfo.Extcredits5 = TypeConverter.ObjectToDecimal(reader["extcredits5"]);
                userinfo.Extcredits6 = TypeConverter.ObjectToDecimal(reader["extcredits6"]);
                userinfo.Extcredits7 = TypeConverter.ObjectToDecimal(reader["extcredits7"]);
                userinfo.Extcredits8 = TypeConverter.ObjectToDecimal(reader["extcredits8"]);
                userinfo.Extcredits9 = TypeConverter.ObjectToDecimal(reader["extcredits9"]);
                userinfo.Extcredits10 = TypeConverter.ObjectToDecimal(reader["extcredits10"]);
                userinfo.Extcredits11 = TypeConverter.ObjectToDecimal(reader["extcredits11"]);
                userinfo.Extcredits12 = TypeConverter.ObjectToDecimal(reader["extcredits12"]);

                userinfo.Joindate = reader["joindate"].ToString();

                userinfo.Uid = uid;
            }
            reader.Close();
            reader.Dispose();


            return userinfo;
        }
        /// <summary>
        /// 返回指定用户的简短信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public static PTUserInfo GetBtUserInfo(int uid)
        {
            PTUserInfo userinfo = new PTUserInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetPTUserInfoToReader(uid);

            if (reader.Read())
            {
                userinfo.Vip = Int32.Parse(reader["vip"].ToString());
                userinfo.Ratio = float.Parse(reader["ratio"].ToString());
                userinfo.Passkey = reader["passkey"].ToString().Trim();
                userinfo.Groupid = Int16.Parse(reader["groupid"].ToString());

                userinfo.UploadCount = Int32.Parse(reader["uploadcount"].ToString());
                userinfo.DownloadCount = Int32.Parse(reader["downloadcount"].ToString());
                userinfo.FinishCount = Int32.Parse(reader["finishcount"].ToString());
                userinfo.PublishCount = Int32.Parse(reader["publishcount"].ToString());

                userinfo.Credits = decimal.Parse(reader["credits"].ToString());
                userinfo.Extcredits1 = decimal.Parse(reader["extcredits1"].ToString());
                userinfo.Extcredits2 = decimal.Parse(reader["extcredits2"].ToString());
                userinfo.Extcredits3 = decimal.Parse(reader["extcredits3"].ToString());
                userinfo.Extcredits4 = decimal.Parse(reader["extcredits4"].ToString());
                userinfo.Extcredits5 = decimal.Parse(reader["extcredits5"].ToString());
                userinfo.Extcredits6 = decimal.Parse(reader["extcredits6"].ToString());
                userinfo.Extcredits7 = decimal.Parse(reader["extcredits7"].ToString());
                userinfo.Extcredits8 = decimal.Parse(reader["extcredits8"].ToString());
                userinfo.Extcredits9 = decimal.Parse(reader["extcredits9"].ToString());
                userinfo.Extcredits10 = decimal.Parse(reader["extcredits10"].ToString());

                userinfo.Joindate = reader["joindate"].ToString();

                userinfo.Uid = uid;
            }
            reader.Close();
            reader.Dispose();
            return userinfo;
        }
        ///// <summary>
        ///// 返回指定用户的简短信息
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <returns>用户信息</returns>
        //public static PTUserInfo GetBtUserInfo(string passkey)
        //{
        //    PTUserInfo userinfo = new PTUserInfo();

        //    IDataReader reader = DatabaseProvider.GetInstance().GetPTUserInfoToReader(passkey);

        //    if (reader.Read())
        //    {
        //        userinfo.Vip = Int32.Parse(reader["vip"].ToString());
        //        userinfo.Ratio = float.Parse(reader["ratio"].ToString());
        //        userinfo.Passkey = passkey;
        //        userinfo.Groupid = Int16.Parse(reader["groupid"].ToString());

        //        userinfo.UploadCount = Int32.Parse(reader["uploadcount"].ToString());
        //        userinfo.DownloadCount = Int32.Parse(reader["downloadcount"].ToString());
        //        userinfo.FinishCount = Int32.Parse(reader["finishcount"].ToString());
        //        userinfo.PublishCount = Int32.Parse(reader["publishcount"].ToString());

        //        userinfo.Credits = int.Parse(reader["credits"].ToString());
        //        userinfo.Extcredits1 = decimal.Parse(reader["extcredits1"].ToString());
        //        userinfo.Extcredits2 = decimal.Parse(reader["extcredits2"].ToString());
        //        userinfo.Extcredits3 = decimal.Parse(reader["extcredits3"].ToString());
        //        userinfo.Extcredits4 = decimal.Parse(reader["extcredits4"].ToString());
        //        userinfo.Extcredits5 = decimal.Parse(reader["extcredits5"].ToString());
        //        userinfo.Extcredits6 = decimal.Parse(reader["extcredits6"].ToString());
        //        userinfo.Extcredits7 = decimal.Parse(reader["extcredits7"].ToString());
        //        userinfo.Extcredits8 = decimal.Parse(reader["extcredits8"].ToString());
        //        userinfo.Extcredits9 = decimal.Parse(reader["extcredits9"].ToString());
        //        userinfo.Extcredits10 = decimal.Parse(reader["extcredits10"].ToString());

        //        userinfo.Uid = Int32.Parse(reader["uid"].ToString());
        //    }
        //    reader.Close();
        //    reader.Dispose();
        //    return userinfo;
        //}
        /// <summary>
        /// 返回指定用户的简短信息，只包含以下信息uid,vip,ext3,ext4,groupid,ratioprotection,以及查询用到的passkey
        /// </summary>
        /// <param name="passkey">用户passkey</param>
        /// <returns>用户信息</returns>
        public static PTUserInfo GetBtUserInfoForTracker(string passkey)
        {
            PTUserInfo userinfo = new PTUserInfo();

            IDataReader reader = DatabaseProvider.GetInstance().GetPTUserInfoToReaderForTracker(passkey);

            if (reader.Read())
            {
                userinfo.Uid = Int32.Parse(reader["uid"].ToString());
                userinfo.Vip = Int32.Parse(reader["vip"].ToString());

                userinfo.UploadCount = Int32.Parse(reader["uploadcount"].ToString());
                userinfo.DownloadCount = Int32.Parse(reader["downloadcount"].ToString());

                userinfo.Extcredits3 = decimal.Parse(reader["extcredits3"].ToString());
                userinfo.Extcredits4 = decimal.Parse(reader["extcredits4"].ToString());

                userinfo.Groupid = Int16.Parse(reader["groupid"].ToString());
                userinfo.RatioProtection = TypeConverter.StrToInt(reader["ratioprotection"].ToString());

                userinfo.LastTrackerUpdateTime = TypeConverter.ObjectToDateTime(reader["lasttrackerupdate"]);
                userinfo.LastCreditsUpdateTime = TypeConverter.ObjectToDateTime(reader["lastcreditsupdate"]);
                userinfo.LastKeepRewardUpdateTime = TypeConverter.ObjectToDateTime(reader["lastkeeprewardtime"]);

                userinfo.Joindate = reader["joindate"].ToString();

                userinfo.Passkey = passkey;
            }
            reader.Close();
            reader.Dispose();
            return userinfo;
        }
        /// <summary>
        /// 更新用户发种数目
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int UpdateUserInfoPublishedCount(int userid)
        {
            int seedcount = PTSeeds.GetSeedInfoCount(0, userid, 3, 0, "", 0, "");
            return DatabaseProvider.GetInstance().UpdateUserInfo_RefreashPublish(userid, seedcount);
        }


        /// <summary>
        /// 午夜事件委托
        /// </summary>
        /// <returns></returns>
        public delegate int MoonEventDelegate();
        /// <summary>
        /// 午夜事件委托
        /// </summary>
        /// <param name="process"></param>
        /// <param name="lastok"></param>
        /// <param name="processname"></param>
        private static void RunMoonEvents(MoonEventDelegate process, ref DateTime lastok, string processname)
        {
            try
            {
                //距离上次执行时间8小时以上才执行
                if ((DateTime.Now - lastok).TotalHours > 8)
                {
                    DateTime tskstart = DateTime.Now;
                    int count = process.Invoke();
                    PTLog.InsertSystemLog(PTLog.LogType.MoonEvent, PTLog.LogStatus.Normal, "MOON EVENT", string.Format("{0} 正常:{1} 耗时:{2}", processname, count, (DateTime.Now - tskstart).TotalSeconds.ToString("0.0")));
                    lastok = DateTime.Now;
                }
                else PTLog.InsertSystemLog(PTLog.LogType.MoonEvent, PTLog.LogStatus.Detail, "MOON EVENT Skip", string.Format("{0} 跳过: LastRun:{1}", processname, lastok));
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.MoonEvent, PTLog.LogStatus.Exception, "MOON EVENT Ex", string.Format("{0} 异常:{1}", processname, ex));
            }
        }
        private static object SynObjectUpdateUserList2WeekAndVIP = new object();
        private static DateTime LastTimeUpdateUserList2WeekAndVIPRun = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_User2WeeksBan_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_User2MonthsBan_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_UpdateServerStats_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_ClearUserDailyTraffic_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_ClearSeedFinishedToday_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_ClearTopicRepliesToday_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_ClearSystemLog_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_CleanSeedSearchCache_lastok = new DateTime(2000, 1, 1);
        private static DateTime MoonEvent_CleanExpirePeer_lastok = new DateTime(2000, 1, 1);
        /// <summary>
        /// 午夜事件 每24小时更新一次用户列表，禁封2周内没有产生有效上传的用户，同时调整vip权限，、、、【暂不实施】对非VIP用户，2个月以上未登录者，每天扣除5G上传
        /// </summary>
        public static void MoonEvents()
        {
            lock (SynObjectUpdateUserList2WeekAndVIP)
            {
                try
                {
                    //禁封不活动用户
                    RunMoonEvents(new MoonEventDelegate(DatabaseProvider.GetInstance().UpdateUser2WeeksBan), ref MoonEvent_User2WeeksBan_lastok, "User2WeeksBan");
                    //禁封未能完成新手任务的用户    
                    RunMoonEvents(new MoonEventDelegate(DatabaseProvider.GetInstance().UpdateUser60Ban), ref MoonEvent_User2MonthsBan_lastok, "User2MonthsBan");
                    //强制最后一次更新统计信息
                    RunMoonEvents(new MoonEventDelegate(PrivateBT.UpdateServerStatsMoonEvent), ref MoonEvent_UpdateServerStats_lastok, "UpdateServerStats");
                    //清空用户今日流量
                    RunMoonEvents(new MoonEventDelegate(DatabaseProvider.GetInstance().UpdateUserDailyTraffic), ref MoonEvent_ClearUserDailyTraffic_lastok, "ClearUserDailyTraffic");
                    //清空种子今日完成
                    RunMoonEvents(new MoonEventDelegate(DatabaseProvider.GetInstance().ClearSeedFinishedToday), ref MoonEvent_ClearSeedFinishedToday_lastok, "ClearSeedFinishedToday");
                    //清空帖子今日回复
                    RunMoonEvents(new MoonEventDelegate(DatabaseProvider.GetInstance().ClearTopicRepliesToday), ref MoonEvent_ClearTopicRepliesToday_lastok, "ClearTopicRepliesToday");

                    //只在12:30之后执行
                    if (DateTime.Now.Minute > 30)
                    {
                        //清除过期日志
                        RunMoonEvents(new MoonEventDelegate(PTLog.CleanSystemLog), ref MoonEvent_ClearSystemLog_lastok, "CleanSystemLog");

                        //清除过期日志
                        RunMoonEvents(new MoonEventDelegate(PTSeeds.CleanSeedSearchCache), ref MoonEvent_CleanSeedSearchCache_lastok, "CleanSeedSearchCache");

                        //清除过期节点信息
                        //RunMoonEvents(new MoonEventDelegate(PTPeers.CleanExpirePeer), ref MoonEvent_CleanExpirePeer_lastok, "CleanExpirePeer");
                    }
                }
                catch (System.Exception ex)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.MoonEvent, PTLog.LogStatus.Exception, "MOON EVENT", string.Format("MOON EVENT EX:{0}", ex));
                }
                
            }
        }
        /// <summary>
        /// 更新BT用户信息Ratio
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static int UpdateUserInfo_Ratio(int uid, double ratio)
        {
            return DatabaseProvider.GetInstance().UpdateUserInfo_Ratio(uid, ratio);
        }
        /// <summary>
        /// 更新BT用户信息LastKeepReward
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static int UpdateUserInfo_LastKeepReward(int uid, DateTime updatetime)
        {
            return DatabaseProvider.GetInstance().UpdateUserInfo_LastKeepReward(uid, updatetime);
        }
        /// <summary>
        /// 更新BT用户信息Ratio，UploadCount，DownloadCount
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static int UpdateUserInfo_Ratio(int uid, double ratio, int uploadcount, int downloadcount)
        {
            return DatabaseProvider.GetInstance().UpdateUserInfo_Ratio(uid, ratio, uploadcount, downloadcount);
        }
       
        /// <summary>
        /// 【重要：先清零，后使用】
        ///  更新BT用户信息，realupload/realdownload/extcredits3,4,5,6,7,8,9,10,11,12/finished均为增量【必须先清零，后使用】
        ///  此外还更新ratio,upcout,downcount,lastactivity
        /// </summary>
        /// <param name="btuserinfo"></param>
        /// <returns></returns>
        public static int UpdateUserInfo_Tracker(PTUserInfo btuserinfo, bool updatecreditslastupdatetime)
        {
            if (updatecreditslastupdatetime)
            {
                return DatabaseProvider.GetInstance().UpdateUserInfo_TrackerWithCredits(btuserinfo);
            }
            else
            {
                return DatabaseProvider.GetInstance().UpdateUserInfo_Tracker(btuserinfo);
            }
        }



        //////////////////////////////////////////////////////////////////////////
        //用户密码相关


        /// <summary>
        /// 获取用户随机RKey（登陆一次改变一次）
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetUserRKey(int uid)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetUserRKey(uid);
            string rkey = "";
            if (reader.Read())
            {
                rkey = reader[0].ToString();
            }
            reader.Close();
            return rkey;
        }
        /// <summary>
        /// 更新用户随机RKey，并下发cookie
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public static int SetUserRKey(int uid, string rkey)
        {
            //应该一起重置onlneuser表中所有的rkey。。。

            if (uid < 1) return -1;
            if (rkey.Trim().Length != 10) return -1;


            PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "SetUserRKey",
                                    string.Format("UID:{0} -RKEY:{1} --PAGE:{2}", uid, rkey, DNTRequest.GetPageName()));

            System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = rkey;
            System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
            System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;

            return DatabaseProvider.GetInstance().SetUserRKey(uid, rkey);
        }
        /// <summary>
        /// 更新用户随机RKey，更新在线用户信息rkey，并下发cookie
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public static int SetOnlineRKey(int uid, string rkey)
        {
            //应该一起重置onlneuser表中所有的rkey。。。

            if (uid < 1) return -1;
            if (rkey.Trim().Length != 10) return -1;


            PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "SetOnlineRKey",
                                    string.Format("UID:{0} -RKEY:{1} --PAGE:{2}", uid, rkey, DNTRequest.GetPageName()));

            System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = rkey;
            System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
            System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;

            DatabaseProvider.GetInstance().SetUserRKey(uid, rkey);

            return DatabaseProvider.GetInstance().SetOnlineRKey(uid, rkey);
        }

        /// <summary>
        /// 更新用户随机RKey，只更新user表，不下发cookie
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public static int UpdateUserRKey(int uid, string rkey)
        {
            //应该一起重置onlneuser表中所有的rkey。。。

            if (uid < 1) return -1;
            if (rkey.Trim().Length != 10) return -1;


            //D//PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "UpdateUserRKey",string.Format("UID:{0} -RKEY:{1} --PAGE:{2}", uid, rkey, DNTRequest.GetPageName()));

            return DatabaseProvider.GetInstance().SetUserRKey(uid, rkey);
        }


        /// <summary>
        /// 更新用户随机RKey，并下发cookie
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        public static string SetUserRKey(int uid)
        {
            //应该一起重置onlneuser表中所有的rkey。。。


            if (uid < 1) return "";
            string rkey = PTTools.GetRandomString(10, true);


            //D//PTLog.InsertSystemLogDebug(PTLog.LogType.OnlineUser, PTLog.LogStatus.Warning, "SetUserRKey1", string.Format("UID:{0} -RKEY:{1} --PAGE:{2}", uid, rkey, DNTRequest.GetPageName()));

            if (DatabaseProvider.GetInstance().SetUserRKey(uid, rkey) > 0)
            {
                System.Web.HttpContext.Current.Response.Cookies["rkey"].Value = rkey;
                System.Web.HttpContext.Current.Response.Cookies["rkey"].Expires = DateTime.MaxValue;
                System.Web.HttpContext.Current.Response.Cookies["rkey"].HttpOnly = true;
                return rkey;
            }
            else return "";
        }


        /// <summary>
        /// 从MD5密码生成SHA1密码，用于系统升级
        /// </summary>
        /// <param name="md5pass"></param>
        /// <param name="username"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string GetSHAPasswordFromMD5Password(string md5pass, string username, string salt)
        {
            //原MD5散列为 32Bytes，128Bits
            if (md5pass.Length != 32) return "";
            //加盐（用户名）
            string tmp = string.Format("{0}{1}{2}{3}{4}{5}{6}", md5pass.Substring(0, 8), username, md5pass.Substring(8, 8), "FGBT", md5pass.Substring(16, 8), username, md5pass.Substring(24, 8));
            //新MD5散列
            tmp = Utils.MD5(tmp);
            //SHA散列
            return GetSHAPasswordFrom2xMD5Password(tmp, salt);
        }
        /// <summary>
        /// 从2xMD5密码生成SHA1密码，用于从用户获取的已经加密的密码生成
        /// </summary>
        /// <param name="md5pass2"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string GetSHAPasswordFrom2xMD5Password(string md5pass, string salt)
        {
            //原MD5x2散列为 32Bytes，128Bits
            if (md5pass.Length != 32) return "";
            //加盐
            string tmp = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", md5pass.Substring(0, 8), salt.Substring(0, 4), md5pass.Substring(8, 8), salt.Substring(4, 4), md5pass.Substring(16, 8), salt.Substring(8, 4), md5pass.Substring(24, 8), salt.Substring(12, 4));
            //生成SHA512散列 128Bytes，512Bits
            byte[] tmpbyte = PTTools.SHA512BYTE(tmp);
            //DES加密 128Bytes，512Bits，密钥c4a28de9
            tmp = Discuz.Common.DES.EncodeToHEX(tmpbyte, "c4a28de9");
            //隔一位抽取一个，为什么会是72个。。。。
            string tmp2 = "";
            for (int i = 0; i < tmp.Length; i += 2) tmp2 += tmp.Substring(i, 1);

            if (tmp2.Length > 64) tmp2 = tmp2.Substring(0, 64);
            //返回值 64Bytes，256Bits
            tmp2 = Convert.ToBase64String(PTTools.HEX2BYTE(tmp2));

            if (tmp2.Length != 44) return "";

            return tmp2.Substring(0, 43);
        }

        /// <summary>
        /// 更新用户SHA密码，成功返回1，失败返回负数。 isOriginalPassword = true：password目前为明文密码，否则为2xMD5结果
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="isOriginalPassword"></param>
        /// <returns></returns>
        public static int UpdatePasswordSHA512(int uid, string password, bool isOriginalPassword)
        {
            if (uid < 1 || password.Length < 6) return -1;

            string salt = PTTools.GetRandomString(16, true);
            string passwordsha = "";
            //兼容旧密码模式
            if (isOriginalPassword) passwordsha = GetSHAPasswordFromMD5Password(Utils.MD5(password), Users.GetUserName(uid), salt);
            //新密码模式
            else passwordsha = GetSHAPasswordFrom2xMD5Password(password, salt);
            //如果有问题
            if (passwordsha.Length != 43) return -2;
            //更新
            return DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, passwordsha, salt);
        }


        //private static int updateuid = 0;
        ///// <summary>
        ///// 更新用户密码的额函数，放在peer计划任务中执行
        ///// </summary>
        //public static void UpdateUserPass()
        //{
        //    for (int i = 0; i < 2000; i++)
        //    {
        //        if (updateuid > 43000) return;

        //        updateuid++;
        //        if (updateuid < 1) return;

        //        IDataReader reader = DatabaseProvider.GetInstance().GetUserPasswordSHAInfoToReader(updateuid);
        //        string username = "";
        //        string passwordsha = "";
        //        string passwordmd5 = "";
        //        string secquesmd5 = "";
        //        string salt = "";
        //        int uid = -1;

        //        if (reader.Read())
        //        {
        //            uid = TypeConverter.ObjectToInt(reader["uid"], -1);
        //            username = reader["username"].ToString().Trim();
        //            passwordsha = reader["passwordsha"].ToString().Trim();
        //            passwordmd5 = reader["password"].ToString().Trim();
        //            salt = reader["salt"].ToString().Trim();
        //            secquesmd5 = reader["secques"].ToString().Trim();
        //        }
        //        reader.Close();
        //        reader.Dispose();

        //        if (uid < 0) continue;

        //        //新密码不存在，则生成新密码，md5来自数据库，而不是用户提交
        //        //if ((passwordsha == "" || salt == "" || passwordsha.Length != 64) && passwordmd5.Length == 32)
        //        if (passwordmd5.Length == 32)
        //        {
        //            //IHOME默认空密码，不生成新密码，置空
        //            if (passwordmd5.Substring(0, 16).ToLower() == "0123456789abcdef")
        //            {
        //                DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, "", "");
        //                continue;
        //            }

        //            salt = PTTools.GetRandomString(16, true);
        //            passwordsha = GetSHAPasswordFromMD5Password(passwordmd5, username, salt);
        //            DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, passwordsha, salt);
        //        }
        //        //else if (passwordmd5.Length == 32 && passwordmd5.Substring(0, 16).ToLower() == "0123456789abcdef" && (passwordsha != "" || salt != ""))
        //        //{
        //        //    DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, "", "");
        //        //    continue;
        //        //}
        //    }
        //}



        /// <summary>
        /// 校验用户SHA密码，成功返回用户uid，失败返回负数。 isOriginalPassword = true：password目前为明文密码，否则为2xMD5结果
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int CheckPasswordSHA512(string username, string password, bool isOriginalPassword, bool isMd5, string secques, bool CheckSecQues)
        {
            if (username == null || username.Trim() == "") return -2;
            if (password.Trim() == "") return -4;

            IDataReader reader = DatabaseProvider.GetInstance().GetUserPasswordSHAInfoToReader(username);
            string passwordsha = "";
            string passwordmd5 = "";
            string salt = "";
            string secquesmd5 = "";
            int uid = -1;


            if (reader.Read())
            {
                uid = TypeConverter.ObjectToInt(reader["uid"], -1);
                username = reader["username"].ToString().Trim();
                passwordsha = reader["passwordsha"].ToString().Trim();
                passwordmd5 = reader["password"].ToString().Trim();
                salt = reader["salt"].ToString().Trim();
                secquesmd5 = reader["secques"].ToString().Trim();
            }
            reader.Close();
            reader.Dispose();

            if (uid < 0) return -3;
            if (passwordsha == "" || salt == "") return -1;

            ////老密码兼容模式，仅仅检测，需要检测密码是否存在，不存在则更新密码
            //if(isOriginalPassword && passwordmd5 != "")
            //{
            //    //新密码不存在，则生成新密码，md5来自数据库，而不是用户提交
            //    if (passwordsha == "" || salt == "" || passwordsha.Length != 43)
            //    {
            //        if (passwordmd5.Substring(0, 16).ToLower() == "0123456789abcdef") return -1;

            //        salt = PTTools.GetRandomString(16, true);
            //        passwordsha = GetSHAPasswordFromMD5Password(passwordmd5, username, salt);
            //        DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, passwordsha, salt);
            //        return uid;
            //    }
            //    //新密码已经存在，则校验密码
            //    else
            //    {
            //        if (passwordsha == GetSHAPasswordFromMD5Password(isMd5 ? password : Utils.MD5(password), username, salt) &&
            //            (CheckSecQues ? secquesmd5 == secques : true))
            //            return uid;
            //        else
            //        {
            //            PTError.InsertErrorLog("SHAPASS", string.Format("用户{0}---{3}新密码校验错误2：原MD5：{1}，新SHA：{2}", username, passwordmd5, passwordsha, uid));
            //            return -1;
            //        }
            //    }
            //}


            //过度模式
            if (isOriginalPassword)
            {
                if (passwordsha == GetSHAPasswordFromMD5Password(isMd5 ? password : Utils.MD5(password), username, salt) &&
                    (CheckSecQues ? secquesmd5 == secques : true))
                    return uid;
                else
                {
                    if(secquesmd5 != secques)
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASS", string.Format("用户{0}---{3}提示问题校验错误2：原问题：{1}，提交的问题：{2}", username, secquesmd5, secques, uid));
                    }

                    if(passwordsha != GetSHAPasswordFromMD5Password(isMd5 ? password : Utils.MD5(password), username, salt))
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASS", string.Format("用户{0}---{3}新密码校验错误2：原MD5：{1}，新SHA：{2}", username, passwordmd5, passwordsha, uid));

                        string md5 = Utils.MD5(password);
                        if (md5 == passwordmd5)
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASSERR", string.Format("!!!!!用户{0}---{3} MD5正确-----------：原MD5：{1}，新SHA：{2}", username, passwordmd5, passwordsha, uid));

                            //出问题的情况，更新MD5
                            salt = PTTools.GetRandomString(16, true);
                            passwordsha = GetSHAPasswordFromMD5Password(passwordmd5, username, salt);
                            DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, passwordsha, salt);
                            return uid;
                        }
                        else PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASS", string.Format("用户{0}---{3}MD5错误：原MD5：{1}，提交MD5：{2}", username, passwordmd5, md5, uid));
                   }

                   return -1;
                }
            }    
            //正常工作模式，传递来的password已经是MD5x2
            else
            {
                if (passwordsha == GetSHAPasswordFrom2xMD5Password(password, salt) && (CheckSecQues ? secquesmd5 == secques : true)) return uid;
                else return -1;
            }
        }
        /// <summary>
        /// 检测用户是否不存在SHA512密码
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool IsEmptyPasswordSHA512(int uid)
        {
            //出错返回不为空
            if (uid < 1) return false;

            IDataReader reader = DatabaseProvider.GetInstance().GetUserPasswordSHAInfoToReader(uid);
            string username = "";
            string passwordsha = "";
            string passwordmd5 = "";
            string secquesmd5 = "";
            string salt = "";

            if (reader.Read())
            {
                uid = TypeConverter.ObjectToInt(reader["uid"], -1);
                username = reader["username"].ToString().Trim();
                passwordsha = reader["passwordsha"].ToString().Trim();
                passwordmd5 = reader["password"].ToString().Trim();
                salt = reader["salt"].ToString().Trim();
                secquesmd5 = reader["secques"].ToString().Trim();
            }
            reader.Close();
            reader.Dispose();

            //密码为空
            if (passwordsha == "" && salt == "") return true;
            //密码不为空
            else return false;
        }
        /// <summary>
        /// 校验用户SHA密码，成功返回用户uid，失败返回负数。 isOriginalPassword = true：password目前为明文密码，否则为2xMD5结果
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int CheckPasswordSHA512(int uid, string password, bool isOriginalPassword, bool isMd5, string secques, bool CheckSecQues)
        {
            if (uid < 1) return -2;
            if (password.Trim() == "") return -4;

            IDataReader reader = DatabaseProvider.GetInstance().GetUserPasswordSHAInfoToReader(uid);
            string username = "";
            string passwordsha = "";
            string passwordmd5 = "";
            string secquesmd5 = "";
            string salt = "";

            if (reader.Read())
            {
                uid = TypeConverter.ObjectToInt(reader["uid"], -1);
                username = reader["username"].ToString().Trim();
                passwordsha = reader["passwordsha"].ToString().Trim();
                passwordmd5 = reader["password"].ToString().Trim();
                salt = reader["salt"].ToString().Trim();
                secquesmd5 = reader["secques"].ToString().Trim();
            }
            reader.Close();
            reader.Dispose();

            if (uid < 0) return -3;
            if (passwordsha == "" || salt == "") return -1;


            ////老密码兼容模式，仅仅检测，需要检测密码是否存在，不存在则更新密码
            //if (isOriginalPassword && passwordmd5 != "")
            //{
            //    //新密码不存在，则生成新密码，md5来自数据库，而不是用户提交
            //    if (passwordsha == "" || salt == "" || passwordsha.Length != 43)
            //    {
            //        if (passwordmd5.Substring(0, 16).ToLower() == "0123456789abcdef") return -1;

            //        salt = PTTools.GetRandomString(16, true);
            //        passwordsha = GetSHAPasswordFromMD5Password(passwordmd5, username, salt);
            //        DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, passwordsha, salt);
            //        return uid;
            //    }
            //    //新密码已经存在，则校验密码
            //    else
            //    {
            //        if (passwordsha == GetSHAPasswordFromMD5Password(isMd5 ? password : Utils.MD5(password), username, salt) && 
            //            (CheckSecQues ? secquesmd5 == secques : true)) 
            //            return uid;
            //        else
            //        {
            //            PTError.InsertErrorLog("SHAPASS", string.Format("用户{0}---{3}新密码校验错误2：原MD5：{1}，新SHA：{2}", username, passwordmd5, passwordsha, uid));
            //            return -1;
            //        }
            //    }
            //}
            
            //过度模式
            if (isOriginalPassword)
            {
                if (passwordsha == GetSHAPasswordFromMD5Password(isMd5 ? password : Utils.MD5(password), username, salt) &&
                    (CheckSecQues ? secquesmd5 == secques : true))
                    return uid;
                else
                {
                    if (secquesmd5 != secques)
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASS", string.Format("用户{0}---{3}提示问题校验错误1：原问题：{1}，提交的问题：{2}", username, secquesmd5, secques, uid));
                    }

                    if (passwordsha != GetSHAPasswordFromMD5Password(isMd5 ? password : Utils.MD5(password), username, salt))
                    {
                        PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASS", string.Format("用户{0}---{3}新密码校验错误1：原MD5：{1}，新SHA：{2}", username, passwordmd5, passwordsha, uid));

                        string md5 = Utils.MD5(password);
                        if (md5 == passwordmd5)
                        {
                            PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASSERR", string.Format("!!!!!用户{0}---{3} MD5正确-1----------：原MD5：{1}，新SHA：{2}", username, passwordmd5, passwordsha, uid));

                            //出问题的情况，更新MD5
                            salt = PTTools.GetRandomString(16, true);
                            passwordsha = GetSHAPasswordFromMD5Password(passwordmd5, username, salt);
                            DatabaseProvider.GetInstance().UpdateUserPasswordSHAInfo(uid, passwordsha, salt);
                            return uid;
                        }
                        else PTLog.InsertSystemLogDebug(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "SHAPASS", string.Format("用户{0}---{3}MD5错误1：原MD5：{1}，提交MD5：{2}", username, passwordmd5, md5, uid));
                    }

                    return -1;
                }
            }
            //正常工作模式，传递来的password已经是MD5x2
            else
            {
                if (passwordsha == GetSHAPasswordFrom2xMD5Password(password, salt) && (CheckSecQues ? secquesmd5 == secques : true)) return uid;
                else return -1;
            }
        }



        /// <summary>
        /// 【主要使用模块】判断指定用户Cookie密码是否正确.
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果用户密码正确则返回true, 否则返回false</returns>
        public static int CheckCookiePassword(int uid, string password)
        {
            ShortUserInfo userInfo = Discuz.Data.Users.CheckPassword(uid, password, false);

            return userInfo == null ? -1 : userInfo.Uid;
        }

        /// <summary>
        /// 【主要使用模块】更新指定用户Cookie密码是否正确.
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果用户密码正确则返回true, 否则返回false</returns>
        public static void UpdateCookiePassword(int uid, string password)
        {
            Discuz.Data.Users.UpdateUserPassword(uid, password, false);
        }
        /// <summary>
        /// 【主要使用模块】更新指定用户Cookie密码是否正确.
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果用户密码正确则返回true, 否则返回false</returns>
        public static string UpdateCookiePassword(int uid)
        {
            string password = PTTools.GetRandomString(32, true);
            Discuz.Data.Users.UpdateUserPassword(uid, password, false);
            return password;
        }




        /// <summary>
        /// 【主要使用模块】检测密码和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public static int CheckPasswordAndSecques(string username, string password, bool originalpassword, int questionid, string answer)
        {
            return CheckPasswordSHA512(username, password, true, !originalpassword, ForumUtils.GetUserSecques(questionid, answer), true);
        }

        /// <summary>
        /// 检查密码，不常用
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public static int CheckPassword(string username, string password, bool originalpassword)
        {
            return CheckPasswordSHA512(username, password, true, !originalpassword, "", false);
        }

        /// <summary>
        /// 判断指定用户密码是否正确.
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果用户密码正确则返回true, 否则返回false</returns>
        public static int CheckPassword(int uid, string password, bool originalpassword)
        {
            return  CheckPasswordSHA512(uid, password, true, !originalpassword, "", false);
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <returns>成功返回true否则false</returns>
        public static bool UpdateUserPassword(int uid, string password, bool originalpassword)
        {
            UpdateCookiePassword(uid);
            return (PTUsers.UpdatePasswordSHA512(uid, password, true) > 0);
        }

        /// <summary>
        /// 更新用户安全问题
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>成功返回true否则false</returns>
        public static bool UpdateUserSecques(int uid, int questionid, string answer)
        {
            if (Discuz.Data.Users.GetShortUserInfo(uid) == null)
                return false;

            Discuz.Data.Users.UpdateUserSecques(uid, ForumUtils.GetUserSecques(questionid, answer));
            return true;
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="userInfo">用户信息(密码字段为明文)</param>
        /// <returns>是否成功</returns>
        public static bool ResetPassword(UserInfo userInfo)
        {
            UpdateCookiePassword(userInfo.Uid);
            return UpdatePasswordSHA512(userInfo.Uid, userInfo.Password, true) > 0;
        }


        //【END】密码相关
        //////////////////////////////////////////////////////////////////////////

    }
}