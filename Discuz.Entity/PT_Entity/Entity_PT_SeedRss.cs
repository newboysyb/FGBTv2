using System;
using System.Collections.Generic;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// RSS信息类
    /// </summary>
    public class PTSeedRssinfo
    {
        /// <summary>
        /// RSS ID
        /// </summary>
        public int Rssid = -1;
        /// <summary>
        /// Seed ID
        /// </summary>
        public int Seedid = -1;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDateTime = new DateTime(1970, 1, 1);
        /// <summary>
        /// Rss类别：0默认，1自动加速，2优质资源长期保种，3外链资源ihome
        /// </summary>
        public int RssType = -1;
        /// <summary>
        /// Rss状态：0默认，-5删除，-4失效，1添加，2下载种子，3下载中，4做种中，5停止中
        /// </summary>
        public int RssStatus = -1;
        /// <summary>
        /// Rss添加理由（自动加速）：0默认，1两小时peer
        /// </summary>
        public int RssCondition = -1;
        /// <summary>
        /// 种子对应的主题id
        /// </summary>
        public int Topicid = -1;
        /// <summary>
        /// RSS信息最后更新时间
        /// </summary>
        public DateTime LastUpdated = new DateTime(1970, 1, 1);
        /// <summary>
        /// 种子类别
        /// </summary>
        public int SeedType = -1;
        /// <summary>
        /// 种子状态
        /// </summary>
        public int SeedStatus = -1;
        /// <summary>
        /// 种子名称
        /// </summary>
        public string SeedTitle = "";
        /// <summary>
        /// 种子大小
        /// </summary>
        public decimal SeedSize = -1M;
        /// <summary>
        /// 种子对应的帖子Id
        /// </summary>
        public int PostId = -1;

    }
}
