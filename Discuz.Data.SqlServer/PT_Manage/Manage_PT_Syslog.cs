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
//BT系统错误日志

namespace Discuz.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        public int ClearnSystemLog()
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddDays(-60)),
                                  };
            string sqlstring = "DELETE FROM [bt_syslog] WHERE [time] < @time";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        public int ClearnSystemDebugLog()
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, DateTime.Now.AddDays(-60)),
                                  };
            string sqlstring = "DELETE FROM [bt_syslogdebug] WHERE [time] < @time";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }


        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public int InsertErrorLog(string errortype, string message)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@errortype",(DbType)SqlDbType.NChar, 20, errortype),
                                       DbHelper.MakeInParam("@message",(DbType)SqlDbType.NChar, 200, message),
                                       DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                  };
            string sqlstring = "INSERT INTO [bt_error] ([time],[errortype],[message]) VALUES(@time, @errortype, @message)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public int InsertSystemLog(short logtype, short logstatus, string logaction, string message)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                       DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                       DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),
                                       DbHelper.MakeInParam("@message",(DbType)SqlDbType.NText, 0, message),
                                       DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                       
                                  };
            string sqlstring = "INSERT INTO [bt_syslog] ([time],[logtype],[logstatus],[logaction],[message]) VALUES(@time, @logtype, @logstatus, @logaction, @message)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        public int InsertSystemLogDebug(short logtype, short logstatus, string logaction, string message)
        {
            DbParameter[] parms = {
                                       DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                       DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                       DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),
                                       DbHelper.MakeInParam("@message",(DbType)SqlDbType.NText, 0, message),
                                       DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                       
                                  };
            string sqlstring = "INSERT INTO [bt_syslogdebug] ([time],[logtype],[logstatus],[logaction],[message]) VALUES(@time, @logtype, @logstatus, @logaction, @message)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 插入BUAASSO日志
        /// </summary>
        /// <returns></returns>
        public int InsertBuaaSSOLog(int ssoid, short logtype, short logstatus, string logaction, string message)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@ssoid",(DbType)SqlDbType.Int, 4, ssoid),
                                       DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                       DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                       DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),
                                       DbHelper.MakeInParam("@message",(DbType)SqlDbType.NVarChar, 500, message),
                                       DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                       
                                  };
            string sqlstring = "INSERT INTO [bt_buaassolog] ([time],[ssoid],[logtype],[logstatus],[logaction],[message]) VALUES(@time, @ssoid, @logtype, @logstatus, @logaction, @message)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }

        /// <summary>
        /// 插入CNGI日志
        /// </summary>
        /// <returns></returns>
        public int InsertCNGILog(string cngi_school, string cngi_name, short logtype, short logstatus, string logaction, string message)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@cngi_school",(DbType)SqlDbType.NVarChar, 100, logaction),
                                      DbHelper.MakeInParam("@cngi_name",(DbType)SqlDbType.NVarChar, 100, logaction),
                                       DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                       DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                       DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),
                                       DbHelper.MakeInParam("@message",(DbType)SqlDbType.NVarChar, 500, message),
                                       DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                       
                                  };
            string sqlstring = "INSERT INTO [bt_cngilog] ([time],[cngi_school],[cngi_name],[logtype],[logstatus],[logaction],[message]) VALUES(@time, @cngi_school, @cngi_name, @logtype, @logstatus, @logaction, @message)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public DataTable GetSysLogTable(int pageindex, int numperpage, int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                                      DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int, 4, pageindex),
                                      DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                      DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                      DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),     
                                      DbHelper.MakeInParam("@start",(DbType)SqlDbType.DateTime, 8, start), 
                                      DbHelper.MakeInParam("@end",(DbType)SqlDbType.DateTime, 8, end), 
                                   };

            string sqlstring = "SELECT TOP(@numperpage) [id],[time],[logtype],[logstatus],[logaction],[message]";
            sqlstring += " FROM [bt_syslog] ";

            string fromwhere = "";
            if (logtype > 0) fromwhere += "[logtype] = @logtype ";
            if (logstatus > 0 && !stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] = @logstatus ";
            else if (logstatus > 0 && stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] >= @logstatus ";
            if (logaction != "") fromwhere += (fromwhere == "" ? "" : "AND ") + "[logaction] = @logaction ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] > ISNULL((SELECT MAX([id]) FROM [bt_syslog] WHERE [time] < @start),(SELECT MIN([id]) FROM [bt_syslog])) ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] <= ISNULL((SELECT MAX([id]) FROM [bt_syslog] WHERE [time] < @end),(SELECT MAX([id]) FROM [bt_syslog])) ";
            
            sqlstring += fromwhere == "" ? "" : "WHERE " + fromwhere;

            if (pageindex > 1)
            {
                sqlstring += (fromwhere == "" ? "WHERE " : " AND ") + "[id] < (SELECT MIN([id]) FROM (";
                sqlstring += "SELECT TOP(@numperpage * @pageindex - @numperpage) [id] FROM [bt_syslog] ";
                sqlstring += (fromwhere == "" ? "" : "WHERE " + fromwhere) + " ORDER BY [id] DESC) AS tblTmp) ";
            }
            sqlstring += "  ORDER BY [id] DESC";

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public int GetSysLogCount(int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, 200),
                                      DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                      DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                      DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),   
                                      DbHelper.MakeInParam("@start",(DbType)SqlDbType.DateTime, 8, start), 
                                      DbHelper.MakeInParam("@end",(DbType)SqlDbType.DateTime, 8, end), 
                                  };

            string sqlstring = "SELECT COUNT([id])";
            sqlstring += " FROM [bt_syslog] ";

            string fromwhere = "";
            if (logtype > 0) fromwhere += "[logtype] = @logtype ";
            if (logstatus > 0 && !stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] = @logstatus ";
            else if (logstatus > 0 && stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] >= @logstatus ";
            if (logaction != "") fromwhere += (fromwhere == "" ? "" : "AND ") + "[logaction] = @logaction ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] > ISNULL((SELECT MAX([id]) FROM [bt_syslog] WHERE [time] < @start),(SELECT MIN([id]) FROM [bt_syslog])) ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] <= ISNULL((SELECT MAX([id]) FROM [bt_syslog] WHERE [time] < @end),(SELECT MAX([id]) FROM [bt_syslog])) ";

            sqlstring += fromwhere == "" ? "" : "WHERE " + fromwhere;

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        }


        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public DataTable GetSysDebugLogTable(int pageindex, int numperpage, int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, numperpage),
                                      DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int, 4, pageindex),
                                      DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                      DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                      DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),     
                                      DbHelper.MakeInParam("@start",(DbType)SqlDbType.DateTime, 8, start), 
                                      DbHelper.MakeInParam("@end",(DbType)SqlDbType.DateTime, 8, end), 
                                   };

            string sqlstring = "SELECT TOP(@numperpage) [id],[time],[logtype],[logstatus],[logaction],[message]";
            sqlstring += " FROM [bt_syslogdebug] ";

            string fromwhere = "";
            if (logtype > 0) fromwhere += "[logtype] = @logtype ";
            if (logstatus > 0 && !stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] = @logstatus ";
            else if (logstatus > 0 && stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] >= @logstatus ";
            if (logaction != "") fromwhere += (fromwhere == "" ? "" : "AND ") + "[logaction] = @logaction ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] > ISNULL((SELECT MAX([id]) FROM [bt_syslogdebug] WHERE [time] < @start),(SELECT MIN([id]) FROM [bt_syslog])) ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] <= ISNULL((SELECT MAX([id]) FROM [bt_syslogdebug] WHERE [time] < @end),(SELECT MAX([id]) FROM [bt_syslog])) ";

            sqlstring += fromwhere == "" ? "" : "WHERE " + fromwhere;

            if (pageindex > 1)
            {
                sqlstring += (fromwhere == "" ? "WHERE " : " AND ") + "[id] < (SELECT MIN([id]) FROM (";
                sqlstring += "SELECT TOP(@numperpage * @pageindex - @numperpage) [id] FROM [bt_syslogdebug] ";
                sqlstring += (fromwhere == "" ? "" : "WHERE " + fromwhere) + " ORDER BY [id] DESC) AS tblTmp) ";
            }
            sqlstring += "  ORDER BY [id] DESC";

            return DbHelper.ExecuteDataset(CommandType.Text, sqlstring, parms).Tables[0];
        }
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        public int GetSysDebugLogCount(int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end)
        {
            DbParameter[] parms = {
                                      DbHelper.MakeInParam("@numperpage",(DbType)SqlDbType.Int, 4, 200),
                                      DbHelper.MakeInParam("@logtype",(DbType)SqlDbType.TinyInt, 1, logtype),
                                      DbHelper.MakeInParam("@logstatus",(DbType)SqlDbType.TinyInt, 1, logstatus),
                                      DbHelper.MakeInParam("@logaction",(DbType)SqlDbType.NVarChar, 50, logaction),   
                                      DbHelper.MakeInParam("@start",(DbType)SqlDbType.DateTime, 8, start), 
                                      DbHelper.MakeInParam("@end",(DbType)SqlDbType.DateTime, 8, end), 
                                  };

            string sqlstring = "SELECT COUNT([id])";
            sqlstring += " FROM [bt_syslogdebug] ";

            string fromwhere = "";
            if (logtype > 0) fromwhere += "[logtype] = @logtype ";
            if (logstatus > 0 && !stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] = @logstatus ";
            else if (logstatus > 0 && stabove) fromwhere += (fromwhere == "" ? "" : "AND ") + "[logstatus] >= @logstatus ";
            if (logaction != "") fromwhere += (fromwhere == "" ? "" : "AND ") + "[logaction] = @logaction ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] > ISNULL((SELECT MAX([id]) FROM [bt_syslogdebug] WHERE [time] < @start),(SELECT MIN([id]) FROM [bt_syslog])) ";
            if (timelimit) fromwhere += (fromwhere == "" ? "" : "AND ") + "[id] <= ISNULL((SELECT MAX([id]) FROM [bt_syslogdebug] WHERE [time] < @end),(SELECT MAX([id]) FROM [bt_syslog])) ";

            sqlstring += fromwhere == "" ? "" : "WHERE " + fromwhere;

            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms).ToString(), 0);
        }
    }
}