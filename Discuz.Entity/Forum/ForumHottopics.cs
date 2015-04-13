using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discuz.Entity
{
    /// <summary>
    /// 板块热门主题类
    /// </summary>
    [Serializable]
    public class ForumHottopics
    {
        public int Tid = -1;
        public string TopicTitle = "";
    }
    /// <summary>
    /// 板块热门主题类
    /// </summary>
    [Serializable]
    public class ForumHottopicsList
    {
        public int Fid = -1;
        public List<ForumHottopics> HotTopicList = new List<ForumHottopics>(); 
    }
}
