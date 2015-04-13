using System;
using System.Text;
using Discuz.Forum.ScheduledEvents;
using Discuz.Forum;

namespace Discuz.Event
{
    /// <summary>
    /// 有关论坛的计划任务
    /// </summary>
    public class PrivateBTForumStaticEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            //重新统计论坛贴数
            PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBT.ReSetAllFourmTopicStatic));
        }

        #endregion
    }
}