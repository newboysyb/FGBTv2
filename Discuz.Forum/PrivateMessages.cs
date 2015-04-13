using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Cache;
using Discuz.Common.Generic;

namespace Discuz.Forum
{
    /// <summary>
    /// 短消息操作类
    /// </summary>
    public class PrivateMessages
    {
        /// <summary>
        /// 负责发送新用户注册欢迎信件的用户名称, 该名称同时不允许用户注册
        /// </summary>
        public const string SystemUserName = "系统";


        /// <summary>
        /// 获得指定ID的短消息的内容
        /// </summary>
        /// <param name="pmid">短消息pmid</param>
        /// <returns>短消息内容</returns>
        public static PrivateMessageInfo GetPrivateMessageInfo(int pmid)
        {
            return pmid > 0 ? Discuz.Data.PrivateMessages.GetPrivateMessageInfo(pmid) : null;
        }

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <returns>短消息数量</returns>
        public static int GetPrivateMessageCount(int userId, int folder)
        {
            return userId > 0 ? GetPrivateMessageCount(userId, folder, -1) : 0;
        }

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、2:最近消息（7天内）、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        public static int GetPrivateMessageCount(int userId, int folder, int state)
        {
            return userId > 0 ? Discuz.Data.PrivateMessages.GetPrivateMessageCount(userId, folder, state) : 0;
        }

        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        public static int GetAnnouncePrivateMessageCount()
        {
            DNTCache cache = DNTCache.GetCacheService();
            int announcepmcount = Utils.StrToInt(cache.RetrieveObject("/Forum/AnnouncePrivateMessageCount"), -1);

            if (announcepmcount < 0)
            {
                announcepmcount = Discuz.Data.PrivateMessages.GetAnnouncePrivateMessageCount();
                cache.AddObject("/Forum/AnnouncePrivateMessageCount", announcepmcount);
            }
            return announcepmcount;
        }

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        public static int CreatePrivateMessage(PrivateMessageInfo privatemessageinfo, int savetosentbox)
        {
            //////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////// 
            //【BT修改】每个消息都更新在线用户的短消息数

            int rtvalue = Discuz.Data.PrivateMessages.CreatePrivateMessage(privatemessageinfo, savetosentbox);

            //为在线用户更新短消息数
            int targetolid = OnlineUsers.GetOlidByUid(privatemessageinfo.Msgtoid);
            if (targetolid > 0)
                Users.UpdateUserNewPMCount(privatemessageinfo.Msgtoid, targetolid);

            return rtvalue;

            //原始
            //return Discuz.Data.PrivateMessages.CreatePrivateMessage(privatemessageinfo, savetosentbox);
            //【END BT修改】
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            
        }


        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        public static int DeletePrivateMessage(int userId, string[] pmitemid, string ipdel)
        {
            if (!Utils.IsNumericArray(pmitemid))
                return -1;

            int reval = Discuz.Data.PrivateMessages.DeletePrivateMessages(userId, String.Join(",", pmitemid), ipdel);
            if (reval > 0)
                Discuz.Data.Users.SetUserNewPMCount(userId, Discuz.Data.PrivateMessages.GetNewPMCount(userId));

            //执行数量与提交数量不匹配，由于删除操作执行两条SQL，因为为2倍
            if (reval != 2 * pmitemid.Length)
            {
                PTLog.InsertSystemLog(PTLog.LogType.UserPassword, PTLog.LogStatus.Error, "DEL PM", 
                    string.Format("DELETE PM COUNT UMATCH: UID:{0} --IP:{1} --LIST:{2} --DONE:{3} --PMLIST:{4}", userId, ipdel, pmitemid.Length, reval, String.Join(",", pmitemid)));
            }

            return reval;
        }

        /// <summary>
        /// 删除指定用户的一条短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmid">要删除的短信息ID</param>
        /// <returns>删除记录数</returns>
        public static int DeletePrivateMessage(int userId, int pmid, string ipdel)
        {
            return userId > 0 ? DeletePrivateMessage(userId, new string[] { pmid.ToString() }, ipdel) : 0;
        }

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        public static int SetPrivateMessageState(int pmid, byte state)
        {
            return pmid > 0 ? Discuz.Data.PrivateMessages.SetPrivateMessageState(pmid, state) : 0;
        }

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        public static List<PrivateMessageInfo> GetPrivateMessageCollection(int userId, int folder, int pagesize, int pageindex, int readStatus)
        {
            return (userId > 0 && pageindex > 0 && pagesize > 0) ? Discuz.Data.PrivateMessages.GetPrivateMessageCollection(userId, folder, pagesize, pageindex, readStatus) : null;
        }

        /// <summary>
        /// 获得公共消息列表
        /// </summary>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <returns>公共消息列表</returns>
        public static List<PrivateMessageInfo> GetAnnouncePrivateMessageCollection(int pagesize, int pageindex)
        {
            if (pagesize == -1)
                return Discuz.Data.PrivateMessages.GetAnnouncePrivateMessageCollection(-1, 0);
            return (pagesize > 0 && pageindex > 0) ? Discuz.Data.PrivateMessages.GetAnnouncePrivateMessageCollection(pagesize, pageindex) : null;
        }

        /// <summary>
        /// 返回短标题的收件箱短消息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="strwhere">筛选条件</param>
        /// <returns>收件箱短消息列表</returns>
        public static List<PrivateMessageInfo> GetPrivateMessageListForIndex(int userId, int pagesize, int pageindex, int inttype)
        {
            List<PrivateMessageInfo> list = GetPrivateMessageCollection(userId, 0, pagesize, pageindex, inttype);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Message = Utils.GetSubString(list[i].Message, 20, "...");
                }
            }
            return list;
        }

        /// <summary>
        /// 返回最新的一条记录ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static int GetLatestPMID(int userId)
        {
            List<PrivateMessageInfo> list = Discuz.Data.PrivateMessages.GetPrivateMessageCollection(userId, 0, 1, 1, 0);
            int latestpmid = 0;

            foreach (PrivateMessageInfo info in list)
            {
                latestpmid = info.Pmid;
                break;
            }
            return latestpmid;
        }

        /// <summary>
        /// 获取删除短消息的条件
        /// </summary>
        /// <param name="isNew">是否删除新短消息</param>
        /// <param name="postDateTime">发送日期</param>
        /// <param name="msgFromList">发送者列表</param>
        /// <param name="lowerUpper">是否区分大小写</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        /// <param name="isUpdateUserNewPm">是否更新用户短消息数</param>
        /// <returns></returns>
        public static string DeletePrivateMessages(bool isNew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm)
        {
            return "";
            //取消后台删除短消息功能20140508
            //return Data.PrivateMessages.DeletePrivateMessages(isNew, postDateTime, msgFromList, lowerUpper, subject, message, isUpdateUserNewPm);
        }
    } //class end
}
