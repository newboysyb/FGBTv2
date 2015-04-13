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
        /// 获取热门保种RSS_KEEPHOT列表
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedRssListKeepHot(int downloadcountlimit, int dayscheck, int daysbefore, int daysafter, int totalcount);
        /// <summary>
        /// 获取热门老种RSS_OLDHOT列表
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedRssListOldHot(int downloadcountlimit, int dayscheck, int daysbefore, int totalcount);
        /// <summary>
        /// 获取热门老种RSS_OLDHOT_NMB列表
        /// </summary>
        /// <returns></returns>
        DataTable GetSeedRssListOldHotNMB(int downloadcountlimit, int dayscheck, int daysbefore, int totalcount);
        /// <summary>
        /// 获取热门下载种子RSS_ACC_SH列表
        /// </summary>
        /// <param name="maxlogcount"></param>
        /// <param name="mindownload"></param>
        /// <returns></returns>
        DataTable GetSeedRssListHotDownload(int maxlogcount, int mindownload);
        /// <summary>
        /// 添加RSS记录
        /// </summary>
        /// <returns>数据库更改行数</returns>
        int AddSeedRss(PTSeedRssinfo rssinfo);
        /// <summary>
        /// 更新RSS条目
        /// </summary>
        /// <param name="rssinfo"></param>
        /// <returns></returns>
        int UpdateSeedRss(PTSeedRssinfo rssinfo);
        /// <summary>
        /// 删除RSS条目
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <returns></returns>
        int DeleteSeedRss(int seedid, int rsstype);
        /// <summary>
        /// 判断RSS条目是否存在
        /// </summary>
        /// <param name="seedid"></param>
        /// <param name="rsstype"></param>
        /// <param name="adddatetime"></param>
        /// <returns></returns>
        bool IsExistsSeedRss(int seedid, int rsstype, DateTime adddatetime);
        /// <summary>
        /// 获取SeedRSS表
        /// </summary>
        /// <param name="numperpage"></param>
        /// <param name="pageindex"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        DataTable GetSeedRssListbyType(int numperpage, int pageindex, int seedtype, int rsstype);
        /// <summary>
        /// 获取SeedRSS表
        /// </summary>
        /// <param name="numperpage"></param>
        /// <param name="pageindex"></param>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        DataTable GetSeedRssWithDetailListbyType(int numperpage, int pageindex, int seedtype, int rsstype);
        /// <summary>
        /// 获取SeedRSS 总数
        /// </summary>
        /// <param name="seedtype"></param>
        /// <returns></returns>
        int GetSeedRssCountbyType(int seedtype, int rsstype);


    }
}
