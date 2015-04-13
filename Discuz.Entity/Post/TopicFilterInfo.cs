using System;

namespace Discuz.Entity
{
    /// <summary>
    /// 帖子信息描述类
    /// </summary>
    public class TopicListFilterInfo
    {
        /// <summary>
        /// 是否强制进行Count统计更新，不适用缓存数据
        /// </summary>
        public bool ForceCount = false;


        /// <summary>
        /// 板块ID
        /// </summary>
        public int Fid = -1;
        /// <summary>
        /// 是否删除，获取规则为>displayorder，因此此处值为-1时为正常主题，数据库中：-1删除，0正常，1板块置顶，2分区置顶，3全局置顶
        /// </summary>
        public int Displayorder = -1;
        /// <summary>
        /// 是否关闭，默认为包括关闭的主题，数据库中大于0为关闭的主题
        /// </summary>
        public int Closed = -1;
        /// <summary>
        /// 主题分类
        /// </summary>
        public int Topictypeid = -1;
        /// <summary>
        /// 特殊主题类别，2、3除外
        /// </summary>
        public int Special = -1;
        /// <summary>
        /// 距离现在时间限制
        /// </summary>
        public int Interval = -1;



        /// <summary>
        /// 排序依据
        /// </summary>
        public string Orderby = "lastpostid";
        /// <summary>
        /// 是否为DESC，逆序（默认为desc）
        /// </summary>
        public bool Desc = true;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int Pagesize = 50;
        /// <summary>
        /// 当前页
        /// </summary>
        public int Pageindex = 1;
        /// <summary>
        /// 首页起始（置顶消耗的位置）
        /// </summary>
        public int Pagestart = 0;

        public TopicListFilterInfo()
        {

        }
        public TopicListFilterInfo(int fid, int displayorder, int closed, int topictypeid, int special, int interval) 
        {
            Fid = fid;
            Displayorder = displayorder;
            Closed = closed;
            Topictypeid = topictypeid;
            Special = special;
            Interval = interval;
        }
        public TopicListFilterInfo(int fid, int displayorder, int closed, int topictypeid, int special, int interval, string orderby, bool desc, int pagesize, int pageindex, int pagestart)
        {
            Fid = fid;
            Displayorder = displayorder;
            Closed = closed;
            Topictypeid = topictypeid;
            Special = special;
            Interval = interval;

            Orderby = orderby;
            Desc = desc;
            Pagesize = pagesize;
            Pageindex = pageindex;
            Pagestart = pagestart;
        }

    }
}
