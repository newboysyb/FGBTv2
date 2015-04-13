using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Cache;
using Discuz.Common.Generic;

namespace Discuz.Forum
{
    public partial class PrivateBT
    {
        /// <summary>
        /// 获取论坛统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public static PrivateBTAllStats GetAllStatsHTML(string statstype, int userid)
        {
            PrivateBTAllStats tempstats = GetAllStatsHTML(statstype);
            tempstats.StatsValue = tempstats.StatsValue.Replace("<li><a href=\"userinfo-" + userid.ToString() + ".aspx\"", "<li style=\"background-color:#FC6\"><a href=\"userinfo-" + userid + ".aspx\"");
            //tempstats.StatsValue.Replace("<li><a href=\"userinfo-1.aspx\"", "<li style=\"background-color:#FC6\"><a href=\"userinfo-1.aspx\"");
            return tempstats;
        }
        /// <summary>
        /// 获取论坛统计信息缓存，含当前uid
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public static PrivateBTAllStats GetAllStatsHTML(string statstype)
        {
            PrivateBTAllStats newstats = new PrivateBTAllStats();
            IDataReader reader = DatabaseProvider.GetInstance().GetAllStatsHTML(statstype);
            string errmsg = "";
            if (reader.Read())
            {
                newstats.StatsValue = Utils.HtmlDecode(reader["statsvalue"].ToString());
                newstats.LastUpdate = DateTime.Parse(reader["lastupdate"].ToString());
                newstats.UpdateLock = TypeConverter.ObjectToInt(reader["updatelock"], 0);
                newstats.NextUpdate = newstats.LastUpdate.AddMinutes(5);
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            if(newstats.StatsValue != null && newstats.StatsValue != "")
            {
                //缓存过期，锁定后更新
                if ((DateTime.Now - newstats.LastUpdate).TotalSeconds > 5 * 60 && newstats.UpdateLock == 0)
                {
                    if (DatabaseProvider.GetInstance().LockAllStatsHTML(statstype) > 0)
                    {
                        try
                        {
                            newstats.StatsValue = GetAllStatsHTMLNew(statstype, -1);
                            DatabaseProvider.GetInstance().UpdateAllStatsHTML(statstype, Utils.HtmlEncode(newstats.StatsValue)); //自动解锁
                            newstats.LastUpdate = DateTime.Now;
                            newstats.NextUpdate = newstats.LastUpdate.AddMinutes(5);
                        }
                        catch (System.Exception ex)
                        {
                            errmsg = ex.ToString();
                        }
                        DatabaseProvider.GetInstance().UnLockAllStatsHTML(statstype);
                    }
                }
                //超期锁定，强制更新
                else if ((DateTime.Now - newstats.LastUpdate).TotalSeconds > 15 * 60)
                {
                    try
                    {
                        newstats.StatsValue = GetAllStatsHTMLNew(statstype, -1);
                        DatabaseProvider.GetInstance().UpdateAllStatsHTML(statstype, Utils.HtmlEncode(newstats.StatsValue)); //自动解锁
                        newstats.LastUpdate = DateTime.Now;
                        newstats.NextUpdate = newstats.LastUpdate.AddMinutes(5);
                    }
                    catch (System.Exception ex)
                    {
                        errmsg = ex.ToString();
                        
                    }
                    DatabaseProvider.GetInstance().UnLockAllStatsHTML(statstype);
                }
            }
            else //数据库中没有该缓存
            {
                try
                {
                    newstats.StatsValue = GetAllStatsHTMLNew(statstype, -1);
                    DatabaseProvider.GetInstance().InstertAllStatsHTML(statstype, Utils.HtmlEncode(newstats.StatsValue));
                    newstats.LastUpdate = DateTime.Now;
                    newstats.NextUpdate = newstats.LastUpdate.AddMinutes(5);
                }
                catch (System.Exception ex)
                {
                    errmsg = ex.ToString();
                    
                }
                DatabaseProvider.GetInstance().UnLockAllStatsHTML(statstype);
            }

            //调试
            //newstats.StatsValue = GetAllStatsHTMLNew(statstype, -1);
            //newstats.LastUpdate = DateTime.Now;
            //newstats.NextUpdate = DateTime.Now;

            return newstats;
        }

        /// <summary>
        /// 获取更新的HTML格式论坛统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public static string GetAllStatsHTMLNew(string statstype, int userid)
        {
            switch (statstype)
            {
                case "Default_Basic":
                    return GetAllStatsHTML_DefaultBasic();
                case "Default_Forum":
                    return GetAllStatsHTML_DefaultForum();
                case "Default_Traffic":
                    return GetAllStatsHTML_DefaultTraffic();
                case "Default_MonthTraffic":
                    return GetAllStatsHTML_DefaultMonthTraffic();
                case "Views_Week":
                    return GetAllStatsHTML_ViewsWeek();
                case "Views_Hour":
                    return GetAllStatsHTML_ViewsHour();
                case "Client_OS":
                    return GetAllStatsHTML_ClientOS();
                case "Client_Browser":
                    return GetAllStatsHTML_ClientBrowser();
                case "Posts_Month":
                    return GetAllStatsHTML_PostsMonth();
                case "Posts_Day":
                    return GetAllStatsHTML_PostsDay();
                case "ForumsRank":
                    return GetAllStatsHTML_ForumsRank();
                case "TopicsRank":
                    return GetAllStatsHTML_TopicsRank();
                case "PostsRank":
                    return GetAllStatsHTML_PostsRank(userid);
                case "CreditsRank":
                    return GetAllStatsHTML_CreditsRank(userid);
                case "UpDownRank":
                    return GetAllStatsHTML_UpDownRank(userid);
                case "OnlineTimeRank":
                    return GetAllStatsHTML_OnlineTimeRank(userid);
                case "RatioRank":
                    return GetAllStatsHTML_RatioRank();
                //break;
                default:
                    return "";
            }
        }
        /// <summary>
        /// 获取统计信息HTML 共享率排行
        /// </summary>
        public static string GetAllStatsHTML_RatioRank()
        {
            int i = 1;
            StringBuilder sb1 = new StringBuilder();

            DataTable dt = DatabaseProvider.GetInstance().GetUserRatioList(false);
            foreach (DataRow dr in dt.Rows)
            {
                sb1.AppendFormat("<li><a href=\"userinfo-{0}.aspx\" target=\"_blank\"><em>{2}</em><span style=\"color:#F00\">{3}</span> {1}</a></li>", dr["uid"].ToString().Trim(), dr["username"].ToString().Trim(), PTTools.Ratio2Str(decimal.Parse(dr["extcredits3"].ToString()), decimal.Parse(dr["extcredits4"].ToString().Trim())), i.ToString("000"));
                i++;
            }
            dt.Clear();
            dt.Dispose();
            dt = null;


            i = 1;
            StringBuilder sb2 = new StringBuilder();

            dt = DatabaseProvider.GetInstance().GetUserRatioList(true);
            foreach (DataRow dr in dt.Rows)
            {
                sb2.AppendFormat("<li><a href=\"userinfo-{0}.aspx\" target=\"_blank\"><em>{2}</em><span style=\"color:#F00\">{3}</span> {1}</a></li>", dr["uid"].ToString().Trim(), dr["username"].ToString().Trim(), PTTools.Ratio2Str(decimal.Parse(dr["extcredits3"].ToString()), decimal.Parse(dr["extcredits4"].ToString().Trim())), i.ToString("000"));
                i++;
            }
            dt.Clear();
            dt.Dispose();
            dt = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");
            sb3.Append("	<thead>");
            sb3.Append("		<tr class=\"colplural\">");
            sb3.Append("			<td>最佳共享者排行</td>");
            sb3.Append("			<td>最差共享者排行</td>");
            sb3.Append("		</tr>");
            sb3.Append("	</thead>");
            sb3.Append("	<tbody>");
            sb3.Append("		<tr>");
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", sb1.ToString());
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", sb2.ToString());
            sb3.Append("		</tr>");
            sb3.Append("	</tbody>");
            sb3.Append("</table>");

            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 在线时间排行
        /// </summary>
        public static string GetAllStatsHTML_OnlineTimeRank(int userid)
        {
            ShortUserInfo[] total = Stats.GetUserOnlinetime("total");
            ShortUserInfo[] thismonth = Stats.GetUserOnlinetime("thismonth");

            int maxrows = Math.Max(total.Length, thismonth.Length);

            string totalonlinerank = Stats.GetUserRankHtml(total, "onlinetime", maxrows, userid);
            string thismonthonlinerank = Stats.GetUserRankHtml(thismonth, "onlinetime", maxrows, userid);

            total = null;
            thismonth = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");
            sb3.Append("	<thead>");
            sb3.Append("		<tr class=\"colplural\">");
            sb3.Append("			<td>总在线时间排行(小时)</td>");
            sb3.Append("			<td>本月在线时间排行(小时)</td>");
            sb3.Append("		</tr>");
            sb3.Append("	</thead>");
            sb3.Append("	<tbody>");
            sb3.Append("		<tr>");
            sb3.AppendFormat("			<td><ul>{0}</ul></td>",totalonlinerank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>",thismonthonlinerank);
            sb3.Append("		</tr>");
            sb3.Append("	</tbody>");
            sb3.Append("</table>");

            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 上传下载排行
        /// </summary>
        public static string GetAllStatsHTML_UpDownRank(int userid)
        {
            ShortUserInfo[] credits = Stats.GetUserArray("credits"); ;
            ShortUserInfo[][] extendedcredits = Stats.GetExtsRankUserArray();

            int maxrows = 200;

            string extcreditsrank3 = Stats.GetUserRankHtml(extendedcredits[2], "extcredits3", maxrows, userid);
            string extcreditsrank4 = Stats.GetUserRankHtml(extendedcredits[3], "extcredits4", maxrows, userid);
            string extcreditsrank5 = Stats.GetUserRankHtml(extendedcredits[4], "extcredits5", maxrows, userid);
            string extcreditsrank6 = Stats.GetUserRankHtml(extendedcredits[5], "extcredits6", maxrows, userid);
            //string extcreditsrank7 = Stats.GetUserRankHtml(extendedcredits[6], "extcredits7", maxrows, userid);
            //string extcreditsrank8 = Stats.GetUserRankHtml(extendedcredits[7], "extcredits8", maxrows, userid);

            extendedcredits[0] = null;
            extendedcredits[1] = null;
            extendedcredits[2] = null;
            extendedcredits[3] = null;
            extendedcredits[4] = null;
            extendedcredits[5] = null;
            extendedcredits[6] = null;
            extendedcredits[7] = null;
            extendedcredits = null;
            credits = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");
            sb3.Append("	<thead>");
            sb3.Append("		<tr class=\"colplural\">");
            sb3.Append("			<td>上传 排行榜</td>");
            sb3.Append("			<td>下载 排行榜</td>");
            sb3.Append("			<td>今天上传 排行榜</td>");
            sb3.Append("			<td>今天下载 排行榜</td>");
            sb3.Append("		</tr>");
            sb3.Append("	</thead>");
            sb3.Append("	<tbody>");
            sb3.Append("		<tr>");
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", extcreditsrank3);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", extcreditsrank4);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", extcreditsrank5);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", extcreditsrank6);
            sb3.Append("		</tr>");
            sb3.Append("	</tbody>");
            sb3.Append("</table>");

            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 积分排行
        /// </summary>
        public static string GetAllStatsHTML_CreditsRank(int userid)
        {
            ShortUserInfo[] credits = Stats.GetUserArray("credits"); ;
            ShortUserInfo[][] extendedcredits = Stats.GetExtsRankUserArray();

            int maxrows = 200;
            //maxrows = Math.Max(credits.Length, maxrows);
            //maxrows = Math.Max(extendedcredits[0].Length, maxrows);
            //maxrows = Math.Max(extendedcredits[1].Length, maxrows);
            //maxrows = Math.Max(extendedcredits[2].Length, maxrows);
            //maxrows = Math.Max(extendedcredits[3].Length, maxrows);
            //maxrows = Math.Max(extendedcredits[4].Length, maxrows);
            //maxrows = Math.Max(extendedcredits[5].Length, maxrows);
            //maxrows = Math.Max(extendedcredits[6].Length, maxrows);
            //maxrows = Math.Max(extendedcredits[7].Length, maxrows);

            string creditsrank = Stats.GetUserRankHtml(credits, "credits", maxrows, userid);
            string extcreditsrank1 = Stats.GetUserRankHtml(extendedcredits[0], "extcredits1", maxrows, userid);
            string extcreditsrank2 = Stats.GetUserRankHtml(extendedcredits[1], "extcredits2", maxrows, userid);
            string extcreditsrank3 = Stats.GetUserRankHtml(extendedcredits[2], "extcredits3", maxrows, userid);
            //string extcreditsrank4 = Stats.GetUserRankHtml(extendedcredits[3], "extcredits4", maxrows, userid);
            //string extcreditsrank5 = Stats.GetUserRankHtml(extendedcredits[4], "extcredits5", maxrows, userid);
            //string extcreditsrank6 = Stats.GetUserRankHtml(extendedcredits[5], "extcredits6", maxrows, userid);
            //string extcreditsrank7 = Stats.GetUserRankHtml(extendedcredits[6], "extcredits7", maxrows, userid);
            //string extcreditsrank8 = Stats.GetUserRankHtml(extendedcredits[7], "extcredits8", maxrows, userid);

            extendedcredits[0] = null;
            extendedcredits[1] = null;
            extendedcredits[2] = null;
            extendedcredits[3] = null;
            extendedcredits[4] = null;
            extendedcredits[5] = null;
            extendedcredits[6] = null;
            extendedcredits[7] = null;
            extendedcredits = null;
            credits = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");
            sb3.Append("	<thead>");
            sb3.Append("		<tr class=\"colplural\">");
            sb3.Append("			<td>积分 排行榜</td>");
            sb3.Append("			<td>威望 排行榜</td>");
            sb3.Append("			<td>金币 排行榜</td>");
            sb3.Append("			<td>上传 排行榜</td>");
            sb3.Append("		</tr>");
            sb3.Append("	</thead>");
            sb3.Append("	<tbody>");
            sb3.Append("		<tr>");
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", creditsrank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", extcreditsrank1);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", extcreditsrank2);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", extcreditsrank3);
            sb3.Append("		</tr>");
            sb3.Append("	</tbody>");
            sb3.Append("</table>");

            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 帖子排行
        /// </summary>
        public static string GetAllStatsHTML_PostsRank(int userid)
        {
            ShortUserInfo[] posts = Stats.GetUserArray("posts");
            ShortUserInfo[] digestposts = Stats.GetUserArray("digestposts");
            ShortUserInfo[] thismonth = Stats.GetUserArray("thismonth");
            ShortUserInfo[] today = Stats.GetUserArray("today");

            int maxrows = 0;
            maxrows = Math.Max(posts.Length, maxrows);
            maxrows = Math.Max(digestposts.Length, maxrows);
            maxrows = Math.Max(thismonth.Length, maxrows);
            maxrows = Math.Max(today.Length, maxrows);

            string postsrank = Stats.GetUserRankHtml(posts, "posts", maxrows, userid);
            string digestpostsrank = Stats.GetUserRankHtml(digestposts, "digestposts", maxrows, userid);
            string thismonthpostsrank = Stats.GetUserRankHtml(thismonth, "thismonth", maxrows, userid);
            string todaypostsrank = Stats.GetUserRankHtml(today, "today", maxrows, userid);

            posts = null;
            digestposts = null;
            thismonth = null;
            today = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");
            sb3.Append("	<thead>");
            sb3.Append("		<tr class=\"colplural\">");
            sb3.Append("			<td>发帖 排行榜</td>");
            sb3.Append("			<td>精华帖 排行榜</td>");
            sb3.Append("			<td>最近 30 天发帖 排行榜</td>");
            sb3.Append("			<td>最近 24 小时发帖 排行榜</td>");
            sb3.Append("		</tr>");
            sb3.Append("	</thead>");
            sb3.Append("	<tbody>");
            sb3.Append("		<tr>");
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", postsrank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", digestpostsrank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", thismonthpostsrank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", todaypostsrank);
            sb3.Append("		</tr>");
            sb3.Append("	</tbody>");
            sb3.Append("</table>");

            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 帖子排行
        /// </summary>
        public static string GetAllStatsHTML_TopicsRank()
        {
            string hottopics = Stats.GetHotTopicsHtml();
            string hotreplytopics = Stats.GetHotReplyTopicsHtml();

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");
            sb3.Append("	<thead>");
            sb3.Append("		<tr class=\"colplural\">");
            sb3.Append("			<td>被浏览最多的主题</td>");
            sb3.Append("			<td>被回复最多的主题</td>");
            sb3.Append("		</tr>");
            sb3.Append("	</thead>");
            sb3.Append("	<tbody>");
            sb3.Append("		<tr>");
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", hottopics);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", hotreplytopics);
            sb3.Append("		</tr>");
            sb3.Append("	</tbody>");
            sb3.Append("</table>");

            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 论坛排行
        /// </summary>
        public static string GetAllStatsHTML_ForumsRank()
        {
            List<ForumInfo> topics = Stats.GetForumArray("topics");
            List<ForumInfo> posts = Stats.GetForumArray("posts");
            List<ForumInfo> thismonth = Stats.GetForumArray("thismonth");
            List<ForumInfo> today = Stats.GetForumArray("today");

            int maxrows = 0;

            maxrows = Math.Max(topics.Count, maxrows);
            maxrows = Math.Max(posts.Count, maxrows);
            maxrows = Math.Max(thismonth.Count, maxrows);
            maxrows = Math.Max(today.Count, maxrows);

            string topicsforumsrank = Stats.GetForumsRankHtml(topics, "topics", maxrows);
            string postsforumsrank = Stats.GetForumsRankHtml(posts, "posts", maxrows);
            string thismonthforumsrank = Stats.GetForumsRankHtml(thismonth, "thismonth", maxrows);
            string todayforumsrank = Stats.GetForumsRankHtml(today, "today", maxrows);

            topics = null;
            posts = null;
            thismonth = null;
            today = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");
            sb3.Append("	<thead>");
            sb3.Append("		<tr class=\"colplural\">");
            sb3.Append("			<td>发帖 排行榜</td>");
            sb3.Append("			<td>回复 排行榜</td>");
            sb3.Append("			<td>最近 30 天发帖 排行榜</td>");
            sb3.Append("			<td>最近 24 小时发帖 排行榜</td>");
            sb3.Append("		</tr>");
            sb3.Append("	</thead>");
            sb3.Append("	<tbody>");
            sb3.Append("		<tr>");
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", topicsforumsrank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", postsforumsrank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", thismonthforumsrank);
            sb3.AppendFormat("			<td><ul>{0}</ul></td>", todayforumsrank);
            sb3.Append("		</tr>");
            sb3.Append("	</tbody>");
            sb3.Append("</table>");

            return sb3.ToString();
        }

        /// <summary>
        /// 获取统计信息HTML 发帖量-天
        /// </summary>
        public static string GetAllStatsHTML_PostsDay()
        {
            string monthrec = "";
            string maxmonth = "";
            int maxmonthpost = 0;
            int totalpost = 0;
            int temp = 0;
            string tempmonth = "";
            string temppost = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetDayPostsStats(Posts.GetPostTableId());
            while (reader.Read())
            {
                tempmonth = reader["year"].ToString() + "-" + Utils.StrToInt(reader["month"], 1).ToString("00") + "-" + Utils.StrToInt(reader["day"], 1).ToString("00");
                monthrec += tempmonth + "," + reader["count"].ToString().Trim() + ",";
                temp = int.Parse(reader["count"].ToString());
                totalpost += temp;
                if (temp > maxmonthpost)
                {
                    maxmonthpost = temp;
                    maxmonth = tempmonth;
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");

            for (int i = 0; i < 100; i++)
            {
                tempmonth = GetFirstString(ref monthrec);
                temppost = GetFirstString(ref monthrec);
                if (tempmonth == "" || temppost == "") break;
                temp = int.Parse(temppost);
                if (tempmonth == maxmonth)
                    sb3.AppendFormat("<tr><th width=\"100\"><strong>{0}</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;<strong>{2} ({3}%)</strong></td></tr>",
                    tempmonth, (int)(370 * (maxmonthpost == 0 ? 0.0 : ((double)temp / (double)maxmonthpost))), temp, ((double)Math.Round((double)temp * 100 / (double)(totalpost == 0 ? 1 : totalpost), 2)));
                else
                    sb3.AppendFormat("<tr><th width=\"100\">{0}</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;{2} ({3}%)</td></tr>",
                    tempmonth, (int)(370 * (maxmonthpost == 0 ? 0.0 : ((double)temp / (double)maxmonthpost))), temp, ((double)Math.Round((double)temp * 100 / (double)(totalpost == 0 ? 1 : totalpost), 2)));

            }
            sb3.Append("</table>");
            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 发帖量-月份
        /// </summary>
        public static string GetAllStatsHTML_PostsMonth()
        {
            string monthrec = "";
            string maxmonth = "";
            int maxmonthpost = 0;
            int totalpost = 0;
            int temp = 0;
            string tempmonth = "";
            string temppost = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetMonthPostsStats(Posts.GetPostTableId());
            while (reader.Read())
            {
                tempmonth = reader["year"].ToString() + "-" + Utils.StrToInt(reader["month"], 1).ToString("00");
                monthrec += tempmonth + "," + reader["count"].ToString().Trim() + ",";
                temp = int.Parse(reader["count"].ToString());
                totalpost += temp;
                if (temp > maxmonthpost)
                {
                    maxmonthpost = temp;
                    maxmonth = tempmonth;
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");

            for (int i = 0; i < 100; i++)
            {
                tempmonth = GetFirstString(ref monthrec);
                temppost = GetFirstString(ref monthrec);
                if (tempmonth == "" || temppost == "") break;
                temp = int.Parse(temppost);
                if (tempmonth == maxmonth)
                    sb3.AppendFormat("<tr><th width=\"100\"><strong>{0}</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;<strong>{2} ({3}%)</strong></td></tr>",
                        tempmonth, (int)(370 * (maxmonthpost == 0 ? 0.0 : ((double)temp / (double)maxmonthpost))), temp, ((double)Math.Round((double)temp * 100 / (double)(totalpost == 0 ? 1 : totalpost), 2)));
                else
                    sb3.AppendFormat("<tr><th width=\"100\">{0}</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;{2} ({3}%)</td></tr>",
                    tempmonth, (int)(370 * (maxmonthpost == 0 ? 0.0 : ((double)temp / (double)maxmonthpost))), temp, ((double)Math.Round((double)temp * 100 / (double)(totalpost == 0 ? 1 : totalpost), 2)));
            }
            sb3.Append("</table>");
            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 客户端-浏览器
        /// </summary>
        public static string GetAllStatsHTML_ClientBrowser()
        {
            string weekrec = "";
            string maxweek = "";
            int maxweektraffic = 0;
            int totaltraffic = 0;
            int temp = 0;
            string tempweek = "";
            string temptraffic = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetAllStats("browser");
            while (reader.Read())
            {
                weekrec += reader["variable"].ToString().Trim() + "," + reader["count"].ToString().Trim() + ",";
                temp = int.Parse(reader["count"].ToString());
                totaltraffic += temp;
                if (temp > maxweektraffic)
                {
                    maxweektraffic = temp;
                    maxweek = reader["variable"].ToString().Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");

            for (int i = 0; i < 100; i++)
            {
                tempweek = GetFirstString(ref weekrec);
                temptraffic = GetFirstString(ref weekrec);
                if (tempweek == "" || temptraffic == "") break;
                temp = int.Parse(temptraffic);
                if (tempweek == maxweek)
                    sb3.AppendFormat("<tr><th width=\"100\"><strong><img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;<strong>{2} ({3}%)</strong></td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));
                else
                    sb3.AppendFormat("<tr><th width=\"100\">" +
                    "<img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;{2} ({3}%)</td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));

            }
            sb3.Append("</table>");
            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 客户端-操作系统
        /// </summary>
        public static string GetAllStatsHTML_ClientOS()
        {
            string weekrec = "";
            string maxweek = "";
            int maxweektraffic = 0;
            int totaltraffic = 0;
            int temp = 0;
            string tempweek = "";
            string temptraffic = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetAllStats("os");
            while (reader.Read())
            {
                weekrec += reader["variable"].ToString().Trim() + "," + reader["count"].ToString().Trim() + ",";
                temp = int.Parse(reader["count"].ToString());
                totaltraffic += temp;
                if (temp > maxweektraffic)
                {
                    maxweektraffic = temp;
                    maxweek = reader["variable"].ToString().Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");

            for (int i = 0; i < 100; i++)
            {
                tempweek = GetFirstString(ref weekrec);
                temptraffic = GetFirstString(ref weekrec);
                if (tempweek == "" || temptraffic == "") break;
                temp = int.Parse(temptraffic);
                
                if (tempweek == maxweek)
                    sb3.AppendFormat("<tr><th width=\"100\"><strong><img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;<strong>{2} ({3}%)</strong></td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));
                else
                    sb3.AppendFormat("<tr><th width=\"100\">" +
                    "<img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;{2} ({3}%)</td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));

                //if (tempweek == maxweek)
                //    sb3.AppendFormat("<tr><th width=\"100\"><strong>" +
                //    "<img src='images/stats/" + tempweek + ".gif ' border='0' alt='" + tempweek + "' title='" + tempweek + "' />&nbsp;" +
                //        tempweek +
                //    "</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;<strong>" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</strong></td></tr>";
                //else
                //    sb3.AppendFormat("<tr><th width=\"100\">" +
                //    "<img src='images/stats/" + tempweek + ".gif ' border='0' alt='" + tempweek + "' title='" + tempweek + "' />&nbsp;" +
                //    tempweek +
                //    "</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</td></tr>";

            }
            sb3.Append("</table>");
            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 流量统计-时段流量
        /// </summary>
        public static string GetAllStatsHTML_ViewsHour()
        {
            string weekrec = "";
            string maxweek = "";
            int maxweektraffic = 0;
            int totaltraffic = 0;
            int temp = 0;
            string tempweek = "";
            string temptraffic = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetAllStats("hour");
            while (reader.Read())
            {
                weekrec += reader["variable"].ToString().Trim() + "," + reader["count"].ToString().Trim() + ",";
                temp = int.Parse(reader["count"].ToString());
                totaltraffic += temp;
                if (temp > maxweektraffic)
                {
                    maxweektraffic = temp;
                    maxweek = reader["variable"].ToString().Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");

            for (int i = 0; i < 100; i++)
            {
                tempweek = GetFirstString(ref weekrec);
                temptraffic = GetFirstString(ref weekrec);
                if (tempweek == "" || temptraffic == "" || tempweek.Length != 2) break;
                temp = int.Parse(temptraffic);
                
                if (tempweek == maxweek)
                    sb3.AppendFormat("<tr><th width=\"100\"><strong><img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;<strong>{2} ({3}%)</strong></td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));
                else
                    sb3.AppendFormat("<tr><th width=\"100\">" +
                    "<img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;{2} ({3}%)</td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));

                
                //if (tempweek == maxweek)
                //    sb3.AppendFormat("<tr><th width=\"100\"><strong>" +
                //    tempweek + "-" + (int.Parse(tempweek) + 1).ToString("00") +
                //    "</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;<strong>" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</strong></td></tr>";
                //else
                //    sb3.AppendFormat("<tr><th width=\"100\">" +
                //    tempweek + "-" + (int.Parse(tempweek) + 1).ToString("00") +
                //    "</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</td></tr>";

            }
            sb3.Append("</table>");
            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 流量统计-周流量
        /// </summary>
        public static string GetAllStatsHTML_ViewsWeek()
        {
            string weekrec = "";
            string maxweek = "";
            int maxweektraffic = 0;
            int totaltraffic = 0;
            int temp = 0;
            string tempweek = "";
            string temptraffic = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetAllStats("week");
            while (reader.Read())
            {
                weekrec += reader["variable"].ToString().Trim() + "," + reader["count"].ToString().Trim() + ",";
                temp = int.Parse(reader["count"].ToString());
                totaltraffic += temp;
                if (temp > maxweektraffic)
                {
                    maxweektraffic = temp;
                    maxweek = reader["variable"].ToString().Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");

            for (int i = 0; i < 100; i++)
            {
                tempweek = GetFirstString(ref weekrec);
                temptraffic = GetFirstString(ref weekrec);
                if (tempweek == "" || temptraffic == "" || tempweek.Length != 1) break;
                temp = int.Parse(temptraffic);

                if (tempweek == maxweek)
                    sb3.AppendFormat("<tr><th width=\"100\"><strong><img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;<strong>{2} ({3}%)</strong></td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));
                else
                    sb3.AppendFormat("<tr><th width=\"100\">" +
                    "<img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;{2} ({3}%)</td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));


                //if (tempweek == maxweek)
                //    sb3.AppendFormat("<tr><th width=\"100\"><strong>" +
                //    GetWeekDay(tempweek) +
                //    "</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;<strong>" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</strong></td></tr>";
                //else
                //    sb3.AppendFormat("<tr><th width=\"100\">" +
                //    GetWeekDay(tempweek) +
                //    "</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</td></tr>";

            }
            sb3.Append("</table>");
            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 默认页-月份流量
        /// </summary>
        public static string GetAllStatsHTML_DefaultMonthTraffic()
        {
            //月份流量
            string monthrec = "";
            string maxweek = "";
            int maxweektraffic = 0;
            int totaltraffic = 0;
            int temp = 0;
            string tempweek = "";
            string temptraffic = "";
            IDataReader reader = DatabaseProvider.GetInstance().GetAllStats("month");
            while (reader.Read())
            {
                monthrec += reader["variable"].ToString().Trim() + "," + reader["count"].ToString().Trim() + ",";
                temp = int.Parse(reader["count"].ToString());
                totaltraffic += temp;
                if (temp > maxweektraffic)
                {
                    maxweektraffic = temp;
                    maxweek = reader["variable"].ToString().Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\">");

            for (int i = 0; i < 100; i++)
            {
                tempweek = GetFirstString(ref monthrec);
                temptraffic = GetFirstString(ref monthrec);
                if (tempweek == "" || temptraffic == "" || tempweek.Length != 6) break;
                temp = int.Parse(temptraffic);

                if (tempweek == maxweek)
                    sb3.AppendFormat("<tr><th width=\"100\"><strong><img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;<strong>{2} ({3}%)</strong></td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));
                else
                    sb3.AppendFormat("<tr><th width=\"100\">" +
                    "<img src='images/stats/{0}.gif ' border='0' alt='{0}' title='{0}' />&nbsp;{0}</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:{1}px'>&nbsp;</div></div>&nbsp;{2} ({3}%)</td></tr>",
                    tempweek, (int)(370 * (maxweektraffic == 0 ? 0.0 : ((double)temp / (double)maxweektraffic))), temp, ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)));


                //if (tempm == maxmonth)
                //    sb3.AppendFormat("<tr><th width=\"100\"><strong>" +
                //    tempm.Substring(0, 4) + "-" + tempm.Substring(4, 2) +
                //    "</strong></th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxmonthtraffic == 0 ? 0.0 : ((double)temp / (double)maxmonthtraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;<strong>" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</strong></td></tr>";
                //else
                //    sb3.AppendFormat("<tr><th width=\"100\">" +
                //    tempm.Substring(0, 4) + "-" + tempm.Substring(4, 2) +
                //    "</th><td style='text-align:left'><div class='optionbar left'><div class='pollcolor5' style='width:" +
                //    (int)(370 * (maxmonthtraffic == 0 ? 0.0 : ((double)temp / (double)maxmonthtraffic))) +
                //    "px'>&nbsp;</div></div>&nbsp;" +
                //    temp + " (" + ((double)Math.Round((double)temp * 100 / (double)(totaltraffic == 0 ? 1 : totaltraffic), 2)) + "%)</td></tr>";
            }
            sb3.Append("</table>");
            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 默认页-流量概况
        /// </summary>
        public static string GetAllStatsHTML_DefaultTraffic()
        {
            int total_hit = 0;
            int total_visitors = 0;
            int total_members = 0;
            int total_guest = 0;

            //总流量
            IDataReader reader = DatabaseProvider.GetInstance().GetAllStats("total");
            while (reader.Read())
            {
                if (reader["variable"].ToString().Trim() == "guests") total_guest = int.Parse(reader["count"].ToString());
                else if (reader["variable"].ToString().Trim() == "hits") total_hit = int.Parse(reader["count"].ToString());
                else if (reader["variable"].ToString().Trim() == "members") total_members = int.Parse(reader["count"].ToString());
            }
            total_visitors = total_guest + total_members;
            double pageviewavg = (double)Math.Round((double)total_hit / (double)(total_visitors == 0 ? 1 : total_visitors), 2);
            reader.Close();
            reader.Dispose();
            reader = null;

            //月份流量
            string maxmonth = "";
            int maxmonthpost = 0;
            int temp = 0;
            reader = DatabaseProvider.GetInstance().GetAllStats("month");
            while (reader.Read())
            {
                temp = int.Parse(reader["count"].ToString());
                if (temp > maxmonthpost)
                {
                    maxmonthpost = temp;
                    maxmonth = reader["variable"].ToString().Trim();
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            maxmonth = maxmonth.Substring(0, 4) + "年" + maxmonth.Substring(4, 2) + "月";

            //时段流量
            int maxhour = 0;
            int maxhourpost = 0;
            reader = DatabaseProvider.GetInstance().GetAllStats("hour");
            while (reader.Read())
            {
                temp = int.Parse(reader["count"].ToString());
                if (temp > maxhourpost)
                {
                    maxhourpost = temp;
                    maxhour = int.Parse(reader["variable"].ToString());
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\" style=\"margin-bottom: 10px);\">");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">总页面流量</td>");
            sb3.AppendFormat("		<td>{0}</td>", total_hit);
            sb3.Append("		<td class=\"t_th\">访问量最多的月份</td>");
            sb3.AppendFormat("		<td>{0}</td>", maxmonth);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">共计来访</td>");
            sb3.AppendFormat("		<td>{0} 人次</td>", total_visitors);
            sb3.Append("		<td class=\"t_th\">月份总页面流量</td>");
            sb3.AppendFormat("		<td>{0}</td>", maxmonthpost);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">会员</td>");
            sb3.AppendFormat("		<td>{0}</td>", total_members);
            sb3.Append("		<td class=\"t_th\">时段</td>");
            sb3.AppendFormat("		<td>{0} - {1}</td>", maxhour, (maxhour + 1));
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">游客</td>");
            sb3.AppendFormat("		<td>{0}</td>", total_guest);
            sb3.Append("		<td class=\"t_th\">时段总页面流量</td>");
            sb3.AppendFormat("		<td>{0}</td>", maxhourpost);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">平均每人浏览</td>");
            sb3.AppendFormat("		<td>{0}</td>", pageviewavg);
            sb3.Append("		<td class=\"t_th\">&nbsp);</td>");
            sb3.Append("		<td>&nbsp);</td>");
            sb3.Append("	</tr>");
            sb3.Append("</table>");

            return sb3.ToString();
        }

        /// <summary>
        /// 获取统计信息HTML 默认页-论坛统计
        /// </summary>
        public static string GetAllStatsHTML_DefaultForum()
        {
            int members = Stats.GetMemberCount();//注册会员 
            int posts = Stats.GetPostCount();//发帖数
            int forums = Stats.GetForumCount();//版块数
            ForumInfo hotforum = Stats.GetHotForum();//最热门论坛
            int topics = Stats.GetTopicCount();//主题数
            string postsaddtoday = Stats.GetTodayPostCount().ToString();//今天发帖
            string membersaddtoday = Stats.GetTodayNewMemberCount().ToString();//今天注册会员
            //string monthpostsofstatsbar = string.Empty;
            //string daypostsofstatsbar = string.Empty;
            //string monthofstatsbar = string.Empty;
            int runtime = (DateTime.Now - Convert.ToDateTime("2009-09-06 00:00:00")).Days;
            double topicreplyavg = (double)Math.Round((double)(posts - topics) / (double)topics, 2);
            double postsaddavg = (double)Math.Round((double)posts / (double)runtime, 2);
            double membersaddavg = members / runtime;
            //string activeindex = ((Math.Round(membersaddavg / (double)(members == 0 ? 1 : members), 2) + Math.Round(postsaddavg / (double)(posts == 0 ? 1 : posts), 2)) * 1500.00 + topicreplyavg * 10.00 + mempostavg + Math.Round(mempostpercent / 10.00, 2) + pageviewavg).ToString();

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\" style=\"margin-bottom: 10px);\">");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">版块数</td>");
            sb3.AppendFormat("		<td>{0}</td>", forums);
            sb3.Append("		<td class=\"t_th\">平均每日新增帖子数</td>");
            sb3.AppendFormat("		<td>{0}</td>", postsaddavg);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">最热门版块</td>");
            sb3.AppendFormat("		<td><a href=\\showforum-{0}.aspx\" target=\"_blank\">{1}</a></td>",hotforum.Fid, hotforum.Name);
            sb3.Append("		<td class=\"t_th\">主题数</td>");
            sb3.AppendFormat("		<td>{0}</td>", topics);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">平均每日注册会员数</td>");
            sb3.AppendFormat("		<td>{0}</td>", membersaddavg);
            sb3.Append("		<td class=\"t_th\">主题数</td>");
            sb3.AppendFormat("		<td>{0}</td>", hotforum.Topics);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">帖子数</td>");
            sb3.AppendFormat("		<td>{0}</td>", posts);
            sb3.Append("		<td class=\"t_th\">最近24小时新增帖子数</td>");
            sb3.AppendFormat("		<td>{0}</td>", postsaddtoday);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">帖子数</td>");
            sb3.AppendFormat("		<td>{0}</td>", hotforum.Posts);
            sb3.Append("		<td class=\"t_th\">平均每个主题被回复次数</td>");
            sb3.AppendFormat("		<td{0}</td>", topicreplyavg);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">今日新增会员数</td>");
            sb3.AppendFormat("		<td>{0}</td>", membersaddtoday);
            sb3.Append("		<td class=\"t_th\"></td>");
            sb3.Append("		<td></td>");
            sb3.Append("	</tr>");
            sb3.Append("</table>");

            return sb3.ToString();
        }
        /// <summary>
        /// 获取统计信息HTML 默认页-基本状况
        /// </summary>
        public static string GetAllStatsHTML_DefaultBasic()
        {
            int members = Stats.GetMemberCount();//注册会员        
            int memnonpost = Stats.GetNonPostMemCount();//未发帖会员
            int mempost = members - memnonpost;//发帖会员
            string admins = Stats.GetAdminCount().ToString();//管理成员人数
            string lastmember = Statistics.GetStatisticsRowItem("lastusername");//最新会员
            string bestmem = "";//今日之星
            int bestmemposts = 0;//今日之星发帖
            Stats.GetBestMember(out bestmem, out bestmemposts);
            int posts = Stats.GetPostCount();//发帖数
            double mempostavg = (double)Math.Round((double)posts / (double)members, 2); //平均每人发帖数
            double mempostpercent = (double)Math.Round((double)(mempost * 100) / (double)members, 2); //发帖会员占总数

            //Stats.UpdateStatVars("main", "runtime", runtime.ToString());
            //Stats.UpdateStatVars("main", "postsaddtoday", postsaddtoday);
            //Stats.UpdateStatVars("main", "membersaddtoday", membersaddtoday);
            //Stats.UpdateStatVars("main", "admins", admins);
            //Stats.UpdateStatVars("main", "memnonpost", memnonpost.ToString());
            //Stats.UpdateStatVars("main", "hotforum", SerializationHelper.Serialize(hotforum));
            //Stats.UpdateStatVars("main", "bestmem", bestmem);
            //Stats.UpdateStatVars("main", "bestmemposts", bestmemposts.ToString());

            StringBuilder sb3 = new StringBuilder();
            sb3.Append("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"datatable\" style=\"margin-bottom: 10px);\">");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">注册会员</td>");
            sb3.AppendFormat("		<td>{0}</td>", members);
            sb3.Append("		<td class=\"t_th\">发帖会员</td>");
            sb3.AppendFormat("		<td>{0}</td>", mempost);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">管理成员</td>");
            sb3.AppendFormat("		<td>{0}</td>", admins);
            sb3.Append("		<td class=\"t_th\">未发帖会员</td>");
            sb3.AppendFormat("		<td>{0}</td>", memnonpost);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">新会员</td>");
            sb3.AppendFormat("		<td>{0}</td>", lastmember);
            sb3.Append("		<td class=\"t_th\">发帖会员占总数</td>");
            sb3.AppendFormat("		<td>{0}%</td>", mempostpercent);
            sb3.Append("	</tr>");
            sb3.Append("	<tr>");
            sb3.Append("		<td class=\"t_th\">今日论坛之星</td><td>");
            if (bestmem != "") sb3.AppendFormat("<a href=\"userinfo.aspx?username={0}\">{0}</a>({1})", bestmem, bestmemposts);
            sb3.Append("		</td><td class=\"t_th\">平均每人发帖数</td>");
            sb3.AppendFormat("		<td>{0}</td>", mempostavg);
            sb3.Append("	</tr>");
            sb3.Append("</table>");

            return sb3.ToString();
        }

        /// <summary>
        /// 设置数据更新锁，防止多次更新，论坛统计信息
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public static int LockAllStatsHTML(string statstype)
        {
            return DatabaseProvider.GetInstance().LockAllStatsHTML(statstype);
        }

        /// <summary>
        /// 解除数据更新锁，论坛统计信息
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public static int UnLockAllStatsHTML(string statstype)
        {
            return DatabaseProvider.GetInstance().UnLockAllStatsHTML(statstype);
        }

        /// <summary>
        /// 更新论坛统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <param name="statsvalue"></param>
        /// <returns></returns>
        public static int UpdateAllStatsHTML(string statstype, string statsvalue)
        {
            return DatabaseProvider.GetInstance().UpdateAllStatsHTML(statstype, statsvalue);
        }
    }
}