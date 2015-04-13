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
//RSS相关的SQL数据库操作

namespace Discuz.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {




        /// <summary>
        /// 添加RSS记录
        /// </summary>
        /// <returns>数据库更改行数</returns>
        public int AddSeedRss(PTSeedRssinfo rssinfo)
        {
            DbParameter[] parms = {
                                      	DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, rssinfo.Seedid),
                                        DbHelper.MakeInParam("@adddatetime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@rssstatus",(DbType)SqlDbType.Int, 4, rssinfo.RssStatus),
                                        DbHelper.MakeInParam("@rsstype",(DbType)SqlDbType.Int, 4, rssinfo.RssType),
                                        DbHelper.MakeInParam("@rsscondition",(DbType)SqlDbType.Int, 4, rssinfo.RssCondition),
                                        //DbHelper.MakeInParam("@postid",(DbType)SqlDbType.Int, 4, rssinfo.PostId),
                                        //DbHelper.MakeInParam("@topicid",(DbType)SqlDbType.Int, 4, rssinfo.Topicid),
                                        //DbHelper.MakeInParam("@lastupdated",(DbType)SqlDbType.Int, 4, rssinfo.LastUpdated),
                                        //DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, rssinfo.SeedType),
                                        //DbHelper.MakeInParam("@seedstatus",(DbType)SqlDbType.Int, 4, rssinfo.SeedStatus),
                                        //DbHelper.MakeInParam("@seedtitle",(DbType)SqlDbType.NVarChar, 255, rssinfo.SeedTitle),
                                        //DbHelper.MakeInParam("@seedsize",(DbType)SqlDbType.Decimal, 16, rssinfo.SeedSize),
                                        //,[topicid],[lastupdated],[seedtype],[seedstatus],[seedtitle],[seedsize]
                                        //,@topicid,@lastupdated,@seedtype,@seedstatus,@seedtitle,@seedsize
									   };

