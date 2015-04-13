using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// 服务器状态类，获得服务器当前状态
    /// </summary>
    public class PrivateBTServerStats
    {
        public int PeerCount = 0;           //节点计数
        public int DownPeerCount = 0;       //正在下载的节点计数
        public int UpPeerCount = 0;         //正在下载的节点计数
        public int OnlineUserCount = 0;     //客户端在线人数
        public int SeederCount = 0;         //正在上传人数计数
        public int LeecherCount = 0;        //正在下载人数计数
        public int OnlineSeedsCount = 0;    //在线种子计数
        public string SOnlineCountRatio = "";  //在线种子计数比例
        public decimal OnlineSize = 0M;     //在线种子容量
        public string SOnlineSize = "";
        public string SOnlineSizeRatio = "";//在线种子容量比例
        public decimal DownSpeed = 0M;      //当前下载速度
        public string SDownSpeed = "";
        public decimal AllTraffic = 0M;     //历史总流量
        public string SAllTraffic = "";
        public decimal TodayTraffic = 0M;   //今天总流量
        public string STodayTraffic = "";
        public decimal AllSize = 0M;        //总计发种容量
        public string SAllSize = "";
        public int AllSeedsCount = 0;       //总计发种计数


        public decimal StatsUpload = 0M;    //统计总上传
        public string SStatsUpload = "";
        public decimal StatsDownload = 0M;  //统计总下载
        public string SStatsDownload = "";
        public decimal StatsUploadToday = 0M;   //统计今天总上传
        public string SStatsUploadToday = "";
        public decimal StatsDownloadToday = 0M;    //统计今天总下载
        public string SStatsDownloadToday = "";
        public double StatsRatio = 0;       //统计 上传/下载
        public decimal StatsUploadAll = 0M; //包含金币的统计上传
        public string SStatsUploadAll = "";



        public DateTime LastUpdate = DateTime.Now;  //最后更新时间
        public DateTime NextUpdate = DateTime.Now;  //下次更新时间

        public PrivateBTServerStats()
        {
        }
    }
    public class PrivateBTAllStats
    {
        public string StatsValue = "";                     //HTML格式存储的统计信息
        public DateTime LastUpdate = DateTime.Now;  //最后更新时间
        public DateTime NextUpdate = DateTime.Now;  //下次更新时间
        public int UpdateLock = 0;

        public PrivateBTAllStats()
        {
        }
         ~PrivateBTAllStats()
        {
            StatsValue = "";
        }
    }
}
