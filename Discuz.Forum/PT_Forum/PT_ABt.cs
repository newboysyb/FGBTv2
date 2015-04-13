using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Common.Generic;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using System.Collections;
using Discuz.Cache;

namespace Discuz.Forum
{
    public partial class PTAbt
    {
        /// <summary>
        /// IP地址是否允许访问Abt
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static bool IsIPAllowed(string ipaddress)
        {
            //if (PTTools.IsIPV6(ipaddress) && ipaddress.IndexOf("2001:da8:203") != 0 && ipaddress.IndexOf("2001:da8:ae") != 0 && ipaddress.IndexOf("::1") != 0)
            //    return false;
            //else return true;
            #if DEBUG
                if (PTTools.IsIPv6(ipaddress) && ipaddress != "::1" && ipaddress.IndexOf("2001:da8:203") != 0 && ipaddress.IndexOf("2001:da8:ae") != 0)
            #else
                if (PTTools.IsIPv6(ipaddress) && ipaddress.IndexOf("2001:da8:203") != 0 && ipaddress.IndexOf("2001:da8:ae") != 0)
            #endif
                return false;
            else return true;
        }


        /// <summary>
        /// 插入Abt种子
        /// </summary>
        /// <returns></returns>
        public static int AbtInsertSeed(string infohash, int filecount, decimal filesize, string filename, int uid)
        {
            return DatabaseProvider.GetInstance().AbtInsertSeed(infohash, filecount, filesize, filename,uid);
        }
        /// <summary>
        /// 插入Abt节点信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int AbtInsertPeer(AbtPeerInfo peerinfo)
        {
            return DatabaseProvider.GetInstance().AbtInsertPeer(peerinfo);
        }
        /// <summary>
        /// 插入Abt下载信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int AbtInsertDownload(int aid, int uid, string passkey, string infohash, int status)
        {
            try
            {
                return DatabaseProvider.GetInstance().AbtInsertDownload(aid, uid, passkey, infohash, status);
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.AbtSeedInsert, PTLog.LogStatus.Exception, "ABT_INSERT", ex.ToString());
                return -1;
            }
           
        }

        /// <summary>
        /// 插入Abt日志信息(0-50用户事件，51-100自动任务事件，101-150下载种子事件，151-200，201-255服务器内部错误)
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int AbtInsertLog(int aid, int uid, int type, string msg)
        {
            return DatabaseProvider.GetInstance().AbtInsertLog(aid, uid, type, msg);
        }



        //////////////////////////////////////////////////////////////////////////


