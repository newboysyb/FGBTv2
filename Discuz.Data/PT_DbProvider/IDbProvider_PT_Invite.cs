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
        /// 获得邀请人
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        int GetInviter(int userid);
        /// <summary>
        /// 获取被邀请用户中指定奖励级别和流量的用户
        /// </summary>
        /// <param name="AwardLevel"></param>
        /// <param name="AwardUpload"></param>
        /// <returns></returns>
        DataTable GetInviteUser(int AwardLevel, decimal AwardUpload);
        /// <summary>
        /// 设置指定邀请用户的奖励级别
        /// </summary>
        /// <param name="RegisterId"></param>
        /// <param name="AwardLevel"></param>
        /// <returns></returns>
        int SetInviteUserLevel(int RegisterId, int AwardLevel);
        /// <summary>
        /// 获得对应userid的用户的所有邀请码
        /// </summary>
        /// <param name="userid"></param>
        DataTable GetInviteCodeList(int userid, int pagesize, int currentpage);
        /// <summary>
        /// 获得对应userid的用户的未使用邀请码
        /// </summary>
        /// <param name="userid"></param>
        DataTable GetInviteCodeList(int userid, int pagesize, int currentpage, DateTime buytime);
        /// <summary>
        /// 获得对应userid的用户的used或过期邀请码
        /// </summary>
        /// <param name="userid"></param>
        DataTable GetInviteCodeListUsed(int userid, int pagesize, int currentpage, DateTime buytime);
        /// <summary>
        /// 获得对应userid的用户的未使用邀请码数量
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码DataTable</returns>
        int GetInviteCodeListUsedCount(int userid, DateTime buytime);
        /// <summary>
        /// 获得对应userid的用户的未使用邀请码数量
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>邀请码DataTable</returns>
        int GetInviteCodeListCount(int userid, DateTime buytime);
        /// <summary>
        /// 获得对应id用户的邀请码数量
        /// </summary>
        /// <param name="userid"></param>
        int GetInviteCodeCount(int userid);
        /// <summary>
        /// 获得对应userid的用户所邀请的用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pagesize"></param>
        /// <param name="currentpage"></param>
        /// <returns></returns>
        DataTable GetInviteUsersList(int userid, int pagesize, int currentpage);
        /// <summary>
        /// 获得对应id用户的邀请用户数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        int GetInviteUserCount(int userid);
        /// <summary>
        /// 为对应userid的用户添加指定的邀请码
        /// </summary>
        /// <param name="userid">id</param>
        /// <param name="invitecode">邀请码</param>
        /// <returns>数据库更改行数</returns>
        int AddInviteCode(int userid, string invitecode);
        /// <summary>
        /// 验证邀请码是否存在，并将使用标记为True，返回数据库更改行数
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns>数据库更改行数</returns>
        int VerifyInviteCode(string invitecode);
        /// <summary>
        /// 将邀请码标记为已经使用，返回数据库更改行数
        /// </summary>
        /// <param name="invitecode"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        int UseInviteCode(string invitecode, int userid);
        /// <summary>
        /// 注册用户失败时，将邀请码恢复，返回数据库更改行数
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        int RestoreInviteCode(string invitecode);
        /// <summary>
        /// 获取该注册码的主人id
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        IDataReader GetInviteCodeBuyer(string invitecode);
        /// <summary>
        /// 获取该注册码的购买时间
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        IDataReader GetInviteCodeTime(string invitecode);
    }
}