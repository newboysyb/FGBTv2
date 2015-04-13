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
    public partial class PTRss
    {
        /// <summary>
        /// 添加RSS记录
        /// </summary>
        /// <returns>数据库更改行数</returns>
        public static int AddSeedRss(PTSeedRssinfo rssinfo)
        {
            try
            {
                int rtvalue = DatabaseProvider.GetInstance().AddSeedRss(rssinfo);
                PrivateBT.InsertSeedModLog(rssinfo.Seedid, "添加到RSS_ACC", "系统", "", 0, 101);
                return rtvalue;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return -1;
            }
            
        }


        /// <summary>
        /// 添加RSS记录
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <param name="rsstype"></param>
        /// <param name="rsscondition"></param>
        /// <returns></returns>
        public static int AddSeedRss(PTSeedinfo seedinfo, int rsstype, int rsscondition)
        {
            if (seedinfo.SeedId < 1 || seedinfo.TopicId < 1) return -1;

            PTSeedRssinfo rssinfo = new PTSeedRssinfo();
            rssinfo.Seedid = seedinfo.SeedId;
            rssinfo.AddDateTime = DateTime.Now;
            rssinfo.RssStatus = 1;
            rssinfo.RssType = rsstype;
            rssinfo.RssCondition = rsscondition;
            rssinfo.Topicid = seedinfo.TopicId;
            rssinfo.LastUpdated = DateTime.Now;
            rssinfo.SeedType = seedinfo.Type;
            rssinfo.SeedStatus = seedinfo.Status;
            rssinfo.SeedTitle = seedinfo.TopicTitle;
            rssinfo.SeedSize = seedinfo.FileSize;

            //重复条目，暂不继续添加
            if (IsExistsSeedRss(seedinfo.SeedId, rsstype))
            {
                if(IsExistsSeedRss(seedinfo.SeedId, rsstype, DateTime.Now.AddDays(-30))) return -2;
                else
                {
                    //两个月之前存在，则删除以前的条目，继续添加新条目
                    DeleteSeedRss(seedinfo.SeedId, rsstype);
                    PrivateBT.InsertSeedModLog(seedinfo.SeedId, "从RSS_ACC中删除", "系统", "", 0, 102);
                }
            }

            int rtvalue = AddSeedRss(rssinfo);
            if (rtvalue > 0)
            {
                //种子列表部分更新
                PTSeeds.UpdateSeedbyRssType(seedinfo.SeedId, rsstype, 1);
            }

            return rtvalue;
        }

        /// <summary>
        /// 添加RSS记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="rsscondition"></param>
        /// <returns></returns>
        public static int AddSeedRss(int seedid, int rsstype, int rsscondition)
        {
            if(seedid < 1) return -1;
            PTSeedinfo seedinfo = PTSeeds.GetSeedInfo(seedid);
            if (seedinfo.SeedId > 0 && seedinfo.Status == 2)
                return AddSeedRss(seedinfo, rsstype, rsscondition);
            else return -1;
        }


        /// <summary>
        /// 更新RSS条目
        /// </summary>
        /// <param name="rssinfo"></param>
        /// <returns></returns>
        public static int UpdateSeedRss(PTSeedRssinfo rssinfo)
        {
            if (rssinfo.Seedid < 1 || rssinfo.Topicid < 1) return -1;
            return DatabaseProvider.GetInstance().UpdateSeedRss(rssinfo);
        }

        /// <summary>
        /// 删除RSS条目
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <returns></returns>
        public static int DeleteSeedRss(int seedid, int rsstype)
        {
            if (seedid < 1 || rsstype < 1) return -1;
            return DatabaseProvider.GetInstance().DeleteSeedRss(seedid, rsstype);
        }

        /// <summary>
        /// 判断RSS条目是否存在
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="adddatetime"></param>
        /// <returns></returns>
        public static bool IsExistsSeedRss(int seedid, int rsstype, DateTime adddatetime)
        {
            if (seedid < 1 || rsstype < 1) return false;
            return DatabaseProvider.GetInstance().IsExistsSeedRss(seedid, rsstype, adddatetime);
        }
        /// <summary>
        /// 判断RSS条目是否存在
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="adddatetime"></param>
        /// <returns></returns>
        public static bool IsExistsSeedRss(int seedid, int rsstype)
        {
            if (seedid < 1 || rsstype < 1) return false;
            return DatabaseProvider.GetInstance().IsExistsSeedRss(seedid, rsstype, DateTime.Now.AddDays(-30));
        }
        /// <summary>
        ///  获取热门下载种子RSS_ACC_SH列表
        /// </summary>
        /// <param name="maxlogcount"></param>
        /// <param name="mindownload"></param>
        /// <returns></returns>
        public static DataTable GetSeedRssListHotDownload(int maxlogcount, int mindownload)
        {
            if(maxlogcount < 100 || maxlogcount > 10000) maxlogcount = 2000;
            if (mindownload < 3) mindownload = 3;
            return DatabaseProvider.GetInstance().GetSeedRssListHotDownload(maxlogcount, mindownload);
        }
        /// <summary>
        /// 获取热门老种RSS_OLDHOT列表
        /// </summary>
        /// <param name="downloadcountlimit"></param>
        /// <param name="dayscheck"></param>
        /// <param name="daysbefore"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public static DataTable GetSeedRssListOldHot(int downloadcountlimit, int dayscheck, int daysbefore, int totalcount)
        {
            if (downloadcountlimit < 5) downloadcountlimit = 5;
            if (totalcount > 3000) totalcount = 3000;
            return DatabaseProvider.GetInstance().GetSeedRssListOldHot(downloadcountlimit, dayscheck, daysbefore, totalcount);
        }
        /// <summary>
        /// 获取热门老种RSS_OLDHOT列表
        /// </summary>
        /// <param name="downloadcountlimit"></param>
        /// <param name="dayscheck"></param>
        /// <param name="daysbefore"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public static DataTable GetSeedRssListOldHotNMB(int downloadcountlimit, int dayscheck, int daysbefore, int totalcount)
        {
            if (downloadcountlimit < 5) downloadcountlimit = 5;
            if (totalcount > 3000) totalcount = 3000;
            return DatabaseProvider.GetInstance().GetSeedRssListOldHotNMB(downloadcountlimit, dayscheck, daysbefore, totalcount);
        }

        /// <summary>
        /// 获取热门保种RSS_KEEPHOT列表
        /// </summary>
        /// <param name="downloadcountlimit"></param>
        /// <param name="dayscheck"></param>
        /// <param name="daysbefore"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public static DataTable GetSeedRssListKeepHot(int downloadcountlimit, int dayscheck, int daysbefore, int daysafter, int totalcount)
        {
            if (downloadcountlimit < 5) downloadcountlimit = 5;
            if (totalcount > 3000) totalcount = 3000;
            return DatabaseProvider.GetInstance().GetSeedRssListKeepHot(downloadcountlimit, dayscheck, daysbefore, daysafter, totalcount);
        }

        public static PTSeedRssinfo LoadSingleSeedRssinfo(DataRow dr)
        {
            PTSeedRssinfo rssinfo = new PTSeedRssinfo();
            if (dr != null)
            {
                rssinfo.Rssid = TypeConverter.ObjectToInt(dr["rssid"]);
                rssinfo.Seedid = TypeConverter.ObjectToInt(dr["seedid"]);
                rssinfo.AddDateTime = TypeConverter.ObjectToDateTime(dr["adddatetime"]);
                rssinfo.RssStatus = TypeConverter.ObjectToInt(dr["rssstatus"]);
                rssinfo.RssType = TypeConverter.ObjectToInt(dr["rsstype"]);
                rssinfo.RssCondition = TypeConverter.ObjectToInt(dr["rsscondition"]);
                rssinfo.Topicid = TypeConverter.ObjectToInt(dr["topicid"]);
                //rssinfo.LastUpdated = TypeConverter.ObjectToDateTime(dr["lastupdated"]);
                rssinfo.SeedType = TypeConverter.ObjectToInt(dr["type"]);
                rssinfo.SeedStatus = TypeConverter.ObjectToInt(dr["status"]);
                rssinfo.SeedTitle = dr["topictitle"].ToString();
                rssinfo.SeedSize = TypeConverter.ObjectToDecimal(dr["filesize"]);
            }
            return rssinfo;
        }

        /// <summary>
        /// 获取SeedRSS表
        /// </summary>
        /// <param name="numperpage"></param>
        /// <param name="pageindex"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public static List<PTSeedRssinfo> GetSeedRssListbyType(int numperpage, int pageindex, int seedtype, int rsstype)
        {
            if (numperpage < 10 || numperpage > 200) numperpage = 10;
            if (pageindex < 1) pageindex = 1;
            if (seedtype < 0) seedtype = 0;
            if (rsstype < 1) rsstype = 0;

            List<PTSeedRssinfo> rsslist = new List<PTSeedRssinfo>();
            DataTable listtable = DatabaseProvider.GetInstance().GetSeedRssListbyType(numperpage, pageindex, seedtype, rsstype);
            
            if (listtable != null)
            {
                foreach(DataRow dr in listtable.Rows)
                {
                    PTSeedRssinfo drinfo = LoadSingleSeedRssinfo(dr);
                    if (drinfo.Rssid > 0)
                    {
                        rsslist.Add(drinfo);
                    }
                }
            }
            listtable.Dispose();
            return rsslist;
        }

        /// <summary>
        /// 获取SeedRSS 总数
        /// </summary>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public static int GetSeedRssCountbyType(int seedtype, int rsstype)
        {
            if (seedtype < 0) seedtype = 0;
            if (rsstype < 1) rsstype = 0;
            return DatabaseProvider.GetInstance().GetSeedRssCountbyType(seedtype, rsstype);
        }

    }
}
