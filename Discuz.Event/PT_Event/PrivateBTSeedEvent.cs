using System;
using System.Text;
using Discuz.Forum.ScheduledEvents;
using Discuz.Forum;

namespace Discuz.Event
{
    /// <summary>
    /// 有关论坛的计划任务
    /// </summary>
    public class PrivateBTSeedEvent : IEvent
    {
        #region IEvent 成员
        
        //private static int curseedid = 0;
        
        public void Execute(object state)
        {
            //种子下载系数修改
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTSeeds.UpdateSeedRatio));

            //清理死亡种子
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTSeeds.DeleteSeedNoSeed));

            //删除过期Abt种子
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTAbt.AbtDeleteSeed));

            //种子下载系数修改
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTAbt.AbtDeleteSeedPublishNoSeed));

            //更新种子流量日志表
            //PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PTSeeds.UpdateSeedTrafficLog));

            //删除过期用户访问记录，仅在夜间操作
            PTRunTaskEvents.RunTaskEvents(new TaskEventDelegate(PrivateBT.DoAccessLogCleanTaskEvent));
        }

        #endregion
    }


    /// <summary>
    /// 有关论坛的计划任务
    /// </summary>
    public class PrivateBTGC : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            System.GC.Collect();
        }

        #endregion
    }
}