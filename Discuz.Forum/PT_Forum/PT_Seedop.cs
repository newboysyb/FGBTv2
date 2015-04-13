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
    public partial class PrivateBT
    {

        public static PTSeedOPinfo LoadSeedOPInfo(DataRow rd)
        {
            //调用基类方法赋值
            //PTSeedinfoShort seedinfo = (PTSeedinfoShort)(new PTSeedOPinfo());
            //seedinfo = PTSeeds.LoadSeedInfoShort(dr);
            PTSeedOPinfo seedopinfo = new PTSeedOPinfo();

            seedopinfo.Download = TypeConverter.ObjectToInt(rd["download"]);
            seedopinfo.DownloadRatio = TypeConverter.ObjectToFloat(rd["downloadratio"]);
            seedopinfo.DownloadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["downloadratioexpiredate"]);
            seedopinfo.FileCount = TypeConverter.ObjectToInt(rd["filecount"]);
            seedopinfo.FileSize = TypeConverter.ObjectToDecimal(rd["filesize"]);
            seedopinfo.Finished = TypeConverter.ObjectToInt(rd["finished"]);
            //seedopinfo.InfoHash_c = rd["infohash_c"].ToString().Trim();
            seedopinfo.IPv6 = TypeConverter.ObjectToInt(rd["ipv6"]);
            seedopinfo.LastLive = TypeConverter.ObjectToDateTime(rd["lastlive"]);
            seedopinfo.Live = TypeConverter.ObjectToInt(rd["live"]);
            seedopinfo.PostDateTime = TypeConverter.ObjectToDateTime(rd["postdatetime"]);
            seedopinfo.Replies = TypeConverter.ObjectToInt(rd["replies"]);
            seedopinfo.SeedId = TypeConverter.ObjectToInt(rd["seedid"]);
            seedopinfo.Status = TypeConverter.ObjectToInt(rd["status"]);
            seedopinfo.TopicId = TypeConverter.ObjectToInt(rd["topicid"]);
            seedopinfo.TopicTitle = rd["topictitle"].ToString().Trim();
            seedopinfo.Traffic = TypeConverter.ObjectToDecimal(rd["traffic"]);
            seedopinfo.Type = TypeConverter.ObjectToInt(rd["type"]);
            seedopinfo.Uid = TypeConverter.ObjectToInt(rd["uid"]);
            seedopinfo.Upload = TypeConverter.ObjectToInt(rd["upload"]);
            seedopinfo.UploadRatio = TypeConverter.ObjectToFloat(rd["uploadratio"]);
            seedopinfo.UploadRatioExpireDate = TypeConverter.ObjectToDateTime(rd["uploadratioexpiredate"]);
            seedopinfo.UpTraffic = TypeConverter.ObjectToDecimal(rd["uptraffic"]);
            seedopinfo.UserName = rd["username"].ToString().Trim();
            seedopinfo.Views = TypeConverter.ObjectToInt(rd["views"]);

            //子类的赋值方法
            seedopinfo.OperatorId = TypeConverter.ObjectToInt(rd["operatorid"]);
            seedopinfo.Operator = rd["operator"].ToString().Trim();
            seedopinfo.OpDateTime = TypeConverter.ObjectToDateTime(rd["date"]);
            seedopinfo.OpType = TypeConverter.ObjectToInt(rd["operattype"]);
            seedopinfo.Operation = rd["operation"].ToString().Trim();
            seedopinfo.OpReason = rd["operatreason"].ToString().Trim();

            seedopinfo.Dis_OpDateTime = ForumUtils.ConvertDateTime(seedopinfo.OpDateTime.ToString());

            return seedopinfo;
        }
        /// <summary>
        /// 对种子列表进行处理，以方便显示
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<PTSeedOPinfo> ProcessSeedInfoList(List<PTSeedOPinfo> seedinfolist)
        {
            //种子列表
            string seedidlist = "";
            foreach (PTSeedinfoShort seedinfo in seedinfolist)
            {
                seedidlist += seedinfo.SeedId + ",";
            }
            if (seedidlist != "") seedidlist = seedidlist.Remove(seedidlist.Length - 2);
            else seedidlist = "0";

            foreach (PTSeedinfoShort seedinfo in seedinfolist)
            {

                //过滤单引号之后的种子名
                //dr["topictitle"] = System.Web.HttpUtility.HtmlEncode(dr["topictitle"].ToString());
                seedinfo.Dis_TopicTitleFilter = seedinfo.TopicTitle.Replace("'", "\\'").Replace("\"", "\\\"");

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
            }

            return seedinfolist;
        }
        /// <summary>
        /// 获得种子操作记录数
        /// </summary>
        /// <returns></returns>
        public static int GetSeedOPCount(int OperatorId, int OperatType, int SeedType, int userid)
        {
            return DatabaseProvider.GetInstance().GetSeedOPCount(OperatorId, OperatType, SeedType, userid);
        }

        /// <summary>
        /// 获得种子操作记录表
        /// </summary>
        /// <returns></returns>
        public static List<PTSeedOPinfo> GetSeedOPList(int OperatorId, int OperatType, int SeedType, int userid, int numperpage, int pageindex)
        {
            List<PTSeedOPinfo> seedoplist = new List<PTSeedOPinfo>();
            
            DataTable dt = DatabaseProvider.GetInstance().GetSeedOPList(OperatorId, OperatType, SeedType, userid, numperpage, pageindex);
            foreach (DataRow dr in dt.Rows)
            {
                PTSeedOPinfo seedinfo = LoadSeedOPInfo(dr);
                if (seedinfo.SeedId > 0) seedoplist.Add(seedinfo);
            }
            dt.Clear();
            dt.Dispose();
            return ProcessSeedInfoList(seedoplist);
        }
        /// <summary>
        /// 插入种子操作记录，置顶0，取消置顶1，流量系数2，取消流量系数3，删除4，自删除5，编辑6，自编辑7，替换种子8，自替换种子9，移动种子10，自移动种子11，屏蔽种子12，取消屏蔽13，奖励流量14，扣除流量15，禁止种子16，添加RSS_ACC101，删除RSS_ACC102
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="op"></param>
        /// <param name="opr"></param>
        /// <returns></returns>
        public static int InsertSeedModLog(int seedid, string op, string opr, string operatreason, int operatorid, int operattype)
        {
            if (seedid < 0) return 0;
            else return DatabaseProvider.GetInstance().InsertSeedModLog(seedid, op, opr, operatreason, operatorid, operattype, 0);
        }
        /// <summary>
        /// 插入种子操作记录，置顶0，取消置顶1，流量系数2，取消流量系数3，删除4，自删除5，编辑6，自编辑7，替换种子8，自替换种子9，移动种子10，自移动种子11，屏蔽种子12，取消屏蔽13，奖励流量14，扣除流量15，禁止种子16，添加RSS_ACC101，删除RSS_ACC102
        /// operatvalue:0 普通，  1- 60%下载    2- 30%下载    3- 0%下载
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="op"></param>
        /// <param name="opr"></param>
        /// <returns></returns>
        public static int InsertSeedModLog(int seedid, string op, string opr, string operatreason, int operatorid, int operattype, int operatvalue)
        {
            if (seedid < 0) return 0;
            else return DatabaseProvider.GetInstance().InsertSeedModLog(seedid, op, opr, operatreason, operatorid, operattype, operatvalue);
        }
        /// <summary>
        /// 获得种子操作记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public static DataTable GetSeedModLog(int seedid)
        {
            return DatabaseProvider.GetInstance().GetSeedModLog(seedid);
        }
    }
}