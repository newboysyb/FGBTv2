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
        /// 获得对应SeedId的种子的最近曾经活动用户ID信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetUserIdListActiveInSeed(int seedid, DateTime lastupdate);

        /// <summary>
        /// 获取指定时间后，某个种子新增流量记录数，可反映种子在指定事件后的下载次数
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        int GetNewTrafficRecordCount(int seedid, DateTime datetime);

        /// <summary>
        /// 获得对应SeedId的种子的历史Peer节点信息/历史流量信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetPeerHistoryList(int seedid);
        /// <summary>
        /// 插入个人单种上传下载数据
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        int InsertPerUserSeedTraffic(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6);


        /// <summary>
        /// 更新个人单种上传下载数据，保种过程
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        int UpdatePerUserSeedTraffic(int seedid, int userid, decimal addupload, int addkeeptime);
        /// <summary>
        /// 更新个人单种上传下载数据，保种过程，包含IP地址更新
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        int UpdatePerUserSeedTraffic_WithIP(int seedid, int userid, decimal addupload, int addkeeptime, string ipv4, string ipv6);
        /// <summary>
        /// 更新个人单种上传下载数据，下载过程，更新lastleft和lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        int UpdatePerUserSeedTraffic_Download(int seedid, int userid, decimal addupload, decimal adddownload, decimal lastleft, decimal lastdownload);
        /// <summary>
        /// 更新个人单种上传下载数据，下载过程，更新lastleft和lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        int UpdatePerUserSeedTraffic_DownloadWithIP(int seedid, int userid, decimal addupload, decimal adddownload, decimal lastleft, decimal lastdownload, string ipv4, string ipv6);
        /// <summary>
        /// 更新个人单种上传下载数据，种子首次更新，更新firstratio、lastpeerid、lastleft、lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        int UpdatePerUserSeedTraffic_SeedFirst(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6, decimal lastleft, decimal lastdownload, string peerid, float firstratio);
        /// <summary>
        /// 更新个人单种上传下载数据，本次任务首次更新，更新lastpeerid、lastleft、lastdownload
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <param name="addupload"></param>
        /// <param name="adddownload"></param>
        /// <param name="ipv4"></param>
        /// <param name="ipv6"></param>
        /// <returns></returns>
        int UpdatePerUserSeedTraffic_SessionFirst(int seedid, int userid, decimal addupload, decimal adddownload, string ipv4, string ipv6, decimal lastleft, decimal lastdownload, string peerid);


        
        /// <summary>
        /// 获取个人单种上传下载数据
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        IDataReader GetPerUserSeedTraffic(int seedid, int userid);
    }
}