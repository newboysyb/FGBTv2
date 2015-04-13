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
        /// 获得对应SeedId的种子的最近曾经活动用户ID信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetUserIdListActiveInSeed(int seedid, DateTime lastupdate)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, lastupdate)
                                  };
            string sqlstring = string.Format("SELECT [uid] FROM [bt_traffic] WHERE [seedid] = @seedid AND [lastupdate] > @lastupdate", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        } 


        /// <summary>
        /// 【存储过程】插入个人单种上传下载数据，一般在下载种子时即执行
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public int InsertPerUserSeedTraffic(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 32, addupload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 32, adddownload),
                                        DbHelper.MakeInParam("@firstipv4",(DbType)SqlDbType.Char, 70, ipv4),
                                        DbHelper.MakeInParam("@firstipv6",(DbType)SqlDbType.Char, 70, ipv6),
                                        DbHelper.MakeInParam("@firstupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@lastipv4",(DbType)SqlDbType.Char, 70, ipv4),
                                        DbHelper.MakeInParam("@lastipv6",(DbType)SqlDbType.Char, 70, ipv6),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            //string sqlstring = "INSERT INTO [bt_traffic] ([seedid],[uid],[upload],[download],[firstupdate],[lastupdate],[firstipv4],[firstipv6],[lastipv4],[lastipv6]) ";
            //sqlstring += " VALUES(@seedid, @uid, @upload, @download,@firstupdate,@lastupdate,@firstipv4,@firstipv6,@lastipv4,@lastipv6)";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_traffic_insertperuserseedtraffic", parms);
        }


        /// <summary>
        /// 更新个人单种上传下载数据，保种过程，只更新上传量和最后更新时间
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public int UpdatePerUserSeedTraffic(int seedid, int userid, decimal addupload, int addkeeptime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 32, addupload),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@keeptime",(DbType)SqlDbType.Int, 4, addkeeptime),
                                  };
            //string sqlstring = "UPDATE [bt_traffic] SET [upload] = [upload] + @upload, [download] = [download] + @download, [lastupdate] = @lastupdate, [lastipv4] = @lastipv4, [lastipv6] = @lastipv6 ";
            //sqlstring += " WHERE [seedid] = @seedid AND [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_traffic_updateperuserseedtraffic", parms);
        }
        /// <summary>
        /// 更新个人单种上传下载数据，保种过程，包含IP地址更新
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public int UpdatePerUserSeedTraffic_WithIP(int seedid, int userid, decimal addupload, int addkeeptime, string ipv4, string ipv6)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 32, addupload),
                                        DbHelper.MakeInParam("@keeptime",(DbType)SqlDbType.Int, 4, addkeeptime),

                                        DbHelper.MakeInParam("@lastipv4",(DbType)SqlDbType.VarChar, 22, ipv4),
                                        DbHelper.MakeInParam("@lastipv6",(DbType)SqlDbType.VarChar, 51, ipv6),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            //string sqlstring = "UPDATE [bt_traffic] SET [upload] = [upload] + @upload, [download] = [download] + @download, [lastupdate] = @lastupdate, [lastipv4] = @lastipv4, [lastipv6] = @lastipv6 ";
            //sqlstring += " WHERE [seedid] = @seedid AND [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_traffic_updateperuserseedtraffic_withip", parms);
        }
        /// <summary>
        /// 更新个人单种上传下载数据，下载过程，更新lastleft和lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public int UpdatePerUserSeedTraffic_Download(int seedid, int userid, decimal addupload, decimal adddownload, decimal lastleft, decimal lastdownload)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, addupload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, adddownload),

                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@lastleft",(DbType)SqlDbType.Decimal, 18, lastleft),
                                        DbHelper.MakeInParam("@lastdownload",(DbType)SqlDbType.Decimal, 18, lastdownload),
                                  };
            //string sqlstring = "UPDATE [bt_traffic] SET [upload] = [upload] + @upload, [download] = [download] + @download, [lastupdate] = @lastupdate, [lastipv4] = @lastipv4, [lastipv6] = @lastipv6 ";
            //sqlstring += " WHERE [seedid] = @seedid AND [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_traffic_updateperuserseedtraffic_download", parms);
        }
        /// <summary>
        /// 更新个人单种上传下载数据，下载过程，更新lastleft和lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public int UpdatePerUserSeedTraffic_DownloadWithIP(int seedid, int userid, decimal addupload, decimal adddownload, decimal lastleft, decimal lastdownload, string ipv4, string ipv6)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, addupload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, adddownload),
                                                                               
                                        DbHelper.MakeInParam("@lastipv4",(DbType)SqlDbType.VarChar, 22, ipv4),
                                        DbHelper.MakeInParam("@lastipv6",(DbType)SqlDbType.VarChar, 51, ipv6),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),

                                        DbHelper.MakeInParam("@lastleft",(DbType)SqlDbType.Decimal, 18, lastleft),
                                        DbHelper.MakeInParam("@lastdownload",(DbType)SqlDbType.Decimal, 18, lastdownload),
                                  };
            //string sqlstring = "UPDATE [bt_traffic] SET [upload] = [upload] + @upload, [download] = [download] + @download, [lastupdate] = @lastupdate, [lastipv4] = @lastipv4, [lastipv6] = @lastipv6 ";
            //sqlstring += " WHERE [seedid] = @seedid AND [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_traffic_updateperuserseedtraffic_downloadwithip", parms);
        }
        /// <summary>
        /// 更新个人单种上传下载数据，种子首次更新，更新firstratio、lastpeerid、lastleft、lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public int UpdatePerUserSeedTraffic_SeedFirst(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6, decimal lastleft, decimal lastdownload, string peerid, float firstratio)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, addupload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, adddownload),

                                        DbHelper.MakeInParam("@firstratio",(DbType)SqlDbType.Float, 4, firstratio),
                                        DbHelper.MakeInParam("@firstipv4",(DbType)SqlDbType.VarChar, 22, ipv4),
                                        DbHelper.MakeInParam("@firstipv6",(DbType)SqlDbType.VarChar, 51, ipv6),
                                        DbHelper.MakeInParam("@firstupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@lastipv4",(DbType)SqlDbType.VarChar, 22, ipv4),
                                        DbHelper.MakeInParam("@lastipv6",(DbType)SqlDbType.VarChar, 51, ipv6),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),

                                        DbHelper.MakeInParam("@lastleft",(DbType)SqlDbType.Decimal, 18, lastleft),
                                        DbHelper.MakeInParam("@lastdownload",(DbType)SqlDbType.Decimal, 18, lastdownload),
                                        DbHelper.MakeInParam("@lastpeerid",(DbType)SqlDbType.Char, 10, peerid),
                                  };
            //string sqlstring = "UPDATE [bt_traffic] SET [upload] = [upload] + @upload, [download] = [download] + @download, [lastupdate] = @lastupdate, [lastipv4] = @lastipv4, [lastipv6] = @lastipv6 ";
            //sqlstring += " WHERE [seedid] = @seedid AND [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_traffic_updateperuserseedtraffic_seedfirst", parms);
        }
        /// <summary>
        /// 更新个人单种上传下载数据，本次任务首次更新，更新lastpeerid、lastleft、lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        public int UpdatePerUserSeedTraffic_SessionFirst(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6, decimal lastleft, decimal lastdownload, string peerid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, addupload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, adddownload),

                                        DbHelper.MakeInParam("@lastipv4",(DbType)SqlDbType.Char, 22, ipv4),
                                        DbHelper.MakeInParam("@lastipv6",(DbType)SqlDbType.Char, 51, ipv6),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),

                                        DbHelper.MakeInParam("@lastleft",(DbType)SqlDbType.Decimal, 18, lastleft),
                                        DbHelper.MakeInParam("@lastdownload",(DbType)SqlDbType.Decimal, 18, lastdownload),
                                        DbHelper.MakeInParam("@lastpeerid",(DbType)SqlDbType.Char, 10, peerid),
                                  };
            //string sqlstring = "UPDATE [bt_traffic] SET [upload] = [upload] + @upload, [download] = [download] + @download, [lastupdate] = @lastupdate, [lastipv4] = @lastipv4, [lastipv6] = @lastipv6 ";
            //sqlstring += " WHERE [seedid] = @seedid AND [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_traffic_updateperuserseedtraffic_sessionfirst", parms);
        }




        /// <summary>
        /// 【存储过程】获取个人单种上传下载数据
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IDataReader GetPerUserSeedTraffic(int seedid, int userid)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                           DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
									   };
            //string sqlstring = "SELECT TOP 1 * FROM [bt_traffic] WHERE [seedid] = @seedid AND [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, "bt_traffic_getperuserseedtraffic", parms);
        }
        /// <summary>
        /// 获得对应SeedId的种子的历史Peer节点信息/历史流量信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetPeerHistoryList(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                  };
            string sqlstring = string.Format("SELECT [p].*, [u].[username], [u].[ratio],[u].[extcredits3],[u].[extcredits4], " + 
                "(SELECT TOP 1 [f].[finishtime] FROM [bt_finished] as [f] WITH(NOLOCK) WHERE [f].[uid] = [p].[uid] AND [f].[seedid] = [p].[seedid] ORDER BY [f].[id] DESC) AS [finishtime]" + 
                "FROM [bt_traffic] AS [p]" + 
                " LEFT JOIN [{0}users] AS [u] ON [u].[uid] = [p].[uid]" + 
                "WHERE [p].[seedid] = @seedid ORDER BY [p].[firstupdate] DESC", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 获取指定时间后，某个种子新增流量记录数，可反映种子在指定事件后的下载次数
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public int GetNewTrafficRecordCount(int seedid, DateTime datetime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@datetime",(DbType)SqlDbType.DateTime, 8, datetime),
                                  };
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_traffic_getnewtrafficrecordcount", parms).ToString(), 0);
        }
    }
}