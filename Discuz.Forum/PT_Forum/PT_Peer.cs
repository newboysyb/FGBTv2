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



    /// <summary>
    /// PTPeer节点操作类
    /// </summary>
    public class PTPeers
    {
        /// <summary>
        /// 清理失效的节点
        /// </summary>
        /// <returns></returns>
        public static int CleanExpirePeer()
        {
            return DatabaseProvider.GetInstance().CleanExpirePeer();
        }


        public static bool ExistPeerInfo()
        {
            return true;
        }

        /// <summary>
        /// 按照seedid删除peer，并更新seed和user表内容
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static int DeletePeerBySeedid(int seedid)
        {
            PTSeedinfo seedinfo = PTSeeds.GetSeedInfo(seedid);
            if (seedinfo.SeedId < 1) return -1;

            DataTable seedidlist = DatabaseProvider.GetInstance().GetPeerList(seedid);
            int delcount = 0;

            foreach (DataRow dr in seedidlist.Rows)
            {
                PrivateBTPeerInfo peerinfo = new PrivateBTPeerInfo();
                peerinfo.SeedId = TypeConverter.ObjectToInt(dr["seedid"]);
                peerinfo.PeerId = dr["peerid"].ToString().Trim();
                peerinfo.Uid = TypeConverter.ObjectToInt(dr["uid"]);

                //删除这个Peer
                PrivateBT.DeletePeerInfo(peerinfo);

                //更新userinfo
                PTUserInfo btuserinfo = PTUsers.GetBtUserInfoForPagebase(peerinfo.Uid);
                if (btuserinfo == null || btuserinfo.Uid < 1) continue;

                //重置内容
                btuserinfo.UploadCount = PTSeeds.GetSeedInfoCount(0, peerinfo.Uid, 1, 0, "", 0, "");
                btuserinfo.DownloadCount = PTSeeds.GetSeedInfoCount(0, peerinfo.Uid, 2, 0, "", 0, "");

                //增量数值清零
                btuserinfo.Extcredits3 = 0M;
                btuserinfo.Extcredits4 = 0M;
                btuserinfo.Extcredits5 = 0M;
                btuserinfo.Extcredits6 = 0M;
                btuserinfo.Extcredits7 = 0M;
                btuserinfo.Extcredits8 = 0M;
                btuserinfo.Extcredits9 = 0M;
                btuserinfo.Extcredits10 = 0M;
                btuserinfo.Extcredits11 = 0M;
                btuserinfo.Extcredits12 = 0M;
                btuserinfo.FinishCount = 0;
                PTUsers.UpdateUserInfo_Tracker(btuserinfo, false);

                delcount++;
                
            }
            seedidlist.Dispose();
            seedidlist = null;

            //更新种子
            PTSeeds.UpdateSeedAnnounce(seedid, 0, 0, -1, 0);

            return delcount;
        }

        /// <summary>
        /// 获得用户正在上传的种子数
        /// 【announce用】
        /// </summary>
        /// <returns></returns>
        public static int GetPeerUserUploadCount(int uid)
        {
            return DatabaseProvider.GetInstance().GetPeerUserUploadCount(uid);
        }
        /// <summary>
        /// 获得用户正在下载的种子数
        /// 【announce用】
        /// </summary>
        /// <returns></returns>
        public static int GetPeerUserDownloadCount(int uid)
        {
            return DatabaseProvider.GetInstance().GetPeerUserDownloadCount(uid);
        }


    }




    public partial class PrivateBT
    {

        public static PTKeepRewardStatic GetKeepReward(int uid, DateTime lastkeeprewardupdatetime, string userjoindate)
        {
            
            PTKeepRewardStatic krs = new PTKeepRewardStatic();
            DataTable userpeerlist = PrivateBT.GetPeerList(uid, true);
            //剔除重复
            int lastseedid = 0;
            decimal lastseedreward = 0M;
            foreach (DataRow dr in userpeerlist.Rows)
            {
                int k_seedid = TypeConverter.ObjectToInt(dr["seedid"]);
                if (k_seedid == lastseedid)
                {
                    dr["seedid"] = "-1";
                }
                lastseedid = k_seedid;
            }
            foreach (DataRow dr in userpeerlist.Rows)
            {
                int k_seedid = TypeConverter.ObjectToInt(dr["seedid"]);

                DateTime k_firsttime = TypeConverter.ObjectToDateTime(dr["firsttime"]);
                DateTime k_lasttime = TypeConverter.ObjectToDateTime(dr["lasttime"]);
                int k_keeptime = TypeConverter.ObjectToInt(dr["keeptime"]) + (int)(DateTime.Now - k_firsttime).TotalSeconds;

                decimal k_filesize = TypeConverter.ObjectToDecimal(dr["filesize"]);
                decimal k_useruptraffic = TypeConverter.ObjectToDecimal(dr["uptraffic"]);
                DateTime k_lastfinish = TypeConverter.ObjectToDateTime(dr["lastfinish"]);
                int k_live = TypeConverter.ObjectToInt(dr["live"]);
                int k_upload = TypeConverter.ObjectToInt(dr["upload"]);

                if (k_upload < 1) k_upload = 1;
                
                if (k_seedid > 0 && k_firsttime < lastkeeprewardupdatetime)
                {
                    //如果超时（可能节点已经消失），不计保种奖励，未来修改为nexttime判断
                    if ((DateTime.Now - k_lasttime).TotalSeconds < 600 || PrivateBT.IsServerUser(uid) || uid == 13 || uid == 10597)
                    {
                        //计算保种奖励数值
                        decimal hourkeepreward = PTSeeds.GetSeedAward(k_filesize, k_useruptraffic, k_live, k_keeptime, k_upload, k_lastfinish, userjoindate);
                        decimal oldkeepreward = PTSeeds.GetSeedAward(k_filesize, k_live, k_upload);

                        krs.Old_RewardPerHour += oldkeepreward;
                        krs.Old_RewardPerHour_Real += oldkeepreward / k_upload;

                        if (k_filesize > 100 * 1024 * 1024 * 1024M)
                        {
                            krs.BigBig_UpCount++;
                            krs.BigBig_RewardPerHour += hourkeepreward;
                        }
                        else if (k_filesize > 25 * 1024 * 1024 * 1024M)
                        {
                            krs.Big_UpCount++;
                            krs.Big_RewardPerHour += hourkeepreward;
                        }
                        else if (k_filesize > 5 * 1024 * 1024 * 1024M)
                        {
                            krs.Mid_UpCount++;
                            krs.Mid_RewardPerHour += hourkeepreward;
                        }
                        else if (k_filesize > 1024 * 1024 * 1024M)
                        {
                            krs.Small_UpCount++;
                            krs.Small_RewardPerHour += hourkeepreward;
                        }
                        else
                        {
                            krs.Tiny_UpCount++;
                            krs.Tiny_RewardPerHour += hourkeepreward;
                        }
                        lastseedreward = hourkeepreward;
                        krs.TotalUpCount++;
                    }
                    else lastseedreward = 0;
                }
                else if (k_seedid == -1)
                {
                    //减扣上一个种子的数值
                    if (k_filesize > 100 * 1024 * 1024 * 1024M)
                    {
                        krs.BigBig_UpCount--;
                        krs.BigBig_RewardPerHour -= lastseedreward;
                    }
                    else if (k_filesize > 25 * 1024 * 1024 * 1024M)
                    {
                        krs.Big_UpCount--;
                        krs.Big_RewardPerHour -= lastseedreward;
                    }
                    else if (k_filesize > 5 * 1024 * 1024 * 1024M)
                    {
                        krs.Mid_UpCount--;
                        krs.Mid_RewardPerHour -= lastseedreward;
                    }
                    else if (k_filesize > 1024 * 1024 * 1024M)
                    {
                        krs.Small_UpCount--;
                        krs.Small_RewardPerHour -= lastseedreward;
                    }
                    else
                    {
                        krs.Tiny_UpCount--;
                        krs.Tiny_RewardPerHour -= lastseedreward;
                    }
                    krs.TotalUpCount--;
                }
            }
            if (krs.BigBig_RewardPerHour > krs.BigBig_RewardPerHour_Limit) krs.BigBig_RewardPerHour = krs.BigBig_RewardPerHour_Limit;
            if (krs.Big_RewardPerHour > krs.Big_RewardPerHour_Limit) krs.Big_RewardPerHour = krs.Big_RewardPerHour_Limit;
            if (krs.Mid_RewardPerHour > krs.Mid_RewardPerHour_Limit) krs.Mid_RewardPerHour = krs.Mid_RewardPerHour_Limit;
            if (krs.Small_RewardPerHour > krs.Small_RewardPerHour_Limit) krs.Small_RewardPerHour = krs.Small_RewardPerHour_Limit;
            if (krs.Tiny_RewardPerHour > krs.Tiny_RewardPerHour_Limit) krs.Tiny_RewardPerHour = krs.Tiny_RewardPerHour_Limit;

            krs.TotalRewardPerHour = krs.BigBig_RewardPerHour + krs.Big_RewardPerHour + krs.Mid_RewardPerHour + krs.Small_RewardPerHour + krs.Tiny_RewardPerHour;

            return krs;
        }


        /// <summary>
        /// 获得用户上传保种奖励数值
        /// </summary>
        /// <param name="userpeerlist"></param>
        /// <param name="lastupdate"></param>
        /// <returns></returns>
        public static PTKeepRewardStatic GetKeepReward(PTUserInfo keepuserinfo)
        {
            PTKeepRewardStatic krs = new PTKeepRewardStatic();
            if ((DateTime.Now - keepuserinfo.LastKeepRewardUpdateTime).TotalSeconds < 10) return krs;
            else return GetKeepReward(keepuserinfo.Uid, keepuserinfo.LastKeepRewardUpdateTime, keepuserinfo.Joindate);
            
        }


        /// <summary>
        /// 获得对应uid的Peer节点信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetPeerList(int uid, bool seeding)
        {
            return DatabaseProvider.GetInstance().GetPeerList(uid, seeding);
        }
        /// <summary>
        /// 获得对应SeedId的种子的Peer节点信息
        /// 此处获得的用于显示的down和up均已包含历史数据和本次数据
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetPeerList(int seedid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetPeerList(seedid);
            dt.Columns.Add("up");
            dt.Columns.Add("upspeed");
            dt.Columns.Add("down");
            dt.Columns.Add("downspeed");
            dt.Columns.Add("rat");
            dt.Columns.Add("ipstr");
            dt.Columns.Add("per");
            dt.Columns.Add("active");
            foreach (DataRow dr in dt.Rows)
            {
                dr["up"] = PTTools.Upload2Str(decimal.Parse(dr["totalupload"].ToString()) + decimal.Parse(dr["upload"].ToString()));
                dr["upspeed"] = PTTools.Upload2Str((decimal)double.Parse(dr["uploadspeed"].ToString())) + "/s";
                dr["down"] = PTTools.Upload2Str(decimal.Parse(dr["totaldownload"].ToString()) + decimal.Parse(dr["download"].ToString()));
                dr["downspeed"] = PTTools.Upload2Str((decimal)double.Parse(dr["downloadspeed"].ToString())) + "/s";
                dr["rat"] = PTTools.Ratio2Str(decimal.Parse(dr["extcredits3"].ToString()), decimal.Parse(dr["extcredits4"].ToString()));
                
                int ipstatus = int.Parse(dr["isipv6"].ToString());
                if (ipstatus == 0 || ipstatus == 2) dr["ipstr"] = "IPv4";
                else if (ipstatus == 1 || ipstatus == 3) dr["ipstr"] = "IPv4+IPv6";
                else if (ipstatus == 4 || ipstatus == 5 || ipstatus == 6 || ipstatus == 7) dr["ipstr"] = "IPv6";
                else dr["ipstr"] = "异常";
                
                dr["per"] = (double.Parse(dr["percentage"].ToString()) * 100).ToString("0.00") + "%";
                dr["active"] = PTTools.Minutes2String((DateTime.Now - DateTime.Parse(dr["firsttime"].ToString())).TotalMinutes);
            }
            dt.Dispose();
            return dt;
        }
        /// <summary>
        /// 获得当前总下载速度
        /// </summary>
        /// <returns></returns>
        public static decimal GetPeerTotalSpeed()
        {
            return DatabaseProvider.GetInstance().GetPeerTotalSpeed();
        }

        ///// <summary>
        ///// 阻止用户同时使用双Tracker，寻找seedid相等，ipv6addip = 指定ip或者peerid=指定的数量
        ///// </summary>
        ///// <returns></returns>
        //public static int GetPeerCountV4V6(string ip, int seedid, string peerid)
        //{
        //    if (!(PTTools.IsIPv6(ip))) return 0;
        //    else return DatabaseProvider.GetInstance().GetPeerCountV4V6(ip, seedid, peerid);
        //}

        /// <summary>
        /// 获得指定种子的制定uid或者ip的peer节点数，0或者空为不限
        /// </summary>
        /// <returns></returns>
        public static int GetPeerCount(int userid, int seedid, bool isdownload)
        {
            return DatabaseProvider.GetInstance().GetPeerCount(userid, seedid, isdownload);
        }
        /// <summary>
        /// 获得正在上传/下载/全部的用户数，不计算重复用户的peer，1下载，2上传，其余不限
        /// </summary>
        /// <returns></returns>
        public static int GetPeerUserCount(int userstatus)
        {
            return DatabaseProvider.GetInstance().GetPeerUserCount(userstatus);
        }

        //C//2014.12.07/// <summary>
        ///// 【EX除去指定uid】获得指定种子的指定uid的peer节点数，uid为0则不限uid，只判断seedid和peerid
        ///// 此函数用于：tracker中，判断同一个ut是否挂有多人tracker（需要检测同seedid和peerid的种子，限制uid和不限制情况下数量是否相等）
        ///// 以及：判断是否用户同时两个ut下载同一个种子（开始新下载时，判断通seedid和uid，限制peerid和不限制peerid的情况，数量是否相等）
        ///// </summary>
        ///// <returns></returns>
        //public static int GetPeerCount(int userid, string peerid, int seedid)
        //{
        //    //seedid不能为空
        //    if (seedid < 1) return 0;
        //    else return DatabaseProvider.GetInstance().GetPeerCount(userid, peerid, seedid);
        //}
                /// <summary>
        /// 判断是否存在同peerid的其他节点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="peerid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static int IsExistsOtherPeer(int uid, string peerid, int seedid)
        {
            if (uid < 1 || seedid < 1) return -1;
            else return DatabaseProvider.GetInstance().IsExistsOtherPeer(uid, peerid, seedid);
        }
                /// <summary>
        /// 判断是否存在同peerid的其他节点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="peerid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static int IsExistsPeer(int uid, int seedid, bool isdownload)
        {
            if (uid < 1) return -1;
            else return DatabaseProvider.GetInstance().IsExistsPeer(uid, seedid, isdownload);
        }
        //C//2014.12.07///// <summary>
        ///// 获得指定seedid，指定uid，置顶peerid的记录数（这三项是确定peer的项目）
        ///// </summary>
        ///// <returns></returns>
        //public static int GetPeerCount(PrivateBTPeerInfo peerinfo)
        //{
        //    if (peerinfo.SeedId < 1 || peerinfo.Uid < 1 || peerinfo.PeerId.Length != 40) return 0;
        //    else return DatabaseProvider.GetInstance().GetPeerCount(peerinfo.Uid, peerinfo.PeerId, peerinfo.SeedId);
        //}


        private static object SynObjectInsertPeer = new object();
        /// <summary>
        /// 插入peer信息，会更新peerinfo中的pid
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int InsertPeerInfo(PrivateBTPeerInfo peerinfo)
        {
            //decimal totalupload = 0M;
            //decimal totaldownload = 0M;
            //GetPerUserSeedTraffic(peerinfo.SeedId, peerinfo.Uid, ref totalupload, ref totaldownload);
            //peerinfo.TotalUpload = totalupload;
            //peerinfo.TotalDownload = totaldownload;
            
            //临时措施：使用lock判断是否存在，防止重复，未来更改为数据库唯一键值
            int rtvalue = -1;
            //lock (SynObjectInsertPeer)
            //{
                //if(GetPeerInfo(peerinfo) == null)
                //if (GetPeerCount(peerinfo) == 0)
                //{
                    try
                    {
                        rtvalue = DatabaseProvider.GetInstance().InsertPeerInfo(peerinfo);
                    }
                    catch (System.Exception ex)
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.TrackerInsertPeer, PTLog.LogStatus.Exception, "INSERT PEER", string.Format("UID:{0}-SID:{1}-EX:{2}", peerinfo.Uid, peerinfo.SeedId, ex));
                    }

                    try
                    {
                        if (rtvalue > 0)
                        {
                            peerinfo.Pid = rtvalue;
                            PTPeerLog.InsertPeerHistory(peerinfo);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        PTLog.InsertSystemLog(PTLog.LogType.TrackerInsertPeer, PTLog.LogStatus.Exception, "INSERT PEERH", string.Format("UID:{0}-SID:{1}-EX:{2}", peerinfo.Uid, peerinfo.SeedId, ex));
                    }


                //}
            //}
            return rtvalue;
        }
        
        ///// <summary>
        ///// 【无引用】//////获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //public static DataTable GetPeerList(int seedid, bool upload, int maxcount)
        //{
        //    return DatabaseProvider.GetInstance().GetPeerList(seedid, upload, maxcount);
        //}
        
        /// <summary>
        /// 【只获得IP，供tracker使用，区分上传下载，用于上传者】获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetPeerListTracker(int seedid, bool upload, int maxcount, bool withpeerid)
        {
            return DatabaseProvider.GetInstance().GetPeerListTracker(seedid, upload, maxcount, withpeerid);
        }
        /// <summary>
        /// 【只获得IP，供tracker使用，不区分上传下载，用于下载者】获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetPeerListTrackerAll(int seedid, int maxcount, bool withpeerid)
        {
            return DatabaseProvider.GetInstance().GetPeerListTrackerAll(seedid, maxcount, withpeerid);
        }
        /// <summary>
        /// 获得指定种子的上传数
        /// </summary>
        /// <returns></returns>
        public static int GetPeerSeedUploadCount(int seedid)
        {
            return DatabaseProvider.GetInstance().GetPeerSeedUploadCount(seedid);
        }
        /// <summary>
        /// 获得指定种子的下载数
        /// </summary>
        /// <returns></returns>
        public static int GetPeerSeedDownloadCount(int seedid)
        {
            return DatabaseProvider.GetInstance().GetPeerSeedDownloadCount(seedid);
        }


       

        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（2项）：
        /// v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int UpdatePeerInfo_NoTraffic(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            return DatabaseProvider.GetInstance().UpdatePeerInfo_NoTraffic(peerinfo, IsIPv6Request);
        }
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（4项）：
        /// upload，uploadspeed，v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int UpdatePeerInfo_UpTrafficOnly(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            return DatabaseProvider.GetInstance().UpdatePeerInfo_UpTrafficOnly(peerinfo, IsIPv6Request);
        }
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（7项）：
        /// upload，uploadspeed，download，downloadspeed，percentage，v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int UpdatePeerInfo_UpDownTrafficOnly(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            return DatabaseProvider.GetInstance().UpdatePeerInfo_UpDownTrafficOnly(peerinfo, IsIPv6Request);
        }
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（13项）：
        /// upload，uploadspeed，download，downloadspeed，percentage，v4last/v6last，lasttime
        /// ip，ipv6ip，ipv6addip，isipv6，port，seed
        /// 以下内容不会被更新：
        /// client，firsttime，totalupload，totaldownload，以及3要素seedid，uid，peerid
        /// 更换client的行为应该被记录为警告事件
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int UpdatePeerInfo(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            return DatabaseProvider.GetInstance().UpdatePeerInfo(peerinfo, IsIPv6Request);
        }




        ///// <summary>
        ///// 更新指定的peer信息，同时更新PeerID，更新除seedid,uid（三要素之二）和firsttime之外的18-3项
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public static int UpdatePeerInfoPeerID(PrivateBTPeerInfo peerinfo, bool ipv6tracker)
        //{
        //    return DatabaseProvider.GetInstance().UpdatePeerInfoPeerID(peerinfo, ipv6tracker);
        //}
        ///// <summary>
        ///// 更新指定的peer信息，只更新PeerID
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public static int UpdatePeerInfoIDOnly(PrivateBTPeerInfo peerinfo)
        //{
        //    return DatabaseProvider.GetInstance().UpdatePeerInfoIDOnly(peerinfo);
        //}
        /// <summary>
        /// 获得指定的peer信息，仅用于tracker，获取有限信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static PrivateBTPeerInfo GetPeerInfo(PrivateBTPeerInfo peerinfo)
        {
            PrivateBTPeerInfo oldseedinfo = null;

            IDataReader reader = DatabaseProvider.GetInstance().GetPeerInfo(peerinfo);

            if (reader.Read())
            {
                oldseedinfo = LoadSinglePeerInfo(reader);

                reader.Close();
                reader.Dispose();
                return oldseedinfo;
            }
            reader.Close();
            reader.Dispose();
            return null;
        }
        /// <summary>
        /// 获得指定的peer信息，仅用于tracker，获取有限信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static PrivateBTPeerInfo GetPeerInfo_OldDownload(PrivateBTPeerInfo peerinfo)
        {
            PrivateBTPeerInfo oldseedinfo = null;

            IDataReader reader = DatabaseProvider.GetInstance().GetPeerInfo(peerinfo);

            if (reader.Read())
            {
                oldseedinfo = LoadSinglePeerInfo(reader);

                reader.Close();
                reader.Dispose();
                return oldseedinfo;
            }
            reader.Close();
            reader.Dispose();
            return null;
        }

        public static PrivateBTPeerInfo LoadSinglePeerInfo(IDataReader reader)
        {
            PrivateBTPeerInfo oldseedinfo = new PrivateBTPeerInfo();

            oldseedinfo.Pid = TypeConverter.ObjectToInt(reader["pid"], -1);
            oldseedinfo.Uid = TypeConverter.ObjectToInt(reader["uid"], -1);
            oldseedinfo.SeedId = TypeConverter.ObjectToInt(reader["seedid"], -1);
            oldseedinfo.IsSeed = TypeConverter.StrToBool(reader["seed"].ToString(), false);
            oldseedinfo.PeerLock = TypeConverter.StrToInt(reader["peerlock"].ToString(), -1);
            oldseedinfo.IPStatus = TypeConverter.StrToInt(reader["isipv6"].ToString(), 0);
            oldseedinfo.PeerId = reader["peerid"].ToString();
            oldseedinfo.IPv4IP = reader["ip"].ToString().Trim();
            oldseedinfo.v4Last = TypeConverter.ObjectToDateTime(reader["v4last"]);
            oldseedinfo.IPv6IP = reader["ipv6ip"].ToString().Trim();
            oldseedinfo.v6Last = TypeConverter.ObjectToDateTime(reader["v6last"]);
            oldseedinfo.IPv6IPAdd = reader["ipv6ip"].ToString().Trim();
            oldseedinfo.Port = TypeConverter.ObjectToInt(reader["port"], -1);
            oldseedinfo.Percentage = TypeConverter.ObjectToFloat(reader["percentage"]);
            oldseedinfo.Upload = TypeConverter.ObjectToDecimal(reader["upload"], -1);
            oldseedinfo.UploadSpeed = TypeConverter.ObjectToFloat(reader["uploadspeed"], -1);
            oldseedinfo.Download = TypeConverter.ObjectToDecimal(reader["download"], -1);
            oldseedinfo.DownloadSpeed = TypeConverter.ObjectToFloat(reader["downloadspeed"], -1);
            oldseedinfo.FirstTime = TypeConverter.ObjectToDateTime(reader["firsttime"], DateTime.MinValue);
            oldseedinfo.LastTime = TypeConverter.ObjectToDateTime(reader["lasttime"], DateTime.MinValue);
            oldseedinfo.KeepTime = TypeConverter.ObjectToInt(reader["keeptime"], -1);

            oldseedinfo.Client = reader["client"].ToString();
            oldseedinfo.TotalUpload = TypeConverter.ObjectToDecimal(reader["totalupload"], -1);
            oldseedinfo.TotalDownload = TypeConverter.ObjectToDecimal(reader["totaldownload"], -1);

            oldseedinfo.IPRegionInBuaa = (PTsIpRegionInBuaa)TypeConverter.StrToInt(reader["ipregioninbuaa"].ToString(), 0);
            oldseedinfo.LastSend = TypeConverter.StrToInt(reader["lastsend"].ToString(), 0);

            oldseedinfo.Left = TypeConverter.ObjectToDecimal(reader["left"], -1);

            return oldseedinfo;
        }
        /// <summary>
        /// 获得指定种子的IPv6上传数
        /// </summary>
        /// <returns></returns>
        public static int GetPeerSeedIPv6UploadCount(int seedid)
        {
            return DatabaseProvider.GetInstance().GetPeerSeedIPv6UploadCount(seedid);
        }
        ///// <summary>
        ///// 删除指定IP正在下载指定种子的peer信息，只删除正在下载的
        ///// //此函数有问题，勿使用
        ///// </summary>
        ///// <param name="ip"></param>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //public static int DeletePeerInfo(string ip, int seedid)
        //{
        //    return DatabaseProvider.GetInstance().DeletePeerInfo(ip, seedid);
        //}
        /// <summary>
        /// 删除指定的peer信息，依据：seedid，peerid，uid，三要素必须相同
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int DeletePeerInfo(PrivateBTPeerInfo peerinfo)
        {
            try
            {
                //正常删除Peer，history中标记为正常退出
                return DatabaseProvider.GetInstance().DeletePeerInfo(peerinfo);
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.PeerDelete, PTLog.LogStatus.Exception, "DELETE PEER", string.Format("删除PEER异常：UID:{0} SEEDID:{1} PID:{2} EX:{3}", peerinfo.Uid, peerinfo.SeedId, peerinfo.Pid, ex.ToString()));
                return -1;
            }
            
        }
        ///// <summary>
        ///// 锁定指定的peer信息，判断信息：Seedid、uid、peerid三者相同
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public static int LockPeer(PrivateBTPeerInfo peerinfo)
        //{
        //    return DatabaseProvider.GetInstance().LockPeer(peerinfo);
        //}
        ///// <summary>
        ///// 锁定指定的peer信息
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public static int UnLockPeer(PrivateBTPeerInfo peerinfo)
        //{
        //    return DatabaseProvider.GetInstance().UnLockPeer(peerinfo);
        //}
        /// <summary>
        /// 删除指定UID正在下载指定种子的peer信息，只删除正在下载的
        /// </summary>
        /// <returns></returns>
        public static int DeletePeerInfo(int uid, int seedid)
        {
            return DatabaseProvider.GetInstance().DeletePeerInfo(uid, seedid);
        }
        /// <summary>
        /// 重置正在上传下载列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int ResetPeer(int userid)
        {
            PTUserInfo btuserinfo = PTUsers.GetBtUserInfoForPagebase(userid);
            if (btuserinfo == null || btuserinfo.Uid < 1) return 0;
            
            //重置的内容
            btuserinfo.UploadCount = 0;
            btuserinfo.DownloadCount = 0;
            
            //增量数值清零
            btuserinfo.Extcredits3 = 0M;
            btuserinfo.Extcredits4 = 0M;
            btuserinfo.Extcredits5 = 0M;
            btuserinfo.Extcredits6 = 0M;
            btuserinfo.Extcredits7 = 0M;
            btuserinfo.Extcredits8 = 0M;
            btuserinfo.Extcredits9 = 0M;
            btuserinfo.Extcredits10 = 0M;
            btuserinfo.Extcredits11 = 0M;
            btuserinfo.Extcredits12 = 0M;
            btuserinfo.FinishCount = 0;
            
            PTUsers.UpdateUserInfo_Tracker(btuserinfo, false);
            return DatabaseProvider.GetInstance().ResetPeer(userid);
        }
        /// <summary>
        /// 清理Peer列表中的无响应项目
        /// </summary>
        public static void CleanPeerListNoResponse()
        {
            DateTime timelimit = DateTime.Now.AddMinutes(-26);

            ////数据库死锁的临时解决方案：停止将不更新的IPv4、IPv6地址置空。不影响系统正常工作。
            //DatabaseProvider.GetInstance().UpdatePeerIPv6NoResponse(timelimit);   //IPv6停止响应的项目                  


            PTLog.InsertSystemLogDebug(PTLog.LogType.PeerTaskEvent, PTLog.LogStatus.Normal, "CleanPeerList", "GetPeerUidListNoResponse");
            DataTable uidlist = DatabaseProvider.GetInstance().GetPeerUidListNoResponse(timelimit, 0); //所有都停止响应的项目

            PTLog.InsertSystemLogDebug(PTLog.LogType.PeerTaskEvent, PTLog.LogStatus.Normal, "CleanPeerList", "GetPeerSeedIdListNoResponse");
            DataTable seedidlist = DatabaseProvider.GetInstance().GetPeerSeedIdListNoResponse(timelimit, 0);

            PTLog.InsertSystemLogDebug(PTLog.LogType.PeerTaskEvent, PTLog.LogStatus.Normal, "CleanPeerList", "DeletePeerListNoResponse");
            DatabaseProvider.GetInstance().DeletePeerListNoResponse(timelimit, 0);

            int lastnum = 0, num = 0;

            PTLog.InsertSystemLogDebug(PTLog.LogType.PeerTaskEvent, PTLog.LogStatus.Normal, "CleanPeerList", "UpdateUserInfo_Tracker");
            foreach (DataRow dr in uidlist.Rows)
            {
                num = Convert.ToInt32(dr["uid"].ToString());
                if (num != lastnum)  //过滤掉重复项目
                {
                    lastnum = num;
                    PTUserInfo btuserinfo = PTUsers.GetBtUserInfoForPagebase(num);
                    if (btuserinfo == null || btuserinfo.Uid < 1) continue;
                    
                    //重置内容
                    btuserinfo.UploadCount = PTSeeds.GetSeedInfoCount(0, num, 1, 0, "", 0, "");
                    btuserinfo.DownloadCount = PTSeeds.GetSeedInfoCount(0, num, 2, 0, "", 0, "");

                    //增量数值清零
                    btuserinfo.Extcredits3 = 0M;
                    btuserinfo.Extcredits4 = 0M;
                    btuserinfo.Extcredits5 = 0M;
                    btuserinfo.Extcredits6 = 0M;
                    btuserinfo.Extcredits7 = 0M;
                    btuserinfo.Extcredits8 = 0M;
                    btuserinfo.Extcredits9 = 0M;
                    btuserinfo.Extcredits10 = 0M;
                    btuserinfo.Extcredits11 = 0M;
                    btuserinfo.Extcredits12 = 0M;
                    btuserinfo.FinishCount = 0;
                    PTUsers.UpdateUserInfo_Tracker(btuserinfo, false);
                }
            }
            uidlist.Dispose();
            uidlist = null;


            PTLog.InsertSystemLogDebug(PTLog.LogType.PeerTaskEvent, PTLog.LogStatus.Normal, "CleanPeerList", "UpdateSeedAnnounce");
            foreach (DataRow dr in seedidlist.Rows)
            {
                num = Convert.ToInt32(dr["seedid"].ToString());
                if (num != lastnum)  //过滤掉重复项目
                {
                    lastnum = num;
                    PTSeedinfoShort seedinfo = PTSeeds.GetSeedInfoShort(num);
                    if (seedinfo == null || seedinfo.SeedId < 1) continue;
                    seedinfo.Upload = PrivateBT.GetPeerSeedUploadCount(seedinfo.SeedId);
                    seedinfo.Download = PrivateBT.GetPeerSeedDownloadCount(seedinfo.SeedId);
                    seedinfo.IPv6 = PrivateBT.GetPeerSeedIPv6UploadCount(seedinfo.SeedId);
                    PTSeeds.UpdateSeedAnnounce(seedinfo.SeedId, seedinfo.Upload, seedinfo.Download, -1, seedinfo.IPv6);
                }
            }

            PTLog.InsertSystemLogDebug(PTLog.LogType.PeerTaskEvent, PTLog.LogStatus.Normal, "CleanPeerList", "Done!");
            seedidlist.Dispose();
            seedidlist = null;
        }


        /// <summary>
        /// 清理Peer列表中的无响应项目
        /// </summary>
        public static void CleanPeerListNoResponseCore2()
        {
            DateTime timelimit = DateTime.Now.AddMinutes(-100);

            ////数据库死锁的临时解决方案：停止将不更新的IPv4、IPv6地址置空。不影响系统正常工作。
            //DatabaseProvider.GetInstance().UpdatePeerIPv6NoResponse(timelimit);   //IPv6停止响应的项目                  

            DataTable uidlist = DatabaseProvider.GetInstance().GetPeerUidListNoResponse(timelimit, 1); //所有都停止响应的项目

            DataTable seedidlist = DatabaseProvider.GetInstance().GetPeerSeedIdListNoResponse(timelimit, 1);

            DatabaseProvider.GetInstance().DeletePeerListNoResponse(timelimit, 1);
            int lastnum = 0, num = 0;

            foreach (DataRow dr in uidlist.Rows)
            {
                num = Convert.ToInt32(dr["uid"].ToString());
                if (num != lastnum)  //过滤掉重复项目
                {
                    lastnum = num;
                    PTUserInfo btuserinfo = PTUsers.GetBtUserInfoForPagebase(num);
                    if (btuserinfo == null || btuserinfo.Uid < 1) continue;

                    //重置内容
                    btuserinfo.UploadCount = PTSeeds.GetSeedInfoCount(0, num, 1, 0, "", 0, "");
                    btuserinfo.DownloadCount = PTSeeds.GetSeedInfoCount(0, num, 2, 0, "", 0, "");

                    //增量数值清零
                    btuserinfo.Extcredits3 = 0M;
                    btuserinfo.Extcredits4 = 0M;
                    btuserinfo.Extcredits5 = 0M;
                    btuserinfo.Extcredits6 = 0M;
                    btuserinfo.Extcredits7 = 0M;
                    btuserinfo.Extcredits8 = 0M;
                    btuserinfo.Extcredits9 = 0M;
                    btuserinfo.Extcredits10 = 0M;
                    btuserinfo.Extcredits11 = 0M;
                    btuserinfo.Extcredits12 = 0M;
                    btuserinfo.FinishCount = 0;
                    PTUsers.UpdateUserInfo_Tracker(btuserinfo, false);
                }
            }
            uidlist.Dispose();
            uidlist = null;

            foreach (DataRow dr in seedidlist.Rows)
            {
                num = Convert.ToInt32(dr["seedid"].ToString());
                if (num != lastnum)  //过滤掉重复项目
                {
                    lastnum = num;
                    PTSeedinfoShort seedinfo = PTSeeds.GetSeedInfoShort(num);
                    if (seedinfo == null || seedinfo.SeedId < 1) continue;
                    seedinfo.Upload = PrivateBT.GetPeerSeedUploadCount(seedinfo.SeedId);
                    seedinfo.Download = PrivateBT.GetPeerSeedDownloadCount(seedinfo.SeedId);
                    seedinfo.IPv6 = PrivateBT.GetPeerSeedIPv6UploadCount(seedinfo.SeedId);
                    PTSeeds.UpdateSeedAnnounce(seedinfo.SeedId, seedinfo.Upload, seedinfo.Download, -1, seedinfo.IPv6);
                }
            }
            seedidlist.Dispose();
            seedidlist = null;
        }




    }
}