        public static AbtSeedInfo LoadAbtSeedInfo(IDataReader rd)
        {
            AbtSeedInfo seedinfo = new AbtSeedInfo();

            seedinfo.Aid = TypeConverter.ObjectToInt(rd["aid"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            seedinfo.InfoHash = rd["infohash"].ToString().Trim();
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.FileName = rd["filename"].ToString().Trim();

            return seedinfo;
        }
        public static AbtSeedInfo LoadAbtSeedInfo(DataRow rd)
        {
            AbtSeedInfo seedinfo = new AbtSeedInfo();

            seedinfo.Aid = TypeConverter.ObjectToInt(rd["aid"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            seedinfo.InfoHash = "";
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.FileName = "";
            seedinfo.Dis_Size = PTTools.Upload2Str(seedinfo.FileSize);
            seedinfo.Dis_Live = PTTools.Minutes2String((DateTime.Now - seedinfo.LastLive).TotalMinutes, true);
            
            //做种人数描述html
            if (seedinfo.Upload == 0) seedinfo.Dis_Upload = "<span style=\"font-weight:bolder; color:#F00\">0</span>";
            else if (seedinfo.Upload < 5) seedinfo.Dis_Upload = "<span style=\" font-weight:bolder; color:#050\">" + seedinfo.Upload.ToString() + "</span>";
            else if (seedinfo.Upload < 10) seedinfo.Dis_Upload = "<span style=\"font-weight:bolder; color:#080\">" + seedinfo.Upload.ToString() + "</span>";
            else if (seedinfo.Upload < 20) seedinfo.Dis_Upload = "<span style=\"font-weight:bolder; color:#0b0\">" + seedinfo.Upload.ToString() + "</span>";
            else seedinfo.Dis_Upload = "<span style=\"font-weight:bolder; color:#0e0\">" + seedinfo.Upload.ToString() + "</span>";

            return seedinfo;
        }
        public static AbtDownloadInfo LoadAbtDownloadInfo(IDataReader rd)
        {
            AbtDownloadInfo downinfo = new AbtDownloadInfo();

            downinfo.Aid = TypeConverter.ObjectToInt(rd["aid"]);
            downinfo.InfoHash = rd["infohash"].ToString().Trim();
            downinfo.LastTime = TypeConverter.ObjectToDateTime(rd["lasttime"]);
            downinfo.Passkey = rd["passkey"].ToString().Trim();
            downinfo.Peerid = rd["peerid"].ToString().Trim();
            downinfo.Percentage = TypeConverter.ObjectToFloat(rd["percentage"]);
            downinfo.RecordTime = TypeConverter.ObjectToDateTime(rd["recordtime"]);
            downinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            downinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);

            return downinfo;
        }
        public static AbtPeerInfo LoadAbtPeerInfo(IDataReader rd)
        {
            AbtPeerInfo peerinfo = new AbtPeerInfo();

            peerinfo.Aid = TypeConverter.ObjectToInt(rd["aid"]);
            peerinfo.IPv4 = rd["ipv4"].ToString().Trim();
            peerinfo.IPv6 = rd["ipv6"].ToString().Trim();
            peerinfo.LastTime = TypeConverter.ObjectToDateTime(rd["lasttime"]);
            peerinfo.Peerid = rd["peerid"].ToString().Trim();
            peerinfo.Percentage = TypeConverter.ObjectToFloat(rd["percentage"]);
            peerinfo.Port = TypeConverter.ObjectToInt(rd["port"]);
            peerinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);

            return peerinfo;
        }


        /// <summary>
        /// 获取Abt种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static AbtSeedInfo AbtGetSeedInfo(int aid)
        {
            AbtSeedInfo seedinfo = new AbtSeedInfo();
            IDataReader rd = DatabaseProvider.GetInstance().AbtGetSeedInfo(aid);
            if (rd.Read())
            {
                seedinfo = LoadAbtSeedInfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }

        public static List<AbtSeedInfo> AbtGetSeedInfoList(List<TopicInfo> topiclist)
        {
            if (topiclist == null || topiclist.Count == 0) return new List<AbtSeedInfo>();

            DataTable dt = new DataTable();
            dt.Columns.Add("IntValue", typeof(int), "");
            foreach (TopicInfo topic in topiclist)
            {
                DataRow dr = dt.NewRow();
                dr["IntValue"] = -topic.SeedId;
                dt.Rows.Add(dr);
                
            }

            DataTable dt2 = DatabaseProvider.GetInstance().AbtGetSeedInfoList(dt);
            List<AbtSeedInfo> abtseedlist = new List<AbtSeedInfo>();
            foreach (DataRow dr in dt2.Rows)
            {
                AbtSeedInfo seedinfo = LoadAbtSeedInfo(dr);
                abtseedlist.Add(seedinfo);
                
            }
            //abtseedlist.Reverse();
            return abtseedlist;
        }


        /// <summary>
        /// 获取Abt节点信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static AbtPeerInfo AbtGetPeerInfo(int aid, string peerid)
        {
            AbtPeerInfo peerinfo = new AbtPeerInfo();
            IDataReader rd = DatabaseProvider.GetInstance().AbtGetPeerInfo(aid, peerid);
            if (rd.Read())
            {
                peerinfo = LoadAbtPeerInfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return peerinfo;
        }
        /// <summary>
        /// 获取Abt节点数
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public static int AbtGetPeerCount(int aid, bool isupload)
        {
            return DatabaseProvider.GetInstance().AbtGetPeerCount(aid, isupload);
        }
        /// <summary>
        /// 获取Abt下载记录
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public static AbtDownloadInfo AbtGetDownload(int aid, int uid, string passkey)
        {
            AbtDownloadInfo downinfo = new AbtDownloadInfo();
            IDataReader rd = DatabaseProvider.GetInstance().AbtGetDownload(aid, uid, passkey);
            if (rd.Read())
            {
                downinfo = LoadAbtDownloadInfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return downinfo;
        }
        /// <summary>
        /// 获取Abt下载记录
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public static AbtDownloadInfo AbtGetDownload(int aid, int uid)
        {
            AbtDownloadInfo downinfo = new AbtDownloadInfo();
            IDataReader rd = DatabaseProvider.GetInstance().AbtGetDownload(aid, uid);
            if (rd.Read())
            {
                downinfo = LoadAbtDownloadInfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return downinfo;
        }
        /// <summary>
        /// 获取Abt节点列表（IPv4、IPv6、Port列表）
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public static DataTable AbtGetPeerList(int aid, bool isupload)
        {
            return DatabaseProvider.GetInstance().AbtGetPeerList(aid, isupload);
        }

        /// <summary>
        /// 获取Abt节点列表（aid列表）
        /// </summary>
        /// <returns></returns>
        public static DataTable AbtGetPeerList(DateTime lasttime)
        {
            return DatabaseProvider.GetInstance().AbtGetPeerList(lasttime);
        }



        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 删除Abt节点
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public static void AbtDeletePeerTaskEvent()
        {
            DatabaseProvider.GetInstance().AbtDeletePeer(DateTime.Now.AddMinutes(-30));
        }
        /// <summary>
        /// 删除Abt节点
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public static int AbtDeletePeer(DateTime lasttime)
        {
            return DatabaseProvider.GetInstance().AbtDeletePeer(lasttime);
        }
        /// <summary>
        /// 删除Abt节点
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public static int AbtDeletePeer(int aid, string peerid)
        {
            return DatabaseProvider.GetInstance().AbtDeletePeer(aid, peerid);
        }
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public static int AbtDeleteDownload(DateTime lasttime)
        {
            return DatabaseProvider.GetInstance().AbtDeleteDownload(lasttime);
        }
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public static int AbtDeleteDownload(int aid)
        {
            return DatabaseProvider.GetInstance().AbtDeleteDownload(aid);
        }
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public static int AbtDeleteDownload(int aid, int uid)
        {
            return DatabaseProvider.GetInstance().AbtDeleteDownload(aid, uid);
        }

        /// <summary>
        /// 删除Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public static int AbtDeleteSeed(int aid)
        {
            return DatabaseProvider.GetInstance().AbtDeleteSeed(aid);
        }
        /// <summary>
        /// 删除Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public static void AbtDeleteSeed()
        {
            AbtDeleteSeed(DateTime.Now.AddDays(-7));
        }
        /// <summary>
        /// 删除长时间无人做种的Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public static int AbtDeleteSeed(DateTime lastlive)
        {
            //最低要求5天
            if ((DateTime.Now - lastlive).TotalDays < 5) return -1;

            DataTable dt = DatabaseProvider.GetInstance().AbtGetSeedInfoList(lastlive);
            int rt = DatabaseProvider.GetInstance().AbtDeleteSeed(lastlive);
            AbtInsertLog(-1, -1, 51, "删除最后活动" + lastlive + "之前的种子" + rt + "个");

            foreach (DataRow dr in dt.Rows)
            {
                int tid = TypeConverter.ObjectToInt(dr["tid"], -1);
                int aid = TypeConverter.ObjectToInt(dr["aid"], -1);

                AbtInsertLog(aid, -1, 51, string.Format("删除最后活动时间{0}之前的种子：AID:{1} -TID:{2}", lastlive, aid, tid));
                
                if (tid > 0)
                {
                    TopicAdmins.SetClose(tid.ToString(), 1);
                    TopicAdmins.BumpTopics(tid.ToString(), -1);
                }
            }

            return rt;
        }
        /// <summary>
        /// 删除发布后无人做种的Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public static void AbtDeleteSeedPublishNoSeed()
        {
            AbtDeleteSeedPublishNoSeed(DateTime.Now.AddHours(-12));
        }
        /// <summary>
        /// 删除发布后无人做种的Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public static int AbtDeleteSeedPublishNoSeed(DateTime lastlive)
        {

            DataTable dt = DatabaseProvider.GetInstance().AbtGetSeedInfoList(lastlive);

            foreach (DataRow dr in dt.Rows)
            {
                int tid = TypeConverter.ObjectToInt(dr["tid"], -1);
                int aid = TypeConverter.ObjectToInt(dr["aid"], -1);
                DateTime abtlastlive = TypeConverter.ObjectToDateTime(dr["lastlive"], DateTime.Now);

                if (tid > 0 && aid > 0)
                {

                    TopicInfo topic = Topics.GetTopicInfo(tid);

                    if ((abtlastlive - DateTime.Parse(topic.Postdatetime)).TotalMinutes < 5)
                    {
                        AbtDeleteSeed(aid);
                        AbtInsertLog(aid, -1, 52, string.Format("删除最后活动时间{0}之前的未做种种子：AID:{1} -TID:{2}", lastlive, aid, tid));

                        TopicAdmins.SetClose(tid.ToString(), 1);
                        TopicAdmins.BumpTopics(tid.ToString(), -1); 
                    }
                }
            }

            return 0;
        }

        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 更新Abt节点信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int AbtUpdatePeer(AbtPeerInfo peerinfo)
        {
            return DatabaseProvider.GetInstance().AbtUpdatePeer(peerinfo);
        }
        /// <summary>
        /// 更新Abt下载信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public static int AbtUpdateDownload(int aid, int uid, string passkey, int status, string peerid, float percentage)
        {
            return DatabaseProvider.GetInstance().AbtUpdateDownload(aid, uid, passkey, status, peerid, percentage);
        }
        /// <summary>
        /// 更新种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="updatelive"></param>
        /// <returns></returns>
        public static int AbtUpdateSeed(int aid, int upload, int download, bool updatelive, bool addfinished)
        {
            return DatabaseProvider.GetInstance().AbtUpdateSeed(aid, upload, download, updatelive, addfinished);
        }
    }
}
