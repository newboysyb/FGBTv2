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
        /// 获取论坛统计信息缓存HTML
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        IDataReader GetAllStatsHTML(string statstype);
        /// <summary>
        /// 插入论坛统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <param name="statsvalue"></param>
        /// <returns></returns>
        int InstertAllStatsHTML(string statstype, string statsvalue);
        /// <summary>
        /// 获取论坛本身的统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        IDataReader GetAllStats(string statstype);
        /// <summary>
        /// 设置数据更新锁，防止多次更新，论坛统计信息
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        int LockAllStatsHTML(string statstype);
        /// <summary>
        /// 解除数据更新锁，论坛统计信息
        /// </summary>
        /// <param name="statstype"></param>
        /// <returns></returns>
        int UnLockAllStatsHTML(string statstype);
        /// <summary>
        /// 更新论坛统计信息缓存
        /// </summary>
        /// <param name="statstype"></param>
        /// <param name="statsvalue"></param>
        /// <returns></returns>
        int UpdateAllStatsHTML(string statstype, string statsvalue);
        /// <summary>
        /// 插入BT状态
        /// </summary>
        /// <returns></returns>
        int InsertServerStats(PrivateBTServerStats btstats, bool day);
        /// <summary>
        /// 锁定BT状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int LockServerStats(int id);
        /// <summary>
        /// 解除锁定BT状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int UnLockServerStats(int id);
        /// <summary>
        /// 更新BT状态
        /// </summary>
        /// <returns></returns>
        int UpdateServerStats(PrivateBTServerStats btstats, int id);
        /// <summary>
        /// 插入BT状态
        /// </summary>
        /// <returns></returns>
        int InsertServerStats(PrivateBTServerStats btstats);
        /// <summary>
        /// 读取BT状态
        /// </summary>
        /// <returns></returns>
        IDataReader GetServerStatsToReader(int id);
        /// <summary>
        /// 更新论坛统计信息
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="topiccount"></param>
        /// <param name="postcount"></param>
        /// <param name="todaypost"></param>
        /// <returns></returns>
        int UpdateForumStatic(int forumid, int topiccount, int postcount, int todaypost);
    }
}