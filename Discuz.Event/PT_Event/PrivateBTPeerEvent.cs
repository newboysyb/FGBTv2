using System;
using System.Text;
using Discuz.Forum.ScheduledEvents;
using Discuz.Forum;
using Discuz.Entity;

namespace Discuz.Event
{
    /// <summary>
    /// PeerEvent计划任务，5分钟执行一次
    /// </summary>
    public class PrivateBTPeerEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            //清理过期的Peer项目
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBT.CleanPeerListNoResponse));

            //清理过期的Peer项目core2
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBT.CleanPeerListNoResponseCore2));

            //更新统计
            PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBT.UpdateServerStatsTaskEvent));

            //更新十大
            //C//PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTTopTen.UpdateTopTenbyScheduleTask));

            //清理AbtPeer
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTAbt.AbtDeletePeerTaskEvent));

            //升级用户密码的计划任务
            //C//PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTUsers.UpdateUserPass));

        }

        #endregion
    }
}