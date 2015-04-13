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
        /// 获取CNGI联盟的用户ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public int GetCNGIUserID(string cngi_name, string cngi_school)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@cngi_name",(DbType)SqlDbType.NChar, 100, cngi_name),
                                        DbHelper.MakeInParam("@cngi_school",(DbType)SqlDbType.NChar, 100, cngi_school),
                                  };

            string sqlstring = "SELECT [uid] FROM [bt_cngi] WHERE [cngi_school] = @cngi_school AND [cngi_name]=@cngi_name";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        /// <summary>
        /// 判断未来花园账号是否已经被绑定
        /// </summary>
        /// <param name="username"></param>
        /// <param name="school"></param>
        /// <returns></returns>
        public int IsCNGIUser(int userid)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                  };

            string sqlstring = "SELECT COUNT([uid]) FROM [bt_cngi] WHERE [uid]=@uid";
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }

        /// <summary>
        /// 绑定CNGI联盟用户id
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="cngi_name"></param>
        /// <param name="cngi_school"></param>
        /// <returns></returns>
        public int SetCNGIUserID(int userid, string cngi_name, string cngi_school)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, userid),
                                        DbHelper.MakeInParam("@cngi_name",(DbType)SqlDbType.NChar, 100, cngi_name),
                                        DbHelper.MakeInParam("@cngi_school",(DbType)SqlDbType.NChar, 100, cngi_school),
                                        DbHelper.MakeInParam("@settime",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "INSERT INTO [bt_cngi] ([uid],[cngi_name],[cngi_school],[settime]) ";
            sqlstring += " VALUES(@uid, @cngi_name, @cngi_school, @settime)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
    }
}