using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using System.Text.RegularExpressions;
//using SQLDMO;
using System.Collections.Generic;

//////////////////////////////////////////////////////////////////////////
//BT相关的SQL数据库操作

namespace Discuz.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        /// <summary>
        /// 获取论坛统计信息缓存HTML
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public IDataReader GetAllStatsHTML(string statstype)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@statstype",(DbType)SqlDbType.NChar, 20, statstype)
									   };
            string sqlstring = "SELECT TOP 1 [statsvalue],[lastupdate],[updatelock] FROM [bt_allstats] WHERE [statstype] = @statstype";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取论坛本身的统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public IDataReader GetAllStats(string statstype)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@statstype",(DbType)SqlDbType.NChar, 10, statstype)
									   };
            string sqlstring = "SELECT [variable], [count] FROM [dnt_stats] WHERE [type] = @statstype ORDER BY [variable] asc";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 设置数据更新锁，防止多次更新，论坛统计信息
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public int LockAllStatsHTML(string statstype)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@statstype",(DbType)SqlDbType.NChar, 20, statstype),	   
                                  };
            string sqlstring = "UPDATE [bt_allstats] SET [updatelock] = 1 WHERE [statstype] = @statstype AND [updatelock] = 0";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 解除数据更新锁，论坛统计信息
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        public int UnLockAllStatsHTML(string statstype)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@statstype",(DbType)SqlDbType.NChar, 20, statstype),	   
                                  };
            string sqlstring = "UPDATE [bt_allstats] SET [updatelock] = 0 WHERE [statstype] = @statstype";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新论坛统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <param name="statsvalue"></param>
        /// <returns></returns>
        public int UpdateAllStatsHTML(string statstype, string statsvalue)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@statstype",(DbType)SqlDbType.NChar, 20, statstype),
                                        DbHelper.MakeInParam("@statsvalue",(DbType)SqlDbType.Text, 0, statsvalue),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "UPDATE [bt_allstats] SET [updatelock] = 0, [statsvalue] = @statsvalue, [lastupdate] = @lastupdate  WHERE [statstype] = @statstype";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 插入论坛统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <param name="statsvalue"></param>
        /// <returns></returns>
        public int InstertAllStatsHTML(string statstype, string statsvalue)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@statstype",(DbType)SqlDbType.NChar, 20, statstype),
                                        DbHelper.MakeInParam("@statsvalue",(DbType)SqlDbType.Text, 0, statsvalue),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "INSERT INTO [bt_allstats]([statstype], [statsvalue], [lastupdate], [updatelock])  VALUES(@statstype, @statsvalue, @lastupdate, 0)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 读取BT状态
        /// </summary>
        /// <returns></returns>
        public IDataReader GetServerStatsToReader(int id)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id),
                                  };
            string sqlstring = string.Format("SELECT TOP 1 [bt_stats].* FROM [bt_stats] WHERE [id] = @id");
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 锁定BT状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int LockServerStats(int id)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id),
                                  };
            string sqlstring = "UPDATE [bt_stats] SET [lock] = 'True' WHERE [id] = @id AND [lock] = 'False'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 解除锁定BT状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UnLockServerStats(int id)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id),
                                  };
            string sqlstring = "UPDATE [bt_stats] SET [lock] = 'False' WHERE [id] = @id";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 插入BT状态
        /// </summary>
        /// <returns></returns>
        public int InsertServerStats(PrivateBTServerStats btstats)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@peercount",(DbType)SqlDbType.Int, 4, btstats.PeerCount),
                                        DbHelper.MakeInParam("@downpeercount",(DbType)SqlDbType.Int, 4, btstats.DownPeerCount),
                                        DbHelper.MakeInParam("@uppeercount",(DbType)SqlDbType.Int, 4, btstats.UpPeerCount),
                                        DbHelper.MakeInParam("@onlineusercount",(DbType)SqlDbType.Int, 4, btstats.OnlineUserCount),
                                        DbHelper.MakeInParam("@seedercount",(DbType)SqlDbType.Int, 4, btstats.SeederCount),
                                        DbHelper.MakeInParam("@leechercount",(DbType)SqlDbType.Int, 4, btstats.LeecherCount),
                                        DbHelper.MakeInParam("@onlineseedscount",(DbType)SqlDbType.Int, 4, btstats.OnlineSeedsCount),
                                        DbHelper.MakeInParam("@onlinesize",(DbType)SqlDbType.Decimal, 32, btstats.OnlineSize),
                                        DbHelper.MakeInParam("@downspeed",(DbType)SqlDbType.Decimal, 32, btstats.DownSpeed),
                                        DbHelper.MakeInParam("@alltraffic",(DbType)SqlDbType.Decimal, 32, btstats.AllTraffic),
                                        DbHelper.MakeInParam("@todaytraffic",(DbType)SqlDbType.Decimal, 32, btstats.TodayTraffic),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@allsize",(DbType)SqlDbType.Decimal, 32, btstats.AllSize),
                                        DbHelper.MakeInParam("@allseedscount",(DbType)SqlDbType.Int, 4, btstats.AllSeedsCount),
                                        DbHelper.MakeInParam("@statsupload",(DbType)SqlDbType.Decimal, 32, btstats.StatsUpload),
                                        DbHelper.MakeInParam("@statsdownload",(DbType)SqlDbType.Decimal, 32, btstats.StatsDownload),
                                        DbHelper.MakeInParam("@statsuploadtoday",(DbType)SqlDbType.Decimal, 32, btstats.StatsUploadToday),
                                        DbHelper.MakeInParam("@statsdownloadtoday",(DbType)SqlDbType.Decimal, 32, btstats.StatsDownloadToday),
                                        DbHelper.MakeInParam("@statsratio",(DbType)SqlDbType.Float, 8, btstats.StatsRatio),
                                        DbHelper.MakeInParam("@statsuploadall",(DbType)SqlDbType.Decimal, 32, btstats.StatsUploadAll),
                                  };

            string sqlstring = "INSERT INTO [bt_stats] ([peercount],[downpeercount],[uppeercount],[onlineusercount],[seedercount],[leechercount],[onlineseedscount],[onlinesize],[downspeed],[alltraffic],[todaytraffic],[lastupdate],[allsize],[allseedscount],[statsupload],[statsdownload],[statsuploadtoday],[statsdownloadtoday],[statsratio],[statsuploadall]) ";
            sqlstring += " VALUES(@peercount,@downpeercount,@uppeercount,@onlineusercount,@seedercount,@leechercount,@onlineseedscount,@onlinesize,@downspeed,@alltraffic,@todaytraffic,@lastupdate,@allsize,@allseedscount,@statsupload,@statsdownload,@statsuploadtoday,@statsdownloadtoday,@statsratio,@statsuploadall)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 插入BT状态
        /// </summary>
        /// <returns></returns>
        public int InsertServerStats(PrivateBTServerStats btstats, bool day)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@peercount",(DbType)SqlDbType.Int, 4, btstats.PeerCount),
                                        DbHelper.MakeInParam("@downpeercount",(DbType)SqlDbType.Int, 4, btstats.DownPeerCount),
                                        DbHelper.MakeInParam("@uppeercount",(DbType)SqlDbType.Int, 4, btstats.UpPeerCount),
                                        DbHelper.MakeInParam("@onlineusercount",(DbType)SqlDbType.Int, 4, btstats.OnlineUserCount),
                                        DbHelper.MakeInParam("@seedercount",(DbType)SqlDbType.Int, 4, btstats.SeederCount),
                                        DbHelper.MakeInParam("@leechercount",(DbType)SqlDbType.Int, 4, btstats.LeecherCount),
                                        DbHelper.MakeInParam("@onlineseedscount",(DbType)SqlDbType.Int, 4, btstats.OnlineSeedsCount),
                                        DbHelper.MakeInParam("@onlinesize",(DbType)SqlDbType.Decimal, 32, btstats.OnlineSize),
                                        DbHelper.MakeInParam("@downspeed",(DbType)SqlDbType.Decimal, 32, btstats.DownSpeed),
                                        DbHelper.MakeInParam("@alltraffic",(DbType)SqlDbType.Decimal, 32, btstats.AllTraffic),
                                        DbHelper.MakeInParam("@todaytraffic",(DbType)SqlDbType.Decimal, 32, btstats.TodayTraffic),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@allsize",(DbType)SqlDbType.Decimal, 32, btstats.AllSize),
                                        DbHelper.MakeInParam("@allseedscount",(DbType)SqlDbType.Int, 4, btstats.AllSeedsCount),
                                        DbHelper.MakeInParam("@statsupload",(DbType)SqlDbType.Decimal, 32, btstats.StatsUpload),
                                        DbHelper.MakeInParam("@statsdownload",(DbType)SqlDbType.Decimal, 32, btstats.StatsDownload),
                                        DbHelper.MakeInParam("@statsuploadtoday",(DbType)SqlDbType.Decimal, 32, btstats.StatsUploadToday),
                                        DbHelper.MakeInParam("@statsdownloadtoday",(DbType)SqlDbType.Decimal, 32, btstats.StatsDownloadToday),
                                        DbHelper.MakeInParam("@statsratio",(DbType)SqlDbType.Float, 8, btstats.StatsRatio),
                                        DbHelper.MakeInParam("@statsuploadall",(DbType)SqlDbType.Decimal, 32, btstats.StatsUploadAll),
                                  };

            //string sqlstring = "INSERT INTO [bt_stats] ([datetime],[peercount],[downpeercount],[uppeercount],[onlineusercount],[seedercount],[leechercount],[onlineseedscount],[onlinesize],[downspeed],[alltraffic],[todaytraffic],[lastupdate],[allsize],[allseedscount],[statsupload],[statsdownload],[statsuploadtoday],[statsdownloadtoday],[statsratio],[statsuploadall]) ";
            //sqlstring += " VALUES(@lastupdate, @peercount,@downpeercount,@uppeercount,@onlineusercount,@seedercount,@leechercount,@onlineseedscount,@onlinesize,@downspeed,@alltraffic,@todaytraffic,@lastupdate,@allsize,@allseedscount,@statsupload,@statsdownload,@statsuploadtoday,@statsdownloadtoday,@statsratio,@statsuploadall)";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_stats_insertserverstats", parms);
        }
        /// <summary>
        /// 更新BT状态
        /// </summary>
        /// <returns></returns>
        public int UpdateServerStats(PrivateBTServerStats btstats, int id)
        {
            if ((btstats.LastUpdate - DateTime.Now).TotalSeconds > 120 && (btstats.LastUpdate - DateTime.Now).TotalSeconds < 150) btstats.LastUpdate = btstats.LastUpdate.AddSeconds(120);
            else btstats.LastUpdate = DateTime.Now;
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id),
                                        DbHelper.MakeInParam("@peercount",(DbType)SqlDbType.Int, 4, btstats.PeerCount),
                                        DbHelper.MakeInParam("@downpeercount",(DbType)SqlDbType.Int, 4, btstats.DownPeerCount),
                                        DbHelper.MakeInParam("@uppeercount",(DbType)SqlDbType.Int, 4, btstats.UpPeerCount),
                                        DbHelper.MakeInParam("@onlineusercount",(DbType)SqlDbType.Int, 4, btstats.OnlineUserCount),
                                        DbHelper.MakeInParam("@seedercount",(DbType)SqlDbType.Int, 4, btstats.SeederCount),
                                        DbHelper.MakeInParam("@leechercount",(DbType)SqlDbType.Int, 4, btstats.LeecherCount),
                                        DbHelper.MakeInParam("@onlineseedscount",(DbType)SqlDbType.Int, 4, btstats.OnlineSeedsCount),
                                        DbHelper.MakeInParam("@onlinesize",(DbType)SqlDbType.Decimal, 32, btstats.OnlineSize),
                                        DbHelper.MakeInParam("@downspeed",(DbType)SqlDbType.Decimal, 32, btstats.DownSpeed),
                                        DbHelper.MakeInParam("@alltraffic",(DbType)SqlDbType.Decimal, 32, btstats.AllTraffic),
                                        DbHelper.MakeInParam("@todaytraffic",(DbType)SqlDbType.Decimal, 32, btstats.TodayTraffic),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, btstats.LastUpdate),
                                        DbHelper.MakeInParam("@allsize",(DbType)SqlDbType.Decimal, 32, btstats.AllSize),
                                        DbHelper.MakeInParam("@allseedscount",(DbType)SqlDbType.Int, 4, btstats.AllSeedsCount),
                                        DbHelper.MakeInParam("@statsupload",(DbType)SqlDbType.Decimal, 32, btstats.StatsUpload),
                                        DbHelper.MakeInParam("@statsdownload",(DbType)SqlDbType.Decimal, 32, btstats.StatsDownload),
                                        DbHelper.MakeInParam("@statsuploadtoday",(DbType)SqlDbType.Decimal, 32, btstats.StatsUploadToday),
                                        DbHelper.MakeInParam("@statsdownloadtoday",(DbType)SqlDbType.Decimal, 32, btstats.StatsDownloadToday),
                                        DbHelper.MakeInParam("@statsratio",(DbType)SqlDbType.Float, 8, btstats.StatsRatio),
                                        DbHelper.MakeInParam("@statsuploadall",(DbType)SqlDbType.Decimal, 32, btstats.StatsUploadAll),
                                        DbHelper.MakeInParam("@newupdate",(DbType)SqlDbType.DateTime, 8,  btstats.LastUpdate),
                                  };

            //string sqlstring = "UPDATE [bt_stats] SET [peercount] = @peercount, [downpeercount] = @downpeercount, [uppeercount] = @uppeercount, [onlineusercount] = @onlineusercount,";
            //sqlstring += " [seedercount] = @seedercount, [leechercount] = @leechercount, [onlineseedscount] = @onlineseedscount, [onlinesize] = @onlinesize, [downspeed] = @downspeed,";
            //sqlstring += " [alltraffic] = @alltraffic, [todaytraffic] = @todaytraffic, [lastupdate] = @newupdate, [allsize] = @allsize, [allseedscount] = @allseedscount,";
            //sqlstring += " [statsupload] = @statsupload, [statsdownload] = @statsdownload,  [statsuploadtoday] = @statsuploadtoday,  [statsdownloadtoday] = @statsdownloadtoday,  [statsratio] = @statsratio, [statsuploadall] = @statsuploadall";
            //sqlstring += " WHERE [id] = @id";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_stats_updateserverstats", parms);
        }
        /// <summary>
        /// 更新论坛统计信息
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="topiccount"></param>
        /// <param name="postcount"></param>
        /// <param name="todaypost"></param>
        /// <returns></returns>
        public int UpdateForumStatic(int forumid, int topiccount, int postcount, int todaypost)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@topiccount",(DbType)SqlDbType.Int, 4, topiccount),
                                        DbHelper.MakeInParam("@forumid",(DbType)SqlDbType.Int, 4, forumid),
                                        DbHelper.MakeInParam("@postcount",(DbType)SqlDbType.Int, 4, postcount),
                                        DbHelper.MakeInParam("@todaypost",(DbType)SqlDbType.Int, 4, todaypost),
                                  };
            string sqlstring = string.Format("UPDATE [dnt_forums] SET [topics] = @topiccount, [posts] = @postcount, [todayposts] = @todaypost WHERE [fid] = @forumid", BaseConfigs.GetTablePrefix); ;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

    }
}