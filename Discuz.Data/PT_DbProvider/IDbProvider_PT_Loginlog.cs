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
        int AddUserLoginRecord(int uid, string ip, int type, int ok, DateTime time, string url, string agent, string msg);
                /// <summary>
        /// 获取用户IP地址所在区域id
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        int GetUserIPRegionId(string IPHeader);
        /// <summary>
        /// 插入用户IP地址所在区域，并获取id
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        int InsertUserIPRegion(string IPHeader);
        /// <summary>
        /// 获取用户agentid，用户浏览器字符串
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        int GetUserAgentId(string UserAgent);
        /// <summary>
        /// 插入用户agent，并获取id，用户浏览器字符串
        /// </summary>
        /// <param name="IPHeader"></param>
        /// <returns></returns>
        int InsertUserAgent(string UserAgent);
        /// <summary>
        /// 获取用户domainid，域名编号
        /// </summary>
        /// <param name="DomainName"></param>
        /// <returns></returns>
        int GetUserDomainId(string DomainName);
        /// <summary>
        /// 插入用户domain，并获取id
        /// </summary>
        /// <param name="DomainName"></param>
        /// <returns></returns>
        int InsertUserDomainName(string DomainName);
        /// <summary>
        /// 插入访问记录
        /// </summary>
        /// <param name="uid">用户ID，无ID记为-1</param>
        /// <param name="username">用户名</param>
        /// <param name="acclevel">访问级别，普通用户1，荣誉版主2，实习版主3，版主4，超级版主5，管理员6，后台访问+50，危险访问+100</param>
        /// <param name="accresult">访问结果，成功1，失败2</param>
        /// <param name="acctype">访问方式，Cookie普通1，Cookie HTTPS 2，CNGI普通3，CNGI HTTPS 4，SSO普通5，SSO HTTPS 6，密码普通7，密码HTTPS 8</param>
        /// <param name="accdate">访问日期</param>
        /// <param name="acctime">访问时间</param>
        /// <param name="ipregion">IP地址范围ID</param>
        /// <param name="iptail">IP地址 除去范围的部分</param>
        /// <param name="agentid">用户浏览器代码ID</param>
        /// <param name="accdomain">用户访问域名ID</param>
        /// <param name="url">用户访问的地址</param>
        /// <param name="md5">用户凭证 截取前5位</param>
        /// <returns></returns>
        int InsertUserAccessLog(int uid, string username, int acclevel, int accresult, int acctype, DateTime accdate, TimeSpan acctime, int ipregion, string iptail, int agentid, int accdomain, string url, string md5, string rkey);
        /// <summary>
        /// 删除指定条件的访问记录（临时采用accid判断，由于时间项目没有索引，数据量过大）
        /// </summary>
        /// <param name="accdate"></param>
        /// <param name="acclevel"></param>
        /// <param name="acctype"></param>
        /// <param name="accresult"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int DeleteUserAccessLog(DateTime accdate, int acclevel, int acctype, int accresult, int count);
    
    }
}