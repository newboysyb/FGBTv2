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
        /// 添加用户登录记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ip"></param>
        /// <param name="type">1.cookie；2.password；3.CNGI</param>
        /// <param name="ok">1.pass；2.fail</param>
        /// <param name="time"></param>
        /// <param name="url"></param>
        /// <param name="agent"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int AddUserLoginRecord(int uid, string ip, int type, int ok, DateTime time, string url, string agent, string msg)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@ip",(DbType)SqlDbType.NChar, 60, ip),
                                        DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                                        DbHelper.MakeInParam("@ok",(DbType)SqlDbType.Int, 4, ok),
                                        DbHelper.MakeInParam("@time",(DbType)SqlDbType.DateTime, 8, time),
                                        DbHelper.MakeInParam("@url",(DbType)SqlDbType.NChar, 100, url),
                                        DbHelper.MakeInParam("@agent",(DbType)SqlDbType.NChar, 100, agent),
                                        DbHelper.MakeInParam("@msg",(DbType)SqlDbType.NChar, 100, msg),
                                  };
            string sqlstring = "INSERT INTO [bt_loginlog] ([uid],[ip],[type],[ok],[time],[url],[agent],[msg]) ";
            sqlstring += " VALUES(@uid, @ip, @type, @ok, @time, @url, @agent, @msg)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取用户IP地址所在区域id
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        public int GetUserIPRegionId(string IPHeader)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@ipheader",(DbType)SqlDbType.VarChar, 30, IPHeader),
                                  };
            string sqlstring = "SELECT [ipregionid] FROM [access_ipregion] WITH(NOLOCK) WHERE [ipheader] = @ipheader";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }
        /// <summary>
        /// 插入用户IP地址所在区域，并获取id
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        public int InsertUserIPRegion(string IPHeader)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@ipheader",(DbType)SqlDbType.VarChar, 30, IPHeader),
                                  };
            string sqlstring = "INSERT INTO [access_ipregion] ([ipheader]) VALUES (@ipheader); SELECT SCOPE_IDENTITY()";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取用户agentid，用户浏览器字符串
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        public int GetUserAgentId(string UserAgent)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@agent",(DbType)SqlDbType.VarChar, 300, UserAgent),
                                  };
            string sqlstring = "SELECT [agentid] FROM [access_agent] WITH(NOLOCK) WHERE [agent] = @agent";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }
        /// <summary>
        /// 插入用户agent，并获取id，用户浏览器字符串
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        public int InsertUserAgent(string UserAgent)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@agent",(DbType)SqlDbType.VarChar, 300, UserAgent),
                                  };
            string sqlstring = "INSERT INTO [access_agent] ([agent]) VALUES (@agent); SELECT SCOPE_IDENTITY()";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 获取用户domainid，域名编号
        /// </summary>
        /// <param name="DomainName"></param>
        /// <returns></returns>
        public int GetUserDomainId(string DomainName)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@domainname",(DbType)SqlDbType.VarChar, 50, DomainName),
                                  };
            string sqlstring = "SELECT [domainid] FROM [access_domain] WITH(NOLOCK) WHERE [domainname] = @domainname";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
        }
        /// <summary>
        /// 插入用户domain，并获取id
        /// </summary>
        /// <param name="DomainName"></param>
        /// <returns></returns>
        public int InsertUserDomainName(string DomainName)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@domainname",(DbType)SqlDbType.VarChar, 50, DomainName),
                                  };
            string sqlstring = "INSERT INTO [access_domain] ([domainname]) VALUES (@domainname); SELECT SCOPE_IDENTITY()";
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms), -1);
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);
        }
        /// <summary>
        /// 添加访问记录
        /// </summary>
        /// <param name="uid">用户ID，无ID记为-1</param>
        /// <param name="username">用户名</param>
        /// <param name="acclevel">访问级别，普通用户1，荣誉版主2，实习版主3，版主4，超级版主5，管理员6，后台访问+50，危险访问+100</param>
        /// <param name="accresult">访问结果，成功1，失败2</param>
        /// <param name="acctype">访问方式，Cookie维持1，CNGI维持3，Cookie登陆11，CNGI登陆13，SSO登陆15，密码登陆21，CNGI绑定登陆23，SSO绑定登陆25，后台登陆31，邀请注册登陆41，CNGI注册43，SSO注册45</param>
        /// <param name="accdate">访问日期</param>
        /// <param name="acctime">访问时间</param>
        /// <param name="ipregion">IP地址范围ID</param>
        /// <param name="iptail">IP地址 除去范围的部分</param>
        /// <param name="agentid">用户浏览器代码ID</param>
        /// <param name="accdomain">用户访问域名ID</param>
        /// <param name="url">用户访问的地址</param>
        /// <param name="md5">用户凭证 截取前5位</param>
        /// <returns></returns>
        public int InsertUserAccessLog(int uid, string username, int acclevel, int accresult, int acctype, DateTime accdate, TimeSpan acctime, int ipregion, string iptail, int agentid, int accdomain, string url, string md5, string rkey)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@username",(DbType)SqlDbType.NChar, 20, username),

                                        DbHelper.MakeInParam("@acctype",(DbType)SqlDbType.TinyInt, 1, acctype),
                                        DbHelper.MakeInParam("@acclevel",(DbType)SqlDbType.TinyInt, 1, acclevel),
                                        DbHelper.MakeInParam("@accresult",(DbType)SqlDbType.TinyInt, 1, accresult),

                                        DbHelper.MakeInParam("@accdate",(DbType)SqlDbType.Date, 3, accdate),
                                        DbHelper.MakeInParam("@acctime",(DbType)SqlDbType.Time, 3, acctime),

                                        DbHelper.MakeInParam("@ipregion",(DbType)SqlDbType.Int, 4, ipregion),
                                        DbHelper.MakeInParam("@iptail",(DbType)SqlDbType.VarChar, 25, iptail),

                                        DbHelper.MakeInParam("@agentid",(DbType)SqlDbType.Int, 4, agentid),
                                        
                                        DbHelper.MakeInParam("@accdomain",(DbType)SqlDbType.SmallInt, 2, accdomain),
                                        DbHelper.MakeInParam("@url",(DbType)SqlDbType.VarChar, 300, url),

                                        DbHelper.MakeInParam("@md5",(DbType)SqlDbType.VarChar, 5, md5),
                                        DbHelper.MakeInParam("@rkey",(DbType)SqlDbType.VarChar, 5, rkey),
                                  };
            string sqlstring = "INSERT INTO [access_log] ([uid],[username],[acctype],[acclevel],[accresult],[accdate],[acctime],[ipregion],[iptail],[agentid],[accdomain],[url],[md5],[rkey]) ";
            sqlstring += " VALUES(@uid, @username, @acctype, @acclevel, @accresult, @accdate, @acctime, @ipregion, @iptail, @agentid, @accdomain, @url, @md5, @rkey)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring, parms);

        }

        /// <summary>
        /// 获取符合条件最早的记录
        /// </summary>
        /// <param name="acclevel"></param>
        /// <param name="acctype"></param>
        /// <param name="accresult"></param>
        /// <returns></returns>
        public DateTime GetFirstUserAccessLogDate(int acclevel, int acctype, int accresult)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@acctype",(DbType)SqlDbType.TinyInt, 1, acctype),
                                        DbHelper.MakeInParam("@acclevel",(DbType)SqlDbType.TinyInt, 1, acclevel),
                                        DbHelper.MakeInParam("@accresult",(DbType)SqlDbType.TinyInt, 1, accresult),
                                  };
            string sqlstring = "SELECT TOP(1)[accdate] FROM [access_log] WITH(NOLOCK) WHERE [acctype] = @acctype AND [acclevel] = @acclevel AND [accresult] = @accresult ORDER BY [accid] ASC";
            DateTime firstdate = DateTime.Now;
            try
            {
                firstdate = TypeConverter.ObjectToDateTime(DbHelper.ExecuteScalar(CommandType.Text, sqlstring, parms));
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            	return DateTime.Now;
            }
            return firstdate;
        }

        public int DeleteUserAccessLog(DateTime accdate, int acclevel, int acctype, int accresult, int count)
        {


            if (GetFirstUserAccessLogDate(acclevel, acctype, accresult) < accdate.AddDays(-5))
            {
                DbParameter[] parms = {
                                        DbHelper.MakeInParam("@acctype",(DbType)SqlDbType.TinyInt, 1, acctype),
                                        DbHelper.MakeInParam("@acclevel",(DbType)SqlDbType.TinyInt, 1, acclevel),
                                        DbHelper.MakeInParam("@accresult",(DbType)SqlDbType.TinyInt, 1, accresult),
                                        DbHelper.MakeInParam("@count",(DbType)SqlDbType.Int, 1, count),
                                        DbHelper.MakeInParam("@accdate",(DbType)SqlDbType.Date, 8, accdate),
                                  };
                //string sqlstring = "DELETE FROM [access_log] WHERE [accid] IN ";
                //sqlstring += "(SELECT TOP (@count)[accid] FROM [access_log] WITH(NOLOCK) WHERE [acctype] = @acctype AND [acclevel] = @acclevel AND [accresult] = @accresult ORDER BY [accid] ASC)";
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "bt_accesslog_clear", parms);

            }
            else return 0;

        }
    }
}