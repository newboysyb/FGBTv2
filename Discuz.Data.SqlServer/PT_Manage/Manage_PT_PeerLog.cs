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
        public int InsertPeerHistoryLog(int pid, int seedid, int uid, int loglevel, string logtype, string message)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, pid), 
                                       DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid), 
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid), 

                                       DbHelper.MakeInParam("@loglevel",(DbType)SqlDbType.TinyInt, 1, loglevel),
                                       DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.VarChar, 20, logtype),
                                       DbHelper.MakeInParam("@message",(DbType)SqlDbType.NVarChar, 500, message),
                                       DbHelper.MakeInParam("@logtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                       
                                  };
            string sqlstring = "INSERT INTO [bt_peer_historylog] ([pid],[seedid],[uid],[log_level],[log_type],[log_message],[log_time]) VALUES(@pid, @seedid, @uid, @loglevel, @logtype ,@message, @logtime)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 插入peer error记录信息
        /// </summary>
        /// <returns></returns>
        public int InsertPeerErrorInfo(int uid, int seedid, int errortype, int errorlevel, int ipregion, string iptail, int preipregion, string preiptail, int preipv6region, string preipv6tail,
            decimal upload, decimal preupload, decimal download, decimal predownload, string client, int port, string message)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@recordtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@errortype",(DbType)SqlDbType.SmallInt, 2, errortype),
                                        DbHelper.MakeInParam("@errorlevel",(DbType)SqlDbType.SmallInt, 2, errorlevel),
                                        DbHelper.MakeInParam("@ipregion",(DbType)SqlDbType.Int, 4, ipregion),
                                        DbHelper.MakeInParam("@iptail",(DbType)SqlDbType.VarChar, 25, iptail),
                                        DbHelper.MakeInParam("@preipregion",(DbType)SqlDbType.Int, 4, preipregion),
                                        DbHelper.MakeInParam("@preiptail",(DbType)SqlDbType.VarChar, 25, preiptail),
                                        DbHelper.MakeInParam("@preipv6region",(DbType)SqlDbType.Int, 4, preipv6region),
                                        DbHelper.MakeInParam("@preipv6tail",(DbType)SqlDbType.VarChar, 25, preipv6tail),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, upload),
                                        DbHelper.MakeInParam("@preupload",(DbType)SqlDbType.Decimal, 18, preupload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, download),
                                        DbHelper.MakeInParam("@predownload",(DbType)SqlDbType.Decimal, 18, predownload),
                                        DbHelper.MakeInParam("@client",(DbType)SqlDbType.Char, 20, client),
                                        DbHelper.MakeInParam("@port",(DbType)SqlDbType.Int, 4, port),
                                        DbHelper.MakeInParam("@message",(DbType)SqlDbType.NVarChar, 50, message),
                                  };
            //string sqlstring = "INSERT INTO [bt_peer_error] ([recordtime],[uid],[seedid],[errortype],[errorlevel],[ipregion],[iptail],[preipregion],[preiptail],[preipv6region],[preipv6tail],[upload],[preupload],[download],[predownload],[client],[port],[message])";
            //sqlstring += " VALUES (@recordtime, @uid, @seedid, @errortype, @errorlevel, @ipregion, @iptail, @preipregion, @preiptail, @preipv6region, @preipv6tail, @upload, @preupload, @download, @predownload, @client, @port, @message);SELECT SCOPE_IDENTITY()";
            //return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -2);

            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, "bt_peererror_insertpeererror", parms), -2);
        }
        /// <summary>
        /// 插入peer日志信息
        /// </summary>
        /// <param name="fromipregion"></param>
        /// <param name="fromiptail"></param>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int InsertPeerLog(int eid, int loglevel, int fromipregion, string fromiptail, string rawstring, PrivateBTPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@eid",(DbType)SqlDbType.Int, 2, eid),
                                        DbHelper.MakeInParam("@recordtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@loglevel",(DbType)SqlDbType.SmallInt, 2, loglevel),
                                        DbHelper.MakeInParam("@fromipregion",(DbType)SqlDbType.Int, 4, fromipregion),
                                        DbHelper.MakeInParam("@fromiptail",(DbType)SqlDbType.VarChar, 25, fromiptail),
                                        DbHelper.MakeInParam("@rawstring",(DbType)SqlDbType.VarChar, 500, rawstring),

                                        DbHelper.MakeInParam("@client",(DbType)SqlDbType.Char, 20, peerinfo.Client),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
                                        DbHelper.MakeInParam("@downloadspeed",(DbType)SqlDbType.Float, 8, peerinfo.DownloadSpeed),
                                        DbHelper.MakeInParam("@firsttime",(DbType)SqlDbType.DateTime, 8, peerinfo.FirstTime),
                                        DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char, 15, peerinfo.IPv4IP),
                                        //DbHelper.MakeInParam("@isipv6",(DbType)SqlDbType.Int, 4, peerinfo.IPStatus),
                                        DbHelper.MakeInParam("@ipv6ip",(DbType)SqlDbType.Char, 45, peerinfo.IPv6IP),
                                        DbHelper.MakeInParam("@ipv6addip",(DbType)SqlDbType.Char, 45, peerinfo.IPv6IPAdd),
                                        //DbHelper.MakeInParam("@seed",(DbType)SqlDbType.Bit, 1, peerinfo.IsSeed),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, peerinfo.LastTime),
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 40, peerinfo.PeerId),
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
                                  };
            //string sqlstring = "INSERT INTO [bt_peer_log] ([eid],[recordtime],[loglevel],[uid],[seedid],[fromipregion],[fromiptail],[peerid],[ip],[v4last],[ipv6ip],[v6last],[ipv6addip],[port],[percentage],[upload],[uploadspeed],[download],[downloadspeed],[firsttime],[lasttime],[client],[totalupload],[totaldownload],[rawstring])";
            //sqlstring += " VALUES (@eid, @recordtime, @loglevel, @uid, @seedid, @fromipregion, @fromiptail, @peerid, @ip, @v4last, @ipv6ip, @v6last, @ipv6addip, @port, @percentage, @upload, @uploadspeed, @download, @downloadspeed, @firsttime, @lasttime, @client, @totalupload, @totaldownload, @rawstring)";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peerlog_insertpeerlog", parms);
        }

        
        public int InsertPeerHistory(PrivateBTPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, peerinfo.Pid),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),

                                        DbHelper.MakeInParam("@start_ip",(DbType)SqlDbType.VarChar, 45, peerinfo.IPv4IP == "IP_NULL" ? peerinfo.IPv6IP: peerinfo.IPv4IP),
                                        DbHelper.MakeInParam("@start_time",(DbType)SqlDbType.DateTime, 8, peerinfo.FirstTime),
                                        DbHelper.MakeInParam("@start_port",(DbType)SqlDbType.Int, 4, peerinfo.Port),
                                        DbHelper.MakeInParam("@start_client",(DbType)SqlDbType.VarChar, 20, peerinfo.Client),

                                        DbHelper.MakeInParam("@start_percentage",(DbType)SqlDbType.Float, 8, peerinfo.Percentage),
                                        DbHelper.MakeInParam("@start_download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
                                        DbHelper.MakeInParam("@start_upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
                                        DbHelper.MakeInParam("@start_left",(DbType)SqlDbType.Decimal, 18, peerinfo.Left),

                                        DbHelper.MakeInParam("@start_totalupload",(DbType)SqlDbType.Decimal, 32, peerinfo.TotalUpload),
                                        DbHelper.MakeInParam("@start_totaldownload",(DbType)SqlDbType.Decimal, 32, peerinfo.TotalDownload),

                                        //DbHelper.MakeInParam("@start_seedupload",(DbType)SqlDbType.Decimal, 32, seedinfo.UpTraffic),
                                        //DbHelper.MakeInParam("@start_seeddownload",(DbType)SqlDbType.Decimal, 32, seedinfo.Traffic),
                                        //DbHelper.MakeInParam("@start_seedupcount",(DbType)SqlDbType.Int, 4, seedinfo.Upload),
                                        //DbHelper.MakeInParam("@start_seeddowncount",(DbType)SqlDbType.Int, 4, seedinfo.Download),

                                        //DbHelper.MakeInParam("@end_time",(DbType)SqlDbType.DateTime, 8, DateTime.MinValue),
                                        //DbHelper.MakeInParam("@end_type",(DbType)SqlDbType.TinyInt, 4, 0),
                                        //DbHelper.MakeInParam("@end_trafficdiff",(DbType)SqlDbType.Decimal, 45, 0),

                                        //DbHelper.MakeInParam("@end_percentage",(DbType)SqlDbType.Float, 8, -1),
                                        //DbHelper.MakeInParam("@end_download",(DbType)SqlDbType.Decimal, 18, -1),
                                        //DbHelper.MakeInParam("@end_upload",(DbType)SqlDbType.Decimal, 18, -1),
                                        //DbHelper.MakeInParam("@end_left",(DbType)SqlDbType.Decimal, 18, -1),

                                        //DbHelper.MakeInParam("@end_seedupload",(DbType)SqlDbType.Decimal, 32, -1),
                                        //DbHelper.MakeInParam("@end_seeddownload",(DbType)SqlDbType.Decimal, 32, -1),
                                        //DbHelper.MakeInParam("@end_seedupcount",(DbType)SqlDbType.Int, 4, -1),
                                        //DbHelper.MakeInParam("@end_seeddowncount",(DbType)SqlDbType.Int, 4, -1),
                                  };
            //string sqlstring = "INSERT INTO [bt_peer_log] ([eid],[recordtime],[loglevel],[uid],[seedid],[fromipregion],[fromiptail],[peerid],[ip],[v4last],[ipv6ip],[v6last],[ipv6addip],[port],[percentage],[upload],[uploadspeed],[download],[downloadspeed],[firsttime],[lasttime],[client],[totalupload],[totaldownload],[rawstring])";
            //sqlstring += " VALUES (@eid, @recordtime, @loglevel, @uid, @seedid, @fromipregion, @fromiptail, @peerid, @ip, @v4last, @ipv6ip, @v6last, @ipv6addip, @port, @percentage, @upload, @uploadspeed, @download, @downloadspeed, @firsttime, @lasttime, @client, @totalupload, @totaldownload, @rawstring)";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peerhistory_insert", parms);
        }

        public int UpdatePeerHistory(PrivateBTPeerInfo peerinfo, int end_type)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@pid",(DbType)SqlDbType.Int, 4, peerinfo.Pid),
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, peerinfo.SeedId),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),

                                        //DbHelper.MakeInParam("@start_ip",(DbType)SqlDbType.VarChar, 45, peerinfo.IPv4IP == "IP_NULL" ? peerinfo.IPv6IP: peerinfo.IPv4IP),
                                        //DbHelper.MakeInParam("@start_time",(DbType)SqlDbType.DateTime, 8, peerinfo.FirstTime),
                                        //DbHelper.MakeInParam("@start_port",(DbType)SqlDbType.Int, 4, peerinfo.Port),
                                        //DbHelper.MakeInParam("@start_client",(DbType)SqlDbType.VarChar, 20, peerinfo.Client),

                                        //DbHelper.MakeInParam("@start_percentage",(DbType)SqlDbType.Float, 8, peerinfo.Percentage),
                                        //DbHelper.MakeInParam("@start_download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
                                        //DbHelper.MakeInParam("@start_upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
                                        //DbHelper.MakeInParam("@start_left",(DbType)SqlDbType.Decimal, 18, peerinfo.Left),

                                        //DbHelper.MakeInParam("@start_totalupload",(DbType)SqlDbType.Decimal, 32, peerinfo.TotalUpload),
                                        //DbHelper.MakeInParam("@start_totaldownload",(DbType)SqlDbType.Decimal, 32, peerinfo.TotalDownload),

                                        //DbHelper.MakeInParam("@start_seedupload",(DbType)SqlDbType.Decimal, 32, seedinfo.UpTraffic),
                                        //DbHelper.MakeInParam("@start_seeddownload",(DbType)SqlDbType.Decimal, 32, seedinfo.Traffic),
                                        //DbHelper.MakeInParam("@start_seedupcount",(DbType)SqlDbType.Int, 4, seedinfo.Upload),
                                        //DbHelper.MakeInParam("@start_seeddowncount",(DbType)SqlDbType.Int, 4, seedinfo.Download),

                                        DbHelper.MakeInParam("@end_time",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@end_type",(DbType)SqlDbType.TinyInt, 4, end_type),
                                        //DbHelper.MakeInParam("@end_trafficdiff",(DbType)SqlDbType.Decimal, 45, 0),

                                        DbHelper.MakeInParam("@end_percentage",(DbType)SqlDbType.Float, 8, peerinfo.Percentage),
                                        DbHelper.MakeInParam("@end_download",(DbType)SqlDbType.Decimal, 18, peerinfo.Download),
                                        DbHelper.MakeInParam("@end_upload",(DbType)SqlDbType.Decimal, 18, peerinfo.Upload),
                                        DbHelper.MakeInParam("@end_left",(DbType)SqlDbType.Decimal, 18, peerinfo.Left),

                                        //DbHelper.MakeInParam("@end_seedupload",(DbType)SqlDbType.Decimal, 32, -1),
                                        //DbHelper.MakeInParam("@end_seeddownload",(DbType)SqlDbType.Decimal, 32, -1),
                                        //DbHelper.MakeInParam("@end_seedupcount",(DbType)SqlDbType.Int, 4, -1),
                                        //DbHelper.MakeInParam("@end_seeddowncount",(DbType)SqlDbType.Int, 4, -1),
                                  };
            //string sqlstring = "INSERT INTO [bt_peer_log] ([eid],[recordtime],[loglevel],[uid],[seedid],[fromipregion],[fromiptail],[peerid],[ip],[v4last],[ipv6ip],[v6last],[ipv6addip],[port],[percentage],[upload],[uploadspeed],[download],[downloadspeed],[firsttime],[lasttime],[client],[totalupload],[totaldownload],[rawstring])";
            //sqlstring += " VALUES (@eid, @recordtime, @loglevel, @uid, @seedid, @fromipregion, @fromiptail, @peerid, @ip, @v4last, @ipv6ip, @v6last, @ipv6addip, @port, @percentage, @upload, @uploadspeed, @download, @downloadspeed, @firsttime, @lasttime, @client, @totalupload, @totaldownload, @rawstring)";
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_peerhistory_update", parms);
        }



    }
}
