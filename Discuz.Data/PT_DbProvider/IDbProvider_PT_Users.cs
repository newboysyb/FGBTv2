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
        /// 执行签到
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="lastcheckincount"></param>
        /// <param name="lastcheckintime"></param>
        /// <returns></returns>
        int DoCheckIn(int uid, int lastcheckincount, DateTime lastcheckintime, int newcheckincount, DateTime newcheckintime);
                /// <summary>
        /// 添加签到日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int AddCheckInLog(int uid, DateTime date, DateTime time, int count);
        /// <summary>
        /// 添加签到日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int GetCheckInRank(int uid, DateTime date, DateTime time, int count);
        /// <summary>
        /// 获得今天签到次序
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        int GetCheckInRank(int uid);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        int ReActiveUser(int uid);
        /// <summary>
        /// 校验用户复活校验码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="VFCode"></param>
        /// <param name="invitecodeid"></param>
        /// <returns></returns>
        int VerifyUserReActiveVFCode(int uid, string VFCode, int invitecodeid);

        /// <summary>
        /// 记录用户复活校验码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="VFCode"></param>
        /// <returns></returns>
        int CreateUserReActiveVFCode(int uid, string VFCode);

        /// <summary>
        /// 更新用户的RatioProtection
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratioprotection"></param>
        /// <returns></returns>
        int SetUserRatioProtection(int uid, int ratioprotection);
        /// <summary>
        /// 获取用户随机RKey（登陆一次改变一次）
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDataReader GetUserRKey(int uid);
        /// <summary>
        /// 更新用户随机RKey
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        int SetOnlineRKey(int uid, string rkey);
        /// <summary>
        /// 更新用户随机RKey
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rkey"></param>
        /// <returns></returns>
        int SetUserRKey(int uid, string rkey);
        /// <summary>
        /// 禁封2010.09.30日之后注册，注册两个月之后下载达不到20G的用户
        /// </summary>
        /// <returns></returns>
        int UpdateUser60Ban();

        /// <summary>
        /// 由username获得用户密码信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetUserPasswordSHAInfoToReader(string username);
        /// <summary>
        /// 由uid获得用户密码信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetUserPasswordSHAInfoToReader(int uid);
        /// <summary>
        /// 更新用户sha密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="passwordsha"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        int UpdateUserPasswordSHAInfo(int uid, string passwordsha, string salt);
        /// <summary>
        /// 由passkey获得用户信息
        /// </summary>
        /// <returns></returns>
        IDataReader GetPTUserInfoToReaderForTracker(string passkey);
        /// <summary>
        /// 由uid获得用户信息
        /// </summary>
        /// <returns></returns>
        IDataReader GetPTUserInfoToReaderForPagebase(int uid);
        /// <summary>
        /// 由uid获得用户信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetPTUserInfoToReader(int uid);
        /// <summary>
        /// 获取共享率排行
        /// </summary>
        /// <param name="bASC"></param>
        /// <returns></returns>
        DataTable GetUserRatioList(bool bASC);
        /// <summary>
        /// 更新BT用户信息Ratio
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        int UpdateUserInfo_Ratio(int uid, double ratio);
        /// <summary>
        /// 更新BT用户信息lastkeepreward
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        int UpdateUserInfo_LastKeepReward(int uid, DateTime updatetime);
        /// <summary>
        /// 更新BT用户信息Ratio，UploadCount，DownloadCount
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        int UpdateUserInfo_Ratio(int uid, double ratio, int uploadcount, int downloadcount);
        /// <summary>
        /// 获取所有用户id表
        /// </summary>
        /// <returns></returns>
        DataTable GetAllUserID();
        /// <summary>
        /// 检查该Passkey是否存在，返回该Passkey存在数量
        /// </summary>
        /// <param name="passkey"></param>
        /// <returns></returns>
        int CheckPasskey(string passkey);
        /// <summary>
        /// 为指定uid的用户更新passkey，返回影响行数
        /// </summary>
        /// <param name="passkey"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        int UpdatePasskey(string passkey, int uid);
        /// <summary>
        /// 获得今天总下载量
        /// </summary>
        /// <returns></returns>
        decimal GetUserTodayDownload();
        /// <summary>
        /// 获得历史总下载量,1为全部物理，2为今天物理，3为全部统计下载，4为统计今天下载，5为统计全部上传，6为统计今天上传
        /// </summary>
        /// <returns></returns>
        decimal GetUserTotalDownload(int totaltype);
        /// <summary>
        /// 更新BT用户信息，realupload/realdownload/extcredits3/extcredits4,5,6,7,8/finished均为增量
        /// </summary>
        /// <param name="btuserinfo"></param>
        /// <returns></returns>
        int UpdateUserInfo_Tracker(PTUserInfo bt);
        /// <summary>
        /// 更新BT用户信息，realupload/realdownload/extcredits3/extcredits4,5,6,7,8/finished均为增量,此外还更新ratio,upcout,downcount,lastactivity
        /// </summary>
        /// <param name="btuserinfo"></param>
        /// <returns></returns>
        int UpdateUserInfo_TrackerWithCredits(PTUserInfo btuserinfo);
        /// <summary>
        /// 更新BT用户发布数
        /// </summary>
        /// <param name="btuserinfo"></param>
        /// <returns></returns>
        int UpdateUserInfo_RefreashPublish(int userid, int published);
        /// <summary>
        /// 禁封2周内无真实上传的用户
        /// </summary>
        /// <returns></returns>
        int UpdateUser2WeeksBan();
        /// <summary>
        /// 修改用户VIP SET
        /// </summary>
        /// <returns></returns>
        int UpdateUserVIPSet();
        /// <summary>
        /// 修改用户VIP REMOVE
        /// </summary>
        /// <returns></returns>
        int UpdateUserVIPRemove();
        /// <summary>
        /// 修改用户DailyTraffic SET
        /// </summary>
        /// <returns></returns>
        int UpdateUserDailyTraffic();
    }
}