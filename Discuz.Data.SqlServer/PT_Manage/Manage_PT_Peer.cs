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
        /// 清理失效的节点
        /// </summary>
        /// <returns></returns>
        public int CleanExpirePeer()
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_cleanexpirepeer").ToString(), 0);
        }


        /// <summary>
        /// 获得用户正在上传的种子数
        /// 【announce用】
        /// </summary>
        /// <returns></returns>
        public int GetPeerUserUploadCount(int uid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, true),
                                  };
            //string sqlstring = "SELECT DISTINCT COUNT([seedid]) FROM [bt_peer] WITH(NOLOCK) WHERE [uid] = @userid AND [seed] = @seed ";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_byuseridonly", parms).ToString(), 0);
        }
        /// <summary>
        /// 获得用户正在下载的种子数
        /// 【announce用】
        /// </summary>
        /// <returns></returns>
        public int GetPeerUserDownloadCount(int uid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, false),
                                  };
            //string sqlstring = "SELECT DISTINCT COUNT([seedid]) FROM [bt_peer] WITH(NOLOCK) WHERE [uid] = @userid AND [seed] = 'False' ";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_byuseridonly", parms).ToString(), 0);
        }
        /// <summary>
        /// 【存储过程】获得指定种子的上传数
        /// </summary>
        /// <returns></returns>
        public int GetPeerSeedUploadCount(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, true),
                                  };
            //string sqlstring = "SELECT COUNT(1) FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [seed] = 'True'";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_byseedid", parms).ToString(), 0);
        }
        /// <summary>
        /// 【存储过程】获得指定种子的IPv6上传数
        /// </summary>
        /// <returns></returns>
        public int GetPeerSeedIPv6UploadCount(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, true),
                                        DbHelper.MakeInParam("@isipv6",(DbType)SqlDbType.Bit, 1, true),
                                  };
            //string sqlstring = "SELECT COUNT(1) FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [seed] = 'True' AND [isipv6] = '1'";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_byseedidipv6", parms).ToString(), 0);
        }
        /// <summary>
        /// 【存储过程】获得指定种子的下载数
        /// </summary>
        /// <returns></returns>
        public int GetPeerSeedDownloadCount(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, false),
                                  };
            //string sqlstring = "SELECT COUNT(1) FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [seed] = 'False'";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_byseedid", parms).ToString(), 0);
        }

        
        /// <summary>
        /// 获得对应SeedId的种子的Peer节点信息，
        /// 用于显示Peer列表的页面，无需存储过程化，需要：分页显示功能，降低系统负载
        /// 
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetPeerList(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                  };
            string sqlstring = string.Format("SELECT [p].*, [u].[username], [u].[ratio], [u].[extcredits3], [u].[extcredits4] FROM [bt_peer] AS [p] WITH(NOLOCK), [{0}users] AS [u] WITH(NOLOCK) WHERE [p].[seedid] = @seedid AND [u].[uid] = [p].[uid] ORDER BY [p].[percentage] DESC", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 获得对应uid的Peer节点信息，
        /// 仅用于计算保种奖励，合并提取种子大小、生存时间、最后完成时间、用户保种时间、用户上传量
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetPeerList(int uid, bool seeding)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@seeding",(DbType)SqlDbType.Bit, 1, seeding),
                                  };
            string sqlstring = string.Format("SELECT [p].[seedid], [p].[uid], [p].[firsttime], [p].[lasttime], [p].[keeptime], [s].[filesize], [s].[live], [s].[upload]," +
                "[t].[upload] AS [uptraffic], [d].[lastfinish] " + 
                "FROM [bt_peer] AS [p] WITH(NOLOCK)" + 
                "LEFT JOIN [bt_seed] AS [s] WITH(NOLOCK) ON [p].[seedid] = [s].[seedid] " +
                "LEFT JOIN [bt_seed_detail] AS [d] WITH(NOLOCK) ON [p].[seedid] = [d].[seedid] " +
                "LEFT JOIN [bt_traffic] AS [t] WITH(NOLOCK) ON [p].[seedid] = [t].[seedid] AND [p].[uid] = [t].[uid] " +
                "WHERE [p].[uid] = @uid AND [p].[seed] = @seeding ORDER BY [p].[seedid]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        //C//2014.12.07///// <summary>
        ///// 【存储过程】获得指定种子的指定uid的peer节点数，uid为0则不限uid，只判断seedid和peerid
        ///// 此函数用于：tracker中，判断同一个ut是否挂有多人tracker（需要检测同seedid和peerid的种子，限制uid和不限制情况下数量是否相等）
        ///// 以及：判断是否用户同时两个ut下载同一个种子（开始新下载时，判断通seedid和uid，限制peerid和不限制peerid的情况，数量是否相等）
        ///// 【此函数查询均不加锁】
        ///// </summary>
        ///// <returns></returns>
        //public int GetPeerCount(int uid, string peerid, int seedid)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
        //                                DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerid),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                          };
        //    //string sqlstring;
        //    //if (userid > 0) sqlstring = "SELECT COUNT(*) FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [peerid] = @peerid AND [uid] = @userid";
        //    //else sqlstring = "SELECT COUNT(*) FROM [bt_peer] WITH(NOLOCK) WHERE [peerid] = @peerid AND [seedid] = @seedid";

        //    if (uid > 0 && peerid != "") return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount", parms).ToString(), 0);
        //    else if (uid < 1 && peerid != "") return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_without_userid", parms).ToString(), 0);
        //    else if (uid > 0 && peerid == "") return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_without_peerid", parms).ToString(), 0);
        //    else return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_only_seedid", parms).ToString(), 0);
        //}

        /// <summary>
        /// 判断是否存在同peerid的其他节点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="peerid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public int IsExistsOtherPeer(int uid, string peerid, int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerid),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                  };
            //string sqlstring;
            //if (userid > 0) sqlstring = "SELECT COUNT(*) FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [peerid] = @peerid AND [uid] = @userid";
            //else sqlstring = "SELECT COUNT(*) FROM [bt_peer] WITH(NOLOCK) WHERE [peerid] = @peerid AND [seedid] = @seedid";

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_isexistsotherpeer", parms).ToString(), 0);
        }
        /// <summary>
        /// 判断是否存在同peerid的其他节点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="peerid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public int IsExistsPeer(int uid, int seedid, bool isdownload)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, !isdownload),
                                  };
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_isexistspeer", parms).ToString(), 0);
        }



        /// <summary>
        /// 获得指定种子的制定uid或者ip的peer节点数，0或者空为不限
        /// 现在只允许uid和seedid同时为0和同时不为0
        /// </summary>
        /// <returns></returns>
        public int GetPeerCount(int uid, int seedid, bool isdownload)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, !isdownload),
                                  };
            //string sqlstring;
            //if (userid > 0 && ip.Length == 0) sqlstring = "SELECT COUNT(1) FROM [bt_peer] WHERE [uid] = @userid AND [seedid] = @seedid";
            //else if (userid == 0 && ip.Length > 0) sqlstring = "SELECT COUNT(1) FROM [bt_peer] WHERE [seedid] = @seedid AND [ip] = @ip";
            //else if (userid == 0 && ip.Length == 0) sqlstring = "SELECT COUNT(1) FROM [bt_peer] WHERE [seedid] = @seedid";
            //else sqlstring = "SELECT COUNT(1) FROM [bt_peer] WHERE [seedid] = @seedid AND [uid] = @userid AND [ip] = @ip";
            //if (isdownload) sqlstring += " AND [seed] = 'False'";
            //else sqlstring += " AND [seed] = 'True'";

            //string sqlstring = "SELECT COUNT(1) FROM [bt_peer] WHERE ";
            //if (userid > 0) sqlstring += "[uid] = @userid AND ";
            //if (ip.Length > 0) sqlstring += "[ip] = @ip AND ";
            //if (seedid > 0) sqlstring += "[seedid] = @seedid AND ";
            //if (isdownload) sqlstring += " [seed] = 'False'";
            //else sqlstring += " [seed] = 'True'";

            if (uid < 1 && seedid < 1) return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_seed", parms).ToString(), 0);

            else return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_byuserid", parms).ToString(), 0);
        }


        /// <summary>
        /// 获得正在上传/下载/全部的用户数，不计算重复用户的peer，1下载，2上传，其余不限
        /// </summary>
        /// <returns></returns>
        public int GetPeerUserCount(int userstatus)
        {
            bool isseed = false;
            if (userstatus == 1) isseed = false;
            else if (userstatus == 2) isseed = true;

            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, isseed),
                                  };
            //string sqlstring = "SELECT COUNT(1) FROM ( SELECT DISTINCT [bt_peer].[uid] FROM [bt_peer] )";
            //string sqlstring = "SELECT COUNT(DISTINCT [uid]) FROM [bt_peer] ";

            //else sqlstring += " )";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeercount_disctinctuid").ToString(), 0);
        }

        ///// <summary>
        ///// 获得指定seedid，指定uid，置顶peerid的记录数（这三项是确定peer的项目）
        ///// 【函数目前作废】与上面的函数功能相同
        ///// </summary>
        ///// <returns></returns>
        //public int GetPeerCount(PrivateBTPeerInfo peerinfo)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
        //                                DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 40, peerinfo.PeerId),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
        //                          };
        //    string sqlstring = "SELECT COUNT(1) FROM [bt_peer] WHERE [seedid] = @seedid AND [peerid] = @peerid AND [uid] = @userid";
        //    return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        //}
        
        ///// <summary>
        ///// 阻止用户同时使用双Tracker，寻找seedid相等，ipv6addip = 指定ip或者peerid=指定的数量
        ///// </summary>
        ///// <returns></returns>
        //public int GetPeerCountV4V6(string ip, int seedid, string peerid)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 20, peerid),
        //                                DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char, 60, ip),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                          };
        //    string sqlstring;
        //    sqlstring = "SELECT COUNT(1) FROM [bt_peer] WHERE [seedid] = @seedid AND (( [ipv6addip] = @ip AND [ip] != @ip) OR [peerid] = @peerid )";
        //    return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        //}
        

        /// <summary>
        /// 【存储过程】插入peer信息，全部信息均更新，19项
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int InsertPeerInfo(PrivateBTPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@client",(DbType)SqlDbType.VarChar, 20, peerinfo.Client),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
                                        DbHelper.MakeInParam("@downloadspeed",(DbType)SqlDbType.Float, 8, peerinfo.DownloadSpeed),
                                        DbHelper.MakeInParam("@firsttime",(DbType)SqlDbType.DateTime, 8, peerinfo.FirstTime),
                                        DbHelper.MakeInParam("@ip",(DbType)SqlDbType.VarChar, 15, peerinfo.IPv4IP),
                                        DbHelper.MakeInParam("@isipv6",(DbType)SqlDbType.Int, 4, peerinfo.IPStatus),
                                        DbHelper.MakeInParam("@ipv6ip",(DbType)SqlDbType.VarChar, 45, peerinfo.IPv6IP),
                                        DbHelper.MakeInParam("@ipv6addip",(DbType)SqlDbType.VarChar, 45, peerinfo.IPv6IPAdd),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, peerinfo.IsSeed),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, peerinfo.LastTime),
                                        DbHelper.MakeInParam("@keeptime",(DbType)SqlDbType.Int, 4, peerinfo.KeepTime),
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 24, peerinfo.PeerId),
                                        DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 8, peerinfo.Percentage),
                                        DbHelper.MakeInParam("@port",(DbType)SqlDbType.Int, 4, peerinfo.Port),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
                                        DbHelper.MakeInParam("@uploadspeed",(DbType)SqlDbType.Float, 8, peerinfo.UploadSpeed),
                                        DbHelper.MakeInParam("@v4last",(DbType)SqlDbType.DateTime, 8, peerinfo.v4Last),
                                        DbHelper.MakeInParam("@v6last",(DbType)SqlDbType.DateTime, 8, peerinfo.v6Last),
                                        DbHelper.MakeInParam("@totalupload",(DbType)SqlDbType.Decimal, 32, peerinfo.TotalUpload),
                                        DbHelper.MakeInParam("@totaldownload",(DbType)SqlDbType.Decimal, 32, peerinfo.TotalDownload),

                                        DbHelper.MakeInParam("@ipregioninbuaa",(DbType)SqlDbType.TinyInt, 1, (int)peerinfo.IPRegionInBuaa),
                                        DbHelper.MakeInParam("@lastsend",(DbType)SqlDbType.SmallInt, 1, peerinfo.LastSend),

                                         DbHelper.MakeInParam("@left",(DbType)SqlDbType.Decimal, 18, peerinfo.Left),

                                  };
            //string sqlstring = "INSERT INTO [bt_peer] ([client],[download],[downloadspeed],[firsttime],[ip],[isipv6],[seed],[lasttime],[peerid],[percentage],[port],[seedid],[uid],[upload],[uploadspeed],[ipv6addip],[ipv6ip],[v4last],[v6last],[totalupload],[totaldownload])";
            //sqlstring += " VALUES (@client,@download,@downloadspeed,@firsttime,@ip,@isipv6,@seed,@lasttime,@peerid,@percentage,@port,@seedid,@uid,@upload,@uploadspeed,@ipv6addip,@ipv6ip,@v4last,@v6last,@totalupload,@totaldownload)";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_insertpeer", parms), -1);
        }

        ///// <summary>
        ///// 【无引用】//////获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //public DataTable GetPeerList(int seedid, bool upload, int maxcount)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                                DbHelper.MakeInParam("@maxcount",(DbType)SqlDbType.Int, 4, maxcount),
        //                          };
        //    string sqlstring;
        //    if (upload) sqlstring = "SELECT TOP(@maxcount) * FROM [bt_peer] WHERE [seedid] = @seedid AND [seed] = 'True' ORDER BY [lasttime] DESC";
        //    else sqlstring = "SELECT TOP(@maxcount) * FROM [bt_peer] WHERE [seedid] = @seedid AND [seed] = 'False' ORDER BY [percentage] DESC";
        //    return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        //}

        /// <summary>
        /// 【存储过程】【只获得IP，供tracker使用，区分上传下载】获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序bt_peer_getpeerlist_fortracker
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetPeerListTracker(int seedid, bool upload, int maxcount, bool withpeerid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@maxcount",(DbType)SqlDbType.Int, 4, maxcount),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, upload),
                                  };
            //string sqlstring;
            //if (upload) sqlstring = "SELECT TOP (@maxcount) [uid],[ip],[ipv6ip],[port],[peerid],[ipv6addip] FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [seed] = 'True' ORDER BY [lasttime] DESC";
            //else sqlstring = "SELECT TOP (@maxcount) [uid],[ip],[ipv6ip],[port],[peerid],[ipv6addip] FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [seed] = 'False' ORDER BY [percentage] DESC";
            
            if (!withpeerid) return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_fortracker", parms).Tables[0];
            else return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_fortracker_withpeerid", parms).Tables[0];
        }
        /// <summary>
        /// 【存储过程】【只获得IP，供tracker使用，不区分上传下载】获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序bt_peer_getpeerlist_fortracker
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetPeerListTrackerAll(int seedid, int maxcount, bool withpeerid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@maxcount",(DbType)SqlDbType.Int, 4, maxcount),
                                  };
            //string sqlstring;
            //if (upload) sqlstring = "SELECT TOP (@maxcount) [uid],[ip],[ipv6ip],[port],[peerid],[ipv6addip] FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [seed] = 'True' ORDER BY [lasttime] DESC";
            //else sqlstring = "SELECT TOP (@maxcount) [uid],[ip],[ipv6ip],[port],[peerid],[ipv6addip] FROM [bt_peer] WITH(NOLOCK) WHERE [seedid] = @seedid AND [seed] = 'False' ORDER BY [percentage] DESC";

            if (!withpeerid) return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_all_fortracker", parms).Tables[0];
            else return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_all_fortracker_withpeerid", parms).Tables[0];
        }

        ///// <summary>
        ///// 更新指定的peer信息，只更新PeerID
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public int UpdatePeerInfoIDOnly(PrivateBTPeerInfo peerinfo)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 40, peerinfo.PeerId),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
        //                                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
        //                          };
        //    string sqlstring = "UPDATE [bt_peer] SET [peerid] = @peerid WHERE [seedid] = @seedid AND [uid] = @uid";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}


        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（2项）：
        /// v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int UpdatePeerInfo_NoTraffic(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, peerinfo.Pid),
                                        //DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        //DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
                                        //DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),

                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, peerinfo.LastTime),
                                        DbHelper.MakeInParam("@peerlock",(DbType)SqlDbType.Int, 4, peerinfo.PeerLock),
                                  };
            if (IsIPv6Request)
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_null_ipv6", parms);
            }
            else
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_null_ipv4", parms);
            }
        }

        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（4项）：
        /// upload，uploadspeed，v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int UpdatePeerInfo_UpTrafficOnly(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, peerinfo.Pid),
                                        //DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        //DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
                                        //DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),

                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
                                        DbHelper.MakeInParam("@uploadspeed",(DbType)SqlDbType.Float, 8, peerinfo.UploadSpeed),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, peerinfo.LastTime),
                                        DbHelper.MakeInParam("@peerlock",(DbType)SqlDbType.Int, 4, peerinfo.PeerLock),
                                  };
            if (IsIPv6Request)
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_up_ipv6", parms);
            }
            else
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_up_ipv4", parms);
            }
        }
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（7项）：
        /// upload，uploadspeed，download，downloadspeed，percentage，v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int UpdatePeerInfo_UpDownTrafficOnly(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, peerinfo.Pid),
                                        //DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        //DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
                                        //DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),

                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
                                        DbHelper.MakeInParam("@uploadspeed",(DbType)SqlDbType.Float, 8, peerinfo.UploadSpeed),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
                                        DbHelper.MakeInParam("@downloadspeed",(DbType)SqlDbType.Float, 8, peerinfo.DownloadSpeed),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, peerinfo.LastTime),
                                        DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 8, peerinfo.Percentage),

                                        DbHelper.MakeInParam("@left",(DbType)SqlDbType.Decimal, 18, peerinfo.Left),
                                        DbHelper.MakeInParam("@peerlock",(DbType)SqlDbType.Int, 4, peerinfo.PeerLock),
                                  };
            if (IsIPv6Request)
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_updown_ipv6", parms);
            }
            else
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_updown_ipv4", parms);
            }
        }

        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（13项）：
        /// upload，uploadspeed，download，downloadspeed，percentage，v4last/v6last，lasttime
        /// ip，ipv6ip，ipv6addip，isipv6，port，seed
        /// 以下内容不会被更新：
        /// client，firsttime，totalupload，totaldownload，以及3要素seedid，uid，peerid
        /// 更换client的行为应该被记录为警告事件
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int UpdatePeerInfo(PrivateBTPeerInfo peerinfo, bool IsIPv6Request)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, peerinfo.Pid),
                                        //DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        //DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
                                        //DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),

                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
                                        DbHelper.MakeInParam("@uploadspeed",(DbType)SqlDbType.Float, 8, peerinfo.UploadSpeed),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
                                        DbHelper.MakeInParam("@downloadspeed",(DbType)SqlDbType.Float, 8, peerinfo.DownloadSpeed),
                                        DbHelper.MakeInParam("@isipv6",(DbType)SqlDbType.Int, 4, peerinfo.IPStatus),
                                        DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char, 15, peerinfo.IPv4IP),                                        
                                        DbHelper.MakeInParam("@ipv6ip",(DbType)SqlDbType.Char, 45, peerinfo.IPv6IP),
                                        DbHelper.MakeInParam("@ipv6addip",(DbType)SqlDbType.Char, 45, peerinfo.IPv6IPAdd),
                                        DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, peerinfo.IsSeed),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, peerinfo.LastTime),
                                        DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 8, peerinfo.Percentage),
                                        DbHelper.MakeInParam("@port",(DbType)SqlDbType.Int, 4, peerinfo.Port),

                                        DbHelper.MakeInParam("@ipregioninbuaa",(DbType)SqlDbType.TinyInt, 1, (int)peerinfo.IPRegionInBuaa),
                                        DbHelper.MakeInParam("@lastsend",(DbType)SqlDbType.SmallInt, 1, peerinfo.LastSend),

                                        DbHelper.MakeInParam("@left",(DbType)SqlDbType.Decimal, 18, peerinfo.Left),
                                        DbHelper.MakeInParam("@peerlock",(DbType)SqlDbType.Int, 4, peerinfo.PeerLock),
                                  };
            //string sqlstring = "";
            if (IsIPv6Request)
            {
                //sqlstring = "UPDATE [bt_peer] SET [totalupload] = [totalupload] + @upload - [upload], [totaldownload] = [totaldownload] + @download - [download], [v6last] = @v4v6last, [ip] = @ip, [port] = @port, [ipv6ip] = @ipv6ip, [download] = @download, [downloadspeed] = @downloadspeed,[seed] = @seed,[lasttime] = @lasttime,[percentage] = @percentage,[upload] = @upload,[uploadspeed] = @uploadspeed,[ipv6addip] = @ipv6addip,[isipv6] = @isipv6,[client] = @client ";
                //sqlstring += "WHERE [seedid] = @seedid AND [uid] = @uid AND [peerid] = @peerid";
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_ipv6", parms);
            }
            else
            {
                //sqlstring = "UPDATE [bt_peer] SET [totalupload] = [totalupload] + @upload - [upload], [totaldownload] = [totaldownload] + @download - [download], [v4last] = @v4v6last, [ip] = @ip, [port] = @port, [ipv6ip] = @ipv6ip, [download] = @download, [downloadspeed] = @downloadspeed,[seed] = @seed,[lasttime] = @lasttime,[percentage] = @percentage,[upload] = @upload,[uploadspeed] = @uploadspeed,[ipv6addip] = @ipv6addip,[isipv6] = @isipv6,[client] = @client ";
                //sqlstring += "WHERE [seedid] = @seedid AND [uid] = @uid AND [peerid] = @peerid";
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_updatepeerinfo_ipv4", parms);
            }
        }




        ///// <summary>
        ///// 更新指定的peer信息，同时更新PeerID，更新除seedid,uid（三要素之二）和firsttime之外的18-3项
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public int UpdatePeerInfoPeerID(PrivateBTPeerInfo peerinfo, bool ipv6tracker)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@client",(DbType)SqlDbType.Char, 20, peerinfo.Client),
        //                                DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
        //                                DbHelper.MakeInParam("@downloadspeed",(DbType)SqlDbType.Float, 8, peerinfo.DownloadSpeed),
        //                                DbHelper.MakeInParam("@firsttime",(DbType)SqlDbType.DateTime, 8, peerinfo.FirstTime),
        //                                DbHelper.MakeInParam("@isipv6",(DbType)SqlDbType.Int, 4, peerinfo.IsIPv6),
        //                                DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char, 15, peerinfo.IPv4IP),                                        
        //                                DbHelper.MakeInParam("@ipv6ip",(DbType)SqlDbType.Char, 45, peerinfo.IPv6IP),
        //                                DbHelper.MakeInParam("@ipv6addip",(DbType)SqlDbType.Char, 45, peerinfo.IPv6IPAdd),
        //                                DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, peerinfo.IsSeed),
        //                                DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, peerinfo.LastTime),
        //                                DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 40, peerinfo.PeerId),
        //                                DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 8, peerinfo.Percentage),
        //                                DbHelper.MakeInParam("@port",(DbType)SqlDbType.Int, 4, peerinfo.Port),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
        //                                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
        //                                DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
        //                                DbHelper.MakeInParam("@uploadspeed",(DbType)SqlDbType.Float, 8, peerinfo.UploadSpeed),
        //                                DbHelper.MakeInParam("@v4v6last",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
        //                          };
        //    string sqlstring = "";
        //    if (ipv6tracker)
        //    {
        //        sqlstring = "UPDATE [bt_peer] SET [peerid] = @peerid, [totalupload] = [totalupload] + @upload - [upload], [totaldownload] = [totaldownload] + @download - [download], [v6last] = @v4v6last, [ip] = @ip, [port] = @port, [ipv6ip] = @ipv6ip, [download] = @download, [downloadspeed] = @downloadspeed,[seed] = @seed,[lasttime] = @lasttime,[percentage] = @percentage,[upload] = @upload,[uploadspeed] = @uploadspeed,[ipv6addip] = @ipv6addip,[isipv6] = @isipv6,[client] = @client ";
        //        sqlstring += "WHERE [seedid] = @seedid AND [uid] = @uid";
        //    }
        //    else
        //    {
        //        sqlstring = "UPDATE [bt_peer] SET [peerid] = @peerid, [totalupload] = [totalupload] + @upload - [upload], [totaldownload] = [totaldownload] + @download - [download], [v4last] = @v4v6last, [ip] = @ip, [port] = @port, [ipv6ip] = @ipv6ip, [download] = @download, [downloadspeed] = @downloadspeed,[seed] = @seed,[lasttime] = @lasttime,[percentage] = @percentage,[upload] = @upload,[uploadspeed] = @uploadspeed,[ipv6addip] = @ipv6addip,[isipv6] = @isipv6,[client] = @client ";
        //        sqlstring += "WHERE [seedid] = @seedid AND [uid] = @uid";
        //    }

        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        
        /// <summary>
        /// 【存储过程】获得指定的peer信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public IDataReader GetPeerInfo(PrivateBTPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
                                  };
            //string sqlstring = "SELECT TOP (1) [firsttime],[lasttime],[download],[upload],[seed],[ip],[ipv6ip],[seedid],[uid],[peerid],[percentage] FROM [bt_peer] WHERE [seedid] = @seedid AND [peerid] = @peerid AND [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, "bt_peer_getpeerinfo", parms);
        }
        /// <summary>
        /// 【存储过程】获得指定的peer信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public IDataReader GetPeerInfo_OldDownload(PrivateBTPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
                                  };
            //string sqlstring = "SELECT TOP (1) [firsttime],[lasttime],[download],[upload],[seed],[ip],[ipv6ip],[seedid],[uid],[peerid],[percentage] FROM [bt_peer] WHERE [seedid] = @seedid AND [peerid] = @peerid AND [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, "bt_peer_getpeerinfo_olddownload", parms);
        }
        ///// <summary>
        ///// 【存储过程】锁定指定的peer信息
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public int LockPeer(PrivateBTPeerInfo peerinfo)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
        //                                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
        //                          };
        //    //string sqlstring = "UPDATE [bt_peer] SET [peerlock] = 1 WHERE [seedid] = @seedid AND [peerid] = @peerid AND [uid] = @uid AND [peerlock] = 0";
        //    return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_lockpeer", parms);
        //}
        ///// <summary>
        ///// 【存储过程】解锁指定的peer信息
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //public int UnLockPeer(PrivateBTPeerInfo peerinfo)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
        //                                DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
        //                          };
        //    //string sqlstring = "UPDATE [bt_peer] SET [peerlock] = 0 WHERE [seedid] = @seedid AND [uid] = @uid AND [peerid] = @peerid";
        //    return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_unlockpeer", parms);
        //}
        /// <summary>
        /// 【存储过程】删除指定的peer信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int DeletePeerInfo(PrivateBTPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                        //DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.VarChar, 24, peerinfo.PeerId),
                                        //DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        //DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
                                        DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, peerinfo.Pid),
                                  };
            //OLD//string sqlstring = "DELETE FROM [bt_peer] WHERE [seedid] = @seedid AND [uid] = @uid AND [peerid] = @peerid";
            //OLD//return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_deletepeer", parms);
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_deletepeer_bypid", parms);
        }
        ///// <summary>
        ///// 删除指定IP正在下载指定种子的peer信息，只删除正在下载的
        ///// //此函数有问题，勿使用
        ///// </summary>
        ///// <param name="ip"></param>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //public int DeletePeerInfo(string ip, int seedid)
        //{
            
        //    DbParameter[] parms = {
                                        
        //                                DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char, 15, ip),
        //                                DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
        //                          };
        //    string sqlstring = "DELETE FROM [bt_peer] WHERE [seedid] = @seedid AND [ip] = @ip AND [percentage] < 1";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        /// <summary>
        /// 【存储过程】删除指定UID正在下载指定种子的peer信息，只删除正在下载的
        /// </summary>
        /// <returns></returns>
        public int DeletePeerInfo(int uid, int seedid)
        {
            DbParameter[] parms = {
                                        
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                  };
            //string sqlstring = "DELETE FROM [bt_peer] WHERE [seedid] = @seedid AND [uid] = @uid AND [percentage] < 1";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_deletepeer_without_peerid", parms);
        }
        /// <summary>
        /// 【存储过程】重置peer列表，删除该用户的所有记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int ResetPeer(int uid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                  };
            //string sqlstring = "DELETE FROM [bt_peer] WHERE [uid] = @userid ";
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_resetpeer", parms);
        }
        /// <summary>
        /// 【存储过程】获取所有长时间无响应的Peer的UID项目,mode=1清理core2的种子，否则不清理
        /// </summary>
        /// <returns></returns>
        public DataTable GetPeerUidListNoResponse(DateTime timelimit, int mode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@timelimit",(DbType)SqlDbType.DateTime, 4, timelimit),
                                  };
            if(mode == 1)
                return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_uid_bylasttime_core2", parms).Tables[0];
            else
                return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_uid_bylasttime", parms).Tables[0];
        }
        /// <summary>
        /// 【存储过程】获取所有长时间无响应的Peer的Seedid项目,mode=1清理core2的种子，否则不清理
        /// </summary>
        /// <returns></returns>
        public DataTable GetPeerSeedIdListNoResponse(DateTime timelimit, int mode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@timelimit",(DbType)SqlDbType.DateTime, 4, timelimit),
                                  };
            if(mode == 1)
                return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_seedid_bylasttime_core2", parms).Tables[0];
            else
                return DbHelper.ExecuteDataset(CommandType.StoredProcedure, "bt_peer_getpeerlist_seedid_bylasttime", parms).Tables[0];
        }

        ///// <summary>
        ///// 将指定时间未更新的IPv6地址置为IP_NULL
        ///// </summary>
        ///// <returns></returns>
        //public int UpdatePeerIPv6NoResponse(DateTime timelimit)
        //{
        //    DbParameter[] parms = {
        //                                DbHelper.MakeInParam("@timelimit",(DbType)SqlDbType.DateTime, 4, timelimit),
        //                          };
        //    string sqlstring = "UPDATE [bt_peer] SET [ipv6ip] = 'IP_NULL',[isipv6] = '0' WHERE [v6last] < @timelimit AND [ipv6ip] != 'IP_NULL'";
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        
        /// <summary>
        /// 删除所有长时间无响应的Peer项目,mode=1清理core2的种子，否则不清理
        /// </summary>
        /// <returns></returns>
        public int DeletePeerListNoResponse(DateTime timelimit, int mode)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@timelimit",(DbType)SqlDbType.DateTime, 4, timelimit),
                                  };

            int a = 0;
            //string sqlstring = "DELETE FROM [bt_peer] WHERE [lasttime] < @timelimit";
            if(mode == 1)
                a = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_deletepeer_bylasttime_core2", parms);
            else
                a = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peer_deletepeer_bylasttime", parms);
            
            //数据库死锁的临时解决方案：停止将不更新的IPv4、IPv6地址置空。不影响系统正常工作。
            //sqlstring = "UPDATE [bt_peer] SET [ip] = 'IP_NULL' WHERE [ip] != 'IP_NULL' AND [v4last] < @timelimit";
            //DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            //sqlstring = "UPDATE [bt_peer] SET [ipv6ip] = 'IP_NULL' WHERE [ipv6ip] != 'IP_NULL' AND [v6last] < @timelimit";
            //DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
            
            return a;
        }


        /// <summary>
        /// 获得当前总下载速度
        /// </summary>
        /// <returns></returns>
        public decimal GetPeerTotalSpeed()
        {
            //string sqlstring = "SELECT ISNULL(SUM([downloadspeed]),0) FROM [bt_peer]"; //可能是空集，输出为0
            return TypeConverter.ObjectToDecimal(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peer_getpeersumspeed").ToString());
        }
    }
}