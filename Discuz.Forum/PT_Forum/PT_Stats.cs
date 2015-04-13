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
        /// 获得历史总下载量,1为全部物理，2为今天物理，3为全部统计下载，4为统计今天下载，5为统计全部上传，6为统计今天上传,7为金币
        /// </summary>
        /// <returns></returns>
        public static decimal GetUserTotalDownload(int totaltype)
        {
            return DatabaseProvider.GetInstance().GetUserTotalDownload(totaltype);
        }
        /// <summary>
        /// 获得今天总下载量
        /// </summary>
        /// <returns></returns>
        public static decimal GetUserTodayDownload()
        {
            return DatabaseProvider.GetInstance().GetUserTodayDownload();
        }
        /// <summary>
        /// 更新BT服务器状态记录
        /// </summary>
        /// <returns></returns>
        public static int UpdateServerStats(PrivateBTServerStats btstats, int id)
        {
            return DatabaseProvider.GetInstance().UpdateServerStats(btstats, id);
        }
        /// <summary>
        /// 对服务器状态数值进行处理，主要是显示格式转换
        /// </summary>
        /// <param name="btstats"></param>
        public static void ProcessPrivateBTServerStats(ref PrivateBTServerStats btstats)
        {
            btstats.NextUpdate = btstats.LastUpdate.AddMinutes(2);
            btstats.SAllTraffic = PTTools.Upload2Str(btstats.AllTraffic, false);
            btstats.SOnlineSize = PTTools.Upload2Str(btstats.OnlineSize, false);
            btstats.STodayTraffic = PTTools.Upload2Str(btstats.TodayTraffic, false);
            btstats.SDownSpeed = PTTools.Upload2Str(btstats.DownSpeed, false);
            btstats.SAllSize = PTTools.Upload2Str(btstats.AllSize, false);
            btstats.SStatsUpload = PTTools.Upload2Str(btstats.StatsUpload);
            btstats.SStatsDownload = PTTools.Upload2Str(btstats.StatsDownload);
            btstats.SStatsUploadToday = PTTools.Upload2Str(btstats.StatsUploadToday);
            btstats.SStatsDownloadToday = PTTools.Upload2Str(btstats.StatsDownloadToday);
            btstats.SStatsUploadAll = PTTools.Upload2Str(btstats.StatsUploadAll);
            if (btstats.AllSeedsCount > 0) btstats.SOnlineCountRatio = (btstats.OnlineSeedsCount * 100.0 / btstats.AllSeedsCount).ToString("00.0") + "%";
            if (btstats.AllSize > 0) btstats.SOnlineSizeRatio = ((double)btstats.OnlineSize * 100.0 / (double)btstats.AllSize).ToString("00.0") + "%";
        }
        /// <summary>
        /// 获取BT服务器状态，1当前值，2历史最高值，其余为时间排序记录
        /// </summary>
        /// <returns></returns>
        public static PrivateBTServerStats GetServerStats(int id)
        {
            //if (id == 1) return GetServerStats();
            //else
            {
                PrivateBTServerStats btstats = new PrivateBTServerStats();
                IDataReader reader = DatabaseProvider.GetInstance().GetServerStatsToReader(id);
                if (reader.Read())
                {
                    btstats.LastUpdate = DateTime.Parse(reader["lastupdate"].ToString());
                    btstats.AllTraffic = decimal.Parse(reader["alltraffic"].ToString());
                    btstats.DownPeerCount = int.Parse(reader["downpeercount"].ToString());
                    btstats.UpPeerCount = int.Parse(reader["uppeercount"].ToString());
                    btstats.LeecherCount = int.Parse(reader["leechercount"].ToString());
                    btstats.SeederCount = int.Parse(reader["seedercount"].ToString());
                    btstats.PeerCount = btstats.UpPeerCount + btstats.DownPeerCount;
                    btstats.OnlineSeedsCount = int.Parse(reader["onlineseedscount"].ToString());
                    btstats.OnlineSize = decimal.Parse(reader["onlinesize"].ToString());
                    btstats.OnlineUserCount = int.Parse(reader["onlineusercount"].ToString());
                    btstats.TodayTraffic = decimal.Parse(reader["todaytraffic"].ToString());
                    btstats.DownSpeed = decimal.Parse(reader["downspeed"].ToString());
                    btstats.AllSize = decimal.Parse(reader["allsize"].ToString());
                    btstats.AllSeedsCount = int.Parse(reader["allseedscount"].ToString());
                    btstats.StatsUpload = decimal.Parse(reader["statsupload"].ToString());
                    btstats.StatsDownload = decimal.Parse(reader["statsdownload"].ToString());
                    btstats.StatsUploadToday = decimal.Parse(reader["statsuploadtoday"].ToString());
                    btstats.StatsDownloadToday = decimal.Parse(reader["statsdownloadtoday"].ToString());
                    btstats.StatsRatio = double.Parse(reader["statsratio"].ToString());
                    btstats.StatsUploadAll = decimal.Parse(reader["statsuploadall"].ToString());
                }
                reader.Close();
                reader.Dispose();
                reader = null;

                ProcessPrivateBTServerStats(ref btstats);
                return btstats;
            }
        }
                /// <summary>
        /// 获取并更新BT服务器状态的当前值，同时更新最大值
        /// </summary>
        /// <returns></returns>
        public static void UpdateServerStatsTaskEvent()
        {
            UpdateServerStats();
        }
        public static int UpdateServerStatsMoonEvent()
        {
            UpdateServerStats();
            return 1;
        }
        /// <summary>
        /// 获取并更新BT服务器状态的当前值，同时更新最大值
        /// </summary>
        /// <returns></returns>
        public static PrivateBTServerStats UpdateServerStats()
        {
            PrivateBTServerStats btstats = new PrivateBTServerStats();

            btstats.AllTraffic = GetUserTotalDownload(1);
            btstats.DownPeerCount = GetPeerCount(0, 0, true);
            btstats.UpPeerCount = GetPeerCount(0, 0, false);
            //btstats.LastUpdate = DateTime.Now;//lastupdate保留原来的值，用来确认更新，防止多次更新数值
            btstats.LeecherCount = GetPeerUserCount(1);
            btstats.SeederCount = GetPeerUserCount(2);
            btstats.PeerCount = btstats.UpPeerCount + btstats.DownPeerCount;
            btstats.OnlineSeedsCount = PTSeeds.GetSeedInfoCount(0, 0, 0, 1, "", 0, "");
            btstats.OnlineSize = PTSeeds.GetSeedSumSize(0, 0, 0, 1, "", 0, "");
            btstats.OnlineUserCount = GetPeerUserCount(0);
            btstats.TodayTraffic = GetUserTodayDownload();
            btstats.DownSpeed = GetPeerTotalSpeed();
            btstats.AllSize = PTSeeds.GetSeedSumSize(0, 0, 0, 0, "", 0, "");
            btstats.AllSeedsCount = PTSeeds.GetSeedInfoCount(0, 0, 0, 0, "", 0, "");
            btstats.StatsUpload = GetUserTotalDownload(5);
            btstats.StatsDownload = GetUserTotalDownload(3);
            btstats.StatsUploadToday = GetUserTotalDownload(6);
            btstats.StatsDownloadToday = GetUserTotalDownload(4);
            btstats.StatsRatio = (double)btstats.StatsDownload == 0 ? 0 : (double)btstats.StatsUpload / (double)btstats.StatsDownload;
            btstats.StatsUploadAll = GetUserTotalDownload(5) + 2M * GetUserTotalDownload(7);

            if (DatabaseProvider.GetInstance().UpdateServerStats(btstats, 1) > 0) //成功更新数值后再插入记录
            {
                DatabaseProvider.GetInstance().InsertServerStats(btstats, true);
            }

            PrivateBTServerStats btstatspk = GetServerStats(2);
            if (btstatspk.AllSeedsCount < btstats.AllSeedsCount) btstatspk.AllSeedsCount = btstats.AllSeedsCount;
            if (btstatspk.AllSize < btstats.AllSize) btstatspk.AllSize = btstats.AllSize;
            if (btstatspk.AllTraffic < btstats.AllTraffic) btstatspk.AllTraffic = btstats.AllTraffic;
            if (btstatspk.DownPeerCount < btstats.DownPeerCount) btstatspk.DownPeerCount = btstats.DownPeerCount;
            if (btstatspk.DownSpeed < btstats.DownSpeed) btstatspk.DownSpeed = btstats.DownSpeed;
            btstatspk.LastUpdate = btstats.LastUpdate;
            if (btstatspk.LeecherCount < btstats.LeecherCount) btstatspk.LeecherCount = btstats.LeecherCount;
            btstatspk.NextUpdate = btstats.NextUpdate;
            if (btstatspk.OnlineSeedsCount < btstats.OnlineSeedsCount) btstatspk.OnlineSeedsCount = btstats.OnlineSeedsCount;
            if (btstatspk.OnlineSize < btstats.OnlineSize) btstatspk.OnlineSize = btstats.OnlineSize;
            if (btstatspk.OnlineUserCount < btstats.OnlineUserCount) btstatspk.OnlineUserCount = btstats.OnlineUserCount;
            if (btstatspk.PeerCount < btstats.PeerCount) btstatspk.PeerCount = btstats.PeerCount;
            if (btstatspk.SeederCount < btstats.SeederCount) btstatspk.SeederCount = btstats.SeederCount;
            if (btstatspk.StatsDownload < btstats.StatsDownload) btstatspk.StatsDownload = btstats.StatsDownload;
            if (btstatspk.StatsDownloadToday < btstats.StatsDownloadToday) btstatspk.StatsDownloadToday = btstats.StatsDownloadToday;
            if (btstatspk.StatsRatio > btstats.StatsRatio) btstatspk.StatsRatio = btstats.StatsRatio;
            if (btstatspk.StatsUpload < btstats.StatsUpload) btstatspk.StatsUpload = btstats.StatsUpload;
            if (btstatspk.StatsUploadAll < btstats.StatsUploadAll) btstatspk.StatsUploadAll = btstats.StatsUploadAll;
            if (btstatspk.StatsUploadToday < btstats.StatsUploadToday) btstatspk.StatsUploadToday = btstats.StatsUploadToday;
            if (btstatspk.TodayTraffic < btstats.TodayTraffic) btstatspk.TodayTraffic = btstats.TodayTraffic;
            if (btstatspk.UpPeerCount < btstats.UpPeerCount) btstatspk.UpPeerCount = btstats.UpPeerCount;
            DatabaseProvider.GetInstance().UpdateServerStats(btstatspk, 2);

            ProcessPrivateBTServerStats(ref btstats);
            return btstats;
        }
        /// <summary>
        /// 获取BT服务器状态 当前值，此函数调用GetServerStats(false);
        /// </summary>
        /// <returns></returns>
        public static PrivateBTServerStats GetServerStats()
        {
            return GetServerStats(false);
        }
        /// <summary>
        /// 获取BT服务器状态 当前值
        /// </summary>
        /// <returns></returns>
        public static PrivateBTServerStats GetServerStats(bool forceupdate)
        {
            //bool statslock = false;

            PrivateBTServerStats btstats = GetServerStats(1);
            
            if(((DateTime.Now - btstats.LastUpdate).TotalSeconds > 300 && forceupdate) || (DateTime.Now - btstats.LastUpdate).TotalSeconds > 600)
            {
                btstats = UpdateServerStats();
            }

            //if(((DateTime.Now - btstats.LastUpdate).TotalSeconds < 240 || !statslock) && !forceupdate && (DateTime.Now - btstats.LastUpdate).TotalSeconds < 480)
            //IDataReader reader = DatabaseProvider.GetInstance().GetServerStatsToReader(1);
            //if (DatabaseProvider.GetInstance().LockServerStats(1) > 0) statslock = true;
            //if (reader.Read())
            //{
            //    btstats.LastUpdate = DateTime.Parse(reader["lastupdate"].ToString());
            //    //<180，不是强制，这两条是必须的。
            //    if ()
            //    {
            //        btstats.AllTraffic = decimal.Parse(reader["alltraffic"].ToString());
            //        btstats.DownPeerCount = int.Parse(reader["downpeercount"].ToString());
            //        btstats.UpPeerCount = int.Parse(reader["uppeercount"].ToString());
            //        btstats.LeecherCount = int.Parse(reader["leechercount"].ToString());
            //        btstats.SeederCount = int.Parse(reader["seedercount"].ToString());
            //        btstats.PeerCount = btstats.UpPeerCount + btstats.DownPeerCount;
            //        btstats.OnlineSeedsCount = int.Parse(reader["onlineseedscount"].ToString());
            //        btstats.OnlineSize = decimal.Parse(reader["onlinesize"].ToString());
            //        btstats.OnlineUserCount = int.Parse(reader["onlineusercount"].ToString());
            //        btstats.TodayTraffic = decimal.Parse(reader["todaytraffic"].ToString());
            //        btstats.DownSpeed = decimal.Parse(reader["downspeed"].ToString());
            //        btstats.AllSize = decimal.Parse(reader["allsize"].ToString());
            //        btstats.AllSeedsCount = int.Parse(reader["allseedscount"].ToString());
            //        btstats.StatsUpload = decimal.Parse(reader["statsupload"].ToString());
            //        btstats.StatsDownload = decimal.Parse(reader["statsdownload"].ToString());
            //        btstats.StatsUploadToday = decimal.Parse(reader["statsuploadtoday"].ToString());
            //        btstats.StatsDownloadToday = decimal.Parse(reader["statsdownloadtoday"].ToString());
            //        btstats.StatsRatio = double.Parse(reader["statsratio"].ToString());
            //        btstats.StatsUploadAll = decimal.Parse(reader["statsuploadall"].ToString());
            //    }
            //    else
            //    {
            //        statslock = true; //强制等会儿取消锁定，防止错误发生
            //        btstats.AllTraffic = GetUserTotalDownload(1);
            //        btstats.DownPeerCount = GetPeerCount(0, "", 0, true);
            //        btstats.UpPeerCount = GetPeerCount(0, "", 0, false);
            //        //btstats.LastUpdate = DateTime.Now;//lastupdate保留原来的值，用来确认更新，防止多次更新数值
            //        btstats.LeecherCount = GetPeerUserCount(1);
            //        btstats.SeederCount = GetPeerUserCount(2);
            //        btstats.PeerCount = btstats.UpPeerCount + btstats.DownPeerCount;
            //        btstats.OnlineSeedsCount = PTSeeds.GetSeedInfoCount(0, 0, 0, 1, "");
            //        btstats.OnlineSize = PTSeeds.GetSeedSumSize(0, 0, 0, 1, "");
            //        btstats.OnlineUserCount = GetPeerUserCount(0);
            //        btstats.TodayTraffic = GetUserTodayDownload();
            //        btstats.DownSpeed = GetPeerTotalSpeed();
            //        btstats.AllSize = PTSeeds.GetSeedSumSize(0, 0, 0, 0, "");
            //        btstats.AllSeedsCount = PTSeeds.GetSeedInfoCount(0, 0, 0, 0, "");
            //        btstats.StatsUpload = GetUserTotalDownload(5);
            //        btstats.StatsDownload = GetUserTotalDownload(3);
            //        btstats.StatsUploadToday = GetUserTotalDownload(6);
            //        btstats.StatsDownloadToday = GetUserTotalDownload(4);
            //        btstats.StatsRatio = (double)btstats.StatsDownload == 0 ? 0 : (double)btstats.StatsUpload / (double)btstats.StatsDownload;
            //        btstats.StatsUploadAll = GetUserTotalDownload(5) + 2M * GetUserTotalDownload(7);

            //        if (DatabaseProvider.GetInstance().UpdateServerStats(btstats, 1) > 0) //成功更新数值后再插入记录
            //        {
            //            if (forceupdate) DatabaseProvider.GetInstance().InsertServerStats(btstats, true);
            //            else DatabaseProvider.GetInstance().InsertServerStats(btstats);
            //        }

            //        PrivateBTServerStats btstatspk = GetServerStats(2);
            //        if (btstatspk.AllSeedsCount < btstats.AllSeedsCount) btstatspk.AllSeedsCount = btstats.AllSeedsCount;
            //        if (btstatspk.AllSize < btstats.AllSize) btstatspk.AllSize = btstats.AllSize;
            //        if (btstatspk.AllTraffic < btstats.AllTraffic) btstatspk.AllTraffic = btstats.AllTraffic;
            //        if (btstatspk.DownPeerCount < btstats.DownPeerCount) btstatspk.DownPeerCount = btstats.DownPeerCount;
            //        if (btstatspk.DownSpeed < btstats.DownSpeed) btstatspk.DownSpeed = btstats.DownSpeed;
            //        btstatspk.LastUpdate = btstats.LastUpdate;
            //        if (btstatspk.LeecherCount < btstats.LeecherCount) btstatspk.LeecherCount = btstats.LeecherCount;
            //        btstatspk.NextUpdate = btstats.NextUpdate;
            //        if (btstatspk.OnlineSeedsCount < btstats.OnlineSeedsCount) btstatspk.OnlineSeedsCount = btstats.OnlineSeedsCount;
            //        if (btstatspk.OnlineSize < btstats.OnlineSize) btstatspk.OnlineSize = btstats.OnlineSize;
            //        if (btstatspk.OnlineUserCount < btstats.OnlineUserCount) btstatspk.OnlineUserCount = btstats.OnlineUserCount;
            //        if (btstatspk.PeerCount < btstats.PeerCount) btstatspk.PeerCount = btstats.PeerCount;
            //        if (btstatspk.SeederCount < btstats.SeederCount) btstatspk.SeederCount = btstats.SeederCount;
            //        if (btstatspk.StatsDownload < btstats.StatsDownload) btstatspk.StatsDownload = btstats.StatsDownload;
            //        if (btstatspk.StatsDownloadToday < btstats.StatsDownloadToday) btstatspk.StatsDownloadToday = btstats.StatsDownloadToday;
            //        if (btstatspk.StatsRatio > btstats.StatsRatio) btstatspk.StatsRatio = btstats.StatsRatio;
            //        if (btstatspk.StatsUpload < btstats.StatsUpload) btstatspk.StatsUpload = btstats.StatsUpload;
            //        if (btstatspk.StatsUploadAll < btstats.StatsUploadAll) btstatspk.StatsUploadAll = btstats.StatsUploadAll;
            //        if (btstatspk.StatsUploadToday < btstats.StatsUploadToday) btstatspk.StatsUploadToday = btstats.StatsUploadToday;
            //        if (btstatspk.TodayTraffic < btstats.TodayTraffic) btstatspk.TodayTraffic = btstats.TodayTraffic;
            //        if (btstatspk.UpPeerCount < btstats.UpPeerCount) btstatspk.UpPeerCount = btstats.UpPeerCount;
            //        DatabaseProvider.GetInstance().UpdateServerStats(btstatspk, 2);
            //        btstats.LastUpdate = DateTime.Now;
            //    }

            //}
            //reader.Close();
            //reader.Dispose();
            //reader = null;

            //if (statslock) DatabaseProvider.GetInstance().UnLockServerStats(1);

            return btstats;
        }
    }
}