using System;
using System.Text;
using Discuz.Forum.ScheduledEvents;
using Discuz.Forum;
using Discuz.Entity;

namespace Discuz.Event
{
    /// <summary>
    /// Web计划任务，10分钟执行一次
    /// </summary>
    public class PrivateBTWebEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            //更新十大（Web缓存）
            PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTTopTen.UpdateTopTenbyScheduleTask));

            //更新最新热门种子
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTSeeds.UpdateHotSeedinfoListEvent));
        }

        #endregion
    }
}