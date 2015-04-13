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
        /// 添加RSA记录
        /// </summary>
        /// <returns>数据库更改行数</returns>
        public int InsertRsaRecord(int uid, string rsaxml, string rkey)
        {
            DbParameter[] parms = {
                                      	DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@rsaxml",(DbType)SqlDbType.NText, 0, rsaxml),
                                        DbHelper.MakeInParam("@rkey",(DbType)SqlDbType.NChar, 10, rkey),
									   };

            string sqlstring = "INSERT INTO [bt_rsa] " +
                "([uid],[lastupdate],[rsaxml],[rkey]) " +
                "VALUES(@uid,@lastupdate,@rsaxml,@rkey)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新RSA条目
        /// </summary>
        /// <returns></returns>
        public int UpdateRsaRecord(int uid, string rsaxml, string rkey)
        {
            DbParameter[] parms = {
                                      	DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@lastupdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@rsaxml",(DbType)SqlDbType.NText, 0, rsaxml),
                                        DbHelper.MakeInParam("@rkey",(DbType)SqlDbType.NChar, 10, rkey),
									   };

            string sqlstring = "UPDATE [bt_rss] SET " +
                "[lastupdate] = @lastupdate, [rsaxml] = @rsaxml, [rkey] = @rkey " +
                "WHERE [uid] = @uid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取RSA记录
        /// </summary>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        public IDataReader GetRsaRecord(int uid)
        {
            DbParameter[] parms = {
                            DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                        };
            string sqlstring = "SELECT [uid],[lastupdate],[rkey],[rsaxml] FROM [bt_ras] WHERE [uid] = @uid";
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }


    }
}
