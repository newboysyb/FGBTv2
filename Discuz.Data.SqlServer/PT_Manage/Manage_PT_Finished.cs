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
        /// 获得对应SeedId的种子的完成节点信息，仅获取uid，用于删除种子发送已完成用户通知
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public DataTable GetUserListFinished(int seedid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid)
                                  };
            string sqlstring = string.Format("SELECT [uid] FROM [bt_finished] WITH(NOLOCK) WHERE [seedid] = @seedid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }

        /// <summary>
        /// 判断用户是否曾经完成过此种子
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        public bool IsUserHaveFinished(int uid, int seedid)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                      DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                  };
            string sqlstring = string.Format("IF EXISTS (SELECT [uid] FROM [bt_finished] WHERE [seedid] = @seedid AND [uid] = @uid) SELECT 'TRUE' ELSE SELECT 'FALSE'", BaseConfigs.GetTablePrefix);
            return TypeConverter.StrToBool(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), false);
        }

        /// <summary>
        /// 插入下载完成记录
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <returns></returns>
        public int InsertFinished(int seedid, int userid, decimal upload, decimal download)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@seedid",(DbType)SqlDbType.Int, 4, seedid),
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@finishtime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@upload",(DbType)SqlDbType.Decimal, 18, upload),
                                        DbHelper.MakeInParam("@download",(DbType)SqlDbType.Decimal, 18, download),
                                  };
            string sqlstring = "INSERT INTO [bt_finished] ([seedid],[uid],[finishtime],[upload],[download]) VALUES(@seedid, @uid, @finishtime, @upload, @download)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
    }
}