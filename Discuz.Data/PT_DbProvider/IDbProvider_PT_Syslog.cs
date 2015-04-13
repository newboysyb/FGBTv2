using System;
using System.Data;
using System.Text;

#if NET1
#else
using Discuz.Common.Generic;
#endif

using Discuz.Entity;
using System.Data.Common;


namespace Discuz.Data
{
    public partial interface IDataProvider
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        int ClearnSystemLog();
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        int ClearnSystemDebugLog();
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        int InsertErrorLog(string errortype, string message);
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        int InsertSystemLog(short logtype, short logstatus, string logaction, string message);
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <returns></returns>
        int InsertSystemLogDebug(short logtype, short logstatus, string logaction, string message);

        /// <summary>
        /// 插入CNGI日志
        /// </summary>
        /// <returns></returns>
        int InsertCNGILog(string cngi_school, string cngi_name, short logtype, short logstatus, string logaction, string message);

        /// <summary>
        /// 插入BUAASSO日志
        /// </summary>
        /// <returns></returns>
        int InsertBuaaSSOLog(int ssoid, short logtype, short logstatus, string logaction, string message);



                /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        DataTable GetSysLogTable(int pageindex, int numperpage, int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end);
                /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        int GetSysLogCount(int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end);
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        DataTable GetSysDebugLogTable(int pageindex, int numperpage, int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end);
        /// <summary>
        /// 获取系统日志
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="logstatus"></param>
        /// <param name="logaction"></param>
        /// <returns></returns>
        int GetSysDebugLogCount(int logtype, int logstatus, string logaction, bool stabove, bool timelimit, DateTime start, DateTime end);
    }
}