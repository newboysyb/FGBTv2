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
        /// 读取ssoinfo列表
        /// </summary>
        public const string SQL_SSOINFO_LIST = " [uid],[ssouid],[ssoname],[ssostatus],[token],[tokendate],[tokenstatus],[token1] ";

        public const string SQL_INSERT_SSOINFO_LIST = " [uid],[ssouid],[ssoname],[ssostatus],[token],[tokendate],[tokenstatus],[token1],[ssodate],[ssoconfirmdate] ";

        public const string SQL_INSERT_SSOINFO_VALUE = " @uid, @ssouid, @ssoname, @ssostatus, @token, @tokendate, @tokenstatus, @token1, @ssodate, @ssoconfirmdate ";

        /// <summary>
        /// 获取seedinfo中所有seedinfoshort包含的项目列表，bt_seed表中列名，全26项
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        private DbParameter[] GetBuaaSSOInfoParameter(PTBuaaSSOinfo ssoinfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, ssoinfo.Uid),
                                        DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int, 4, ssoinfo.ssoUid),
                                        DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar, 20, ssoinfo.ssoName),
                                        DbHelper.MakeInParam("@ssostatus",(DbType)SqlDbType.Float, 8, ssoinfo.ssoStatus),
                                        DbHelper.MakeInParam("@token",(DbType)SqlDbType.NChar, 64, ssoinfo.Token),
                                        DbHelper.MakeInParam("@tokendate",(DbType)SqlDbType.DateTime, 8, ssoinfo.TokenDate),
                                        DbHelper.MakeInParam("@tokenstatus",(DbType)SqlDbType.Int, 4, ssoinfo.TokenStatus),
                                        DbHelper.MakeInParam("@token1",(DbType)SqlDbType.NChar, 64, ssoinfo.Token1),
                                        DbHelper.MakeInParam("@ssodate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                        DbHelper.MakeInParam("@ssoconfirmdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            return parms;
        }

        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetBuaaSSOInfobyUid(int uid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
			                        };
            string sqlstring = string.Format("SELECT TOP (1) {0} FROM [bt_buaasso] WHERE [uid] = @uid", SQL_SSOINFO_LIST);
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetBuaaSSOInfobyssoUid(int ssoUid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int,4,ssoUid),
			                        };
            string sqlstring = string.Format("SELECT TOP (1) {0} FROM [bt_buaasso] WHERE [ssouid] = @ssouid", SQL_SSOINFO_LIST);
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetBuaaSSOInfobyssoNameOldData(string ssoname)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
                                       DbHelper.MakeInParam("@ssoconfirmdate",(DbType)SqlDbType.DateTime, 8, DateTime.Parse("2012-09-20 14:48:27.937")),
			                        };
            string sqlstring = string.Format("SELECT TOP (1) {0} FROM [bt_buaasso] WHERE [ssoname] = @ssoname AND [ssoconfirmdate] < @ssoconfirmdate", SQL_SSOINFO_LIST);
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetBuaaSSOInfobyToken(string token)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@token",(DbType)SqlDbType.NChar,64,token),
			                        };
            string sqlstring = string.Format("SELECT TOP (1) {0} FROM [bt_buaasso] WHERE [token] = @token", SQL_SSOINFO_LIST);
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 读取ssoinfo
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetBuaaSSOInfobyToken1(string token1)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@token1",(DbType)SqlDbType.NChar,64,token1),
			                        };
            string sqlstring = string.Format("SELECT TOP (1) {0} FROM [bt_buaasso] WHERE [token1] = @token1", SQL_SSOINFO_LIST);
            return DbHelper.ExecuteReader(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 插入sso记录
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        public int InsertBuaaSSOInfo(PTBuaaSSOinfo ssoinfo)
        {
            DbParameter[] parms = GetBuaaSSOInfoParameter(ssoinfo);
            string sqlstring = "INSERT INTO [bt_buaasso] ( " + SQL_INSERT_SSOINFO_LIST + " ) VALUES( " + SQL_INSERT_SSOINFO_VALUE + " )";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除指定uid对应的sso记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteBuaaSSOInfobyUid(int uid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
			                        };
            string sqlstring = string.Format("DELETE FROM [bt_buaasso] WHERE [uid] = @uid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 删除指定ssoname对应的sso记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteBuaaSSOInfobyssoName(string ssoname)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
			                        };
            string sqlstring = string.Format("DELETE FROM [bt_buaasso] WHERE [ssoname] = @ssoname");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新ssoname，条件uid和ssostatus=0，更新后ssostatus=2
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfossoNamebyUid(int uid, string ssoname)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
                                       DbHelper.MakeInParam("@ssoconfirmdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [ssoname] = @ssoname,[ssostatus] = 2,[ssoconfirmdate] = @ssoconfirmdate WHERE [uid] = @uid AND [ssostatus] = 0");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 更新ssouid，条件ssoname和uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfossoUidbyssoName(int uid, string ssoname, int ssouid)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
                                       DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int,4,ssouid),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [ssouid] = @ssouid WHERE [uid] = @uid AND [ssoname] = @ssoname");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新ssoname，条件ssouid和uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfossoNamebyssoUid(int uid, int ssouid, string ssoname)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
                                       DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int,4,ssouid),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [ssoname] = @ssoname WHERE [uid] = @uid AND [ssouid] = @ssouid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        ///// <summary>
        ///// 更新uid，条件ssoname和ssostatus=1，更新后ssostatus=3
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="ssoname"></param>
        ///// <returns></returns>
        //public int UpdateBuaaSSOInfoUidbyssoName(int uid, string ssoname)
        //{
        //    DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
        //                               DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
        //                               DbHelper.MakeInParam("@ssoconfirmdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
        //                            };
        //    string sqlstring = string.Format("UPDATE [bt_buaasso] SET [uid] = @uid,[ssostatus] = 3,[ssoconfirmdate] = @ssoconfirmdate WHERE [ssoname] = @ssoname AND [ssostatus] = 1");
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        /// <summary>
        /// 更新uid，条件ssouid和ssostatus=1，更新后ssostatus=3
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfoUidbyssoUid(int uid, int ssouid)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int,4,ssouid),
                                       DbHelper.MakeInParam("@ssoconfirmdate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [uid] = @uid,[ssostatus] = 3,[ssoconfirmdate] = @ssoconfirmdate WHERE [ssouid] = @ssouid AND [ssostatus] = 1");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 更新ssostatus，条件uid
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ssoname"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfossoStatusbyUid(int uid, int ssostatus)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@ssostatus",(DbType)SqlDbType.Int,4,ssostatus),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [ssostatus] = @ssostatus WHERE [uid] = @uid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新token和tokendate，条件uid，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfoTokenbyUid(int uid, string token)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@token",(DbType)SqlDbType.Char,64,token),
                                       DbHelper.MakeInParam("@tokendate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [token] = @token,[tokendate] = @tokendate,[tokenstatus] = 1 WHERE [uid] = @uid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新token1，token和tokendate，条件uid，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfoToken1byUid(int uid, string token, string token1)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@token",(DbType)SqlDbType.Char,64,token),
                                       DbHelper.MakeInParam("@token1",(DbType)SqlDbType.Char,64,token1),
                                       DbHelper.MakeInParam("@tokendate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [token] = @token,[tokendate] = @tokendate,[token1] = @token1,[tokenstatus] = 1 WHERE [uid] = @uid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        ///// <summary>
        ///// 更新token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        ///// </summary>
        ///// <param name="ssoname"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public int UpdateBuaaSSOInfoTokenbyssoName(string ssoname, string token)
        //{
        //    DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
        //                               DbHelper.MakeInParam("@token",(DbType)SqlDbType.Char,64,token),
        //                               DbHelper.MakeInParam("@tokendate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
        //                            };
        //    string sqlstring = string.Format("UPDATE [bt_buaasso] SET [token] = @token,[tokendate] = @tokendate,[tokenstatus] = 1 WHERE [ssoname] = @ssoname");
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        /// <summary>
        /// 更新token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="ssoname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfoTokenbyssoUid(int ssouid, string token)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int,4,ssouid),
									   DbHelper.MakeInParam("@token",(DbType)SqlDbType.Char,64,token),
                                       DbHelper.MakeInParam("@tokendate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [token] = @token,[tokendate] = @tokendate,[tokenstatus] = 1 WHERE [ssouid] = @ssouid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        ///// <summary>
        ///// 更新token1，token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        ///// </summary>
        ///// <param name="ssoname"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public int UpdateBuaaSSOInfoToken1byssoName(string ssoname, string token, string token1)
        //{
        //    DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
        //                               DbHelper.MakeInParam("@token",(DbType)SqlDbType.Char,64,token),
        //                               DbHelper.MakeInParam("@token1",(DbType)SqlDbType.Char,64,token1),
        //                               DbHelper.MakeInParam("@tokendate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
        //                            };
        //    string sqlstring = string.Format("UPDATE [bt_buaasso] SET [token] = @token,[tokendate] = @tokendate,[token1] = @token1,[tokenstatus] = 1 WHERE [ssoname] = @ssoname");
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        /// <summary>
        /// 更新token1，token和tokendate，条件ssoname，更新后tokenstatus=1（可用）
        /// </summary>
        /// <param name="ssoname"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfoToken1byssoUid(int ssouid, string token, string token1)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int,4,ssouid),
									   DbHelper.MakeInParam("@token",(DbType)SqlDbType.Char,64,token),
                                       DbHelper.MakeInParam("@token1",(DbType)SqlDbType.Char,64,token1),
                                       DbHelper.MakeInParam("@tokendate",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [token] = @token,[tokendate] = @tokendate,[token1] = @token1,[tokenstatus] = 1 WHERE [ssouid] = @ssouid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tokenstatus"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfoTokenStatusbyUid(int uid, int tokenstatus)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
                                       DbHelper.MakeInParam("@tokenstatus",(DbType)SqlDbType.Int,4,tokenstatus),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [tokenstatus] = @tokenstatus WHERE [uid] = @uid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        ///// <summary>
        ///// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="tokenstatus"></param>
        ///// <returns></returns>
        //public int UpdateBuaaSSOInfoTokenStatusbyssoName(string ssoname, int tokenstatus)
        //{
        //    DbParameter[] parms = {
        //                               DbHelper.MakeInParam("@ssoname",(DbType)SqlDbType.NChar,20,ssoname),
        //                               DbHelper.MakeInParam("@tokenstatus",(DbType)SqlDbType.Int,4,tokenstatus),
        //                            };
        //    string sqlstring = string.Format("UPDATE [bt_buaasso] SET [tokenstatus] = @tokenstatus WHERE [ssoname] = @ssoname");
        //    return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        //}
        /// <summary>
        /// 更新tokenstatus,令牌状态：1可用，2登陆标记，-1不可用
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tokenstatus"></param>
        /// <returns></returns>
        public int UpdateBuaaSSOInfoTokenStatusbyssoUid(int ssouid, int tokenstatus)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@ssouid",(DbType)SqlDbType.Int,4,ssouid),
                                       DbHelper.MakeInParam("@tokenstatus",(DbType)SqlDbType.Int,4,tokenstatus),
			                        };
            string sqlstring = string.Format("UPDATE [bt_buaasso] SET [tokenstatus] = @tokenstatus WHERE [ssouid] = @ssouid");
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        

    }
}
