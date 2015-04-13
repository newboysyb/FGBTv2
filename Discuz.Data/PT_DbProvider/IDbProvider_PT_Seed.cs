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

        int UpdateSeedTrafficLog();

        /// <summary>
        /// 增加种子的当天完成数
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        int AddSeedFinishedToday(int seedid, int uid);
        /// <summary>
        /// 清零种子当天完成数
        /// </summary>
        /// <returns></returns>
        int ClearSeedFinishedToday();


        //////////////////////////////////////////////////////////////////////////
        //info

        /// <summary>
        /// 获取状态正常（status为2/3）SeedinfoTracker
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfoTracker(string infohash);
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo，简单信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfoShort(int seedid);
        /// <summary>
        /// 【临时解决】获取状态正常（status为2/3）Seedinfo，简单信息
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfoShort(string infohash);
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo，基本信息，用户贴内信息显示
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfo(int seedid);
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo完全版信息，用于发种信息检测
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfoFull(string infohash_c);
        /// <summary>
        /// 获取任意状态的Seedinfo完全版信息，用于发种信息检测
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfoFullAllStatus(string infohash_c);
        /// <summary>
        /// 获取状态正常（status为2/3）Seedinfo完全版信息，用于编辑等
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfoFull(int seedid);
        /// <summary>
        /// 获取Seedinfo完全版信息，用于编辑等
        /// </summary>
        /// <param name="infohash"></param>
        /// <returns></returns>
        IDataReader GetSeedInfoFullAllStatus(int seedid);

        //////////////////////////////////////////////////////////////////////////
        //list

        /// <summary>
        /// 获取热门种子列表
        /// </summary>
        /// <param name="num"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        DataTable GetHotSeedinfoList(int num, int seedtype);
        /// <summary>
        /// 获得种子列表
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedInfoList(int numperpage, int pageindex, int topseedcount, int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, int orderby, bool asc, string notin);
        /// <summary>
        /// 获得相应分类的置顶种子表
        /// </summary>
        /// <returns></returns>
        DataTable GetTopSeedInfoList(int type);
        /// <summary>
        /// 按seedid列表获取种子信息
        /// </summary>
        /// <param name="seedidlist"></param>
        /// <returns></returns>
        DataTable GetSeedInfoList(string seedidlist);
        /// <summary>
        /// 获取指定seedidlist中，与用户状态相关的种子id列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成</param>
        /// <param name="seedidlist">种子id列表字符串</param>
        /// <returns></returns>
        DataTable GetSeedIdList(int userid, int userstat, string seedidlist);
        /// <summary>
        /// 获得种子数
        /// </summary>
        /// <returns></returns>
        int GetSeedInfoCount(int seedtype, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin);
        /// <summary>
        /// 重新获得相应分类的置顶种子ID数组
        /// </summary>
        /// <returns></returns>
        DataTable GetTopSeedIdListNew(int seedtype);

        /// <summary>
        /// 获取指定类别种子id列表
        /// </summary>
        /// <param name="seedtype"></param>
        /// <param name="numperpage"></param>
        /// <returns></returns>
        DataTable GetSeedIdListIntTablebyType(int seedtype, int numperpage);
        /// <summary>
        /// 获取目前总优惠操作点数
        /// </summary>
        /// <param name="seedtype"></param>
        /// <param name="numperpage"></param>
        /// <returns></returns>
        int GetSeedOpValueSUM(int seedtype, int numperpage);
        /// <summary>
        /// 获得种子列表RSS专用
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedRssList(int numperpage, int pageindex, int seedtype, int withinfo);
        /// <summary>
        /// 获得种子总大小
        /// </summary>
        /// <param name="type">种子种类</param>
        /// <param name="userid">用户id</param>
        /// <param name="userstat">用户状态：1上传，2下载，3发布，4完成</param>
        /// <param name="seedstat">种子状态：1活种，2IPv4，3IPv6，4死种</param>
        /// <param name="orderby">排序：1文件数，2大小，3种子数，4下载中，5完成数，6总流量，7存活时间</param>
        /// <param name="keywords">搜索关键词</param>
        /// <returns></returns>
        decimal GetSeedSumSize(int type, int userid, int userstat, int seedstat, string keywords, int keywordsmode, string notin);


        //////////////////////////////////////////////////////////////////////////
        //list schedule task
        
        
        /// <summary>
        /// 获得指定时间内，存活时间不足的种子列表
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedIdListNoSeed(DateTime postdatetime, int live);
        /// <summary>
        /// 获得指定时间之后一直无种子的种子列表
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedIdListNoSeed(DateTime lastlive);
        /// <summary>
        /// 获取指定downloadratioexpiredate日期前的优惠种子列表，用于自动调整流量系数
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        DataTable GetSeedIdListDownloadRatioExpire(DateTime downloadratioexpiredate);
        /// <summary>
        /// 获取指定ratioexpiredate日期前的优惠种子列表，用于自动调整流量系数
        /// </summary>
        /// <param name="timelimit"></param>
        /// <returns></returns>
        DataTable GetSeedIdListUploadRatioExpire(DateTime uploadratioexpiredate);



        //////////////////////////////////////////////////////////////////////////
        //insert


        /// <summary>
        /// 【存储过程】插入种子，并返回种子id
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        int InsertSeed(PTSeedinfo seedinfo);
        /// <summary>
        /// 插入种子，并返回种子id
        /// 表格共26项，插入25项，不包括seedid
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        int InsertSeedShort(PTSeedinfo seedinfo);
        /// <summary>
        /// 插入seed_detail表，种子附加项目
        /// 表格共25项，插入1+5+19=25项，全部内容
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        int InsertSeedDetail(PTSeedinfo seedinfo);
        /// <summary>
        /// 插入seed_tracker表，种子附加项目
        /// 表格共7项，插入7项，全部内容
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        int InsertSeedTracker(PTSeedinfo seedinfo);

        //////////////////////////////////////////////////////////////////////////
        //update

        /// <summary>
        /// 设置置顶种子
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="top">true置顶，false取消置顶</param>
        /// <returns></returns>
        int UpdateSeedTop(int seedid, bool top);
        /// <summary>
        /// 【存储过程】更新种子状态，全部表，status，0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        int UpdateSeedStatus(int seedid, int status);
        /// <summary>
        /// 根据RSS类型更新bt_seed表相应字段accrss、keeprss和pubrss
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="rssstatus"></param>
        /// <returns></returns>
        int UpdateSeedbyRssType(int seedid, int rsstype, int rssstatus);
        ///// <summary>
        ///// 更新种子状态bt_seed.status，0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //int UpdateSeedStatus(int seedid, int status);
        ///// <summary>
        ///// 更新种子状态bt_seed_tracker.status，0 未上传，1 已上传，2 正常，3 过期休眠，4 一般删除，5 自删除，6 禁止的种子
        ///// </summary>
        ///// <param name="seedid"></param>
        ///// <returns></returns>
        //int UpdateSeedStatusTracker(int seedid, int status);
        /// <summary>
        /// 【存储过程】更新全部表，指定种子的下载系数和过期时间
        /// </summary>
        /// <returns></returns>
        int UpdateSeedRatio(int seedid, float downloadratio, DateTime downloadratioexpiredate, float uploadratio, DateTime uploadratioexpiredate);
        ///// <summary>
        ///// 更新bt_seed表，指定种子的下载系数和过期时间
        ///// </summary>
        ///// <returns></returns>
        //int UpdateSeedRatio(int seedid, double downloadratio, DateTime downloadratioexpiredate, double uploadratio, DateTime uploadratioexpiredate);
        ///// <summary>
        ///// 更新bt_seed表，指定种子的下载系数和过期时间
        ///// </summary>
        ///// <returns></returns>
        //int UpdateSeedRatioTracker(int seedid, double downloadratio, double uploadratio);
        /// <summary>
        /// 【存储过程】更新种子状态，全部表，包含更新种子信息
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        int UpdateSeedEditWithSeed(PTSeedinfo seedinfo);
        /// <summary>
        /// 【存储过程】更新种子状态，全部表，不包含更新种子信息
        /// </summary>
        /// <param name="seedinfo"></param>
        /// <returns></returns>
        int UpdateSeedEditWithOutSeed(PTSeedinfo seedinfo);
        ///// <summary>
        ///// 更新种子状态，bt_seed表，包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //int UpdateSeedEditShortWithSeed(PTSeedinfo seedinfo);
        ///// <summary>
        ///// 更新种子状态，bt_seed表，不包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //int UpdateSeedEditShortWithOutSeed(PTSeedinfo seedinfo);
        ///// <summary>
        ///// 更新种子状态，bt_seed_detail表，包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //int UpdateSeedEditDetailWithSeed(PTSeedinfo seedinfo);
        ///// <summary>
        ///// 更新种子状态，bt_seed_detail表，不包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //int UpdateSeedEditDetailWithOutSeed(PTSeedinfo seedinfo);
        ///// <summary>
        ///// 更新种子状态，bt_seed_tracker表，包含更新种子信息
        ///// </summary>
        ///// <param name="seedinfo"></param>
        ///// <returns></returns>
        //int UpdateSeedEditTrackerWithSeed(PTSeedinfo seedinfo);
        
        /// <summary>
        /// 更新种子最后完成时间
        /// </summary>
        /// <param name="seedid"></param>
        /// <returns></returns>
        int UpdateSeedLastFinish(int seedid, int coincount);
        /// <summary>
        /// announce页面更新bt_seed表，种子（开始、完成、停止）动作，需要更新上传下载数，除ipv6数据以外均为增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        int UpdateSeedAnnounce(int seedid, int upload, int download, int finished, int ipv6, decimal uploadtraffic, decimal downloadtraffic, bool add, int oldup, int olddown);
        /// <summary>
        /// announce页面更新bt_seed表，非增量，均为绝对数值
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        int UpdateSeedAnnounce(int seedid, int upload, int download, int finished, int ipv6);
        /// <summary>
        /// announce页面更新bt_seed表，非增量，均为绝对数值
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        int UpdateSeedAnnounce(int seedid, int upload, int download, int ipv6);
        /// <summary>
        /// announce页面更新bt_seed表，种子中间状态更新，只更新上传数，增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        int UpdateSeedAnnounceUpTrafficOnly(int seedid, decimal uploadtraffic);
        /// <summary>
        /// announce页面更新bt_seed表，种子中间状态更新，只更新下载数，增量
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="download"></param>
        /// <param name="finished"></param>
        /// <param name="ipv6"></param>
        /// <param name="downloadtraffic"></param>
        /// <param name="uploadtraffic"></param>
        /// <returns></returns>
        int UpdateSeedAnnounceTrafficOnly(int seedid, decimal uploadtraffic, decimal downloadtraffic);
        /// <summary>
        /// 【存储过程】更新seed_top表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int UpdateTopSeedList(int type);

        /// <summary>
        /// 更新seed和detail表，live和lastseeder
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="live"></param>
        /// <param name="uid"></param>
        /// <param name="usernmae"></param>
        /// <returns></returns>
        int UpdateSeedLive(int seedid, int live, int uid);

        //////////////////////////////////////////////////////////////////////////
        //out update views,reply

        /// <summary>
        /// 更新种子浏览量，增量
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="viewcount">浏览量</param>
        /// <returns>成功返回1，否则返回0</returns>
        int UpdateSeedTopicViewCount(int tid, int viewCount);

        /// <summary>
        /// 更新种子浏览量(准确值，从topic表获取)
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <returns>成功返回1，否则返回0</returns>
        int UpdateSeedTopicViewCountAccurate(int tid, int seedid);

        /// <summary>
        /// 列新种子的回复数
        /// </summary>
        /// <param name="tid">主题ID</param>
        /// <param name="postTableid">回复表ID</param>
        int UpdateSeedTopicReplyCount(int tid);
        /// <summary>
        /// 获取种子搜索缓存
        /// </summary>
        /// <param name="seedstatus"></param>
        /// <param name="seedtype"></param>
        /// <param name="keywords"></param>
        /// <param name="notin"></param>
        /// <returns></returns>
        string GetSeedSearchCache(int searchmode, int seedstatus, int seedtype, string keywords);
        /// <summary>
        /// 插入种子搜索缓存
        /// </summary>
        /// <param name="seedstatus"></param>
        /// <param name="seedtype"></param>
        /// <param name="keywords"></param>
        /// <param name="notin"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        int InsertSeedSearchCache(int searchmode, int seedstatus, int seedtype, string keywords, string result);
        /// <summary>
        /// 清理种子搜索缓存
        /// </summary>
        /// <returns></returns>
        int CleanSeedSearchCache();
        /// <summary>
        /// 种子搜索
        /// </summary>
        /// <param name="searchmode"></param>
        /// <param name="seedstatus"></param>
        /// <param name="seedtype"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        DataTable GetSeedSearchList(int searchmode, int seedstatus, int seedtype, string keywords, int userid, int userstat);
    }
}