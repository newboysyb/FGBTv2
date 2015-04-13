using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;
using System.Text;

namespace Discuz.Web
{
    /// <summary>
    /// 显示种子内包含的文件
    /// </summary>
    public class showseedinfo : PageBase
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        public DataTable seedoplist = new DataTable();
        public string handlekey = DNTRequest.GetString("handlekey");
        public PTSeedinfo seedinfo;
        public string imdblink = "";
        public string rationote = "";
        public int seedid = DNTRequest.GetInt("seedid", 0);

        protected override void ShowPage()
        {
            infloat = 1;

            if (userid < 0)
            {
                AddErrLine("你尚未登录");
                return;
            }
            // 获取该种子的信息
            seedinfo = PTSeeds.GetSeedInfoFull(seedid);
            // 如果该种子不存在
            if (seedinfo.SeedId < 1)
            {
                AddErrLine("不存在的种子ID");
                return;
            }

            int topicid = seedinfo.TopicId;
            // 获取该主题的信息
            TopicInfo topic = Topics.GetTopicInfo(topicid);
            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine("不存在的主题ID");
                return;
            }

            ForumInfo forum = Forums.GetForumInfo(topic.Fid);

            //添加判断特殊用户的代码
            if (!Forums.AllowViewByUserId(forum.Permuserlist, userid))
            {
                if (!Forums.AllowView(forum.Viewperm, usergroupid))
                {
                    AddErrLine("您没有查看该板块的权限");
                    return;
                }
            }
            //seedoplist = PrivateBT.GetSeedModLog(seedid);
            if (seedinfo.Type == 1) imdblink = PTTools.GetImdbLink(seedinfo.Info3);

            //蓝种剩余
            rationote = PrivateBT.GetRatioNote(seedinfo, 0);
        }
    }
}
