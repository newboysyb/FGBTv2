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
        /// 插入Abt种子
        /// </summary>
        /// <returns></returns>
        public int AbtInsertSeed(string infohash, int filecount, decimal filesize, string filename, int uid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@infohash",(DbType)SqlDbType.Char, 40, infohash),
                                        DbHelper.MakeInParam("@filecount",(DbType)SqlDbType.Int, 4, filecount),
                                        DbHelper.MakeInParam("@filesize",(DbType)SqlDbType.Decimal, 20, filesize),

                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Int, 4, 0),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Int, 4, 0),
                                        DbHelper.MakeInParam("@finished",(DbType)SqlDbType.Int, 4, 0),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@lastlive",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@filename",(DbType)SqlDbType.NVarChar, 260, filename),
                                  };
            string sqlstring = "INSERT INTO [abt_seed] ([infohash],[upload],[download],[finished],[lastlive],[filecount],[filesize],[filename],[uid]) ";
            sqlstring += "VALUES(@infohash,@upload,@download,@finished,@lastlive,@filecount,@filesize,@filename,@uid);SELECT SCOPE_IDENTITY()";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }
        /// <summary>
        /// 插入Abt节点信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int AbtInsertPeer(AbtPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, peerinfo.Aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 20, peerinfo.Peerid),
                                        DbHelper.MakeInParam("@ipv4",(DbType)SqlDbType.Char, 15, peerinfo.IPv4),
                                        DbHelper.MakeInParam("@ipv6",(DbType)SqlDbType.Char, 45, peerinfo.IPv6),
                                        DbHelper.MakeInParam("@port",(DbType)SqlDbType.Int, 4, peerinfo.Port),
                                        DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 4, peerinfo.Percentage),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "INSERT INTO [abt_peer] ([aid],[uid],[peerid],[ipv4],[ipv6],[port],[percentage],[lasttime]) ";
            sqlstring += "VALUES(@aid,@uid,@peerid,@ipv4,@ipv6,@port,@percentage,@lasttime)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 插入Abt下载信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int AbtInsertDownload(int aid, int uid, string passkey, string infohash, int status)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@passkey",(DbType)SqlDbType.Char, 32, passkey),
                                        DbHelper.MakeInParam("@infohash",(DbType)SqlDbType.Char, 8, infohash),
                                        DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt, 1, status),
                                        DbHelper.MakeInParam("@recordtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 20, ""),
                                         DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 4, 0),
                                  };
            string sqlstring = "INSERT INTO [abt_download] ([aid],[uid],[passkey],[infohash],[status],[recordtime],[lasttime],[peerid],[percentage]) ";
            sqlstring += "VALUES(@aid,@uid,@passkey,@infohash,@status,@recordtime,@lasttime,@peerid,@percentage)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 插入Abt日志信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int AbtInsertLog(int aid, int uid, int type, string msg)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@type",(DbType)SqlDbType.TinyInt, 1, type),
                                        DbHelper.MakeInParam("@msg",(DbType)SqlDbType.NVarChar, 100, msg),
                                        DbHelper.MakeInParam("@recordtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "INSERT INTO [abt_log] ([aid],[uid],[type],[msg],[recordtime]) ";
            sqlstring += "VALUES(@aid,@uid,@type,@msg,@recordtime)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }



        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 获取Abt种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public IDataReader AbtGetSeedInfo(int aid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                  };
            string sqlstring = "SELECT TOP (1) [aid],[uid],[infohash],[upload],[download],[finished],[lastlive],[filecount],[filesize],[filename] FROM [abt_seed] WHERE [aid] = @aid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取Abt种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public DataTable AbtGetSeedInfoList(DataTable aid)
        {
            DbParameter[] parms = {
                                      new SqlParameter("@TVP", aid),
                                  };
            ((SqlParameter)parms[0]).SqlDbType = SqlDbType.Structured;
            ((SqlParameter)parms[0]).TypeName = "fgbtIntTable";
            //此处表值参数创建是不应创建索引，否则将被排序
            string sqlstring = "SELECT A.[IntValue] AS [aid],B.[uid],B.[upload],B.[download],B.[finished],B.[lastlive],B.[filecount],B.[filesize] FROM (SELECT [IntValue],ROW_NUMBER() OVER (ORDER BY (SELECT 0)) AS [RNUM] FROM @TVP) AS A LEFT JOIN [abt_seed] AS B ON A.[IntValue] = B.[aid] ORDER BY A.[RNUM]";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取Abt种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public DataTable AbtGetSeedInfoList(DateTime lasttime)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, lasttime),
                                  };

            string sqlstring = "SELECT B.[aid],B.[uid],B.[upload],B.[download],B.[finished],B.[lastlive],B.[filecount],B.[filesize],A.[tid] FROM [abt_seed] AS B LEFT JOIN [dnt_topics] AS A ON A.[seedid] = -B.[aid] WHERE B.[lastlive] < @lasttime";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取Abt节点信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public IDataReader AbtGetPeerInfo(int aid, string peerid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 20, peerid),
                                  };
            string sqlstring = "SELECT TOP (1) [aid],[uid],[peerid],[ipv4],[ipv6],[port],[percentage],[lasttime] FROM [abt_peer] WHERE [aid] = @aid AND [peerid] = @peerid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取Abt节点数
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public int AbtGetPeerCount(int aid, bool isupload)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                  };
            string sqlstring = "SELECT COUNT (*) FROM [abt_peer] WHERE [aid] = @aid";
            if (isupload) sqlstring += " AND [percentage] = 1";
            else sqlstring += " AND [percentage] < 1";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        }
        /// <summary>
        /// 获取Abt下载记录
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public IDataReader AbtGetDownload(int aid, int uid, string passkey)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                      DbHelper.MakeInParam("@passkey",(DbType)SqlDbType.Char, 32, passkey),
                                  };
            string sqlstring = "SELECT TOP (1) [aid],[uid],[passkey],[infohash],[peerid],[status],[recordtime],[lasttime],[percentage] FROM [abt_download] WHERE [aid] = @aid AND [uid] = @uid AND [passkey] = @passkey";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取Abt下载记录
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public IDataReader AbtGetDownload(int aid, int uid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                  };
            string sqlstring = "SELECT TOP (1) [aid],[uid],[passkey],[infohash],[peerid],[status],[recordtime],[lasttime],[percentage] FROM [abt_download] WHERE [aid] = @aid AND [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取Abt节点列表（IPv4、IPv6、Port列表）
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="isupload"></param>
        /// <returns></returns>
        public DataTable AbtGetPeerList(int aid, bool isupload)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                  };
            string sqlstring = "SELECT [ipv4],[ipv6],[port] FROM [abt_peer] WHERE [aid] = @aid";
            if (isupload) sqlstring += " AND [percentage] = 1";
            else sqlstring += " AND [percentage] < 1";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 获取Abt节点列表（aid列表）
        /// </summary>
        /// <returns></returns>
        public DataTable AbtGetPeerList(DateTime lasttime)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, lasttime),
                                  };
            string sqlstring = "SELECT [aid] FROM [abt_peer] WHERE [lasttime] < @lasttime";
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }



        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 删除Abt节点
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public int AbtDeletePeer(DateTime lasttime)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, lasttime),
                                  };
            string sqlstring = "DELETE FROM [abt_peer] WHERE [lasttime] < @lasttime";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除Abt节点
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public int AbtDeletePeer(int aid, string peerid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 20, peerid),
                                  };
            string sqlstring = "DELETE FROM [abt_peer] WHERE [aid] = @aid AND [peerid] = @peerid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public int AbtDeleteDownload(DateTime lasttime)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, lasttime),
                                  };
            string sqlstring = "DELETE FROM [abt_download] WHERE [lasttime] < @lasttime";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public int AbtDeleteDownload(int aid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                  };
            string sqlstring = "DELETE FROM [abt_download] WHERE [aid] = @aid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除Abt下载记录
        /// </summary>
        /// <param name="lasttime"></param>
        /// <returns></returns>
        public int AbtDeleteDownload(int aid, int uid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                  };
            string sqlstring = "DELETE FROM [abt_download] WHERE [aid] = @aid AND [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public int AbtDeleteSeed(DateTime lastlive)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@lastlive",(DbType)SqlDbType.DateTime, 8, lastlive),
                                  };
            string sqlstring = "DELETE FROM [abt_seed] WHERE [lastlive] < @lastlive";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除Abt种子
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="peerid"></param>
        /// <returns></returns>
        public int AbtDeleteSeed(int aid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                  };
            string sqlstring = "DELETE FROM [abt_seed] WHERE [aid] = @aid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 更新Abt节点信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int AbtUpdatePeer(AbtPeerInfo peerinfo)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, peerinfo.Aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, peerinfo.Uid),
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 20, peerinfo.Peerid),
                                        DbHelper.MakeInParam("@ipv4",(DbType)SqlDbType.Char, 15, peerinfo.IPv4),
                                        DbHelper.MakeInParam("@ipv6",(DbType)SqlDbType.Char, 45, peerinfo.IPv6),
                                        DbHelper.MakeInParam("@port",(DbType)SqlDbType.Int, 4, peerinfo.Port),
                                        DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 4, peerinfo.Percentage),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "UPDATE [abt_peer] SET [ipv4] = @ipv4, [ipv6] = @ipv6, [port] = @port, [percentage] = @percentage, [lasttime] = @lasttime WHERE [aid] = @aid AND [peerid] = @peerid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新Abt下载信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        public int AbtUpdateDownload(int aid, int uid, string passkey, int status, string peerid, float percentage)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@peerid",(DbType)SqlDbType.Char, 20, peerid),
                                        DbHelper.MakeInParam("@passkey",(DbType)SqlDbType.Char, 32, passkey),
                                        DbHelper.MakeInParam("@percentage",(DbType)SqlDbType.Float, 4, percentage),
                                        DbHelper.MakeInParam("@status",(DbType)SqlDbType.TinyInt, 4, status),
                                        DbHelper.MakeInParam("@lasttime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "UPDATE [abt_download] SET [peerid] = @peerid, [percentage] = @percentage, [status] = @status, [lasttime] = @lasttime WHERE [aid] = @aid AND [uid] = @uid AND [passkey] = @passkey";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新种子信息
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="updatelive"></param>
        /// <returns></returns>
        public int AbtUpdateSeed(int aid, int upload, int download, bool updatelive, bool addfinished)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@aid",(DbType)SqlDbType.Int, 4, aid),
                                      DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Int, 4, upload),
                                      DbHelper.MakeInParam("@download",(DbType)SqlDbType.Int, 4, download),
                                      DbHelper.MakeInParam("@lastlive",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "UPDATE [abt_seed] SET [upload] = @upload, [download] = @download WHERE [aid] = @aid";
            if (addfinished) sqlstring = "UPDATE [abt_seed] SET [upload] = @upload, [download] = @download, [lastlive] = @lastlive, [finished] = [finished] + 1 WHERE [aid] = @aid";
            else if (updatelive) sqlstring = "UPDATE [abt_seed] SET [upload] = @upload, [download] = @download, [lastlive] = @lastlive WHERE [aid] = @aid";
            
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }



    }
}