            string sqlstring = "INSERT INTO [bt_rss] " +
                "([seedid],[adddatetime],[rssstatus],[rsstype],[rsscondition]) " +
                "VALUES(@seedid,@adddatetime,@rssstatus,@rsstype,@rsscondition)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新RSS条目
        /// </summary>
        /// <param name="rssinfo"></param>
        /// <returns></returns>
        public int UpdateSeedRss(PTSeedRssinfo rssinfo)
        {
            //DbParameter[] parms = {
            //                            DbHelper.MakeInParam("@rssid",(DbType)SqlDbType.Int, 4, rssinfo.Rssid),
            //                            DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, rssinfo.Seedid),
            //                            DbHelper.MakeInParam("@topicid",(DbType)SqlDbType.Int, 4, rssinfo.Topicid),

            //                            DbHelper.MakeInParam("@lastupdated",(DbType)SqlDbType.Int, 4, rssinfo.LastUpdated),
            //                            DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, rssinfo.SeedType),
            //                            DbHelper.MakeInParam("@seedstatus",(DbType)SqlDbType.Int, 4, rssinfo.SeedStatus),
            //                            DbHelper.MakeInParam("@seedtitle",(DbType)SqlDbType.NVarChar, 255, rssinfo.SeedTitle),
            //                            DbHelper.MakeInParam("@seedsize",(DbType)SqlDbType.Decimal, 16, rssinfo.SeedSize),
            //                           };
            //string sqlstring = "UPDATE [bt_rss] SET " +
            //    "[lastupdated] = @lastupdated, [seedtype] = @seedtype, [seedstatus] = @seedstatus, [seedtitle] = @seedtitle, [seedsize] = @seedsize " +
            //    "WHERE [seedid] = @seedid AND [topicid] = @topicid";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            return -1;
        }
        /// <summary>
        /// 删除RSS条目
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <returns></returns>
        public int DeleteSeedRss(int seedid, int rsstype)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@rsstype",(DbType)SqlDbType.Int, 4, rsstype),
                                      	DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4,seedid),
									   };

            //string sqlstring = "UPDATE [bt_rss] SET [rssstatus] = -5 WHERE [seedid] = @seedid AND [rsstype] = @rsstype";
            string sqlstring = "DELETE FROM [bt_rss] WHERE [seedid] = @seedid AND [rsstype] = @rsstype";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 判断RSS条目是否存在
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="adddatetime">在此时间之后</param>
        /// <returns></returns>
        public bool IsExistsSeedRss(int seedid, int rsstype, DateTime adddatetime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@rsstype",(DbType)SqlDbType.Int, 4, rsstype),
                                      	DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4,seedid),
                                        DbHelper.MakeInParam("@adddatetime",(DbType)SqlDbType.DateTime, 8, adddatetime),
									   };

            string sqlstring = "IF EXISTS(SELECT [seedid] FROM [bt_rss] WITH(NOLOCK) WHERE [seedid] = @seedid AND [rsstype] = @rsstype AND [adddatetime] > @adddatetime) SELECT 'TRUE' ELSE SELECT 'FALSE'";
            return TypeConverter.StrToBool(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), false);
        }

        /// <summary>
        /// 获取热门下载种子RSS_ACC_SH列表
        /// </summary>
        /// <param name="maxlogcount"></param>
        /// <param name="mindownload"></param>
        /// <returns></returns>
        public DataTable GetSeedRssListHotDownload(int maxlogcount, int mindownload)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@maxlogcount",(DbType)SqlDbType.Int, 4, maxlogcount),
                            DbHelper.MakeInParam("@mindownload",(DbType)SqlDbType.Int, 4, mindownload),
                        };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_rss_getrsslisthotdownload", parms).Tables[0];
        }

        /// <summary>
        /// 获取热门老种RSS_OLDHOT列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSeedRssListOldHot(int downloadcountlimit, int dayscheck, int daysbefore, int totalcount)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@downloadcountlimit",(DbType)SqlDbType.Int, 4, downloadcountlimit),
                            DbHelper.MakeInParam("@dayscheck",(DbType)SqlDbType.Int, 4, dayscheck),
                            DbHelper.MakeInParam("@daysbefore",(DbType)SqlDbType.Int, 4, daysbefore),
                            DbHelper.MakeInParam("@totalcount",(DbType)SqlDbType.Int, 4, totalcount),
                        };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_rss_getrsslistoldhot", parms).Tables[0];
        }

        /// <summary>
        /// 获取热门老种RSS_OLDHOT_NMB列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSeedRssListOldHotNMB(int downloadcountlimit, int dayscheck, int daysbefore, int totalcount)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@downloadcountlimit",(DbType)SqlDbType.Int, 4, downloadcountlimit),
                            DbHelper.MakeInParam("@dayscheck",(DbType)SqlDbType.Int, 4, dayscheck),
                            DbHelper.MakeInParam("@daysbefore",(DbType)SqlDbType.Int, 4, daysbefore),
                            DbHelper.MakeInParam("@totalcount",(DbType)SqlDbType.Int, 4, totalcount),
                        };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_rss_getrsslistoldhotnmb", parms).Tables[0];
        }

        /// <summary>
        /// 获取热门保种RSS_KEEPHOT列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSeedRssListKeepHot(int downloadcountlimit, int dayscheck, int daysbefore, int daysafter, int totalcount)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@downloadcountlimit",(DbType)SqlDbType.Int, 4, downloadcountlimit),
                            DbHelper.MakeInParam("@dayscheck",(DbType)SqlDbType.Int, 4, dayscheck),
                            DbHelper.MakeInParam("@daysbefore",(DbType)SqlDbType.Int, 4, daysbefore),
                            DbHelper.MakeInParam("@daysafter",(DbType)SqlDbType.Int, 4, daysafter),
                            DbHelper.MakeInParam("@totalcount",(DbType)SqlDbType.Int, 4, totalcount),
                        };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_rss_getrsslistkeephot", parms).Tables[0];
        }

        /// <summary>
        /// 获取SeedRSS表
        /// </summary>
        /// <param name="numperpage"></param>
        /// <param name="pageindex"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public DataTable GetSeedRssListbyType(int numperpage, int pageindex, int seedtype, int rsstype)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                            DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int, 4, pageindex),
                            DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                            DbHelper.MakeInParam("@rsstype",(DbType)SqlDbType.Int, 4, rsstype),
                        };
            string sqlstring = "";
            //string sqlwhere = seedtype > 0 ? " WHERE [rsstype] = @rsstype AND [seedstatus] = 2 AND [seedtype] = @seedtype " : " WHERE [rsstype] = @rsstype AND [seedstatus] = 2 ";
            string sqlwhere = " WHERE [rsstype] = @rsstype ";
            string sqlsection = " [rssid],[bt_rss].[seedid],[adddatetime],[rssstatus],[rsstype],[rsscondition],[bt_seed].[topicid],[bt_seed].[type],[bt_seed].[status],[bt_seed].[topictitle],[bt_seed].[filesize] ";

            if (pageindex == 1)
            {
                sqlstring += string.Format("SELECT TOP (@numperpage) " + sqlsection 
                    + " FROM [bt_rss] WITH(NOLOCK) LEFT JOIN [bt_seed] WITH(NOLOCK) ON [bt_seed].[seedid] = [bt_rss].[seedid] "
                    + sqlwhere + " ORDER BY [adddatetime] desc");
            }
            else
            {
                sqlstring += string.Format("SELECT TOP (@numperpage) " + sqlsection 
                    + " FROM [bt_rss] WITH(NOLOCK) LEFT JOIN [bt_seed] WITH(NOLOCK) ON [bt_seed].[seedid] = [bt_rss].[seedid] " 
                    + sqlwhere);
                if (sqlwhere != "") sqlstring += " AND ";
                else sqlstring += " WHERE ";
                sqlstring += " [adddatetime] < (SELECT MIN([adddatetime]) FROM (SELECT TOP (@numperpage * @pageindex - @numperpage) [adddatetime] FROM [bt_rss] " + sqlwhere;
                sqlstring += " ORDER BY [adddatetime] desc) AS tblTmp) ORDER BY [adddatetime] desc";

            }
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取SeedRSS表
        /// </summary>
        /// <param name="numperpage"></param>
        /// <param name="pageindex"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public DataTable GetSeedRssWithDetailListbyType(int numperpage, int pageindex, int seedtype, int rsstype)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                            DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int, 4, pageindex),
                            DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                            DbHelper.MakeInParam("@rsstype",(DbType)SqlDbType.Int, 4, rsstype),
                        };
            string sqlstring = "";
            //string sqlwhere = seedtype > 0 ? " WHERE [rsstype] = @rsstype AND [seedstatus] = 2 AND [seedtype] = @seedtype " : " WHERE [rsstype] = @rsstype AND [seedstatus] = 2 ";
            string sqlwhere = " WHERE [rsstype] = @rsstype ";
            string sqlsection = " [rssid],[bt_rss].[seedid],[adddatetime],[rssstatus],[rsstype],[rsscondition],[bt_seed].[topicid],[bt_seed].[type],[bt_seed].[status],[bt_seed].[topictitle],[bt_seed].[filesize] ";

            if (pageindex == 1)
            {
                sqlstring += string.Format("SELECT TOP (@numperpage) " + sqlsection
                    + " FROM [bt_rss] WITH(NOLOCK) LEFT JOIN [bt_seed] WITH(NOLOCK) ON [bt_seed].[seedid] = [bt_rss].[seedid] "
                    + sqlwhere + " ORDER BY [adddatetime] desc");
            }
            else
            {
                sqlstring += string.Format("SELECT TOP (@numperpage) " + sqlsection
                    + " FROM [bt_rss] WITH(NOLOCK) LEFT JOIN [bt_seed] WITH(NOLOCK) ON [bt_seed].[seedid] = [bt_rss].[seedid] "
                    + sqlwhere);
                if (sqlwhere != "") sqlstring += " AND ";
                else sqlstring += " WHERE ";
                sqlstring += " [adddatetime] < (SELECT MIN([adddatetime]) FROM (SELECT TOP (@numperpage * @pageindex - @numperpage) [adddatetime] FROM [bt_rss] " + sqlwhere;
                sqlstring += " ORDER BY [adddatetime] desc) AS tblTmp) ORDER BY [adddatetime] desc";

            }
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取SeedRSS 总数
        /// </summary>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public int GetSeedRssCountbyType(int seedtype, int rsstype)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@seedtype",(DbType)SqlDbType.Int, 4, seedtype),
                            DbHelper.MakeInParam("@rsstype",(DbType)SqlDbType.Int, 4, rsstype),
                        };
            string sqlstring = "";
            //string sqlwhere = seedtype > 0 ? " WHERE [rsstype] = @rsstype AND [seedstatus] = 2 AND [seedtype] = @seedtype " : " WHERE [rsstype] = @rsstype AND [seedstatus] = 2 ";
            string sqlwhere = " WHERE [rsstype] = @rsstype ";

            sqlstring += string.Format("SELECT COUNT(adddatetime) FROM [bt_rss] WITH(NOLOCK) " + sqlwhere);

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        }


    }
}
