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
    /// <summary>
    /// PT，种子信息处理类
    /// </summary>
    public class PTSeeds
    {
        /// <summary>
        /// 更新种子流量日志表
        /// </summary>
        /// <returns></returns>
        public static void UpdateSeedTrafficLog()
        {
            DatabaseProvider.GetInstance().UpdateSeedTrafficLog();
        }

        /// <summary>
        /// 从seedinfo加载到taginfo
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public static PT_SeedTagInfo LoadTagInfofromSeedInfo(PTSeedinfo seedinfo)
        {
            PT_SeedTagInfo taginfo = new PT_SeedTagInfo();
            taginfo.TYPE = "";
            switch (seedinfo.Type)
            {
                case 1:
                    {
                        taginfo.Movie_1 = seedinfo.Info1;
                        taginfo.Movie_2 = seedinfo.Info2;
                        taginfo.Movie_3 = seedinfo.Info3;
                        taginfo.Movie_4 = seedinfo.Info4;
                        taginfo.Movie_5 = seedinfo.Info5;
                        taginfo.Movie_6 = seedinfo.Info6;
                        taginfo.Movie_7 = seedinfo.Info7;
                        taginfo.Movie_8 = seedinfo.Info8;
                        taginfo.Movie_9 = seedinfo.Info9;
                        taginfo.Movie_10 = seedinfo.Info10;
                        taginfo.Movie_11 = seedinfo.Info11;
                        taginfo.Movie_12 = seedinfo.Info12;
                        taginfo.Movie_13 = seedinfo.Info13;
                        taginfo.Movie_14 = seedinfo.Info14;
                        break;
                    }
                case 2:
                    {
                        taginfo.Tv_1 = seedinfo.Info1;
                        taginfo.Tv_2 = seedinfo.Info2;
                        taginfo.Tv_3 = seedinfo.Info3;
                        taginfo.Tv_4 = seedinfo.Info4;
                        taginfo.Tv_5 = seedinfo.Info5;
                        taginfo.Tv_6 = seedinfo.Info6;
                        taginfo.Tv_7 = seedinfo.Info7;
                        break;
                    }
                case 3:
                    {
                        taginfo.Comic_1 = seedinfo.Info1;
                        taginfo.Comic_2 = seedinfo.Info2;
                        taginfo.Comic_3 = seedinfo.Info3;
                        taginfo.Comic_4 = seedinfo.Info4;
                        taginfo.Comic_5 = seedinfo.Info5;
                        taginfo.Comic_6 = seedinfo.Info6;
                        taginfo.Comic_7 = seedinfo.Info7;
                        taginfo.Comic_8 = seedinfo.Info8;
                        taginfo.Comic_9 = seedinfo.Info9;
                        taginfo.Comic_10 = seedinfo.Info10;
                        taginfo.Comic_11 = seedinfo.Info11;
                        break;
                    }
                case 4:
                    {
                        taginfo.Music_1 = seedinfo.Info1;
                        taginfo.Music_2 = seedinfo.Info2;
                        taginfo.Music_3 = seedinfo.Info3;
                        taginfo.Music_4 = seedinfo.Info4;
                        taginfo.Music_5 = seedinfo.Info5;
                        taginfo.Music_6 = seedinfo.Info6;
                        taginfo.Music_7 = seedinfo.Info7;
                        taginfo.Music_8 = seedinfo.Info8;
                        taginfo.Music_9 = seedinfo.Info9;
                        break;
                    }
                case 5:
                    {
                        taginfo.Game_1 = seedinfo.Info1;
                        taginfo.Game_2 = seedinfo.Info2;
                        taginfo.Game_3 = seedinfo.Info3;
                        taginfo.Game_4 = seedinfo.Info4;
                        taginfo.Game_5 = seedinfo.Info5;
                        taginfo.Game_6 = seedinfo.Info6;
                        taginfo.Game_7 = seedinfo.Info7;
                        taginfo.Game_8 = seedinfo.Info8;
                        break;
                    }
                case 6:
                    {
                        //纪录
                        taginfo.Discovery_1 = seedinfo.Info1;
                        taginfo.Discovery_2 = seedinfo.Info2;
                        taginfo.Discovery_3 = seedinfo.Info3;
                        taginfo.Discovery_4 = seedinfo.Info4;
                        taginfo.Discovery_5 = seedinfo.Info5;
                        taginfo.Discovery_6 = seedinfo.Info6;
                        taginfo.Discovery_7 = seedinfo.Info7;
                        break;
                    }
                case 7:
                    {
                        taginfo.Sport_1 = seedinfo.Info1;
                        taginfo.Sport_2 = seedinfo.Info2;
                        taginfo.Sport_3 = seedinfo.Info3;
                        taginfo.Sport_4 = seedinfo.Info4;
                        taginfo.Sport_5 = seedinfo.Info5;
                        taginfo.Sport_6 = seedinfo.Info6;
                        taginfo.Sport_7 = seedinfo.Info7;
                        taginfo.Sport_8 = seedinfo.Info8;
                        break;
                    }
                case 8:
                    {
                        taginfo.Entertainment_1 = seedinfo.Info1;
                        taginfo.Entertainment_2 = seedinfo.Info2;
                        taginfo.Entertainment_3 = seedinfo.Info3;
                        taginfo.Entertainment_4 = seedinfo.Info4;
                        taginfo.Entertainment_5 = seedinfo.Info5;
                        taginfo.Entertainment_6 = seedinfo.Info6;
                        taginfo.Entertainment_7 = seedinfo.Info7;
                        taginfo.Entertainment_8 = seedinfo.Info8;
                        taginfo.Entertainment_9 = seedinfo.Info9;
                        break;
                    }
                case 9:
                    {
                        taginfo.Software_1 = seedinfo.Info1;
                        taginfo.Software_2 = seedinfo.Info2;
                        taginfo.Software_3 = seedinfo.Info3;
                        taginfo.Software_4 = seedinfo.Info4;
                        taginfo.Software_5 = seedinfo.Info5;
                        taginfo.Software_6 = seedinfo.Info6;
                        break;
                    }
                case 10:
                    {
                        taginfo.Staff_1 = seedinfo.Info1;
                        taginfo.Staff_2 = seedinfo.Info2;
                        taginfo.Staff_3 = seedinfo.Info3;
                        taginfo.Staff_4 = seedinfo.Info4;
                        taginfo.Staff_5 = seedinfo.Info5;
                        taginfo.Staff_6 = seedinfo.Info6;
                        break;
                    }
                case 11:
                    {
                        taginfo.Video_1 = seedinfo.Info1;
                        taginfo.Video_2 = seedinfo.Info2;
                        taginfo.Video_3 = seedinfo.Info3;
                        taginfo.Video_4 = seedinfo.Info4;
                        taginfo.Video_5 = seedinfo.Info5;
                        taginfo.Video_6 = seedinfo.Info6;
                        taginfo.Video_7 = seedinfo.Info7;
                        taginfo.Video_8 = seedinfo.Info8;
                        break;
                    }
                case 12:
                    {
                        taginfo.Other_1 = seedinfo.Info1;
                        taginfo.Other_2 = seedinfo.Info2;
                        taginfo.Other_3 = seedinfo.Info3;
                        taginfo.Other_4 = seedinfo.Info4;
                        taginfo.Other_5 = seedinfo.Info5;
                        break;
                    }
                default:
                    return taginfo;
            }
            taginfo.TYPE = PrivateBT.Type2Str(seedinfo.Type);
            return taginfo;
        }

        /// <summary>
        /// 从taginfo加载到seedinfo
        /// </summary>
        /// <param name="taginfo"></param>
        /// <returns></returns>
        public static void LoadSeedInfofromTagInfo(PT_SeedTagInfo taginfo, ref PTSeedinfo seedinfo)
        {
            switch (seedinfo.Type)
            {
                case 1:
                    {
                        seedinfo.Info1 = taginfo.Movie_1;
                        seedinfo.Info2 = taginfo.Movie_2;
                        seedinfo.Info3 = taginfo.Movie_3;
                        seedinfo.Info4 = taginfo.Movie_4;
                        seedinfo.Info5 = taginfo.Movie_5;
                        seedinfo.Info6 = taginfo.Movie_6;
                        seedinfo.Info7 = taginfo.Movie_7;
                        seedinfo.Info8 = taginfo.Movie_8;
                        seedinfo.Info9 = taginfo.Movie_9;
                        seedinfo.Info10 = taginfo.Movie_10;
                        seedinfo.Info11 = taginfo.Movie_11;
                        seedinfo.Info12 = taginfo.Movie_12;
                        seedinfo.Info13 = taginfo.Movie_13;
                        seedinfo.Info14 = taginfo.Movie_14;
                        break;
                    }
                case 2:
                    {
                        seedinfo.Info1 = taginfo.Tv_1;
                        seedinfo.Info2 = taginfo.Tv_2;
                        seedinfo.Info3 = taginfo.Tv_3;
                        seedinfo.Info4 = taginfo.Tv_4;
                        seedinfo.Info5 = taginfo.Tv_5;
                        seedinfo.Info6 = taginfo.Tv_6;
                        seedinfo.Info7 = taginfo.Tv_7;
                        break;
                    }
                case 3:
                    {
                        seedinfo.Info1 = taginfo.Comic_1;
                        seedinfo.Info2 = taginfo.Comic_2;
                        seedinfo.Info3 = taginfo.Comic_3;
                        seedinfo.Info4 = taginfo.Comic_4;
                        seedinfo.Info5 = taginfo.Comic_5;
                        seedinfo.Info6 = taginfo.Comic_6;
                        seedinfo.Info7 = taginfo.Comic_7;
                        seedinfo.Info8 = taginfo.Comic_8;
                        seedinfo.Info9 = taginfo.Comic_9;
                        seedinfo.Info10 = taginfo.Comic_10;
                        seedinfo.Info11 = taginfo.Comic_11;
                        break;
                    }
                case 4:
                    {
                        seedinfo.Info1 = taginfo.Music_1;
                        seedinfo.Info2 = taginfo.Music_2;
                        seedinfo.Info3 = taginfo.Music_3;
                        seedinfo.Info4 = taginfo.Music_4;
                        seedinfo.Info5 = taginfo.Music_5;
                        seedinfo.Info6 = taginfo.Music_6;
                        seedinfo.Info7 = taginfo.Music_7;
                        seedinfo.Info8 = taginfo.Music_8;
                        seedinfo.Info9 = taginfo.Music_9;
                        break;
                    }
                case 5:
                    {
                        seedinfo.Info1 = taginfo.Game_1;
                        seedinfo.Info2 = taginfo.Game_2;
                        seedinfo.Info3 = taginfo.Game_3;
                        seedinfo.Info4 = taginfo.Game_4;
                        seedinfo.Info5 = taginfo.Game_5;
                        seedinfo.Info6 = taginfo.Game_6;
                        seedinfo.Info7 = taginfo.Game_7;
                        seedinfo.Info8 = taginfo.Game_8;
                        break;
                    }
                case 6:
                    {
                        seedinfo.Info1 = taginfo.Discovery_1;
                        seedinfo.Info2 = taginfo.Discovery_2;
                        seedinfo.Info3 = taginfo.Discovery_3;
                        seedinfo.Info4 = taginfo.Discovery_4;
                        seedinfo.Info5 = taginfo.Discovery_5;
                        seedinfo.Info6 = taginfo.Discovery_6;
                        seedinfo.Info7 = taginfo.Discovery_7;
                        break;
                    }
                case 7:
                    {
                        seedinfo.Info1 = taginfo.Sport_1;
                        seedinfo.Info2 = taginfo.Sport_2;
                        seedinfo.Info3 = taginfo.Sport_3;
                        seedinfo.Info4 = taginfo.Sport_4;
                        seedinfo.Info5 = taginfo.Sport_5;
                        seedinfo.Info6 = taginfo.Sport_6;
                        seedinfo.Info7 = taginfo.Sport_7;
                        seedinfo.Info8 = taginfo.Sport_8;
                        break;
                    }
                case 8:
                    {
                        seedinfo.Info1 = taginfo.Entertainment_1;
                        seedinfo.Info2 = taginfo.Entertainment_2;
                        seedinfo.Info3 = taginfo.Entertainment_3;
                        seedinfo.Info4 = taginfo.Entertainment_4;
                        seedinfo.Info5 = taginfo.Entertainment_5;
                        seedinfo.Info6 = taginfo.Entertainment_6;
                        seedinfo.Info7 = taginfo.Entertainment_7;
                        seedinfo.Info8 = taginfo.Entertainment_8;
                        seedinfo.Info9 = taginfo.Entertainment_9;
                        break;
                    }
                case 9:
                    {
                        seedinfo.Info1 = taginfo.Software_1;
                        seedinfo.Info2 = taginfo.Software_2;
                        seedinfo.Info3 = taginfo.Software_3;
                        seedinfo.Info4 = taginfo.Software_4;
                        seedinfo.Info5 = taginfo.Software_5;
                        seedinfo.Info6 = taginfo.Software_6;
                        break;
                    }
                case 10:
                    {
                        seedinfo.Info1 = taginfo.Staff_1;
                        seedinfo.Info2 = taginfo.Staff_2;
                        seedinfo.Info3 = taginfo.Staff_3;
                        seedinfo.Info4 = taginfo.Staff_4;
                        seedinfo.Info5 = taginfo.Staff_5;
                        seedinfo.Info6 = taginfo.Staff_6;
                        break;
                    }
                case 11:
                    {
                        seedinfo.Info1 = taginfo.Video_1;
                        seedinfo.Info2 = taginfo.Video_2;
                        seedinfo.Info3 = taginfo.Video_3;
                        seedinfo.Info4 = taginfo.Video_4;
                        seedinfo.Info5 = taginfo.Video_5;
                        seedinfo.Info6 = taginfo.Video_6;
                        seedinfo.Info7 = taginfo.Video_7;
                        seedinfo.Info8 = taginfo.Video_8;
                        break;
                    }
                case 12:
                    {
                        seedinfo.Info1 = taginfo.Other_1;
                        seedinfo.Info2 = taginfo.Other_2;
                        seedinfo.Info3 = taginfo.Other_3;
                        seedinfo.Info4 = taginfo.Other_4;
                        seedinfo.Info5 = taginfo.Other_5;
                        break;
                    }
                default:
                    return;
            }
        }


        /// <summary>
        /// 加载Seedinfo_Tracker信息（bt_seed_tracker表），不读取infohash
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfoTracker LoadSeedInfoTracker(IDataReader rd)
        {
            PTSeedinfoTracker seedinfo = new PTSeedinfoTracker();

            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            //seedinfo_t.InfoHash = rd["infohash"].ToString();
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);

            return seedinfo;
        }
        /// <summary>
        /// 加载Seedinfo_Tracker信息（bt_seed_tracker表），不读取infohash
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfoTracker LoadSeedInfoTracker(DataRow rd)
        {
            PTSeedinfoTracker seedinfo = new PTSeedinfoTracker();

            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            //seedinfo_t.InfoHash = rd["infohash"].ToString();
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);

            return seedinfo;
        }
        /// <summary>
        /// 加载Seedinfo_Short信息，从（bt_seed_tracker表），不读取infohash
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfoShort LoadSeedInfoShortFromTracker(IDataReader rd)
        {
            PTSeedinfoShort seedinfo = new PTSeedinfoShort();

            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            //seedinfo_t.InfoHash = rd["infohash"].ToString();
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);

            return seedinfo;
        }
        /// <summary>
        /// 加载 Seedinfo_Short信息（bt_seed表）【25项，不包括infohash_c】
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfoShort LoadSeedInfoShort(IDataReader rd)
        {
            PTSeedinfoShort seedinfo = new PTSeedinfoShort();

            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.DownloadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["downloadratioexpiredate"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            //seedinfo.InfoHash_c = rd["infohash_c"].ToString().Trim();
            seedinfo.IPv6 = TypeConverter.ObjectToInt(rd["ipv6"]);
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Live = TypeConverter.ObjectToInt(rd["live"]);
            seedinfo.PostDateTime = TypeConverter.ObjectToDateTime(rd["postdatetime"]);
            seedinfo.Replies = TypeConverter.ObjectToInt(rd["replies"]);
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.TopicId = TypeConverter.ObjectToInt(rd["topicid"]);
            seedinfo.TopicTitle = rd["topictitle"].ToString().Trim();
            seedinfo.TopSeed = TypeConverter.ObjectToInt(rd["topseed"]);
            seedinfo.Traffic = TypeConverter.ObjectToDecimal(rd["traffic"]);
            seedinfo.Type = TypeConverter.ObjectToInt(rd["type"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);
            seedinfo.UploadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["uploadratioexpiredate"]);
            seedinfo.UpTraffic = TypeConverter.ObjectToDecimal(rd["uptraffic"]);
            seedinfo.UserName = rd["username"].ToString().Trim();
            seedinfo.Views = TypeConverter.ObjectToInt(rd["views"]);
            seedinfo.Rss_Acc = TypeConverter.ObjectToInt(rd["accrss"]);
            seedinfo.Rss_Keep = TypeConverter.ObjectToInt(rd["keeprss"]);
            seedinfo.Rss_Pub = TypeConverter.ObjectToInt(rd["pubrss"]);

            if (seedinfo.Upload < 0) seedinfo.Upload = 0;
            if (seedinfo.Download < 0) seedinfo.Download = 0;

            return seedinfo;
        }
        /// <summary>
        /// 加载 Seedinfo_Short信息（bt_seed表）【25项，不包括infohash_c】
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfoShort LoadSeedInfoShort(DataRow rd)
        {
            PTSeedinfoShort seedinfo = new PTSeedinfoShort();

            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.DownloadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["downloadratioexpiredate"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            //seedinfo.InfoHash_c = rd["infohash_c"].ToString().Trim();
            seedinfo.IPv6 = TypeConverter.ObjectToInt(rd["ipv6"]);
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Live = TypeConverter.ObjectToInt(rd["live"]);
            seedinfo.PostDateTime = TypeConverter.ObjectToDateTime(rd["postdatetime"]);
            seedinfo.Replies = TypeConverter.ObjectToInt(rd["replies"]);
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.TopicId = TypeConverter.ObjectToInt(rd["topicid"]);
            seedinfo.TopicTitle = rd["topictitle"].ToString().Trim();
            seedinfo.TopSeed = TypeConverter.ObjectToInt(rd["topseed"]);
            seedinfo.Traffic = TypeConverter.ObjectToDecimal(rd["traffic"]);
            seedinfo.Type = TypeConverter.ObjectToInt(rd["type"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);
            seedinfo.UploadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["uploadratioexpiredate"]);
            seedinfo.UpTraffic = TypeConverter.ObjectToDecimal(rd["uptraffic"]);
            seedinfo.UserName = rd["username"].ToString().Trim();
            seedinfo.Views = TypeConverter.ObjectToInt(rd["views"]);
            seedinfo.Rss_Acc = TypeConverter.ObjectToInt(rd["accrss"]);
            seedinfo.Rss_Keep = TypeConverter.ObjectToInt(rd["keeprss"]);
            seedinfo.Rss_Pub = TypeConverter.ObjectToInt(rd["pubrss"]);

            if (seedinfo.Upload < 0) seedinfo.Upload = 0;
            if (seedinfo.Download < 0) seedinfo.Download = 0;

            return seedinfo;
        }
        /// <summary>
        /// 加载 Seedinfo信息（bt_seed表和bt_seed_detail表的一部分，用以贴内种子信息显示、种子下载等）
        /// 【25项，不包括infohash_c】【5项】
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfo LoadSeedInfo(IDataReader rd)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();

            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.DownloadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["downloadratioexpiredate"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            //seedinfo.InfoHash_c = rd["infohash_c"].ToString().Trim();
            seedinfo.IPv6 = TypeConverter.ObjectToInt(rd["ipv6"]);
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Live = TypeConverter.ObjectToInt(rd["live"]);
            seedinfo.PostDateTime = TypeConverter.ObjectToDateTime(rd["postdatetime"]);
            seedinfo.Replies = TypeConverter.ObjectToInt(rd["replies"]);
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.TopicId = TypeConverter.ObjectToInt(rd["topicid"]);
            seedinfo.TopicTitle = rd["topictitle"].ToString().Trim();
            seedinfo.TopSeed = TypeConverter.ObjectToInt(rd["topseed"]);
            seedinfo.Traffic = TypeConverter.ObjectToDecimal(rd["traffic"]);
            seedinfo.Type = TypeConverter.ObjectToInt(rd["type"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);
            seedinfo.UploadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["uploadratioexpiredate"]);
            seedinfo.UpTraffic = TypeConverter.ObjectToDecimal(rd["uptraffic"]);
            seedinfo.UserName = rd["username"].ToString().Trim();
            seedinfo.Views = TypeConverter.ObjectToInt(rd["views"]);
            seedinfo.Rss_Acc = TypeConverter.ObjectToInt(rd["accrss"]);
            seedinfo.Rss_Keep = TypeConverter.ObjectToInt(rd["keeprss"]);
            seedinfo.Rss_Pub = TypeConverter.ObjectToInt(rd["pubrss"]);

            seedinfo.FileName = rd["filename"].ToString().Trim();
            seedinfo.InfoHash = rd["infohash"].ToString();
            seedinfo.LastSeederId = TypeConverter.ObjectToInt(rd["lastseederid"]);
            seedinfo.LastSeederName = rd["lastseedername"].ToString().Trim();
            seedinfo.Path = rd["path"].ToString().Trim();

            if (seedinfo.Upload < 0) seedinfo.Upload = 0;
            if (seedinfo.Download < 0) seedinfo.Download = 0;

            return seedinfo;
        }
        /// <summary>
        /// 加载 Seedinfo信息（bt_seed表和bt_seed_detail表的一部分，用以贴内种子信息显示、种子下载等）
        /// 【25项，不包括infohash_c】【5项】
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfo LoadSeedInfo(DataRow rd)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();

            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.DownloadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["downloadratioexpiredate"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            //seedinfo.InfoHash_c = rd["infohash_c"].ToString().Trim();
            seedinfo.IPv6 = TypeConverter.ObjectToInt(rd["ipv6"]);
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Live = TypeConverter.ObjectToInt(rd["live"]);
            seedinfo.PostDateTime = TypeConverter.ObjectToDateTime(rd["postdatetime"]);
            seedinfo.Replies = TypeConverter.ObjectToInt(rd["replies"]);
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.TopicId = TypeConverter.ObjectToInt(rd["topicid"]);
            seedinfo.TopicTitle = rd["topictitle"].ToString().Trim();
            seedinfo.TopSeed = TypeConverter.ObjectToInt(rd["topseed"]);
            seedinfo.Traffic = TypeConverter.ObjectToDecimal(rd["traffic"]);
            seedinfo.Type = TypeConverter.ObjectToInt(rd["type"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);
            seedinfo.UploadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["uploadratioexpiredate"]);
            seedinfo.UpTraffic = TypeConverter.ObjectToDecimal(rd["uptraffic"]);
            seedinfo.UserName = rd["username"].ToString().Trim();
            seedinfo.Views = TypeConverter.ObjectToInt(rd["views"]);
            seedinfo.Rss_Acc = TypeConverter.ObjectToInt(rd["accrss"]);
            seedinfo.Rss_Keep = TypeConverter.ObjectToInt(rd["keeprss"]);
            seedinfo.Rss_Pub = TypeConverter.ObjectToInt(rd["pubrss"]);

            seedinfo.FileName = rd["filename"].ToString().Trim();
            seedinfo.InfoHash = rd["infohash"].ToString();
            seedinfo.LastSeederId = TypeConverter.ObjectToInt(rd["lastseederid"]);
            seedinfo.LastSeederName = rd["lastseedername"].ToString().Trim();
            seedinfo.Path = rd["path"].ToString().Trim();

            if (seedinfo.Upload < 0) seedinfo.Upload = 0;
            if (seedinfo.Download < 0) seedinfo.Download = 0;

            return seedinfo;
        }
        /// <summary>
        /// 加载 Seedinfo信息（bt_seed表和bt_seed_detail表全部）
        /// 【25项，不包括infohash_c】【5+19=24项】
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfo LoadSeedInfoFull(IDataReader rd)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();

            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.DownloadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["downloadratioexpiredate"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            seedinfo.InfoHash_c = rd["infohash_c"].ToString().Trim();
            seedinfo.IPv6 = TypeConverter.ObjectToInt(rd["ipv6"]);
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Live = TypeConverter.ObjectToInt(rd["live"]);
            seedinfo.PostDateTime = TypeConverter.ObjectToDateTime(rd["postdatetime"]);
            seedinfo.Replies = TypeConverter.ObjectToInt(rd["replies"]);
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.TopicId = TypeConverter.ObjectToInt(rd["topicid"]);
            seedinfo.TopicTitle = rd["topictitle"].ToString().Trim();
            seedinfo.TopSeed = TypeConverter.ObjectToInt(rd["topseed"]);
            seedinfo.Traffic = TypeConverter.ObjectToDecimal(rd["traffic"]);
            seedinfo.Type = TypeConverter.ObjectToInt(rd["type"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);
            seedinfo.UploadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["uploadratioexpiredate"]);
            seedinfo.UpTraffic = TypeConverter.ObjectToDecimal(rd["uptraffic"]);
            seedinfo.UserName = rd["username"].ToString().Trim();
            seedinfo.Views = TypeConverter.ObjectToInt(rd["views"]);
            seedinfo.Rss_Acc = TypeConverter.ObjectToInt(rd["accrss"]);
            seedinfo.Rss_Keep = TypeConverter.ObjectToInt(rd["keeprss"]);
            seedinfo.Rss_Pub = TypeConverter.ObjectToInt(rd["pubrss"]);

            seedinfo.FileName = rd["filename"].ToString().Trim();
            seedinfo.InfoHash = rd["infohash"].ToString();
            seedinfo.LastSeederId = TypeConverter.ObjectToInt(rd["lastseederid"]);
            seedinfo.LastSeederName = rd["lastseedername"].ToString().Trim();
            seedinfo.Path = rd["path"].ToString().Trim();

            seedinfo.Award = TypeConverter.ObjectToDecimal(rd["award"]);
            seedinfo.CreatedBy = rd["createdby"].ToString().Trim();
            seedinfo.CreatedDate = TypeConverter.ObjectToDateTime(rd["createddate"]);
            seedinfo.FolderName = rd["foldername"].ToString().Trim();
            seedinfo.Info1 = rd["info1"].ToString().Trim();
            seedinfo.Info2 = rd["info2"].ToString().Trim();
            seedinfo.Info3 = rd["info3"].ToString().Trim();
            seedinfo.Info4 = rd["info4"].ToString().Trim();
            seedinfo.Info5 = rd["info5"].ToString().Trim();
            seedinfo.Info6 = rd["info6"].ToString().Trim();
            seedinfo.Info7 = rd["info7"].ToString().Trim();
            seedinfo.Info8 = rd["info8"].ToString().Trim();
            seedinfo.Info9 = rd["info9"].ToString().Trim();
            seedinfo.Info10 = rd["info10"].ToString().Trim();
            seedinfo.Info11 = rd["info11"].ToString().Trim();
            seedinfo.Info12 = rd["info12"].ToString().Trim();
            seedinfo.Info13 = rd["info13"].ToString().Trim();
            seedinfo.Info14 = rd["info14"].ToString().Trim();
            seedinfo.SingleFile = rd["singlefile"].ToString().Trim().ToLower() == "true" ? true : false;

            if (seedinfo.Upload < 0) seedinfo.Upload = 0;
            if (seedinfo.Download < 0) seedinfo.Download = 0;

            return seedinfo;
        }
        /// <summary>
        /// 加载 Seedinfo信息（bt_seed表和bt_seed_detail表全部）
        /// 【25项，不包括infohash_c】【5+19=24项】
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static PTSeedinfo LoadSeedInfoFull(DataRow rd)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();

            seedinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedinfo.DownloadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["downloadratioexpiredate"]);
            seedinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            seedinfo.InfoHash_c = rd["infohash_c"].ToString().Trim();
            seedinfo.IPv6 = TypeConverter.ObjectToInt(rd["ipv6"]);
            seedinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedinfo.Live = TypeConverter.ObjectToInt(rd["live"]);
            seedinfo.PostDateTime = TypeConverter.ObjectToDateTime(rd["postdatetime"]);
            seedinfo.Replies = TypeConverter.ObjectToInt(rd["replies"]);
            seedinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedinfo.TopicId = TypeConverter.ObjectToInt(rd["topicid"]);
            seedinfo.TopicTitle = rd["topictitle"].ToString().Trim();
            seedinfo.TopSeed = TypeConverter.ObjectToInt(rd["topseed"]);
            seedinfo.Traffic = TypeConverter.ObjectToDecimal(rd["traffic"]);
            seedinfo.Type = TypeConverter.ObjectToInt(rd["type"]);
            seedinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);
            seedinfo.UploadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["uploadratioexpiredate"]);
            seedinfo.UpTraffic = TypeConverter.ObjectToDecimal(rd["uptraffic"]);
            seedinfo.UserName = rd["username"].ToString().Trim();
            seedinfo.Views = TypeConverter.ObjectToInt(rd["views"]);
            seedinfo.Rss_Acc = TypeConverter.ObjectToInt(rd["accrss"]);
            seedinfo.Rss_Keep = TypeConverter.ObjectToInt(rd["keeprss"]);
            seedinfo.Rss_Pub = TypeConverter.ObjectToInt(rd["pubrss"]);

            seedinfo.FileName = rd["filename"].ToString().Trim();
            seedinfo.InfoHash = rd["infohash"].ToString();
            seedinfo.LastSeederId = TypeConverter.ObjectToInt(rd["lastseederid"]);
            seedinfo.LastSeederName = rd["lastseedername"].ToString().Trim();
            seedinfo.Path = rd["path"].ToString().Trim();

            seedinfo.Award = TypeConverter.ObjectToDecimal(rd["award"]);
            seedinfo.CreatedBy = rd["createdby"].ToString().Trim();
            seedinfo.CreatedDate = TypeConverter.ObjectToDateTime(rd["createddate"]);
            seedinfo.FolderName = rd["foldername"].ToString().Trim();
            seedinfo.Info1 = rd["info1"].ToString().Trim();
            seedinfo.Info2 = rd["info2"].ToString().Trim();
            seedinfo.Info3 = rd["info3"].ToString().Trim();
            seedinfo.Info4 = rd["info4"].ToString().Trim();
            seedinfo.Info5 = rd["info5"].ToString().Trim();
            seedinfo.Info6 = rd["info6"].ToString().Trim();
            seedinfo.Info7 = rd["info7"].ToString().Trim();
            seedinfo.Info8 = rd["info8"].ToString().Trim();
            seedinfo.Info9 = rd["info9"].ToString().Trim();
            seedinfo.Info10 = rd["info10"].ToString().Trim();
            seedinfo.Info11 = rd["info11"].ToString().Trim();
            seedinfo.Info12 = rd["info12"].ToString().Trim();
            seedinfo.Info13 = rd["info13"].ToString().Trim();
            seedinfo.Info14 = rd["info14"].ToString().Trim();
            seedinfo.SingleFile = rd["singlefile"].ToString().Trim().ToLower() == "true" ? true : false;

            if (seedinfo.Upload < 0) seedinfo.Upload = 0;
            if (seedinfo.Download < 0) seedinfo.Download = 0;

            return seedinfo;
        }

        /// <summary>
        /// 获取状态正常（status为2/3）SeedinfoTracker（返回seedid大于0时为正常，否则异常），
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfoTracker GetSeedInfoTracker(string infohash)
        {
            PTSeedinfoTracker seedinfo = new PTSeedinfoTracker();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoTracker(infohash);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoTracker(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 获取状态正常（status为2/3）SeedinfoShort，从Seed_tracker表（返回seedid大于0时为正常，否则异常），
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfoShort GetSeedInfoShortFromTracker(string infohash)
        {
            PTSeedinfoShort seedinfo = new PTSeedinfoShort();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoTracker(infohash);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoShortFromTracker(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 获取状态正常（status为2/3）SeedinfoShort，（返回seedid大于0时为正常，否则异常），种子列表显示，showseedfile/seedpeer/seedop/seedpeerhistory
        /// 如果种子状态不是2/3，则返回值seedid小于零
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfoShort GetSeedInfoShort(int seedid)
        {
            PTSeedinfoShort seedinfo = new PTSeedinfoShort();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoShort(seedid);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoShort(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 【临时解决】获取状态正常（status为2/3）SeedinfoShort，（返回seedid大于0时为正常，否则异常），种子列表显示，showseedfile/seedpeer/seedop/seedpeerhistory
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfoShort GetSeedInfoShort(string infohash)
        {
            PTSeedinfoShort seedinfo = new PTSeedinfoShort();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoShort(infohash);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoShort(rd);
                //PeerCount最后更新时间
                seedinfo.LastPeerCountUpdate = TypeConverter.ObjectToDateTime(rd["lastpeercountupdate"]);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo，（返回seedid大于0时为正常，否则异常），帖子内种子详细信息显示页面，下载等，非全部信息
        /// NOLOCK，可能脏读
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfo GetSeedInfo(int seedid)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfo(seedid);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfo(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 获取状态正常（status为2/3）的SeedinfoFull，（返回seedid大于0时为正常，否则异常），showseedinfo页面，发布、编辑页面
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfo GetSeedInfoFull(int seedid)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoFull(seedid);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoFull(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 获取任意状态的SeedinfoFull，用于发种信息检测
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfo GetSeedInfoFullAllStatus(string infohash_c)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoFullAllStatus(infohash_c);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoFull(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 获取状态正常（status为2/3）的SeedinfoFull，（返回seedid大于0时为正常，否则异常），用于发种信息检测
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfo GetSeedInfoFull(string infohash_c)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoFull(infohash_c);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoFull(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }
        /// <summary>
        /// 获取SeedinfoFull，（返回seedid大于0时为正常，否则异常），showseedinfo页面，发布、编辑页面
        /// 此函数返回任意状态的种子
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        public static PTSeedinfo GetSeedInfoFullAllStatus(int seedid)
        {
            PTSeedinfo seedinfo = new PTSeedinfo();
            IDataReader rd = DatabaseProvider.GetInstance().GetSeedInfoFullAllStatus(seedid);
            if (rd.Read())
            {
                seedinfo = LoadSeedInfoFull(rd);
            }
            rd.Close();
            rd.Dispose();
            return seedinfo;
        }

        /// <summary>
        /// 对种子列表进行处理，以方便显示
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<PTSeedinfoShort> ProcessSeedInfoList(int userid, List<PTSeedinfoShort> seedinfolist, bool calckeepreward)
        {
            //种子列表
            string seedidlist = "";
            foreach (PTSeedinfoShort seedinfo in seedinfolist)
            {
                seedidlist += seedinfo.SeedId + ",";
            }
            if (seedidlist != "") seedidlist = seedidlist.Remove(seedidlist.Length - 1);
            else seedidlist = "0";

            //获取用户上传、下载、完成的种子列表
            DataTable upload = DatabaseProvider.GetInstance().GetSeedIdList(userid, 1, seedidlist);
            DataTable download = DatabaseProvider.GetInstance().GetSeedIdList(userid, 2, seedidlist);
            DataTable finished = DatabaseProvider.GetInstance().GetSeedIdList(userid, 4, seedidlist);

            PTUserInfo btuserinfo = null;
            if(calckeepreward) btuserinfo = PTUsers.GetBtUserInfo(userid);

            foreach (PTSeedinfoShort seedinfo in seedinfolist)
            {
                //自己发布的种子，上传系数*2
                if (userid == seedinfo.Uid && seedinfo.UploadRatio >= 1.0 && seedinfo.UploadRatio < 2.0)
                {
                    seedinfo.UploadRatio = 2.0f;
                }

                //过滤单引号之后的种子名
                //dr["topictitle"] = System.Web.HttpUtility.HtmlEncode(dr["topictitle"].ToString());
                seedinfo.Dis_TopicTitleFilter = seedinfo.TopicTitle.Replace("'", "\\'").Replace("\"", "\\\"");

                //区分用户正在上传下载的种子
                seedinfo.Dis_UserDisplayStyle = 1;
                if (upload.Compute("COUNT([seedid])", "[seedid]=" + seedinfo.SeedId.ToString()).ToString() != "0") seedinfo.Dis_UserDisplayStyle += 10000;
                if (download.Compute("COUNT([seedid])", "[seedid]=" + seedinfo.SeedId.ToString()).ToString() != "0") seedinfo.Dis_UserDisplayStyle += 1000;
                if (finished.Compute("COUNT([seedid])", "[seedid]=" + seedinfo.SeedId.ToString()).ToString() != "0") seedinfo.Dis_UserDisplayStyle += 100;
                if (userid == seedinfo.Uid) seedinfo.Dis_UserDisplayStyle += 10;


                //种子类别中文字符串
                seedinfo.Dis_ChnTypeName = PrivateBT.Type2Name(seedinfo.Type);
                //种子大小字符串
                seedinfo.Dis_Size = PTTools.Upload2Str(seedinfo.FileSize, false);

                //做种人数描述html
                if (seedinfo.Upload == 0) seedinfo.Dis_UploadCount = "<span style=\"font-weight:bolder; color:#F00\">0</span>";
                else if (seedinfo.Upload < 5) seedinfo.Dis_UploadCount = "<span style=\" font-weight:bolder; color:#050\">" + seedinfo.Upload.ToString() + "</span>";
                else if (seedinfo.Upload < 10) seedinfo.Dis_UploadCount = "<span style=\"font-weight:bolder; color:#080\">" + seedinfo.Upload.ToString() + "</span>";
                else if (seedinfo.Upload < 20) seedinfo.Dis_UploadCount = "<span style=\"font-weight:bolder; color:#0b0\">" + seedinfo.Upload.ToString() + "</span>";
                else seedinfo.Dis_UploadCount = "<span style=\"font-weight:bolder; color:#0e0\">" + seedinfo.Upload.ToString() + "</span>";

                //流量描述html
                seedinfo.Dis_DownloadTraffic = PTTools.Upload2Str(seedinfo.Traffic, false);
                //种类英文描述例如movie
                seedinfo.Dis_EngTypeName = PrivateBT.Type2Str(seedinfo.Type);

                //流量异常信息，只管理员显示
                if (userid == 1)
                {
                    if (seedinfo.Traffic - seedinfo.UpTraffic > 5 * 1024 * 1024 * 1024M) seedinfo.Dis_TrafficCheck = "blue";
                    else if (seedinfo.Traffic - seedinfo.UpTraffic > 50 * 1024 * 1024 * 1024M) seedinfo.Dis_TrafficCheck = "red";
                    else if (seedinfo.Traffic - seedinfo.UpTraffic > 5 * 1024 * 1024 * 1024M) seedinfo.Dis_TrafficCheck = "yellow";
                    else seedinfo.Dis_TrafficCheck = "green";
                }

                //存活时间字符串
                seedinfo.Dis_TimetoLive = PTTools.Minutes2String(seedinfo.Live / 60.0, true);

                //发布时间字符串
                seedinfo.Dis_PostDateTime = ForumUtils.ConvertDateTime(seedinfo.PostDateTime.ToString());

                //流量系数过期信息
                if (seedinfo.UploadRatio != 1 && seedinfo.UploadRatioExpireDate < DateTime.Parse("2099-1-1"))
                    seedinfo.Dis_UploadRatioNote = string.Format("(剩余{0})", PTTools.Minutes2String((seedinfo.UploadRatioExpireDate - DateTime.Now).TotalMinutes));
                if (seedinfo.DownloadRatio != 1 && seedinfo.DownloadRatioExpireDate < DateTime.Parse("2099-1-1"))
                    seedinfo.Dis_DownloadRatioNote = string.Format("(剩余{0})", PTTools.Minutes2String((seedinfo.DownloadRatioExpireDate - DateTime.Now).TotalMinutes));


                //蓝种剩余提醒
                if (seedinfo.DownloadRatio == 0 && (seedinfo.DownloadRatioExpireDate - DateTime.Now).TotalMinutes < 4320)
                    seedinfo.Dis_RatioNoteAdd = "蓝种剩余" + (PTTools.Minutes2String((seedinfo.DownloadRatioExpireDate - DateTime.Now).TotalMinutes)).Replace("时", "小时").Replace("分", "分钟");
                else seedinfo.Dis_RatioNoteAdd = "";
                //dr["rationoteadd"] = "蓝种剩余" + (PTTools.Minutes2String(blueseedleft)).Replace("时", "小时").Replace("分", "分钟");
                //dr["rationoteadd"] = bluehour * 60 - (DateTime.Now - DateTime.Parse(dr["Postdatetime"].ToString())).TotalMinutes;

                //保种系数
                if (calckeepreward && btuserinfo != null)
                {
                    double a = 0, b = 0, c = 0, d = 0, e = 0, all = 0;
                    decimal reward = GetSeedAward(seedinfo.FileSize, seedinfo.User_UpTraffic, seedinfo.Live, seedinfo.User_Keeptime, seedinfo.Upload, seedinfo.LastFinish, btuserinfo.Joindate, ref a, ref b, ref c, ref d, ref e, ref all);
                    seedinfo.Dis_KeepReward_All = string.Format("<span title=\"{2}\"><b>{0}</b></span>, <span title=\"{3}\">{1}/h</span>", all.ToString("0.00"), PTTools.Upload2Str(reward), "保种系数", "保种奖励");
                    seedinfo.Dis_KeepRewardFactor = string.Format("<span title=\"{5}\">{0}</span> - <span title=\"{6}\">{1}</span> - <span title=\"{7}\">{2}</span> - <span title=\"{8}\">{3}</span> - <span title=\"{9}\">{4}</span>"
                        , a, b, c, d, e, "种子存活时间系数", "用户保种时间系数", "用户上传量系数", "种子最近完成系数", "种子正在上传系数");
                    seedinfo.Dis_UserKeepTime = string.Format("<span title=\"{1}\">{0}</span>", PTTools.Second2String((double)seedinfo.User_Keeptime, true), PTTools.Second2String((double)seedinfo.User_Keeptime, false));
                    seedinfo.Dis_UserUpTraffic = string.Format("<span title=\"真实上传\"><b>{0}</b></span>", PTTools.Upload2Str(seedinfo.User_UpTraffic));
                }
            }

            //清理内存
            upload.Clear(); upload.Dispose();
            download.Clear(); download.Dispose();
            finished.Clear(); finished.Dispose();

            return seedinfolist;
        }
        /// <summary>
        /// 获得指定条件的种子列表
        /// </summary>
        /// <param name="numperpage">每页显示的种子数量</param>
        /// <param name="pageindex">当前页码</param>
        /// <param name="topseedcount">置顶种子数</param>
        /// <param name="seedtype">种子种类</param>
        /// <param name="userid">用户id 或者 当前访问者的id，取决于后面userstat的值</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成，0使userid失效</param>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种，0全部</param>
        /// <param name="keywords">搜索关键词</param>
        /// <param name="orderby">排序：1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间</param>
        /// <param name="asc">正序/反序</param>
        /// <param name="top">置顶种子id表</param>
        /// <param name="upload">用户正在上传种子id表</param>
        /// <param name="download">用户正在下载种子id表</param>
        /// <param name="nowuserid">当前登录的用户id</param>
        /// <returns></returns>
        public static List<PTSeedinfoShort> GetSeedInfoList(int numperpage, int pageindex, int topseedcount, int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, int orderby, bool asc, int nowuserid, string notin, ref string seedsearchlist, ref int totalcount)
        {
            int seedstatus = 2;
            if (seedstat == 8) seedstatus = 0;
            else if (seedstat == 9) seedstatus = 3;

            if (keywords != "")
            {
                seedsearchlist = GetSeedSearchList(keywordsmode, seedstatus, seedtype, PTTools.SQLFullContentIndexKeywordsProcess(keywords, keywordsmode), userid, userstat < 0 ? -userstat : userstat, ref totalcount);
                //如果是搜索但没有符合要求的结果，返回空
                if (seedsearchlist == "") return new List<PTSeedinfoShort>();
            }

            //DEBUG//
            if (keywords == "美剧") PTLog.InsertSystemLogDebug(PTLog.LogType.SearchPage, PTLog.LogStatus.Normal, "TEST", string.Format("LEN:{0} COUNT:{1}", seedsearchlist.Length, PTTools.GetIntTableFromString(seedsearchlist).Rows.Count));

            //有搜索结果，或不使用关键词搜索的情况
            List<PTSeedinfoShort> seedinfolist = new List<PTSeedinfoShort>();
            DataTable dt = DatabaseProvider.GetInstance().GetSeedInfoList(numperpage, pageindex, topseedcount, seedtype, userid, userstat, seedstat,
                seedsearchlist, keywordsmode, orderby, asc,
                PTTools.SQLFullContentIndexKeywordsProcess(notin, -2)
                );
            foreach (DataRow dr in dt.Rows)
            {
                PTSeedinfoShort seedinfo = LoadSeedInfoShort(dr);
                if (userstat < 0) LoadSeedKeepRewardInfo(ref seedinfo, dr);
                if (seedinfo.SeedId > 0) seedinfolist.Add(seedinfo);
            }
            dt.Clear();
            dt.Dispose();

            if (userstat < 0) return ProcessSeedInfoList(nowuserid, seedinfolist, true);
            return ProcessSeedInfoList(nowuserid, seedinfolist, false);
        }

        /// <summary>
        /// 获取种子搜索结果seedid
        /// </summary>
        /// <param name="searchmode"></param>
        /// <param name="seedstatus"></param>
        /// <param name="seedtype"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static string GetSeedSearchList(int searchmode, int seedstatus, int seedtype, string keywords, int userid, int userstat, ref int totalcount)
        {
            if (keywords.Trim() == "") return "";
            string rst = "";
            //不包含用户状态的查询，使用缓存
            if(userid < 1 || userstat < 1) rst = DatabaseProvider.GetInstance().GetSeedSearchCache(searchmode, seedstatus, seedtype, keywords.ToLower()).Trim();
            if (rst.ToLower() == "null")
            {
                DataTable dt = DatabaseProvider.GetInstance().GetSeedSearchList(searchmode, seedstatus, seedtype, keywords, userid, userstat);
                rst = "";
                totalcount = 0;
                if (dt != null && dt.Rows != null)
                {
                    totalcount = dt.Rows.Count;
                    foreach (DataRow dr in dt.Rows)
                    {
                        rst += dr["seedid"].ToString() + ",";
                    }
                    if (rst.Length > 0) rst = rst.Substring(0, rst.Length - 1);
                    if ((userid < 1 || userstat < 1) && totalcount < 5001) DatabaseProvider.GetInstance().InsertSeedSearchCache(searchmode, seedstatus, seedtype, keywords.ToLower(), rst);
                }
                if (dt != null) dt.Dispose();
                dt = null;
            }
            else
            {
                totalcount = PTTools.GetIntTableFromString(rst).Rows.Count;
                PTLog.InsertSystemLogDebug(PTLog.LogType.SearchPage, PTLog.LogStatus.Normal, "SearchPage", string.Format("缓存命中：Mode:{0} -Status:{1} -Type:{2} -keywords:{3}", searchmode, seedstatus, seedtype, keywords));
            }
            return rst;
        }

        public static int CleanSeedSearchCache()
        {
            try
            {
                int c = 0;

                DateTime st = DateTime.Now;
                c = DatabaseProvider.GetInstance().CleanSeedSearchCache();
                PTLog.InsertSystemLog(PTLog.LogType.SystemLog, PTLog.LogStatus.Exception, "CleanSeedSearchCache", string.Format("清理CleanSeedSearchCache：{0} 耗时：{1}", c, (DateTime.Now - st).TotalSeconds));

                return c;
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.SystemLog, PTLog.LogStatus.Exception, "CleanSeedSearchCache", ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 加载计算保种信息所需的额外信息
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <param name="rd"></param>
        public static void LoadSeedKeepRewardInfo(ref PTSeedinfoShort seedinfo, DataRow rd)
        {
            seedinfo.User_Keeptime = TypeConverter.ObjectToInt(rd["userkeeptime"]);
            seedinfo.User_UpTraffic = TypeConverter.ObjectToDecimal(rd["useruptraffic"]);
            seedinfo.LastFinish = TypeConverter.ObjectToDateTime(rd["lastfinish"]);
        }


        private delegate void UpdateHotSeedinfoListEventDelegate();
        private static DateTime UpdateHotSeedinfoListEvent_lastrun = DateTime.Now;
        /// <summary>
        /// 获取热门种子列表
        /// </summary>
        /// <param name="num"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public static List<PTSeedinfoShort> GetHotSeedinfoList(int num, int seedtype, int nowuserid)
        {
            DNTCache cache = DNTCache.GetCacheService();

            List<PTSeedinfoShort> newhotlist = cache.RetrieveObject("/BT/SeedList/NewHot" + seedtype.ToString("00")) as List<PTSeedinfoShort>;
            if (newhotlist == null || newhotlist.Count < num)
            {
                //超过2000秒仍未更新，强制更新
                if ((DateTime.Now - UpdateHotSeedinfoListEvent_lastrun).TotalSeconds > 2000)
                {
                    UpdateHotSeedinfoListEvent_lastrun = DateTime.Now;

                    List<PTSeedinfoShort> newhotlistadd = new List<PTSeedinfoShort>();
                    newhotlist = new List<PTSeedinfoShort>();

                    //插入一个空元素，用于分割列表
                    PTSeedinfoShort seedinfo_e = new PTSeedinfoShort();
                    PTSeedinfoShort seedinfo_eadd = new PTSeedinfoShort();
                    newhotlistadd.Add(seedinfo_eadd);
                    newhotlist.Add(seedinfo_e);

                    DataTable dt = DatabaseProvider.GetInstance().GetHotSeedinfoList(num, seedtype);
                    foreach (DataRow dr in dt.Rows)
                    {
                        PTSeedinfoShort seedinfo = LoadSeedInfoShort(dr);
                        PTSeedinfoShort seedinfoadd = LoadSeedInfoShort(dr);
                        if (seedinfo.SeedId > 0)
                        {
                            newhotlist.Add(seedinfo);
                            newhotlistadd.Add(seedinfoadd);
                        }
                    }
                    dt.Clear();
                    dt.Dispose();
                    cache.RemoveObject("/BT/SeedList/NewHot" + seedtype.ToString("00"));
                    cache.AddObject("/BT/SeedList/NewHot" + seedtype.ToString("00"), newhotlistadd, 3600);
                    //复制一份到返回值，防止因为后续处理，造成添加的缓存被修改，上面的AddObject是异步操作？？？
                    //C//newhotlist = new List<PTSeedinfoShort>(newhotlistadd.ToArray());
                    PTLog.InsertSystemLog(PTLog.LogType.HotNewSeed, PTLog.LogStatus.Error, "Update NewSeedList", string.Format("强制更新 HotNewSeed 缓存 -TYPE:{0} -COUNT:{1} -LastEvent:{2}", seedtype, newhotlist.Count, UpdateHotSeedinfoListEvent_lastrun));
                }
                else
                {
                    //加速早期执行，返回一个20个种子的列表
                    newhotlist = new List<PTSeedinfoShort>();
                    PTSeedinfoShort seedinfo_e = new PTSeedinfoShort();
                    newhotlist.Add(seedinfo_e);
                    DataTable dt = DatabaseProvider.GetInstance().GetSeedInfoList(20, 3, 0, seedtype, 0, 0, 0, "", 0, 0, false, "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        PTSeedinfoShort seedinfo = LoadSeedInfoShort(dr);
                        if (seedinfo.SeedId > 0)
                        {
                            newhotlist.Add(seedinfo);
                        }
                    }
                    dt.Clear();
                    dt.Dispose();
                    PTLog.InsertSystemLog(PTLog.LogType.HotNewSeed, PTLog.LogStatus.Detail, "Update NewSeedList", string.Format("跳过 HotNewSeed -TYPE:{0} -COUNT:{1} -LastEvent:{2}", seedtype, newhotlist.Count, UpdateHotSeedinfoListEvent_lastrun));
                }
            }
            
            return ProcessSeedInfoList(nowuserid, newhotlist,false); 
        }
        

        /// <summary>
        /// 更新最近热门种子的计划任务
        /// </summary>
        public static void UpdateHotSeedinfoListEvent()
        {
            UpdateHotSeedinfoListEvent_lastrun = DateTime.Now;

            try
            {
                DNTCache cache = DNTCache.GetCacheService();
                for (int i = 0; i < 13; i++)
                {
                    List<PTSeedinfoShort> newhotlistadd = new List<PTSeedinfoShort>();

                    //插入一个空元素，用于分割列表
                    PTSeedinfoShort seedinfo_eadd = new PTSeedinfoShort();
                    newhotlistadd.Add(seedinfo_eadd);

                    DataTable dt = DatabaseProvider.GetInstance().GetHotSeedinfoList(20, i);
                    foreach (DataRow dr in dt.Rows)
                    {
                        PTSeedinfoShort seedinfoadd = LoadSeedInfoShort(dr);
                        if (seedinfoadd.SeedId > 0)
                        {
                            newhotlistadd.Add(seedinfoadd);
                        }
                    }
                    dt.Clear();
                    dt.Dispose();
                    //cache.RemoveObject("/BT/SeedList/NewHot" + i.ToString("00"));
                    cache.RemoveObject("/BT/SeedList/NewHot" + i.ToString("00"));
                    cache.AddObject("/BT/SeedList/NewHot" + i.ToString("00"), newhotlistadd, 3600);
                    PTLog.InsertSystemLog(PTLog.LogType.HotNewSeed, PTLog.LogStatus.Detail, "UpdateNewSeedListEvent", string.Format("更新 HotNewSeed 缓存 -TYPE:{0} -COUNT:{1} -Lastrun:{2}", i, newhotlistadd.Count, UpdateHotSeedinfoListEvent_lastrun));
                }
                PTLog.InsertSystemLog(PTLog.LogType.HotNewSeed, PTLog.LogStatus.Normal, "UpdateNewSeedListEvent", string.Format("更新 HotNewSeed 缓存 完毕 -Lastrun:{0}", UpdateHotSeedinfoListEvent_lastrun));
            }
            catch (System.Exception ex)
            {
                PTLog.InsertSystemLog(PTLog.LogType.HotNewSeed, PTLog.LogStatus.Exception, "UpdateNewSeedListEvent", string.Format("更新 HotNewSeed 缓存 异常 EX:{0}", ex));
            }

        }

                /// <summary>
        /// 获取目前总优惠操作点数
        /// </summary>
        /// <param name="seedtype"></param>
        /// <param name="numperpage"></param>
        /// <returns></returns>
        public static int GetSeedOpValueSUM(int seedtype, int numperpage)
        {
            return DatabaseProvider.GetInstance().GetSeedOpValueSUM(seedtype, numperpage);
        }


        /// <summary>
        /// 获得指定条件的种子列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSeedRssList(int numperpage, int pageindex, int seedtype, int withinfo)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetSeedRssList(numperpage, pageindex, seedtype, withinfo);
            dt.Dispose();
            return dt;
        }

        /// <summary>
        /// 获得相应分类置顶种子表，分类小于0时，获取全部分类的置顶种子
        /// </summary>
        /// <param name="userid">当前浏览的用户id</param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public static List<PTSeedinfoShort> GetTopSeedInfoList(int seedtype, int nowuserid)
        {
            DNTCache cache = DNTCache.GetCacheService();
            string c = cache.RetrieveObject("/BT/SeedList/Top" + seedtype.ToString("00")) as string;
            if (c == null)
            {
                List<PTSeedinfoShort> seedinfolist = new List<PTSeedinfoShort>();
                DataTable dt = DatabaseProvider.GetInstance().GetTopSeedInfoList(seedtype);
                string idlist = "";
                foreach (DataRow dr in dt.Rows)
                {
                    PTSeedinfoShort seedinfo = LoadSeedInfoShort(dr);
                    if (seedinfo.SeedId > 0)
                    {
                        idlist += "," + seedinfo.SeedId.ToString();
                        seedinfolist.Add(seedinfo);
                    }
                }
                dt.Clear();
                dt.Dispose();
                if (idlist.Length > 0) idlist = idlist.Substring(1);
                cache.AddObject("/BT/SeedList/Top" + seedtype.ToString("00"), idlist, 7200);
                return ProcessSeedInfoList(nowuserid, seedinfolist, false);
            }
            else
            {
                List<PTSeedinfoShort> seedinfolist = new List<PTSeedinfoShort>();
                DataTable dt = DatabaseProvider.GetInstance().GetSeedInfoList(c);
                foreach (DataRow dr in dt.Rows)
                {
                    PTSeedinfoShort seedinfo = LoadSeedInfoShort(dr);
                    if (seedinfo.SeedId > 0)
                    {
                        seedinfolist.Add(seedinfo);
                    }
                }
                return ProcessSeedInfoList(nowuserid, seedinfolist, false);
            }
        }
        public static void CleanTopSeedIdListCache(int seedtype)
        {
            DNTCache cache = DNTCache.GetCacheService();
            cache.RemoveObject("/BT/SeedList/Top" + seedtype.ToString("00"));
            cache.RemoveObject("/BT/SeedList/Top" + 0.ToString("00"));
        }
        /// <summary>
        /// 获得指定条件的种子计数
        /// </summary>
        /// <param name="seedtype">种子种类</param>
        /// <param name="userid">用户id</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成</param>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种，0全部</param>
        /// <returns></returns>
        public static int GetSeedInfoCount(int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin)
        {
            if (keywords == "" && notin == "" && seedstat == 0 && userid == 0)
            {
                DNTCache cache = DNTCache.GetCacheService();
                string c = cache.RetrieveObject("/BT/SeedList/Count" + seedtype.ToString("00")) as string;
                if (c == null)
                {
                    int cc = DatabaseProvider.GetInstance().GetSeedInfoCount(seedtype, 0, 0, 0, "", 0, "");
                    cache.AddObject("/BT/SeedList/Count" + seedtype.ToString("00"), cc.ToString(), 7200);
                    return cc;
                } 
                else return TypeConverter.StrToInt(c);
            }
            else return DatabaseProvider.GetInstance().GetSeedInfoCount(seedtype, userid, userstat, seedstat,
                keywords, keywordsmode,
                PTTools.SQLFullContentIndexKeywordsProcess(notin, -2)
                );
        }



        /// <summary>
        /// 生成种子名称
        /// </summary>
        /// <param name="seedinfo"></param>
        public static string FillSeedTitle(PTSeedinfo seedinfo)
        {
            string publishtype = PrivateBT.Type2Str(seedinfo.Type);

            if (publishtype == "movie")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info10, seedinfo.Info11, seedinfo.Info1, seedinfo.Info2, seedinfo.Info4, seedinfo.Info13, seedinfo.Info5, seedinfo.Info14);
            }
            else if (publishtype == "tv")
            {
                TitleFilter(ref seedinfo);

                return FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info8, seedinfo.Info7, seedinfo.Info6);
            }
            else if (publishtype == "comic")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info1, seedinfo.Info5, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info9, seedinfo.Info10, seedinfo.Info8, seedinfo.Info7);
            }
            else if (publishtype == "music")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info5, seedinfo.Info3, seedinfo.Info4, seedinfo.Info7, seedinfo.Info8, seedinfo.Info9);
            }
            else if (publishtype == "game")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info5, seedinfo.Info6);
            }
            else if (publishtype == "discovery")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info3, seedinfo.Info1, seedinfo.Info2, seedinfo.Info7, seedinfo.Info6, seedinfo.Info5);
            }
            else if (publishtype == "sport")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info4, seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info5, seedinfo.Info7, seedinfo.Info6);
            }
            else if (publishtype == "entertainment")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info5, seedinfo.Info9, seedinfo.Info8, seedinfo.Info7);
            }
            else if (publishtype == "software")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info4, seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info5);
            }
            else if (publishtype == "staff")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info3, seedinfo.Info1, seedinfo.Info2, seedinfo.Info4, seedinfo.Info5);
            }
            else if (publishtype == "video")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info2, seedinfo.Info3, seedinfo.Info4, seedinfo.Info8, seedinfo.Info7, seedinfo.Info6);
            }
            else if (publishtype == "other")
            {
                TitleFilter(ref seedinfo);
                return FillTitle(seedinfo.Info1, seedinfo.Info2, seedinfo.Info3, seedinfo.Info4);
            }
            else
            {
                return "错误的种子分类";
            }
        }
        /// <summary>
        /// 过滤标题信息
        /// </summary>
        /// <param name="seedinfo"></param>
        private static void TitleFilter(ref PTSeedinfo seedinfo)
        {
            seedinfo.Info1 = seedinfo.Info1.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info2 = seedinfo.Info2.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info3 = seedinfo.Info3.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info4 = seedinfo.Info4.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info5 = seedinfo.Info5.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info6 = seedinfo.Info6.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info7 = seedinfo.Info7.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info8 = seedinfo.Info8.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info9 = seedinfo.Info9.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info10 = seedinfo.Info10.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info11 = seedinfo.Info11.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info12 = seedinfo.Info12.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info13 = seedinfo.Info13.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
            seedinfo.Info14 = seedinfo.Info14.Replace("【", "").Replace("】", "").Replace("〖", "").Replace("〗", "").Replace("『", "").Replace("』", "").Replace("[", "").Replace("]", "").Trim();
        }
        /// <summary>
        /// 生成标题，由参数列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string FillTitle(params string[] list)
        {
            string title = "";
            foreach (string str in list)
            {
                if (str != "") title += "[" + str.Trim() + "]";
            }
            return title;
        }
        /// <summary>
        /// 更改种子的类型，同时更改info列表的排序
        /// </summary>
        /// <param name="seedinfo"></param>
        public static void ChangeSeedType(ref PTSeedinfo seedinfo, int newtype)
        {
            PT_SeedTagInfo taginfo = LoadTagInfofromSeedInfo(seedinfo);
            seedinfo.Type = newtype;
            LoadSeedInfofromTagInfo(taginfo, ref seedinfo);
            seedinfo.TopicTitle = FillSeedTitle(seedinfo);
            return;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        //未经修改的程序

        /// <summary>
        /// 在数据库中创建种子，返回种子id，若失败则返回-1
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public static int CreateSeed(PTSeedinfo seedinfo)
        {
            //if (seedinfo.InfoHash.Length != 40) return -1;
            return DatabaseProvider.GetInstance().InsertSeed(seedinfo);
        }
        //此函数已作废，由updateseedstatus取代
        ///// <summary>
        ///// 删除种子记录[bt_seed]
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //public static bool DeleteSeed(int seedid)
        //{

        //    if (DatabaseProvider.GetInstance().UpdateSeedStatus(seedid, 4) < 1) 
        //    {
        //        //错误处理
        //        PTError.InsertErrorLog("删除种子失败，未能更新bt_seed_tracker表，seedid " + seedid.ToString());
        //        return false;
        //    }
        //    return true;
        //}

                /// <summary>
        /// 更新种子最后完成时间
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static int UpdateSeedLastFinish(int seedid, int coincount)
        {
            return DatabaseProvider.GetInstance().UpdateSeedLastFinish(seedid, coincount);
        }
        /// <summary>
        /// announce页面更新bt_seed表，种子（开始、完成、停止）动作，需要更新上传下载数，除ipv6、上传数、下载数数据以外均为增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public static int UpdateSeedAnnounce(int seedid, int upload, int download, int finished, int ipv6, decimal uploadtraffic, decimal downloadtraffic, bool add, int oldup, int olddown)
        {
            return DatabaseProvider.GetInstance().UpdateSeedAnnounce(seedid, upload, download, finished, ipv6, uploadtraffic, downloadtraffic, add, oldup, olddown);
        }
        /// <summary>
        /// announce页面更新bt_seed表，非增量，均为绝对数值
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished">小于零时，不更新完成数</param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public static int UpdateSeedAnnounce(int seedid, int upload, int download, int finished, int ipv6)
        {
            if (finished < 0)
            {
                return DatabaseProvider.GetInstance().UpdateSeedAnnounce(seedid, upload, download, ipv6);
            }
            else
            {
                return DatabaseProvider.GetInstance().UpdateSeedAnnounce(seedid, upload, download, finished, ipv6);
            }
        }
        /// <summary>
        /// announce页面更新bt_seed表，种子中间状态更新，只更新上传数，增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public static int UpdateSeedAnnounceUpTrafficOnly(int seedid, decimal uploadtraffic)
        {
            return DatabaseProvider.GetInstance().UpdateSeedAnnounceUpTrafficOnly(seedid, uploadtraffic);
        }
        /// <summary>
        /// announce页面更新bt_seed表，种子中间状态更新，只更新下载数，增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        public static int UpdateSeedAnnounceTrafficOnly(int seedid, decimal uploadtraffic, decimal downloadtraffic)
        {
            return DatabaseProvider.GetInstance().UpdateSeedAnnounceTrafficOnly(seedid, uploadtraffic, downloadtraffic);
        }
        /// <summary>
        /// 【存储过程】更新种子，全部表，不包含更新种子信息
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public static int UpdateSeedEditWithOutSeed(PTSeedinfo seedinfo)
        {
            return DatabaseProvider.GetInstance().UpdateSeedEditWithOutSeed(seedinfo);
        }
        /// <summary>
        /// 【存储过程】更新种子，全部表，包含更新种子信息
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public static int UpdateSeedEditWithSeed(PTSeedinfo seedinfo)
        {
            return DatabaseProvider.GetInstance().UpdateSeedEditWithSeed(seedinfo);
        }
        /// <summary>
        /// 更新bt_seed表，指定种子的下载系数和过期时间
        /// </summary>
        /// <returns></returns>
        public static int UpdateSeedRatio(int seedid, float downloadratio, DateTime downloadratioexpiredate, float uploadratio, DateTime uploadratioexpiredate)
        {
            return DatabaseProvider.GetInstance().UpdateSeedRatio(seedid, downloadratio, downloadratioexpiredate, uploadratio, uploadratioexpiredate);
        }

        /// <summary>
        /// 更新种子状态bt_seed.status，0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        /// 当更新前状态为正常（2或3），更新后状态为删除（>3）时，将自动删除对应的帖子，并更新相关用户和板块信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns>1.成功，其他失败</returns>
        public static int UpdateSeedStatus(int seedid, int status)
        {
            PTSeedinfoShort seedinfo = GetSeedInfoShort(seedid);
            if (seedinfo.SeedId > 0)
            {
                //可以正常获取种子信息，即操作之前status只能是2或3
                int rtv = DatabaseProvider.GetInstance().UpdateSeedStatus(seedid, status);
                if (rtv > 0)
                {
                    //删除种子的行为
                    if ((seedinfo.Status == 2 || seedinfo.Status == 3) && status > 3)
                    {
                        //删除SeedFileList
                        PrivateBT.DeleteSeedFileInfo(seedid);

                        ForumInfo forum = Forums.GetForumInfo(PrivateBT.Type2Forum(seedinfo.Type));

                        //更新用户发种数目
                        PTUsers.UpdateUserInfoPublishedCount(seedinfo.Uid);

                        //删除对应的帖子
                        TopicAdmins.DeleteTopics(seedinfo.TopicId.ToString(), (byte)1, true);
                        Forums.SetRealCurrentTopics(forum.Fid);

                        //更新指定版块的最新发帖数信息
                        Forums.UpdateLastPost(forum);

                        //取消置顶种子【待改进】
                        //UpdateSeedTop(seedinfo.SeedId, false); //此事种子已经不存在，不能使用此功能
                        DatabaseProvider.GetInstance().UpdateSeedTop(seedid, false);
                        UpdateTopSeedList(seedinfo.Type);

                        //清除种子总数的缓存
                        CleanSeedCountSizeCache(seedinfo.Type);
                    }
                    else if (status == 2)
                    {
                        //清除种子总数的缓存
                        CleanSeedCountSizeCache(seedinfo.Type);
                    }

                    //更新Infohash
                    PTSeeds.UpdateSeedInfohash_c(seedid);

                    return 1;
                }
                else return -1;
            }
            //其他情况，因为seedstatus不是2/3，无法获取种子信息，直接更新种子状态到指定值
            else  
            {
                
                int rtv = DatabaseProvider.GetInstance().UpdateSeedStatus(seedid, status);

                if (status < 0)
                {
                    //更新Infohash_c
                    PTSeeds.UpdateSeedInfohash_c(seedid);
                }
                if (status == 2)
                {
                    //清除种子总数的缓存
                    CleanSeedCountSizeCache(seedinfo.Type);
                }

                return rtv;
            }
        }
        /// <summary>
        /// 根据RSS类型更新bt_seed表相应字段accrss、keeprss和pubrss
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="rssstatus"></param>
        /// <returns></returns>
        public static int UpdateSeedbyRssType(int seedid, int rsstype, int rssstatus)
        {
            if(seedid < 1) return -1;
            return DatabaseProvider.GetInstance().UpdateSeedbyRssType(seedid, rsstype, rssstatus);
        }
        /// <summary>
        /// 设置置顶种子
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="top">true置顶，false取消置顶</param>
        /// <returns>-2 种子不存在，-1 设置种子置顶失败，0 更新置顶种子列表失败，1 成功</returns>
        public static int UpdateSeedTop(int seedid, bool top)
        {
            PTSeedinfoShort seedinfo = GetSeedInfoShort(seedid);
            if (seedinfo.SeedId < 0)
            {
                //要置顶的种子已经不存在
                return -2;
            }
            if(DatabaseProvider.GetInstance().UpdateSeedTop(seedid, top) > 0)
            {
                if (UpdateTopSeedList(seedinfo.Type) > 0)
                {
                    CleanTopSeedIdListCache(seedinfo.Type);
                    return 1;
                }
                else return 0;
            }
            return -1;
        }
        /// <summary>
        /// 更新指定板块的置顶种子列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int UpdateTopSeedList(int type)
        {
            if (DatabaseProvider.GetInstance().UpdateTopSeedList(type) > 0) return 1;
            else return 0;
        }

        /// <summary>
        /// 更新seed和detail表，live和lastseeder
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="live"></param>
        /// <param name="uid"></param>
        /// <param name="usernmae"></param>
        /// <returns></returns>
        public static int UpdateSeedLive(int seedid, int live, int uid)
        {
            return DatabaseProvider.GetInstance().UpdateSeedLive(seedid, live, uid);
        }

        /// <summary>
        /// 清空种子大小和数量缓存信息
        /// </summary>
        /// <param name="seedtype"></param>
        public static void CleanSeedCountSizeCache(int seedtype)
        {
            DNTCache cache = DNTCache.GetCacheService();
            cache.RemoveObject("/BT/SeedList/SumSize" + seedtype.ToString("00"));
            cache.RemoveObject("/BT/SeedList/SumSize" + 0.ToString("00"));
            cache.RemoveObject("/BT/SeedList/Count" + seedtype.ToString("00"));
            cache.RemoveObject("/BT/SeedList/Count" + 0.ToString("00"));
        }
        /// <summary>
        /// 获得种子总大小
        /// </summary>
        /// <param name="type">种子种类</param>
        /// <param name="userid">用户id</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成</param>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种</param>
        /// <param name="orderby">排序：1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间</param>
        /// <param name="keywords">搜索关键词</param>
        /// <returns></returns>
        public static decimal GetSeedSumSize(int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin)
        {
            if (keywords == "" && notin == "" && seedstat == 0 && userid == 0)
            {
                DNTCache cache = DNTCache.GetCacheService();
                string c = cache.RetrieveObject("/BT/SeedList/SumSize" + seedtype.ToString("00")) as string;
                if (c == null)
                {
                    decimal cc = DatabaseProvider.GetInstance().GetSeedSumSize(seedtype, 0, 0, 0, "", 0, "");
                    cache.AddObject("/BT/SeedList/SumSize" + seedtype.ToString("00"), cc.ToString(), 7200);
                    return cc;
                }
                else return TypeConverter.StrToDecimal(c);
            }
            else return DatabaseProvider.GetInstance().GetSeedSumSize(seedtype, userid, userstat, seedstat,
                keywords, keywordsmode,
                PTTools.SQLFullContentIndexKeywordsProcess(notin, -2)
                );
        }

        /// <summary>
        /// 按照设定的时间更新上传下载系数，蓝种60天后取消，小于4G的计算全部下载，大于4G的计算60%；所有过期时间均在设置流量系数时设置好，此处仅需选择到期种子，并将系数置为1
        /// </summary>
        public static void UpdateSeedRatio()
        {
            //下载系数调整
            DataTable dt = DatabaseProvider.GetInstance().GetSeedIdListDownloadRatioExpire(DateTime.Now);
            foreach (DataRow dr in dt.Rows)
            {
                int seedid = Convert.ToInt32(dr["seedid"].ToString());
                PTSeedinfoShort seedinfo = GetSeedInfo(seedid);
                float newdownloadratio = 1.0f;
                if (seedinfo.DownloadRatioExpireDate < DateTime.Now)
                {
                    //流量系数为0、0.3的种子，过期之后设置为0.6，其余种子，过期之后设置为1.0
                    if (seedinfo.DownloadRatio < 0.5 && seedinfo.FileSize > 4 * 1024 * 1024 * 1024M) newdownloadratio = 0.6f;
                    DatabaseProvider.GetInstance().UpdateSeedRatio(seedid, newdownloadratio, new DateTime(9999,12,31), seedinfo.UploadRatio, seedinfo.UploadRatioExpireDate);
                    PrivateBT.InsertSeedModLog(seedid, string.Format("下载流量系数期限已到，系统自动将设置下载流量系数为{0}", newdownloadratio), "系统", "流量系数期限已到", 0, 3);
                }
            }
            dt.Dispose();
            dt = null;

            //上传系数调整
            dt = DatabaseProvider.GetInstance().GetSeedIdListUploadRatioExpire(DateTime.Now);
            foreach (DataRow dr in dt.Rows)
            {
                int seedid = Convert.ToInt32(dr["seedid"].ToString());
                PTSeedinfoShort seedinfo = GetSeedInfo(seedid);
                float newuploadratio = 1.0f;
                if (seedinfo.UploadRatioExpireDate < DateTime.Now)
                {
                    DatabaseProvider.GetInstance().UpdateSeedRatio(seedid, seedinfo.DownloadRatio, seedinfo.DownloadRatioExpireDate, newuploadratio, new DateTime(9999,12,31));
                    PrivateBT.InsertSeedModLog(seedid, string.Format("上传流量系数期限已到，系统自动将设置上传流量系数为{0}", newuploadratio), "系统", "流量系数期限已到", 0, 3);
                }
            }
            dt.Dispose();
            dt = null;
        }
        /// <summary>
        /// 清理，1天内生存小于2小时，7天内生存小于32小时，80天无生存的种子
        /// </summary>
        public static void DeleteSeedNoSeed()
        {
            DateTime datelimit = DateTime.Now.AddDays(-1);
            int livelimit = 3600 * 2;
            DataTable dt = DatabaseProvider.GetInstance().GetSeedIdListNoSeed(datelimit, livelimit);
            foreach (DataRow dr in dt.Rows)
            {
                int seedid = Utils.StrToInt(dr["seedid"].ToString(), -1);
                if (seedid > 0)
                {
                    PTSeedinfoShort btseedinfo = PTSeeds.GetSeedInfoShort(seedid);

                    if (UpdateSeedStatus(seedid, 4) > 0)
                    {
                        //PrivateBT.DeleteSeedFileInfo(seedid);
                        UserInfo __userinfo = Users.GetUserInfo(btseedinfo.Uid);
                        //计算并更新金币值
                        float extcredit2paynum = -2f * 1024 * 1024 * 1024;

                        if ((float)(btseedinfo.FileSize * 2M) > -extcredit2paynum) extcredit2paynum = -(float)(btseedinfo.FileSize * 2M);

                        if (extcredit2paynum < 0)
                        {
                            Users.UpdateUserExtCredits(__userinfo.Uid, 3, extcredit2paynum);
                            CreditsLogs.AddCreditsLog(__userinfo.Uid, __userinfo.Uid, 3, 3, -extcredit2paynum, 0, Utils.GetDateTime(), 5);
                        }
                        string seedopmessage = "该种子因为“上传后24小时内存活时间小于2小时”，被系统自动执行 删除 操作";
                        seedopmessage += (extcredit2paynum < 0 ? ("，扣除发种者" + btseedinfo.UserName + " 上传流量 " + PTTools.Upload2Str(-extcredit2paynum)) : "");
                        PrivateBT.InsertSeedModLog(seedid, seedopmessage, "系统", "上传后24小时内存活时间小于2小时", 0, 4);
                        string reason = "上传后24小时内存活时间小于2小时";
                        string operationName = "删除";
                        PrivateBT.MessagePost(__userinfo.Uid, __userinfo.Username, btseedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());
                    }
                }
            }
            dt.Dispose();
            dt = null;

            datelimit = DateTime.Now.AddDays(-7);
            livelimit = 3600 * 16;
            dt = DatabaseProvider.GetInstance().GetSeedIdListNoSeed(datelimit, livelimit);
            foreach (DataRow dr in dt.Rows)
            {
                int seedid = Utils.StrToInt(dr["seedid"].ToString(), -1);
                if (seedid > 0)
                {
                    PTSeedinfoShort btseedinfo = PTSeeds.GetSeedInfoShort(seedid);
                    ForumInfo forum = Forums.GetForumInfo(PrivateBT.Type2Forum(btseedinfo.Type));
                    if (UpdateSeedStatus(seedid, 4) > 0)
                    {
                        //PrivateBT.DeleteSeedFileInfo(seedid);
                        UserInfo __userinfo = Users.GetUserInfo(btseedinfo.Uid);
                        //计算并更新金币值
                        float extcredit2paynum = -(float)(btseedinfo.FileSize);

                        if (extcredit2paynum < 0)
                        {
                            Users.UpdateUserExtCredits(__userinfo.Uid, 3, extcredit2paynum);
                            CreditsLogs.AddCreditsLog(__userinfo.Uid, __userinfo.Uid, 3, 3, -extcredit2paynum, 0, Utils.GetDateTime(), 5);
                        }
                        string seedopmessage = "该种子因为“上传后1周内存活时间小于16小时”，被系统自动执行 删除 操作";
                        seedopmessage += (extcredit2paynum < 0 ? ("，扣除发种者" + btseedinfo.UserName + " 上传流量" + PTTools.Upload2Str(-extcredit2paynum)) : "");
                        PrivateBT.InsertSeedModLog(seedid, seedopmessage, "系统", "上传后1周内存活时间小于16小时", 0, 4);
                        string reason = "上传后1周内存活时间小于16小时";
                        string operationName = "删除";
                        PrivateBT.MessagePost(__userinfo.Uid, __userinfo.Username, btseedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());
                    }
                }
            }
            dt.Dispose();
            dt = null;

            datelimit = DateTime.Now.AddDays(-7);
            livelimit = 3600 * 32;
            dt = DatabaseProvider.GetInstance().GetSeedIdListNoSeed(datelimit, livelimit);
            foreach (DataRow dr in dt.Rows)
            {
                int seedid = Utils.StrToInt(dr["seedid"].ToString(), -1);
                if (seedid > 0)
                {
                    PTSeedinfoShort btseedinfo = PTSeeds.GetSeedInfoShort(seedid);
                    ForumInfo forum = Forums.GetForumInfo(PrivateBT.Type2Forum(btseedinfo.Type));
                    if (UpdateSeedStatus(seedid, 4) > 0)
                    {
                        //PrivateBT.DeleteSeedFileInfo(seedid);
                        UserInfo __userinfo = Users.GetUserInfo(btseedinfo.Uid);
                        //计算并更新金币值
                        float extcredit2paynum = 0;

                        if (extcredit2paynum < 0)
                        {
                            Users.UpdateUserExtCredits(__userinfo.Uid, 3, extcredit2paynum);
                            CreditsLogs.AddCreditsLog(__userinfo.Uid, __userinfo.Uid, 3, 3, -extcredit2paynum, 0, Utils.GetDateTime(), 5);
                        }
                        string seedopmessage = "该种子因为“上传后1周内存活时间小于32小时”，被系统自动执行 删除 操作";
                        seedopmessage += (extcredit2paynum < 0 ? ("，扣除发种者" + btseedinfo.UserName + " 上传流量" + PTTools.Upload2Str(-extcredit2paynum)) : "");
                        PrivateBT.InsertSeedModLog(seedid, seedopmessage, "系统", "上传后一周内存活时间小于32小时", 0, 4);
                        string reason = "上传后1周内存活时间小于32小时";
                        string operationName = "删除";
                        PrivateBT.MessagePost(__userinfo.Uid, __userinfo.Username, btseedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());
                    }
                }
            }
            dt.Dispose();
            dt = null;

            datelimit = DateTime.Now.AddDays(-80);
            dt = DatabaseProvider.GetInstance().GetSeedIdListNoSeed(datelimit);
            foreach (DataRow dr in dt.Rows)
            {
                int seedid = Utils.StrToInt(dr["seedid"].ToString(), -1);
                if (seedid > 0)
                {
                    PTSeedinfoShort btseedinfo = PTSeeds.GetSeedInfoShort(seedid);
                    ForumInfo forum = Forums.GetForumInfo(PrivateBT.Type2Forum(btseedinfo.Type));
                    if (UpdateSeedStatus(seedid, 3) > 0)
                    {
                        //UserInfo __userinfo = Users.GetUserInfo(btseedinfo.Uid);

                        //string seedopmessage = "该种子因为“连续80天无人做种”，被系统自动执行 删除 操作";
                        //PrivateBT.InsertSeedModLog(seedid, seedopmessage, "系统", "连续80天无人做种", 0, 4);
                        //string reason = "连续80天无人做种";
                        //string operationName = "删除";
                        //if (btseedinfo.Uid > 0)
                        //{
                        //    //发送消息
                        //    PrivateBT.MessagePost(__userinfo.Uid, __userinfo.Username, btseedinfo.TopicTitle, operationName, seedopmessage, reason, DateTime.Now.ToString());

                        //    //更新用户发种数目
                        //    PrivateBT.UpdateUserInfoPublishedCount(__userinfo.Uid);
                        //}
                    }
                }
            }
            dt.Dispose();
            dt = null;
        }
        /// <summary>
        /// 更新种子浏览量(准确值，从topic表获取)
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <returns>成功返回1，否则返回0</returns>
        public static int UpdateSeedTopicViewCountAccurate(int tid, int seedid)
        {
            return DatabaseProvider.GetInstance().UpdateSeedTopicViewCountAccurate(tid, seedid);
        }
        /// <summary>
        /// 获得种子每小时的做种奖励
        /// </summary>
        /// <param name="filesize">种子大小</param>
        /// <param name="liveseconds">种子存活时间</param>
        /// <param name="uploadcount">共同做种者数量</param>
        /// <returns></returns>
        public static decimal GetSeedAward(decimal seedize, int liveseconds, int uploadcount)
        {
            if (uploadcount < 1) uploadcount = 1;
            decimal seedaward = (decimal)((double)seedize * (Math.Log(liveseconds / 1.0 / 60 / 60 / 24 + 1) / 1000 / uploadcount));
            if (seedaward > 1024M * 1024M) return seedaward;
            else return 1024M * 1024M;
        }

        /// <summary>
        /// 获得种子每小时的做种奖励
        /// </summary>
        /// <param name="filesize">种子大小</param>
        /// <param name="liveseconds">种子存活时间</param>
        /// <param name="keepsecond">用户总计保种时间</param>
        /// <param name="uploadcount">共同做种者数量</param>
        /// <returns></returns>
        public static decimal GetSeedAward(decimal seedize, decimal useruptraffic, int liveseconds, int keepsecond, int uploadcount,
            DateTime lastfinish, string joindate)
        {
            double a = 0, b = 0, c = 0, d = 0, e = 0, all = 0;
            return PTSeeds.GetSeedAward(seedize, useruptraffic, liveseconds, keepsecond, uploadcount,
                lastfinish, (DateTime.Now - TypeConverter.StrToDateTime(joindate, new DateTime(1990, 1, 1))).TotalDays < 60,
                ref a, ref b, ref c, ref d, ref e, ref all);
        }
                /// <summary>
        /// 获得种子每小时的做种奖励
        /// </summary>
        /// <param name="filesize">种子大小</param>
        /// <param name="liveseconds">种子存活时间</param>
        /// <param name="keepsecond">用户总计保种时间</param>
        /// <param name="uploadcount">共同做种者数量</param>
        /// <returns></returns>
        public static decimal GetSeedAward(decimal seedize, decimal useruptraffic, int liveseconds, int keepsecond, int uploadcount, 
            DateTime lastfinish, string joindate,
            ref double a, ref double b, ref double c, ref double d, ref double e, ref double all)
        {
            return PTSeeds.GetSeedAward(seedize, useruptraffic, liveseconds, keepsecond, uploadcount, 
                lastfinish, (DateTime.Now - TypeConverter.StrToDateTime(joindate, new DateTime(1990, 1, 1))).TotalDays < 60,
                ref a, ref b, ref c, ref d, ref e, ref all);
        }

        /// <summary>
        /// 获得种子每小时的做种奖励
        /// </summary>
        /// <param name="filesize">种子大小</param>
        /// <param name="liveseconds">种子存活时间</param>
        /// <param name="keepsecond">用户总计保种时间</param>
        /// <param name="uploadcount">共同做种者数量</param>
        /// <returns></returns>
        public static decimal GetSeedAward(decimal seedize, decimal useruptraffic, int liveseconds, int keepsecond, int uploadcount,
            DateTime lastfinish, bool newuser,
            ref double a, ref double b, ref double c, ref double d, ref double e, ref double all)
        {
            if (uploadcount < 1) uploadcount = 1;

            a = 0; b = 0; c = 0; d = 0; e = 0; all = 0;

            //种子存活时间 a
            if (liveseconds < 3600 * 24 * 15) { if (!newuser) { a = 0; } else { a = 1; } }
            else if (liveseconds < 3600 * 24 * 60) { a = 1.0; }
            else if (liveseconds < 3600 * 24 * 240) { a = 1.2; }
            else if (liveseconds < 3600 * 24 * 720) { a = 1.4; }
            else { a = 1.6; }

            //用户保种时间 b
            if (keepsecond < 3600 * 24 * 3) { if (!newuser) { b = 0.6; } else { b = 1; } }
            else if (keepsecond < 3600 * 24 * 15) { b = 1.0; }
            else if (keepsecond < 3600 * 24 * 60) { b = 1.2; }
            else if (keepsecond < 3600 * 24 * 240) { b = 1.4; }
            else if (keepsecond < 3600 * 24 * 720) { b = 1.6; }
            else { b = 1.8; }

            //用户种子上传量系数 c
            decimal upratio = useruptraffic / seedize;
            if (upratio < 0.2M) { if (!newuser) { c = 0.2; } else { c = 1; } }
            else if (upratio < 1M) { if (!newuser) { c = 0.8; } else { c = 1; } }
            else if (upratio < 5M) { c = 1.2; }
            else if (upratio < 10M) { c = 1.6; }
            else if (upratio < 20M) { c = 2.0; }
            else { c = 2.4; }

            //种子最近完成系数 d
            double days = (DateTime.Now - lastfinish).TotalDays;
            if (days > 180) { if (!newuser) { d = 0.2; } else { d = 1; } }
            else if (days > 120) { if (!newuser) { d = 0.6; } else { d = 1; } }
            else if (days > 60) { d = 1.0; }
            else if (days > 30) { d = 1.4; }
            else { d = 1.8; }

            //上传数 e
            if (uploadcount > 30) { if (!newuser) { e = 0; } else { e = 1; } }
            else if (uploadcount > 25) { if (!newuser) { e = 0.2; } else { e = 1; } }
            else if (uploadcount > 20) { if (!newuser) { e = 0.4; } else { e = 1; } }
            else if (uploadcount > 15) { if (!newuser) { e = 0.6; } else { e = 1; } }
            else if (uploadcount > 10) { if (!newuser) { e = 0.8; } else { e = 1; } }
            else if (uploadcount > 5) { if (!newuser) { e = 1.0; } else { e = 1; } }
            else { e = 1.2; }

            all = a * b * c * d * e;

            return seedize * (decimal)all / 1000M;;
        }
        /// <summary>
        /// 获得种子指定时间内的做种奖励
        /// </summary>
        /// <param name="filesize">种子大小</param>
        /// <param name="liveseconds">种子存活时间</param>
        /// <param name="uploadcount">共同做种者数量</param>
        /// <param name="seedseconds">做种时间</param>
        /// <returns></returns>
        public static decimal GetSeedAward(decimal seedize, int liveseconds, int uploadcount, int seedseconds)
        {
            return GetSeedAward(seedize, liveseconds, uploadcount) * (decimal)(seedseconds) / 3600m;
        }

        /// <summary>
        /// 更新指定seedid种子的原始Infohash_c信息（若infohash信息为空，读取torrent文件并获取infohash_c）
        /// </summary>
        /// <param name="seedid"></param>
        public static void UpdateSeedInfohash_c(int seedid)
        {
            if (seedid < 1) return;

            // 获取该种子的信息
            PTSeedinfo seedinfo = PTSeeds.GetSeedInfoFullAllStatus(seedid);
            
            // 如果该种子不存在
            if (seedinfo.SeedId < 1) return;
            
            //如果infohash_c已经存在且种子为正常状态或禁止状态（禁止状态保留infohash_c）
            if (seedinfo.Status == 2 || seedinfo.Status == 3 || seedinfo.Status == 6)
            {
                if (seedinfo.InfoHash_c.Length == 40) return;
            }
            //种子被删除状态
            else if (seedinfo.Status == 1 || seedinfo.Status == 4 || seedinfo.Status == 5)
            {
                //if (seedinfo.InfoHash_c.Length > 7 && seedinfo.InfoHash_c.Substring(0, 7) == "DELETE_") return;
                //else
                {
                    if (seedinfo.InfoHash_c.Length > 20)
                    {
                        seedinfo.InfoHash_c = "DELETE_" + seedid.ToString() + seedinfo.InfoHash_c.Substring(0, 20);
                    }
                    else
                    {
                        seedinfo.InfoHash_c = "DELETE_" + seedid.ToString();
                    }
                    PTSeeds.UpdateSeedEditWithSeed(seedinfo);
                    return;
                }
            }
            //上传失败
            else if (seedinfo.Status <= 0)
            {
                //if (seedinfo.InfoHash_c.Length > 7 && seedinfo.InfoHash_c.Substring(0, 7) == "FAILED_") return;
                //else
                {
                    if (seedinfo.InfoHash_c.Length > 20)
                    {
                        seedinfo.InfoHash_c = "FAILED_" + seedid.ToString() + seedinfo.InfoHash_c.Substring(0, 20);
                    }
                    else
                    {
                        seedinfo.InfoHash_c = "FAILED_" + seedid.ToString();
                    }
                    PTSeeds.UpdateSeedEditWithSeed(seedinfo);
                    return;
                }
            }

            //检查附件是否存在
            if (System.IO.File.Exists(seedinfo.Path))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(seedinfo.Path, FileMode.Open);
                }
                catch
                {
                    System.Threading.Thread.Sleep(333);
                    try
                    {
                        fs = new FileStream(seedinfo.Path, FileMode.Open);
                    }
                    catch
                    {
                        if (fs != null)
                        {
                            // 关闭文件
                            fs.Close();
                            fs.Dispose();
                        }
                        return;
                    }
                }

                try
                {
                    //大小限制
                    if (fs.Length > 2147483648) return;

                    byte[] buffer = new Byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    fs.Close();

                    string seedhead = "";
                    string tracker1 = "http://buaabt.cn";

                    //添加主Tracker
                    seedhead = string.Format("d8:announce{0}:{1}", tracker1.Length, tracker1);

                    byte[] output = new byte[buffer.Length + seedhead.Length];
                    Array.Copy(System.Text.Encoding.UTF8.GetBytes(seedhead), 0, output, 0, seedhead.Length);
                    Array.Copy(buffer, 0, output, seedhead.Length, buffer.Length);

                    //重新获取并更新seedinfo
                    seedinfo = PTSeeds.GetSeedInfoFullAllStatus(seedinfo.SeedId);
                    seedinfo.InfoHash_c = PTTorrent.GetSeedInfohash_c(output);
                    if (seedinfo.InfoHash_c == "ERROR") seedinfo.InfoHash_c = seedinfo.InfoHash_c + "2_" + seedid.ToString();
                    PTSeeds.UpdateSeedEditWithSeed(seedinfo);

                    buffer = null;
                    output = null;

                }
                catch (Exception ex)
                {
                    PTLog.InsertSystemLog(PTLog.LogType.SeedProcess, PTLog.LogStatus.Exception, "SEED_HASH_C", ex.ToString());
                }

                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            else
            {
                seedinfo.InfoHash_c = "ERROR0_" + seedid.ToString();
                PTSeeds.UpdateSeedEditWithSeed(seedinfo);
            }
        }
    }








    public partial class PrivateBT
    {
       




        ///// <summary>
        ///// 更新数据库中的种子信息，返回更改行数
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public static bool UpdateSeed_Uploader(PrivateBTSeedInfo seedinfo)
        //{
        //    if (DatabaseProvider.GetInstance().UpdateSeed_Uploader(seedinfo) > 0) return true;
        //    else return false;
        //}
        ///// <summary>
        ///// 更新数据库中的种子TopicID，返回更改行数
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public static bool UpdateSeed_TopicID(PrivateBTSeedInfo seedinfo)
        //{
        //    if (DatabaseProvider.GetInstance().UpdateSeed_TopicID(seedinfo) > 0) return true;
        //    else return false;
        //}




        ///// <summary>
        ///// 获得种子数，NOLOCK，可能脏读
        ///// </summary>
        ///// <param name="userstat">用户状态：1上传，2下载，3发布，4完成，0时用户id失效</param>
        ///// <returns></returns>
        //public static int GetUserSeedCount(int userid, int userstat)
        //{
        //    return DatabaseProvider.GetInstance().GetUserSeedCount(userid, userstat);
        //}


        
        ///// <summary>
        ///// 设置置顶种子
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <param name="settop">true置顶，false取消置顶</param>
        ///// <returns></returns>
        //public static bool TopSeed(int seedid, bool settop)
        //{
        //    if (seedid < 1) return false;
        //    if (DatabaseProvider.GetInstance().TopSeed(seedid, settop, PTTools.Time2Int(DateTime.Now)) > 0) return true;
        //    else return false;
        //}
        ///// <summary>
        ///// 更新数据库中的种子信息，返回更改行数,finished/live/traffic为增量
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public static int UpdateSeed_Tracker(PrivateBTSeedInfo seedinfo)
        //{
        //    return DatabaseProvider.GetInstance().UpdateSeed_Tracker(seedinfo);
        //}
        ///// <summary>
        ///// 更新数据库中的种子信息，返回更改行数,finished/live/traffic为增量
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //public static int UpdateSeed_Tracker(PrivateBTSeedInfo seedinfo, bool IsSeed)
        //{
        //    if (IsSeed)
        //        return DatabaseProvider.GetInstance().UpdateSeed_TrackerUp(seedinfo);
        //    else
        //        return DatabaseProvider.GetInstance().UpdateSeed_TrackerDown(seedinfo);
        //}

        ///// <summary>
        ///// 设置种子的上传下载系数和过期时间
        ///// </summary>
        //public static int UpdateSeedRatio(int seedid, double downloadratio, double uploadratio, DateTime downloadratioexpiredate, DateTime uploadratioexpiredate)
        //{
        //    return DatabaseProvider.GetInstance().UpdateSeedRatio(seedid, downloadratio, uploadratio, downloadratioexpiredate, uploadratioexpiredate);
        //}

        
        ///// <summary>
        ///// 发帖出错后恢复帖子数
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="forumid"></param>
        ///// <returns></returns>
        //public static int UpdateSeedPublishError(int userid, int forumid)
        //{
        //    return DatabaseProvider.GetInstance().UpdateSeedPublishError(userid, forumid);
        //}
        

        
    }
}