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
        /// 清理失效的节点
        /// </summary>
        /// <returns></returns>
        int CleanExpirePeer();

        /// <summary>
        /// 获得用户正在上传的种子数
        /// </summary>
        /// <returns></returns>
        int GetPeerUserUploadCount(int userid);
        /// <summary>
        /// 获得用户正在下载的种子数
        /// </summary>
        /// <returns></returns>
        int GetPeerUserDownloadCount(int userid);
        /// <summary>
        /// 获得指定种子的上传数
        /// </summary>
        /// <returns></returns>
        int GetPeerSeedUploadCount(int seedid);
        /// <summary>
        /// 获得指定种子的IPv6上传数
        /// </summary>
        /// <returns></returns>
        int GetPeerSeedIPv6UploadCount(int seedid);
        /// <summary>
        /// 获得指定种子的上传数
        /// </summary>
        /// <returns></returns>
        int GetPeerSeedDownloadCount(int seedid);


        ///// <summary>
        ///// 更新指定的peer信息，只更新PeerID
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //int UpdatePeerInfoIDOnly(PrivateBTPeerInfo peerinfo);
        ///// <summary>
        ///// 更新指定的peer信息，同时更新PeerID，更新除seedid,uid（三要素之二）和firsttime之外的18-3项
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //int UpdatePeerInfoPeerID(PrivateBTPeerInfo peerinfo, bool ipv6tracker);
        ///// <summary>
        ///// 锁定指定的peer信息
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //int LockPeer(PrivateBTPeerInfo peerinfo);
        ///// <summary>
        ///// 解锁指定的peer信息
        ///// </summary>
        ///// <param name="peerinfo"></param>
        ///// <returns></returns>
        //int UnLockPeer(PrivateBTPeerInfo peerinfo);
        ///// <summary>
        ///// 获得种子数
        ///// </summary>
        ///// <param name="userstat">用户状态：1上传，2下载，3发布，4完成，0时用户id失效</param>
        ///// <returns></returns>
        //int GetUserSeedCount(int userid, int userstat);
        ///// <summary>
        ///// 将指定时间未更新的IPv6地址置为IP_NULL
        ///// </summary>
        ///// <returns></returns>
        //int UpdatePeerIPv6NoResponse(DateTime timelimit);
        ///// <summary>
        ///// 删除指定IP正在下载指定种子的peer信息
        ///// </summary>
        ///// <param name="ip"></param>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //int DeletePeerInfo(string ip, int seedid);
        /// <summary>
        /// 获得对应SeedId的种子的Peer节点信息
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetPeerList(int seedid);
        /// <summary>
        /// 获得对应uid的Peer节点信息，
        /// 用于计算保种奖励
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetPeerList(int uid, bool seeding);
        //C//2014.12.07///// <summary>
        ///// 阻止用户同时使用双Tracker，寻找seedid相等，ipv6addip = 指定ip或者peerid=指定的数量
        ///// </summary>
        ///// <returns></returns>
        //int GetPeerCountV4V6(string ip, int seedid, string peerid);
        /// <summary>
        /// 获得指定种子的制定uid或者ip的peer节点对应的用户数（不计算重复用户的peer），0或者空为不限
        /// </summary>
        /// <returns></returns>
        int GetPeerUserCount(int userstatus);
        ///// <summary>
        ///// 【EX除去指定uid】获得指定种子的指定uid的peer节点数，0或者空为不限
        ///// </summary>
        ///// <returns></returns>
        //int GetPeerCount(int userid, string peerid, int seedid);
        /// <summary>
        /// 判断是否存在同peerid的其他节点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="peerid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        int IsExistsOtherPeer(int uid, string peerid, int seedid);
        /// <summary>
        /// 判断是否存在同peerid的其他节点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="peerid"></param>
        /// <param name="seedid"></param>
        /// <returns></returns>
        int IsExistsPeer(int uid, int seedid, bool isdownload);
        /// <summary>
        /// 获得指定种子的制定uid或者ip的peer节点数，0或者空为不限
        /// </summary>
        /// <returns></returns>
        int GetPeerCount(int uid, int seedid, bool isdownload);
        ///// <summary>
        ///// 获得指定seedid，指定uid，置顶peerid的记录数（这三项是确定peer的项目）
        ///// </summary>
        ///// <returns></returns>
        //int GetPeerCount(PrivateBTPeerInfo peerinfo);
        /// <summary>
        /// 插入peer信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int InsertPeerInfo(PrivateBTPeerInfo peerinfo);
        ///// <summary>
        ///// 获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //DataTable GetPeerList(int seedid, bool upload, int maxcount);
        /// <summary>
        /// 【只获得IP，供tracker使用】获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetPeerListTracker(int seedid, bool upload, int maxcount, bool withpeerid);
        /// <summary>
        /// 【只获得IP，供tracker使用，不区分上传下载】获取指定seedid的PeerList，指定正在上传或者下载，正在上传者按照最后访问排序，正在下载者按照完成率排序bt_peer_getpeerlist_fortracker
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        DataTable GetPeerListTrackerAll(int seedid, int maxcount, bool withpeerid);
        
        
        
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（2项）：
        /// v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int UpdatePeerInfo_NoTraffic(PrivateBTPeerInfo peerinfo, bool IsIPv6Request);
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（4项）：
        /// upload，uploadspeed，v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int UpdatePeerInfo_UpTrafficOnly(PrivateBTPeerInfo peerinfo, bool IsIPv6Request);
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（7项）：
        /// upload，uploadspeed，download，downloadspeed，percentage，v4last/v6last，lasttime
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int UpdatePeerInfo_UpDownTrafficOnly(PrivateBTPeerInfo peerinfo, bool IsIPv6Request);
        /// <summary>
        /// 【存储过程】更新指定（3要素seedid,uid,peerid）的peer信息，只更新如下信息（13项）：
        /// upload，uploadspeed，download，downloadspeed，percentage，v4last/v6last，lasttime
        /// ip，ipv6ip，ipv6addip，isipv6，port，seed
        /// 以下内容不会被更新：
        /// client，firsttime，totalupload，totaldownload，以及3要素seedid，uid，peerid
        /// 更换client的行为应该被记录为警告事件
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int UpdatePeerInfo(PrivateBTPeerInfo peerinfo, bool ipv6tracker);
        /// <summary>
        /// 获得当前总下载速度
        /// </summary>
        /// <returns></returns>
        decimal GetPeerTotalSpeed();
        /// <summary>
        /// 获得指定的peer信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        IDataReader GetPeerInfo(PrivateBTPeerInfo peerinfo);
        /// <summary>
        /// 获得指定的peer信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        IDataReader GetPeerInfo_OldDownload(PrivateBTPeerInfo peerinfo);
        /// <summary>
        /// 删除指定的peer信息
        /// </summary>
        /// <param name="peerinfo"></param>
        /// <returns></returns>
        int DeletePeerInfo(PrivateBTPeerInfo peerinfo);
        /// <summary>
        /// 删除指定UID正在下载指定种子的peer信息，只删除正在下载的
        /// </summary>
        /// <returns></returns>
        int DeletePeerInfo(int uid, int seedid);
        /// <summary>
        /// 重置peer列表，删除该用户的所有记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        int ResetPeer(int userid);
        /// <summary>
        /// 获取所有长时间无响应的Peer的UID项目,mode=1清理core2的种子，否则不清理
        /// </summary>
        /// <returns></returns>
        DataTable GetPeerUidListNoResponse(DateTime timelimit, int mode);
        /// <summary>
        /// 获取所有长时间无响应的Peer的Seedid项目,mode=1清理core2的种子，否则不清理
        /// </summary>
        /// <returns></returns>
        DataTable GetPeerSeedIdListNoResponse(DateTime timelimit, int mode);
        /// <summary>
        /// 删除所有长时间无响应的Peer项目,mode=1清理core2的种子，否则不清理
        /// </summary>
        /// <returns></returns>
        int DeletePeerListNoResponse(DateTime timelimit, int mode);
    }
}