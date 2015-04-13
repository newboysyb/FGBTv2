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
        /// 获取TOPTEN Seeds 十大种子
        /// </summary>
        /// <returns></returns>
        DataTable GetTopTenSeeds();
        /// <summary>
        /// 获取TOPTEN Topics 十大主题
        /// </summary>
        /// <returns></returns>
        DataTable GetTopTenTopics();
        /// <summary>
        /// 获取TOPTEN Topics 十大悬赏
        /// </summary>
        /// <returns></returns>
        DataTable GetTopTenBonus();
        /// <summary>
        /// 清零帖子当天回复数
        /// </summary>
        /// <returns></returns>
        int ClearTopicRepliesToday();

        /// <summary>
        /// 获取指定条件的帖子pid在帖子中的排序
        /// </summary>
        /// <param name="_postpramsinfo">参数列表</param>
        /// <returns>指定条件的帖子DataSet</returns>
        int GetPostCountInTopic(int Tid, int Pid, string posttablename);
        /// <summary>
        /// 获得指定TopicId对应的seedid
        /// </summary>
        /// <param name="topicid"></param>
        /// <returns></returns>
        int GetSeedIdByTopicId(int topicid);
        ///// <summary>
        ///// 发帖出错后恢复帖子数
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="forumid"></param>
        ///// <returns></returns>
        //int UpdateSeedPublishError(int userid, int forumid);
        /// <summary>
        ///  更新指定uid和nid的通知的状态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newType"></param>
        void UpdateNoticeNewByUidNid(int uid, int nid, int newType);
        /// <summary>
        ///  更新指定uid和type的通知的状态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newType"></param>
        void UpdateNoticeNewByUidType(int uid, int type, int newType);
    }
}